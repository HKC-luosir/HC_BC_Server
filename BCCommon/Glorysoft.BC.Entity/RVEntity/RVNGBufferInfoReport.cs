using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVNGBufferInfoReport : RVBodyBase
    {
        public RVNGBufferInfoReport()
        {
            MessageName = "M2.NGBUFFERINFOREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string SUBUNITID { get; set; }
        public string QTY { get; set; }
    }
}
