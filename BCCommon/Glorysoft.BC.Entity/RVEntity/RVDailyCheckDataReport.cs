using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVDailyCheckDataReport : RVBodyBase
    {
        public RVDailyCheckDataReport()
        {
            MessageName = "M2.DAILYCHECKDATAREPORT";
            CHECKTYPE = "DailyCheck";
            ITEMLIST = new List<DailyCheckDataItem>();
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PARTNAME { get; set; }
        public string RECIPENAME { get; set; }
        /// <summary>
        /// 区分周期检还是日常检:
        ///{DailyCheck|CycleCheck}
        /// </summary>
        public string CHECKTYPE { get; set; }
        [XmlArray("ITEMLIST")]
        [XmlArrayItem("ITEM")]
        public List<DailyCheckDataItem> ITEMLIST { get; set; }
    }

    public class DailyCheckDataItem
    {
        public string ITEMNAME { get; set; }
        public string ITEMVALUE { get; set; }
    }
}
