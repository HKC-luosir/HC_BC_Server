
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class TrayProcessStartedFDC
    {
        public TrayProcessStartedFDC()
        {
             MACHINENAME ="";
             PORTNAME ="";
             TRAYNAME ="";
             MACHINERECIPENAME ="";
             POSITION ="";
             PRODUCTSPECNAME ="";
        }
        public string MACHINENAME { get; set; }
        public string PORTNAME { get; set; }
        public string TRAYNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string POSITION { get; set; }
        public string PRODUCTSPECNAME { get; set; }
    }
}
