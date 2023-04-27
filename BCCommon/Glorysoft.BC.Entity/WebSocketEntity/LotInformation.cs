using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   public class LotInformation : BaseClass
    {
        public string UnitID { get; set; }
        public string PortID { get; set; }
        public string CassetteID { get; set; }
        public string GlassExistence { get; set; }
        public string GlassCount { get; set; }
        public string CSTSeqNo { get; set; }
        public string InspectionFlag { get; set; }
        public string SkipFlag { get; set; }
        public List<GlassInfo> GlassInfos { get; set; }
    }
}
