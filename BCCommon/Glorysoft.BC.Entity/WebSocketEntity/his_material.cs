using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_material
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public string materialtype { get; set; }
        public string materialid { get; set; }
        public string materialstate { get; set; }
        public string materialusedcnt { get; set; }
        public string materialposition { get; set; }
        public DateTime createdate { get; set; }
    }
}
