using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_eqp
    {
        public string eqpid { get; set; }
        public DateTime updatedate { get; set; }
        public DateTime startcreatedate { get; set; }
        public DateTime endcreatedate { get; set; }
        public string reasoncode { get; set; }
        public string indexeroperationmode { get; set; }
        public int linetype { get; set; }
        public int linemode { get; set; }
        public int robotdispatchmode { get; set; }
        public int coldruntotalquantity { get; set; }
        public int coldruncurrentquantity { get; set; }
        public string eqpstatus { get; set; }
        public string functionname { get; set; }
    }
}
