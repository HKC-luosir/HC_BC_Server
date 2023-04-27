using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_opilog
    {
        public string userid { get; set; }
        public string operating { get; set; }
        public string operationresult { get; set; }
        public string clientip { get; set; }
        public DateTime createtime { get; set; }
        public int id { get; set; }
    }
}
