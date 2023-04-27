using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVBatchLoadComplete : RVBodyBase
    {
        public RVBatchLoadComplete()
        {
            MessageName = "LCM.BATCHLOADCOMPLETE";
            DURABLELIST = new List<DURABLE>();
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTNUM { get; set; }
        [XmlArray("DURABLELIST")]
        [XmlArrayItem("DURABLE")]
        public List<DURABLE> DURABLELIST = new List<DURABLE>();
    }
    public class DURABLE
    {
        public string SEQNO { get; set; }
        public string DURABLEID { get; set; }
    }
}
