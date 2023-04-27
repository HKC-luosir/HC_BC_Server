
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class CSTOperationModeReport : BaseClass
    {
        public CSTOperationModeReport() { }
        public string UnitID { get; set; }
        public string CassetteOperationMode { get; set; }
    }
}
