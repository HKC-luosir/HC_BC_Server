using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    //matti 20220315
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelInfoDownload : RVBodyBase
    {
        public RVPanelInfoDownload()
        {
            MessageName = "LCM.PANELINFOREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PANELID { get; set; }
        public string BONDINGID { get; set; }
        /// <summary>
        /// PCB / BL / 空-PANEL
        /// </summary>
        public string IDTYPE { get; set; }
    }
}
