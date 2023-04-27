using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVDefectAlarm : RVBodyBase
    {
        public RVDefectAlarm()
        {
            MessageName = "DEFECTALARM";
        }
        public string EQUIPMENTID { get; set; }
        public string ALARMTEXT { get; set; }
        public string ALARMCODE { get; set; }
        public string ALARMLEVEL { get; set; }

        [XmlArray("UNITLIST")]
        [XmlArrayItem("UNIT")]
        public List<RVUnitList> UNITLIST { get; set; }
    }
}
