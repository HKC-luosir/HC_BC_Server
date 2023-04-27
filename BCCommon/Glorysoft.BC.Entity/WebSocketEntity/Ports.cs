using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class Ports
    {
        public int productNo { get; set; }
        public string cassetteId { get; set; }
        public int cassetteSeq { get; set; }
        public string cassetteStatus { get; set; }
        public string portStatus { get; set; }
        public string portNo { get; set; }
        public string portId { get; set; }
        public string porttype { get; set; }
        public string portDown { get; set; }
        public string equipmentNo { get; set; }
        public string unitId { get; set; }
        public string unitName { get; set; }
        public string cstid { get; set; }
        public string portMode { get; set; }
        public string portTypeAutoChg { get; set; }
        public string transferMode { get; set; }
        public string portPauseMode { get; set; }
        public string portOperationMode { get; set; }
        public string portCSTType { get; set; }
        public string partialFullFlag { get; set; }
        public string glassExistence { get; set; }
        public int jobCountIncassette { get; set; }
        public int completedCassetteData { get; set; }
        public int capacity { get; set; }
        public int portQTime { get; set; }
        public string portGrade { get; set; }
        public string portEnableMode { get; set; }
        public DateTime updateDate { get; set; }
    }
}
