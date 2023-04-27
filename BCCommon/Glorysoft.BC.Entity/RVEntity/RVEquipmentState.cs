using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVEquipmentState : RVBodyBase
    {
        public RVEquipmentState()
        {
            MessageName = "MES.EQUIPMENTSTATEREPORT";
            ACTIONTYPE = "STATE";
        }
        public string ACTIONTYPE { get; set; }
        public string EQUIPMENTID { get; set; }
        public string COMCLASS { get; set; }
        public string STATE { get; set; }
    }
}
