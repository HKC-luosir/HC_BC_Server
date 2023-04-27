using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVMaterialLotInfoRequest : RVBodyBase
    {
        public RVMaterialLotInfoRequest()
        {
            MessageName = "MES.MATERIALLOTINFOREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PORTID { get; set; }
        public string MATERIALTYPE { get; set; }
        public string DURABLEID { get; set; }
        public string MLOTID { get; set; }
        public string OPERATOR { get; set; }
    }
}
