using System;
using System.Threading;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.SECSwell;

namespace Glorysoft.BC.SECS.Service
{
    public class S1F4Handler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                var trans = data.OrignalMessage as SECSTransaction;
                //收到的消息信息
                //var msg = trans.Primary;
                //发送的消息信息
                //var reply = trans.Secondary;
                var tag = trans.Tag as AutoResetEvent;
                tag.Set();
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(S1F4Handler)}", e);
                return;
            }
            finally
            {
            }
        }
    }
}
