using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.RVService
{
    public class MsgEvent
    {
        public delegate void EventMessageReceived(object listener, Message requestMessage);
    }
}
