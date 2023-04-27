using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    [Serializable]
    [XmlRoot(ElementName = "Body")]
    public class Body
    {
        public Body()
        {
            Header = new Header();
            //  DataLayer = new DataLayer();
            Return = new Return();
        }
        public Header Header { get; set; }
        public DataLayer DataLayer { get; set; }
        public Return Return { get; set; }
        [XmlElement("Header")]
        [XmlArray("DataLayer")]
        [XmlElement("Return")]
        [XmlIgnoreAttribute]
        public string StringXml { get; set; }
        [XmlIgnoreAttribute]
        public XElement XmlBody { get; set; }

    }
}
