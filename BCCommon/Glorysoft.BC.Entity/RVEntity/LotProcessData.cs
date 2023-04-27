
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("BODY")]
    public class LotProcessData
    {
        public LotProcessData()
        {
            ITEMLIST = new LotProcessDataITEMLIST();
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
        public LotProcessDataITEMLIST ITEMLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("ITEM")]
    public class LotProcessDataITEMLIST
    {
        public LotProcessDataITEMLIST()
        {
            ITEMLIST = new List<LotProcessDataITEM>();
        }
        [XmlElement("ITEM")]
        public List<LotProcessDataITEM> ITEMLIST { get; set; }
    }
    public class LotProcessDataITEM
    {
        public LotProcessDataITEM()
        {
            SITELIST = new LotProcessDataSITELIST();
        }
        public string ITEMNAME { get; set; }
        [XmlElement("SITELIST")]
        public LotProcessDataSITELIST SITELIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SITE")]
    public class LotProcessDataSITELIST
    {
        public LotProcessDataSITELIST()
        {
            SITELIST = new List<LotProcessDataSITE>();
        }
        [XmlElement("SITE")]
        public List<LotProcessDataSITE> SITELIST { get; set; }
    }
    public class LotProcessDataSITE
    {
        public string SITENAME { get; set; }
        public string SITEVALUE { get; set; }
    }
}
