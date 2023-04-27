using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
  
    public class EQPInfoReport : BaseClass
    {
        public EQPInfoReport() { }
        public string EqpStatus { get; set; }
        public string ControlState { get; set; }
        public string ReasonCode { get; set; }
        public string EquipmentAutoMode { get; set; }        

    }
}
