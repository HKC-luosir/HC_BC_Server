using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_carrier
    {
        public string lineid { get; set; }
        public string eqpid { get; set; }
        public string portid { get; set; }
        public string carrierid { get; set; }
        public int carriertype { get; set; }
        public string mescarriertype { get; set; }
        public string slotmap { get; set; }
        public string inputproductmap { get; set; }
        public string lotid { get; set; }
        public string lotgrade { get; set; }
        public string productspecid { get; set; }
        public string productiontype { get; set; }
        public string operationid { get; set; }
        public DateTime createdate { get; set; }
    }
}
