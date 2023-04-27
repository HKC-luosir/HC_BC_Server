using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCarrierProcessStart : RVBodyBase
    {
        public RVCarrierProcessStart()
        {
            MessageName = "LCM.CARRIERPROCESSSTART";
        }
        public string EQUIPMENTID { get; set; }
        public string DURABLEID { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTNUM { get; set; }
        public string PORTID { get; set; }
    }
}
