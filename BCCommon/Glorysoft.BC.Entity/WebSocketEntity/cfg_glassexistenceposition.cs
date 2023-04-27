using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_glassexistenceposition
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public int position { get; set; }
        public string positionname { get; set; }
        public int cassettesequenceno { get; set; }
        public int slotsequenceno { get; set; }
        public bool exist { get; set; }
    }
}
