using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Glorysoft.BC.Entity;
using Glorysoft.EIPDriver;
using log4net;

namespace Glorysoft.BC.EIP
{
    public class PLCCmdExecutor
    {
        private static readonly PLCContexts Contexts = PLCContexts.Current;
        private static readonly ILog Logger = Entity.LogHelper.EIPLog;

        #region matti

        /// <summary>
        /// SendJobDataRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="jobdata"></param>
        /// <param name="JobDataRequestAck">1: Job Data Exist  2: Job Data do not Exist</param>
        /// 
        public static void SendJobDataRequestReply(string eqpName, JobDataInfo jobdata, string JobDataRequestAck, string transactionID = "")
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                var plcMsg = new PLCMessage { Name = "JobDataRequestReplyBlock" };
                SetPLCMsgJobdata(ref jobdata, ref plcMsg);
                plcMsg.ItemCollection.Add(PLCEventItem.JobDataRequestAck, JobDataRequestAck);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, dtNow.ToString("yyyy"));
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendJobDataRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }

        public static void SendCheckLotBindingRequestReply(string eqpName, string returnCode, string NGType, string transactionID)
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "CheckLotBindingRequestReplyBlock" };
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode,returnCode);
                plcMsg.ItemCollection.Add(PLCEventItem.NGType, NGType);
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCheckLotBindingRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }

        public static void SetPLCMsgJobdata(ref JobDataInfo jobdata, ref PLCMessage plcMsg)
        {
            //if (!string.IsNullOrEmpty(jobdata.PRODID))
            plcMsg.ItemCollection.Add(PLCEventItem.PRODID, jobdata.PRODID);
            //if (!string.IsNullOrEmpty(jobdata.OperID))
            plcMsg.ItemCollection.Add(PLCEventItem.OperID, jobdata.OperID);
            //if (!string.IsNullOrEmpty(jobdata.LotID))
            plcMsg.ItemCollection.Add(PLCEventItem.LotID, jobdata.LotID);
            //if (!string.IsNullOrEmpty(jobdata.PPID1))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID1, "0");
            //if (!string.IsNullOrEmpty(jobdata.PPID2))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID2, jobdata.PPID1);
            //if (!string.IsNullOrEmpty(jobdata.PPID3))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID3, jobdata.PPID2);
            //if (!string.IsNullOrEmpty(jobdata.PPID4))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID4, jobdata.PPID3);
            //if (!string.IsNullOrEmpty(jobdata.PPID5))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID5, jobdata.PPID4);
            //if (!string.IsNullOrEmpty(jobdata.PPID6))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID6, jobdata.PPID5);
            //if (!string.IsNullOrEmpty(jobdata.PPID7))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID7, jobdata.PPID6);
            //if (!string.IsNullOrEmpty(jobdata.PPID8))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID8, jobdata.PPID7);
            //if (!string.IsNullOrEmpty(jobdata.PPID9))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID9, jobdata.PPID8);
            //if (!string.IsNullOrEmpty(jobdata.PPID10))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID10, jobdata.PPID9);
            //if (!string.IsNullOrEmpty(jobdata.PPID11))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID11, jobdata.PPID10);
            //if (!string.IsNullOrEmpty(jobdata.PPID12))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID12, jobdata.PPID11);
            //if (!string.IsNullOrEmpty(jobdata.PPID13))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID13, jobdata.PPID12);
            //if (!string.IsNullOrEmpty(jobdata.PPID14))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID14, jobdata.PPID13);
            //if (!string.IsNullOrEmpty(jobdata.PPID15))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID15, jobdata.PPID14);
            //if (!string.IsNullOrEmpty(jobdata.PPID16))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID16, jobdata.PPID15);
            //if (!string.IsNullOrEmpty(jobdata.PPID17))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID17, jobdata.PPID16);
            //if (!string.IsNullOrEmpty(jobdata.PPID18))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID18, jobdata.PPID17);
            //if (!string.IsNullOrEmpty(jobdata.PPID19))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID19, jobdata.PPID18);
            //if (!string.IsNullOrEmpty(jobdata.PPID20))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID20, jobdata.PPID19);
            //if (!string.IsNullOrEmpty(jobdata.PPID21))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID21, jobdata.PPID20);
            //if (!string.IsNullOrEmpty(jobdata.PPID22))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID22, jobdata.PPID21);
            //if (!string.IsNullOrEmpty(jobdata.PPID23))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID23, jobdata.PPID22);
            //if (!string.IsNullOrEmpty(jobdata.PPID24))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID24, jobdata.PPID23);
            //if (!string.IsNullOrEmpty(jobdata.PPID25))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID25, jobdata.PPID24);
            //if (!string.IsNullOrEmpty(jobdata.PPID26))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID26, jobdata.PPID25);
            //if (!string.IsNullOrEmpty(jobdata.PPID27))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID27, jobdata.PPID26);
            //if (!string.IsNullOrEmpty(jobdata.PPID28))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID28, jobdata.PPID27);
            //if (!string.IsNullOrEmpty(jobdata.PPID29))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID29, jobdata.PPID28);
            //if (!string.IsNullOrEmpty(jobdata.PPID30))
            plcMsg.ItemCollection.Add(PLCEventItem.PPID30, jobdata.PPID29);
            //if (!string.IsNullOrEmpty(jobdata.JobType))
            plcMsg.ItemCollection.Add(PLCEventItem.JobType, jobdata.JobType);
            //if (!string.IsNullOrEmpty(jobdata.JobID))
            plcMsg.ItemCollection.Add(PLCEventItem.JobID, jobdata.JobID);
            //if (!string.IsNullOrEmpty(jobdata.LotSequenceNumber))
            plcMsg.ItemCollection.Add(PLCEventItem.LotSequenceNumber, jobdata.LotSequenceNumber);
            //if (!string.IsNullOrEmpty(jobdata.SlotSequenceNumber))
            plcMsg.ItemCollection.Add(PLCEventItem.SlotSequenceNumber, jobdata.SlotSequenceNumber);
            //if (!string.IsNullOrEmpty(jobdata.PropertyCode))
            plcMsg.ItemCollection.Add(PLCEventItem.PropertyCode, jobdata.PropertyCode);
            //if (!string.IsNullOrEmpty(jobdata.JobJudgeCode))
            plcMsg.ItemCollection.Add(PLCEventItem.JobJudgeCode, jobdata.JobJudgeCode);
            //if (!string.IsNullOrEmpty(jobdata.JobGradeCode))
            plcMsg.ItemCollection.Add(PLCEventItem.JobGradeCode, jobdata.JobGradeCode);
            //if (!string.IsNullOrEmpty(jobdata.SubstrateType))
            plcMsg.ItemCollection.Add(PLCEventItem.SubstrateType, jobdata.SubstrateType);
            //if (!string.IsNullOrEmpty(jobdata.ProcessingFlag1))
            plcMsg.ItemCollection.Add(PLCEventItem.ProcessingFlag1, jobdata.ProcessingFlag1);
            //if (!string.IsNullOrEmpty(jobdata.ProcessingFlag2))
            plcMsg.ItemCollection.Add(PLCEventItem.ProcessingFlag2, jobdata.ProcessingFlag2);
            //if (!string.IsNullOrEmpty(jobdata.ProcessingFlag3))
            plcMsg.ItemCollection.Add(PLCEventItem.ProcessingFlag3, jobdata.ProcessingFlag3);
            //if (!string.IsNullOrEmpty(jobdata.SkipFlag1))
            plcMsg.ItemCollection.Add(PLCEventItem.SkipFlag1, jobdata.SkipFlag1);
            //if (!string.IsNullOrEmpty(jobdata.SkipFlag2))
            plcMsg.ItemCollection.Add(PLCEventItem.SkipFlag2, jobdata.SkipFlag2);
            //if (!string.IsNullOrEmpty(jobdata.SkipFlag3))
            plcMsg.ItemCollection.Add(PLCEventItem.SkipFlag3, jobdata.SkipFlag3);
            //if (!string.IsNullOrEmpty(jobdata.GlassThickness))
            plcMsg.ItemCollection.Add(PLCEventItem.GlassThickness, jobdata.GlassThickness);
            //if (!string.IsNullOrEmpty(jobdata.JobAngle))
            plcMsg.ItemCollection.Add(PLCEventItem.JobAngle, jobdata.JobAngle);
            //if (!string.IsNullOrEmpty(jobdata.JobFlip))
            plcMsg.ItemCollection.Add(PLCEventItem.JobFlip, jobdata.JobFlip);
            //if (!string.IsNullOrEmpty(jobdata.MMGCode))
            plcMsg.ItemCollection.Add(PLCEventItem.MMGCode, jobdata.MMGCode);
            //if (!string.IsNullOrEmpty(jobdata.PanelInchSizeX))
            plcMsg.ItemCollection.Add(PLCEventItem.PanelInchSizeX, jobdata.PanelInchSizeX);
            //if (!string.IsNullOrEmpty(jobdata.PanelInchSizeY))
            plcMsg.ItemCollection.Add(PLCEventItem.PanelInchSizeY, jobdata.PanelInchSizeY);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag1))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag1, jobdata.AbnormalFlag1);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag2))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag2, jobdata.AbnormalFlag2);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag3))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag3, jobdata.AbnormalFlag3);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag4))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag4, jobdata.AbnormalFlag4);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag5))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag5, jobdata.AbnormalFlag5);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag6))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag6, jobdata.AbnormalFlag6);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag7))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag7, jobdata.AbnormalFlag7);
            //if (!string.IsNullOrEmpty(jobdata.AbnormalFlag8))
            plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag8, jobdata.AbnormalFlag8);
            //if (!string.IsNullOrEmpty(jobdata.WorkOrderID))
            plcMsg.ItemCollection.Add(PLCEventItem.WorkOrderID, jobdata.WorkOrderID);
        }
        public static void SendDateTimeRequestReply(string eqpName, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "DateTimeRequestReplyBlock" };
                DateTime dtNow = DateTime.Now;
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeYear, dtNow.ToString("yyyy"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeMonth, dtNow.ToString("MM"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeDay, dtNow.ToString("dd"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeHour, dtNow.ToString("HH"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeMinute, dtNow.ToString("mm"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeSecond, dtNow.ToString("ss"));
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendDateTimeRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public static void SendDateTimeSetCommand(string eqpName, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "DateTimeSetCommandBlock" };
                DateTime dtNow = DateTime.Now;
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeYear, dtNow.ToString("yyyy"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeMonth, dtNow.ToString("MM"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeDay, dtNow.ToString("dd"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeHour, dtNow.ToString("HH"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeMinute, dtNow.ToString("mm"));
                plcMsg.ItemCollection.Add(PLCEventItem.DateTimeSecond, dtNow.ToString("ss"));
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendDateTimeSetCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
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
        public static void SendCIMMessageSetCommand(string eqpName, string CIMMessageType, string CIMMessageID, string TouchPanelNumber, string CIMMessageData, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "CIMMessageSetCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.CIMMessageType, CIMMessageType);//1-Error 2-Warming 3-Information
                plcMsg.ItemCollection.Add(PLCEventItem.CIMMessageID, CIMMessageID);
                plcMsg.ItemCollection.Add(PLCEventItem.TouchPanelNumber, TouchPanelNumber);//0-all panel
                plcMsg.ItemCollection.Add(PLCEventItem.CIMMessageData, CIMMessageData);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendDateTimeSetCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CIMMessageClearCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMessageID"></param>
        /// <param name="TouchPanelNo">0-all panel</param>
        public static void SendCIMMessageClearCommand(string eqpName, string CIMMessageID, string TouchPanelNo, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "CIMMessageClearCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.CIMMessageID, CIMMessageID);
                plcMsg.ItemCollection.Add(PLCEventItem.TouchPanelNo, TouchPanelNo);//0-all panel
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCIMMessageClearCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
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
        public static void SendCVReportTimeChangeCommand(string eqpName, string CVReportEnableMode, string CycleType, string CVReportFrequencyMinute, string CVReportHour1, string CVReportMinute1, string CVReportHour2, string CVReportMinute2, string CVReportHour3, string CVReportMinute3, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "CVReportTimeChangeCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.CVReportEnableMode, CVReportEnableMode);//1-Enable Mode 2-Disable Mode
                //plcMsg.ItemCollection.Add(PLCEventItem.CycleType, CycleType);//1-By time 2-By frequency
                plcMsg.ItemCollection.Add("CVReportTime", CVReportFrequencyMinute);// By frequency  1-65535
                //plcMsg.ItemCollection.Add(PLCEventItem.CVReportHour1, CVReportHour1);//By time 00-23
                //plcMsg.ItemCollection.Add(PLCEventItem.CVReportMinute1, CVReportMinute1);// By time 00-59
                //plcMsg.ItemCollection.Add(PLCEventItem.CVReportHour2, CVReportHour2);//By time 00-23
                //plcMsg.ItemCollection.Add(PLCEventItem.CVReportMinute2, CVReportMinute2);// By time 00-59
                //plcMsg.ItemCollection.Add(PLCEventItem.CVReportHour3, CVReportHour3);//By time 00-23
                //plcMsg.ItemCollection.Add(PLCEventItem.CVReportMinute3, CVReportMinute3);// By time 00-59
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCVReportTimeChangeCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CIMModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="CIMMode">1-off 2-on</param>
        public static void SendCIMModeChangeCommand(string eqpName, string CIMMode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "CIMModeChangeCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.CIMMode, CIMMode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCIMModeChangeCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// PortTypeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortType"></param>
        public static void SendPortTypeChangeCommand(string eqpName, int i, string PortType, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortTypeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PortType", PortType);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortTypeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortTypeAutoChangeModeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortType"></param>
        public static void SendPortTypeAutoChangeModeCommand(string eqpName, int i, string PortType, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortTypeAutoChangeModeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PortTypeAutoChangeMode", PortType);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortTypeAutoChangeModeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortMode"></param>
        public static void SendPortModeChangeCommand(string eqpName, int i, string PortMode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortModeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PortMode", PortMode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortEnableModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortEnableMode">1-Enable 2-Disable</param>
        public static void SendPortEnableModeChangeCommand(string eqpName, int i, string PortEnableMode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "EnableModeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "EnableMode", PortEnableMode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortEnableModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortTransferModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortTransferMode">1-MGV Mode 2-AGV Mode 3-Stocker Inline Mode</param>
        public static void SendPortTransferModeChangeCommand(string eqpName, int i, string PortTransferMode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortTransferModeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "TransferMode", PortTransferMode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortTransferModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortPauseModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortPauseMode">1-Paused 2-Normal</param>
        public static void SendPortPauseModeChangeCommand(string eqpName, int i, string PortPauseMode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PauseModeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PauseMode", PortPauseMode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortPauseModeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortGradeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortGrade"></param>
        public static void SendPortGradeChangeCommand(string eqpName, int i, string PortGrade, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortGradeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PortGrade", PortGrade);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortGradeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortCassetteTypeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortCassetteType"></param>
        public static void SendPortCassetteTypeChangeCommand(string eqpName, int i, string PortCassetteType, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortCassetteTypeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PortCassetteType", PortCassetteType);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortCassetteTypeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortQTimeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortQTime"></param>
        public static void SendPortQTimeChangeCommand(string eqpName, int i, string PortQTime, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "QTimeChangeCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "QTime", PortQTime);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortQTimeChangeCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// MachineModeChangeCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="MachineMode"></param>
        public static void SendMachineModeChangeCommand(string eqpName, string MachineMode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "MachineModeChangeCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.MachineMode, MachineMode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendMachineModeChangeCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// SamplingDownloadCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        public static void SendSamplingDownloadCommand(string eqpName, string BatchQty, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "SamplingDownloadCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.BatchQty, BatchQty);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendSamplingDownloadCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// DVSamplingFlagCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="BatchQty"></param>
        public static void SendDVSamplingFlagCommand(string eqpName, string BatchQty, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "DVSamplingFlagCommandBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.BatchQty, BatchQty);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendDVSamplingFlagCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
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
        public static void SendSpecialCodeRequestReply(string eqpName, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string AbnormalFlag1, string AbnormalFlag2, string AbnormalFlag3, string AbnormalFlag4, string AbnormalFlag5, string AbnormalFlag6, string AbnormalFlag7, string AbnormalFlag8, string WorkOrderID, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "SpecialCodeRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.JobID, JobID);
                plcMsg.ItemCollection.Add(PLCEventItem.LotSequenceNumber, LotSequenceNumber);
                plcMsg.ItemCollection.Add(PLCEventItem.SlotSequenceNumber, SlotSequenceNumber);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag1, AbnormalFlag1);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag2, AbnormalFlag2);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag3, AbnormalFlag3);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag4, AbnormalFlag4);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag5, AbnormalFlag5);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag6, AbnormalFlag6);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag7, AbnormalFlag7);
                plcMsg.ItemCollection.Add(PLCEventItem.AbnormalFlag8, AbnormalFlag8);
                plcMsg.ItemCollection.Add(PLCEventItem.WorkOrderID, WorkOrderID);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendSpecialCodeRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// CuttingRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public static void SendCuttingRequestReply(string eqpName, string ReturnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "CuttingRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, ReturnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCuttingRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// RecipeChangeReportReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public static void SendRecipeChangeReportReply(string eqpName, string ReturnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "RecipeChangeReportReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, ReturnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendRecipeChangeReportReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// BoxWeightCheckRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public static void SendBoxWeightCheckRequestReply(string eqpName, string ReturnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "BoxWeightCheckRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, ReturnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendBoxWeightCheckRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
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
        public static void SendOCIDRequestReply(string eqpName, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string OCID, string DateTime, string ModelType, string Version, string ReturnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "OCIDRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.JobID, JobID);
                plcMsg.ItemCollection.Add(PLCEventItem.LotSequenceNumber, LotSequenceNumber);
                plcMsg.ItemCollection.Add(PLCEventItem.SlotSequenceNumber, SlotSequenceNumber);
                plcMsg.ItemCollection.Add(PLCEventItem.OCID, OCID);
                plcMsg.ItemCollection.Add(PLCEventItem.DateTime, DateTime);
                plcMsg.ItemCollection.Add(PLCEventItem.ModelType, ModelType);
                plcMsg.ItemCollection.Add(PLCEventItem.Version, Version);
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, ReturnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendOCIDRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// PortControlCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="PortControlCommandCode">1-Chuck 2-Unchuck</param>
        public static void SendPortControlCommand(string eqpName, int i, string PortControlCommandCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "PortControlCommandBlock" };
                plcMsg.ItemCollection.Add("Port" + i.ToString() + "PortControlCommandCode", PortControlCommandCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortControlCommand]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }
        /// <summary>
        /// PortBoxInfoRequestReply
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="i">portno</param>
        /// <param name="ReturnCode">1-OK 2-NG</param>
        public static void SendPortBoxInfoRequestReply(string eqpName, int i, string ReturnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "Port" + i.ToString() + "BoxInfoRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, ReturnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPortBoxInfoRequestReply]:EQPName:{0},PortNo:{1},Error：{2}+++", eqpName, i.ToString(), ex);
            }
        }

        #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
        public static void Terminate()
        {
            try
            {
                Contexts.Terminate();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[Terminate]:Error：{0}+++", ex);
            }
        }
        #endregion

        #endregion

        #region Yuan 
        public static void SendPalletInfoRequestReply(string eqpName, string palletID, string boxQTY, string reserveQTY, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "PalletInfoRequestReplyBlock" };
                DateTime dtNow = DateTime.Now;
                plcMsg.ItemCollection.Add(PLCEventItem.PalletID, palletID);
                plcMsg.ItemCollection.Add(PLCEventItem.BoxQTY, boxQTY);
                plcMsg.ItemCollection.Add(PLCEventItem.ReserveQTY, reserveQTY);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPalletInfoRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public static void SendRecipeParameterRequestCommand(string eqpName, string recipeNumber, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "RecipeParameterRequestCommandBlock" };
                DateTime dtNow = DateTime.Now;
                plcMsg.ItemCollection.Add(PLCEventItem.RecipeNumber, recipeNumber);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendSamplingDownloadCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public static void SendSamplingRequestReply(string eqpName, string BatchQty, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "SamplingRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.BatchQty, BatchQty);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendSamplingDownloadCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public static void SendPanelJudgeDataDownloadRequestReply(string eqpName, int returnCode, GlassInfo glassInfo, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "PanelJudgeDataDownloadRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.JobID, glassInfo.GlassID);
                plcMsg.ItemCollection.Add(PLCEventItem.LotSequenceNumber, glassInfo.CassetteSequenceNo.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SlotSequenceNumber, glassInfo.SlotSequenceNo.ToString());
                var subJudge = glassInfo.PanelJudge;
                //Judge 赋值
                if (!string.IsNullOrEmpty(subJudge))
                {
                    //int i = 0;
                    //var gradeList = subGrade.Split('/');
                    for (int i = 0; i < subJudge.Length; i++)
                    {
                        //var newGrade = grade.ToString();//grade.Length > 2 ? grade.Substring(0, 2) : grade;
                        plcMsg.ItemCollection.Add("PanelJudgeData" + (i + 1).ToString(), subJudge[i].ToString());
                    }
                    plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, returnCode.ToString());
                }
                else
                {
                    plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, "2");
                }

                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCIMMessageClearCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }


        public static void SendMaterialValidationRequestReply(string eqpName, int requestNo, string returnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = $"MaterialValidationRequest{requestNo}ReplyBlock" };
                plcMsg.ItemCollection.Add("ReturnCode", returnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendMaterialValidationRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public static void SendMaterialLotInfoRequestReply(string eqpName, int requestNo, string materrialCarrierID, List<MaterialInfo> materialList, int returnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = $"MaterialLotInfoRequest{(requestNo == 0 ? "" : requestNo.ToString())}ReplyBlock" };

                plcMsg.ItemCollection.Add(PLCEventItem.MaterrialCarrierID, materrialCarrierID);

                int i = 1;
                foreach (var material in materialList)
                {
                    plcMsg.ItemCollection.Add(PLCEventItem.MaterialID + i, material.MaterialID);
                    plcMsg.ItemCollection.Add(PLCEventItem.MaterialCount + i, material.MaterialQty.ToString());
                    plcMsg.ItemCollection.Add(PLCEventItem.MaterialQTime + i, material.MaterialQTime.ToString());
                    i++;
                }
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, returnCode.ToString());
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCIMMessageClearCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// SendRobotControlCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="localNo"></param>
        /// <param name="cmd"></param>
        /// <param name="transactionID"></param>
        public static void SendRobotControlCommand(string eqpName, string localNo, RobotCommand cmd, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = PLCCommandName.RobotControlCommandBlock };
                plcMsg.ItemCollection.Add(PLCEventItem.CommandSequenceNumber, cmd.SequenceNo.ToString());

                plcMsg.ItemCollection.Add(PLCEventItem.FirstRCMD, cmd.STRCMD1.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstArmNumber, cmd.STArmNo1.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstGetPosition, cmd.STGetPosition1.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstPutPosition, cmd.STPutPosition1.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstGetSlotNumber, cmd.STGetSlotNo1.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstPutSlotNumber, cmd.STPutSlotNo1.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstSuCIMommand, cmd.STSubCommand1.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstGetSlotPosition, cmd.STGetSlotPostion1.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FirstPutSlotPosition, cmd.STPutSlotPostion1.ToString());

                plcMsg.ItemCollection.Add(PLCEventItem.SecondRCMD, cmd.NDRCMD2.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondArmNumber, cmd.NDArmNo2.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondGetPosition, cmd.NDGetPosition2.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondPutPosition, cmd.NDPutPosition2.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondGetSlotNumber, cmd.NDGetSlotNo2.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondPutSlotNumber, cmd.NDPutSlotNo2.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondSuCIMommand, cmd.NDSubCommand2.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondGetSlotPosition, cmd.NDGetSlotPostion2.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.SecondPutSlotPosition, cmd.NDPutSlotPostion2.ToString());

                plcMsg.ItemCollection.Add(PLCEventItem.ThirdRCMD, cmd.RDRCMD3.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdArmNumber, cmd.RDArmNo3.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdGetPosition, cmd.RDGetPosition3.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdPutPosition, cmd.RDPutPosition3.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdGetSlotNumber, cmd.RDGetSlotNo3.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdPutSlotNumber, cmd.RDPutSlotNo3.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdSuCIMommand, cmd.RDSubCommand3.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdGetSlotPosition, cmd.RDGetSlotPostion3.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.ThirdPutSlotPosition, cmd.RDPutSlotPostion3.ToString());

                plcMsg.ItemCollection.Add(PLCEventItem.FourthRCMD, cmd.THRCMD4.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthArmNumber, cmd.THArmNo4.GetHashCode().ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthGetPosition, cmd.THGetPosition4.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthPutPosition, cmd.THPutPosition4.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthGetSlotNumber, cmd.THGetSlotNo4.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthPutSlotNumber, cmd.THPutSlotNo4.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthSuCIMommand, cmd.THSubCommand4.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthGetSlotPosition, cmd.THGetSlotPostion4.ToString());
                plcMsg.ItemCollection.Add(PLCEventItem.FourthPutSlotPosition, cmd.THPutSlotPostion4.ToString());
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);

            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCIMMessageClearCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// SendCassetteMapDownloadCommand
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="portNo"></param>
        /// <param name="jobExistenceSlot"></param>
        /// <param name="jobCountInCassette"></param>
        public static void SendCassetteMapDownloadCommand(string eqpName, string portNo, string jobExistenceSlot, string jobCountInCassette, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = $"Port{portNo}CassetteMapDownloadCommandBlock" };
                int slotcount = jobExistenceSlot.Length / 16;
                for (int i = 0; i < slotcount; i++)
                {
                    plcMsg.ItemCollection.Add($"Port{portNo}{PLCEventItem.JobExistenceSlot}{i + 1}", jobExistenceSlot.Substring(i * 16, 16));
                }
                plcMsg.ItemCollection.Add($"Port{portNo}{PLCEventItem.JobCountInCassette}", jobCountInCassette);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCIMMessageClearCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        /// <summary>
        /// SendCassetteMapDownloadWordCommand   
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="localNo"></param>
        /// <param name="portNo"></param>
        /// <param name="glassInfoList"></param>
        /// <param name="capacity"></param>
        public static bool SendCassetteMapDownloadWordCommand(string eqpName, string localNo, string portNo, List<GlassInfo> glassInfoList, int capacity, int iMapDownLoadDelay)
        {
            #region 需求7 1.下账失败则不on bit liuyusen 20221013
            bool Result = false;
            #endregion
            try
            {
                Logger.ErrorFormat("+++[SendCassetteMapDownloadWordCommand]:Start-" + DateTime.Now.ToString());

                //BC_CIMToIDX_Port1CSTMapDL_01_02_01 
                string tagName = $"LC_CIMToIDX_Port{portNo}CSTMapDL_01_{localNo.PadLeft(2, '0')}_";
                //
                List<string> tagList = new List<string>();
                for (int i = 0; i < capacity; i++)
                {
                    var tag = tagName + (i + 1).ToString().PadLeft(2, '0');
                    var block = Contexts.GetBlock(eqpName, tagName + (i + 1).ToString().PadLeft(2, '0'));
                    Logger.ErrorFormat("+++[SendCassetteMapDownloadWordCommand]:Start" + DateTime.Now.ToString() + "-" + block.Name);
                    if (block != null)
                    {
                        var itemsCmd = block.BlockCollection[$"Port{portNo}CSTMapDownloadCommandBlock"];
                        Logger.ErrorFormat("+++[SendCassetteMapDownloadWordCommand]:Start" + DateTime.Now.ToString() + "-" + itemsCmd.Name);
                        if (glassInfoList.Any(c => c.Position == (i + 1)))
                        {
                            var glassInfo = glassInfoList.FirstOrDefault(c => c.Position == (i + 1));
                            SetJobDataBlock(itemsCmd, i + 1, glassInfo);
                        }
                        else
                            SetJobDataBlock(itemsCmd, i + 1, new GlassInfo());
                        Contexts.SendCommand(block);
                        tagList.Add(tag);
                    }
                }
                #region 需求7 1.下账失败则不on bit liuyusen 20221013
                if (iMapDownLoadDelay > 0)
                    Thread.Sleep(iMapDownLoadDelay);

                var AllTagRes = true;
                if (tagList != null && tagList.Count > 0)
                {
                    foreach (var tag in tagList)
                    {
                        var block = PLCContexts.Current.GetBlock(eqpName, tag);
                        if (block != null)
                        {
                            if (!block.ExcuteResult)
                            {
                                AllTagRes = false;
                                break;
                            }
                        }
                    }
                }
                Result = AllTagRes;
                #endregion
                Logger.ErrorFormat("+++[SendCassetteMapDownloadWordCommand]:End-" + DateTime.Now.ToString());

            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCassetteMapDownloadWordCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
            #region 需求7 1.下账失败则不on bit liuyusen 20221013
            return Result;
            #endregion
        }
        /// <summary>
        /// SetJobDataBlock
        /// </summary>
        /// <param name="ReplyData"></param>
        /// <param name="glassInfo"></param>
        private static void SetJobDataBlock(Block ReplyData, int i, GlassInfo glassInfo)
        {

            ReplyData.ItemCollection[PLCEventItem.PRODID].Value = HostInfo.Current.FormatString(glassInfo.ProductID);

            ReplyData.ItemCollection[PLCEventItem.OperID].Value = HostInfo.Current.FormatString(glassInfo.OperationID);

            ReplyData.ItemCollection[PLCEventItem.LotID].Value = HostInfo.Current.FormatString(glassInfo.LotID);

            ReplyData.ItemCollection[PLCEventItem.PPID1].Value = "0";

            ReplyData.ItemCollection[PLCEventItem.PPID2].Value = glassInfo.PPID1;

            ReplyData.ItemCollection[PLCEventItem.PPID3].Value = glassInfo.PPID2;

            ReplyData.ItemCollection[PLCEventItem.PPID4].Value = glassInfo.PPID3;

            ReplyData.ItemCollection[PLCEventItem.PPID5].Value = glassInfo.PPID4;

            ReplyData.ItemCollection[PLCEventItem.PPID6].Value = glassInfo.PPID5;

            ReplyData.ItemCollection[PLCEventItem.PPID7].Value = glassInfo.PPID6;

            ReplyData.ItemCollection[PLCEventItem.PPID8].Value = glassInfo.PPID7;

            ReplyData.ItemCollection[PLCEventItem.PPID9].Value = glassInfo.PPID8;

            ReplyData.ItemCollection[PLCEventItem.PPID10].Value = glassInfo.PPID9;

            ReplyData.ItemCollection[PLCEventItem.PPID11].Value = glassInfo.PPID10;

            ReplyData.ItemCollection[PLCEventItem.PPID12].Value = glassInfo.PPID11;

            ReplyData.ItemCollection[PLCEventItem.PPID13].Value = glassInfo.PPID12;

            ReplyData.ItemCollection[PLCEventItem.PPID14].Value = glassInfo.PPID13;

            ReplyData.ItemCollection[PLCEventItem.PPID15].Value = glassInfo.PPID14;

            ReplyData.ItemCollection[PLCEventItem.PPID16].Value = glassInfo.PPID15;

            ReplyData.ItemCollection[PLCEventItem.PPID17].Value = glassInfo.PPID16;

            ReplyData.ItemCollection[PLCEventItem.PPID18].Value = glassInfo.PPID17;

            ReplyData.ItemCollection[PLCEventItem.PPID19].Value = glassInfo.PPID18;

            ReplyData.ItemCollection[PLCEventItem.PPID20].Value = glassInfo.PPID19;

            ReplyData.ItemCollection[PLCEventItem.PPID21].Value = glassInfo.PPID20;

            ReplyData.ItemCollection[PLCEventItem.PPID22].Value = glassInfo.PPID21;

            ReplyData.ItemCollection[PLCEventItem.PPID23].Value = glassInfo.PPID22;

            ReplyData.ItemCollection[PLCEventItem.PPID24].Value = glassInfo.PPID23;

            ReplyData.ItemCollection[PLCEventItem.PPID25].Value = glassInfo.PPID24;

            ReplyData.ItemCollection[PLCEventItem.PPID26].Value = glassInfo.PPID25;

            ReplyData.ItemCollection[PLCEventItem.PPID27].Value = glassInfo.PPID26;

            ReplyData.ItemCollection[PLCEventItem.PPID28].Value = glassInfo.PPID27;

            ReplyData.ItemCollection[PLCEventItem.PPID29].Value = glassInfo.PPID28;

            ReplyData.ItemCollection[PLCEventItem.PPID30].Value = glassInfo.PPID29;

            ReplyData.ItemCollection[PLCEventItem.JobType].Value = glassInfo.JobType.ToString();

            ReplyData.ItemCollection[PLCEventItem.JobID].Value = HostInfo.Current.FormatString(glassInfo.GlassID);

            ReplyData.ItemCollection[PLCEventItem.LotSequenceNumber].Value = glassInfo.CassetteSequenceNo.ToString();

            ReplyData.ItemCollection[PLCEventItem.SlotSequenceNumber].Value = glassInfo.SlotSequenceNo.ToString();

            ReplyData.ItemCollection[PLCEventItem.PropertyCode].Value = glassInfo.PropertyCode;

            ReplyData.ItemCollection[PLCEventItem.JobJudgeCode].Value = HostInfo.Current.FormatString(glassInfo.GlassJudge);

            ReplyData.ItemCollection[PLCEventItem.JobGradeCode].Value = HostInfo.Current.FormatString(glassInfo.GlassGradeCode);

            ReplyData.ItemCollection[PLCEventItem.SubstrateType].Value = glassInfo.SubstrateType.ToString();

            ReplyData.ItemCollection[PLCEventItem.ProcessingFlag1].Value = "0";//TBD

            ReplyData.ItemCollection[PLCEventItem.ProcessingFlag2].Value = "0";//TBD

            ReplyData.ItemCollection[PLCEventItem.ProcessingFlag3].Value = "0";//TBD

            ReplyData.ItemCollection[PLCEventItem.SkipFlag1].Value = "0";//TBD

            ReplyData.ItemCollection[PLCEventItem.SkipFlag2].Value = "0";//TBD

            ReplyData.ItemCollection[PLCEventItem.SkipFlag3].Value = "0";//TBD

            ReplyData.ItemCollection[PLCEventItem.GlassThickness].Value = glassInfo.Thickness.ToString();

            ReplyData.ItemCollection[PLCEventItem.JobAngle].Value = "0";

            ReplyData.ItemCollection[PLCEventItem.JobFlip].Value = "0";

            ReplyData.ItemCollection[PLCEventItem.MMGCode].Value = "0";

            ReplyData.ItemCollection[PLCEventItem.PanelInchSizeX].Value = "0";

            ReplyData.ItemCollection[PLCEventItem.PanelInchSizeY].Value = "0";

            if (!String.IsNullOrEmpty(glassInfo.AbnormalCodes))
            {
                string code = GetAbnormalFlag(glassInfo.AbnormalCodes);
                var codes = code.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                for (int index = 0; index < codes.Length; index++)
                {
                    ReplyData.ItemCollection[string.Format("{0}{1}", PLCEventItem.AbnormalFlag, index + 1)].Value = codes[index];
                }
                //var abnormalCodeList = glassInfo.AbnormalCodes.Split(';');
                //for (int index = 0; index < 8; index++)
                //{
                //    string abnormalCode = "0";
                //    if (index < abnormalCodeList.Count())
                //        abnormalCode = abnormalCodeList[index];
                //    ReplyData.ItemCollection[string.Format("{0}{1}", PLCEventItem.AbnormalFlag, index + 1)].Value = abnormalCode;
                //}
            }
            //ReplyData.ItemCollection[PLCEventItem.WorkOrderID].Value = HostInfo.Current.FormatString(glassInfo.WorkOrder);
        }
        private static string GetAbnormalFlag(string AbnormalCodes)
        {
            string code = "";
            string abnormalflag1 = "";
            string abnormalflag2 = "";
            string abnormalflag3 = "";
            string abnormalflag4 = "";
            string abnormalflag5 = "";
            string abnormalflag6 = "";
            string abnormalflag7 = "";
            string abnormalflag8 = "";
            List<string> abcode = new List<string>();
            if (!String.IsNullOrEmpty(AbnormalCodes))
            {
                string[] codeids = AbnormalCodes.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (codeids.Length > 0)
                {
                    foreach (var abs in codeids)
                    {
                        var ab = abs.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        if (ab.Length == 1)//glass
                        {
                            abcode.Add(ab[0]);
                        }
                        else if (ab.Length == 2)//panel
                        {
                            abcode.Add(ab[1]);
                        }
                    }
                }
            }
            #region flag赋值
            for (int i = 16; i >= 1; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag1 += "1";
                else
                    abnormalflag1 += "0";
            }
            for (int i = 32; i >= 17; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag2 += "1";
                else
                    abnormalflag2 += "0";
            }
            for (int i = 48; i >= 33; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag3 += "1";
                else
                    abnormalflag3 += "0";
            }
            for (int i = 64; i >= 49; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag4 += "1";
                else
                    abnormalflag4 += "0";
            }
            for (int i = 80; i >= 65; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag5 += "1";
                else
                    abnormalflag5 += "0";
            }
            for (int i = 96; i >= 81; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag6 += "1";
                else
                    abnormalflag6 += "0";
            }
            for (int i = 112; i >= 97; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag7 += "1";
                else
                    abnormalflag7 += "0";
            }
            for (int i = 128; i >= 113; i--)
            {
                if (abcode.Contains(i.ToString()))
                    abnormalflag8 += "1";
                else
                    abnormalflag8 += "0";
            }
            #endregion
            code = abnormalflag1 + ";" + abnormalflag2 + ";" + abnormalflag3 + ";" + abnormalflag4 + ";" + abnormalflag5 + ";" + abnormalflag6 + ";" + abnormalflag7 + ";" + abnormalflag8;
            return code;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eqpName"></param>
        /// <param name="portNo"></param>
        /// <param name="jobExistenceSlot"></param>
        /// <param name="jobCountforProcess"></param>
        /// <param name="transactionID"></param>
        public static void SendCassetteControlCommand(string eqpName, string portNo, EnumCassetteControlCommand commandType, string jobExistenceSlot, string jobCountforProcess, string transactionID = "")
        {
            try
            {
                //Port1CassetteControlCommandBlock
                var plcMsg = new PLCMessage { Name = $"Port{portNo}CassetteControlCommandBlock" };
                plcMsg.ItemCollection.Add($"Port{portNo}{PLCEventItem.CassetteControlCommandCode}", ((int)commandType).ToString());
                int slotcount = string.IsNullOrEmpty(jobExistenceSlot) ? 0 : jobExistenceSlot.Length / 16;
                if (slotcount > 0)
                    for (int i = 0; i < slotcount; i++)
                    {
                        plcMsg.ItemCollection.Add($"{PLCEventItem.JobExistenceSlot}{i + 1}", jobExistenceSlot.Substring(i * 16, 16));
                    }
                plcMsg.ItemCollection.Add($"Port{portNo}{PLCEventItem.JobCountforProcess}", jobCountforProcess);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendCassetteControlCommand]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        #endregion

        #region
        public static void SendPanelProcessEndRequestReply(string eqpName, string jobid, string returnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = "PanelProcessEndRequestReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.JobID, jobid);
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, returnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendPanelProcessEndRequestReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        public static void SendMaterialStatusChangeReportReply(string eqpName, int i, string returnCode, string transactionID = "")
        {
            try
            {
                var plcMsg = new PLCMessage { Name = $"MaterialStatusChangeReport{i}ReplyBlock" };
                plcMsg.ItemCollection.Add(PLCEventItem.ReturnCode, returnCode);
                plcMsg.EQPName = eqpName;
                plcMsg.TransactionID = transactionID;
                Contexts.Send2PLC(plcMsg);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("+++[SendMaterialStatusChangeReportReply]:EQPName:{0},Error：{1}+++", eqpName, ex);
            }
        }
        #endregion
    }
}