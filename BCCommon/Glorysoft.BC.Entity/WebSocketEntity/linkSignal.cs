using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class linkSignal
    {
        public string downSignalString { get; set; }
        public string fromMachineNo { get; set; }
        public string signalName { get; set; }
        public string toMachineNo { get; set; }
        public string upSignalString { get; set; }
    }
}
