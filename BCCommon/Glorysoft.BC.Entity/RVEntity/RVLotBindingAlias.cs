using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLotBindingAlias : RVBodyBase
    {
        public RVLotBindingAlias()
        {
            MessageName = "HKC.LOTBINDING";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        /// <summary>
        /// BINDING 绑定MATERIALSN|UNBINDING 解绑MATERIALSN
        /// </summary>
        public string ACTIONTYPE { get; set; }
        /// <summary>
        /// BLID/PCBID
        /// </summary>
        public string MATERIALSN { get; set; }
        /// <summary>
        /// PanelId
        /// </summary>
        public string LOTID { get; set; }
    }
}
