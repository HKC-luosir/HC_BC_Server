using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_alarm
    {
        public string eqpid { get; set; }
        public string alarmtext { get; set; }
        public int alarmenable { get; set; }
        public string unitid { get; set; }
        public string sunitid { get; set; }
        public string alarmcode { get; set; }
        public string alarmlevel { get; set; }
        public string alarmtype { get; set; }
        public string alarmid { get; set; }
    }
}
