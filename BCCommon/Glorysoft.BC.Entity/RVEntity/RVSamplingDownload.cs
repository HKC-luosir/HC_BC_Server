using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVSamplingDownload : RVBodyBase
    {
        public RVSamplingDownload()
        {
            MessageName = "M2.SAMPLINGDOWNLOAD";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PARTNAME { get; set; }
        public string BATCHQTY { get; set; }
        public string SAMPLINGQTY { get; set; }
    }
}
