using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    //matti 20220315
    [Serializable]
    [XmlRoot("BODY")]
    public class RVProcessData : RVBodyBase
    {
        public RVProcessData()
        {
            MessageName = "ProductProcessData";
            ITEMLIST = new List<Item>();
        }
        public string MACHINENAME { get; set; } = "";
        public string UNITNAME { get; set; } = "";
        public string SUBUNITNAME { get; set; } = "";
        public string LOTNAME { get; set; } = "";
        public string CARRIERNAME { get; set; } = "";
        public string PRODUCTNAME { get; set; } = "";
        public string MACHINERECIPENAME { get; set; } = "";
        public string PROCESSOPERATIONNAME { get; set; } = "";
        public string PRODUCTSPECNAME { get; set; } = "";
        public string POSITION { get; set; } = "";
        public string PROCESSTIME { get; set; } = "";
        [XmlArray("ITEMLIST")]
        [XmlArrayItem("ITEM")]
        public List<Item> ITEMLIST { get; set; }
    }

    public class Item
    {
        public Item()
        {
            SITELIST = new List<Site>();
        }
        public string ITEMNAME { get; set; }
        [XmlArray("SITELIST")]
        [XmlArrayItem("SITE")]
        public List<Site> SITELIST { get; set; }
    }

    public class Site
    {
        [XmlIgnore]
        public int ID { get; set; }
        public string SITENAME { get; set; }
        public string SITEVALUE { get; set; }
    }
}
