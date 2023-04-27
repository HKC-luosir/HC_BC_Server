using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class UnitTraceListInfo
    {
       // public DateTime CreateTime { get; set; }
        public string UnitID { get; set; }
        public string TRID { get; set; }
        public List<string> SVIDList { get; set; }
       // public string DateTime { get; set; }
        //public List<Parameter> ParameterList { get; set; }
        /// <summary>
        /// 扫描频率
        /// </summary>
        public int DSPER { get; set; }
        /// <summary>
        /// 扫描次数  -1是无限扫描
        /// </summary>
        public int TOTSMP { get; set; }

        /// <summary>
        /// Consts.CommandType.CCLink.GetHashCode()//1:CCLink
        /// Consts.CommandType.HSMS.GetHashCode()//2:HSMS
        /// </summary>
        public int CommandType { get; set; }
        public bool HsmsSendTraceData { get; set; }
    }
}
