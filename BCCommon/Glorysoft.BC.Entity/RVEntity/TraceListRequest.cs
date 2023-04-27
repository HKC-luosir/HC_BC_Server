
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("BODY")]
    public class TraceListRequest
    {
        public TraceListRequest()
        {
            SVIDLIST = new TraceListRequestSVIDLIST();
        }
        public string MACHINENAME { get; set; }
        public string TRID { get; set; }
        public string DSPER { get; set; }
        public string TOTSMP { get; set; }
        public string REPGSZ { get; set; }
        //[XmlArray("ALARMLIST")]
        //[XmlArrayItem("ALARM")]
        [XmlElement("SVIDLIST")]
        public TraceListRequestSVIDLIST SVIDLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("SVID")]
    public class TraceListRequestSVIDLIST
    {
        public TraceListRequestSVIDLIST()
        {
            SVIDLIST = new List<TraceListRequestSVID>();
        }
        [XmlElement("SVID")]
        public List<TraceListRequestSVID> SVIDLIST { get; set; }
    }
    public class TraceListRequestSVID
    {
        public string SVID { get; set; }      
    }
}
