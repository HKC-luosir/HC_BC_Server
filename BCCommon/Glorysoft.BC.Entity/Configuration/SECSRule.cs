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
    public class SECSRule
    {
        public SECSRule()
        {
            SECSMappingItemList = new SECSMappingItemList();
        }       
        [XmlElement("mappingItemList")]
        public SECSMappingItemList SECSMappingItemList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItemList")]
    public class SECSMappingItemList
    {
        public SECSMappingItemList()
        {
            mappingItems = new List<SECSMappingItem>();
        }
        [XmlElement("mappingItem")]

        public List<SECSMappingItem> mappingItems { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItem")]
    public class SECSMappingItem
    {
        public SECSMappingItem()
        {
            SECSMappingValueList = new List<SECSMappingValue>();
        }
        [XmlAttribute]
        public string name { get; set; }
        [XmlElement("mappingValue")]
        public List<SECSMappingValue> SECSMappingValueList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingValue")]
    public class SECSMappingValue
    {
        public SECSMappingValue()
        {           
        }
        [XmlAttribute]
        public string value { get; set; }
        [XmlAttribute]
        public string mappingValue { get; set; }
    }
}
