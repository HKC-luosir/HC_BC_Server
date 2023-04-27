using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class bc_eqp_command_para
    {
        public int object_id { get; set; }
        public string command_type { get; set; }
        public string parameter_id { get; set; }
        public string parameter_type { get; set; }
        public bool required { get; set; }
        public string reference_value { get; set; }
        public double max_value { get; set; }
        public double min_value { get; set; }
        public string line_id { get; set; }
        public string parameter_name { get; set; }
        public string item_number { get; set; }
        public string equipment_no { get; set; }
    }
}
