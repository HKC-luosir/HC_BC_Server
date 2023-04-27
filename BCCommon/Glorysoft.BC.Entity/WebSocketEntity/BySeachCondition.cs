using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class BySeachCondition
    {
        public string lineId { get; set; }
        public string tableName { get; set; }
        public Array fields { get; set; }
        //public IList<object> fieldList { get; set; }
    }
}
