using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class PortWIPDataInfo
    {
        public int CassetteSequenceNo { get; set; }
        public int SlotSequenceNo { get; set; }
        /// <summary>
        /// 0:Waitting Process;
        /// 1:Process Completed;
        /// 2:Processing
        /// </summary>
        public int SlotProcessState { get; set; }
    }
}
