using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelTrackInOut : RVBodyBase
    {
        public RVPanelTrackInOut()
        {
            MessageName = "LCM.PANELTRACKINOUTREPORT";
            DEFECTLIST = new List<RVDEFECTCODE>();
        }
        public string EQUIPMENTID { get; set; }
        public string NEXTEQUIPMENTID { get; set; }
        public string PANELID { get; set; }
        public string LOTTYPE { get; set; }
        public string POSITION { get; set; }
        public string BONDINGID { get; set; }
        public string GRADE { get; set; }
        public string LCDGRADE { get; set; }
        public string MODJUDGEFLAG { get; set; }
        public string ABNORMALCODE { get; set; }
        [XmlArray("DEFECTLIST")]
        [XmlArrayItem("DEFECT")]
        public List<RVDEFECTCODE> DEFECTLIST { get; set; }
    }

    public class RVDEFECTCODE
    {
        public string DEFECTCODE { get; set; }
        public string DEFECTMAIN { get; set; }
        public string MAINFLAG { get; set; }
    }
}
