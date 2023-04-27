using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVMaterialValidation : RVBodyBase
    {
        public RVMaterialValidation()
        {
            MessageName = "MES.MATERIALVALIDATION";
        }
        public string EQUIPMENTID { get; set; }
        public string OPERATOR { get; set; }
        [XmlArray("UNITLIST")]
        [XmlArrayItem("UNIT")]
        public List<RVUNIT> UNITLIST = new List<RVUNIT>();
    }
    [Serializable]
    public class RVUNIT
    {
        public string UNITID { get; set; }
        public string PORTID { get; set; }
        [XmlArray("MLOTLIST")]
        [XmlArrayItem("MLOT")]
        public List<RVMaterialList> MLOTLIST = new List<RVMaterialList>();
    }
}
