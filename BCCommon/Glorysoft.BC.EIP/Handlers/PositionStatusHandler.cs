using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.Auto.Contract;

namespace Glorysoft.BC.EIP.Handlers
{
    public class PositionStatusHandler : AbstractEventHandler
    {
        public PositionStatusHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                //LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = plcmsg.EQPName;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ PositionExist001Handler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                List<GlassExistencePosition> piInfo = new List<GlassExistencePosition>();
                foreach (var key in plcmsg.ItemCollection.Keys)
                {
                    // key PositionExist001
                    var keynum = Convert.ToInt32(key.Replace("PositionExist", ""));
                    var bitValue = plcmsg.ItemCollection[key].ToString();
                    if (bitValue == "1")
                    {
                        piInfo.Add(new GlassExistencePosition() { EQPID = oEQP.EQPID, UnitID = oEQP.UnitID, Position = keynum, Exist = true });
                    }
                }
                //获取Block对应数据
                if (piInfo.Count > 0)
                {
                    var blockName = "RV_EQToCIM_PositionStatus_" + (oEQP.LocalNo.ToString().PadLeft(2, '0')) + "_01_00";
                    var block = PLCContexts.Current.GetBlock(eqpName, blockName);
                    if (block != null)
                    {
                        if (block.BlockCollection.ContainsKey("PositionGlassCodeBlock"))
                        {
                            var data = block.BlockCollection.FirstOrDefault(f => f.Key == "PositionGlassCodeBlock");
                            //Dictionary<string, string> dic = new Dictionary<string, string>();
                            foreach (var item in data.Value.ItemCollection)
                            {
                                // key Position001GlassCodeLotNumber Position001GlassCodeSlotNumber
                                var datanum = Convert.ToInt32(item.Key.Replace("Position", "").Substring(0, 3));
                                if (piInfo.Any(c => c.Position == datanum))
                                {
                                    var pi = piInfo.FirstOrDefault(c => c.Position == datanum);
                                    if (item.Key.Contains("GlassCodeLotNumber"))
                                    {
                                        pi.CassetteSequenceNo = Convert.ToInt32(item.Value.Value.ToString());
                                    }
                                    else if (item.Key.Contains("GlassCodeSlotNumber"))
                                    {
                                        pi.SlotSequenceNo = Convert.ToInt32(item.Value.Value.ToString());
                                    }
                                }
                            }
                        }
                    }

                    logicService.PositionStatusChange(oEQP, piInfo, txid);
                }
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ JobEachPositionHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}