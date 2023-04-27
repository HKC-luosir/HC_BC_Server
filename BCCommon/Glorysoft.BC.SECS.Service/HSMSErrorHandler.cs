using System;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.SECSwell;

namespace Glorysoft.BC.SECS.Service
{
    public class HSMSErrorHandler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                if (context == null)
                {
                    SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSErrorHandler) + "context=NULL"}");
                    return;
                }
                var trans = data.OrignalMessage as SECSTransaction;
                if (trans == null)
                {
                    SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSErrorHandler) + "trans=NULL"}");
                    return;
                }
                //收到的消息信息
                var msg = trans.Primary;
                if (msg == null)
                {
                    SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSErrorHandler) + "msg=NULL"}");
                    return;
                }
                //发送的消息信息
                var reply = trans.Secondary;
                if (reply == null)
                {
                    SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSErrorHandler) + "reply=NULL"}");
                    return;
                }
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(HSMSErrorHandler)}", e);
            }            
            return;
        }
    }
}
