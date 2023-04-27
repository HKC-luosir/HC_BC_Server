using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVEQPMaterialQTY : RVBodyBase
    {
        public RVEQPMaterialQTY()
        {
            MessageName = "FMC.EQPMATERIALQTYREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PORTID { get; set; }
        public string MATERIALTYPE { get; set; }
        public string MATERIALQTY { get; set; }
    }
}
