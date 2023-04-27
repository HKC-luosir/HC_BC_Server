using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class FetchedOutJobReportBlockHandler : AbstractEventHandler
    {
        public FetchedOutJobReportBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = plcmsg.EQPName;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ FetchedOutJobReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var JobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                var LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var UnitorPort = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitorPort);
                var UnitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);
                var PortNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortNo);
                var SlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotNumber);
                logicService.FetchOutJobEventReport(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, UnitorPort, UnitNumber, PortNo, SlotNumber, txid);

                //var panelID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.PanelID);
                //var portID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.PortID);
                //var oPort = HostInfo.Current.PortList.FirstOrDefault(f => f.PortID == portID);
                //var slotID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.SlotID);
                //var carrierID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.CarrierID);
                //int carrierType = 0;
                //int.TryParse(GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.CarrierType), out carrierType);
                //portService.FetchOutJobEventReport(eqpName, panelID, portID, slotID, carrierID, carrierType);
                //Unit oUnit = null;
                //logicService.AddPanelTrackHistory(panelID, oUnit, oEQP, args.Message.EventName);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ FetchedOutJobReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}