using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;


namespace Glorysoft.BC.EIP.Handlers
{
    public class VCRReadCompleteReportBlockHandler : AbstractEventHandler
    {
        public VCRReadCompleteReportBlockHandler(IPLCContext context)
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
                var dict = plcmsg.ItemCollection;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ VCRReadCompleteReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var JobID = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.JobID);
                var LotSequenceNumber = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.LotSequenceNumber);
                var SlotSequenceNumber = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var UnitNumber = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.UnitNumber);
                var VCRNumber = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.VCRNumber);
                var VCRResult = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.VCRResult);

                logicService.VCRReadCompleteReport(oEQP, JobID, LotSequenceNumber, SlotSequenceNumber, UnitNumber, VCRNumber, VCRResult, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ VCRReadCompleteReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}