using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class CuttingCompleteReportFDC
    {
        public CuttingCompleteReportFDC()
        {
            SUBPRODUCTLIST = new CuttingCompleteReportFDCSUBPRODUCTLIST();
             MACHINENAME ="";
             UNITNAME ="";
             LOTNAME ="";
             PRODUCTNAME ="";
             PRODUCTSPECNAME ="";
             PRODUCTJUDGE ="";
             PROCESSOPERATIONNAME ="";
             MACHINERECIPENAME ="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string LOTNAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTJUDGE { get; set; }
        public string PROCESSOPERATIONNAME { get; set; }
        public string MACHINERECIPENAME { get; set; }
        [XmlElement("SUBPRODUCTLIST")]
        public CuttingCompleteReportFDCSUBPRODUCTLIST SUBPRODUCTLIST { get; set; }
    }
    [Serializable]
    [XmlRoot("SUBPRODUCT")]
    public class CuttingCompleteReportFDCSUBPRODUCTLIST
    {
        public CuttingCompleteReportFDCSUBPRODUCTLIST()
        {
            SUBPRODUCTLIST = new List<CuttingCompleteReportFDCSUBPRODUCT>();
        }
        [XmlElement("SUBPRODUCT")]
        public List<CuttingCompleteReportFDCSUBPRODUCT> SUBPRODUCTLIST { get; set; }
    }
    public class CuttingCompleteReportFDCSUBPRODUCT
    {
        public CuttingCompleteReportFDCSUBPRODUCT()
        {
            SUBPRODUCTNAME = "";
        }
        public string SUBPRODUCTNAME { get; set; }
       
    }
}
