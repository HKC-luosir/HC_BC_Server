using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ComponentPanelInSubUnitFDC
    {
        public ComponentPanelInSubUnitFDC()
        {
              MACHINENAME="";
             UNITNAME="";
             SUBUNITNAME="";
             FROMTRAYNAME="";
             PANELNAME="";
             MACHINERECIPENAME="";
             FROMPOSITION="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string SUBUNITNAME { get; set; }
        public string FROMTRAYNAME { get; set; }
        public string PANELNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string FROMPOSITION { get; set; }
    }

}
