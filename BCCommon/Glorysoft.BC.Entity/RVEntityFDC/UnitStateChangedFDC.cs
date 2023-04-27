using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class UnitStateChangedFDC
    {
        public UnitStateChangedFDC()
        {
            MACHINENAME = "";
            UNITNAME = "";
            UNITSTATENAME = "";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string UNITSTATENAME { get; set; }
    }
}
