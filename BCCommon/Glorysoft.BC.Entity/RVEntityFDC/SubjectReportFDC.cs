using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class SubjectReportFDC
    {
        public SubjectReportFDC()
        {
            MACHINENAME = "";
            SUBJECTNAME = "";
        }
        public string MACHINENAME { get; set; }
        public string SUBJECTNAME { get; set; }


    }
}
