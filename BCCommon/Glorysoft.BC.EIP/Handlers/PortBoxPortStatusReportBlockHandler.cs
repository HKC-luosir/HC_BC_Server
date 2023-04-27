using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{

    public class PortBoxPortStatusReportBlockHandler : AbstractEventHandler
    {
        public PortBoxPortStatusReportBlockHandler(IPLCContext context)
           : base(context)
        {
        }

        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{2}][{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName, args.Message.TransactionID);
                var i = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ PortBoxPortStatusReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var PortStatus = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortStatus);
                var PortType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortType);
                var BoxID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.BoxID);
                var lotSequenceNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var jobCountInCassetteStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobCountInCassette);
                var jobExistenceSlot1 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot1);
                var jobExistenceSlot2 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot2);
                var jobExistenceSlot3 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot3);
                var jobExistenceSlot = jobExistenceSlot1 + jobExistenceSlot2 + jobExistenceSlot3;
                //字段转Int
                var lotSequenceNumber = 0;
                var jobCountInCassette = 0;

                int.TryParse(lotSequenceNumberStr, out lotSequenceNumber);
                int.TryParse(jobCountInCassetteStr, out jobCountInCassette);
                portService.PortBoxPortStatusReport(oEQP, i, Convert.ToInt32(PortStatus), PortType, BoxID, lotSequenceNumber, jobCountInCassette, jobExistenceSlot, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PortBoxPortStatusReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
