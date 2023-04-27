using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class OpCallInfo
    {
        public string EqpId { get; set; }
        //"messahe":LC->EIS ,"OpCall":EIS->LC "Receive":S10F1
        public string EqpName { get; set; }
        public string OpCallEvent { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
