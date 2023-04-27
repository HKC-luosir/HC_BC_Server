
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class bc_robot_linksignal_configure
    {
        public string line_name { get; set; }
        public string eqp_name { get; set; }
        public string unit_name { get; set; }
        public int inout_type { get; set; }
        public bool is_put_first { get; set; }
        public bool is_get_delay { get; set; }
        public int get_delay_time { get; set; }
        

    }

}
