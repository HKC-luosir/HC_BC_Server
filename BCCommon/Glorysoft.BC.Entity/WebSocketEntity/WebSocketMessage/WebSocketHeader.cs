using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage
{
    // [XmlRoot("header")]
    public class WebSocketHeader
    {
        public WebSocketHeader()
        {
            messageName = "";
            transactionId = "";
            inboxName = "";
            userName = "";

        }
        // [XmlElement("messageName")]
        public string messageName;
        // [XmlElement("transactionId")]
        public string transactionId;
        // [XmlElement("userId")]
        public string userId;
        public string inboxName;
        public string uuid;
        public string userName;

    }
}
