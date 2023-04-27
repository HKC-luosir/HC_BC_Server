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
    public class RVPanelInOut : RVBodyBase
    {
        public RVPanelInOut()
        {
            MessageName = "LCM.PANELLINEOUT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PANELID { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
        public string GRADE { get; set; }
        public string ACTIONTYPE { get; set; }
    }
}
