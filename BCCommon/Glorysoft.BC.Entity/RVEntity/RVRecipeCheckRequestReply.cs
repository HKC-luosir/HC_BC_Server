using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVRecipeCheckRequestReply : RVBodyBase
    {
        public RVRecipeCheckRequestReply()
        {
            MessageName = "";
        }
        public string EQUIPMENTID { get; set; }
        public string DURABLEID { get; set; }
        public string LOTID { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }

        [XmlArray("UNITLIST")]
        [XmlArrayItem("UNIT")]
        public List<UNITRECIPE> UNITRECIPELIST = new List<UNITRECIPE>();
    }
}
