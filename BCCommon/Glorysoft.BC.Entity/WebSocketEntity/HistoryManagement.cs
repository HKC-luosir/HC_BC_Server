using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class HistoryManagement
    {
        public string tableName { get; set; }
        public string description { get; set; }
        public bool getListByPage { get; set; }
        public Array columnList { get; set; }
    }
}
