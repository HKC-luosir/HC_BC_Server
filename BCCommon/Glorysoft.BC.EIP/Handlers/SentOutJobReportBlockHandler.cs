using System.Linq;
using Glorysoft.BC.EIP.Common;
using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{
    public class SentOutJobReportBlockHandler : AbstractEventHandler
    {
        public SentOutJobReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ SentOutJobReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                //var panelID = GetItemValue(args.Message.EventName,plcmsg.ItemCollection, PLCEventItem.PanelID);
                //Unit oUnit = null;
                //logicService.AddPanelTrackHistory(panelID, oUnit, oEQP, args.Message.EventName);
                //logicService.SendOutJobEventReport(args.Message.EventName,args.Message.EQPName, args.Message.ItemCollection, i);

                //jobdata 59个item
                List<JobDataInfo> jobdatas = new List<JobDataInfo>();
                int jobcount = 1;// (plcmsg.ItemCollection.Count - 1) / 59;
                for (int iJob = 1; iJob <= jobcount; iJob++)
                {
                    //var suffix = "#" + iJob.ToString();
                    JobDataInfo jobdata = GetEQPJobData(args.Message.EventName, plcmsg.ItemCollection, "");
                    jobdatas.Add(jobdata);
                }
                //var DownstreamPathNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.DownstreamPathNumber);
                logicService.SendOutJobEventReport(oEQP, i, jobdatas, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ SentOutJobReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}