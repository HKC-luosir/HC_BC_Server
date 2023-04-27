using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class ProcessEndGlassInfo
    {
        public ProcessEndGlassInfo()
        {
            listData = new List<wip_processend_glass>();
        }
        public List<wip_processend_glass> listData { get; set; }
        public string needDelData { get; set; }
    }
}
