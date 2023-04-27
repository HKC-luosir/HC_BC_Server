using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.RVService
{
    public interface IMsgServer
    {
        bool Initialize();
        void Terminate();
        void Send(string sendMessage, string targetSubject);
        Message SendRequest(string sendMessage, string targetSubject);
        bool SendReply(string sendMessage, Message reqMessage);

        event MsgEvent.EventMessageReceived OwnListenerReceived;
        event MsgEvent.EventMessageReceived ListenerReceived;
    }
}
