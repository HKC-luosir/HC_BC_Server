using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVRecipeChangeReport : RVBodyBase
    {
        public RVRecipeChangeReport()
        {
            MessageName = "MES.RECIPEPARAMETERREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string RECIPENAME { get; set; }
        [XmlArray("PARAMALIST")]
        [XmlArrayItem("RECIPEPARAMETER")]
        public List<PARAM> PARAMLIST = new List<PARAM>();
    }
}
