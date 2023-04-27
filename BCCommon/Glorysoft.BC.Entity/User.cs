using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class User
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string Creator { get; set; }
        public DateTime CreateDate { get; set; }
        public string GroupId { get; set; }
    }
}
