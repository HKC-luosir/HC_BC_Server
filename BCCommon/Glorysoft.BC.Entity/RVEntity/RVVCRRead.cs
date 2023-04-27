using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVVCRRead : RVBodyBase
    {
        public RVVCRRead()
        {
            MessageName = "HKC.VCRREADREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PANELID { get; set; }
        public string VCRPANELID { get; set; }
        public string VCRSTATE { get; set; } 
        public string RESULT { get; set; } 
    }
}
