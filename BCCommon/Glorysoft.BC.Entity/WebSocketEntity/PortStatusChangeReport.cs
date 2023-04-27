
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class PortStatusChangeReport : BaseClass
    {
        public PortStatusChangeReport() { }
//        UnitID
//PortID
        public string UnitID { get; set; }
        public string PortID { get; set; }
        //PortStatus
        //CassetteStatus
        //CassetteSequenceNo
        //CassetteID
        //CompletedCassetteData
        public string PortStatus { get; set; }
        public string CassetteStatus { get; set; }
        public string CassetteSequenceNo { get; set; }
        public string CassetteID { get; set; }
        public string CompletedCassetteData { get; set; }

    }
}
