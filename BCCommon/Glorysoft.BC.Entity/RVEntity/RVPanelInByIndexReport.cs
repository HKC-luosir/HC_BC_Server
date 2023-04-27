using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelInByIndexReport : RVBodyBase
    {
        public RVPanelInByIndexReport()
        {
            MessageName = "LCM.PANELINBYINDEXREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string PANELID { get; set; }
        public string LOTTYPE { get; set; }
        public string POSITION { get; set; }
        public string DURABLEID { get; set; }
        public string BONDINGID { get; set; }
        public string GRADE { get; set; }
        public string ABNORMALCODE { get; set; }
        [XmlArray("DEFECTLIST")]
        [XmlArrayItem("DEFECT")]
        public List<RVDEFECTCODE> DEFECTLIST = new List<RVDEFECTCODE>();
    }
}
