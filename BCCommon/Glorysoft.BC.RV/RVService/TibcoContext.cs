using System;
using System.Threading;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.RV.Common;
using Glorysoft.BC.RV.Entity;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.RVService
{
    public class TibcoContext : ITibcoContext
    {
        public string Name { get; private set; }
        public bool IsConnect
        {
            get
            {
                return MessageServiceInfo.IsConnect;
            }
        }

        private MessageService messageServiceInfo;
        public MessageService MessageServiceInfo
        {
            get { return messageServiceInfo; }
            set { messageServiceInfo = value; }
        }
        public TibcoContext(MessageInfo msgInfo)
        {
            Initialize(msgInfo);
        }

        public void Initialize(MessageInfo msgInfo)
        {
            try
            {
                this.Name = msgInfo.Name;
                messageServiceInfo = new MessageService(msgInfo);
                messageServiceInfo.Initialize();
                messageServiceInfo.ListenerReceived += svc_ListenerReceived;
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("MessageService Initialize Erro!" + ex.Message);
            }
        }
        public void Terminate()
        {
            messageServiceInfo.Terminate();
            LogHelper.MESLog.Debug("MessageService Terminate Complete!");
        }


        void svc_ListenerReceived(object listener, Message requestMessage)
        {
            ThreadDispather(requestMessage);
        }

        private void ThreadDispather(Message message)
        {
            try
            {
                //var eqpID = mesMsg.Header.MACHINENAME;
                var msgStr = message.GetField(MessageServiceInfo.MsgInfo.FieldName).Value.ToString();
                ITibcoDispather dispather = new RVMessageDispatcher(this);
                dispather.Dispath(MessageServiceInfo.MsgInfo.Name, msgStr, message);
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("LC => Received Erro : \r\n" + message.ToString().ToXmlFormat() + ex.Message);
            }
        }
        public bool Send(string messageName, object rvObject, string transactionID)
        {
            string strMessage = rvObject.ToString();
            messageServiceInfo.Send(strMessage, MessageServiceInfo.MsgInfo.TargetSubject);
            LogHelper.MESLog.Info($"[{transactionID}] [Send To {MessageServiceInfo.MsgInfo.Name}] [{messageName}]{System.Environment.NewLine}{strMessage}");
            return true;
        }
        public object SendRequest(string messageName, object rvObject, string transactionID)
        {
            string returnBody = null;
            string strMessage = rvObject.ToString();
            LogHelper.MESLog.Info($"[{transactionID}] [Send Request To {MessageServiceInfo.MsgInfo.Name}] [{messageName}]{System.Environment.NewLine}{strMessage}");
            var replyMessage = messageServiceInfo.SendRequest(strMessage, MessageServiceInfo.MsgInfo.TargetSubject);
            if (replyMessage != null)
            {
                var messageField = replyMessage.GetField(MessageServiceInfo.MsgInfo.FieldName);

                if (messageField != null)
                {
                    var replyXML = messageField.Value.ToString().Trim();
                    returnBody = replyXML;

                    string log;
                    log = XmlSerialization.ToXmlFormat(replyXML);

                    LogHelper.MESLog.Info($"[{transactionID}] [Receive Reply From {MessageServiceInfo.MsgInfo.Name}] [{messageName}]{System.Environment.NewLine}{log}");
                }
            }
            return returnBody;
        }
        public bool SendReply(string messageName, object rvObject, Message reqMessage, string transactionID)
        {
            string strMessage = rvObject.ToString();
            var result = messageServiceInfo.SendReply(strMessage, reqMessage);
            LogHelper.MESLog.Info($"[{transactionID}] [Send Reply To {MessageServiceInfo.MsgInfo.Name}] [{messageName}][{result}]{System.Environment.NewLine}{strMessage}");
            return result;
        }
    }
}
