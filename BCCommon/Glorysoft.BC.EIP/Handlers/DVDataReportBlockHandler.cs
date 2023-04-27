using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class DVDataReportBlockHandler : AbstractEventHandler
    {
        public DVDataReportBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = plcmsg.EQPName;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ DVDataReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }
                var JobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "JOBID");
                var LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var UnitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);
                var SlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotNumber);
                var RecipeNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeNumber);
                var DVSamplingFlag = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.DVSamplingFlag);

                List<Item> items = new List<Item>();
                //获取各Block对应数据
                string[] datatype = new string[] { "INT", "SI", "FLOAT", "ASCII" };
                foreach (var dtype in datatype)
                {
                    for (int num = 0; num < 3; num++)
                    {
                        var tagName = "LC_EQToCIM_DVData" + dtype + "_" + (oEQP.LocalNo.ToString().PadLeft(2, '0')) + "_01_" + num.ToString().PadLeft(2, '0');
                        var block = PLCContexts.Current.GetBlock(eqpName, tagName);
                        if (block != null)
                        {
                            PLCContexts.Current.ReadFromPLC(block);
                            Item it = new Item();
                            it.ITEMNAME = dtype;
                            var blockname = "DVData" + dtype + "Block";
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
                logicService.DVDataReport(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, UnitNumber, SlotNumber, RecipeNumber, DVSamplingFlag, items, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ DVDataReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}