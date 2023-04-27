
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class TrayProcessEndFDC
    {
        public TrayProcessEndFDC()
        {
            PANELLIST = new TrayProcessEndFDCPANELLIST();
              MACHINENAME ="";
              PORTNAME ="";
              TRAYNAME ="";
              MACHINERECIPENAME ="";
              PRODUCTSPECNAME ="";
              PRODUCTIONTYPE ="";
              PRODUCTQUANTITY ="";
        }
        public string MACHINENAME { get; set; }
        public string PORTNAME { get; set; }
        public string TRAYNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTIONTYPE { get; set; }
        public string PRODUCTQUANTITY { get; set; }
        [XmlElement("PANELLIST")]
        public TrayProcessEndFDCPANELLIST PANELLIST { get; set; }
    }
    [Serializable]
    [XmlRoot("PANEL")]
    public class TrayProcessEndFDCPANELLIST
    {
        public TrayProcessEndFDCPANELLIST()
        {
            PANELLIST = new List<TrayProcessEndFDCPANEL>();
        }
        [XmlElement("PANEL")]
        public List<TrayProcessEndFDCPANEL> PANELLIST { get; set; }
    }
    public class TrayProcessEndFDCPANEL
    {
        public TrayProcessEndFDCPANEL()
        {
            PANELNAME = "";
            POSITION = "";
            PRODUCTJUDGE = "";
            PRODUCTGRADE = "";
        }
        public string PANELNAME { get; set; }       
        public string POSITION { get; set; }
        public string PRODUCTJUDGE { get; set; }
        public string PRODUCTGRADE { get; set; }
    }
}
