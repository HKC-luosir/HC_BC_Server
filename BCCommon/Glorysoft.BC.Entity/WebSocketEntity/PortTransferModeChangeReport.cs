using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class PortTransferModeChangeReport : BaseClass
    {
        public PortTransferModeChangeReport() { }
        public string UnitID { get; set; }       
        public string PortID { get; set; }
        public string TransferMode { get; set; }
    }
}
