using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.RV.Entity
{
    [XmlRoot("Response")]
    public class MESReplyData
    {

        public MESReplyData()
        {
            RVBody = new RVBody();
        }
        [XmlElement("Header")]
        public RVHeader RVHeader { get; set; }
        [XmlElement("Body")]
        public RVBody RVBody { get; set; }
    }
}
