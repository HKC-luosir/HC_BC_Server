

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ProductProcessDataFDC
    {
        public ProductProcessDataFDC()
        {
            ITEMLIST = new ProductProcessDataFDCITEMLIST();
             MACHINENAME ="";
             UNITNAME ="";
             SUBUNITNAME ="";
             SUBSUBUNITNAME ="";
             LOTNAME ="";
             CARRIERNAME ="";
             PRODUCTNAME ="";
             MACHINERECIPENAME ="";
             PROCESSOPERATIONNAME ="";
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
        public string PRODUCTSPECNAME { get; set; }
        [XmlElement("ITEMLIST")]
        public ProductProcessDataFDCITEMLIST ITEMLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("ITEM")]
    public class ProductProcessDataFDCITEMLIST
    {
        public ProductProcessDataFDCITEMLIST()
        {
            ITEMLIST = new List<ProductProcessDataFDCITEM>();
        }
        [XmlElement("ITEM")]
        public List<ProductProcessDataFDCITEM> ITEMLIST { get; set; }
    }
    public class ProductProcessDataFDCITEM
    {
        public ProductProcessDataFDCITEM()
        {
            SITELIST = new ProductProcessDataFDCSITELIST();
            ITEMNAME = "";
        }
        public string ITEMNAME { get; set; }
        [XmlElement("SITELIST")]
        public ProductProcessDataFDCSITELIST SITELIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SITE")]
    public class ProductProcessDataFDCSITELIST
    {
        public ProductProcessDataFDCSITELIST()
        {
            SITELIST = new List<ProductProcessDataFDCSITE>();
        }
        [XmlElement("SITE")]
        public List<ProductProcessDataFDCSITE> SITELIST { get; set; }
    }
    public class ProductProcessDataFDCSITE
    {
        public ProductProcessDataFDCSITE()
        {
            SITENAME = "";
            SITEVALUE = "";
        }
        public string SITENAME { get; set; }
        public string SITEVALUE { get; set; }
    }
}
