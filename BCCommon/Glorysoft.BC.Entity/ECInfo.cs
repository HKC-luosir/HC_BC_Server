using System;

namespace Glorysoft.BC.Entity
{
    public class ECInfo
    {
        public string ECID { get; set; }
        public string ECName { get; set; }
        public double? ECMax { get; set; }
        public double? ECMin { get; set; }
        public string ECV { get; set; }
        public double? ECDef { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
