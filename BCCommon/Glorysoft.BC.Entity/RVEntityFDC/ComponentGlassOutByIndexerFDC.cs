
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ComponentGlassOutByIndexerFDC
    {
        public ComponentGlassOutByIndexerFDC()
        {
              MACHINENAME ="";
              UNITNAME ="";
              SUBUNITNAME ="";
              SUBSUBUNITNAME ="";
              LOTNAME ="";
              PRODUCTNAME ="";
              VCRPRODUCTNAME ="";
              PRODUCTGRADE ="";
              PRODUCTJUDGE ="";
              FROMSLOTID ="";
              TOSLOTID ="";
              PORTNAME ="";
              CARRIERNAME ="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string SUBUNITNAME { get; set; }
        public string SUBSUBUNITNAME { get; set; }
        public string LOTNAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public string VCRPRODUCTNAME { get; set; }
        public string PRODUCTGRADE { get; set; }
        public string PRODUCTJUDGE { get; set; }
        public string FROMSLOTID { get; set; }
        public string TOSLOTID { get; set; }
        public string PORTNAME { get; set; }
        public string CARRIERNAME { get; set; }
    }
}
