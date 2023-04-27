using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class CancellationObject
    {
        public object MessageData { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
        public string MessageName { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
    }
}
