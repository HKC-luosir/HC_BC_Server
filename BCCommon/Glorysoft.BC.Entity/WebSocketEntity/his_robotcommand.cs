using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_robotcommand
    {
        public int sequenceno { get; set; }
        public string strcmd1 { get; set; }
        public string starmno1 { get; set; }
        public int stgetposition1 { get; set; }
        public int stputposition1 { get; set; }
        public int stgetslotno1 { get; set; }
        public int stputslotno1 { get; set; }
        public string stsubcommand1 { get; set; }
        public int stgetslotpostion1 { get; set; }
        public int stputslotpostion1 { get; set; }
        public string ndrcmd2 { get; set; }
        public string ndarmno2 { get; set; }
        public int ndgetposition2 { get; set; }
        public int ndputposition2 { get; set; }
        public int ndgetslotno2 { get; set; }
        public int ndputslotno2 { get; set; }
        public string ndsubcommand2 { get; set; }
        public int ndgetslotpostion2 { get; set; }
        public int ndputslotpostion2 { get; set; }
        public int commandresult1 { get; set; }
        public int commandresult2 { get; set; }
        public int commandresult3 { get; set; }
        public int commandresult4 { get; set; }
        public int currentposition { get; set; }
        public string functionname { get; set; }
        public DateTime createdate { get; set; }
        public DateTime startcreatedate { get; set; }
        public DateTime endcreatedate { get; set; }
    }
}
