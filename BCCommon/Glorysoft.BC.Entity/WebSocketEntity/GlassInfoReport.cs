using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class GlassInfoReport : BaseClass
    {
        public GlassInfoReport()
        {
            GlassList = new List<GlassInfoReportGlass>();
        }
        
        public List<GlassInfoReportGlass> GlassList { get; set; }

    }

    public class GlassInfoReportGlass
    {
        public string OutUnitName { get; set; }
        public string OutSUnitName { get; set; }
        public string LotID { get; set; }
        public string Operid { get; set; }
        public string Prodid { get; set; }
        public string LotJudge { get; set; }
        public string GLSST { get; set; }
        public int FSlotNO { get; set; }
        public int TSlotNO { get; set; }
        public string SSLOTNO { get; set; }
        public string Position { get; set; }//POSITION
        public string ProductSpecName { get; set; }
        public string ProductSpecVersion { get; set; }
        public string MaskSpecName { get; set; }//MASKSPECNAME
        public string FSlotPosition { get; set; }
        public string TSlotPosition { get; set; }
        public string GLSID { get; set; }
        public string PPID { get; set; }
        public string RGLSID { get; set; }
        public string GLSIDType { get; set; }
        public string GLSJudge { get; set; }
        public string GLSGrade { get; set; }
        public string HalfProductJudge { get; set; }//HALFPRODUCTJUDGE
        public string PairsLotNo { get; set; }
        public string PairProdID { get; set; }
        public string PairProdType { get; set; }
        public string PairGLSID { get; set; }
        public string PairRGLSID { get; set; }
        public string PairGLSJudge { get; set; }
        public string PairGLSGrade { get; set; }
        public string CrateID { get; set; }
        public string Maker { get; set; }
        public string ProductRecipe { get; set; }//PRODUCTRECIPE
        public string ReworkType { get; set; }//REWORKTYPE
        public string ReworkCount { get; set; }//REWORKCOUNT
        public string EXPOSURERECIPENAME { get; set; }//EXPOSURERECIPENAME                                                                                         
        public string GLSTHK { get; set; }
        public string GLSSize { get; set; }
        public string SmplFlag { get; set; }
        public string RwkCnt { get; set; }
        public string DumusedCNT { get; set; }
        public string MASKID { get; set; }
        public string MASKGroupName { get; set; }
        public string ProberID { get; set; }
        public string GCFlag { get; set; }
        public string GCUnit { get; set; }
        public string EvaSmplFlag { get; set; }
        public string PanelJudge { get; set; }
        public string ArrayrepairType { get; set; }
        public string LCVDRepairType { get; set; }
        public string EXPUnitID { get; set; }
        public string EXPRCPID { get; set; }
        public string ProcessUnitID { get; set; }
        public string ProcessingFlag { get; set; }
        public string ProcessingJudge { get; set; }
        public string OperVersion { get; set; }
        public string ProductionVersion { get; set; }
        public string WorkOrder { get; set; }
        public string ProcessFlow { get; set; }
        public string ProcessFlowVersion { get; set; }
        public string ProcessOperationName { get; set; }//PROCESSOPERATIONNAME
        public string ProcessOperationVersion { get; set; }//PROCESSOPERATIONVERSION
        public string ProductionType { get; set; }
        public string ProductType { get; set; }//PRODUCTTYPE
        public string InspectionInfo { get; set; }
        public string SubGLSJudge { get; set; }
        public string SubGLSGrade { get; set; }
        public string NGInputComment { get; set; }
        public string TURNDEGREE { get; set; }//TURNDEGREE
        public string SORTTURNFLAG { get; set; }
        public string SORTScrapFLAG { get; set; }
        public string FlowModeValue { get; set; }
        public string FIRSTRUNFLAG { get; set; }
        public string BeforeProcessMachine { get; set; }//BEFOREPROCESSMACHINE
        public string GlassType { get; set; }//GLASSTYPE

        public string WorkTableID { get; set; }
        public string CutGlsX { get; set; }
        public string CutGlsY { get; set; }
      
        public string UnitID { get; set; }
        public string SUnitID { get; set; }
        public string SSUnitID { get; set; }
        public string TRAYID { get; set; }
        public string TRAYPOSITIONNO { get; set; }
        public DateTime CreateDate { get; set; }
        public string FPorTID { get; set; }
        public string TPorTID { get; set; }
        public string FCSTID { get; set; }
        public string TCSTID { get; set; }
       
        public bool IsExist { get; set; }
        /// <summary>
        /// EnumGlassSlotStatus   (int)EnumGlassSlotStatus.R;
        ///Empty = 0,//empty
        ///ProcessEnd = 1,//normal process end
        ///Wait = 2,//wait
        ///Skip = 3,//skip
        ///Fail = 4,//fail
        ///Processing = 5,//processing
        ///Recovery = 6,//Recovery 
        ///Removed = 7,//Removed 
        /// </summary>
        public int SlotSatus { get; set; }
        public DateTime FetchDatetime { get; set; }
        public int FCassetteSequence { get; set; }
        public int TCassetteSequence { get; set; }
    }
}
