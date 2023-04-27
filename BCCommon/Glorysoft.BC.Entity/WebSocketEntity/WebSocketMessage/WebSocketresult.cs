
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage
{
    //[Serializable]
    //[XmlRoot("result")]
    public class WebSocketResult
    {
        public WebSocketResult()
        {
            returnCode = "0";
            returnMessageEN = "Success";
            returnMessageCH = "";
        }
        //[XmlElement("code")]
        public string returnCode { get; set; }
        // [XmlElement("messageCH")]
        public string returnMessageEN { get; set; }
        // [XmlElement("messageEN")]
        public string returnMessageCH { get; set; }
    }
}
