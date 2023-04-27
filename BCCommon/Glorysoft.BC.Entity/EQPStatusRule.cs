using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class EQPStatusRule
    {
          //eqpid character varying(50),
          //eqpstatus character varying(50),
          //unitid character varying(50),
          //checkenable boolean
        public string EQPID { get; set; }
        /// <summary>
        /// 1	PM	
        ///2	BM	
        ///3	Pause	
        ///4	Idle	
        ///5	Run
        /// </summary>
        public string EQPStatus { get; set; }
        public string UnitIDList { get; set; }
        public bool CheckEnable { get; set; }
        //<result property = "EQPID" column="eqpid"/>
        //<result property = "EQPStatus" column="eqpstatus"/>
        //<result property = "UnitID" column="unitid"/>
        //<result property = "CheckEnable" column="checkenable"/>
    }
}
