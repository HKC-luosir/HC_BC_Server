using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVMaterialCountReport : RVBodyBase
    {
        public RVMaterialCountReport()
        {
            MessageName = "MES.MATERIALCOUNTREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string ACTIONTYPE { get; set; }
        public string MATERIALNAME { get; set; }
        public string MATERIALTYPE { get; set; }
        public string MLOTID { get; set; }
        public string USEQTY { get; set; }
        public string MATERIALSN { get; set; }
    }
}
