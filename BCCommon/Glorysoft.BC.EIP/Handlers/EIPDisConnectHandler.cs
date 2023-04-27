using Glorysoft.BC.EIP.Common;
using System;
using Glorysoft.BC.Entity;
using System.Linq;

namespace Glorysoft.BC.EIP.Handlers
{
    #region 需求3 1.EIP通讯状态变化 liuyusen 20221010
    public class EIPDisConnectHandler : AbstractEventHandler
    {
        public EIPDisConnectHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ EIPDisConnectHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }
                if (args.EventType==IndexerEventType.Disconnect)
                {
                    oEQP.IsConnect = Consts.IsConnect.Down.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    #endregion
}