using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVRecipeParameterRequest : RVBodyBase
    {
        public RVRecipeParameterRequest()
        {
            UNITLIST = new List<UNITRECIPE>();
            MessageName = "MES.RECIPEPARAMREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        [XmlArray("UNITLIST")]
        [XmlArrayItem("UNIT")]
        public List<UNITRECIPE> UNITLIST { get; set; }
    }
}
