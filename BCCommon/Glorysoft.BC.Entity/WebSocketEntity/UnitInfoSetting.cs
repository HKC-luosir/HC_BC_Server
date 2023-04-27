
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class UnitInfoSetting : BaseClass
    {
        public UnitInfoSetting() { }
//        UnitID
// UnitMode
//VCREnableMode
//IndexerOperationMode
//CassetteOperationMode
//portqtime
        public string UnitID { get; set; }
        public string UnitMode { get; set; }
        public string VCREnableMode { get; set; }
        public string IndexerOperationMode { get; set; }
        public string CassetteOperationMode { get; set; }
        public string portqtime { get; set; }
    }
}
