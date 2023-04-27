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
   public class MESRule
    {
        public MESRule()
        {
            mappingItemList = new mappingItemList();
          
        }
        [XmlElement("mappingItemList")]
        public mappingItemList mappingItemList { get; set; }
        
    }
    #region mappingItemList
    [Serializable]
    [XmlRoot("mappingItemList")]
    public class mappingItemList
    {
        public mappingItemList()
        {
            mappingItems = new List<mappingItem>();
        }
        [XmlElement("mappingItem")]

        public List<mappingItem> mappingItems { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingItem")]
    public class mappingItem
    {
        public mappingItem()
        {
            mappingValueList = new List<mappingValueClass>();
        }
        [XmlAttribute]
        public string name { get; set; }
        [XmlElement("mappingValue")]
        public List<mappingValueClass> mappingValueList { get; set; }
    }
    [Serializable]
    [XmlRoot("mappingValue")]
    public class mappingValueClass
    {
        public mappingValueClass()
        {
        }
        [XmlAttribute]
        public string value { get; set; }
        [XmlAttribute]
        public string mappingValue { get; set; }
    }
    #endregion
    

}
