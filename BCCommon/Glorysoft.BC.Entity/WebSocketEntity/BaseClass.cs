using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.Entity.WebSocketEntity
{
  
    public abstract class BaseClass : WebSocketBody
    {
        public BaseClass()
        {
            EQPID = HostInfo.Current.EQPInfo.EQPID;
        }
        public string TYPE
        {
            get
            {
                return this.GetType().Name;
            }
        }
       
        public string EQPID { get; set; }
       
        public string JsonSerializer()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(this);
        }
    }
}
