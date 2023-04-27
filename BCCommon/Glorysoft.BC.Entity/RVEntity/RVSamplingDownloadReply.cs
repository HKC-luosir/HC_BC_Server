using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVSamplingDownloadReply : RVBodyBase
    {
        public RVSamplingDownloadReply()
        {
            MessageName = "M2.SAMPLINGDOWNLOAD";
        }
    }
}
