using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCheckLotBindingRequest : RVBodyBase
    {
        public RVCheckLotBindingRequest()
        {
            MessageName = "HKC.CHECKLOTBINDING";
        }
        public string LOTID { get; set; }
        public string BLUID { get; set; }
    }
}
