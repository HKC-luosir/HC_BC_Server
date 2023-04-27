using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVWeightReport : RVBodyBase
    {
        public RVWeightReport()
        {
            MessageName = "LCM.WEIGHTREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string BOXID { get; set; }
        public string WEIGHT { get; set; }
    }
}
