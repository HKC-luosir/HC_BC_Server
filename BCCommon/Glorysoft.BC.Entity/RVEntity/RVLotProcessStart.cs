using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLotProcessStart : RVBodyBase
    {
        public RVLotProcessStart()
        {
            MessageName = "LCM.LOTPROCESSSTART";
        }
        public string EQUIPMENTID { get; set; }
        public string DURABLEID { get; set; }
        public string LOTID { get; set; }
        public string PORTID { get; set; }
        public string ACTIONTYPE { get; set; }//{CUTTING:CUTTING线;NORMAL:NORMAL正常线}
    }
}
