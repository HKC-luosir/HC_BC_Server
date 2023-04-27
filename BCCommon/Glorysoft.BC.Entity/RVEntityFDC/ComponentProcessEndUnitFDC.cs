using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class ComponentProcessEndUnitFDC
    {
        public ComponentProcessEndUnitFDC()
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
    }
}
