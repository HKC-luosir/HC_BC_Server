using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class TableInformation
    {
        public string tableName { get; set; }
        public IList<TableStructure> tableProperties { get; set; }
    }
}
