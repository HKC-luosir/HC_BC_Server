using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_eqpstatusrule
    {
        public string eqpid { get; set; }
        public string unitidlist { get; set; }
        public int checkenable { get; set; }
        public string eqpstatus { get; set; }

        public string eqpstatustext { get; set; }
        public string lineId { get; set; }
    }
}
