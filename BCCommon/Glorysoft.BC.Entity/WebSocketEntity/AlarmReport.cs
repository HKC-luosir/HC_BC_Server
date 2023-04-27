
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class AlarmReport
    {
        public AlarmReport() { }
        //        UnitID
        // AlarmID
        //AlarmState
        //AlarmType
        //AlarmText
        //SubUnitNo
        public string UnitID { get; set; }
        public string AlarmID { get; set; }
        public string AlarmState { get; set; }
        public string AlarmType { get; set; }
        public string AlarmText { get; set; }
        public string SubUnitNo { get; set; }
    }
}
