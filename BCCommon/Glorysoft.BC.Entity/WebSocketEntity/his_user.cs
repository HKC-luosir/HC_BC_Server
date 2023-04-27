using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_user
    {
        public string username { get; set; }
        public string userid { get; set; }
        public string password { get; set; }
        public string level { get; set; }
        public string creator { get; set; }
        public DateTime createdate { get; set; }
        public string groupid { get; set; }
    }
}
