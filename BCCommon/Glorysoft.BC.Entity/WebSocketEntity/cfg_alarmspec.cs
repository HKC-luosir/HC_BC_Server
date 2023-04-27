using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_alarmspec
    {
        public string eqpid { get; set; }
        public string alarmtext { get; set; }
        public string unitid { get; set; }
        public string alarmid { get; set; }
        public DateTime createdate { get; set; }
        public string unitno { get; set; }
        public string alarmtype { get; set; }
    }
}
