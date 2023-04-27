using System;
using System.Collections.Generic;
using System.Threading;
using Glorysoft.BC.EIP.Handlers;
using Glorysoft.BC.Entity;
using log4net;
using System.Threading.Tasks;

namespace Glorysoft.BC.EIP.Common
{
    public interface IPLCEventDispather
    {
        //Int64 msgcount { get; set; }//压力测试用 计算每天消息量
        void Dispath(PLCEventArgs args);
    }

    internal class PLCEventDispather : IPLCEventDispather
    {
        private readonly IPLCContext context;
        private readonly ILog logger = LogHelper.EIPLog;
        private readonly Dictionary<string, AbstractEventHandler> dict = new Dictionary<string, AbstractEventHandler>();
        //public Int64 msgcount { get; set; } = 0;//压力测试用 计算每天消息量
        public PLCEventDispather(IPLCContext context)
        {
            this.context = context;
            #region matti 
            dict.Add(PLCEventName.PositionStatus, new PositionStatusHandler(context));
            dict.Add(PLCEventName.ReceivedJobReport1Block, new ReceivedJobReportBlockHandler(context));
            dict.Add(PLCEventName.ReceivedJobReport2Block, new ReceivedJobReportBlockHandler(context));
            dict.Add(PLCEventName.SentOutJobReport1Block, new SentOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.SentOutJobReport2Block, new SentOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport1Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport2Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport3Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport4Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport5Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport6Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport7Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport8Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.StoredJobReport9Block, new StoredJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport1Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport2Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport3Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport4Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport5Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport6Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport7Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport8Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.FetchedOutJobReport9Block, new FetchedOutJobReportBlockHandler(context));
            dict.Add(PLCEventName.JobManualMoveReportBlock, new JobManualMoveReportBlockHandler(context));
            dict.Add(PLCEventName.JobDataRequestBlock, new JobDataRequestBlockHandler(context));
            dict.Add(PLCEventName.JobDataChangeReportBlock, new JobDataChangeReportBlockHandler(context));


            dict.Add(PLCEventName.MachineStatusChangeReportBlock, new MachineStatusChangeReportBlockHandler(context));
            dict.Add(PLCEventName.CIMMode, new CIMModeHandler(context));
            dict.Add(PLCEventName.CIMModeChangeCommandReplyBlock, new CIMModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.MaterialStatusChangeReport1Block, new MaterialStatusChangeReportBlockHandler(context));
            dict.Add(PLCEventName.MaterialStatusChangeReport2Block, new MaterialStatusChangeReportBlockHandler(context));
            dict.Add(PLCEventName.VCRStatusReportBlock, new VCRStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortTypeChangeReportBlock, new PortTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortTypeChangeReportBlock, new PortTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortTypeChangeReportBlock, new PortTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortTypeChangeReportBlock, new PortTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortTypeChangeCommandReplyBlock, new PortTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortTypeChangeCommandReplyBlock, new PortTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortTypeChangeCommandReplyBlock, new PortTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortTypeChangeCommandReplyBlock, new PortTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PortTypeAutoChangeModeReportBlock, new PortTypeAutoChangeModeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortTypeAutoChangeModeReportBlock, new PortTypeAutoChangeModeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortTypeAutoChangeModeReportBlock, new PortTypeAutoChangeModeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortTypeAutoChangeModeReportBlock, new PortTypeAutoChangeModeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortTypeAutoChangeModeCommandReplyBlock, new PortTypeAutoChangeModeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortTypeAutoChangeModeCommandReplyBlock, new PortTypeAutoChangeModeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortTypeAutoChangeModeCommandReplyBlock, new PortTypeAutoChangeModeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortTypeAutoChangeModeCommandReplyBlock, new PortTypeAutoChangeModeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PortModeChangeReportBlock, new PortModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortModeChangeReportBlock, new PortModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortModeChangeReportBlock, new PortModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortModeChangeReportBlock, new PortModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortModeChangeCommandReplyBlock, new PortModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortModeChangeCommandReplyBlock, new PortModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortModeChangeCommandReplyBlock, new PortModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortModeChangeCommandReplyBlock, new PortModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1EnableModeChangeReportBlock, new PortEnableModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2EnableModeChangeReportBlock, new PortEnableModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3EnableModeChangeReportBlock, new PortEnableModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4EnableModeChangeReportBlock, new PortEnableModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1EnableModeChangeCommandReplyBlock, new PortEnableModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2EnableModeChangeCommandReplyBlock, new PortEnableModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3EnableModeChangeCommandReplyBlock, new PortEnableModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4EnableModeChangeCommandReplyBlock, new PortEnableModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PortTransferModeChangeReportBlock, new PortTransferModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortTransferModeChangeReportBlock, new PortTransferModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortTransferModeChangeReportBlock, new PortTransferModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortTransferModeChangeReportBlock, new PortTransferModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortTransferModeChangeCommandReplyBlock, new PortTransferModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortTransferModeChangeCommandReplyBlock, new PortTransferModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortTransferModeChangeCommandReplyBlock, new PortTransferModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortTransferModeChangeCommandReplyBlock, new PortTransferModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PauseModeChangeReportBlock, new PortPauseModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PauseModeChangeReportBlock, new PortPauseModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PauseModeChangeReportBlock, new PortPauseModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PauseModeChangeReportBlock, new PortPauseModeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PauseModeChangeCommandReplyBlock, new PortPauseModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PauseModeChangeCommandReplyBlock, new PortPauseModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PauseModeChangeCommandReplyBlock, new PortPauseModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PauseModeChangeCommandReplyBlock, new PortPauseModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PortGradeChangeReportBlock, new PortGradeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortGradeChangeReportBlock, new PortGradeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortGradeChangeReportBlock, new PortGradeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortGradeChangeReportBlock, new PortGradeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortGradeChangeCommandReplyBlock, new PortGradeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortGradeChangeCommandReplyBlock, new PortGradeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortGradeChangeCommandReplyBlock, new PortGradeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortGradeChangeCommandReplyBlock, new PortGradeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port5PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port6PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port7PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port8PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port9PortCassetteTypeChangeReportBlock, new PortCassetteTypeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1PortCassetteTypeChangeCommandReplyBlock, new PortCassetteTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortCassetteTypeChangeCommandReplyBlock, new PortCassetteTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortCassetteTypeChangeCommandReplyBlock, new PortCassetteTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortCassetteTypeChangeCommandReplyBlock, new PortCassetteTypeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.VCRReadCompleteReportBlock, new VCRReadCompleteReportBlockHandler(context));
            dict.Add(PLCEventName.Port1QTimeChangeCommandReplyBlock, new PortQTimeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2QTimeChangeCommandReplyBlock, new PortQTimeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3QTimeChangeCommandReplyBlock, new PortQTimeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4QTimeChangeCommandReplyBlock, new PortQTimeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.MachineModeChangeCommandReplyBlock, new MachineModeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.CIMMessageSetCommandReplyBlock, new CIMMessageSetCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.CIMMessageClearCommandReplyBlock, new CIMMessageClearCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.SamplingDownloadCommandReplyBlock, new SamplingDownloadCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.DVDataReportBlock, new DVDataReportBlockHandler(context));
            dict.Add(PLCEventName.CVDataReportBlock, new CVDataReportBlockHandler(context));
            dict.Add(PLCEventName.DateTimeRequestBlock, new DateTimeRequestBlockHandler(context));
            dict.Add(PLCEventName.DVSamplingFlagCommandReplyBlock, new DVSamplingFlagCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.SpecialCodeRequestBlock, new SpecialCodeRequestBlockHandler(context));
            dict.Add(PLCEventName.CuttingRequestBlock, new CuttingRequestBlockHandler(context));
            dict.Add(PLCEventName.CVReportTimeChangeCommandReplyBlock, new CVReportTimeChangeCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port1PortControlCommandReplyBlock, new PortControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2PortControlCommandReplyBlock, new PortControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3PortControlCommandReplyBlock, new PortControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4PortControlCommandReplyBlock, new PortControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.AbnormalCodeReportBlock, new AbnormalCodeReportBlockHandler(context));
            dict.Add(PLCEventName.Port1BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port2BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port3BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port4BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port5BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port6BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port7BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port8BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port9BoxGroupPortStatusReportBlock, new PortBoxGroupPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port1BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port2BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port3BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port4BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port5BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port6BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port7BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port8BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port9BoxInfoRequestBlock, new PortBoxInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.Port1BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port2BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port3BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port4BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port5BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port6BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port7BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port8BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.Port9BoxPortStatusReportBlock, new PortBoxPortStatusReportBlockHandler(context));
            dict.Add(PLCEventName.JobData1, new LinkSignalJobDataBlockHandler(context));
            dict.Add(PLCEventName.JobData2, new LinkSignalJobDataBlockHandler(context));
            #endregion
            #region Yuan
            dict.Add(PLCEventName.UpstreamLinkSignal, new LinkSignalHandler(context));
            dict.Add(PLCEventName.DownstreamLinkSignal, new LinkSignalHandler(context));

            dict.Add(PLCEventName.Port1PortStatusChangeReportBlock, new PortStatusChangeReportHandler(context));
            dict.Add(PLCEventName.Port2PortStatusChangeReportBlock, new PortStatusChangeReportHandler(context));
            dict.Add(PLCEventName.Port3PortStatusChangeReportBlock, new PortStatusChangeReportHandler(context));
            dict.Add(PLCEventName.Port4PortStatusChangeReportBlock, new PortStatusChangeReportHandler(context));

            dict.Add(PLCEventName.Port1PortQTimeTimeOutReportBlock, new PortQTimeTimeOutReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortQTimeTimeOutReportBlock, new PortQTimeTimeOutReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortQTimeTimeOutReportBlock, new PortQTimeTimeOutReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortQTimeTimeOutReportBlock, new PortQTimeTimeOutReportBlockHandler(context));

            dict.Add(PLCEventName.Port1PortQTimeChangeReportBlock, new PortQTimeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port2PortQTimeChangeReportBlock, new PortQTimeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port3PortQTimeChangeReportBlock, new PortQTimeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.Port4PortQTimeChangeReportBlock, new PortQTimeChangeReportBlockHandler(context));

            dict.Add(PLCEventName.Port1CassetteMapDownloadCommandReplyBlock, new CassetteMapDownloadCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2CassetteMapDownloadCommandReplyBlock, new CassetteMapDownloadCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3CassetteMapDownloadCommandReplyBlock, new CassetteMapDownloadCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4CassetteMapDownloadCommandReplyBlock, new CassetteMapDownloadCommandReplyBlockHandler(context));

            dict.Add(PLCEventName.Port1CassetteControlCommandReplyBlock, new CassetteControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port2CassetteControlCommandReplyBlock, new CassetteControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port3CassetteControlCommandReplyBlock, new CassetteControlCommandReplyBlockHandler(context));
            dict.Add(PLCEventName.Port4CassetteControlCommandReplyBlock, new CassetteControlCommandReplyBlockHandler(context));

            dict.Add(PLCEventName.Port1CassetteProcessStartReportBlock, new CassetteProcessStartReportBlockHandler(context));
            dict.Add(PLCEventName.Port2CassetteProcessStartReportBlock, new CassetteProcessStartReportBlockHandler(context));
            dict.Add(PLCEventName.Port3CassetteProcessStartReportBlock, new CassetteProcessStartReportBlockHandler(context));
            dict.Add(PLCEventName.Port4CassetteProcessStartReportBlock, new CassetteProcessStartReportBlockHandler(context));

            dict.Add(PLCEventName.Port1CassetteProcessEndReportBlock, new CassetteProcessEndReportBlockHandler(context));
            dict.Add(PLCEventName.Port2CassetteProcessEndReportBlock, new CassetteProcessEndReportBlockHandler(context));
            dict.Add(PLCEventName.Port3CassetteProcessEndReportBlock, new CassetteProcessEndReportBlockHandler(context));
            dict.Add(PLCEventName.Port4CassetteProcessEndReportBlock, new CassetteProcessEndReportBlockHandler(context));

            dict.Add(PLCEventName.RobotCommandResultReportBlock, new RobotCommandResultReportBlockHandler(context));
            //dict.Add(PLCEventName.RobotCommandFetchOutReportBlock, new RobotCommandFetchOutReportBlockHandler(context));
            dict.Add(PLCEventName.RobotControlCommandReplyBlock, new RobotControlCommandReplyBlockHandler(context));

            dict.Add(PLCEventName.RobotArmMonitoringBlock, new RobotArmMonitoringBlockHandler(context));

            dict.Add(PLCEventName.CVMonitoringSlot1Block, new CVMonitoringSlotBlockHandler(context));
            dict.Add(PLCEventName.CVMonitoringSlot2Block, new CVMonitoringSlotBlockHandler(context));
            dict.Add(PLCEventName.CVMonitoringSlot3Block, new CVMonitoringSlotBlockHandler(context));
            dict.Add(PLCEventName.CVMonitoringSlot4Block, new CVMonitoringSlotBlockHandler(context));
            dict.Add(PLCEventName.CVMonitoringSlot5Block, new CVMonitoringSlotBlockHandler(context));
            dict.Add(PLCEventName.CVMonitoringSlot6Block, new CVMonitoringSlotBlockHandler(context));

            dict.Add(PLCEventName.BufferJobMonitoring1Block, new BufferJobMonitoringBlockHandler(context));
            dict.Add(PLCEventName.BufferJobMonitoring2Block, new BufferJobMonitoringBlockHandler(context));
            dict.Add(PLCEventName.BufferJobMonitoring3Block, new BufferJobMonitoringBlockHandler(context));

            dict.Add(PLCEventName.TransferBoxReport1Block, new TransferBoxReportBlockHandler(context));
            dict.Add(PLCEventName.TransferBoxReport2Block, new TransferBoxReportBlockHandler(context));

            dict.Add(PLCEventName.MaterialLotInfoRequestBlock, new MaterialLotInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.MaterialLotInfoRequest1Block, new MaterialLotInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.MaterialLotInfoRequest2Block, new MaterialLotInfoRequestBlockHandler(context));

            dict.Add(PLCEventName.CellMaterialAssemblyReportBlock, new CellMaterialAssemblyReportBlockHandler(context));
            dict.Add(PLCEventName.CellMaterialAssemblyReport1Block, new CellMaterialAssemblyReportBlockHandler(context));
            dict.Add(PLCEventName.CellMaterialAssemblyReport2Block, new CellMaterialAssemblyReportBlockHandler(context));
            dict.Add(PLCEventName.MaterialCountChangeReportBlock, new MaterialCountChangeReportBlockHandler(context));
            dict.Add(PLCEventName.MaterialCountChangeReport1Block, new MaterialCountChangeReportBlockHandler(context));
            dict.Add(PLCEventName.MaterialCountChangeReport2Block, new MaterialCountChangeReportBlockHandler(context));
            
            dict.Add(PLCEventName.AlarmReportBlock, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport1Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport2Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport3Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport4Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport5Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport6Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport7Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport8Block, new AlarmReportBlockHandler(context));
            dict.Add(PLCEventName.AlarmReport9Block, new AlarmReportBlockHandler(context));

            dict.Add(PLCEventName.AutoRecipeChangeModeReportBlock, new AutoRecipeChangeModeReportBlockHandler(context));
            dict.Add(PLCEventName.CurrentRecipeNumberChangeReportBlock, new CurrentRecipeNumberChangeReportBlockHandler(context));
            dict.Add(PLCEventName.RecipeChangeReportBlock, new RecipeChangeReportBlockHandler(context));
            dict.Add(PLCEventName.RecipeParameterRequestCommandReplyBlock, new RecipeParameterRequestCommandReplyBlockHandler(context));


            dict.Add(PLCEventName.PanelJudgeDataDownloadRequestBlock, new PanelJudgeDataDownloadRequestBlockHandler(context));
            dict.Add(PLCEventName.PanelDataUpdateReportBlock, new PanelDataUpdateReportBlockHandler(context));
            dict.Add(PLCEventName.JobJudgeResultReport1Block, new JobJudgeResultReportBlockHandler(context));
            dict.Add(PLCEventName.JobJudgeResultReport2Block, new JobJudgeResultReportBlockHandler(context));
            dict.Add(PLCEventName.JobJudgeResultReportBlock, new JobJudgeResultReportBlockHandler(context));

            dict.Add(PLCEventName.MachineModeChangeReportBlock, new MachineModeChangeReportBlockHandler(context));

            dict.Add(PLCEventName.SamplingRequestBlock, new SamplingRequestBlockHandler(context));
            dict.Add(PLCEventName.SamplingFlagReportBlock, new SamplingFlagReportBlockHandler(context));
            dict.Add(PLCEventName.IonizerStatusReportBlock, new IonizerStatusReportBlockHandler(context));
            dict.Add(PLCEventName.CSTMoveInReportBlock, new CSTMoveInReportBlockHandler(context));
            dict.Add(PLCEventName.CSTMoveOutReportBlock, new CSTMoveOutReportBlockHandler(context));
            dict.Add(PLCEventName.OperatorLoginReportBlock, new OperatorLoginReportBlockHandler(context));
            dict.Add(PLCEventName.JobAssemblyReport1Block, new JobAssemblyReport1BlockHandler(context));
            dict.Add(PLCEventName.BoxWeightCheckRequestBlock, new BoxWeightCheckRequestBlockHandler(context));
            dict.Add(PLCEventName.DefectCodeReportBlock, new DefectCodeReportBlockHandler(context));
            dict.Add(PLCEventName.LableInfoRequestReportBlock, new LableInfoRequestReportBlockHandler(context));
            dict.Add(PLCEventName.PalletInfoRequestBlock, new PalletInfoRequestBlockHandler(context));
            dict.Add(PLCEventName.MaterialValidationRequest1Block, new MaterialValidationRequestBlockHandler(context));
            dict.Add(PLCEventName.MaterialValidationRequest2Block, new MaterialValidationRequestBlockHandler(context));
            dict.Add(PLCEventName.PanelProcessEndRequestBlock, new PanelProcessEndRequestBlockHandler(context));
            dict.Add(PLCEventName.OCIDRequestBlock, new OCIDRequestBlockHandler(context));

            #endregion
            dict.Add(PLCEventName.EIPConnect, new EIPConnectHandler(context));
            #region 需求3 1.EIP通讯状态变化 liuyusen 20221010
            dict.Add(PLCEventName.EIPDisConnect, new EIPDisConnectHandler(context));
            dict.Add(PLCEventName.MachineAlive, new MachineAliveHandler(context));
            #endregion

            #region luoxianjing
            dict.Add(PLCEventName.CheckLotBindingRequestBlock, new CheckLotBindingRequestBlockHandler(context));
            #endregion


        }
        //public class EIPWordHandler
        //{
        //    public AbstractEventHandler Handler;
        //    public PLCEventArgs Args;
        //}
        //protected readonly object SyncRoot = new object();
        //private readonly Queue<EIPWordHandler> rcvQueue = new Queue<EIPWordHandler>();
        //private void Enqueue(EIPWordHandler block)
        //{
        //    if (block == null) return;
        //    lock (SyncRoot)
        //    {
        //        rcvQueue.Enqueue(block);
        //    }
        //}
        //private EIPWordHandler Dequeue()
        //{
        //    if (rcvQueue.Count > 0)
        //    {
        //        EIPWordHandler blk;
        //        lock (SyncRoot)
        //        {
        //            blk = rcvQueue.Dequeue();
        //        }
        //        return blk;
        //    }
        //    return null;
        //}
        //protected void Run()
        //{
        //    while (true)
        //    {
        //        var qData = Dequeue();
        //        if (qData == null)
        //        {
        //            System.Threading.Thread.Sleep(1);
        //            continue;
        //        }
        //        else
        //        {
        //            qData.Handler.Execute(qData.Args);
        //        }
        //        System.Threading.Thread.Sleep(1);
        //    }
        //}
        public IPLCContext Context
        {
            get
            {
                return context;
            }
        }

        public void Dispath(PLCEventArgs args)
        {
            try
            {
                if (args.EventType == IndexerEventType.RCVDEvent)
                {
                    var msg = args.Message;
                    if (dict.ContainsKey(msg.EventName))
                    {
                        ThreadPool.QueueUserWorkItem(EventReportAsyncHandler, args);
                    }
                }
                else if (args.EventType == IndexerEventType.BitOnOff)
                {
                    var msg = args.BitItem;
                    //AbstractEventHandler handler = null;
                    if (dict.ContainsKey(msg.EventID))
                    {
                        ThreadPool.QueueUserWorkItem(BitReportAsyncHandler, args);
                    }
                }
                else
                {
                    if (args.EventType == IndexerEventType.Connect)
                    {

                        var handler = dict["EIPConnect"];
                        handler.Execute(args);
                    }
                    #region 需求3 1.EIP通讯状态变化 liuyusen 20221010
                    else if (args.EventType == IndexerEventType.Disconnect)
                    {
                        var handler = dict["EIPDisConnect"];
                        handler.Execute(args);
                    }
                    #endregion
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("{0}: Error {1}!", GetType().Name, ex.Message), ex);
            }
        }

        private void EventReportAsyncHandler(object obj)
        {
            var args = obj as PLCEventArgs;
            if (args != null)
            {
                var msg = args.Message;
                var handler = dict[msg.EventName];
                handler.Execute(args);
                //msgcount++;//压力测试用 计算每天消息量
            }
        }
        private void BitReportAsyncHandler(object obj)
        {
            var args = obj as PLCEventArgs;
            if (args != null)
            {
                var msg = args.BitItem;
                var handler = dict[msg.EventID];
                handler.Execute(args);
                //msgcount++;//压力测试用 计算每天消息量
            }
        }
    }
}