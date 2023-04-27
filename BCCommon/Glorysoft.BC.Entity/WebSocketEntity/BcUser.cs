
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class bcUser
    {
        //UserName
        //Password
        public string userId { get; set; }
        public string lineId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string roleId { get; set; }
        public string mail { get; set; }
        public string active { get; set; }
        public string createtime { get; set; }
        public string createUserid { get; set; }
        public string department { get; set; }
        public string phone { get; set; }
        public string groupId { get; set; }
        public IList<User> users { get; set; }
        
        public IList<Group_Authority> authorities { get; set; }
    }
}
