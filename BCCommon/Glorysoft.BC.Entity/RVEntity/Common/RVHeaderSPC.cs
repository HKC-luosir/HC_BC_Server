using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [XmlRoot("HEADER")]
    public class RVHeaderSPC
    {
        public RVHeaderSPC()
        {
            MESSAGENAME = "";
            SHOPNAME = "";
            MACHINENAME = "";
            TRANSACTIONID = "";
            ORIGINALSOURCESUBJECTNAME = "";
            SOURCESUBJECTNAME = "";
            TARGETSUBJECTNAME = "";
            EVENTUSER = "";
            EVENTCOMMENT = "";
        }
        [XmlElement("MESSAGENAME")]
        public string MESSAGENAME;
        [XmlElement("SHOPNAME")]
        public string SHOPNAME;
        [XmlElement("MACHINENAME")]
        public string MACHINENAME;
        [XmlElement("TRANSACTIONID")]
        public string TRANSACTIONID;
        [XmlElement("ORIGINALSOURCESUBJECTNAME")]
        public string ORIGINALSOURCESUBJECTNAME;
        [XmlElement("SOURCESUBJECTNAME")]
        public string SOURCESUBJECTNAME;
        [XmlElement("TARGETSUBJECTNAME")]
        public string TARGETSUBJECTNAME;
        [XmlElement("EVENTUSER")]
        public string EVENTUSER;
        [XmlElement("EVENTCOMMENT")]
        public string EVENTCOMMENT;
    }
}
