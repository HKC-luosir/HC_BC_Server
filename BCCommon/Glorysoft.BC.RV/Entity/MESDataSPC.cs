using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.RV.Entity
{
    [XmlRoot("MESSAGE")]
    public class MESDataSPC
    {
        public MESDataSPC()
        {
            RVBody = new RVBodySPC();
            RVReturn = new RVReturnSPC();
        }
        [XmlElement("HEADER")]
        public RVHeaderSPC RVHeader { get; set; }
        [XmlElement("BODY")]
        public RVBodySPC RVBody { get; set; }
        [XmlElement("RETURN")]
        public RVReturnSPC RVReturn { get; set; }        
    }
}
