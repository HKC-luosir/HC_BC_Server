using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class TableStructure
    {
        public string majorKey { get; set; }
        public string propertyName { get; set; }
        public string pType { get; set; }
        public string pLength { get; set; }
        public string pPoint { get; set; }
        public bool isNull { get; set; }
        public string description { get; set; }
    }
}
