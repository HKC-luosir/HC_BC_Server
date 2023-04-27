
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class PortInfoSetting : BaseClass
    {
        public PortInfoSetting() { }
        //        UnitID
        //PortID
        //PortCSTType
        //PortMode
        //PortOperationMode
        public string UnitID { get; set; }
        public string PortID { get; set; }
        public string PortCSTType { get; set; }
        public string PortMode { get; set; }
        public string PortOperationMode { get; set; }
        //TransferMode
        //PortTypeAutoChangeMode
        //PortType
        //PortUseType
        //PortOperationMode
        public string TransferMode { get; set; }
        public int PortPauseMode { get; set; }
        public string PortTypeAutoChangeMode { get; set; }
        public string PortType { get; set; }
        public string PortUseType { get; set; }
        

    }
}
