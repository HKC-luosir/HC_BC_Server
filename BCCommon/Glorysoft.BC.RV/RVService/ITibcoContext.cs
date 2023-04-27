using Glorysoft.BC.Entity.RVMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.RVService
{
    public interface ITibcoContext
    {
        string Name { get; }
        bool IsConnect { get; }
        void Terminate();

        bool Send(string messageName, object Object, string transactionID);
        object SendRequest(string messageName, object Object, string transactionID);
        bool SendReply(string messageName, object rvObject, Message reqMessage, string transactionID);
    }
}
