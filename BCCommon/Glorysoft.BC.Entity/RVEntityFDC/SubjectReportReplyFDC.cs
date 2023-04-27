
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntityFDC
{
    [Serializable]
    [XmlRoot("Body")]
    public class SubjectReportReplyFDC
    {
        public SubjectReportReplyFDC()
        {
            MACHINENAME = "";
            SUBJECTNAME = "";
            ACKNOWLEDGE = "";
        }
        public string MACHINENAME { get; set; }
        public string SUBJECTNAME { get; set; }
        public string ACKNOWLEDGE { get; set; }

    }

}
