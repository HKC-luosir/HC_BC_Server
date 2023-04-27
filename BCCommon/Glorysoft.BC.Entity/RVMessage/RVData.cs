using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.Entity.RVMessage
{
    [XmlRoot("Request")]
    public class RVData
    {
        public RVData()
        {
        }
        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public string ConnName { get; set; } = "";

        public string StringXml { get; set; }
        public Message Message { get; set; }

    }
}
