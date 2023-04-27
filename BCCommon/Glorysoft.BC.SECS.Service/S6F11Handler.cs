using System;
using System.Threading;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.SECSwell;

namespace Glorysoft.BC.SECS.Service
{
    public class S6F11Handler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                var trans = data.OrignalMessage as SECSTransaction;
                //收到的消息信息
                var msg = trans.Primary;
                //发送的消息信息
                var reply = trans.Secondary;
                reply.Root.Value = (byte)4;
                context.ReplyMessage(data);
                ////这里只是示例 如何取节点值，后续需要抛到logic层去解析
                uint ceid = Convert.ToUInt32(msg.Root.Item(2).Value);
                string cassetteid = msg.Root.Item(3).Item(1).Item(2).Item(1).Value.ToString().Trim();
                ////list示例
                var ListSlotMap = msg.Root.Item(3).Item(1).Item(2);
                if (ListSlotMap.ItemCount > 0)
                {
                    for (int i = 0; i < ListSlotMap.ItemCount; i++)
                    {
                        string slot = ListSlotMap.Item(i + 1).Value.ToString().Trim();
                    }
                }
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(S6F11Handler)}", e);
                return;
            }
            finally
            {
            }
        }
    }
}
