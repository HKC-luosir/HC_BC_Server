using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPortState : RVBodyBase
    {
        public RVPortState()
        {
            MessageName = "MES.EQUIPMENTPORTREPORT";
            PORTLIST = new List<RVPortStateInfo>();
        }
        public string EQUIPMENTID { get; set; }
        [XmlArray("PORTLIST")]
        [XmlArrayItem("PORT")]
        public List<RVPortStateInfo> PORTLIST { get; set; }
    }

    [Serializable]
    public class RVPortStateInfo
    {
        public string PORTNUM { get; set; }
        public string PORTID { get; set; }
        public string PORTSTATE { get; set; }
        public string PORTTYPE { get; set; }
        /// <summary>
        /// 卡匣ID
        /// </summary>
        public string DURABLEID { get; set; }
    }
}
