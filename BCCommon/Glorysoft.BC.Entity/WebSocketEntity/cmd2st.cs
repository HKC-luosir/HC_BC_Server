using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cmd2st
    {
        public string rcmd { get; set; }
        public string armNo { get; set; }
        public string getPosition { get; set; }
        public string putPosition { get; set; }
        public string getSlotNo { get; set; }
        public string putSlotNo { get; set; }
        public string subcommand { get; set; }
        public string getSlotPosition { get; set; }
        public string putSlotPosition { get; set; }
    }
}
