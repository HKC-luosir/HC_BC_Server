using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class CimMsg
    {
        public string lineId { get; set; }
        public ArrayList equipmentNo { get; set; }
        public int touchNo { get; set; }
        public string message { get; set; }
        public bool action { get; set; }
        public string protocol { get; set; }
        public int cimmsgId { get; set; }
    }
}
