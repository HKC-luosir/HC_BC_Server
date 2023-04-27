using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCuttingComplete : RVBodyBase
    {
        public RVCuttingComplete()
        {
            MessageName = "LCM.CUTTINGCOMPLETEREPORT";
        }
        public string ACTIONTYPE { get; set; } = "AUTOCUT";
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
        public string RECIPENAME { get; set; }
        public string GLASSID { get; set; }
        /// <summary>
        /// 是否补发的消息:补发的不需要Check PANELLIST，已MES的为准
        ///{Y:补发 |N:正常报的}
        /// </summary>
        public string RESENDFLAG { get; set; } = "N";
        [XmlArray("PANELLIST")]
        [XmlArrayItem("PANEL")]
        public List<RVCuttingCompletePanel> PANELLIST = new List<RVCuttingCompletePanel>();
    }

    public class RVCuttingCompletePanel
    {
        public string PANELID { get; set; }
        public string GRADE { get; set; }
    }
}
