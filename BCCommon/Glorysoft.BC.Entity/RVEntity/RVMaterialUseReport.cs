using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVMaterialUseReport : RVBodyBase
    {
        public RVMaterialUseReport()
        {
            MessageName = "MES.MATERIALUSEREPORT";
            MLOTLIST = new List<RVMaterialList>();
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PANELID { get; set; }
        [XmlArray("MLOTLIST")]
        [XmlArrayItem("MLOT")]
        public List<RVMaterialList> MLOTLIST { get; set; }
    }
}
