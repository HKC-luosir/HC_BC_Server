using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class Equipments
    {
        public string cclinkStatus { get; set; }  //CCLink状态  ON/OFF
        public bool cydlcTransmissionStatus { get; set; }
        public string extendEvents { get; set; }
        public List<Ports> ports { get; set; }
        public List<Units> units { get; set; }
        public string currPlanId { get; set; }
        public string currPlanStatus { get; set; }
        public string targetList { get; set; }
        public string engMode { get; set; }
        public string defaultGrade { get; set; }
        public string waitTime { get; set; }
        public Array abnormalFlag { get; set; }
        public int supplyStopPassNode { get; set; }
        public Array cuttingWheels { get; set; }
        public string jobDataCheckMode { get; set; }
        public string jobDataCheckModeToBCOPI { get; set; }
        public string extendModes { get; set; }
        public string opiTemperatureSettingList { get; set; }
        public string ovenTemperatureSettingList { get; set; }
        public string maskCase { get; set; }
        public int cassetteSettingCodeCheckMode { get; set; }
        public Array interLockSignal { get; set; }
        public string inlineMergeMode { get; set; }
        public string maskStageTemp { get; set; }
        public string samplingSideStatus { get; set; }
        public string robotSafetyCheckStatus { get; set; }
        public string materialStatus { get; set; }
        public string fetchGlassProportionalRule { get; set; }
        public string tactTime { get; set; }
        public Array unloadStoreQTime { get; set; }
        public string robotOperationModeMap { get; set; }
        public string ionizerFanEnable { get; set; }
        public string conditionRun { get; set; }
        public string eqpOperationMode { get; set; }
        public string partialFullMode { get; set; }
        public string autoRecipeChangeMode { get; set; }
        public string currentRecipeId { get; set; }
        public int tftJobCount { get; set; }
        public int cfJobCount { get; set; }
        public int modProductJobCount { get; set; }
        public int productType { get; set; }
        public int productId { get; set; }
        public int productNo { get; set; }
        public string samplingRule { get; set; }
        public string samplerAtio { get; set; }
        public string samplingUnit { get; set; }
        public string sideInformation { get; set; }
        public string vcrs { get; set; }
        public string waitCassetteStatus { get; set; }
        public string materialEntranceStatus { get; set; }
        public int opCoolRunCount { get; set; }
        public int opInspectionIdleTime { get; set; }
        public int coolRunSetCount { get; set; }
        public int coolRunRemainCount { get; set; }
        public int forcecleanout { get; set; }
        public string fastRunMode { get; set; }
        public string downstreamTimeoutEqpNo { get; set; }
        public string conditionRunStatus { get; set; }
        public string transferPauseStatus { get; set; }
        public string lastGlassId { get; set; }
        public string efumode { get; set; }
        public int mplcinterlockReply { get; set; }
        public string hostEquipmentId { get; set; }
        public int dmJobCount { get; set; }
        public int cassetteQtime { get; set; }
        public Array materials { get; set; }
        public int throughDummyJobCount { get; set; }
        public int thicknessDummyJobCount { get; set; }
        public int uvMaskJobCount { get; set; }
        public string equipmentType { get; set; }
        public string groupIndex { get; set; }
        public string tcMode { get; set; }
        public string linkMode { get; set; }
        public string preStatus { get; set; }
        public string cimMode { get; set; }
        public string runMode { get; set; }
        public string equipmentName { get; set; }
        public string alive { get; set; }
        public string recipeIdCheck { get; set; }
        public string lowConcentrationKOHTankNumber { get; set; }
        public string bypassInspectionEquipmentMode { get; set; }
        public string isIonizer { get; set; }
        public string equipmentNo { get; set; }
        public string lineId { get; set; }
        public string equipmentId { get; set; }
        public string reportMode { get; set; }
        public string tactTimeControlMode { get; set; }
        public string tactTimeSetCommand { get; set; }
        public string coolerInspectMode { get; set; }
        public string tactTimeChange { get; set; }
        public string notifyPreGetMode { get; set; }
        public string skipInspectionMode { get; set; }
        public string EMSMonitorMode { get; set; }
        public string barcodeReaderMode { get; set; }
        public string forceBypassMode { get; set; }
        public string bufferInspectMode { get; set; }
        public string turnTableMode { get; set; }
        public string firstRunMode { get; set; }
        public string mmgControlMode { get; set; }
        public string upstreamInlineMode { get; set; }
        public string secsControlMode { get; set; }
        public string wasteWaterEnableMode { get; set; }
        public string coaterBufferHoldStatus { get; set; }
        public string reverseMode { get; set; }
        public string ovenBufferMode { get; set; }
        public string tcvSamplingMode { get; set; }
        public string tCUCheckMode { get; set; }
        public string transferState { get; set; }
        public string localAlarmStatus { get; set; }
        public string cstOperationMode { get; set; }
        public string downstreamInlineMode { get; set; }
        public string currentStatus { get; set; }
        public string indexerOperationMode { get; set; }
        public string recipeParamterCheck { get; set; }
        public string localWarningStatus { get; set; }
        public string curRecipeValidationEnabled { get; set; }
        public int suspectOfflineTimer { get; set; }
        public int jobCount { get; set; }
        public string alarmLevel { get; set; }
        public string stcode { get; set; }
        public string currentRecipeIdValue { get; set; }
        public string vcrEnableMode { get; set; }
        public string commandType { get; set; }
        public int loaderQTime { get; set; }
        public string currentRecipeIdCheck { get; set; }
        /// <summary>
        /// 是否是必经设备
        /// </summary>
        public string isProcessEnd { get; set; }
        /// <summary>
        /// 是否重新请帐
        /// </summary>
        public string isJobDataRequest { get; set; }
        /// <summary>
        /// VCR状态 number:status;
        /// </summary>
        public string vCRStatus { get; set; }

        //public Units Unit1 { get; set; }
        //public Units Unit2 { get; set; }
        //public Units Unit3 { get; set; }
        //public Units Unit4 { get; set; }
        //public Units Unit5 { get; set; }
        //public Ports Port1 { get; set; }
        //public Ports Port2 { get; set; }
        //public Ports Port3 { get; set; }
        //public Ports Port4 { get; set; }
        //public Ports Port5 { get; set; }
        //public Ports Port6 { get; set; }
        //public Ports Port7 { get; set; }
        //public Ports Port8 { get; set; }
        //public Ports Port9 { get; set; }

    }
}
