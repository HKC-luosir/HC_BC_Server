using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class HostReturnMessage : BaseClass
    {
        public HostReturnMessage() { }

        public string returnLevel { get; set; }
        public string message { get; set; }
    }
}
