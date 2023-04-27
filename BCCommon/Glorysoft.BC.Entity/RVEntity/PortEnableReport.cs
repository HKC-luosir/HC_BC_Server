
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PortEnableReport
    {
        public PortEnableReport()
        {
              MACHINENAME ="";
             PORTNAME ="";
             CARRIERNAME ="";

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
        }
        public string MACHINENAME { get; set; }
        public string PORTNAME { get; set; }
        public string CARRIERNAME { get; set; }

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

    }

}
