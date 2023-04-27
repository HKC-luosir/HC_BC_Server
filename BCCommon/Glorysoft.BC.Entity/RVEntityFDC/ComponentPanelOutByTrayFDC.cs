
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ComponentPanelOutByTrayFDC
    {
        public ComponentPanelOutByTrayFDC()
        {
              MACHINENAME ="";
             PORTNAME ="";
             FROMTRAYNAME ="";
             PANELNAME ="";
             MACHINERECIPENAME ="";
             FROMPOSITION ="";
        }
        public string MACHINENAME { get; set; }
        public string PORTNAME { get; set; }
        public string FROMTRAYNAME { get; set; }
        public string PANELNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string FROMPOSITION { get; set; }
    }

}
