using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class MIXRunInputRatio
    {
        public string EQPID { get; set; }
        /// <summary>
        /// 投入比例
        /// </summary>
        public int InputRatio { get; set; }
        public string MachineRecipeName { get; set; }    
        public int InputRatioID { get; set; }
        
    }
}
