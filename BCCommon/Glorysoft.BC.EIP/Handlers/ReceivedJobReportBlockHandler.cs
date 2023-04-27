using System.Linq;
using Glorysoft.BC.EIP.Common;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{
    public class ReceivedJobReportBlockHandler : AbstractEventHandler
    {
        public ReceivedJobReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ ReceivedJobReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }

                //jobdata 59个item
                List<JobDataInfo> jobdatas = new List<JobDataInfo>();
                int jobcount = 1;//(plcmsg.ItemCollection.Count - 1) / 59;
                for (int iJob = 1; iJob <= jobcount; iJob++)
                {
                    //var suffix = "#" + iJob.ToString();
                    JobDataInfo jobdata = GetEQPJobData(args.Message.EventName, plcmsg.ItemCollection, "");
                    jobdatas.Add(jobdata);
                }
                //var UpstreamPathNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UpstreamPathNumber);

                //int productionType = 0;
                //int.TryParse(GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ProductionType), out productionType);

                //LogHelper.EIPLog.InfoFormat("+++ [ReceivedJobReport1BlockHandler]EQPName:{0},PanelID:{1},PanelGrade:{2} +++", args.Message.EQPName, panelID, grade);
                //Unit oUnit = null;
                //logicService.AddPanelTrackHistory(panelID, oUnit, oEQP, args.Message.EventName);

                logicService.ReceiveJobEventReport(oEQP, i, jobdatas, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ ReceivedJobReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}