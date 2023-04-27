using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class TraceDataFDC
    {
        public TraceDataFDC()
        {
            SVIDLIST = new TraceDataFDCSVIDLIST();
               MACHINENAME ="";
              TRID ="";
              SMPLN ="";
              STIME ="";
        }
        public string MACHINENAME { get; set; }
        public string TRID { get; set; }
        public string SMPLN { get; set; }
        public string STIME { get; set; }
      
        [XmlElement("SVIDLIST")]
        public TraceDataFDCSVIDLIST SVIDLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("SVDATA")]
    public class TraceDataFDCSVIDLIST
    {
        public TraceDataFDCSVIDLIST()
        {
            SVIDLIST = new List<TraceDataFDCSVDATA>();
        }
        [XmlElement("SVDATA")]
        public List<TraceDataFDCSVDATA> SVIDLIST { get; set; }
    }
    public class TraceDataFDCSVDATA
    {
        public TraceDataFDCSVDATA()
        {
            SVID = "";
            VALUE = "";
        }
        public string SVID { get; set; }
        public string VALUE { get; set; }
    }
}
