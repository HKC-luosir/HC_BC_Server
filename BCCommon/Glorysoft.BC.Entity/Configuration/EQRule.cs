
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
    public class EQRule
    {
        public EQRule()
        {
            EQMappingItemList = new EQMappingItemList();
        }
        [XmlElement("mappingItemList")]
        public EQMappingItemList EQMappingItemList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItemList")]
    public class EQMappingItemList
    {
        public EQMappingItemList()
        {
            mappingItems = new List<EQMappingItem>();
        }
        [XmlElement("mappingItem")]

        public List<EQMappingItem> mappingItems { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItem")]
    public class EQMappingItem
    {
        public EQMappingItem()
        {
            EQMappingValueList = new List<EQMappingValue>();
        }
        [XmlAttribute]
        public string name { get; set; }
        [XmlElement("mappingValue")]
        public List<EQMappingValue> EQMappingValueList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingValue")]
    public class EQMappingValue
    {
        public EQMappingValue()
        {
        }
        [XmlAttribute]
        public string value { get; set; }
        [XmlAttribute]
        public string mappingValue { get; set; }
    }
}
