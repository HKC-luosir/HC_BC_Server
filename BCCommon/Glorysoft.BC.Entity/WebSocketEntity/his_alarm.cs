using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_alarm
    {
        public string eqpid { get; set; }
        public string alarmstatus { get; set; }
        public string alarmtext { get; set; }
        public DateTime createdate { get; set; }
        public DateTime startcreatedate { get; set; }
        public DateTime endcreatedate { get; set; }
        public string unitid { get; set; }
        public string alarmtype { get; set; }
        public string alarmid { get; set; }
        public string alarmunitnumber { get; set; }
        public string alarmcode { get; set; }
    }
}
