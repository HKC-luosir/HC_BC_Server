using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class CompareNGInfo
    {

        public CompareNGInfo()
        {
            // AlarmEnable = true;
            // AlarmID = "";
            ParamName = "";
            TargetValue = "";
            SoureceValue = "";
            MaxValue = "";
            ModuleID = "";
            MinValue = "";
        }

        public string ParamName { get; set; }
        public string TargetValue { get; set; }
        public string SoureceValue { get; set; }
        public string MaxValue { get; set; }
        public string MinValue { get; set; }
        public string ModuleID { get; set; }
    }
}
