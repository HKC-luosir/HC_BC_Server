
namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class bc_robot_path_configure
    {
        public string idx_name { get; set; }
        public string path_name { get; set; } = "";
        public string source_path_name { get; set; }
        public string target_path_name { get; set; }
        public bool robot_fixed { get; set; } = false;
        public int robot_arm { get; set; } = 0;
        public string rule_id { get; set; }
        public int out_source_priority { get; set; } = 0;
        public int in_target_priority { get; set; } = 0;
        public string line_name { get; set; }
        public bool port_get_check_receive { get; set; }
        public string modepath { get; set; }
    }
}
