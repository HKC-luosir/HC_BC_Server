
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class MachineStateChangedFDC
    {
        public MachineStateChangedFDC()
        {
            MACHINENAME = "";
            MACHINESTATENAME = "";
        }
        public string MACHINENAME { get; set; }
        public string MACHINESTATENAME { get; set; }
      
    }
}
