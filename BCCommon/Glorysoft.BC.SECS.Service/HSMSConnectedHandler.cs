using System;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;

namespace Glorysoft.BC.SECS.Service
{
    public class HSMSConnectedHandler : AbstractSECSHandler, ISECSMessageHandler
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                SECSLogger.Warn($"{context.Name}, MessageName: {nameof(HSMSConnectedHandler)}");
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSConnectedHandler)}", e);
            }
        }
    }
}