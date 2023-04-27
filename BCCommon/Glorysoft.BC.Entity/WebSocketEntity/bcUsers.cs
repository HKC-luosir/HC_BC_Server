using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class bcUsers
    {
        public bcUser bcUser { get; set; }

        public IList<Group_Authority> authorities { get; set; }
    }
}
