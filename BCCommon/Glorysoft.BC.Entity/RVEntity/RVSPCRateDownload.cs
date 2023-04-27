using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVSPCRateDownload : RVBodyBase
    {
        public RVSPCRateDownload()
        {
            MessageName = "SPCRATEDOWNLOAD";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string RATE { get; set; }
    }
}
