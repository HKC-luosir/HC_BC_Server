
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    [XmlRoot("Header")]
    public class Header
    {
        public Header()
        {
            TransactionId = "";
            SourceSubject = "";
            TargetSubject = "";
        }
        [XmlElement("TransactionId")]
        public string TransactionId;
        [XmlElement("SourceSubject")]
        public string SourceSubject;
        [XmlElement("TargetSubject")]
        public string TargetSubject;
        [XmlElement("ReplySubject")]
        public string ReplySubject;
    }
}
