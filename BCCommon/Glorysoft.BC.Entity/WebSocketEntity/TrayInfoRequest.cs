using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class TrayInfoRequest : BaseClass
    {
        public TrayInfoRequest() { }
     
        public string TRAYID { get; set; }
        public string PorTID { get; set; }
    }
}
