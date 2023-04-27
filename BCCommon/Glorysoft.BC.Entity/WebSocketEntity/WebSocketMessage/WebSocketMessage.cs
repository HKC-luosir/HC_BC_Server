
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage
{
    
    public class WebSocketMessage
    {
        //Tibco 实体类
        //public WebSocketMessage()
        //{
        //    body = new WebSocketBody();
        //}
        //public WebSocketMessage()
        //{
        //    body = new List<WebSocketBody>();
        //}



        public WebSocketHeader header { get; set; }


        public Object body { get; set; }
        public Object common { get; set; }
        //public List<WebSocketBody> body { get; set; }


        public WebSocketResult result { get; set; }

       
        public string JsonSerializer()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(this);
        }

    }
}
