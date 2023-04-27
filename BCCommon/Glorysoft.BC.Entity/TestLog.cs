using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
   public class TestLog
    {
        public string FunctionName { get; set; }
        public string Time { get; set; }
        public DateTime BitBeginTime { get; set; }
        public DateTime HandlerBeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
