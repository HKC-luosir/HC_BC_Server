using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.RV.Entity;
using Glorysoft.BC.RV.RVService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.Common
{
    public class MESClient
    {
        public static RVHeader SendAsync(TibcoContext context, string messageName, object mesEntity, string transactionID)
        {
            try
            {
                RVHeader replyHeader = new RVHeader();
                var requestMessage = new MESData();
                var header = new RVHeader()
                {
                    MESSAGENAME = messageName,
                    TRANSACTIONID = transactionID,
                };
                var xmlBody = XmlSerialization.SerializeOnlyBody(mesEntity);

                requestMessage.RVHeader = header;
                var xmlMessage = XmlSerialization.XmlSerializeMessage<MESData>(requestMessage, xmlBody);

                var replyData = context.SendRequest(messageName, xmlMessage, transactionID);
                if (replyData != null)
                {
                    var replyXML = replyData.ToString();
                    replyHeader = XmlSerialization.DeserializeNodeObj<RVHeader>(replyXML, "Response", "Header");
                }
                else
                {
                    replyHeader.RESULT = MESResult.FAIL.ToString();
                    replyHeader.RESULTMESSAGE = $"MES:{messageName} Reply Timeout.";
                    LogHelper.MESLog.Info(replyHeader.RESULTMESSAGE);
                }
                return replyHeader;
            }
            catch (Exception ex)
            {
                RVHeader replyHeader = new RVHeader();
                replyHeader.RESULT = MESResult.FAIL.ToString();
                replyHeader.RESULTMESSAGE = ex.Message;
                LogHelper.MESLog.Error(ex.Message);
                return replyHeader;
            }
        }
       //复写SendASYnc方法 luoxianjing 
        public static NewRVHeader NewSendAsync(TibcoContext context, string messageName, object mesEntity, string transactionID)
        {
            try
            {
                NewRVHeader replyHeader = new NewRVHeader();
                var requestMessage = new NewMESData();
                var header = new NewRVHeader()
                {
                    MESSAGENAME = messageName,
                    TRANSACTIONID = transactionID,
                };
                var xmlBody = XmlSerialization.SerializeOnlyBody(mesEntity);

                requestMessage.NewRVHeader = header;
                var xmlMessage = XmlSerialization.XmlSerializeMessage<NewMESData>(requestMessage, xmlBody);

                var replyData = context.SendRequest(messageName, xmlMessage, transactionID);
                if (replyData != null)
                {
                    var replyXML = replyData.ToString();
                    replyHeader = XmlSerialization.DeserializeNodeObj<NewRVHeader>(replyXML, "Response", "Header");
                }
                else
                {
                    replyHeader.RESULT = MESResult.FAIL.ToString();
                    replyHeader.RESULTMESSAGE = $"MES:{messageName} Reply Timeout.";
                    LogHelper.MESLog.Info(replyHeader.RESULTMESSAGE);
                }
                return replyHeader;
            }
            catch (Exception ex)
            {
                NewRVHeader replyHeader = new NewRVHeader();
                replyHeader.RESULT = MESResult.FAIL.ToString();
                replyHeader.RESULTMESSAGE = ex.Message;
                LogHelper.MESLog.Error(ex.Message);
                return replyHeader;
            }
        }
        public static T SendAsync<T>(TibcoContext context, string messageName, object mesEntity, string transactionID, out RVHeader replyHeader) where T : class
        {
            try
            {
                replyHeader = new RVHeader();
                T t = null;
                var requestMessage = new MESData();
                var header = new RVHeader()
                {
                    MESSAGENAME = messageName,
                    TRANSACTIONID = transactionID,
                };
                var xmlBody = XmlSerialization.SerializeOnlyBody(mesEntity);

                requestMessage.RVHeader = header;
                var xmlMessage = XmlSerialization.XmlSerializeMessage<MESData>(requestMessage, xmlBody);

                var replyData = context.SendRequest(messageName, xmlMessage, transactionID);
                if (replyData != null)
                {
                    var replyXML = replyData.ToString();
                    replyHeader = XmlSerialization.DeserializeNodeObj<RVHeader>(replyXML, "Response", "Header");

                    t = XmlSerialization.DeserializeNodeObj<T>(replyXML, "Response", "Body");
                }
                else
                {
                    replyHeader.RESULT = MESResult.FAIL.ToString();
                    replyHeader.RESULTMESSAGE = $"MES:{messageName} Reply Timeout.";
                    LogHelper.MESLog.Info(replyHeader.RESULTMESSAGE);
                }
                return t;
            }
            catch (Exception ex)
            {
                replyHeader = new RVHeader();
                replyHeader.RESULT = MESResult.FAIL.ToString();
                replyHeader.RESULTMESSAGE = ex.Message;
                LogHelper.MESLog.Error(ex.Message);
                return null;
            }
        }
        public static void Send(TibcoContext context, string messageName, object mesEntity, string transactionID, string MachineName = "")
        {
            try
            {
                string xmlMessage = "";
                switch (context.Name)
                {
                    case "SPC":
                        {
                            var requestMessage = new MESDataSPC();
                            var header = new RVHeaderSPC()
                            {
                                MESSAGENAME = messageName,
                                SHOPNAME = "MDL",
                                MACHINENAME = MachineName,
                                TRANSACTIONID = transactionID,
                                SOURCESUBJECTNAME = context.MessageServiceInfo.MsgInfo.OwnSubject,
                                TARGETSUBJECTNAME = context.MessageServiceInfo.MsgInfo.TargetSubject
                            };
                            var xmlBody = XmlSerialization.SerializeOnlyBody(mesEntity);

                            requestMessage.RVHeader = header;
                            xmlMessage = XmlSerialization.XmlSerializeMessageSPC<MESDataSPC>(requestMessage, xmlBody);
                        }
                        break;
                    default:
                        {
                            //var replyHeader = new RVHeader();
                            var requestMessage = new MESData();
                            var header = new RVHeader()
                            {
                                MESSAGENAME = messageName,
                                TRANSACTIONID = transactionID,
                            };
                            var xmlBody = XmlSerialization.SerializeOnlyBody(mesEntity);

                            requestMessage.RVHeader = header;
                            xmlMessage = XmlSerialization.XmlSerializeMessage<MESData>(requestMessage, xmlBody);
                        }
                        break;
                }

                context.Send(messageName, xmlMessage, transactionID);
            }
            catch (Exception ex)
            {
                var replyHeader = new RVHeader();
                replyHeader.RESULT = MESResult.FAIL.ToString();
                replyHeader.RESULTMESSAGE = ex.Message;
                LogHelper.MESLog.Error(ex.Message);
            }
        }
        public static bool SendReply(TibcoContext context, string messageName, object mesEntity, RVHeader Header, Message message)
        {
            try
            {
                var replyMessage = new MESReplyData();
                //var header = new RVHeader()
                //{
                //    MESSAGENAME = messageName,
                //    TRANSACTIONID = transactionID,
                //};
                var xmlBody = XmlSerialization.SerializeOnlyBody(mesEntity);

                replyMessage.RVHeader = Header;
                var xmlMessage = XmlSerialization.XmlSerializeMessage<MESReplyData>(replyMessage, xmlBody);

                return context.SendReply(messageName, xmlMessage, message, Header.TRANSACTIONID);
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error(ex.Message);
                return false;
            }
        }
        public static T GetRequestMessage<T>(string mesMessage, out RVHeader replyHeader) where T : class
        {
            try
            {
                replyHeader = new RVHeader();
                T t = null;
                replyHeader = XmlSerialization.DeserializeNodeObj<RVHeader>(mesMessage, "Request", "Header");
                t = XmlSerialization.DeserializeNodeObj<T>(mesMessage, "Request", "Body");
                return t;
            }
            catch (Exception ex)
            {
                replyHeader = new RVHeader();
                replyHeader.RESULT = MESResult.FAIL.ToString();
                replyHeader.RESULTMESSAGE = ex.Message;
                LogHelper.MESLog.Error(ex.Message);
                return null;
            }
        }
    }
}
