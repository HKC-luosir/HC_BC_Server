using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class CassetteOnPortTime : BaseClass
    {
        public CassetteOnPortTime() { }
        /// <summary>
        ///  1 ok  
        /// </summary>
        public string ReturnCode { get; set; }
        public string UnitID { get; set; }
        public string Message { get; set; }


    }
}
