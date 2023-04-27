using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVUnitState : RVBodyBase
    {
        public RVUnitState()
        {
            MessageName = "MES.EQUIPMENTUNITREPORT";
            UNITLIST = new List<RVUnitList>();
        }
        public string EQUIPMENTID { get; set; }
        [XmlArray("UNITLIST")]
        [XmlArrayItem("UNIT")]
        public List<RVUnitList> UNITLIST { get; set; }
    }

    [Serializable]
    public class RVUnitList
    {
        public string UNITID { get; set; }
        public string COMCLASS { get; set; }
        public string STATE { get; set; }
    }
}
