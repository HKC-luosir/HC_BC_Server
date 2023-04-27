using System.Linq;
using Glorysoft.BC.EIP.Common;
using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{
    public class JobManualMoveReportBlockHandler : AbstractEventHandler
    {
        public JobManualMoveReportBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var i = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Name;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ JobManualMoveReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }

                var JobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                var LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var JobPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobPosition);
                var ReportOption = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ReportOption);
                var OperatorID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.OperatorID);
                var UnitorPort = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitorPort);
                var UnitNumberorPortNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumberorPortNo);
                var SlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotNumber);
                logicService.JobManualMoveReport(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, JobPosition, ReportOption, OperatorID, UnitorPort, UnitNumberorPortNo, SlotNumber, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ JobManualMoveReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}