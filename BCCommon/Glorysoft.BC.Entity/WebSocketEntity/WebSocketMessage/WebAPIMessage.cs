using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage
{

    public class WebAPIMessage
    {
        //Tibco 实体类
        public WebAPIMessage()
        {
           // body = new dynamic();
        }



        public WebSocketHeader header { get; set; }


        public dynamic body { get; set; }
        public dynamic common { get; set; }

        public WebSocketResult result { get; set; }


        public string JsonSerializer()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(this);
        }

    }
}
