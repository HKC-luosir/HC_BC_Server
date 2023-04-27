using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class DateFormat
    {
        public const string NoSpace = "yyyyMMddHHmmss";
        public const string Normal = "yyyy-MM-dd HH:mm:ss";
    }

    public class EqpList
    {
        public const string Loader1 = "Loader1";
        public const string Loader2 = "Loader2";
    }

    public class Level
    {
        public const int NormarlUser = 1;
        public const int Aministrator = 2;
    }

    public class LineType
    {
        public const string PAKUPK1 = "PAKUPK1";
        public const string PAKUPK2 = "PAKUPK2";
        public const string Cut = "Cut";
        public const string Dense1 = "Dense1";
        public const string Dense2 = "Dense2";
        public const string POL = "POL";
        public const string POL_LG = "POL_LG";
        public const string MF0405 = "MF0405";
        public const string OLB_C6 = "OLB_C6";
        public const string OLB_C7 = "OLB_C7";
        public const string ASSY = "ASSY";
        public const string OCPacking = "OCPacking";
        public const string Rework = "Rework";
    }

    public enum eAlarmCode
    {
        Light = 1,
        Serious = 2
    }

    public enum eAlarmStatus
    {
        Set = 1,
        Clear = 2,
        PLCClear = 0
    }

    public class EQPType
    {
        public const int IndexerModule = 1;//这类设备没有processData
        public const int ProcessModule = 2;//需要上报processData
    }
    /// <summary>
    /// 1	PM	
    ///2	DOWN	
    ///3	Pause	
    ///4	IDLE	
    ///5	RUN
    ///[ IDLE | RUN | DOWN | PM ]
    /// </summary>
    public enum EquipmentStatus
    {
        PM = 1,
        DOWN = 2,
        PAUSE = 3,
        IDLE = 4,
        RUN = 5
    }
    public class PortType
    {
        public const int PL = 1;
        public const int PU = 2;
        public const int PB = 3;
    }

    public class OperationText
    {
        public const string Normal = "Normal";
        public const string Dummy = "Dummy";
    }
    public class SentOutJobInfoName
    {
        public const string SentJob1Block = "SentJob1Block";
        public const string SentJob2Block = "SentJob2Block";
        public const string SentJob3Block = "SentJob3Block";
        public const string SentJob4Block = "SentJob4Block";
        public const string SentJob5Block = "SentJob5Block";
    }

    public class PPType
    {
        public const string Equipment = "E";
        public const string Unit = "U";
    }

    public class VCRReadingMode
    {
        public const string ErrorSkip = "OS";
        public const string ErrorKeyin = "OK";
        public const string ReadingSkip = "FS";
        public const string AlwayskeyIn = "FK";
    }

    public class eCMD
    {
        public const int Start = 1;
        public const int Cancel = 2;
    }
    //public class ControlState
    //{
    //    //[ OnLineLocal | OffLine | OnLineRemote ]
    //    public const string OnLineRemote = "OnLineRemote";
    //    public const string OnLineLocal = "OnLineLocal";
    //    public const string Offline = "OffLine";
    //    //public const string LCControl = "LCCONTROL";
    //}
    public class PortName
    {
        //[ OnLineLocal | OffLine | OnLineRemote ]
        public const string Port1 = "L01";
        public const string Port2 = "L02";
        public const string Port3 = "L03";
        public const string Port4 = "L04";
        public const string Port5 = "L05";
        public const string Port6 = "L06";
        public const string Port7 = "L07";
        public const string Port8 = "L08";
        public const string Port9 = "L09";
        public const string Port10 = "L10";
        public const string BothPort1 = "C01";
        public const string BothPort2 = "C02";
        public const string BothPort3 = "C03";
        public const string BothPort4 = "C04";
    }
    public class RecipeType
    {
        public const string EQP = "E";
        public const string Unit = "U";
        public const string SUnit = "S";
    }
    public class PortCSTType
    {

        public const string Port = "Port";
        public const string OLEDMaskCST = "OLEDMaskCST";
        public const string TrayGroup = "TrayGroup";
        public const string OLEDMask = "OLEDMask";
        public const string Crate = "Crate";
    }
    public class ProductType// [ Sheet | Glass | Panel ]
    {

        public const string Sheet = "Sheet";
        public const string Glass = "Glass";
        public const string Panel = "Panel";
    }
    public enum MaterialState
    {
        Loaded = 1,
        Unloaded = 2
    }
    public enum CassetteType
    {
        Normal,
        Wire,
        AssembleWire,
        OneColumn,
        TwoColumn,
        ThreeColumn,
        Crate,
        Lamination
    }

    public class eClientType
    {
        public const string PLC = "PLC";
        public const string HSMS = "HSMS";
    }

    public enum eDirection
    {
        Left = 0,
        Right = 1
    }

    public class CarrierControl
    {
        public const int Start = 1;
        public const int Pause = 2;
        public const int Resume = 3;
        public const int Cancel = 4;
        public const int Abort = 5;

    }

    public class eCEID
    {
        public const string OperationModeChanged = "103";
        public const string FlowControlModeListChanged = "104";
        public const string UnitStatusChange = "105";
        public const string SubUnitStatusChange = "106";
        public const string ReticleStatusChange = "107";
        public const string MaterialChange = "108";
        public const string ConstantChange = "109";
        public const string ReadyToStart = "110";
        public const string Offline = "111";
        public const string Local = "112";
        public const string Remote = "113";
        public const string EquipmentStatusChange = "114";
        public const string CurrentFlowControlModeChange = "115";
        public const string OPCallConfirm = "118";
        public const string LoadRequest = "200";
        public const string PreLoadComplete = "201";
        public const string LoadComplete = "202";
        public const string UnloadRequest = "203";
        public const string UnloadComplete = "204";
        public const string PortEnabled = "207";
        public const string PortDisabled = "205";
        public const string PortTransferModeChangeAndUseTypeChange = "206";
        public const string PortUseTypeChanged = "208";
        public const string PortTypeChanged = "209";
        public const string CratePortLoadRequest = "210";
        public const string CratePortLoadComplete = "211";
        public const string CratePortUnloadRequest = "212";
        public const string CratePortUnloadComplete = "213";
        public const string CratePortDisabled = "214";
        public const string CratePortEnable = "215";
        public const string CratePortTransferModeChange = "216";
        public const string RemainedglasscountofCrateReport = "217";
        public const string ProcessStart = "301";
        public const string ProcessEnd = "302";
        public const string ProcessAbnormalEnd = "303";
        public const string ProcessCancel = "304";
        public const string ProcessAbort = "305";
        public const string UnitProcessStart = "306";
        public const string UnitProcessEnd = "307";
        public const string SubUnitProcessStart = "308";
        public const string SubUnitProcessEnd = "309";

        public const string LastGlassProcessStart = "311";
        public const string ToCSTProcessEnd = "312";
        public const string FromCSTProcessEnd = "313";

        public const string GlassRemove = "341";
        public const string GlassLineInOut = "701";

        public const string ComponentGlassProcessStartInModule = "331";
        public const string ComponentGlassProcessEndInModule = "332";
        public const string ComponentGlassOutByIndexerEvent = "321";
        public const string ComponentGlassInByIndexerEvent = "322";
        public const string ComponentGlassOutByUnitEvent = "323";
        public const string ComponentGlassInByUnitEvent = "324";
    }

    public class eCEED
    {
        public const string Enable = "0";
        public const string Disable = "1";
    }

    public class eSFCD
    {
        public const string EQ_Status = "1";
        public const string Port_Status = "2";
        public const string Unit_Status = "4";
        public const string SubUnit_Status = "5";
        public const string Material_Status = "7";
        public const string BufferPanelInfo = "9";
        public const string OperationMode = "A";
    }

    public class RemoteCommand
    {
        public const string Start = "1";
        public const string Cancel = "2";
        public const string Abort = "3";
        public const string Pause = "4";
        public const string Resume = "5";
        public const string OPCall = "6";
    }

    public enum PortTransferMode
    {
        Auto = 1,
        Manual = 2
    }

    public class CommunicationStatus
    {
        public const int CimOn = 1;
        public const int CimOff = 2;
    }


    //public class CarrierStatus
    //{
    //    public const int NoCassette = 0;
    //    public const int WaitForCSTData = 1;
    //    public const int WaitForStart = 2;
    //    public const int InProcessing = 3;
    //    public const int ProcessEnd = 4;
    //    public const int ProcessCancel = 5;
    //    public const int ProcessAbort = 6;
    //}

    public class CassetteCompleteCode
    {
        public const int Default = 0;
        public const int NormalComplete = 1;
        public const int OperatorCancel = 2;
        public const int OperatorAbort = 3;
        public const int LCCancel = 4;
        public const int EQCancel = 6;
    }

    public class ACKCode
    {
        // Common 
        public const int Accepted = 0;
        public const int NotAccepted = 1;

        public const int ConstantNotExist = 1;
        public const int ConstantBusy = 2;
        public const int ConstantOutRange = 3;
        public const int ConstantErro = 4;

        public const int EventNotExist = 1;
        public const int EventErro = 2;

        //HCACK S2F42
        public const int PortIdInvalid = 1;
        public const int CSTIdInvalid = 2;
        public const int LotIdInvalid = 3;
        public const int CmdNotExist = 4;
        public const int AlreadyReceived = 5;

        //LIACCK
        public const int PPIDInvalid = 3;
        public const int SLOTInformationMismatch = 4;
        // Online Acknowledge
        public const int AlreadyOnlineLocal = 2;
        public const int AlreadyOnlineRemote = 3;

        // RTCode Return
        public const int OK = 0;
        public const int NG = 1;

        //VCR Status
        public const int OnSkip = 1;
        public const int OnKeyIn = 2;
        public const int OffSkip = 3;
        public const int OffKeyIn = 4;
    }

    public class HostRTCode
    {
        public const int OK = 0;
        public const int NG = 1;
    }

    public enum eLoginAction
    {
        Login = 0,
        Logout = 1,
    }
    public class PLCEventName
    {
        #region matti
        public const string EIPConnect = "EIPConnect";
        public const string PositionStatus = "PositionStatus";
        public const string ReceivedJobReport1Block = "ReceivedJobReport1Block";
        public const string ReceivedJobReport2Block = "ReceivedJobReport2Block";
        public const string SentOutJobReport1Block = "SentOutJobReport1Block";
        public const string SentOutJobReport2Block = "SentOutJobReport2Block";
        public const string StoredJobReport1Block = "StoredJobReport1Block";
        public const string StoredJobReport2Block = "StoredJobReport2Block";
        public const string StoredJobReport3Block = "StoredJobReport3Block";
        public const string StoredJobReport4Block = "StoredJobReport4Block";
        public const string StoredJobReport5Block = "StoredJobReport5Block";
        public const string StoredJobReport6Block = "StoredJobReport6Block";
        public const string StoredJobReport7Block = "StoredJobReport7Block";
        public const string StoredJobReport8Block = "StoredJobReport8Block";
        public const string StoredJobReport9Block = "StoredJobReport9Block";
        public const string FetchedOutJobReport1Block = "FetchedOutJobReport1Block";
        public const string FetchedOutJobReport2Block = "FetchedOutJobReport2Block";
        public const string FetchedOutJobReport3Block = "FetchedOutJobReport3Block";
        public const string FetchedOutJobReport4Block = "FetchedOutJobReport4Block";
        public const string FetchedOutJobReport5Block = "FetchedOutJobReport5Block";
        public const string FetchedOutJobReport6Block = "FetchedOutJobReport6Block";
        public const string FetchedOutJobReport7Block = "FetchedOutJobReport7Block";
        public const string FetchedOutJobReport8Block = "FetchedOutJobReport8Block";
        public const string FetchedOutJobReport9Block = "FetchedOutJobReport9Block";
        public const string JobManualMoveReportBlock = "JobManualMoveReportBlock";
        public const string JobDataRequestBlock = "JobDataRequestBlock";
        public const string DVDataReportBlock = "DVDataReportBlock";
        public const string CVDataReportBlock = "CVDataReportBlock";
        public const string JobDataChangeReportBlock = "JobDataChangeReportBlock";
        public const string JobCountBlock = "JobCountBlock";
        public const string ProcessStartReportBlock = "ProcessStartReportBlock";
        public const string ProcessEndReportBlock = "ProcessEndReportBlock";
        public const string CVDataASCIIBlock = "CVDataASCIIBlock";
        public const string CVDataFLOATBlock = "CVDataFLOATBlock";
        public const string CVDataSIBlock = "CVDataSIBlock";
        public const string CVDataINTBlock = "CVDataINTBlock";
        public const string DateTimeRequestBlock = "DateTimeRequestBlock";
        public const string CIMMessageSetCommandReplyBlock = "CIMMessageSetCommandReplyBlock";
        public const string CIMMessageClearCommandReplyBlock = "CIMMessageClearCommandReplyBlock";
        public const string CVReportTimeChangeCommandReplyBlock = "CVReportTimeChangeCommandReplyBlock";
        public const string CIMMessageConfirmReportBlock = "CIMMessageConfirmReportBlock";
        public const string MachineStatusChangeReportBlock = "MachineStatusChangeReportBlock";
        public const string CIMMode = "CIMMode";
        public const string CIMModeChangeCommandReplyBlock = "CIMModeChangeCommandReplyBlock";
        public const string MaterialStatusChangeReport1Block = "MaterialStatusChangeReport1Block";
        public const string MaterialStatusChangeReport2Block = "MaterialStatusChangeReport2Block";
        public const string VCRStatusReportBlock = "VCRStatusReportBlock";
        public const string Port1PortTypeChangeReportBlock = "Port1PortTypeChangeReportBlock";
        public const string Port2PortTypeChangeReportBlock = "Port2PortTypeChangeReportBlock";
        public const string Port3PortTypeChangeReportBlock = "Port3PortTypeChangeReportBlock";
        public const string Port4PortTypeChangeReportBlock = "Port4PortTypeChangeReportBlock";
        public const string Port1PortTypeChangeCommandReplyBlock = "Port1PortTypeChangeCommandReplyBlock";
        public const string Port2PortTypeChangeCommandReplyBlock = "Port2PortTypeChangeCommandReplyBlock";
        public const string Port3PortTypeChangeCommandReplyBlock = "Port3PortTypeChangeCommandReplyBlock";
        public const string Port4PortTypeChangeCommandReplyBlock = "Port4PortTypeChangeCommandReplyBlock";
        public const string Port1PortTypeAutoChangeModeReportBlock = "Port1PortTypeAutoChangeModeReportBlock";
        public const string Port2PortTypeAutoChangeModeReportBlock = "Port2PortTypeAutoChangeModeReportBlock";
        public const string Port3PortTypeAutoChangeModeReportBlock = "Port3PortTypeAutoChangeModeReportBlock";
        public const string Port4PortTypeAutoChangeModeReportBlock = "Port4PortTypeAutoChangeModeReportBlock";
        public const string Port1PortTypeAutoChangeModeCommandReplyBlock = "Port1PortTypeAutoChangeModeCommandReplyBlock";
        public const string Port2PortTypeAutoChangeModeCommandReplyBlock = "Port2PortTypeAutoChangeModeCommandReplyBlock";
        public const string Port3PortTypeAutoChangeModeCommandReplyBlock = "Port3PortTypeAutoChangeModeCommandReplyBlock";
        public const string Port4PortTypeAutoChangeModeCommandReplyBlock = "Port4PortTypeAutoChangeModeCommandReplyBlock";
        public const string Port1PortModeChangeReportBlock = "Port1PortModeChangeReportBlock";
        public const string Port2PortModeChangeReportBlock = "Port2PortModeChangeReportBlock";
        public const string Port3PortModeChangeReportBlock = "Port3PortModeChangeReportBlock";
        public const string Port4PortModeChangeReportBlock = "Port4PortModeChangeReportBlock";
        public const string Port1PortModeChangeCommandReplyBlock = "Port1PortModeChangeCommandReplyBlock";
        public const string Port2PortModeChangeCommandReplyBlock = "Port2PortModeChangeCommandReplyBlock";
        public const string Port3PortModeChangeCommandReplyBlock = "Port3PortModeChangeCommandReplyBlock";
        public const string Port4PortModeChangeCommandReplyBlock = "Port4PortModeChangeCommandReplyBlock";
        public const string Port1EnableModeChangeReportBlock = "Port1EnableModeChangeReportBlock";
        public const string Port2EnableModeChangeReportBlock = "Port2EnableModeChangeReportBlock";
        public const string Port3EnableModeChangeReportBlock = "Port3EnableModeChangeReportBlock";
        public const string Port4EnableModeChangeReportBlock = "Port4EnableModeChangeReportBlock";
        public const string Port1EnableModeChangeCommandReplyBlock = "Port1EnableModeChangeCommandReplyBlock";
        public const string Port2EnableModeChangeCommandReplyBlock = "Port2EnableModeChangeCommandReplyBlock";
        public const string Port3EnableModeChangeCommandReplyBlock = "Port3EnableModeChangeCommandReplyBlock";
        public const string Port4EnableModeChangeCommandReplyBlock = "Port4EnableModeChangeCommandReplyBlock";
        public const string Port1PortTransferModeChangeReportBlock = "Port1PortTransferModeChangeReportBlock";
        public const string Port2PortTransferModeChangeReportBlock = "Port2PortTransferModeChangeReportBlock";
        public const string Port3PortTransferModeChangeReportBlock = "Port3PortTransferModeChangeReportBlock";
        public const string Port4PortTransferModeChangeReportBlock = "Port4PortTransferModeChangeReportBlock";
        public const string Port1PortTransferModeChangeCommandReplyBlock = "Port1PortTransferModeChangeCommandReplyBlock";
        public const string Port2PortTransferModeChangeCommandReplyBlock = "Port2PortTransferModeChangeCommandReplyBlock";
        public const string Port3PortTransferModeChangeCommandReplyBlock = "Port3PortTransferModeChangeCommandReplyBlock";
        public const string Port4PortTransferModeChangeCommandReplyBlock = "Port4PortTransferModeChangeCommandReplyBlock";
        public const string Port1PauseModeChangeReportBlock = "Port1PauseModeChangeReportBlock";
        public const string Port2PauseModeChangeReportBlock = "Port2PauseModeChangeReportBlock";
        public const string Port3PauseModeChangeReportBlock = "Port3PauseModeChangeReportBlock";
        public const string Port4PauseModeChangeReportBlock = "Port4PauseModeChangeReportBlock";
        public const string Port1PauseModeChangeCommandReplyBlock = "Port1PauseModeChangeCommandReplyBlock";
        public const string Port2PauseModeChangeCommandReplyBlock = "Port2PauseModeChangeCommandReplyBlock";
        public const string Port3PauseModeChangeCommandReplyBlock = "Port3PauseModeChangeCommandReplyBlock";
        public const string Port4PauseModeChangeCommandReplyBlock = "Port4PauseModeChangeCommandReplyBlock";
        public const string Port1PortGradeChangeReportBlock = "Port1PortGradeChangeReportBlock";
        public const string Port2PortGradeChangeReportBlock = "Port2PortGradeChangeReportBlock";
        public const string Port3PortGradeChangeReportBlock = "Port3PortGradeChangeReportBlock";
        public const string Port4PortGradeChangeReportBlock = "Port4PortGradeChangeReportBlock";
        public const string Port1PortGradeChangeCommandReplyBlock = "Port1PortGradeChangeCommandReplyBlock";
        public const string Port2PortGradeChangeCommandReplyBlock = "Port2PortGradeChangeCommandReplyBlock";
        public const string Port3PortGradeChangeCommandReplyBlock = "Port3PortGradeChangeCommandReplyBlock";
        public const string Port4PortGradeChangeCommandReplyBlock = "Port4PortGradeChangeCommandReplyBlock";
        public const string Port1PortCassetteTypeChangeReportBlock = "Port1PortCassetteTypeChangeReportBlock";
        public const string Port2PortCassetteTypeChangeReportBlock = "Port2PortCassetteTypeChangeReportBlock";
        public const string Port3PortCassetteTypeChangeReportBlock = "Port3PortCassetteTypeChangeReportBlock";
        public const string Port4PortCassetteTypeChangeReportBlock = "Port4PortCassetteTypeChangeReportBlock";
        public const string Port5PortCassetteTypeChangeReportBlock = "Port5PortCassetteTypeChangeReportBlock";
        public const string Port6PortCassetteTypeChangeReportBlock = "Port6PortCassetteTypeChangeReportBlock";
        public const string Port7PortCassetteTypeChangeReportBlock = "Port7PortCassetteTypeChangeReportBlock";
        public const string Port8PortCassetteTypeChangeReportBlock = "Port8PortCassetteTypeChangeReportBlock";
        public const string Port9PortCassetteTypeChangeReportBlock = "Port9PortCassetteTypeChangeReportBlock";
        public const string Port1PortCassetteTypeChangeCommandReplyBlock = "Port1PortCassetteTypeChangeCommandReplyBlock";
        public const string Port2PortCassetteTypeChangeCommandReplyBlock = "Port2PortCassetteTypeChangeCommandReplyBlock";
        public const string Port3PortCassetteTypeChangeCommandReplyBlock = "Port3PortCassetteTypeChangeCommandReplyBlock";
        public const string Port4PortCassetteTypeChangeCommandReplyBlock = "Port4PortCassetteTypeChangeCommandReplyBlock";
        public const string VCRReadCompleteReportBlock = "VCRReadCompleteReportBlock";
        public const string Port1QTimeChangeCommandReplyBlock = "Port1QTimeChangeCommandReplyBlock";
        public const string Port2QTimeChangeCommandReplyBlock = "Port2QTimeChangeCommandReplyBlock";
        public const string Port3QTimeChangeCommandReplyBlock = "Port3QTimeChangeCommandReplyBlock";
        public const string Port4QTimeChangeCommandReplyBlock = "Port4QTimeChangeCommandReplyBlock";
        public const string MachineModeChangeCommandReplyBlock = "MachineModeChangeCommandReplyBlock";
        public const string SamplingDownloadCommandReplyBlock = "SamplingDownloadCommandReplyBlock";
        public const string DVSamplingFlagCommandReplyBlock = "DVSamplingFlagCommandReplyBlock";
        public const string SpecialCodeRequestBlock = "SpecialCodeRequestBlock";
        public const string CuttingRequestBlock = "CuttingRequestBlock";
        public const string Port1PortControlCommandReplyBlock = "Port1PortControlCommandReplyBlock";
        public const string Port2PortControlCommandReplyBlock = "Port2PortControlCommandReplyBlock";
        public const string Port3PortControlCommandReplyBlock = "Port3PortControlCommandReplyBlock";
        public const string Port4PortControlCommandReplyBlock = "Port4PortControlCommandReplyBlock";
        public const string AbnormalCodeReportBlock = "AbnormalCodeReportBlock";
        public const string Port1BoxInfoRequestBlock = "Port1BoxInfoRequestBlock";
        public const string Port2BoxInfoRequestBlock = "Port2BoxInfoRequestBlock";
        public const string Port3BoxInfoRequestBlock = "Port3BoxInfoRequestBlock";
        public const string Port4BoxInfoRequestBlock = "Port4BoxInfoRequestBlock";
        public const string Port5BoxInfoRequestBlock = "Port5BoxInfoRequestBlock";
        public const string Port6BoxInfoRequestBlock = "Port6BoxInfoRequestBlock";
        public const string Port7BoxInfoRequestBlock = "Port7BoxInfoRequestBlock";
        public const string Port8BoxInfoRequestBlock = "Port8BoxInfoRequestBlock";
        public const string Port9BoxInfoRequestBlock = "Port9BoxInfoRequestBlock";
        public const string Port1BoxGroupPortStatusReportBlock = "Port1BoxGroupPortStatusReportBlock";
        public const string Port2BoxGroupPortStatusReportBlock = "Port2BoxGroupPortStatusReportBlock";
        public const string Port3BoxGroupPortStatusReportBlock = "Port3BoxGroupPortStatusReportBlock";
        public const string Port4BoxGroupPortStatusReportBlock = "Port4BoxGroupPortStatusReportBlock";
        public const string Port5BoxGroupPortStatusReportBlock = "Port5BoxGroupPortStatusReportBlock";
        public const string Port6BoxGroupPortStatusReportBlock = "Port6BoxGroupPortStatusReportBlock";
        public const string Port7BoxGroupPortStatusReportBlock = "Port7BoxGroupPortStatusReportBlock";
        public const string Port8BoxGroupPortStatusReportBlock = "Port8BoxGroupPortStatusReportBlock";
        public const string Port9BoxGroupPortStatusReportBlock = "Port9BoxGroupPortStatusReportBlock";
        public const string Port1BoxPortStatusReportBlock = "Port1BoxPortStatusReportBlock";
        public const string Port2BoxPortStatusReportBlock = "Port2BoxPortStatusReportBlock";
        public const string Port3BoxPortStatusReportBlock = "Port3BoxPortStatusReportBlock";
        public const string Port4BoxPortStatusReportBlock = "Port4BoxPortStatusReportBlock";
        public const string Port5BoxPortStatusReportBlock = "Port5BoxPortStatusReportBlock";
        public const string Port6BoxPortStatusReportBlock = "Port6BoxPortStatusReportBlock";
        public const string Port7BoxPortStatusReportBlock = "Port7BoxPortStatusReportBlock";
        public const string Port8BoxPortStatusReportBlock = "Port8BoxPortStatusReportBlock";
        public const string Port9BoxPortStatusReportBlock = "Port9BoxPortStatusReportBlock";
        public const string JobData1 = "JobData#1";
        public const string JobData2 = "JobData#2";

        #region 需求3 1.EIP通讯状态变化 liuyusen 20221010
        public const string EIPDisConnect = "EIPDisConnect";
        public const string MachineAlive = "MachineAlive";
        #endregion

        #endregion
        #region qly
        public const string CheckLotBindingRequestBlock = "CheckLotBindingRequestBlock";
        #endregion
        #region Yuan
        public const string UpstreamLinkSignal = "UpstreamLinkSignal";
        public const string DownstreamLinkSignal = "DownstreamLinkSignal";

        public const string PanelProcessEndRequestBlock = "PanelProcessEndRequestBlock";
        public const string PanelJudgeDataDownloadRequestBlock = "PanelJudgeDataDownloadRequestBlock";
        public const string PanelDataUpdateReportBlock = "PanelDataUpdateReportBlock";
        public const string JobJudgeResultReport1Block = "JobJudgeResultReport1Block";
        public const string JobJudgeResultReport2Block = "JobJudgeResultReport2Block";
        public const string JobJudgeResultReportBlock = "JobJudgeResultReportBlock";
        public const string MachineModeChangeReportBlock = "MachineModeChangeReportBlock";
        public const string SamplingRequestBlock = "SamplingRequestBlock";
        public const string SamplingFlagReportBlock = "SamplingFlagReportBlock";
        public const string IonizerStatusReportBlock = "IonizerStatusReportBlock";
        public const string CSTMoveInReportBlock = "CSTMoveInReportBlock";
        public const string CSTMoveOutReportBlock = "CSTMoveOutReportBlock";
        public const string OperatorLoginReportBlock = "OperatorLoginReportBlock";
        public const string JobAssemblyReport1Block = "JobAssemblyReport1Block";
        public const string BoxWeightCheckRequestBlock = "BoxWeightCheckRequestBlock";
        public const string DefectCodeReportBlock = "DefectCodeReportBlock";
        public const string LableInfoRequestReportBlock = "LableInfoRequestReportBlock";
        public const string PalletInfoRequestBlock = "PalletInfoRequestBlock";
        public const string MaterialValidationRequest1Block = "MaterialValidationRequest1Block";
        public const string MaterialValidationRequest2Block = "MaterialValidationRequest2Block";
        public const string OCIDRequestBlock = "OCIDRequestBlock";

        public const string RecipeParameterRequestCommandReplyBlock = "RecipeParameterRequestCommandReplyBlock";
        public const string RecipeChangeReportBlock = "RecipeChangeReportBlock";
        public const string CurrentRecipeNumberChangeReportBlock = "CurrentRecipeNumberChangeReportBlock";
        public const string AutoRecipeChangeModeReportBlock = "AutoRecipeChangeModeReportBlock";
        public const string AlarmReportBlock = "AlarmReportBlock";
        public const string AlarmReport1Block = "AlarmReport1Block";
        public const string AlarmReport2Block = "AlarmReport2Block";
        public const string AlarmReport3Block = "AlarmReport3Block";
        public const string AlarmReport4Block = "AlarmReport4Block";
        public const string AlarmReport5Block = "AlarmReport5Block";
        public const string AlarmReport6Block = "AlarmReport6Block";
        public const string AlarmReport7Block = "AlarmReport7Block";
        public const string AlarmReport8Block = "AlarmReport8Block";
        public const string AlarmReport9Block = "AlarmReport9Block";

        public const string CellMaterialAssemblyReportBlock = "CellMaterialAssemblyReportBlock";
        public const string CellMaterialAssemblyReport1Block = "CellMaterialAssemblyReport1Block";
        public const string CellMaterialAssemblyReport2Block = "CellMaterialAssemblyReport2Block";
        public const string MaterialCountChangeReportBlock = "MaterialCountChangeReportBlock";
        public const string MaterialCountChangeReport1Block = "MaterialCountChangeReport1Block";
        public const string MaterialCountChangeReport2Block = "MaterialCountChangeReport2Block";

        public const string MaterialLotInfoRequestBlock = "MaterialLotInfoRequestBlock";
        public const string MaterialLotInfoRequest1Block = "MaterialLotInfoRequest1Block";
        public const string MaterialLotInfoRequest2Block = "MaterialLotInfoRequest2Block";

        public const string TransferBoxReport1Block = "TransferBoxReport1Block";
        public const string TransferBoxReport2Block = "TransferBoxReport2Block";

        public const string CVMonitoringSlot1Block = "CVMonitoringSlot1Block";
        public const string CVMonitoringSlot2Block = "CVMonitoringSlot2Block";
        public const string CVMonitoringSlot3Block = "CVMonitoringSlot3Block";
        public const string CVMonitoringSlot4Block = "CVMonitoringSlot4Block";
        public const string CVMonitoringSlot5Block = "CVMonitoringSlot5Block";
        public const string CVMonitoringSlot6Block = "CVMonitoringSlot6Block";

        public const string BufferJobMonitoring1Block = "BufferJobMonitoring1Block";
        public const string BufferJobMonitoring2Block = "BufferJobMonitoring2Block";
        public const string BufferJobMonitoring3Block = "BufferJobMonitoring3Block";

        public const string CV1State = "CV1State";
        public const string CV2State = "CV2State";
        public const string CV3State = "CV3State";
        public const string CV4State = "CV4State";

        public const string RobotStatus = "RobotStatus";

        public const string RobotArmMonitoringBlock = "RobotArmMonitoringBlock";

        public const string RobotCommandResultReportBlock = "RobotCommandResultReportBlock";
        public const string RobotCommandFetchOutReportBlock = "RobotCommandFetchOutReportBlock";
        public const string RobotControlCommandReplyBlock = "RobotControlCommandReplyBlock";

        public const string Port1CassetteProcessEndReportBlock = "Port1CassetteProcessEndReportBlock";
        public const string Port2CassetteProcessEndReportBlock = "Port2CassetteProcessEndReportBlock";
        public const string Port3CassetteProcessEndReportBlock = "Port3CassetteProcessEndReportBlock";
        public const string Port4CassetteProcessEndReportBlock = "Port4CassetteProcessEndReportBlock";

        public const string Port1CassetteProcessStartReportBlock = "Port1CassetteProcessStartReportBlock";
        public const string Port2CassetteProcessStartReportBlock = "Port2CassetteProcessStartReportBlock";
        public const string Port3CassetteProcessStartReportBlock = "Port3CassetteProcessStartReportBlock";
        public const string Port4CassetteProcessStartReportBlock = "Port4CassetteProcessStartReportBlock";

        public const string Port1CassetteControlCommandReplyBlock = "Port1CassetteControlCommandReplyBlock";
        public const string Port2CassetteControlCommandReplyBlock = "Port2CassetteControlCommandReplyBlock";
        public const string Port3CassetteControlCommandReplyBlock = "Port3CassetteControlCommandReplyBlock";
        public const string Port4CassetteControlCommandReplyBlock = "Port4CassetteControlCommandReplyBlock";

        public const string Port1CassetteMapDownloadCommandReplyBlock = "Port1CassetteMapDownloadCommandReplyBlock";
        public const string Port2CassetteMapDownloadCommandReplyBlock = "Port2CassetteMapDownloadCommandReplyBlock";
        public const string Port3CassetteMapDownloadCommandReplyBlock = "Port3CassetteMapDownloadCommandReplyBlock";
        public const string Port4CassetteMapDownloadCommandReplyBlock = "Port4CassetteMapDownloadCommandReplyBlock";

        public const string Port1PortQTimeChangeReportBlock = "Port1PortQTimeChangeReportBlock";
        public const string Port2PortQTimeChangeReportBlock = "Port2PortQTimeChangeReportBlock";
        public const string Port3PortQTimeChangeReportBlock = "Port3PortQTimeChangeReportBlock";
        public const string Port4PortQTimeChangeReportBlock = "Port4PortQTimeChangeReportBlock";

        public const string Port1PortQTimeTimeOutReportBlock = "Port1PortQTimeTimeOutReportBlock";
        public const string Port2PortQTimeTimeOutReportBlock = "Port2PortQTimeTimeOutReportBlock";
        public const string Port3PortQTimeTimeOutReportBlock = "Port3PortQTimeTimeOutReportBlock";
        public const string Port4PortQTimeTimeOutReportBlock = "Port4PortQTimeTimeOutReportBlock";

        public const string Port1PortStatusChangeReportBlock = "Port1PortStatusChangeReportBlock";
        public const string Port2PortStatusChangeReportBlock = "Port2PortStatusChangeReportBlock";
        public const string Port3PortStatusChangeReportBlock = "Port3PortStatusChangeReportBlock";
        public const string Port4PortStatusChangeReportBlock = "Port4PortStatusChangeReportBlock";
        #endregion
        public const string EqpAliveBitChange = "EqpAlive";
        public const string CIMCommStatusChangeReport = "CIMCommStatusChangeReport";
        public const string ClearLineReport = "ClearLineReport";
        public const string CurrentRecipeIDChangeReport = "CurrentRecipeIDChangeReport";
        public const string CurrentRecipeIDVldReuqestReply = "CurrentRecipeIDVldReuqestReply";
        public const string CuttingCompleteReport = "CuttingCompleteReport";
        public const string EmptyCarrierVldRequest = "EmptyCarrierVldRequest";
        public const string EQPAlarmReport = "EQPAlarmReport";
        public const string EQPStatusChangeReport = "EQPStatusChangeReport";
        public const string FetchOutJobEventReport = "FetchOutJobEventReport";
        public const string MaterialStatusChangeReport = "MaterialStatusChangeReport";
        public const string PanelInformationRequest1 = "PanelInformationRequest1";
        public const string PanelInformationRequest2 = "PanelInformationRequest2";
        public const string ProcessDataReport = "ProcessDataReport";
        public const string ReceiveJobEventReport = "ReceiveJobEventReport";
        public const string RecipeChangeReport = "RecipeChangeReport";
        public const string RemovePanelEventReport = "RemovePanelEventReport";
        public const string ReturnLineReuqest = "ReturnLineReuqest";
        public const string SendOutJobEventReport1 = "SendOutJobEventReport1";
        public const string SendOutJobEventReport = "SendOutJobEventReport";
        public const string StoreInJobEventReport = "StoreInJobEventReport";
        public const string TracingDataReport = "TracingDataReport";
        public const string UnitInEvent1Report = "UnitInEvent1Report";
        public const string UnitInEvent2Report = "UnitInEvent2Report";
        public const string UnitOutEvent1Report = "UnitOutEvent1Report";
        public const string UnitOutEvent2Report = "UnitOutEvent2Report";
        public const string VCRStatusChangeReport = "VCRStatusChangeReport";
        public const string Port1StatusChangeReport = "Port1StatusChangeReport";
        public const string Port2StatusChangeReport = "Port2StatusChangeReport";
        public const string Carrier1StatusChangeReport = "Carrier1StatusChangeReport";
        public const string Carrier2StatusChangeReport = "Carrier2StatusChangeReport";
    }

    public class PLCEventItem
    {
        #region Yuan
        public const string CassetteIDOrBoxID = "CassetteID(BoxID)";
        public const string BoxWeight = "BoxWeight";

        public const string BLUID = "BLUID";
        public const string BLUIDLotSequenceNumber = "BLUIDLotSequenceNumber";
        public const string BLUIDSlotSequenceNumber = "BLUIDSlotSequenceNumber";
        public const string JobIDLotSequenceNumber = "JobIDLotSequenceNumber";
        public const string JobIDSlotSequenceNumber = "JobIDSlotSequenceNumber";

        public const string AutoRecipeChangeMode = "AutoRecipeChangeMode";
        public const string Result = "Result";
        //CurrentRecipeNumberChangeReportBlock
        public const string CurrentRecipeNumber = "CurrentRecipeNumber";
        public const string RecipeVersionTimeYear = "RecipeVersionTimeYear";
        public const string RecipeVersionTimeMonth = "RecipeVersionTimeMonth";
        public const string RecipeVersionTimeDay = "RecipeVersionTimeDay";
        public const string RecipeVersionTimeHour = "RecipeVersionTimeHour";
        public const string RecipeVersionTimeMinute = "RecipeVersionTimeMinute";
        public const string RecipeVersionTimeSecond = "RecipeVersionTimeSecond";
        public const string RecipeStepNumber = "RecipeStepNumber";

        //RobotCommandFetchOutReportBlock
        public const string CommandSequenceNumber = "CommandSequenceNumber";
        public const string RCMDRobotCommand = "RCMDRobotCommand";
        public const string ArmNumber = "ArmNumber";
        public const string GetPosition = "GetPosition";
        public const string PutPosition = "PutPosition";
        public const string GetSlotNumber = "GetSlotNumber";
        public const string PutSlotNumber = "PutSlotNumber";
        public const string SuCIMommand = "SuCIMommand";
        public const string GetSlotPosition = "GetSlotPosition";
        public const string PutSlotPosition = "PutSlotPosition";

        //RobotCommandResultReportBlock
        public const string FirstCommandResult = "1stCommandResult";
        public const string FirstCommandResultComment = "1stCommandResultComment";
        public const string SecondCommandResult = "2ndCommandResult";
        public const string SecondCommandResultComment = "2ndCommandResultComment";
        public const string ThirdCommandResult = "3rdCommandResult";
        public const string ThirdCommandResultComment = "3rdCommandResultComment";
        public const string FourthCommandResult = "4thCommandResult";
        public const string FourthCommandResultComment = "4thCommandResultComment";
        public const string CurrentPosition = "CurrentPosition";

        //RobotArmSubstrateLoadReportBlock/Unload
        public const string UpperArm1LotSequenceNumber = "UpperArm-1LotSequenceNumber";
        public const string UpperArm1SlotSequenceNumber = "UpperArm-1SlotSequenceNumber";
        public const string UpperArm2LotSequenceNumber = "UpperArm-2LotSequenceNumber";
        public const string UpperArm2SlotSequenceNumber = "UpperArm-2SlotSequenceNumber";
        public const string LowerArm1LotSequenceNumber = "LowerArm-1LotSequenceNumber";
        public const string LowerArm1SlotSequenceNumber = "LowerArm-1SlotSequenceNumber";
        public const string LowerArm2LotSequenceNumber = "LowerArm-2LotSequenceNumber";
        public const string LowerArm2SlotSequenceNumber = "LowerArm-2SlotSequenceNumber";

        //RobotCommandMonitoringBlock
        public const string FirstRCMD = "1stRCMD";
        public const string FirstArmNumber = "1stArmNumber";
        public const string FirstGetPosition = "1stGetPosition";
        public const string FirstPutPosition = "1stPutPosition";
        public const string FirstGetSlotNumber = "1stGetSlotNumber";
        public const string FirstPutSlotNumber = "1stPutSlotNumber";
        public const string FirstSuCIMommand = "1stSuCIMommand";
        public const string FirstGetSlotPosition = "1stGetSlotPosition";
        public const string FirstPutSlotPosition = "1stPutSlotPosition";
        public const string SecondRCMD = "2ndRCMD";
        public const string SecondArmNumber = "2ndArmNumber";
        public const string SecondGetPosition = "2ndGetPosition";
        public const string SecondPutPosition = "2ndPutPosition";
        public const string SecondGetSlotNumber = "2ndGetSlotNumber";
        public const string SecondPutSlotNumber = "2ndPutSlotNumber";
        public const string SecondSuCIMommand = "2ndSuCIMommand";
        public const string SecondGetSlotPosition = "2ndGetSlotPosition";
        public const string SecondPutSlotPosition = "2ndPutSlotPosition";
        public const string ThirdRCMD = "3rdRCMD";
        public const string ThirdArmNumber = "3rdArmNumber";
        public const string ThirdGetPosition = "3rdGetPosition";
        public const string ThirdPutPosition = "3rdPutPosition";
        public const string ThirdGetSlotNumber = "3rdGetSlotNumber";
        public const string ThirdPutSlotNumber = "3rdPutSlotNumber";
        public const string ThirdSuCIMommand = "3rdSuCIMommand";
        public const string ThirdGetSlotPosition = "3rdGetSlotPosition";
        public const string ThirdPutSlotPosition = "3rdPutSlotPosition";
        public const string FourthRCMD = "4thRCMD";
        public const string FourthArmNumber = "4thArmNumber";
        public const string FourthGetPosition = "4thGetPosition";
        public const string FourthPutPosition = "4thPutPosition";
        public const string FourthGetSlotNumber = "4thGetSlotNumber";
        public const string FourthPutSlotNumber = "4thPutSlotNumber";
        public const string FourthSuCIMommand = "4thSuCIMommand";
        public const string FourthGetSlotPosition = "4thGetSlotPosition";
        public const string FourthPutSlotPosition = "4thPutSlotPosition";

        public const string CassetteIDBoxID = "CassetteIDBoxID";
        public const string JobCountforProcess = "JobCountforProcess";

        public const string PortCassetteMapDownloadCommandReturnCode = "PortCassetteMapDownloadCommandReturnCode";
        public const string CassetteControlCommandReturnCode = "CassetteControlCommandReturnCode";
        public const string StartOption = "StartOption";
        public const string CompleteCassetteData = "CompleteCassetteData";

        public const string CV1LotNumber = "CV1LotNumber";
        public const string CV1SlotNumber = "CV1SlotNumber";
        public const string CV2LotNumber = "CV2LotNumber";
        public const string CV2SlotNumber = "CV2SlotNumber";

        public const string MaterialCount = "MaterialCount";
        public const string MaterialQTime = "MaterialQTime";
        public const string MaterialTarget = "MaterialTarget";
        public const string MaterialSource = "MaterialSource";
        public const string MaterrialCarrierID = "MaterrialCarrierID";
        #endregion
        #region matti
        public const string PRODID = "PRODID";
        public const string OperID = "OperID";
        public const string LotID = "LotID";
        public const string PPID1 = "PPID1";
        public const string PPID2 = "PPID2";
        public const string PPID3 = "PPID3";
        public const string PPID4 = "PPID4";
        public const string PPID5 = "PPID5";
        public const string PPID6 = "PPID6";
        public const string PPID7 = "PPID7";
        public const string PPID8 = "PPID8";
        public const string PPID9 = "PPID9";
        public const string PPID10 = "PPID10";
        public const string PPID11 = "PPID11";
        public const string PPID12 = "PPID12";
        public const string PPID13 = "PPID13";
        public const string PPID14 = "PPID14";
        public const string PPID15 = "PPID15";
        public const string PPID16 = "PPID16";
        public const string PPID17 = "PPID17";
        public const string PPID18 = "PPID18";
        public const string PPID19 = "PPID19";
        public const string PPID20 = "PPID20";
        public const string PPID21 = "PPID21";
        public const string PPID22 = "PPID22";
        public const string PPID23 = "PPID23";
        public const string PPID24 = "PPID24";
        public const string PPID25 = "PPID25";
        public const string PPID26 = "PPID26";
        public const string PPID27 = "PPID27";
        public const string PPID28 = "PPID28";
        public const string PPID29 = "PPID29";
        public const string PPID30 = "PPID30";
        public const string JobType = "JobType";
        public const string JobID = "JobID";
        public const string LotSequenceNumber = "LotSequenceNumber";
        public const string SlotSequenceNumber = "SlotSequenceNumber";
        public const string PropertyCode = "PropertyCode";
        public const string JobJudgeCode = "JobJudgeCode";
        public const string JobGradeCode = "JobGradeCode";
        public const string DefectCodeName = "DefectCodeName";
        public const string SubstrateType = "SubstrateType";
        public const string ProcessingFlag1 = "ProcessingFlag1";
        public const string ProcessingFlag2 = "ProcessingFlag2";
        public const string ProcessingFlag3 = "ProcessingFlag3";
        public const string SkipFlag1 = "SkipFlag1";
        public const string SkipFlag2 = "SkipFlag2";
        public const string SkipFlag3 = "SkipFlag3";
        public const string GlassThickness = "GlassThickness";
        public const string JobAngle = "JobAngle";
        public const string JobFlip = "JobFlip";
        public const string MMGCode = "MMGCode";
        public const string PanelInchSizeX = "PanelInchSizeX";
        public const string PanelInchSizeY = "PanelInchSizeY";
        public const string AbnormalFlag = "AbnormalFlag";
        public const string AbnormalFlag1 = "AbnormalFlag1";
        public const string AbnormalFlag2 = "AbnormalFlag2";
        public const string AbnormalFlag3 = "AbnormalFlag3";
        public const string AbnormalFlag4 = "AbnormalFlag4";
        public const string AbnormalFlag5 = "AbnormalFlag5";
        public const string AbnormalFlag6 = "AbnormalFlag6";
        public const string AbnormalFlag7 = "AbnormalFlag7";
        public const string AbnormalFlag8 = "AbnormalFlag8";
        public const string WorkOrderID = "WorkOrderID";

        public const string UpstreamPathNumber = "UpstreamPathNumber";
        public const string DownstreamPathNumber = "DownstreamPathNumber";

        public const string LoginLogoutTimeYear = "LoginLogoutTimeYear";
        public const string LoginLogoutTimeMonth = "LoginLogoutTimeMonth";
        public const string LoginLogoutTimeDay = "LoginLogoutTimeDay";
        public const string LoginLogoutTimeHour = "LoginLogoutTimeHour";
        public const string LoginLogoutTimeMinute = "LoginLogoutTimeMinute";
        public const string LoginLogoutTimeSecond = "LoginLogoutTimeSecond";

        public const string UnitorPort = "UnitorPort";
        public const string UnitNumber = "UnitNumber";
        public const string PortNo = "PortNo";
        public const string SlotNumber = "SlotNumber";
        public const string JobPosition = "JobPosition";
        public const string ReportOption = "ReportOption";
        public const string OperatorID = "OperatorID";
        public const string UnitNumberorPortNo = "UnitNumberorPortNo";
        public const string RequestJobID = "RequestJobID";
        public const string RequestOption = "RequestOption";
        public const string RecipeNumber = "RecipeNumber"; 
        public const string DVSamplingFlag = "DVSamplingFlag";
        public const string RecipeChangeType = "RecipeChangeType";
        public const string JobDataRequestAck = "JobDataRequestAck";
        public const string DateTimeYear = "DateTimeYear";
        public const string DateTimeMonth = "DateTimeMonth";
        public const string DateTimeDay = "DateTimeDay";
        public const string DateTimeHour = "DateTimeHour";
        public const string DateTimeMinute = "DateTimeMinute";
        public const string DateTimeSecond = "DateTimeSecond";
        public const string CIMMessageType = "CIMMessageType";
        public const string CIMMessageID = "CIMMessageID";
        public const string TouchPanelNumber = "TouchPanelNumber";
        public const string TouchPanelNo = "TouchPanelNo";
        public const string CIMMessageData = "CIMMessageData";
        public const string ReturnCode = "ReturnCode";
        public const string ReasonCode = "ReasonCode";
        public const string ReturnCodeText = "ReturnCodeText";
        public const string CVReportEnableMode = "CVReportEnableMode";
        public const string CycleType = "CycleType";
        public const string CVReportFrequencyMinute = "CVReportFrequencyMinute";
        public const string CVReportHour1 = "CVReportHour1";
        public const string CVReportMinute1 = "CVReportMinute1";
        public const string CVReportHour2 = "CVReportHour2";
        public const string CVReportMinute2 = "CVReportMinute2";
        public const string CVReportHour3 = "CVReportHour3";
        public const string CVReportMinute3 = "CVReportMinute3";
        public const string CVCommandReturnCode = "CVCommandReturnCode";
        public const string AlarmID = "AlarmID";
        public const string MachineStatus = "MachineStatus";
        public const string MachinestatusReasonCode = "MachinestatusReasonCode";
        public const string CIMMode = "CIMMode";
        public const string MaterialStatus = "MaterialStatus"; 
        public const string MaterialTypeOperationProportion = "MaterialTypeOperationProportion";
        public const string MaterialID = "MaterialID";
        public const string MaterialType = "MaterialType";
        public const string MaterialUseCount = "MaterialUseCount";
        public const string UnloadingCode = "UnloadingCode";
        public const string VCRNumber = "VCRNumber";
        public const string VCRStatus = "VCRStatus";
        public const string PortType = "PortType";
        public const string PortTypeReturnCode = "PortTypeReturnCode";
        public const string PortTypeAutoChangeMode = "PortTypeAutoChangeMode";
        public const string PortTypeAutoChangeModeReturnCode = "PortTypeAutoChangeModeReturnCode";
        public const string PortMode = "PortMode";
        public const string PortModeReturnCode = "PortModeReturnCode";
        public const string PortEnableMode = "PortEnableMode";
        public const string TransferMode = "TransferMode";
        public const string EnableModeReturnCode = "EnableModeReturnCode";
        public const string TransferModeReturnCode = "TransferModeReturnCode";
        public const string PortPauseMode = "PortPauseMode";
        public const string PauseModeReturnCode = "PauseModeReturnCode";
        public const string PortControlCommandReturnCode = "PortControlCommandReturnCode";
        public const string PortGrade = "PortGrade";
        public const string PortGradeReturnCode = "PortGradeReturnCode";
        public const string PortCassetteType = "PortCassetteType";
        public const string PortCassetteTypeReturnCode = "PortCassetteTypeReturnCode";
        public const string CSTOperationMode = "CSTOperationMode";
        public const string PortQTimeReturnCode = "PortQTimeReturnCode";
        public const string MachineMode = "MachineMode";
        public const string MachineModeChangeReturnCode = "MachineModeChangeReturnCode";
        public const string BatchQty = "BatchQty";
        public const string SamplingCode = "SamplingCode";
        public const string ScriberCount = "ScriberCount";
        public const string PalletStatus = "PalletStatus";
        public const string PalletType = "PalletType";
        public const string OCID = "OCID";
        public const string ModelType = "ModelType";
        public const string Version = "Version";
        public const string DateTime = "DateTime";
        public const string PortCassetteTypeToPortCapacity = "PortCassetteTypeToPortCapacity";

        #endregion

        #region Old
        public const string PortCassetteTypeChangeMode = "PortCassetteTypeChangeMode";
        public const string CVDGlassFlag = "CVDGlassFlag";
        public const string ForceCleanOutMode = "ForceCleanOutMode";
        public const string LocationNo = "LocationNo";
        public const string DownloadSequenceNo = "DownloadSequenceNo";
        public const string Location = "Location";
        public const string PIStatus = "PIStatus";
        public const string DismountLocation = "DismountLocation";
        public const string MountLocation = "MountLocation";
        public const string PIWeight = "PIWeight";
        public const string PILotID = "PILotID";
        public const string RemainedGlassFlag = "RemainedGlassFlag";
        public const string LOTIDC = "LOTIDC";
        public const string ThicknessC = "ThicknessC";
        public const string RecipeAuthenticationDownloadCommandReturnCode = "RecipeAuthenticationDownloadCommandReturnCode";
        public const string NGRecipeParameterName = "NGRecipeParameterName";
        public const string NGRecipeParameterRMSValue = "NGRecipeParameterRMSValue";
        public const string NGRecipeParameterEQPValue = "NGRecipeParameterEQPValue";
        public const string DVDataN = "DVDataN";
        public const string PortQTimeCommand = "PortQTimeCommand";
        public const string JobCountInCassette = "JobCountInCassette";
        public const string SendLocalRecipe = "SendLocalRecipe";
        //public const string PPID11 = "PPID11";
        public const string JobJudgeResult = "JobJudgeResult";
        public const string JobGradeResult = "JobGradeResult";
        public const string PermissionLevel = "PermissionLevel";
        public const string SlotToProcess = "SlotToProcess";
        public const string CassetteOperationMode = "CassetteOperationMode";
        public const string IndexerOperationModeChangeReturnCode = "IndexerOperationModeChangeReturnCode";
        public const string EventID = "EventID";
        public const string CurrentRecipeNo = "CurrentRecipeNo";
        public const string ReceivedUpstreamPathNo = "ReceivedUpstreamPathNo";
        public const string UpstreamPathNo = "UpstreamPathNo";
        public const string ProcessDataItems = "ProcessDataItems";
        public const string ProcessStartTime = "ProcessStartTime";
        public const string ProcessEndTime = "ProcessEndTime";
        public const string ScrapReasonCode = "ScrapReasonCode";
        public const string NGJudgeReasonCode = "NGJudgeReasonCode";
        public const string ReportType = "ReportType";
        public const string Unit = "Unit";
        public const string LocalRecipeID = "LocalRecipeID";
        public const string JobCountinCassette = "JobCountinCassette";
        public const string CompletedCassetteData = "CompletedCassetteData";
        public const string Position1CassetteSequenceNo = "Position1CassetteSequenceNo";
        public const string Position1JobSequenceNo = "Position1JobSequenceNo";
        public const string Position1GlassExist = "Position1GlassExist";
        public const string EquipmentOperationMode = "EquipmentOperationMode";
        public const string UsedJobBlockNo = "UsedJobBlockNo";
        public const string DownstreamPathNo = "DownstreamPathNo";
        //public const string RequestOption = "RequestOption";
        public const string ReceivedJobCount = "ReceivedJobCount";
        public const string PositionType = "PositionType";


        public const string JobTypeOption = "JobTypeOption";





        public const string AlarmType = "AlarmType";
        public const string Millisecond = "Millisecond";
        public const string RecipeParameterValueN = "RecipeParameterValueN";
        public const string UnitCount = "UnitCount";
        public const string SlotSequenceNoOfTheGlassOnThePosition = "SlotSequenceNoOfTheGlassOnThePosition";
        public const string CassetteSequenceNoOfTheGlassOnThePosition = "CassetteSequenceNoOfTheGlassOnThePosition";
        public const string PortTypeChangeReturnCode = "PortTypeChangeReturnCode";
        public const string SubUnitStatus = "SubUnitStatus";
        //public const string PortType = "PortType";
        public const string EquipmentMode = "EquipmentMode";
        //public const string PortTypeAutoChangeModeReturnCode = "PortTypeAutoChangeModeReturnCode";
        //public const string PortTypeAutoChangeMode = "PortTypeAutoChangeMode";
        public const string PortTransferModeChangeReturnCode = "PortTransferModeChangeReturnCode";
        public const string PortTransferMode = "PortTransferMode";
        public const string PortQTimeSettingTime = "PortQTimeSettingTime";
        public const string PortQTime = "PortQTime";
        public const string PortModeChangeReturnCode = "PortModeChangeReturnCode";
        //public const string PortMode = "PortMode";
        public const string PortEnableModeChangeReturnCode = "PortEnableModeChangeReturnCode";
        //public const string PortEnableMode = "PortEnableMode";
        public const string PortCassetteTypeChangeReturnCode = "PortCassetteTypeChangeReturnCode";
        //public const string PortCassetteType = "PortCassetteType";
        public const string PortStatus = "PortStatus";

        public const string JobExistenceSlot = "JobExistenceSlot";
        public const string JobExistenceSlot1 = "JobExistenceSlot1";
        public const string JobExistenceSlot2 = "JobExistenceSlot2";
        public const string JobExistenceSlot3 = "JobExistenceSlot3";
        public const string JobExistenceSlot4 = "JobExistenceSlot4";
        public const string JobExistenceSlot5 = "JobExistenceSlot5";
        public const string JobExistenceSlot6 = "JobExistenceSlot6";
        public const string JobExistenceSlot7 = "JobExistenceSlot7";
        public const string JobExistenceSlot8 = "JobExistenceSlot8";
        public const string JobExistenceSlot9 = "JobExistenceSlot9";
        public const string JobExistenceSlot10 = "JobExistenceSlot10";
        public const string JobExistenceSlot11 = "JobExistenceSlot11";
        public const string JobExistenceSlot12 = "JobExistenceSlot12";
        public const string JobExistenceSlot13 = "JobExistenceSlot13";
        public const string JobExistenceSlot14 = "JobExistenceSlot14";
        public const string JobExistenceSlot15 = "JobExistenceSlot15";
        public const string LoadingCassetteType = "LoadingCassetteType";
        public const string QTimeFlag = "QTimeFlag";
        public const string CassetteMappingState = "CassetteMappingState";
        public const string CassetteStatus = "CassetteStatus";
        public const string LogStatus = "LogStatus";
        public const string OperatorPassword = "OperatorPassword";
        public const string CancelorAbort = "CancelorAbort";
        public const string TransferPauseTarget = "TransferPauseTarget";

        public const string LoadingStopRequestReturn = "LoadingStopRequestReturn";
        public const string LoadingStopReleaseReturn = "LoadingStopReleaseReturn";
        public const string BCAlive = "BCAlive";
        //public const string AlarmID = "AlarmID";
        public const string AlarmStatus = "AlarmStatus";
        public const string AlarmLevel = "AlarmLevel";
        public const string AlarmState = "AlarmState";
        public const string AlarmUnit = "AlarmUnit";
        public const string AlarmCode = "AlarmCode";
        public const string AlarmUnitNumber = "AlarmUnitNumber";
        public const string AlarmText = "AlarmText";

        public const string HalfGlassID1 = "HalfGlassID1";
        public const string HalfGlassID2 = "HalfGlassID2";

        public const string QuarterGlassID1 = "QuarterGlassID1";
        public const string QuarterGlassID2 = "QuarterGlassID2";
        public const string QuarterGlassID3 = "QuarterGlassID3";
        public const string QuarterGlassID4 = "QuarterGlassID4";

        public const string CIMMessage = "CIMMessage";
        //public const string CIMMessageID = "CIMMessageID";
        //public const string TouchPanelNo = "TouchPanelNo";

        public const string Year = "Year";
        public const string Month = "Month";
        public const string Day = "Day";
        public const string Hour = "Hour";
        public const string Minute = "Minute";
        public const string Second = "Second";

        public const string VCRNo = "VCRNo";
        public const string VCREnableMode = "VCREnableMode";
        public const string VCRReadFailOperationMode = "VCRReadFailOperationMode";
        public const string UnitMode = "UnitMode";
        public const string RecipeNo = "RecipeNo";
        public const string RecipeVersion = "RecipeVersion";
        public const string ParameterCount = "ParameterCount";
        public const string CommandFlag = "CommandFlag";
        //public const string CIMMode = "CIMMode";

        public const string PortTypeChange = "PortTypeChange";
        public const string PortModeChange = "PortModeChange";
        public const string PortTransferModeChange = "PortTransferModeChange";
        public const string PortOperationMode = "PortOperationMode";
        public const string PortTypeAutoChangeModeCommand = "PortTypeAutoChangeModeCommand";
        public const string PortCassetteTypeChangeModeCommand = "PortCassetteTypeChangeModeCommand";
        public const string UnloadSortingkey = "UnloadSortingkey";
        public const string PortControlCommand = "PortControlCommand";
        public const string JobExistence = "JobExistence";
        public const string JobCount = "JobCount";
        public const string CassetteControlCommandCode = "CassetteControlCommandCode";

        public const string SequenceNo = "SequenceNo";
        public const string stRCMD1 = "stRCMD1";
        public const string stArmNo1 = "stArmNo1";
        public const string stGetPosition1 = "stGetPosition1";
        public const string stPutPosition1 = "stPutPosition1";
        public const string stGetSlotNo1 = "stGetSlotNo1";
        public const string stPutSlotNo1 = "stPutSlotNo1";
        public const string stSubCommand1 = "stSubCommand1";
        public const string stGetSlotPostion1 = "stGetSlotPostion1";
        public const string stPutSlotPostion1 = "stPutSlotPostion1";
        //int ndRCMD2,int ndArmNo2,int ndGetPosition2,int ndPutPosition2,int ndGetSlotNo2,
        //int ndPutSlotNo2,int ndSubCommand2,int ndGetSlotPostion2,int ndPutSlotPostion2,
        public const string ndRCMD2 = "ndRCMD2";
        public const string ndArmNo2 = "ndArmNo2";
        public const string ndGetPosition2 = "ndGetPosition2";
        public const string ndPutPosition2 = "ndPutPosition2";
        public const string ndGetSlotNo2 = "ndGetSlotNo2";
        public const string ndPutSlotNo2 = "ndPutSlotNo2";
        public const string ndSubCommand2 = "ndSubCommand2";
        public const string ndGetSlotPostion2 = "ndGetSlotPostion2";
        public const string ndPutSlotPostion2 = "ndPutSlotPostion2";
        //int rdRCMD3,int rdArmNo3, int rdGetPosition3, int rdPutPosition3, int rdGetSlotNo3, 
        //int rdPutSlotNo3, int rdSubCommand3, int rdGetSlotPostion3, int rdPutSlotPostion3,
        public const string rdRCMD3 = "rdRCMD3";
        public const string rdArmNo3 = "rdArmNo3";
        public const string rdGetPosition3 = "rdGetPosition3";
        public const string rdPutPosition3 = "rdPutPosition3";
        public const string rdGetSlotNo3 = "rdGetSlotNo3";
        public const string rdPutSlotNo3 = "rdPutSlotNo3";
        public const string rdSubCommand3 = "rdSubCommand3";
        public const string rdGetSlotPostion3 = "rdGetSlotPostion3";
        public const string rdPutSlotPostion3 = "rdPutSlotPostion3";
        //int thRCMD4, int thArmNo4, int thGetPosition4, int thPutPosition4, int thGetSlotNo4, 
        //int thPutSlotNo4, int thSubCommand4, int thGetSlotPostion4, int thPutSlotPostion4)
        public const string thRCMD4 = "thRCMD4";
        public const string thArmNo4 = "thArmNo4";
        public const string thGetPosition4 = "thGetPosition4";
        public const string thPutPosition4 = "thPutPosition4";
        public const string thGetSlotNo4 = "thGetSlotNo4";
        public const string thPutSlotNo4 = "thPutSlotNo4";
        public const string thSubCommand4 = "thSubCommand4";
        public const string thGetSlotPostion4 = "thGetSlotPostion4";
        public const string thPutSlotPostion4 = "thPutSlotPostion4";










        public const string VCRType = "VCRType";
        public const string GlassQTY = "GlassQTY";

        public const string GlassSelectMap = "GlassSelectMap";
        public const string Serial = "Serial";

        public const string ReadGlassID = "ReadGlassID";
        public const string HostGlassID = "HostGlassID";
        public const string CellID = "CellID";
        public const string RemovedFlag = "RemovedFlag";
        //public const string DateTimeYear = "DateTimeYear";
        //public const string DateTimeMonth = "DateTimeMonth";
        //public const string DateTimeDay = "DateTimeDay";
        //public const string DateTimeHour = "DateTimeHour";
        //public const string DateTimeMinute = "DateTimeMinute";
        //public const string DateTimeSecond = "DateTimeSecond";

        public const string VCRMode = "VCRMode";
        public const string NewBoxID = "NewBoxID";
        public const string EmptyBoxID = "EmptyBoxID";
        public const string NewPalletID = "NewPalletID";
        public const string EQPID = "EQPID";
        public const string EQPCommand = "EQPCommand";
        public const string EQPStatus = "EQPStatus";
        public const string RunningMode = "RunningMode";

        public const string FGCode = "FGCode";
        public const string RevisionCode = "RevisionCode";

        public const string SamplingFlag = "SamplingFlag";

        //public const string MaterialType = "MaterialType";
        public const string MaterialQty = "MaterialQty";
        public const string MaterialCurrentQty = "MaterialCurrentQty";
        public const string PortNumber = "PortNumber";
        //public const string MaterialStatus = "MaterialStatus";

        //public const string OperatorID = "OperatorID";
        public const string Password = "Password";
        public const string Action = "Action";
        public const string UserID = "UserID";
        public const string UnitID = "UnitID";
        public const string TraceID = "TraceID";
        public const string DataSamplePeriod = "DataSamplePeriod";
        public const string TotalSamples = "TotalSamples";

        public const string PortUseType = "PortUseType";
        public const string PanelID = "PanelID";
        public const string VCRReadPanelID = "VCRReadPanelID";
        public const string ModuleID = "ModuleID";
        public const string PanelGrade = "PanelGrade";
        public const string UnitPathNo = "UnitPathNo";
        public const string SendOutJobCount = "SendOutJobCount";
        public const string PTID = "PTID";
        public const string QTY = "QTY";
        public const string SLOTSEL = "SLOTSEL";
        public const string SLOTNO = "SLOTNO";
        public const string PNLID = "PNLID";
        public const string LOTID = "LOTID";
        public const string LOTJUDGE = "LOTJUDGE";
        public const string LOTSORTTYPE = "LOTSORTTYPE";
        public const string OPERID = "OPERID";
        //public const string PRODID = "PRODID";
        public const string SMPLFLAG = "SMPLFLAG";
        public const string RWKCNT = "RWKCNT";
        public const string CFGLSID = "CFGLSID";
        public const string LOTTYPE = "LOTTYPE";
        public const string SPNLID = "SPNLID";
        public const string PNLJUDGE = "PNLJUDGE";
        public const string PNLGRADE = "PNLGRADE";
        public const string PNLSORTTYPE = "PNLSORTTYPE";
        public const string ATPNLGRADE = "ATPNLGRADE";
        public const string ASSYPNLGRADE = "ASSYPNLGRADE";
        public const string OSREPAIR = "OSREPAIR";
        public const string ATREPAIR = "ATREPAIR";
        public const string JobSequenceNo = "JobSequenceNo";

        public const string LabelType = "LabelType";
        public const string BoxID = "BoxID";
        public const string BoxQTY = "BoxQTY";
        public const string ReserveQTY = "ReserveQTY";
        public const string Grade = "Grade";
        public const string PalletID = "PalletID";
        public const string DEType = "DEType";
        public const string Weight = "Weight";
        public const string TCONID = "TCONID";
        public const string NGType = "NGType";
        public const string SkipMode = "SkipMode";
        public const string OQAResult = "OQAResult";
        public const string PanelQTY = "PanelQTY";
        public const string SlotSelect = "SlotSelect";
        public const string PanelID1 = "PanelID1";
        public const string PanelIDs = "PanelIDs";
        public const string PanelID30 = "PanelID30";
        public const string ReadPanelID = "ReadPanelID";
        public const string VCRResult = "VCRResult";
        //public const string SubstrateType = "SubstrateType";
        public const string Flag = "Flag";
        public const string ValidationResult = "ValidationResult";
        //public const string MaterialID = "MaterialID";
        public const string MaterialPosition = "MaterialPosition";
        public const string MaterialPartID = "MaterialPartID";
        public const string MaterialLotID = "MaterialLotID";
        public const string MaterialVersion = "MaterialVersion";

        public const string TransferDelayTarget = "TransferDelayTarget";
        public const string ColdRunCount = "ColdRunCount";
        public const string IndexerOperationMode = "IndexerOperationMode";
        public const string SetLastGlassCommandReturnCode = "SetLastGlassCommandReturnCode";
        public const string CurrentGroupCount = "CurrentGroupCount";
        public const string TotalGroupCount = "TotalGroupCount";
        public const string TotalDVItemListCount = "TotalDVItemListCount";
        public const string TouchPanleNo = "TouchPanleNo";
        public const string DownRate = "DownRate";
        public const string IdleRate = "IdleRate";
        public const string RunRate = "RunRate";
        public const string EquipmentReasonCode = "EquipmentReasonCode";
        public const string RequestJobJobSequenceNo = "RequestJobJobSequenceNo";
        public const string RequestJobCassetteSequenceNo = "RequestJobCassetteSequenceNo";
        //public const string RequestJobID = "RequestJobID";
        public const string JobDataRequestOption = "JobDataRequestOption";
        public const string FirstGlassFlag = "FirstGlassFlag";

        //public const string JobDataRequestAck = "JobDataRequestAck";

        public const string ProcessReasonCode = "ProcessReasonCode";
        public const string ProcessFlag = "ProcessFlag";
        public const string OvenSlotNumberInformation = "OvenSlotNumberInformation";
        public const string SlotNumberInformation = "SlotNumberInformation";
        //public const string JobType = "JobType";
        public const string ProductType = "ProductType";
        public const string JobJudge = "JobJudge";
        public const string JobGrade = "JobGrade";
        public const string PPID = "PPID";
        public const string PunchID = "PunchID";
        public const string ChangeType = "ChangeType";
        public const string ChangeID = "ChangeID";
        public const string TestAJudgeData = "INSPJUDGEDATA1TestA";
        public const string AOIJudgeData = "INSPJUDGEDATA1AOI";
        //public const string UnitorPort = "UnitorPort";
        public const string UnitNo = "UnitNo";
        //public const string PortNo = "PortNo";
        public const string SelectedPanelInSlot = "SelectedPanelInSlot";
        public const string PortID = "PortID";
        public const string SlotNo = "SlotNo";
        public const string SubUnitSlotNo = "SubUnitSlotNo";
        public const string SlotID = "SlotID";
        public const string SlotMap = "SlotMap";
        public const string CassetteID = "CassetteID";
        public const string CarrierID = "CarrierID";
        public const string CarrierControl = "CarrierControl";
        public const string CarrierStatus = "CarrierStatus";
        public const string CarrierType = "CarrierType";
        public const string SubCassetteID = "SubCassetteID";

        public const string PNLJudge = "PNLJudge";
        public const string PNLGrade = "PNLGrade";
        public const string PassWord = "PassWord";
        public const string LotFlag = "LotFlag";
        public const string RecipeID = "RecipeID";
        public const string JobSequenceNumber = "JobSequenceNumber";

        public const string JobDataRequestReturnCode = "JobDataRequestReturnCode";

        public const string Count = "Count";
        public const string UnitStatus = "UnitStatus";
        public const string RTCode = "RTCode";
        public const string Time = "Time";
        public const string PanelQty = "PanelQty";
        public const string PRODUCTSPECFLAG = "PRODUCTSPECFLAG";
        public const string FGCODE = "FGCODE";
        public const string REVISIONCODE = "REVISIONCODE";
        public const string GlassDegree = "GlassDegree";
        public const string InspectionJudgeData = "InspectionJudgeData";
        public const string UnitNoorPortNo = "UnitNoorPortNo";
        public const string SubUnitNo = "SubUnitNo";

        public const string OperationWIPCount = "OperationWIPCount";
        public const string CurrentWIPCount = "CurrentWIPCount";
        public const string Mode = "Mode";
        public const string AOIInspectionFlag = "AOIInspectionFlag";
        public const string ReprocessCount = "ReprocessCount";
        public const string ReprocessFlag = "ReprocessFlag";




        public const string ProductID = "ProductID";
        public const string OperationID = "OperationID";
        //public const string LotID = "LotID";
        public const string LotJudge = "LotJudge";
        public const string LotSortingType = "LotSortingType";
        //public const string PPID1 = "PPID1";
        //public const string PPID2 = "PPID2";
        //public const string PPID3 = "PPID3";
        //public const string PPID4 = "PPID4";
        //public const string PPID5 = "PPID5";
        //public const string PPID6 = "PPID6";
        //public const string PPID7 = "PPID7";
        //public const string PPID8 = "PPID8";
        //public const string PPID9 = "PPID9";
        //public const string PPID10 = "PPID10";
        //public const string PPID11 = "PPID11";
        public const string PPPID11 = "PPPID11";
        //public const string PPID12 = "PPID12";
        //public const string PPID13 = "PPID13";
        //public const string PPID14 = "PPID14";
        //public const string PPID15 = "PPID15";
        //public const string PPID16 = "PPID16";
        //public const string PPID17 = "PPID17";
        //public const string PPID18 = "PPID18";
        //public const string PPID19 = "PPID19";
        //public const string PPID20 = "PPID20";
        public const string GlassID = "GlassID";
        public const string CassetteSequenceNo = "CassetteSequenceNo";
        public const string SlotSequenceNo = "SlotSequenceNo";
        public const string SlotPosition = "SlotPosition";
        public const string GlassJudge = "GlassJudge";
        public const string GlassSortType = "GlassSortType";
        public const string SampleFlag = "SampleFlag";
        public const string ReworkCount = "ReworkCount";
        public const string PairGlassID = "PairGlassID";
        public const string Thicknessq = "Thicknessq";
        public const string ESFLAG = "ESFLAG";
        public const string WorkOrder = "WorkOrder";
        public const string LastGlassFlag = "LastGlassFlag";
        public const string GlassAngle = "GlassAngle";
        public const string JobRecoveryFlag = "JobRecoveryFlag";


        public const string ProductIDC = "ProductIDC";
        public const string OperationIDC = "OperationIDC";
        public const string LotIDC = "LotIDC";
        public const string LotJudgeC = "LotJudgeC";
        public const string LotSortingTypeC = "LotSortingTypeC";
        public const string PPID1C = "PPID1C";
        public const string PPID2C = "PPID2C";
        public const string PPID3C = "PPID3C";
        public const string PPID4C = "PPID4C";
        public const string PPID5C = "PPID5C";
        public const string PPID6C = "PPID6C";
        public const string PPID7C = "PPID7C";
        public const string PPID8C = "PPID8C";
        public const string PPID9C = "PPID9C";
        public const string PPID10C = "PPID10C";
        public const string PPID11C = "PPID11C";
        public const string GlassIDC = "GlassIDC";
        public const string CassetteSequenceNoC = "CassetteSequenceNoC";
        public const string SlotSequenceNoC = "SlotSequenceNoC";
        public const string SlotPositionC = "SlotPositionC";
        public const string GlassJudgeC = "GlassJudgeC";
        public const string GlassSortTypeC = "GlassSortTypeC";
        public const string SampleFlagC = "SampleFlagC";
        public const string ReworkCountC = "ReworkCountC";
        public const string PairGlassIDC = "PairGlassIDC";
        public const string ThicknessqC = "ThicknessqC";
        public const string ESFLAGC = "ESFLAGC";
        public const string WorkOrderC = "WorkOrderC";
        public const string LastGlassFlagC = "LastGlassFlagC";
        public const string GlassAngleC = "GlassAngleC";
        public const string JobRecoveryFlagC = "JobRecoveryFlagC";



        //public const string UnitPathNo = "UnitPathNo";

        //CuttingSequenceNo  GlassJudgeCode  GlassGradeCode  ProcessingFlag
        public const string CuttingSequenceNo = "CuttingSequenceNo";
        public const string GlassJudgeCode = "GlassJudgeCode";
        public const string GlassGradeCode = "GlassGradeCode";
        public const string ProcessingFlag = "ProcessingFlag";

        //GlassSizeCode   GlassThicknessCode  GlassType  LOTCode  ProcessingCount
        public const string GlassSizeCode = "GlassSizeCode";
        public const string GlassThicknessCode = "GlassThicknessCode";
        public const string GlassType = "GlassType";
        public const string LOTCode = "LOTCode";
        public const string ProcessingCount = "ProcessingCount";
        //InspectionFlag  SkipFlag  InlineEQData
        public const string InspectionFlag = "InspectionFlag";
        public const string SkipFlag = "SkipFlag";
        public const string InlineEQData = "InlineEQData";

        public const string Vender = "Vender";
        public const string GroupID = "GroupID";
        public const string PanelSpecID = "PanelSpecID";
        public const string CellInfo = "CellInfo";


        public const string VCREnable = "VCREnable";
        public const string BCREnable = "BCREnable";

        public const string CassetteControl = "CassetteControl";
        public const string CSTStatus = "CassetteStatus";
        public const string CSTID = "CSTID";
        public const string CompleteCode = "CompleteCode";
        public const string CassetteRTCode = "CassetteRTCode";

        public const string Parmeter = "Parmeter";
        public const string RecipeStatus = "RecipeStatus";

        public const string CreateDate = "CreateDate";

        public const string SoucePlace = "SoucePlace";
        public const string TargetPlace = "TargetPlace";
        public const string QuarterCutterRecipeID = "QuarterCutterRecipeID";
        public const string QuarterGrinderRecipeID = "QuarterGrinderRecipeID";
        public const string HalfCutterRecipeID = "HalfCutterRecipeID";
        public const string HalfGrinderRecipeID = "HalfGrinderRecipeID";
        public const string UnpackGrinderRecipeID = "UnpackGrinderRecipeID";
        public const string CleanerRecipeID = "CleanerRecipeID";
        public const string HalfGrinderInspectionJudge = "HalfGrinderInspectionJudge";
        public const string UPKGrinderInspectionJudge = "UPKGrinderInspectionJudge";

        public const string CallOperatorID = "CallOperatorID";
        public const string RepairOperatorID = "RepairOperatorID";
        public const string EndOperatorID = "EndOperatorID";
        public const string CallStartTime = "CallStartTime";
        public const string RepairStartTime = "RepairStartTime";
        public const string EndTime = "EndTime";
        public const string ApprovalID = "ApprovalID";
        public const string ApprovalTime = "ApprovalTime";

        public const string ShuttleID = "ShuttleID";
        public const string CommandID = "CommandID";
        public const string TransferStatus = "TransferStatus";
        public const string CarrierLocation = "CarrierLocation";
        public const string CarrierDestination = "CarrierDestination";
        public const string ShuttleLocation = "ShuttleLocation";
        public const string ShuttleStatus = "ShuttleStatus";
        public const string ResultCode = "ResultCode";
        public const string ReadResult = "ReadResult";
        public const string CarrierSourceFloor = "CarrierSourceFloor";
        public const string CarrierDestFloor = "CarrierDestFloor";

        public const string CommandMotion = "CommandMotion";
        public const string CommandStageType = "CommandStageType";
        public const string CommandStage = "CommandStage";
        public const string CommandSlot = "CommandSlot";
        public const string PanelThickness = "PanelThickness";
        //public const string GlassThickness = "GlassThickness";
        public const string CommandHand = "CommandHand";

        public const string LowerCassetteIndex = "LowerCassetteIndex";
        public const string LowerSubstrateIndex = "LowerSubstrateIndex";
        public const string LowerLotId = "LowerLotId";
        public const string UpperCassetteIndex = "UpperCassetteIndex";
        public const string UpperSubstrateIndex = "UpperSubstrateIndex";
        public const string UpperLotId = "UpperLotId";
        public const string SubLowerCassetteIndex = "SubLowerCassetteIndex";
        public const string SubLowerSubstrateIndex = "SubLowerSubstrateIndex";

        public const string SubLowerLotId = "SubLowerLotId";
        public const string SubLowerSubstrateId = "SubLowerSubstrateId";
        public const string SubUpperCassetteIndex = "SubUpperCassetteIndex";
        public const string SubUpperSubstrateIndex = "SubUpperSubstrateIndex";
        public const string SubUpperLotId = "SubUpperLotId";
        public const string SubUpperSubstrateId = "SubUpperSubstrateId";
        public const string SubControlCommandResultCode = "SubControlCommandResultCode";

        public const string SubCommandMotion = "SubCommandMotion";
        public const string SubCommandStageType = "SubCommandStageType";
        public const string SubCommandStage = "SubCommandStage";
        public const string SubCommandSlot = "SubCommandSlot";
        public const string SubCommandHand = "SubCommandHand";
        //public const string ReturnCode = "ReturnCode";
        public const string MessageSequenceNo = "MessageSequenceNo";
        public const string CommandResult = "Result";
        public const string Thickness = "Thickness";
        public const string CST_GEN = "CST_GEN";
        public const string RobotStatus = "RobotStatus";

        public const string LowerHandPanelID = "LowerHandPanelID";
        public const string UpperHandPanelID = "UpperHandPanelID";
        public const string ControlCommandResultCode = "ControlCommandResultCode";
        public const string PanelJudge = "PanelJudge";
        #endregion
    }
    public class ESFLAG
    {
        public const string D = "D";
        public const string E = "E";
    }

    public class PLCCommandName
    {
        #region Yuan
        public const string RobotControlCommandBlock = "RobotControlCommandBlock";
        public const string RobotControlCommand1SubBlock = "RobotControlCommand#1SubBlock";
        public const string RobotControlCommand2SubBlock = "RobotControlCommand#2SubBlock";
        public const string RobotControlCommand3SubBlock = "RobotControlCommand#3SubBlock";
        public const string RobotControlCommand4SubBlock = "RobotControlCommand#4SubBlock";
        #endregion
        public const string CVDGlassFlagBlock = "CVDGlassFlagBlock";
        public const string ForceCleanOutCommandBlock = "ForceCleanOutCommandBlock";
        public const string RemainedGlassFlagBlock = "RemainedGlassFlagBlock";

        public const string RecipeParameterCompareNGNotifyBlock = "RecipeParameterCompareNGNotifyBlock";
        public const string RecipeAuthenticationDownloadCommandBlock = "RecipeAuthenticationDownloadCommandBlock";
        public const string RecipeChangeReportReplyBlock = "RecipeChangeReportReplyBlock";
        public const string CancelAbortRequestReplyBlock = "CancelAbortRequestReplyBlock";
        public const string LocalRecipeRequestReplyBlock = "LocalRecipeRequestReplyBlock";
        public const string ProcessDataReportReplyBlock = "ProcessDataReportReplyBlock";
        public const string EquipmentOperationModeChangeCommandBlock = "EquipmentOperationModeChangeCommandBlock";
        public const string OPLogInReportReplyBlock = "OPLogInReportReplyBlock";
        public const string PIMountRequestReplyBlock = "PIMountRequestReplyBlock";
        public const string PIPreMountRequestReplyBlock = "PIPreMountRequestReplyBlock";
        public const string BCEventReply = "BCEventReply";
        public const string IndexerOperationModeChangeCommand = "IndexerOperationModeChangeCommand";
        public const string CIMMessageSet = "CIMMessageSet";
        public const string BoxInformationRequestReply = "BoxInformationRequestReply";
        public const string EquipmentOperationModeChangeCommand = "EquipmentOperationModeChangeCommand";
        public const string LabelInformationDownload = "LabelInformationDownload";

        public const string BoxLabelInformationDownload = "BoxLabelInformationDownload";


        public const string UserPermissionValidationRequestReply = "UserPermissionValidationRequestReply";
        public const string MaterialValidationRequestReply = "MaterialValidationRequestReply";
        public const string SendOutPanelResult = "SendOutPanelResult";

        public const string RecipeDataDownload = "RecipeDataDownload";
        public const string DateTimeDownloadCommand = "DateTimeDownloadCommand";

        public const string PanelLabelInformationRequestReply = "PanelLabelInformationRequestReply";
        public const string PanelInformationRequestReply = "PanelInformationRequestReply";
        public const string PanelInformationRequestReply1 = "PanelInformationRequestReply1";
        public const string PanelInformationRequestReply2 = "PanelInformationRequestReply2";
        public const string PanelInformationRequestReply3 = "PanelInformationRequestReply3";

        public const string BackLightInformationRequestReply = "BackLightInformationRequestReply";
        public const string PalletInformationRequestReply = "PalletInformationRequestReply";
        public const string PanelModuleIDRequestReply = "PanelModuleIDRequestReply";
        public const string OEMPrintBoxIDCommand = "OEMPrintBoxIDCommand";

        public const string CarrierControlCommand1 = "Carrier1ControlCommand";
        public const string CarrierControlCommand2 = "Carrier2ControlCommand";
        public const string CarrierControlCommand3 = "Carrier3ControlCommand";
        public const string CarrierControlCommand4 = "Carrier4ControlCommand";
        public const string CarrierControlCommand5 = "Carrier5ControlCommand";

        public const string CarrierInformationCommand1 = "Carrier1InformationCommand";
        public const string CarrierInformationCommand2 = "Carrier2InformationCommand";
        public const string CarrierInformationCommand3 = "Carrier3InformationCommand";
        public const string CarrierInformationCommand4 = "Carrier4InformationCommand";
        public const string CarrierInformationCommand5 = "Carrier5InformationCommand";

        public const string MaterialChangeResult = "MaterialChangeResult";
        public const string MaterialChangeResult1 = "MaterialChangeResult1";
        public const string MaterialChangeResult2 = "MaterialChangeResult2";

        public const string MaterialValidationResult = "MaterialValidationResult";

        public const string MaterialValidationResult1 = "Material1ValidationResult";
        public const string MaterialValidationResult2 = "Material2ValidationResult";


        public const string ProcessDataDownload = "ProcessDataDownload";

        public const string OperatorLoginResult = "OperatorLoginResult";

        public const string CassetteControlCommand = "CassetteControlCommand";
        public const string SelectedPanelMapDownload = "SelectedPanelMapDownload";

        public const string FGCodeChangeDownload = "FGCodeChangeDownload";
        public const string GlassCuttingDownload = "GlassCuttingDownload";

        //public const string CassetteControlCommand = "CassetteControlCommand";

        public const string Cassette1ControlCommandBlock = "Cassette1ControlCommandBlock";
        public const string Cassette2ControlCommandBlock = "Cassette2ControlCommandBlock";
        public const string Cassette3ControlCommandBlock = "Cassette3ControlCommandBlock";
        public const string Cassette4ControlCommandBlock = "Cassette4ControlCommandBlock";
        public const string Cassette5ControlCommandBlock = "Cassette5ControlCommandBlock";
        public const string Cassette6ControlCommandBlock = "Cassette6ControlCommandBlock";
        public const string Cassette7ControlCommandBlock = "Cassette7ControlCommandBlock";
        public const string Cassette8ControlCommandBlock = "Cassette8ControlCommandBlock";
        public const string Cassette9ControlCommandBlock = "Cassette9ControlCommandBlock";
        public const string Cassette10ControlCommandBlock = "Cassette10ControlCommandBlock";

        public const string CassetteInformationCommand = "CassetteInformationCommand";


        public const string RecipeIDCheckCommand = "RecipeIDCheckCommand";
        // public const string JobDataDownload = "JobDataDownload";
        public const string CurrentRecipeIDValidationRequest = "CurrentRecipeIDValidationRequest";
        public const string RecipeParameterCommandBlock = "RecipeParameterCommandBlock";
        public const string UnitStatusChangeReportBlock = "UnitStatusChangeReportBlock";
        //int LoadingStopRequestReturn,int LoadingStopReleaseReturn


        public const string Port1CassetteTypeChangeModeReportBlock = "Port1CassetteTypeChangeModeReportBlock";
        public const string Port2CassetteTypeChangeModeReportBlock = "Port2CassetteTypeChangeModeReportBlock";
        public const string Port3CassetteTypeChangeModeReportBlock = "Port3CassetteTypeChangeModeReportBlock";
        public const string Port4CassetteTypeChangeModeReportBlock = "Port4CassetteTypeChangeModeReportBlock";
        public const string Port5CassetteTypeChangeModeReportBlock = "Port5CassetteTypeChangeModeReportBlock";
        public const string Port6CassetteTypeChangeModeReportBlock = "Port6CassetteTypeChangeModeReportBlock";
        public const string Port1ModeChangeReportBlock = "Port1ModeChangeReportBlock";
        public const string Port2ModeChangeReportBlock = "Port2ModeChangeReportBlock";
        public const string Port3ModeChangeReportBlock = "Port3ModeChangeReportBlock";
        public const string Port4ModeChangeReportBlock = "Port4ModeChangeReportBlock";
        public const string Port5ModeChangeReportBlock = "Port5ModeChangeReportBlock";
        public const string Port6ModeChangeReportBlock = "Port6ModeChangeReportBlock";
        public const string Port1OperationModeChangeReportBlock = "Port1OperationModeChangeReportBlock";
        public const string Port2OperationModeChangeReportBlock = "Port2OperationModeChangeReportBlock";
        public const string Port3OperationModeChangeReportBlock = "Port3OperationModeChangeReportBlock";
        public const string Port4OperationModeChangeReportBlock = "Port4OperationModeChangeReportBlock";
        public const string Port5OperationModeChangeReportBlock = "Port5OperationModeChangeReportBlock";
        public const string Port6OperationModeChangeReportBlock = "Port6OperationModeChangeReportBlock";
        public const string Port1StatusBlock = "Port1StatusBlock";
        public const string Port2StatusBlock = "Port2StatusBlock";
        public const string Port3StatusBlock = "Port3StatusBlock";
        public const string Port4StatusBlock = "Port4StatusBlock";
        public const string Port5StatusBlock = "Port5StatusBlock";
        public const string Port6StatusBlock = "Port6StatusBlock";
        public const string Port1TransferModeChangeReportBlock = "Port1TransferModeChangeReportBlock";
        public const string Port2TransferModeChangeReportBlock = "Port2TransferModeChangeReportBlock";
        public const string Port3TransferModeChangeReportBlock = "Port3TransferModeChangeReportBlock";
        public const string Port4TransferModeChangeReportBlock = "Port4TransferModeChangeReportBlock";
        public const string Port5TransferModeChangeReportBlock = "Port5TransferModeChangeReportBlock";
        public const string Port6TransferModeChangeReportBlock = "Port6TransferModeChangeReportBlock";
        public const string Port1TypeAutoChangeModeReportBlock = "Port1TypeAutoChangeModeReportBlock";
        public const string Port2TypeAutoChangeModeReportBlock = "Port2TypeAutoChangeModeReportBlock";
        public const string Port3TypeAutoChangeModeReportBlock = "Port3TypeAutoChangeModeReportBlock";
        public const string Port4TypeAutoChangeModeReportBlock = "Port4TypeAutoChangeModeReportBlock";
        public const string Port5TypeAutoChangeModeReportBlock = "Port5TypeAutoChangeModeReportBlock";
        public const string Port6TypeAutoChangeModeReportBlock = "Port6TypeAutoChangeModeReportBlock";
        public const string Port1TypeChangeReportBlock = "Port1TypeChangeReportBlock";
        public const string Port2TypeChangeReportBlock = "Port2TypeChangeReportBlock";
        public const string Port3TypeChangeReportBlock = "Port3TypeChangeReportBlock";
        public const string Port4TypeChangeReportBlock = "Port4TypeChangeReportBlock";
        public const string Port5TypeChangeReportBlock = "Port5TypeChangeReportBlock";
        public const string Port6TypeChangeReportBlock = "Port6TypeChangeReportBlock";

        public const string Port1WIPDataBlock = "Port1WIPDataBlock";
        public const string Port2WIPDataBlock = "Port2WIPDataBlock";
        public const string Port3WIPDataBlock = "Port3WIPDataBlock";
        public const string Port4WIPDataBlock = "Port4WIPDataBlock";
        public const string Port5WIPDataBlock = "Port5WIPDataBlock";
        public const string Port6WIPDataBlock = "Port6WIPDataBlock";


        public const string LoadingStopRequestReplyBlock = "LoadingStopRequestReplyBlock";
        public const string LoadingStopReleaseReplyBlock = "LoadingStopReleaseReplyBlock";

        public const string PanelDataDownloadRequestReplyBlock = "PanelDataDownloadRequestReplyBlock";
        public const string RecipeRegisterCheckCommandBlock = "RecipeRegisterCheckCommandBlock";
        public const string JobDataCommand = "JobDataCommand";
        public const string OQASamplingRequestReply = "OQASamplingRequestReply";
        public const string RTSamplingRequestReply = "RTSamplingRequestReply";
        public const string JobDataRequestReplyBlock = "JobDataRequestReplyBlock";
        public const string CIMMessageSetCommandBlock = "CIMMessageSetCommandBlock";
        public const string CIMMessageClearCommandBlock = "CIMMessageClearCommandBlock";

        public const string WIPCountChangeReportBlock = "WIPCountChangeReportBlock";
        public const string RecipeVersionRequestReplyBlock = "RecipeVersionRequestReplyBlock";
        public const string RecipeRegisterCheckRequestReplyBlock = "RecipeRegisterCheckRequestReplyBlock";

        public const string MaterialLifeTimeDownloadCommandBlock = "MaterialLifeTimeDownloadCommandBlock";
        public const string DateTimeSetCommandBlock = "DateTimeSetCommandBlock";
        public const string VCREnableModeChangeCommandBlock = "VCREnableModeChangeCommandBlock";
        public const string UnitModeChangeCommandBlock = "UnitModeChangeCommandBlock";
        public const string JobReservationCommandBlock = "JobReservationCommandBlock";
        public const string CIMModeChangeCommandBlock = "CIMModeChangeCommandBlock";
        public const string SetLastGlassCommandBlock = "SetLastGlassCommandBlock";
        public const string Port1TypeChangeCommandBlock = "Port1TypeChangeCommandBlock";
        public const string Port2TypeChangeCommandBlock = "Port2TypeChangeCommandBlock";
        public const string Port3TypeChangeCommandBlock = "Port3TypeChangeCommandBlock";
        public const string Port4TypeChangeCommandBlock = "Port4TypeChangeCommandBlock";
        public const string Port5TypeChangeCommandBlock = "Port5TypeChangeCommandBlock";
        public const string Port6TypeChangeCommandBlock = "Port6TypeChangeCommandBlock";
        public const string Port7TypeChangeCommandBlock = "Port7TypeChangeCommandBlock";
        public const string Port8TypeChangeCommandBlock = "Port8TypeChangeCommandBlock";
        public const string Port9TypeChangeCommandBlock = "Port9TypeChangeCommandBlock";
        public const string Port10TypeChangeCommandBlock = "Port10TypeChangeCommandBlock";
        public const string Port1ModeChangeCommandBlock = "Port1ModeChangeCommandBlock";
        public const string Port2ModeChangeCommandBlock = "Port2ModeChangeCommandBlock";
        public const string Port3ModeChangeCommandBlock = "Port3ModeChangeCommandBlock";
        public const string Port4ModeChangeCommandBlock = "Port4ModeChangeCommandBlock";
        public const string Port5ModeChangeCommandBlock = "Port5ModeChangeCommandBlock";
        public const string Port6ModeChangeCommandBlock = "Port6ModeChangeCommandBlock";
        public const string Port7ModeChangeCommandBlock = "Port7ModeChangeCommandBlock";
        public const string Port8ModeChangeCommandBlock = "Port8ModeChangeCommandBlock";
        public const string Port9ModeChangeCommandBlock = "Port9ModeChangeCommandBlock";
        public const string Port10ModeChangeCommandBlock = "Port10ModeChangeCommandBlock";
        public const string Port1TransferModeChangeCommandBlock = "Port1TransferModeChangeCommandBlock";
        public const string Port2TransferModeChangeCommandBlock = "Port2TransferModeChangeCommandBlock";
        public const string Port3TransferModeChangeCommandBlock = "Port3TransferModeChangeCommandBlock";
        public const string Port4TransferModeChangeCommandBlock = "Port4TransferModeChangeCommandBlock";
        public const string Port5TransferModeChangeCommandBlock = "Port5TransferModeChangeCommandBlock";
        public const string Port6TransferModeChangeCommandBlock = "Port6TransferModeChangeCommandBlock";
        public const string Port7TransferModeChangeCommandBlock = "Port7TransferModeChangeCommandBlock";
        public const string Port8TransferModeChangeCommandBlock = "Port8TransferModeChangeCommandBlock";
        public const string Port9TransferModeChangeCommandBlock = "Port9TransferModeChangeCommandBlock";
        public const string Port10TransferModeChangeCommandBlock = "Port10TransferModeChangeCommandBlock";
        public const string Port1OperationModeChangeCommandBlock = "Port1OperationModeChangeCommandBlock";
        public const string Port2OperationModeChangeCommandBlock = "Port2OperationModeChangeCommandBlock";
        public const string Port3OperationModeChangeCommandBlock = "Port3OperationModeChangeCommandBlock";
        public const string Port4OperationModeChangeCommandBlock = "Port4OperationModeChangeCommandBlock";
        public const string Port5OperationModeChangeCommandBlock = "Port5OperationModeChangeCommandBlock";
        public const string Port6OperationModeChangeCommandBlock = "Port6OperationModeChangeCommandBlock";
        public const string Port7OperationModeChangeCommandBlock = "Port7OperationModeChangeCommandBlock";
        public const string Port8OperationModeChangeCommandBlock = "Port8OperationModeChangeCommandBlock";
        public const string Port9OperationModeChangeCommandBlock = "Port9OperationModeChangeCommandBlock";
        public const string Port10OperationModeChangeCommandBlock = "Port10OperationModeChangeCommandBlock";
        public const string Port1TypeAutoChangeModeCommandBlock = "Port1TypeAutoChangeModeCommandBlock";
        public const string Port2TypeAutoChangeModeCommandBlock = "Port2TypeAutoChangeModeCommandBlock";
        public const string Port3TypeAutoChangeModeCommandBlock = "Port3TypeAutoChangeModeCommandBlock";
        public const string Port4TypeAutoChangeModeCommandBlock = "Port4TypeAutoChangeModeCommandBlock";
        public const string Port5TypeAutoChangeModeCommandBlock = "Port5TypeAutoChangeModeCommandBlock";
        public const string Port6TypeAutoChangeModeCommandBlock = "Port6TypeAutoChangeModeCommandBlock";
        public const string Port7TypeAutoChangeModeCommandBlock = "Port7TypeAutoChangeModeCommandBlock";
        public const string Port8TypeAutoChangeModeCommandBlock = "Port8TypeAutoChangeModeCommandBlock";
        public const string Port9TypeAutoChangeModeCommandBlock = "Port9TypeAutoChangeModeCommandBlock";
        public const string Port10TypeAutoChangeModeCommandBlock = "Port10TypeAutoChangeModeCommandBlock";
        public const string Port1CassetteTypeChangeModeCommandBlock = "Port1CassetteTypeChangeModeCommandBlock";
        public const string Port2CassetteTypeChangeModeCommandBlock = "Port2CassetteTypeChangeModeCommandBlock";
        public const string Port3CassetteTypeChangeModeCommandBlock = "Port3CassetteTypeChangeModeCommandBlock";
        public const string Port4CassetteTypeChangeModeCommandBlock = "Port4CassetteTypeChangeModeCommandBlock";
        public const string Port5CassetteTypeChangeModeCommandBlock = "Port5CassetteTypeChangeModeCommandBlock";
        public const string Port6CassetteTypeChangeModeCommandBlock = "Port6CassetteTypeChangeModeCommandBlock";
        public const string Port7CassetteTypeChangeModeCommandBlock = "Port7CassetteTypeChangeModeCommandBlock";
        public const string Port8CassetteTypeChangeModeCommandBlock = "Port8CassetteTypeChangeModeCommandBlock";
        public const string Port9CassetteTypeChangeModeCommandBlock = "Port9CassetteTypeChangeModeCommandBlock";
        public const string Port10CassetteTypeChangeModeCommandBlock = "Port10CassetteTypeChangeModeCommandBlock";
        public const string Port1UnloadSortingKeyDownloadCommandBlock = "Port1UnloadSortingKeyDownloadCommandBlock";
        public const string Port2UnloadSortingKeyDownloadCommandBlock = "Port2UnloadSortingKeyDownloadCommandBlock";
        public const string Port3UnloadSortingKeyDownloadCommandBlock = "Port3UnloadSortingKeyDownloadCommandBlock";
        public const string Port4UnloadSortingKeyDownloadCommandBlock = "Port4UnloadSortingKeyDownloadCommandBlock";
        public const string Port5UnloadSortingKeyDownloadCommandBlock = "Port5UnloadSortingKeyDownloadCommandBlock";
        public const string Port6UnloadSortingKeyDownloadCommandBlock = "Port6UnloadSortingKeyDownloadCommandBlock";
        public const string Port7UnloadSortingKeyDownloadCommandBlock = "Port7UnloadSortingKeyDownloadCommandBlock";
        public const string Port8UnloadSortingKeyDownloadCommandBlock = "Port8UnloadSortingKeyDownloadCommandBlock";
        public const string Port9UnloadSortingKeyDownloadCommandBlock = "Port9UnloadSortingKeyDownloadCommandBlock";
        public const string Port10UnloadSortingKeyDownloadCommandBlock = "Port10UnloadSortingKeyDownloadCommandBlock";
        public const string Port1ControlCommandBlock = "Port1ControlCommandBlock";
        public const string Port2ControlCommandBlock = "Port2ControlCommandBlock";
        public const string Port3ControlCommandBlock = "Port3ControlCommandBlock";
        public const string Port4ControlCommandBlock = "Port4ControlCommandBlock";
        public const string Port5ControlCommandBlock = "Port5ControlCommandBlock";
        public const string Port6ControlCommandBlock = "Port6ControlCommandBlock";
        public const string Port7ControlCommandBlock = "Port7ControlCommandBlock";
        public const string Port8ControlCommandBlock = "Port8ControlCommandBlock";
        public const string Port9ControlCommandBlock = "Port9ControlCommandBlock";
        public const string Port10ControlCommandBlock = "Port10ControlCommandBlock";



        //public const string RedisCassetteMap1 = "RedisCassetteMap1";
        //public const string RedisCassetteMap2 = "RedisCassetteMap2";
        //public const string RedisCassetteMap3 = "RedisCassetteMap3";
        //public const string RedisCassetteMap4 = "RedisCassetteMap4";
        //public const string RedisCassetteMap5 = "RedisCassetteMap5";
        //public const string RedisCassetteMap6 = "RedisCassetteMap6";


        public const string Port1CassetteMap = "Port1CassetteMap";
        public const string Port2CassetteMap = "Port2CassetteMap";
        public const string Port3CassetteMap = "Port3CassetteMap";
        public const string Port4CassetteMap = "Port4CassetteMap";



        public const string BoxingCompleteInformationRequestReply = "BoxingCompleteInformationRequestReply";
        public const string PalletCompleteInformationRequestReply = "PalletCompleteInformationRequestReply";
        public const string BoxLabelInformationRequestReply = "BoxLabelInformationRequestReply";
        public const string PalletLabelInformationRequestReply = "PalletLabelInformationRequestReply";
        public const string LoginResultCommand = "LoginResultCommand";
        public const string RecipeChangeReportReply = "RecipeChangeReportReply";
        public const string SubCSTDataSendCommand = "SubCSTDataSendCommand";
        public const string JobDataDownload = "JobDataDownload";
        public const string PanelInformationDownlaod = "PanelInformationDownlaod";
        public const string JobData = "JobData";
        public const string MaterialValidationCommand = "MaterialValidationCommand";
        public const string DateTimeCommand = "DateTimeCommand";
        public const string RecipeRequestCommand = "RecipeRequestCommand";
        public const string RecipeChangePermissionRequestReplyBlock = "RecipeChangePermissionRequestReplyBlock";

        public const string CarrierTransferCommand = "CarrierTransferCommand";
        public const string Carrier1TransferCommand = "Carrier1TransferCommand";
        public const string Carrier2TransferCommand = "Carrier2TransferCommand";
        public const string Carrier3TransferCommand = "Carrier3TransferCommand";

        public const string ShuttleTransferCommand = "ShuttleTransferCommand";
        public const string Shuttle1TransferCommand = "ShuttleTransferCommand";
        public const string Shuttle2TransferCommand = "ShuttleTransferCommand";
        public const string Shuttle3TransferCommand = "ShuttleTransferCommand";

        public const string PortTypeChangeCommand = "PortTypeChangeCommand";
        public const string EQPStatusChangeCommand = "EQPStatusChangeCommand";
        public const string TracingDataRequest = "TracingDataRequest";

        public const string CIMMessageCommand = "CIMMessageCommand";
    }

    public class PLCName
    {

    }

    public class PLCList
    {

    }

    public class EQNameList
    {

    }
    public class AutoEvent
    {

    }

    public enum Colour
    {
        Red,
        Green,
        Blue,
        White,
        Black,
        Yellow,
        DimGray,
        Gray
    }


    public class MESEventItem
    {
        #region matti
        public const string AlarmType = "AlarmType";
        public const string AlarmStatus = "AlarmStatus";
        public const string UnitCommandType = "UnitCommandType";
        public const string PortStatus = "PortStatus";
        public const string EQPStatus = "EQPStatus"; 
        public const string EQPStatusLevelThree = "EQPStatusLevelThree";
        public const string PortType = "PortType";
        public const string PortTypeAutoChangeMode = "PortTypeAutoChangeMode";
        public const string PortMode_Substrate_Type = "PortMode_Substrate_Type";
        public const string PortMode_Job_Type = "PortMode_Job_Type";
        public const string PortMode_Judge_Port_Use_Type = "PortMode_Judge_Port_Use_Type";
        public const string PortEnableMode = "PortEnableMode";
        public const string PortTransferMode = "PortTransferMode";
        public const string PortPauseMode = "PortPauseMode";
        public const string PortCassetteType = "PortCassetteType";
        public const string CSTStatus = "CSTStatus";
        public const string PortControlCommandReturnCode = "PortControlCommandReturnCode";
        #endregion

        public const string BothPortName = "BothPortName";
        public const string UnPortName = "UnPortName";
        public const string PortTypeCst = "PortTypeCst";
        public const string ModuleState = "ModuleState";
        public const string PortModeGlassType = "PortModeGlassType";
        public const string PortModeProductionType = "PortModeProductionType";
        public const string PortModeJudge = "PortModeJudge";
        public const string PortName = "PortName";
        //public const string PortUseType = "PortUseType";
        public const string EquipmentStatus = "EquipmentStatus";
        public const string UnitStatus = "UnitStatus";
        public const string JobType = "GlassType";
        public const string GlassJudge = "GlassJudge";
        public const string GlassGrade = "GlassGrade";
        public const string SUnitStatus = "SUnitStatus";
        public const string SSUnitStatus = "SSUnitStatus";
        public const string MaterialState = "MaterialState";
        public const string PPCINFO = "PPCINFO";
        public const string ProductThickness = "ProductThickness";
        public const string ESFLAGMESToBC = "ESFLAGMESToBC";
        public const string ESFLAGBCToMES = "ESFLAGBCToMES";
        public const string ACK8 = "ACK8";
        public const string OperationMode = "OperationMode";
        public const string PORTSTATENAME = "PORTSTATENAME";
        public const string GlassSizeCode = "GlassSizeCode";
        public const string ALCD = "ALCD";
        public const string ALST = "ALST";
        public const string ALLV = "ALLV";
        public const string PORTSTATUS = "PORTSTATUS";
        public const string PortNumber = "PortNumber";
        public const string MATERIALSTATUS = "MATERIALSTATUS";
    }
}
