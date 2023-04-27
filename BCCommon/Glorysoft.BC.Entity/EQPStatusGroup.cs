using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Glorysoft.BC.Entity
{
   public class EQPStatusGroup
    {
        //eqpid character varying(50),
        //eqpstatus character varying(50),
        //index integer.
        public string EQPID { get; set; }
        /// <summary>
        /// 1	PM	
        ///2	BM	
        ///3	Pause	
        ///4	Idle	
        ///5	Run
        /// </summary>
        public string EQPStatus { get; set; }
        public int Index { get; set; }
         //<result property = "EQPID" column="eqpid"/>
         //<result property = "EQPStatus" column="eqpstatus"/>
         //<result property = "Index" column="index"/>
    }
}
