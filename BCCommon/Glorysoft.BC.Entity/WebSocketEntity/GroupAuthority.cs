using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class GroupAuthority
    {
        public string objectid { get; set; }
        public string groupId { get; set; }
        public string description { get; set; }
        //public Array users { get; set; }
        public List<Authoritie> authorities { get; set; }
    }
}
