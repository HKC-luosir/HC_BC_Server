
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class TraceDataIncludeGroupListFDC
    {
        public TraceDataIncludeGroupListFDC()
        {
            GROUPLIST = new List<TraceDataIncludeGroupListFDCGROUPLIST>();
               MACHINENAME ="";
              TRID ="";
              SMPLN ="";
              STIME ="";
        }
        public string MACHINENAME { get; set; }
        public string TRID { get; set; }
        public string SMPLN { get; set; }
        public string STIME { get; set; }

        [XmlElement("GROUPLIST")]
        public List<TraceDataIncludeGroupListFDCGROUPLIST> GROUPLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("SVIDLIST")]
    public class TraceDataIncludeGroupListFDCGROUPLIST
    {
        public TraceDataIncludeGroupListFDCGROUPLIST()
        {
            SVIDLIST = new List<TraceDataIncludeGroupListFDCSVIDLIST>();
        }
        [XmlElement("SVIDLIST")]
        public List<TraceDataIncludeGroupListFDCSVIDLIST> SVIDLIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SVDATA")]
    public class TraceDataIncludeGroupListFDCSVIDLIST
    {
        public TraceDataIncludeGroupListFDCSVIDLIST()
        {
            SVIDLIST = new List<TraceDataIncludeGroupListFDCSVDATA>();
        }
        [XmlElement("SVDATA")]
        public List<TraceDataIncludeGroupListFDCSVDATA> SVIDLIST { get; set; }
    }
    public class TraceDataIncludeGroupListFDCSVDATA
    {
        public TraceDataIncludeGroupListFDCSVDATA()
        {
            SVID = "";
            VALUE = "";
        }
        public string SVID { get; set; }
        public string VALUE { get; set; }
    }
}
