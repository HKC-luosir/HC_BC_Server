
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class DcpPlanRequestFDC
    {
        public DcpPlanRequestFDC()
        {
            MACHINENAME = "";
        }
        public string MACHINENAME { get; set; }
    }
}
