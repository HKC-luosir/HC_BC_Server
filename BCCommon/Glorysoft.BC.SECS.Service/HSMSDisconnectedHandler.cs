using System;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;

namespace Glorysoft.BC.SECS.Service
{
    /// <summary>
    /// 断开状态
    /// </summary>
    public class HSMSDisconnectedHandler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                SECSLogger.Warn($"{context.Name}, MessageName: {nameof(HSMSDisconnectedHandler)}");
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSDisconnectedHandler)}", e);
            }
        }
    }
}
