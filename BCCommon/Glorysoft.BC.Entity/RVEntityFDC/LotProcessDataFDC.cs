
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class LotProcessDataFDC
    {
        public LotProcessDataFDC()
        {
            ITEMLIST = new LotProcessDataFDCITEMLIST();
               MACHINENAME ="";
              UNITNAME ="";
              SUBUNITNAME ="";
              SUBSUBUNITNAME ="";
              LOTNAME ="";
              CARRIERNAME ="";
              MACHINERECIPENAME ="";
              PROCESSOPERATIONNAME ="";
              PRODUCTSPECNAME ="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string SUBUNITNAME { get; set; }
        public string SUBSUBUNITNAME { get; set; }
        public string LOTNAME { get; set; }
        public string CARRIERNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string PROCESSOPERATIONNAME { get; set; }
        public string PRODUCTSPECNAME { get; set; }

        [XmlElement("ITEMLIST")]
        public LotProcessDataFDCITEMLIST ITEMLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("ITEM")]
    public class LotProcessDataFDCITEMLIST
    {
        public LotProcessDataFDCITEMLIST()
        {
            ITEMLIST = new List<LotProcessDataFDCITEM>();
        }
        [XmlElement("ITEM")]
        public List<LotProcessDataFDCITEM> ITEMLIST { get; set; }
    }
    public class LotProcessDataFDCITEM
    {
        public LotProcessDataFDCITEM()
        {
            SITELIST = new LotProcessDataFDCSITELIST();
        }
        public string ITEMNAME { get; set; }
        [XmlElement("SITELIST")]
        public LotProcessDataFDCSITELIST SITELIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SITE")]
    public class LotProcessDataFDCSITELIST
    {
        public LotProcessDataFDCSITELIST()
        {
            SITELIST = new List<LotProcessDataFDCSITE>();
        }
        [XmlElement("SITE")]
        public List<LotProcessDataFDCSITE> SITELIST { get; set; }
    }
    public class LotProcessDataFDCSITE
    {
        public LotProcessDataFDCSITE()
        {
            SITENAME = "";
            SITEVALUE = "";
        }
        public string SITENAME { get; set; }
        public string SITEVALUE { get; set; }
    }
}
