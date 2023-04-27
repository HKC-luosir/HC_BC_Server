using Glorysoft.Auto.Contract;
using Glorysoft.BC.EIP;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Service
{

    public class EQPService : AbstractEventHandler, IEQPService
    {
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();

        protected ILog BCLog = LogHelper.BCLog;

        #region
        public void SendCheckLotBindingRequestReply(string eqpName, string ReturnCode, string NGType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, ReturnCode, NGType, transactionID }));
                #endregion
                PLCCmdExecutor.SendCheckLotBindingRequestReply(eqpName, ReturnCode, NGType, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendCheckLotBindingRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        #endregion
        #region matti

        /// <summary>
        /// SendJobDataRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="jobdata"></param>
        /// <param name="JobDataRequestAck">1: Job Data Exist  2: Job Data do not Exist</param>
        /// 

        public void SendJobDataRequestReply(string eqpName, JobDataInfo jobdata, string JobDataRequestAck, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, jobdata, JobDataRequestAck, transactionID }));
                #endregion

                PLCCmdExecutor.SendJobDataRequestReply(eqpName, jobdata, JobDataRequestAck, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendJobDataRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendDateTimeRequestReply(string eqpName, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, transactionID }));
                #endregion

                PLCCmdExecutor.SendDateTimeRequestReply(eqpName, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendDateTimeRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendDateTimeSetCommand(string eqpName, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, transactionID }));
                #endregion

                PLCCmdExecutor.SendDateTimeSetCommand(eqpName, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendDateTimeSetCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CIMMessageSetCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMessageType">1-Error 2-Warming 3-Information</param>
        /// <param name="CIMMessageID"></param>
        /// <param name="TouchPanelNumber">0-all panel</param>
        /// <param name="CIMMessageData"></param>
        public void SendCIMMessageSetCommand(string eqpName, string CIMMessageType, string CIMMessageID, string TouchPanelNumber, string CIMMessageData, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, CIMMessageType, CIMMessageID, TouchPanelNumber, CIMMessageData, transactionID }));
                #endregion

                PLCCmdExecutor.SendCIMMessageSetCommand(eqpName, CIMMessageType, CIMMessageID, TouchPanelNumber, CIMMessageData, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendDateTimeSetCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CIMMessageClearCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMessageID"></param>
        /// <param name="TouchPanelNo">0-all panel</param>
        public void SendCIMMessageClearCommand(string eqpName, string CIMMessageID, string TouchPanelNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, CIMMessageID, TouchPanelNo, transactionID }));
                #endregion

                PLCCmdExecutor.SendCIMMessageClearCommand(eqpName, CIMMessageID, TouchPanelNo, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendCIMMessageClearCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CVReportTimeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CVReportEnableMode">-Enable Mode 2-Disable Mode</param>
        /// <param name="CycleType">By time:The Machine Report the specified time to CIM(EX: Report it in at 8:00 am; Report it in at 8:00 pm…)   By frequency:The Machine Report every 100 minutes to CIM（EX: Report every 100 minutes; Report every 200 minutes…） </param>
        /// <param name="CVReportFrequencyMinute">By frequency  1-65535</param>
        /// <param name="CVReportHour1">By time 00-23</param>
        /// <param name="CVReportMinute1">By time 00-59</param>
        /// <param name="CVReportHour2">By time 00-23</param>
        /// <param name="CVReportMinute2">By time 00-59</param>
        /// <param name="CVReportHour3">By time 00-23</param>
        /// <param name="CVReportMinute3">By time 00-59</param>
        public void SendCVReportTimeChangeCommand(string eqpName, string CVReportEnableMode, string CycleType, string CVReportFrequencyMinute, string CVReportHour1, string CVReportMinute1, string CVReportHour2, string CVReportMinute2, string CVReportHour3, string CVReportMinute3, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, CVReportEnableMode, CycleType, CVReportFrequencyMinute, CVReportHour1, CVReportMinute1, CVReportHour2, CVReportMinute2, CVReportHour3, CVReportMinute3, transactionID }));
                #endregion

                PLCCmdExecutor.SendCVReportTimeChangeCommand(eqpName, CVReportEnableMode, CycleType, CVReportFrequencyMinute, CVReportHour1, CVReportMinute1, CVReportHour2, CVReportMinute2, CVReportHour3, CVReportMinute3, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendCVReportTimeChangeCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CIMModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMode">1-off 2-on</param>
        public void SendCIMModeChangeCommand(string eqpName, string CIMMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, CIMMode, transactionID }));
                #endregion

                PLCCmdExecutor.SendCIMModeChangeCommand(eqpName, CIMMode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendCIMModeChangeCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// PortTypeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortType"></param>
        public void SendPortTypeChangeCommand(string eqpName, int i, string PortType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortType, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortTypeChangeCommand(eqpName, i, PortType, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortTypeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortTypeAutoChangeModeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortType"></param>
        public void SendPortTypeAutoChangeModeCommand(string eqpName, int i, string PortType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortType, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortTypeAutoChangeModeCommand(eqpName, i, PortType, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortTypeAutoChangeModeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortMode"></param>
        public void SendPortModeChangeCommand(string eqpName, int i, string PortMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortMode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortModeChangeCommand(eqpName, i, PortMode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortEnableModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortEnableMode">1-Enable 2-Disable</param>
        public void SendPortEnableModeChangeCommand(string eqpName, int i, string PortEnableMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortEnableMode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortEnableModeChangeCommand(eqpName, i, PortEnableMode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortEnableModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortControlCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortControlCommandCode">1-Chuck 2-Unchuck</param>
        public void SendPortControlCommand(string eqpName, int i, string PortControlCommandCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortControlCommandCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortControlCommand(eqpName, i, PortControlCommandCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortControlCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortBoxInfoRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public void SendPortBoxInfoRequestReply(string eqpName, int i, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, ReturnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortBoxInfoRequestReply(eqpName, i, ReturnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortBoxInfoRequestReply]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortTransferModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortTransferMode">1-MGV Mode 2-AGV Mode 3-Stocker Inline Mode</param>
        public void SendPortTransferModeChangeCommand(string eqpName, int i, string PortTransferMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortTransferMode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortTransferModeChangeCommand(eqpName, i, PortTransferMode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortTransferModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortPauseModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortPauseMode">1-Paused 2-Normal</param>
        public void SendPortPauseModeChangeCommand(string eqpName, int i, string PortPauseMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortPauseMode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortPauseModeChangeCommand(eqpName, i, PortPauseMode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortPauseModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortGradeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortGrade"></param>
        public void SendPortGradeChangeCommand(string eqpName, int i, string PortGrade, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortGrade, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortGradeChangeCommand(eqpName, i, PortGrade, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortGradeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortCassetteTypeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortCassetteType"></param>
        public void SendPortCassetteTypeChangeCommand(string eqpName, int i, string PortCassetteType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortCassetteType, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortCassetteTypeChangeCommand(eqpName, i, PortCassetteType, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortCassetteTypeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortQTimeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortQTime"></param>
        public void SendPortQTimeChangeCommand(string eqpName, int i, string PortQTime, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, PortQTime, transactionID }));
                #endregion

                PLCCmdExecutor.SendPortQTimeChangeCommand(eqpName, i, PortQTime, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPortQTimeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// MachineModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="MachineMode"></param>
        public void SendMachineModeChangeCommand(string eqpName, string MachineMode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, MachineMode, transactionID }));
                #endregion

                PLCCmdExecutor.SendMachineModeChangeCommand(eqpName, MachineMode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendMachineModeChangeCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// SamplingDownloadCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        public void SendSamplingDownloadCommand(string eqpName, string BatchQty, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, BatchQty, transactionID }));
                #endregion

                PLCCmdExecutor.SendSamplingDownloadCommand(eqpName, BatchQty, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendSamplingDownloadCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// DVSamplingFlagCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        public void SendDVSamplingFlagCommand(string eqpName, string BatchQty, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, BatchQty, transactionID }));
                #endregion

                PLCCmdExecutor.SendDVSamplingFlagCommand(eqpName, BatchQty, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendDVSamplingFlagCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
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
        public void SendSpecialCodeRequestReply(string eqpName, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string AbnormalFlag1, string AbnormalFlag2, string AbnormalFlag3, string AbnormalFlag4, string AbnormalFlag5, string AbnormalFlag6, string AbnormalFlag7, string AbnormalFlag8, string WorkOrderID, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, JobID, LotSequenceNumber, SlotSequenceNumber, AbnormalFlag1, AbnormalFlag2, AbnormalFlag3, AbnormalFlag4, AbnormalFlag5, AbnormalFlag6, AbnormalFlag7, AbnormalFlag8, WorkOrderID, transactionID }));
                #endregion

                PLCCmdExecutor.SendSpecialCodeRequestReply(eqpName, JobID, LotSequenceNumber, SlotSequenceNumber, AbnormalFlag1, AbnormalFlag2, AbnormalFlag3, AbnormalFlag4, AbnormalFlag5, AbnormalFlag6, AbnormalFlag7, AbnormalFlag8, WorkOrderID, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendSpecialCodeRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CuttingRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public void SendCuttingRequestReply(string eqpName, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, ReturnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendCuttingRequestReply(eqpName, ReturnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendCuttingRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// RecipeChangeReportReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public void SendRecipeChangeReportReply(string eqpName, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, ReturnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendRecipeChangeReportReply(eqpName, ReturnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendRecipeChangeReportReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// BoxWeightCheckRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public void SendBoxWeightCheckRequestReply(string eqpName, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, ReturnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendBoxWeightCheckRequestReply(eqpName, ReturnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendBoxWeightCheckRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
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
        public void SendOCIDRequestReply(string eqpName, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string OCID, string DateTime, string ModelType, string Version, string ReturnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, JobID, LotSequenceNumber, SlotSequenceNumber, OCID, DateTime, ModelType, Version, ReturnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendOCIDRequestReply(eqpName, JobID, LotSequenceNumber, SlotSequenceNumber, OCID, DateTime, ModelType, Version, ReturnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendOCIDRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// SamplingRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        public void SendSamplingRequestReply(string eqpName, string BatchQty, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, BatchQty, transactionID }));
                #endregion

                PLCCmdExecutor.SendSamplingRequestReply(eqpName, BatchQty, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendSamplingRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendRecipeParameterRequestCommand(string eqpName, string recipeNumber, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, recipeNumber, transactionID }));
                #endregion

                PLCCmdExecutor.SendRecipeParameterRequestCommand(eqpName, recipeNumber, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendRecipeParameterRequestCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendPalletInfoRequestReply(string eqpName, string palletID, string boxQTY, string reserveQTY, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, palletID, boxQTY, reserveQTY, transactionID }));
                #endregion

                PLCCmdExecutor.SendPalletInfoRequestReply(eqpName, palletID, boxQTY, reserveQTY, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPalletInfoRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }

        #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
        public void Terminate()
        {
            try
            {
                PLCCmdExecutor.Terminate();
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[Terminate]:Error：{0}+++", ex);
            }
        }
        #endregion

        #endregion

        #region Yuan
        public void SendPanelJudgeDataDownloadRequestReply(string eqpName, int returnCode, GlassInfo glassInfo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, returnCode, glassInfo, transactionID }));
                #endregion

                PLCCmdExecutor.SendPanelJudgeDataDownloadRequestReply(eqpName, returnCode, glassInfo, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPanelJudgeDataDownloadRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendMaterialValidationRequestReply(string eqpName, int requestNo, string returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, requestNo, returnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendMaterialValidationRequestReply(eqpName, requestNo, returnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendMaterialValidationRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendMaterialLotInfoRequestReply(string eqpName, int requestNo, string materrialCarrierID, List<MaterialInfo> materialList, int returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, requestNo, materrialCarrierID, materialList, transactionID }));
                #endregion

                PLCCmdExecutor.SendMaterialLotInfoRequestReply(eqpName, requestNo, materrialCarrierID, materialList, returnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendMaterialLotInfoRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendCassetteControlCommand(string eqpName, string portNo, EnumCassetteControlCommand commandType, string jobExistenceSlot, string jobCountforProcess, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, portNo, commandType, jobExistenceSlot, jobCountforProcess, transactionID }));
                #endregion
                PLCCmdExecutor.SendCassetteControlCommand(eqpName, portNo, commandType, jobExistenceSlot, jobCountforProcess, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendCassetteControlCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendRobotControlCommand(string eqpName, string localNo, RobotCommand cmd, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, localNo, cmd, transactionID }));
                #endregion

                PLCCmdExecutor.SendRobotControlCommand(eqpName, localNo, cmd, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendRobotControlCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void CassetteMapDownloadCommand(string eqpName, string localNo, int eqpCommandType, List<GlassInfo> glassInfoList, string portNo, int capacity, string jobExistenceSlot, string jobCountInCassette, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, localNo, eqpCommandType, glassInfoList, portNo, capacity, jobExistenceSlot, jobCountInCassette, transactionID }));
                #endregion

                int iMapDownLoadDelay = 0;
                if (HostInfo.Current.SystemSetting.Any(c => c.bckey == "MapDownLoadDelay"))
                {
                    var MapDownLoadDelay = HostInfo.Current.SystemSetting.FirstOrDefault(c => c.bckey == "MapDownLoadDelay").bcvalue;

                    int.TryParse(MapDownLoadDelay, out iMapDownLoadDelay);
                }

                if (eqpCommandType == 1)//PLC
                {
                    var downres = eqpCmd.CassetteMapDownloadCommand(eqpName, glassInfoList, portNo, capacity, iMapDownLoadDelay);
                    #region 需求7 1.下账失败则不on bit liuyusen 20221013
                    //if (iMapDownLoadDelay > 0)
                    //    Thread.Sleep(iMapDownLoadDelay);
                    if (!downres)
                    {
                        BCLog.Info($"PLC MapDownload Error: EQP:[{eqpName}] Port:[{portNo}]");
                        return;
                    }
                    #endregion
                    PLCCmdExecutor.SendCassetteMapDownloadCommand(eqpName, portNo, jobExistenceSlot, jobCountInCassette, transactionID);
                }
                else//EIP
                {
                    var downres = PLCCmdExecutor.SendCassetteMapDownloadWordCommand(eqpName, localNo, portNo, glassInfoList, capacity, iMapDownLoadDelay);
                    #region 需求7 1.下账失败则不on bit liuyusen 20221013
                    //if (iMapDownLoadDelay > 0)
                    //    Thread.Sleep(iMapDownLoadDelay);
                    if (!downres)
                    {
                        BCLog.Info($"EIP MapDownload Error: EQP:[{eqpName}] Port:[{portNo}]");
                        return;
                    }
                    #endregion
                    PLCCmdExecutor.SendCassetteMapDownloadCommand(eqpName, portNo, jobExistenceSlot, jobCountInCassette, transactionID);
                }
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[CassetteMapDownloadCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        #endregion

        #region Anemone
        public void SendPanelProcessEndRequestReply(string eqpName, string jobId, string returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, jobId, returnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendPanelProcessEndRequestReply(eqpName, jobId, returnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendPanelProcessEndRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public void SendMaterialStatusChangeReportReply(string eqpName, int i, string returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("BC=>EQP", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { eqpName, i, returnCode, transactionID }));
                #endregion

                PLCCmdExecutor.SendMaterialStatusChangeReportReply(eqpName, i, returnCode, transactionID);
            }
            catch (Exception ex)
            {
                BCLog.ErrorFormat("+++[SendMaterialStatusChangeReportReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }


    }
}
#endregion