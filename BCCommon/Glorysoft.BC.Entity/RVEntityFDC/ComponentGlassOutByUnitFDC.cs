
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ComponentGlassOutByUnitFDC
    {
        public ComponentGlassOutByUnitFDC()
        {
              MACHINENAME ="";
             UNITNAME ="";
             SUBUNITNAME ="";
             SUBSUBUNITNAME ="";
             LOTNAME ="";
             PRODUCTNAME ="";
             VCRPRODUCTNAME ="";
             PRODUCTRECIPE ="";
             PRODUCTSPECNAME ="";
             PRODUCTGRADE ="";
             PRODUCTJUDGE ="";
             FROMSLOTID ="";
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string SUBUNITNAME { get; set; }
        public string SUBSUBUNITNAME { get; set; }
        public string LOTNAME { get; set; }        
        public string PRODUCTNAME { get; set; }
        public string VCRPRODUCTNAME { get; set; }
        public string PRODUCTRECIPE { get; set; }
        public string PRODUCTSPECNAME { get; set; }
        public string PRODUCTGRADE { get; set; }
        public string PRODUCTJUDGE { get; set; }
        public string FROMSLOTID { get; set; }
    
    }
}
