using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCreateCarrierBatch : RVBodyBase
    {
        public RVCreateCarrierBatch()
        {
            MessageName = "LCM.CREATECARRIERBATCH";
            ISAUTO = "Y";
            DURABLELIST = new List<DURABLE>();
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTNUM { get; set; }
        public string ISAUTO { get; set; }
        public string BATCHID { get; set; }
        [XmlArray("DURABLELIST")]
        [XmlArrayItem("DURABLE")]
        public List<DURABLE> DURABLELIST = new List<DURABLE>();
    }
}
