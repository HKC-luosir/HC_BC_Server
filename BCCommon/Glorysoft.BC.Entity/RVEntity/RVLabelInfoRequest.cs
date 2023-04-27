using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLabelInfoRequest : RVBodyBase
    {
        public RVLabelInfoRequest()
        {
            MessageName = "M2.LABELREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string LOTID { get; set; }
        public string LABELTYPE { get; set; }
    }
}
