
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.Configuration
{
    [Serializable]
    [XmlRoot("RVMappingConfig")]
    public class RVMappingConfig
    {
        //Tibco 实体类
        public RVMappingConfig()
        {
            MappingList = new List<Mapping>();
        }



        [XmlElement("Mapping")]

        public List<Mapping> MappingList { get; set; }

    }


    [Serializable]
    [XmlRoot("Mapping")]
    public class Mapping
    {
        public Mapping()
        {
           
        }
        //[XmlArray("MessageName")]
        [XmlAttribute]
        public string MessageName { get; set; }
        //[XmlArray("ConnectionName")]
        [XmlAttribute]
        public string ConnectionName { get; set; }
    }


}
