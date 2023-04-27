
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class PortInfoRequest : BaseClass
    {
        public PortInfoRequest() { }
        public string PortID { get; set; }
        public string PortNo { get; set; }

    }
}
