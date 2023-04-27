using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.RV.Entity
{
    [XmlRoot("Request")]
    public class NewMESData
    {
        public NewMESData()
        {
            RVBody = new RVBody();
        }
        [XmlElement("Header")]
        public NewRVHeader NewRVHeader { get; set; }
        [XmlElement("Body")]
        public RVBody RVBody { get; set; }

    }
}
