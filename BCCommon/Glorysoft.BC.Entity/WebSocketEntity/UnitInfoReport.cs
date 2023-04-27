
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class UnitInfoReport : BaseClass
    {
        public UnitInfoReport()
        {
            UnitList = new List<UnitInfoReportUnit>();
        }
        public List<UnitInfoReportUnit> UnitList { get; set; }

    }
    public class UnitInfoReportUnit
    {
        public string EQPID { get; set; }        
        public int UnitPathNo { get; set; }
        public int UnitType { get; set; }
        public int UnitCapacity { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }        
        public string CRST { get; set; }
        public string UnitStatus { get; set; }      
        public string UnitSTCode { get; set; }       
        public string ReasonCode { get; set; }
        public bool HasSUnit { get; set; }
        public string IsConnect { get; set; }      
        public string RTCode { get; set; }      
        public int UnitMode { get; set; }
        public int VCREnableMode { get; set; }
        public int VCRReadFailOperationMode { get; set; }        
        public int IndexerOperationMode { get; set; }       
        public int CassetteOperationMode { get; set; }       
        public int PortQTime { get; set; }
        
    }
}
