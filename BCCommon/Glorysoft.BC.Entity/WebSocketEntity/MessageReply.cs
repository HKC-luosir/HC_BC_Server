using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class MessageReply : BaseClass
    {
        public MessageReply() { }

        public string ReturnCode { get; set; }
        public string UnitID { get; set; }
        public string Message { get; set; }

        public List<Parameter> ParameterList { get; set; }

    }
}
