using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class GlassInfo : NotifyPropertyChanged
    {
        public GlassInfo()
        {
            // CutGlassInfoList = new List<GlassInfo>();
            SlotSatus = EnumGlassSlotStatus.Wait;
        }

        public string FunctionName { get; set; }
        /// <summary>
        /// PRODUCTSPECNAME
        /// </summary>
        public string ProductID { get; set; }
        /// <summary>
        /// PROCESSOPERATIONNAME
        /// </summary>
        public string OperationID { get; set; }
        // public string RecipeID { get; set; }
        /// <summary>
        /// LOTNAME
        /// </summary>
        public string LotID { get; set; }
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
        public string PPID21 { get; set; }
        public string PPID22 { get; set; }
        public string PPID23 { get; set; }
        public string PPID24 { get; set; }
        public string PPID25 { get; set; }
        public string PPID26 { get; set; }

        public string PPID27 { get; set; }
        public string PPID28 { get; set; }
        public string PPID29 { get; set; }
        public string PPID30 { get; set; }
        public string PPPID11 { get; set; }
        public string AbnormalCodes { get; set; }
        public string DefectCodes { get; set; }
        /// <summary>
        /// PRODUCTNAME
        /// </summary>
        public string GlassID { get; set; }
        public string BLID { get; set; }
        public string JobJudge { get; set; }
        public string JobGrade { get; set; }
        public int Mode { get; set; }
        public int AOIInspectionFlag { get; set; }
        public int ReprocessCount { get; set; }
        public int ReprocessFlag { get; set; }
        public int JobRecoveryFlag { get; set; }
        public string RGlassID { get; set; }
        public int GlassDegree { get; set; }
        public string InspectionJudgeData { get; set; }
        public int JobSequenceNo { get; set; }
        public int CSTOperationMode { get; set; }
        public int SubstrateType { get; set; }
        public int CassetteSequenceNo { get; set; }
        /// <summary>
        /// （SLOTPOSITION*1000+POSITION）
        /// </summary>
        public int SlotSequenceNo { get; set; }
        /// <summary>
        /// SLOTPOSITION 前后片  1 A Glass ;  2 B Glass
        /// </summary>
        public int SlotPosition { get; set; }
        public string PropertyCode { get; set; }
        public string GlassJudge { get; set; }
        public int GlassSortType { get; set; }
        public string SampleFlag { get; set; }

        public int CuttingSequenceNo { get; set; }
        /// <summary>
        /// PRODUCTJUDGE
        /// </summary>
        public string GlassJudgeCode { get; set; }

        /// <summary>
        /// PRODUCTGRADE
        /// </summary>
        public string GlassGradeCode { get; set; }
        /// <summary>
        /// PROCESSINGFLAG
        /// </summary>
        public string ProcessingFlag { get; set; }
        /// <summary>
        /// PRODUCTSIZE
        /// </summary>
        public string GlassSizeCode { get; set; }
        /// <summary>
        /// PRODUCTTHICKNESS
        /// </summary>
        public string GlassThicknessCode { get; set; }
        /// <summary>
        /// 对应JOBDATA的SubstrateType
        /// </summary>
        public string GlassType { get; set; }
        public int LotJudge { get; set; }
        public int LotSortingType { get; set; }
        public int JobType { get; set; }
        public int SlotNumberInformation { get; set; }
        public int OvenSlotNumberInformation { get; set; }
        public int ProcessFlag { get; set; }
        public int ProcessReasonCode { get; set; }
        public int LastGlassFlag { get; set; }
        public int FirstGlassFlag { get; set; }
        public int GlassThickness { get; set; }
        public string LotCode { get; set; }
        /// <summary>
        /// 必经设备的localno 分号隔开
        /// </summary>
        public string ProcessingCount { get; set; } = "";
        public string InspectionFlag { get; set; }
        public string SkipFlag { get; set; }
        public string InLineEQData { get; set; }

        /// <summary>
        /// WORKORDER
        /// </summary>
        public string WorkOrder { get; set; }
        public string ProcessFlowName { get; set; }
        public string ProcessFlowVersion { get; set; }
        public string ProcessOperationVersion { get; set; }
        public string ProductSpecVersion { get; set; }

        public string ProductionType { get; set; }
        public string HalfProductJudge { get; set; }
        public string GlassMaker { get; set; }
        public string ProductRecipe { get; set; }
        public string ReworkType { get; set; }

        public string ReworkCount { get; set; }
        public int PairGlassID { get; set; }
        public int Thickness { get; set; }
        public int ESFLAG { get; set; }
        public int GlassAngle { get; set; }
        /// <summary>
        /// [ Y | N ]
        /// Y的需要生产、N的不需要生产
        /// </summary>
        public string SamplingFlag { get; set; }
        public string ExposureRecipeName { get; set; }
        public string Turndegree { get; set; }
        public string FlowModeValue { get; set; }

        public string EVASkipFlag { get; set; }
        public string BeforeProcessMachine { get; set; }
        /// <summary>
        /// 对应JOBDATA的JobType
        /// </summary>
        public string ProductType { get; set; }
        public string MaskSpecName { get; set; }
        public string ProcessFlowRunState { get; set; }

        /// <summary>
        /// slotno 层数
        /// </summary>
        public int Position { get; set; }

        public string ProcessingInfo { get; set; }
        /// <summary>
        /// VCRPRODUCTNAME
        /// </summary>
        public string VCRProductName { get; set; }
        public string NGInputComment { get; set; }
        /// <summary>
        /// SUBPRODUCTGRADES
        /// </summary>
        public string PanelGrade { get; set; }

        /// <summary>
        /// SUBPRODUCTJUDGES
        /// </summary>
        public string PanelJudge { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ModePath { get; set; }

        public int ModelPosition { get; set; }
        public bool IsStoreIn { get; set; }
        /// <summary>
        /// 当前的unitid
        /// </summary>
        public string CurrentUnit { get; set; }
        public string CurrentSUnit { get; set; }
        public string CurrentSSUnit { get; set; }
        public bool ProductAlarm { get; set; }
        public EnumGlassSlotStatus SlotSatus { get; set; }
        /// <summary>
        /// load port
        /// </summary>
        public string InPortID { get; set; }
        /// <summary>
        /// unload port
        /// </summary>
        public string OutPortID { get; set; }
        /// <summary>
        /// load cst
        /// </summary>
        public string InCSTID { get; set; }
        /// <summary>
        /// unload cst
        /// </summary>
        public string OutCSTID { get; set; }
        public string RecipeID { get; set; }
        /// <summary>
        /// load slotno
        /// </summary>
        public string FSlotNO { get; set; }
        /// <summary>
        /// unload slotno
        /// </summary>
        public string TSlotNO { get; set; }
        public string RGLSID { get; set; }
        public string HGLSID { get; set; }
        public string GLSJudge { get; set; }
        public string ComponentType { get; set; }

        public DateTime FetchDatetime { get; set; }
        public string PortID { get; set; }
        public string CassetteID { get; set; }
        public bool IsDisable { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreateDate { get; set; }

        public int CurrentSlotNo { get; set; }
        public EnumGlassSlotStatus SlotFlag { get; set; }

        public string CRATENAME { get; set; }

        /// <summary>
        /// 1:进过cvd
        /// 0：未进过cvd
        /// </summary>
        public int CVDFlag { get; set; }
        /// <summary>
        /// 设备收片时间
        /// </summary>
        public DateTime? RecvJobTime { get; set; }
        public DateTime? RecvUnitJobTime { get; set; }
        /// <summary>
        /// 是否已发送Mes TrackIn
        /// </summary>
        public bool IsMesTrackIn { get; set; } = false;
        public string PanelCode { get; set; }
        public string ScriberModuleType { get; set; }
        public string JobAngle { get; set; }
        public string JobFlip { get; set; }
        public string MMGCode { get; set; }
        public string PanelInchSizeX { get; set; }
        public string PanelInchSizeY { get; set; }
        public int ID { get; set; }
        public bool IsFetchOutPort { get; set; } = false;
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            string str = $"Glassid:{this.GlassID} CstSeqNo:{this.CassetteSequenceNo} SlotSeqNo:{this.SlotSequenceNo} SlotPosition:{this.SlotPosition} Position:{this.Position} SlotSatus:{this.SlotSatus} SlotFlag:{this.SlotFlag} PortID:{this.PortID} Unit:{this.CurrentUnit} SubUnit:{this.CurrentSUnit} CstID:{this.CassetteID} Grade:{this.GlassGradeCode} RGlassID:{this.RGlassID} IsFetchOutPort:{this.IsFetchOutPort}";
            return str;
        }
    }
}




