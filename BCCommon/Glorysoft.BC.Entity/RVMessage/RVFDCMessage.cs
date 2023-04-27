using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    [XmlRoot("Message")]
    public class RVFDCMessage
    {
        public RVFDCMessage()
        {
            Body = new List<Body>();
        }
        [XmlElement("Header")]
        public FDCHeader Header { get; set; }
        [XmlArray("Body")]

        public List<Body> Body { get; set; }

        [XmlElement("Return")]
        public Return Return { get; set; }

        public string StringXml { get; set; }

    }
}
