using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVPortCSTType : RVBodyBase
    {
        public RVPortCSTType()
        {
            MessageName = "MES.PORTUSETYPEREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTNUM { get; set; }
        public string PORTID { get; set; }
        /// <summary>
        /// Port类型
        /// </summary>
        public string PORTTYPE { get; set; }
        /// <summary>
        /// 卡匣类型
        /// </summary>
        public string PORTUSETYPE { get; set; }
    }
}
