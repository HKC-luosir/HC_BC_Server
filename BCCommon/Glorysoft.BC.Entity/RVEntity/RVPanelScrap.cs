using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelScrap : RVBodyBase
    {
        public RVPanelScrap()
        {
            MessageName = "M2.PANELSCRAP";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string SUBUNITID { get; set; }
        public string PANELID { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
        public string GRADE { get; set; }
    }
}
