using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLoadRequest : RVBodyBase
    {
        public RVLoadRequest()
        {
            MessageName = "MES.LOADREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTNUM { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTUSETYPE { get; set; }
        public string PORTACCESSMODE { get; set; }
        public string DISPATCHTYPE { get; set; }
    }
}
