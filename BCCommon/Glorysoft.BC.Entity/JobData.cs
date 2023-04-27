using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{

    public class JobData
    {

        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string PortID { get; set; }
        public int LotJudge { get; set; }
        public int LotSortingType { get; set; }
        public string GlassJudge { get; set; }

        public string ProductID { get; set; }
        public string OperationID { get; set; }
        public string LOTID { get; set; }
        public string PPID1 { get; set; }
        public string PPID2 { get; set; }

        public string PPID3 { get; set; }
        public string PPID4 { get; set; }
        public string PPID5 { get; set; }
        public string PPID6 { get; set; }
        public string PPID7 { get; set; }

        public string PPID8 { get; set; }
        public string PPID9 { get; set; }
        public string PPID10 { get; set; }
        public string PPID11 { get; set; }
        public string PPID12 { get; set; }
        public string PPID13 { get; set; }
        public string PPID14 { get; set; }
        public string PPID15 { get; set; }
        public string PPID16 { get; set; }
        public string PPID17 { get; set; }
        public string PPID18 { get; set; }
        public string PPID19 { get; set; }
        public string PPID20 { get; set; }
        public string GlassID { get; set; }
        public int CassetteSequenceNo { get; set; }

        public int SlotSequenceNo { get; set; }
        public string SlotPosition { get; set; }
        public int CuttingSequenceNo { get; set; }
        public string GlassJudgeCode { get; set; }  //适用于GlassJudge
        public string GlassGradeCode { get; set; }
        public int GlassSortType { get; set; }
        public string SampleFlag { get; set; }
        public int ReworkCount { get; set; }
        public int PairGlassID { get; set; }
        public int Thickness { get; set; }
        public int LastGlassFlag { get; set; }
        public int GlassAngle { get; set; }
        public int JobRecoveryFlag { get; set; }
        public int ESFLAG { get; set; }

        public string ProcessingFlag { get; set; }
        public int GlassSizeCode { get; set; }
        public int GlassThicknessCode { get; set; } 
        public int GlassType { get; set; }
        public int LOTCode { get; set; }
      
        public int ProcessingCount { get; set; }
        public string InspectionFlag { get; set; }
        public string SkipFlag { get; set; }
        public int InlineEQData { get; set; }
        public string WorkOrder { get; set; }
      

    }
}
