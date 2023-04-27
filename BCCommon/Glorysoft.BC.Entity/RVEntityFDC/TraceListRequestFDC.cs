

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class TraceListRequestFDC
    {
        public TraceListRequestFDC()
        {
            SVIDLIST = new TraceListRequestFDCSVIDLIST();
            MACHINENAME = "";
            TRID = "";
            DSPER = "";
            TOTSMP = "";
            REPGSZ = "";
        }
        public string MACHINENAME { get; set; }
        public string TRID { get; set; }
        /// <summary>
        /// 扫描频率
        /// </summary>
        public string DSPER { get; set; }
        /// <summary>
        /// 扫描次数  -1是无限扫描
        /// </summary>
        public string TOTSMP { get; set; }
        public string REPGSZ { get; set; }
        //[XmlArray("ALARMLIST")]
        //[XmlArrayItem("ALARM")]
        [XmlElement("SVIDLIST")]
        public TraceListRequestFDCSVIDLIST SVIDLIST { get; set; }

    }
    [Serializable]
    [XmlRoot("SVIDLIST")]
    public class TraceListRequestFDCSVIDLIST
    {
        public TraceListRequestFDCSVIDLIST()
        {
            SVIDLIST = new List<String>();
        }
        [XmlElement("SVID")]
        public List<string> SVIDLIST { get; set; }
    }
    //[Serializable]
    //[XmlRoot("SVID")]
    //public class TraceListRequestFDCSVID
    //{
    //    public TraceListRequestFDCSVID()
    //    {
    //        SVID = "";
    //    }
    //    public string SVID { get; set; }
    //}
}
