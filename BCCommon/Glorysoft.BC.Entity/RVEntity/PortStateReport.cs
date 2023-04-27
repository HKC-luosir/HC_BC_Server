
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PortStateReport
    {
        public PortStateReport()
        {
            PortList = new PortStateReportPortList();
            MACHINENAME = "";
        }
        public string MACHINENAME { get; set; }        
        //[XmlArray("PORTLIST")]
        //[XmlArrayItem("PORT")]
           [XmlElement("PORTLIST")]   
        public PortStateReportPortList PortList { get; set; }

    }
  [Serializable]
    [XmlRoot("PORT")]
    public class PortStateReportPortList
    {
        public PortStateReportPortList()
        {
            PortList = new List<PortStateReportPort>();
        }
        [XmlElement("PORT")]
        public List<PortStateReportPort> PortList { get; set; }
    }
    public class PortStateReportPort
    {
        public PortStateReportPort()
        {
             PORTNAME ="";
            /// <summary>
            /// [EMPTY | FULL | DOWN]
            /// </summary>
             PORTSTATENAME ="";
            /// <summary>
            /// [PB | PL | PU | PS ]
            /// </summary>
             PORTTYPE ="";
            /// <summary>
            /// [OO | DM | GG | NG | RW | RP | SC | CR | CL | RL]
            /// </summary>
             PORTUSETYPE ="";
            /// <summary>
            /// [AUTO | MANUAL]
            /// </summary>
             PORTACCESSMODE ="";
             CARRIERNAME ="";
        }
        public string PORTNAME { get; set; }
        /// <summary>
        /// [EMPTY | FULL | DOWN]
        /// </summary>
        public string PORTSTATENAME { get; set; }
        /// <summary>
        /// [PB | PL | PU | PS ]
        /// </summary>
        public string PORTTYPE { get; set; }
        /// <summary>
        /// [OO | DM | GG | NG | RW | RP | SC | CR | CL | RL]
        /// </summary>
        public string PORTUSETYPE { get; set; }
        /// <summary>
        /// [AUTO | MANUAL]
        /// </summary>
        public string PORTACCESSMODE { get; set; }
        public string CARRIERNAME { get; set; }
    }
}
