using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCuttingBoxInfoRequestReply : RVBodyBase
    {
        public RVCuttingBoxInfoRequestReply()
        {
            MessageName = "";
        }
        public string EQUIPMENTID { get; set; }
        public string BOXID { get; set; }
        public string PALLETID { get; set; }
        public string PARTNAME { get; set; }
        public string RECIPENAME { get; set; }
        public string STEPNAME { get; set; }
        public string LOTID { get; set; }
        public string LOTTYPE { get; set; }
        public string GRADE { get; set; }
        public string QTY { get; set; }
        [XmlArray("UNITRECIPELIST")]
        [XmlArrayItem("UNITRECIPE")]
        public List<UNITRECIPE> UNITRECIPELIST = new List<UNITRECIPE>();
        [XmlArray("GLASSLIST")]
        [XmlArrayItem("GLASS")]
        public List<GLASS> GLASSLIST = new List<GLASS>();
    }
    public class GLASS
    {
        public GLASS()
        {
            GLASSID = "";
            GRADE = "";
            SUBGRADE = "";
            POSITION = "";
        }
        public string GLASSID { get; set; }
        public string WOID { get; set; }
        public string GRADE { get; set; }
        public string POSITION { get; set; }
        public string SUBGRADE { get; set; }
        [XmlArray("SPECIALCODELIST")]
        [XmlArrayItem("SPECIALCODE")]
        public List<CODE> SPECIALCODELIST = new List<CODE>();
    }
}
