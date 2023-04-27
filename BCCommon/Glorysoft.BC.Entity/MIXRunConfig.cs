using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class MIXRunConfig
    {
        public int ID { get; set; }
        public string EQPID { get; set; }
        public string MachineRecipeName { get; set; }
        public bool Exist { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public int InputCount { get; set; }
        /// <summary>
        /// 当前ratio id
        /// </summary>
        public int CurrenRatioID { get; set; }
        public string Type { get; set; }
        public List<MIXRunInputRatio> MIXRunInputRatioList { get; set; }
    }
}
