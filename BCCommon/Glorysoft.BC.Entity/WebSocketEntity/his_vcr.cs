using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_vcr
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public string unitname { get; set; }
        public int vcrenablemode { get; set; }
        public int vcrno { get; set; }
        public int vcrreadfailoperationmode { get; set; }
        public DateTime createdate { get; set; }
        public DateTime startcreatedate { get; set; }
        public DateTime endcreatedate { get; set; }
    }
}
