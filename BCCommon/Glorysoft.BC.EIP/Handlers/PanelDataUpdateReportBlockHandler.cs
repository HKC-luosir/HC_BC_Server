using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class PanelDataUpdateReportBlockHandler : AbstractEventHandler
    {
        public PanelDataUpdateReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ PanelDataUpdateReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var jobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                var lotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var slotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);

                var judgeArray = "";
                for (int i = 0; i < 100; i++)
                {
                    var judge = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "PanelJudgeData" + (i + 1));
                    if (!string.IsNullOrEmpty(judge))
                    {
                        judgeArray = judgeArray + (judge + "/");
                    }
                    else
                        break;//如果有一个judge为空，则直接返回，不继续遍历
                }
                if (judgeArray.Length > 0)
                    judgeArray = judgeArray.Substring(0, judgeArray.Length - 1);
                logicService.PanelDataUpdateReport(oEQP, jobID, lotSequenceNumber, slotSequenceNumber, judgeArray, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PanelDataUpdateReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}