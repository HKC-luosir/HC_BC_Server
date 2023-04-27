using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;


namespace Glorysoft.BC.EIP.Handlers
{
    public class VCRStatusReportBlockHandler : AbstractEventHandler
    {
        public VCRStatusReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ VCRStatusReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var VCRNumber = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.VCRNumber);
                var VCRStatus = GetItemValue(args.Message.EventName, args.Message.ItemCollection, PLCEventItem.VCRStatus);
                logicService.VCRStatusReport(oEQP, VCRNumber, VCRStatus, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ VCRStatusReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}