using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class bc_eqp_command_conf
    {
        public int object_id { get; set; }
        public string line_id { get; set; }
        public string equipment_no { get; set; }
        public string equipment_id { get; set; }
        public string subequipment_no { get; set; }
        public string machine { get; set; }
        public string command_type { get; set; }
        public string protocol { get; set; }
        public string command_secsname { get; set; }
        public string command_to_map { get; set; }
    }
}
