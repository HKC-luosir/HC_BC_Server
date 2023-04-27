using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelTrackOut : RVBodyBase
    {
        public RVPanelTrackOut()
        {
            MessageName = "LCM.PANELTRACKOUTREPORT";
            DEFECTLIST = new List<RVDEFECTCODE>();
        }
        public string EQUIPMENTID { get; set; }
        public string PANELID { get; set; }
        public string LOTTYPE { get; set; }
        public string POSITION { get; set; }
        public string BONDINGID { get; set; }
        public string GRADE { get; set; }
        public string ABNORMALCODE { get; set; }
        [XmlArray("DEFECTLIST")]
        [XmlArrayItem("DEFECT")]
        public List<RVDEFECTCODE> DEFECTLIST { get; set; }
    }
}
