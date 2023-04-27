using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class SVInfo
    {
        public SVInfo()
        {
            SVID = "";
            SVName = "";
            SV = "";
        }
        public string UnitID { get; set; }
        public string SVID { get; set; }
        public string SVName { get; set; }
        public string SV { get; set; }
    }

}
