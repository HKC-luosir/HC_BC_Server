using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class CVDataReportBlockHandler : AbstractEventHandler
    {
        public CVDataReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ CVDataReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }
                LogHelper.CVDataLog.Info(plcmsg.ToString());
                List<Item> items = new List<Item>();
                //获取各Block对应数据
                string[] datatype = new string[] { "INT", "SI", "FLOAT", "ASCII" };
                foreach (var dtype in datatype)
                {
                    for (int num = 0; num < 3; num++)
                    {
                        var tagName = "LC_EQToCIM_CVData" + dtype + "_" + (oEQP.LocalNo.ToString().PadLeft(2, '0')) + "_01_" + num.ToString().PadLeft(2, '0');
                        var block = PLCContexts.Current.GetBlock(eqpName, tagName);
                        if (block != null)
                        {
                            PLCContexts.Current.ReadFromPLC(block);
                            LogHelper.CVDataLog.Info(block.ToString());
                            Item it = new Item();
                            it.ITEMNAME = dtype;
                            var blockname = "CVData" + dtype + "Block";
                            if (block.BlockCollection.ContainsKey(blockname))
                            {
                                var data = block.BlockCollection.FirstOrDefault(f => f.Key == blockname);
                                foreach (var item in data.Value.ItemCollection)
                                {
                                    // key Parameter1ID Value1
                                    if (item.Key.Contains("Parameter"))
                                    {
                                        int i = FindInt(item.Key.ToString());
                                        Site si = new Site();
                                        if (item.Value.Value.Trim() != "0")
                                        {
                                            si.ID = i;
                                            si.SITENAME = item.Value.Value.ToString();
                                            it.SITELIST.Add(si);
                                        }
                                    }
                                    else if (item.Key.Contains("Value"))
                                    {
                                        int i = FindInt(item.Key.ToString());
                                        var pi = it.SITELIST.FirstOrDefault(c => c.ID == i);
                                        if (pi != null)
                                            pi.SITEVALUE = item.Value.Value.ToString();
                                    }
                                }
                            }
                            items.Add(it);
                        }
                    }
                }
                logicService.CVDataReport(oEQP, items, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CVDataReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}