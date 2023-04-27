
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class TraceListReplyFDC
    {
        public TraceListReplyFDC()
        {
            MACHINENAME = "";
            TIAACK = "";
            TOTSMP = "";
        }
        public string MACHINENAME { get; set; }
        public string TIAACK { get; set; }
        public string TOTSMP { get; set; }
    }
}
