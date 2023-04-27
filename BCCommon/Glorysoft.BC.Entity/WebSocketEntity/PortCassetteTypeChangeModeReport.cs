using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class PortCassetteTypeChangeModeReport : BaseClass
    {
        public PortCassetteTypeChangeModeReport() { }
        public string UnitID { get; set; }
        
        public string PortID { get; set; }
        public string PortCSTType { get; set; }
    }
}
