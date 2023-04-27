using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class CellInfo
    {
        public CellInfo()
        {
            GlassID = "";
            CellID = "";
            CellJudge="";
            ReasonCode = "";
        }
        public string GlassID { get; set; }
        public string CellID { get; set; }
        public string CellJudge { get; set; }
        public string ReasonCode { get; set; }
    }
}
