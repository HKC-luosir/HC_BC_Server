using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVSamplingRequest : RVBodyBase
    {
        public RVSamplingRequest()
        {
            MessageName = "M2.SAMPLINGREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PARTNAME { get; set; }
    }
}
