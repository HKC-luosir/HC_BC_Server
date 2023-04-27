using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    //matti 20220314
    [Serializable]
    [XmlRoot("Body")]
    public class RVUnitOut : RVBodyBase
    {
        public RVUnitOut()
        {
            MessageName = "LCM.UNITOUTREPORT";
        }
        public string ACTIONTYPE { get; set; }//操作类型{UNITOUT|SUBUNITOUT}
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string SUBUNITID { get; set; }
        public string PANELID { get; set; }
        /// <summary>
        /// 机种名称
        /// </summary>
        public string PARTNAME { get; set; }
        /// <summary>
        /// Step名称
        /// </summary>
        public string STEPNAME { get; set; }
        /// <summary>
        /// Recipe名称
        /// </summary>
        public string RECIPENAME { get; set; }
        /// <summary>
        /// 进Unit时间
        /// </summary>
        public string INTIME { get; set; }
        /// <summary>
        /// 出Unit时间
        /// </summary>
        public string OUTTIME { get; set; }
    }
}
