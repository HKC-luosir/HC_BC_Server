using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class CasInfo
    {
        public string type { get; set; }
        public string unitid { get; set; }
        public string portid { get; set; }
        public string glassExistence { get; set; }
        public string glassCount { get; set; }

        public string modepath { get; set; }

        public string cassetteid { get; set; }

        public List<GlassInfo> allRows { get; set; }
        public List<GlassInfo> checkRows { get; set; }
    }
}
