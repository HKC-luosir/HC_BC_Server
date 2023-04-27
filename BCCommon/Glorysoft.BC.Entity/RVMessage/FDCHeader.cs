using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    [XmlRoot("Header")]
    public class FDCHeader
    {
        public FDCHeader()
        {

            MESSAGENAME = "";
            MACHINENAME = "";
            //SHOPNAME = "";

            TRANSACTIONID = "";
            ORIGINALSOURCESUBJECTNAME = "";
            //SOURCESUBJECTNAME = "";
            //TARGETSUBJECTNAME = "";
            EVENTUSER = "";
            EVENTCOMMENT = "";
        }
        [XmlElement("MESSAGENAME")]
        public string MESSAGENAME;
        [XmlElement("MACHINENAME")]
        public string MACHINENAME;
        //[XmlElement("SHOPNAME")]
        //public string SHOPNAME;


        [XmlElement("TRANSACTIONID")]
        public string TRANSACTIONID;
        [XmlElement("ORIGINALSOURCESUBJECTNAME")]
        public string ORIGINALSOURCESUBJECTNAME;
        //[XmlElement("SOURCESUBJECTNAME")]
        //public string SOURCESUBJECTNAME;
        //[XmlElement("TARGETSUBJECTNAME")]
        //public string TARGETSUBJECTNAME;
        [XmlElement("EVENTUSER")]
        public string EVENTUSER;
        //[XmlElement("MACHINENAME")]
        //public string MACHINENAME;
        //[XmlElement("REPLYSUBJECTNAME")]
        //public string REPLYSUBJECTNAME { get; set; }
        [XmlElement("EVENTCOMMENT")]
        public string EVENTCOMMENT;

    }
}
