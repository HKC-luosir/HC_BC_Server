using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class UserInfoRequest
    {
        public string lineId { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string groupId { get; set; }
        public int pageNum { get; set; }
        public int pageSize { get; set; }

    }
}
