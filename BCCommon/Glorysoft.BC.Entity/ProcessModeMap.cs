using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
   public  class ProcessModeMap
    {
        //      <result property = "ProcessMode" column ="processmode"/>
        //<result property = "MachineRecipeName" column ="machinerecipename"/>
        //<result property = "CreateDate" column ="createdate"/>
        public string ModePath { get; set; }
        public string MachineRecipeName { get; set; }
        public DateTime CreateDate { get; set; }
        public string EQPID { get; set; }
        public bool HasCVD { get; set; }
        public string Remark { get; set; }
    }
}
