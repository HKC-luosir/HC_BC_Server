
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class CimMessageReport : BaseClass
    {
        public CimMessageReport() { }
        //        UnitID
        // CIMMessage
        //CIMMessageID
        //TouchPanelNo
        public string UnitID { get; set; }
        public string CIMMessage { get; set; }
        public string CIMMessageID { get; set; }
        public string TouchPanelNo { get; set; }
    }
}
