using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVSPCRateDownloadReply : RVBodyBase
    {
        public RVSPCRateDownloadReply()
        {
            MessageName = "SPCRATEDOWNLOAD";
        }
    }
}
