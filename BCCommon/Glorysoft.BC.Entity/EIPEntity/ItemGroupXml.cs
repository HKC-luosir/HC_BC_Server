using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("configuration")]
    public class ItemGroupXml
    {
        public ItemGroupXml()
        {
            GroupMap = new List<GItemGroupMapMrg>();
        }
        [XmlElement("ItemGroup")]
        public List<GItemGroupMapMrg> GroupMap { get; set; }
    }
    [Serializable]
    [XmlRoot("ItemGroup")]
    public class GItemGroupMapMrg
    {
        public GItemGroupMapMrg()
        {
            ItemMrg = new List<GItemMrg>();
        }
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlElement("Item")]
        public List<GItemMrg> ItemMrg { get; set; }
    }

    [Serializable]
    public class GItemMrg
    {
        [XmlAttribute("Name")]
        public virtual string Name { get; set; }
        [XmlAttribute("Offset")]
        public string Offset { get; set; }
        [XmlAttribute("Points")]
        public string Points { get; set; }
        [XmlAttribute("Type")]
        public string Type { get; set; }
    }
}
