using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.BC.RV.Entity;
using Glorysoft.BC.RV.RVService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.Common
{
    public class TibcoManager
    {
        private static readonly object syncRoot = new object();

        private static readonly Lazy<TibcoManager> Lazy = new Lazy<TibcoManager>(() => new TibcoManager());
        public static TibcoManager Current
        {
            get
            {
                return Lazy.Value;
            }
        }

        private Dictionary<string, TibcoContext> tibcoList;
        public Dictionary<string, TibcoContext> TibcoList
        {
            get { return tibcoList; }
            set { tibcoList = value; }
        }

        public void InitialTibco(string strFilePath)
        {
            tibcoList = new Dictionary<string, TibcoContext>();
            var messageInfoList = InitialzeConfigure(strFilePath);
            foreach (var messageInfo in messageInfoList)
            {
                var tibcoInfo = new TibcoContext(messageInfo);
                if (tibcoInfo.IsConnect)
                {
                    tibcoList.Add(messageInfo.Name, tibcoInfo);
                    LogHelper.MESLog.Info("Successful Initialized:" + messageInfo.Name);
                }
                else
                {
                    LogHelper.MESLog.Info("Failed Initialized:" + messageInfo.Name);
                }
            }
        }
        //delegate void DelSendOld(string messageName, string requestBody, double requestTimeout, string eqpID, string tranID);
        //public void SendAsync(string messageName, string requestBody, string eqpID, string tranID)
        //{
        //    try
        //    {
        //        var lineInfo = HostInfo.Current.EQPInfo;
        //        if (lineInfo != null)
        //        {
        //            if (lineInfo.ControlState != EnumControlState.OffLine
        //                || messageName == "MESALIVECHECK"
        //                || messageName == "COMM_STATE_CHANGE"
        //                || messageName == "TIMEREQUEST")
        //            {
        //                DelSendOld sendReq = new DelSendOld(SendOld);
        //                //sendReq.BeginInvoke(messageName, requestBody, messageService.MsgInfo.TimeOutInMsec, eqpID, tranID, new AsyncCallback(SendRequestResult), null);
        //            }
        //            //DelSend sendReq = new DelSend(Send);
        //            //sendReq.BeginInvoke(messageName, requestBody, messageService.MsgInfo.TimeOutInMsec, eqpID, tranID, new AsyncCallback(SendRequestResult), null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.MESLog.Error(ex.Message);
        //    }
        //}
        //private void SendOld(string messageName, string requestBody, double requestTimeout, string eqpID, string tranId)
        //{

        //}
        //delegate void DelSend(string rvName, string messageName, string requestBody, string eqpId, string tranID);

        //public void SendAsync(string rvName, string messageName, string requestBody, string eqpId, string tranID)
        //{
        //    try
        //    {
        //        DelSend sendReq = new DelSend(Send);
        //        sendReq.BeginInvoke(rvName, messageName, requestBody, eqpId, tranID, null, null);
        //        LogHelper.EQPLogger.DebugFormat("+++ [EAS=>MES]-[{0}]EQPID:{1},TranID:{2},ControlMode:{3}+++", messageName, eqpId, tranID, HostInfo.Current.ControlState);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.MESLog.Error(ex.Message);
        //    }
        //}
        public RVHeader SendAsync(string messageName, object requestBody, string tranID)
        {
            RVHeader replyHeader = new RVHeader();
            try
            {
                string rvName = HostInfo.Current.RVMessageList.FirstOrDefault(o => o.Key == messageName).Value;
                if (tibcoList != null && tibcoList.ContainsKey(rvName))
                {
                    TibcoContext tibcoContext = tibcoList[rvName];
                    replyHeader = MESClient.SendAsync(tibcoContext, messageName, requestBody, tranID);
                }
                return replyHeader;
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error(ex.ToString());
                return replyHeader;
            }
        }

        public NewRVHeader NewSendAsync(string messageName, object requestBody, string tranID)
        {
            NewRVHeader replyHeader = new NewRVHeader();
            try
            {
                string rvName = HostInfo.Current.RVMessageList.FirstOrDefault(o => o.Key == messageName).Value;
                if (tibcoList != null && tibcoList.ContainsKey(rvName))
                {
                    TibcoContext tibcoContext = tibcoList[rvName];
                    replyHeader = MESClient.NewSendAsync(tibcoContext, messageName, requestBody, tranID);
                }
                return replyHeader;
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error(ex.ToString());
                return replyHeader;
            }
        }
        public T SendAsync<T>(string messageName, object requestBody, out RVHeader replyHeader, string tranID) where T : class
        {
            T t = null;
            replyHeader = new RVHeader();
            try
            {
                string rvName = HostInfo.Current.RVMessageList.FirstOrDefault(o => o.Key == messageName).Value;
                if (tibcoList != null && tibcoList.ContainsKey(rvName))
                {
                    TibcoContext tibcoContext = tibcoList[rvName];
                    t = MESClient.SendAsync<T>(tibcoContext, messageName, requestBody, tranID, out replyHeader);
                }
                return t;
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error(ex.ToString());
                return null;
            }
        }
        public void Send(string messageName, object requestBody, string tranID, string MachineName = "")
        {
            try
            {
                string rvName = HostInfo.Current.RVMessageList.FirstOrDefault(o => o.Key == messageName).Value;
                if (tibcoList != null && tibcoList.ContainsKey(rvName))
                {
                    TibcoContext tibcoContext = tibcoList[rvName];
                    MESClient.Send(tibcoContext, messageName, requestBody, tranID, MachineName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error(ex.ToString());
            }
        }
        //SendReply(TibcoContext context, string messageName, object mesEntity, RVHeader Header, Message message)
        public void SendReply(string messageName, object requestBody, RVHeader replyHeader, Message requestMessage)
        {
            try
            {
                string rvName = HostInfo.Current.RVMessageList.FirstOrDefault(o => o.Key == messageName).Value;
                if (tibcoList != null && tibcoList.ContainsKey(rvName))
                {
                    TibcoContext tibcoContext = tibcoList[rvName];
                    MESClient.SendReply(tibcoContext, messageName, requestBody, replyHeader, requestMessage);
                }
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error(ex.ToString());
            }
        }
        private List<MessageInfo> InitialzeConfigure(string strFilePath)
        {
            try
            {
                var xDoc = new XmlDocument();
                var file = AppDomain.CurrentDomain.BaseDirectory + strFilePath;
                xDoc.Load(file);
                var messageInfoList = new List<MessageInfo>();
                XmlNode rvList = xDoc.SelectSingleNode("RVConnections");
                foreach (XmlNode rv in rvList)
                {
                    var messageInfo = new MessageInfo();
                    messageInfo.Name = rv.SelectSingleNode("Name").InnerText.Trim();
                    var xmlNodeList = rv.SelectNodes("Service");
                    if (xmlNodeList != null && xmlNodeList.Count > 0)
                        messageInfo.Service = xmlNodeList[0].InnerText.Trim();
                    var selectNodes = rv.SelectNodes("Network");
                    if (selectNodes != null && selectNodes.Count > 0)
                        messageInfo.Network = selectNodes[0].InnerText.Trim();
                    messageInfo.DaemonList.Clear();
                    var daemonNodeList = rv.SelectSingleNode("DaemonList");
                    if (daemonNodeList != null)
                    {
                        foreach (XmlNode daemonNode in daemonNodeList.ChildNodes)
                        {
                            messageInfo.DaemonList.Insert(messageInfo.DaemonList.Count, daemonNode.InnerText);
                        }
                    }
                    else
                    {
                        throw new Exception("No daemon-list in config file.");
                    }
                    var address = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                    if (address == null || address.Length <= 0)
                    {
                        messageInfo.Daemon = messageInfo.DaemonList[0];
                    }
                    else
                    {
                        messageInfo.Daemon = messageInfo.DaemonList[0];
                    }
                    var nodeList = rv.SelectNodes("TargetSubject");
                    if (nodeList != null && nodeList.Count > 0)
                        messageInfo.TargetSubject = nodeList[0].InnerText;
                    nodeList = rv.SelectNodes("FieldName");
                    if (nodeList != null && nodeList.Count > 0)
                        messageInfo.FieldName = nodeList[0].InnerText;
                    nodeList = rv.SelectNodes("MessageRequestRoot");
                    if (nodeList != null && nodeList.Count > 0)
                        messageInfo.MessageRequestRoot = nodeList[0].InnerText;
                    nodeList = rv.SelectNodes("MessageReplyRoot");
                    if (nodeList != null && nodeList.Count > 0)
                        messageInfo.MessageReplyRoot = nodeList[0].InnerText;
                    nodeList = rv.SelectNodes("TimeOut");
                    if (nodeList != null && nodeList.Count > 0)
                    {
                        int timeOut = 5000;
                        int.TryParse(nodeList[0].InnerText, out timeOut);
                        messageInfo.TimeOut = timeOut;
                    }
                    nodeList = rv.SelectNodes("EnvName");
                    if (nodeList != null && nodeList.Count > 0)
                        messageInfo.EnvName = nodeList[0].InnerText;
                    messageInfo.ListenSubjectList.Clear();

                    var listenSubjectNodeList = rv.SelectSingleNode("ListenSubjectList");
                    if (listenSubjectNodeList != null)
                    {
                        foreach (XmlNode subjectNode in listenSubjectNodeList.ChildNodes)
                        {
                            messageInfo.ListenSubjectList.Insert(messageInfo.ListenSubjectList.Count, subjectNode.InnerText);
                        }
                    }
                    var selectNodes1 = rv.SelectNodes("OwnSubject");
                    if (selectNodes1 != null && selectNodes1.Count > 0)
                        messageInfo.OwnSubject = selectNodes1[0].InnerText;
                    messageInfoList.Add(messageInfo);
                }
                return messageInfoList;
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("Initialize Config Error:" + ex.ToString());
                return new List<MessageInfo>();
            }
        }
    }
}
