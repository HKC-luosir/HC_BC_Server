using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class DefectCodeReportBlockHandler : AbstractEventHandler
    {
        public DefectCodeReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ DefectCodeReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var jobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                var lotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var slotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var defectCodeName = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.DefectCodeName);
                var jobJudgeCode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobJudgeCode);
                var jobGradeCode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobGradeCode);


                logicService.DefectCodeReport(oEQP, jobID, lotSequenceNumber, slotSequenceNumber, defectCodeName, jobJudgeCode, jobGradeCode, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ DefectCodeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}