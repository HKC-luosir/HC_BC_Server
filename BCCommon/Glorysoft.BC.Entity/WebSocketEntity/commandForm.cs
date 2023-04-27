using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class commandForm
    {
        public string unitid { get; set; }
        public string unitno { get; set; }
        public string sequenceNo { get; set; }
        public cmd1st cmd1st { get; set; }
        public cmd2st cmd2st { get; set; }
        public cmd3st cmd3st { get; set; }
        public cmd4st cmd4st { get; set; }
    }
}
