using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class JobDataForCut
    {
        public string GlassID { get; set; }
        public string ReadGlassID { get; set; }
        public string CellID { get; set; }
        public string LotID { get; set; }
        public string PPID { get; set; }
        public string SoucePlace { get; set; }
        public string TargetPlace { get; set; }
        public string HalfCutterRecipeID { get; set; }
        public string HalfGrinderRecipeID { get; set; }
        public string QuarterCutterRecipeID { get; set; }
        public string QuarterGrinderRecipeID { get; set; }
        public string UnpackGrinderRecipeID { get; set; }
        public string HalfGrinderInspectionJudge { get; set; }
        public string UPKGrinderInspectionJudge { get; set; }

    }
}
