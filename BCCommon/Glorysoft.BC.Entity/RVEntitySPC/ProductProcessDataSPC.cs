using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntitySPC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ProductProcessDataSPC
    {
        public ProductProcessDataSPC()
        {
            ITEMLIST = new ProductProcessDataSPCITEMLIST();
               MACHINENAME ="";
              UNITNAME ="";
              SUBUNITNAME ="";
              LOTNAME ="";
              CARRIERNAME ="";
              PRODUCTNAME ="";
              MACHINERECIPENAME ="";
              PROCESSOPERATIONNAME ="";
              PROCESSOPERATIONVERSION ="";
              PRODUCTSPECNAME ="";
              PRODUCTSPECVERSION ="";
              PROCESSFLOWNAME ="";
              PROCESSFLOWVERSION ="";
              BEFOREPROCESSMACHINE ="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string SUBUNITNAME { get; set; }        
        public string LOTNAME { get; set; }
        public string CARRIERNAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        public string PROCESSOPERATIONNAME { get; set; }
        public string PROCESSOPERATIONVERSION { get; set; }
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTSPECVERSION { get; set; }
        public string PROCESSFLOWNAME { get; set; }
        public string PROCESSFLOWVERSION { get; set; }
        public string BEFOREPROCESSMACHINE { get; set; }

        [XmlElement("ITEMLIST")]
        public ProductProcessDataSPCITEMLIST ITEMLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("ITEM")]
    public class ProductProcessDataSPCITEMLIST
    {
        public ProductProcessDataSPCITEMLIST()
        {
            ITEMLIST = new List<ProductProcessDataSPCITEM>();
        }
        [XmlElement("ITEM")]
        public List<ProductProcessDataSPCITEM> ITEMLIST { get; set; }
    }
    public class ProductProcessDataSPCITEM
    {
        public ProductProcessDataSPCITEM()
        {
            SITELIST = new ProductProcessDataSPCSITELIST();
            ITEMNAME = "";
        }
        public string ITEMNAME { get; set; }
        [XmlElement("SITELIST")]
        public ProductProcessDataSPCSITELIST SITELIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SITE")]
    public class ProductProcessDataSPCSITELIST
    {
        public ProductProcessDataSPCSITELIST()
        {
            SITELIST = new List<ProductProcessDataSPCSITE>();
        }
        [XmlElement("SITE")]
        public List<ProductProcessDataSPCSITE> SITELIST { get; set; }
    }
    public class ProductProcessDataSPCSITE
    {
        public ProductProcessDataSPCSITE()
        {
            SITENAME = "";
            SITEVALUE = "";
        }
        public string SITENAME { get; set; }
        public string SITEVALUE { get; set; }
    }
}
