using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class RepairInfo
    {
        public int Action { get; set; }
        public string CallOperatorID { get; set; }
        public string CallTime { get; set; }
        public string RepairOperatorID { get; set; }
        public string RepairTime { get; set; }
        public string EndOperatorID { get; set; }
        public string EndTime { get; set; }
        public string APPOperatorID { get; set; }
        public string APPTime { get; set; }
    }
}
