using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVAlarmReport : RVBodyBase
    {
        public RVAlarmReport()
        {
            MessageName = "MES.EQPALARMREPORT";
            CATEGORY = "BC";
            ALARMTYPE = "EQ";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string CATEGORY { get; set; }
        public string ALARMTYPE { get; set; }
        public string ALARMID { get; set; }
        public string ALARMCODE { get; set; }
        public string ALARMTEXT { get; set; }
        public string ALARMLEVEL { get; set; }
        public string ALARMSTATE { get; set; }
    }
}
