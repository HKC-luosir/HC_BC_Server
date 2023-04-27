using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelTrackIn : RVBodyBase
    {
        public RVPanelTrackIn()
        {
            MessageName = "LCM.PANELTRACKINREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string PANELID { get; set; }
        public string LOTTYPE { get; set; }
        public string POSITION { get; set; }
        public string GRADE { get; set; }
    }
}
