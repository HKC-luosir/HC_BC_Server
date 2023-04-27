using Glorysoft.BC.Entity.RVMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV.RVService
{
    public interface ITibcoDispather
    {
        void Dispath(string rvName, object rvMessage, Message requestMessage);
    }
}
