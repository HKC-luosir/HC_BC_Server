using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class ExeCMD
    {
        public int objectId { get; set; }
        public string lineId { get; set; }
        public string equipmentNo { get; set; }
        public string equipmentId { get; set; }
        public string subEquipmentNo { get; set; }
        public string command { get; set; }
        public string machine { get; set; }
        public string protocol { get; set; }
        public string commandSecsName { get; set; }
        public string commandToMap { get; set; }
        public List<CMD_Para> parameters { get; set; }


    }
}
