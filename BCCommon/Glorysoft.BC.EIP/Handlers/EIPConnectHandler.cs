using Glorysoft.BC.EIP.Common;
using System;
using Glorysoft.BC.Entity;
using System.Linq;

namespace Glorysoft.BC.EIP.Handlers
{
    public class EIPConnectHandler : AbstractEventHandler
    {
        public EIPConnectHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                var eqpName = args.Name;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ EIPConnectHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }
                if (args.EventType==IndexerEventType.Connect)
                {
                    oEQP.IsConnect = "Alive";
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}