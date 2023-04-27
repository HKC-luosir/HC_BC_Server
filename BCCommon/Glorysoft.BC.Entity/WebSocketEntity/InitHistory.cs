using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class InitHistory
    {
        public int total { get; set; }
        public IList<object> rows { get; set; }
        public int from { get; set; }
        public int size { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public object condition { get; set; }
        public string sort { get; set; }
        public string order { get; set; }
        public string sortCondition { get; set; }
        public string tableName { get; set; }


    }
}
