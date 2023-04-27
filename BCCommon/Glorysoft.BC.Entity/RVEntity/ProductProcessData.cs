
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("BODY")]
    public class ProductProcessData
    {
        public ProductProcessData()
        {
            ITEMLIST = new ProductProcessDataITEMLIST();
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string SUBUNITNAME { get; set; }
        public string SUBSUBUNITNAME { get; set; }
        public string LOTNAME { get; set; }
        public string CARRIERNAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string PROCESSOPERATIONNAME { get; set; }

        [XmlElement("ITEMLIST")]
        public ProductProcessDataITEMLIST ITEMLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("ITEM")]
    public class ProductProcessDataITEMLIST
    {
        public ProductProcessDataITEMLIST()
        {
            ITEMLIST = new List<ProductProcessDataITEM>();
        }
        [XmlElement("ITEM")]
        public List<ProductProcessDataITEM> ITEMLIST { get; set; }
    }
    public class ProductProcessDataITEM
    {
        public ProductProcessDataITEM()
        {
            SITELIST = new ProductProcessDataSITELIST();
        }
        public string ITEMNAME { get; set; }
        [XmlElement("SITELIST")]
        public ProductProcessDataSITELIST SITELIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SITE")]
    public class ProductProcessDataSITELIST
    {
        public ProductProcessDataSITELIST()
        {
            SITELIST = new List<ProductProcessDataSITE>();
        }
        [XmlElement("SITE")]
        public List<ProductProcessDataSITE> SITELIST { get; set; }
    }
    public class ProductProcessDataSITE
    {
        public string SITENAME { get; set; }
        public string SITEVALUE { get; set; }
    }
}
