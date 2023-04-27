using System;
using System.Collections.Generic;
using System.Linq;
using TIBCO.Rendezvous;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity.RVEntity;
using System.Threading;
using System.Text;

namespace Glorysoft.BC.RV.RVService
{
    public class MessageService : IMsgServer
    {
        private Dispatcher dispatcher;
        private Queue queue = null;
        public Transport Transport;
        public bool IsConnect = false;
        private Dictionary<string, Listener> listenerList;
        private Dictionary<string, Transport> transportList = new Dictionary<string, Transport>();
        private object syncObj = new object();
        private Listener ownListener;
        private bool isRun;
        public event MsgEvent.EventMessageReceived OwnListenerReceived;
        public event MsgEvent.EventMessageReceived ListenerReceived;
        string ownSubjectName = "";
        private Message recvMessage = null;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        private MessageInfo msgInfo;
        public MessageInfo MsgInfo
        {
            get { return msgInfo; }
            private set { msgInfo = value; }
        }

        public MessageService(MessageInfo messageInfo)
        {
            msgInfo = messageInfo;
        }
        public bool Initialize()
        {
            transportList = new Dictionary<string, Transport>();
            try
            {
                if (isRun)
                {
                    Terminate();
                }
                OpenRVEnvironment();
                var ret1 = CreateTransport();

                CreateQueue();
                CreateDispatcher();

                CreateOwnListener();
                var ret2 = CreateListener();

                isRun = true;
                if (ret1 & ret2)
                {
                    IsConnect = true;
                    return true;
                }
                else
                {
                    IsConnect = false;
                    return false;
                }
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Initialize Erro," + ex.Message);
                return false;
            }
        }
        public void Terminate()
        {
            if (!isRun) return;

            try
            {
                DestroyQueue();
                DestroyListener();
                DestroyTransfort();
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("Terminate Erro," + ex.Message);
            }
            finally
            {
                CloseTibEnvironment();
                isRun = false;
            }
        }
        private void TerminateQueueAndTransport()
        {
            if (!isRun) return;

            try
            {
                DestroyQueue();
                DestroyTransfort();
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("TerminateQueueAndTransport Erro," + ex.Message);
            }
            finally
            {
                CloseTibEnvironment();
                isRun = false;
            }
        }

        public void Send(string sendMessage, string targetSubject)
        {
            var message = new Message { SendSubject = targetSubject };
            try
            {
                message.AddField(msgInfo.FieldName, sendMessage, 0);
                Transport.Send(message);
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Send Message Erro," + ex.Message);
            }
        }
        public Message SendRequest(string sendMessage, string targetSubject)
        {
            var message = new Message { SendSubject = targetSubject };
            try
            {
                message.AddField(msgInfo.FieldName, sendMessage, 0);
                var replyMessage = Transport.SendRequest(message, msgInfo.TimeOut / 1000);
                return replyMessage;
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Send Message Erro," + ex.Message);
                return null;
            }
        }
        public bool SendReply(string sendMessage, Message reqMessage)
        {
            var message = new Message { SendSubject = reqMessage.ReplySubject };
            try
            {
                message.AddField(msgInfo.FieldName, sendMessage, 0);
                Transport.SendReply(message, reqMessage);
                return true;
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error(ex.ToString());
                return false;
            }
        }
        private void OpenRVEnvironment()
        {
            try
            {
                TIBCO.Rendezvous.Environment.StringEncoding = Encoding.UTF8;
                TIBCO.Rendezvous.Environment.Open();
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Open RV Exvirment Error:" + ex.Message);
            }
        }
        private void ReTryCreateTransPort()
        {
            IsConnect = false;
            while (true)
            {
                foreach (string t in msgInfo.DaemonList)
                {
                    try
                    {
                        LogHelper.MESLog.InfoFormat("Retry Connect Daemon:{0}", t);
                        //logicService.ReceiceHostMsg(string.Format("Retry Connect Daemon:{0}", t));
                        //if ((Transport as NetTransport).Daemon != t)
                        //    continue;
                        Transport = new NetTransport(msgInfo.Service, msgInfo.Network, t);
                        IsConnect = CreateListener();
                        if (IsConnect)
                        {
                            LogHelper.MESLog.InfoFormat("MES Connect Successful, Daemon:{0}", t);
                            //logicService.ReceiceHostMsg(string.Format("MES Connect Successful, Daemon:{0}", t));
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.MESLog.ErrorFormat("Retry Create Transport Error {0}:{1}", t, ex.Message);
                        //logicService.ReceiceHostMsg(string.Format("Retry Connect Failed, Daemon:{0}", t));
                        Thread.Sleep(1000);
                    }
                }
                if (IsConnect)
                    break;
                Thread.Sleep(5000);
            }
        }
        private bool CreateTransport()
        {
            try
            {
                int tryCount = 0;
                foreach (string t in msgInfo.DaemonList)
                {
                    tryCount++;
                    try
                    {
                        Transport = new NetTransport(msgInfo.Service, msgInfo.Network, t);
                        break;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.MESLog.ErrorFormat("Create Transport Error {0}:{1}", t, ex.Message);
                        if (tryCount == msgInfo.DaemonList.Count())
                            return false;
                    }
                }
                return true;
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Create Transport Error:" + ex.Message);
                return false;
            }
        }
        private void CreateQueue()
        {
            try
            {
                queue = new Queue();

            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Create Queue Error:" + ex.Message);
            }
        }
        private void CreateDispatcher()
        {
            try
            {
                dispatcher = new Dispatcher(queue);
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Create Dispatcher Error:" + ex.Message);
            }
        }
        private bool CreateListener()
        {
            try
            {
                listenerList = new Dictionary<string, Listener>();
                foreach (var listenSubjectName in msgInfo.ListenSubjectList)
                {
                    var listener = new Listener(queue, Transport, listenSubjectName, null);
                    listener.MessageReceived += OnMessageListenReceived;
                    listenerList.Add(listenSubjectName, listener);
                    LogHelper.MESLog.InfoFormat("listenSubjectName:", listenSubjectName);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("Create Listener Error:" + ex.Message);
                return false;
            }

        }
        private void CreateOwnListener()
        {
            try
            {
                if (!string.IsNullOrEmpty(msgInfo.OwnSubject))
                {
                    ownListener = new Listener(queue, Transport, msgInfo.OwnSubject, null);
                    //ownListener.MessageReceived += OnMessageReceived;
                }
            }
            catch (RendezvousException ex)
            {
                LogHelper.MESLog.Error("Create OwnListener Failed:" + ex.Message);
            }
        }

        private void DestroyQueue()
        {
            queue.Destroy();
        }

        private void DestroyListener()
        {
            ownListener.Destroy();
            if (listenerList == null) return;
            foreach (var listener in listenerList.Values)
                listener.Destroy();
        }

        private void DestroyTransfort()
        {
            foreach (var transport in transportList.Values)
                transport.Destroy();
            if (Transport != null)
                Transport.Destroy();
        }

        private void CloseTibEnvironment()
        {
            try
            {
                GC.KeepAlive(listenerList);
                TIBCO.Rendezvous.Environment.Close();
            }

            catch (Exception ex)
            {
            }
        }
        //Subject Listen
        private void OnMessageListenReceived(object listener, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            try
            {
                var message = messageReceivedEventArgs.Message;
                if (messageReceivedEventArgs.Message.SendSubject.Contains("DISCONNECTED"))
                {
                    //HostInfo.Current.IsHostConnect = false;
                    //logicService.UpdateHostConnectState(false);
                    //logicService.ReceiceHostMsg("Tibco DISCONNECTED");
                    LogHelper.MESLog.Info("Tibco DISCONNECTED");
                    ReTryCreateTransPort();
                }
                else if (messageReceivedEventArgs.Message.SendSubject.Contains("CONNECTED"))
                {
                  
                        //MachineName name = new MachineName();
                        //name.MACHINENAME = HostInfo.Current.LineID;
                        //mesService.SendToMESAreYouThere(name, "AreYouThere");
                        //logicService.ReceiceHostMsg("Tibco CONNECTED");
                        LogHelper.MESLog.Info("Tibco CONNECTED");
                }
                //System.Diagnostics.Debug.WriteLine(message.ToString());
                //System.Diagnostics.Debug.WriteLine(message.GetField(msgInfo.FieldName).Value.ToString());
                if (message.GetField(msgInfo.FieldName) != null)
                {
                    recvMessage = message;
                    var trxid = System.Guid.NewGuid().ToString();
                    var msgStr = message.GetField(msgInfo.FieldName).Value.ToString();
                    ListenerReceived(listener, message);
                }
            }
            catch (Exception ex)
            {
                LogHelper.MESLog.Error("OnMessageListenReceived Error:" + ex.Message);
            }
        }
        //OwnSubjext Listen
        
    }
}