using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    public class RVBodyBase
    {
        [XmlIgnore]
        public string MessageName { get; set; }
        [XmlIgnore]
        public RVHeader replyHeader { get; set; }
    }
}
