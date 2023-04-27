using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_operationmode
    {
        public string eqpid { get; set; }
        public int equipmentvalue { get; set; }
        public string operationmodename { get; set; }
        public DateTime updatetime { get; set; }
        public string updateuser { get; set; }
    }
}
