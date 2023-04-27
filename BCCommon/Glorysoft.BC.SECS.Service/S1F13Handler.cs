using System;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.SECSwell;

namespace Glorysoft.BC.SECS.Service
{
    public class S1F13Handler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            //判断设备是否回复正确 0为正确 1为异常
            int rtCode = 0;
            string rtMsg = "";
            try
            {
                //var eqpinfo = HostInfo.Current.EQPList[context.Name];
                var trans = data.OrignalMessage as SECSTransaction;
                //收到的消息信息
                var msg = trans.Primary;
                //发送的消息信息
                trans.Secondary = GetReplyData(context, msg);

                //先判断格式是否正确
                if (msg.Root == null || msg.Root.ItemCount != 2)
                {
                    rtCode = 1;//SECSAcknowledge.HANDLER_MISSING_PARAMETERS;
                    rtMsg = "HANDLER_MISSING_PARAMETERS";//nameof(SECSAcknowledge.HANDLER_MISSING_PARAMETERS);
                    SECSLogger.Warn($"{context.Name}, MessageName: {nameof(S1F13Handler)}, {nameof(rtCode)} = 1(HANDLER_MISSING_PARAMETERS)");
                    //发送S9F7（数据格式异常）
                    var tran = context.GetTransaction(9, 7);
                    context.SendMessage(tran);
                    //把错误的命令名字发送给业务逻辑层
                    //eisService.HandlerErrorMessage(context.Name, eqpinfo.eqptype, data.MessageName, rtCode, rtMsg);
                }
                else
                {
                    if (trans.Secondary != null)
                    {
                        trans.Secondary.Root.Item(1).Value = 0;
                        context.ReplyMessage(data);
                    }
                    //var mdln = msg.Root.Item(1).Value.ToString();
                    //var softrev = msg.Root.Item(2).Value.ToString();
                    //eisCmd.Receive_S1F13(context.Name, "", mdln, softrev);
                }
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(S1F13Handler)}", e);
                return;
            }
            finally
            {
            }
        }
    }
}
