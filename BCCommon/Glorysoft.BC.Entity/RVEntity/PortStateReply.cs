
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PortStateReply
    {
        public PortStateReply()
        {
            PortList = new PortStateReplyPortList();
            MACHINENAME = "";
        }
        public string MACHINENAME { get; set; }

        //[XmlArray("PORTLIST")]
        //[XmlArrayItem("PORT")]
           [XmlElement("PORTLIST")]   
        public PortStateReplyPortList PortList { get; set; }

    }
  [Serializable]
    [XmlRoot("PORT")]
    public class PortStateReplyPortList
    {
        public PortStateReplyPortList()
        {
            PortList = new List<PortStateReplyPort>();
        }
        [XmlElement("PORT")]
        public List<PortStateReplyPort> PortList { get; set; }
    }
    public class PortStateReplyPort
    {
        public PortStateReplyPort()
        {
            PORTNAME ="";
            /// <summary>
            /// [ EMPTY | FULL | DOWN | UP ]
            /// </summary>
             PORTSTATENAME ="";
             PORTTYPE ="";
             PORTUSETYPE ="";
            /// <summary>
            /// [AUTO | MANUAL]
            /// </summary>
             PORTACCESSMODE ="";
             CARRIERNAME ="";
        }
        public string PORTNAME { get; set; }
        /// <summary>
        /// [ EMPTY | FULL | DOWN | UP ]
        /// </summary>
        public string PORTSTATENAME { get; set; }
        public string PORTTYPE { get; set; }
        public string PORTUSETYPE { get; set; }
        /// <summary>
        /// [AUTO | MANUAL]
        /// </summary>
        public string PORTACCESSMODE { get; set; }
        public string CARRIERNAME { get; set; }
    }
}
