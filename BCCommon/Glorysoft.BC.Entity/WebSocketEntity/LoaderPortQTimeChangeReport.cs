
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class LoaderPortQTimeChangeReport : BaseClass
    {
        public LoaderPortQTimeChangeReport() { }
        public string UnitID { get; set; }
        public string PortQTime { get; set; }
    }
}
