using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Glorysoft.BC.EIP.Handlers
{

    public class PortTypeChangeReportBlockHandler : AbstractEventHandler
    {
        public PortTypeChangeReportBlockHandler(IPLCContext context)
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
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ PortTypeChangeReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var PortType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortType);

                portService.PortTypeChangeReport(oEQP, PortType, i, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PortTypeChangeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
