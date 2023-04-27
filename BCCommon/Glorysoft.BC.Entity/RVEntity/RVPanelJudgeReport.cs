using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelJudgeReport : RVBodyBase
    {
        public RVPanelJudgeReport()
        {
            MessageName = "LCM.PANELJUDGEREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string LOTID { get; set; }
        public string GRADE { get; set; }
        public string ABNORMALCODE { get; set; }
        public string ISOUTSOURCING { get; set; }
        [XmlArray("DEFECTLIST")]
        [XmlArrayItem("DEFECT")]
        public List<RVDEFECTCODE> DEFECTLIST { get; set; }
    }
}
