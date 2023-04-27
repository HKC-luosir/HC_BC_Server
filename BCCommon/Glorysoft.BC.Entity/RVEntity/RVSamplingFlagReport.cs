using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVSamplingFlagReport : RVBodyBase
    {
        public RVSamplingFlagReport()
        {
            MessageName = "M2.SAMPLINGFLAGREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PANELID { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
    }
}
