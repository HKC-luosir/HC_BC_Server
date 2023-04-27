using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.SECSwell;
using System;
using System.Threading;

namespace Glorysoft.BC.SECS.Service
{
    public class S1F14Handler : AbstractSECSHandler, ISECSMessageHandler
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

                //eisService.Receive_S1F14(context.Name);
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(S1F14Handler)}", e);
                return;
            }
            finally
            {
            }
        }
    }
}
