using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    [XmlRoot("Message")]
    public class WHMessage
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public WHMessage()
        {
        }
        [XmlElement("Body")]
        public Body Body { get; set; }


    }
}
