using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVEmptyPalletReturnRequest : RVBodyBase
    {
        public RVEmptyPalletReturnRequest()
        {
            MessageName = "M2.EMPTYPALLETRETURNREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string PALLETID { get; set; }
        public string PARTNAME { get; set; }
        public string QTY { get; set; }
    }
}
