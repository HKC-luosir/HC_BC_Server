using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class PortOperationModeChangeReport : BaseClass
    {
        public PortOperationModeChangeReport() { }
        public string UnitID { get; set; }       
        public string PortID { get; set; }
        public string PortOperationMode { get; set; }
    }
}
