using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVMaterialState : RVBodyBase
    {
        public RVMaterialState()
        {
            MessageName = "MES.MATERIALSTATEREPORT";
            MLOTLIST = new List<RVMaterialList>();
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PORTID { get; set; }
        /// <summary>
        /// Mount/Unmount/Prepare/Inuse
        /// </summary>
        public string STATE { get; set; }
        public string DURABLEID { get; set; }
        public string OPERATOR { get; set; }
        [XmlArray("MLOTLIST")]
        [XmlArrayItem("MLOT")]
        public List<RVMaterialList> MLOTLIST { get; set; }
    }
    [Serializable]
    public class RVMaterialList
    {
        public string MLOTID { get; set; }
        public string MATERIALNAME { get; set; }
        public string POSITION { get; set; }
        public string MATERIALTYPE { get; set; }
        public string MAINQTY { get; set; }
        public string ACTIONCODE { get; set; }
        public string MAXQTIME { get; set; }
        public string MATERIALSN { get; set; }
        public string SUBUNITID { get; set; }
        public string USEQTY { get; set; }
    }
}
