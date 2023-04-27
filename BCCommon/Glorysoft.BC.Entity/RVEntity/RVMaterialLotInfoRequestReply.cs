using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVMaterialLotInfoRequestReply : RVBodyBase
    {
        public RVMaterialLotInfoRequestReply()
        {
            MessageName = "";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        [XmlArray("MLOTLIST")]
        [XmlArrayItem("MLOT")]
        public List<RVMaterialList> MLOTLIST = new List<RVMaterialList>();
    }
}
