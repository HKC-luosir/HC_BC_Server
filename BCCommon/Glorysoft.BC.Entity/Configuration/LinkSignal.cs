using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.Configuration
{
    [Serializable]
    [XmlRoot("configuration")]
    public class LinkSignal
    {
        public LinkSignal()
        {
            LinkSignalMappingItemList = new LinkSignalMappingItemList();
        }       
        [XmlElement("mappingItemList")]
        public LinkSignalMappingItemList LinkSignalMappingItemList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItemList")]
    public class LinkSignalMappingItemList
    {
        public LinkSignalMappingItemList()
        {
            mappingItems = new List<LinkSignalMappingItem>();
        }
        [XmlElement("mappingItem")]

        public List<LinkSignalMappingItem> mappingItems { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItem")]
    public class LinkSignalMappingItem
    {
        public LinkSignalMappingItem()
        {
            LinkSignalMappingValueList = new List<LinkSignalMappingValue>();
        }
        [XmlAttribute]
        public string name { get; set; }
        [XmlElement("mappingValue")]
        public List<LinkSignalMappingValue> LinkSignalMappingValueList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingValue")]
    public class LinkSignalMappingValue
    {
        public LinkSignalMappingValue()
        {           
        }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Offset { get; set; }
        [XmlAttribute]
        public string Points { get; set; }
        [XmlAttribute]
        public string Type { get; set; }
    }
}
