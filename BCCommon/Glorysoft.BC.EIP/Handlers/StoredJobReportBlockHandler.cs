using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;
using System.Collections;

namespace Glorysoft.BC.EIP.Handlers
{
    public class StoredJobReportBlockHandler : AbstractEventHandler
    {
        public StoredJobReportBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                //var i = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = plcmsg.EQPName;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ StoredJobReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }

                var JobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                var LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var UnitorPort = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitorPort);
                var UnitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);
                var PortNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortNo);
                var SlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotNumber);
                logicService.StoreInJobEventReport(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, UnitorPort, UnitNumber, PortNo, SlotNumber, txid);

                //var panelID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.PanelID); 
                //var portID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.PortID);
                //var slotID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.SlotID);
                //var carrierID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.CarrierID);
                //int carrierType = 0;
                //int.TryParse(plcmsg.ItemCollection[PLCEventItem.CarrierType], out carrierType);
                //try
                //{
                //    //将PanelID的层数存进DB
                //    var panelInfo = logicService.FindPanelInfo(panelID);
                //    int slotNo;
                //    int.TryParse(slotID, out slotNo);
                //    panelInfo.TSlotID = slotNo;
                //    var ret = logicService.InsertPanelInfo(panelInfo);
                //}
                //catch (Exception ex)
                //{
                //    LogHelper.EIPLog.ErrorFormat("+++ StoredJobReport1BlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
                //}
                //portService.StoreInJobEventReport(eqpName, panelID, portID, slotID, carrierID, carrierType);
                //Unit oUnit = null;
                //logicService.AddPanelTrackHistory(panelID, oUnit, oEQP, "StoreInJobEventReport");
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ StoredJobReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}