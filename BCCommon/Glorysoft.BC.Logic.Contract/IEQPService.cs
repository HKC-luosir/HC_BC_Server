using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Contract
{

    public interface IEQPService : IAutoRegister
    {
        #region matti

        /// <summary>
        /// SendJobDataRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="jobdata"></param>
        /// <param name="JobDataRequestAck">1: Job Data Exist  2: Job Data do not Exist</param>
        void SendJobDataRequestReply(string eqpName, JobDataInfo jobdata, string JobDataRequestAck, string transactionID = "");
        void SendCheckLotBindingRequestReply(string eqpName, string ReturnCode,string NGType, string transactionID = "");
        void SendDateTimeRequestReply(string eqpName, string transactionID = "");
        void SendDateTimeSetCommand(string eqpName, string transactionID = "");
        /// <summary>
        /// CIMMessageSetCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMessageType">1-Error 2-Warming 3-Information</param>
        /// <param name="CIMMessageID"></param>
        /// <param name="TouchPanelNumber">0-all panel</param>
        /// <param name="CIMMessageData"></param>
        void SendCIMMessageSetCommand(string eqpName, string CIMMessageType, string CIMMessageID, string TouchPanelNumber, string CIMMessageData, string transactionID = "");
        /// <summary>
        /// CIMMessageClearCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMessageID"></param>
        /// <param name="TouchPanelNo">0-all panel</param>
        void SendCIMMessageClearCommand(string eqpName, string CIMMessageID, string TouchPanelNo, string transactionID = "");
        /// <summary>
        /// CVReportTimeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CVReportEnableMode">1-Enable Mode 2-Disable Mode</param>
        /// <param name="CycleType">By time:The Machine Report the specified time to CIM(EX: Report it in at 8:00 am; Report it in at 8:00 pm…)   By frequency:The Machine Report every 100 minutes to CIM（EX: Report every 100 minutes; Report every 200 minutes…） </param>
        /// <param name="CVReportFrequencyMinute">By frequency  1-65535</param>
        /// <param name="CVReportHour1">By time 00-23</param>
        /// <param name="CVReportMinute1">By time 00-59</param>
        /// <param name="CVReportHour2">By time 00-23</param>
        /// <param name="CVReportMinute2">By time 00-59</param>
        /// <param name="CVReportHour3">By time 00-23</param>
        /// <param name="CVReportMinute3">By time 00-59</param>
        void SendCVReportTimeChangeCommand(string eqpName, string CVReportEnableMode, string CycleType, string CVReportFrequencyMinute, string CVReportHour1, string CVReportMinute1, string CVReportHour2, string CVReportMinute2, string CVReportHour3, string CVReportMinute3, string transactionID = "");
        /// <summary>
        /// CIMModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMode">1-off 2-on</param>
        void SendCIMModeChangeCommand(string eqpName, string CIMMode, string transactionID = "");
        /// <summary>
        /// PortTypeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortType"></param>
        void SendPortTypeChangeCommand(string eqpName, int i, string PortType, string transactionID = "");
        /// <summary>
        /// PortTypeAutoChangeModeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortType"></param>
        void SendPortTypeAutoChangeModeCommand(string eqpName, int i, string PortType, string transactionID = "");
        /// <summary>
        /// PortModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortMode"></param>
        void SendPortModeChangeCommand(string eqpName, int i, string PortMode, string transactionID = "");
        /// <summary>
        /// PortEnableModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortEnableMode">1-Enable 2-Disable</param>
        void SendPortEnableModeChangeCommand(string eqpName, int i, string PortEnableMode, string transactionID = "");
        /// <summary>
        /// PortControlCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortControlCommandCode">1-Chuck 2-Unchuck</param>
        void SendPortControlCommand(string eqpName, int i, string PortControlCommandCode, string transactionID = "");
        /// <summary>
        /// PortBoxInfoRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        void SendPortBoxInfoRequestReply(string eqpName, int i, string ReturnCode, string transactionID = "");
        /// <summary>
        /// PortTransferModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortTransferMode">1-MGV Mode 2-AGV Mode 3-Stocker Inline Mode</param>
        void SendPortTransferModeChangeCommand(string eqpName, int i, string PortTransferMode, string transactionID = "");
        /// <summary>
        /// PortPauseModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortPauseMode">1-Paused 2-Normal</param>
        void SendPortPauseModeChangeCommand(string eqpName, int i, string PortPauseMode, string transactionID = "");
        /// <summary>
        /// PortGradeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortGrade"></param>
        void SendPortGradeChangeCommand(string eqpName, int i, string PortGrade, string transactionID = "");
        /// <summary>
        /// PortCassetteTypeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortCassetteType"></param>
        void SendPortCassetteTypeChangeCommand(string eqpName, int i, string PortCassetteType, string transactionID = "");
        /// <summary>
        /// PortQTimeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortQTime"></param>
        void SendPortQTimeChangeCommand(string eqpName, int i, string PortQTime, string transactionID = "");
        /// <summary>
        /// MachineModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="MachineMode"></param>
        void SendMachineModeChangeCommand(string eqpName, string MachineMode, string transactionID = "");
        /// <summary>
        /// SamplingDownloadCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        void SendSamplingDownloadCommand(string eqpName, string BatchQty, string transactionID = "");
        /// <summary>
        /// DVSamplingFlagCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        void SendDVSamplingFlagCommand(string eqpName, string BatchQty, string transactionID = "");
        void SendSamplingRequestReply(string eqpName, string BatchQty, string transactionID = "");
        /// <summary>
        /// SpecialCodeRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="JobID"></param>
        /// <param name="LotSequenceNumber"></param>
        /// <param name="SlotSequenceNumber"></param>
        /// <param name="AbnormalFlag1"></param>
        /// <param name="AbnormalFlag2"></param>
        /// <param name="AbnormalFlag3"></param>
        /// <param name="AbnormalFlag4"></param>
        /// <param name="AbnormalFlag5"></param>
        /// <param name="AbnormalFlag6"></param>
        /// <param name="AbnormalFlag7"></param>
        /// <param name="AbnormalFlag8"></param>
        /// <param name="WorkOrderID"></param>
        void SendSpecialCodeRequestReply(string eqpName, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string AbnormalFlag1, string AbnormalFlag2, string AbnormalFlag3, string AbnormalFlag4, string AbnormalFlag5, string AbnormalFlag6, string AbnormalFlag7, string AbnormalFlag8, string WorkOrderID, string transactionID = "");
        /// <summary>
        /// CuttingRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        void SendCuttingRequestReply(string eqpName, string ReturnCode, string transactionID = "");
        /// <summary>
        /// RecipeChangeReportReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        void SendRecipeChangeReportReply(string eqpName, string ReturnCode, string transactionID = "");
        /// <summary>
        /// BoxWeightCheckRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        void SendBoxWeightCheckRequestReply(string eqpName, string ReturnCode, string transactionID = "");
        /// <summary>
        /// OCIDRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="JobID"></param>
        /// <param name="LotSequenceNumber"></param>
        /// <param name="SlotSequenceNumber"></param>
        /// <param name="OCID"></param>
        /// <param name="DateTime"></param>
        /// <param name="ModelType"></param>
        /// <param name="Version"></param>
        /// <param name="ReturnCode"></param>
        /// <param name="transactionID"></param>
        void SendOCIDRequestReply(string eqpName, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string OCID, string DateTime, string ModelType, string Version, string ReturnCode, string transactionID = "");

        void SendRecipeParameterRequestCommand(string eqpName, string recipeNumber, string transactionID = "");
        void SendPalletInfoRequestReply(string eqpName, string palletID, string boxQTY, string reserveQTY, string transactionID = "");

        #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
        void Terminate();
        #endregion

        #endregion
        #region Yuan
        void SendPanelJudgeDataDownloadRequestReply(string eqpName, int returnCode, GlassInfo glassInfo, string transactionID = "");
        void SendMaterialValidationRequestReply(string eqpName, int requestNo, string returnCode, string transactionID = "");
        void SendMaterialLotInfoRequestReply(string eqpName, int requestNo, string materrialCarrierID, List<MaterialInfo> materialList, int returnCode, string transactionID = "");
        void SendCassetteControlCommand(string eqpName, string portNo, EnumCassetteControlCommand commandType, string jobExistenceSlot, string jobCountforProcess, string transactionID = "");
        void SendRobotControlCommand(string eqpName, string localNo, RobotCommand cmd, string transactionID = "");
        void CassetteMapDownloadCommand(string eqpName, string localNo, int eqpCommandType, List<GlassInfo> glassInfoList, string portNo, int capacity, string jobExistenceSlot, string jobCountInCassette, string transactionID = "");
        #endregion

        #region 
        void SendPanelProcessEndRequestReply(string eqpName, string jobId, string returnCode, string transactionID = "");
        void SendMaterialStatusChangeReportReply(string eqpName, int i, string returnCode, string transactionID = "");
        #endregion
    }
}
