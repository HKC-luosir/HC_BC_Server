using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class SubUnitStateChangedFDC
    {
        public SubUnitStateChangedFDC()
        {
              MACHINENAME="";
              UNITNAME="";
              UNITSTATENAME="";
              SUBUNITNAME="";
              SUBUNITSTATENAME="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string UNITSTATENAME { get; set; }
        public string SUBUNITNAME { get; set; }
        public string SUBUNITSTATENAME { get; set; }
    }
}
