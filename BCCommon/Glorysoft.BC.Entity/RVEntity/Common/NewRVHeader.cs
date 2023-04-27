using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [XmlRoot("Header")]
    public class NewRVHeader
    {
        public NewRVHeader()
        {
            MESSAGENAME = "";
            TRANSACTIONID = "";
            ORGRRN = "100";
            ORGNAME = "HC";
            USERNAME = "bc";
            LANGUAGE = "EN";
            RESULTCODE = "";
            RESULTMESSAGE = "";
        }
        [XmlElement("MESSAGENAME")]
        public string MESSAGENAME;
        [XmlElement("TRANSACTIONID")]
        public string TRANSACTIONID;
        [XmlElement("ORGRRN")]
        public string ORGRRN;
        [XmlElement("ORGNAME")]
        public string ORGNAME;
        [XmlElement("USERNAME")]
        public string USERNAME;
        [XmlElement("LANGUAGE")]
        public string LANGUAGE;
        [XmlElement("RESULT")]
        public string RESULT;
        [XmlElement("RESULTCODE")]
        public string RESULTCODE;
        [XmlElement("RESULTMESSAGE")]
        public string RESULTMESSAGE;
    }
 }
