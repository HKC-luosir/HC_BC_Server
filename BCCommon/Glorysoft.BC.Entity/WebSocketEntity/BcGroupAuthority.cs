using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class BcGroupAuthority
    {
        public string group_id { get; set; }
        public string menu_name { get; set; }
        public string page_name { get; set; }
        public string action { get; set; }
    }
}
