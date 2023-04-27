using System.Linq;
using Glorysoft.BC.EIP.Common;
using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{
    public class PanelProcessEndRequestBlockHandler : AbstractEventHandler
    {
        public PanelProcessEndRequestBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ PanelProcessEndRequestBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }

                var jobId = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                logicService.PanelProcessEndRequest(oEQP, jobId, "", txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PanelProcessEndRequestBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}