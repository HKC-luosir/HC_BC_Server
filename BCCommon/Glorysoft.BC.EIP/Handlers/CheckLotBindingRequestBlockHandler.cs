using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;


namespace Glorysoft.BC.EIP.Handlers
{
    public class CheckLotBindingRequestBlockHandler : AbstractEventHandler
    {
        public CheckLotBindingRequestBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ CheckLotBindingRequestBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var jobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                var BLUID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.BLUID);

                logicService.CheckLotBindingRequest(oEQP, jobID, BLUID, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CheckLotBindingRequestBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
