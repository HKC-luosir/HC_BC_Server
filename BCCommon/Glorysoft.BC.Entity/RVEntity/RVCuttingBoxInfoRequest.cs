using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCuttingBoxInfoRequest : RVBodyBase
    {
        public RVCuttingBoxInfoRequest()
        {
            MessageName = "LCM.CUTTINGBOXINFOREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string BOXID { get; set; }
    }
}
