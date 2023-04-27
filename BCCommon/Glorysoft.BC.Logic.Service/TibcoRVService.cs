using Glorysoft.BC.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.RV;
using Glorysoft.BC.Entity;
using System.Threading;
using Glorysoft.Auto.Contract;

namespace Glorysoft.BC.Logic.Service
{
    public class TibcoRVService : AbstractEventHandler, ITibcoRVService
    {
       
        #region Yuan
        public void SendToMESDefectAlarmReply(string eqpid, RVDefectAlarmReply mesBody,RVHeader replyHeader,object requestMessage)
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, "", "" }));
            #endregion
            SendMESMessage.SendToMESDefectAlarmReply(mesBody, replyHeader, requestMessage);
        }
        public void SendToMESSamplingDownloadReply(string eqpid, RVSamplingDownloadReply mesBody, RVHeader replyHeader, object requestMessage)
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, "", "" }));
            #endregion
            SendMESMessage.SendToMESSamplingDownloadReply(mesBody, replyHeader, requestMessage);
        }
        public void SendToMESSPCRateDownloadReply(string eqpid, RVSPCRateDownloadReply mesBody, RVHeader replyHeader, object requestMessage)
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, "", "" }));
            #endregion
            SendMESMessage.SendToMESSPCRateDownloadReply(mesBody, replyHeader, requestMessage);
        }
        public void SendToMESRecipeParameterRequestReply(string eqpid, RVRecipeParameterRequestReply mesBody, RVHeader replyHeader, object requestMessage)
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, "", "" }));
            #endregion
            SendMESMessage.SendToMESRecipeParameterRequestReply(mesBody, replyHeader, requestMessage);
        }
        public RVHeader SendToMESRecipeChangeReport(string eqpid, RVRecipeChangeReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var replyHeader = SendMESMessage.SendToMESRecipeChangeReport(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public void SendToMESPanelScrap(string eqpid, RVPanelScrap mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESPanelScrap(mesBody, transactionID);
        }
        public void SendToMESPanelOutByIndexReport(string eqpid, RVPanelOutByIndexReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESPanelOutByIndexReport(mesBody, transactionID);
        }
        public void SendToMESPanelInByIndexReport(string eqpid, RVPanelInByIndexReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESPanelInByIndexReport(mesBody, transactionID);
        }
        public void SendToMESChangeProcessMode(string eqpid, RVChangeProcessMode mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESChangeProcessMode(mesBody, transactionID);
        }
        public RVHeader SendToMESLotProcessEnd(string eqpid, RVLotProcessEnd mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var replyHeader = SendMESMessage.SendToMESLotProcessEnd(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public RVHeader SendToMESProcessStartRequest(string eqpid, RVProcessStartRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var replyHeader = SendMESMessage.SendToMESProcessStartRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public RVHeader SendToMESCarrierProcessStart(string eqpid, RVCarrierProcessStart mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var replyHeader = SendMESMessage.SendToMESCarrierProcessStart(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public void SendToMESLotProcessCancel(string eqpid, RVLotProcessCancel mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESLotProcessCancel(mesBody, transactionID);
        }
        public void SendToMESEmptyPalletReturnRequest(string eqpid, RVEmptyPalletReturnRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESEmptyPalletReturnRequest(mesBody, transactionID);
        }
        public void SendToMESLotProcessAbort(string eqpid, RVLotProcessAbort mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESLotProcessAbort(mesBody, transactionID);
        }
        public void SendToMESLabelInfoRequest(string eqpid, RVLabelInfoRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESLabelInfoRequest(mesBody, transactionID);
        }
        public void SendToMESLotProcessStart(string eqpid, RVLotProcessStart mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESLotProcessStart(mesBody, transactionID);
        }
        public void SendToMESPanelJudgeReport(string eqpid, RVPanelJudgeReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESPanelJudgeReport(mesBody, transactionID);
        }
        public void SendToMESWeightReport(string eqpid, RVWeightReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESWeightReport(mesBody, transactionID);
        }
        public void SendToMESSamplingFlagReport(string eqpid, RVSamplingFlagReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESSamplingFlagReport(mesBody, transactionID);
        }
        public void SendToMESAlarmReport(string eqpid, RVAlarmReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESAlarmReport(mesBody, transactionID);
        }
        public RVHeader SendToMESMaterialValidation(string eqpid, RVMaterialValidation mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var replyHeader = SendMESMessage.SendToMESMaterialValidation(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public void SendToMESMaterialUseReport(string eqpid, RVMaterialUseReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESMaterialUseReport(mesBody, transactionID);
        }
        public void SendToMESMaterialUnloadRequest(string eqpid, RVMaterialUnloadRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESMaterialUnloadRequest(mesBody, transactionID);
        }
        public void SendToMESMaterialUnloadComplete(string eqpid, RVMaterialUnloadComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESMaterialUnloadComplete(mesBody, transactionID);
        }
        public RVMaterialLotInfoRequestReply SendToMESMaterialLotInfoRequest(string eqpid, RVMaterialLotInfoRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var data = SendMESMessage.SendToMESMaterialLotInfoRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public RVSamplingRequestReply SendToMESSamplingRequest(string eqpid, RVSamplingRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var data = SendMESMessage.SendToMESSamplingRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public void SendToMESMaterialLoadRequest(string eqpid, RVMaterialLoadRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESMaterialLoadRequest(mesBody, transactionID);
        }
        public void SendToMESMaterialCountReport(string eqpid, RVMaterialCountReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESMaterialCountReport(mesBody, transactionID);
        }
        public RVHeader SendToMESRecipeCheckRequest(string eqpid, RVRecipeCheckRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var data = SendMESMessage.SendToMESRecipeCheckRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public RVCuttingBoxInfoRequestReply SendToMESCuttingBoxInfoRequest(string eqpid, RVCuttingBoxInfoRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var data = SendMESMessage.SendToMESCuttingBoxInfoRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public RVLotInfoRequestReply SendToMESLotInfoRequest(string eqpid, RVLotInfoRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var data = SendMESMessage.SendToMESLotInfoRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public RVCarrierInfoDownloadReply SendToMESCarrierInfoDownload(string eqpid, RVCarrierInfoDownload mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var data = SendMESMessage.SendToMESCarrierInfoDownload(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public void SendToMESLoadRequest(string eqpid, RVLoadRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESLoadRequest(mesBody, transactionID);
        }
        public void SendToMESBatchLoadRequest(string eqpid, RVBatchLoadRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESBatchLoadRequest(mesBody, transactionID);
        }
        public void SendToMESLoadComplete(string eqpid, RVLoadComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESLoadComplete(mesBody, transactionID);
        }
        public void SendToMESBatchLoadComplete(string eqpid, RVBatchLoadComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESBatchLoadComplete(mesBody, transactionID);
        }
        public void SendToMESCreateCarrierBatch(string eqpid, RVCreateCarrierBatch mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESCreateCarrierBatch(mesBody, transactionID);
        }
        public void SendToMESMultiBoxLoadComplete(string eqpid, RVMultiBoxLoadComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESMultiBoxLoadComplete(mesBody, transactionID);
        }
        public void SendToMESUnloadRequest(string eqpid, RVUnloadRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESUnloadRequest(mesBody, transactionID);
        }
        public void SendToMESBatchUnloadRequest(string eqpid, RVBatchUnloadRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESBatchUnloadRequest(mesBody, transactionID);
        }
        public void SendToMESUnloadComplete(string eqpid, RVUnloadComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESUnloadComplete(mesBody, transactionID);
        }
        public void SendToMESBatchUnloadComplete(string eqpid, RVBatchUnloadComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESBatchUnloadComplete(mesBody, transactionID);
        }
        public void SendToMESBoxPacking(string eqpid, RVBoxPacking mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESBoxPacking(mesBody, transactionID);
        }
        #endregion
        #region Matti
        public bool IsInLine()
        {
            return SendMESMessage.IsInLine();
        }
        public void SendToMESUnitOutReport(string eqpid, RVUnitOut mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESUnitOutReport(mesBody, transactionID);
        }
        public void SendToMESPanelInOutLine(string eqpid, RVPanelInOut mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESPanelInOutLine(mesBody, transactionID);
        }
        public RVPanelInfoDownloadResponse SendToMESPanelInfoDownload(string eqpid, RVPanelInfoDownload mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return null;
            var data = SendMESMessage.SendToMESPanelInfoDownload(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { data }));
            #endregion
            return data;
        }
        public void SendToMESProcessData(string eqpid, RVProcessData mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESProcessData(mesBody, transactionID);
        }
        public void SendToMESEquipmentStateReport(string eqpid, RVEquipmentState mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESEquipmentStateReport(mesBody, transactionID);
        }
        public void SendToMESUnitStateReport(string eqpid, RVUnitState mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESUnitStateReport(mesBody, transactionID);
        }
        public RVHeader SendToMESMaterialStateReport(string eqpid, RVMaterialState mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            var replyHeader = SendMESMessage.SendToMESMaterialStateReport(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public void SendToMESPortTypeReport(string eqpid, RVPortType mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESPortTypeReport(mesBody, transactionID);
        }
        public void SendToMESPortCSTTypeChangeReport(string eqpid, RVPortCSTType mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESPortCSTTypeChangeReport(mesBody, transactionID);
        }
        public void SendToMESVCRReadReport(string eqpid, RVVCRRead mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESVCRReadReport(mesBody, transactionID);
        }
        public RVHeader SendToMESPanelTrackInOutReport(string eqpid, RVPanelTrackInOut mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            var replyHeader = SendMESMessage.SendToMESPanelTrackInOutReport(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public RVHeader SendToMESCommunicationReport(string eqpid, RVCommunication mesBody, string transactionID = "")
        {
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            var replyHeader = SendMESMessage.SendToMESCommunicationReport(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public RVHeader SendToMESCuttingCompleteReport(string eqpid, RVCuttingComplete mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            var replyHeader = SendMESMessage.SendToMESCuttingCompleteReport(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public void SendToMESAreYouThere(string eqpid, RVAreYouThere mesBody, string transactionID = "")
        {
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESAreYouThere(mesBody, transactionID);
        }
        public void SendToMESEquipmentPortReport(string eqpid, RVPortState mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESEquipmentPortReport(mesBody, transactionID);
        }
        public void SendToMESDailyCheckDataReport(string eqpid, RVDailyCheckDataReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESDailyCheckDataReport(mesBody, transactionID);
        }
        public void SendToMESNGBufferInfoReport(string eqpid, RVNGBufferInfoReport mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESNGBufferInfoReport(mesBody, transactionID);
        }
        public void SendToMESPanelTrackInReport(string eqpid, RVPanelTrackIn mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESPanelTrackInReport(mesBody, transactionID);
        }
        public RVHeader SendToMESPanelTrackOutReport(string eqpid, RVPanelTrackOut mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            var replyHeader = SendMESMessage.SendToMESPanelTrackOutReport(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        public void SendToMESEQPMaterialQTYReport(string eqpid, RVEQPMaterialQTY mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            SendMESMessage.SendToMESEQPMaterialQTYReport(mesBody, transactionID);
        }
        public RVHeader SendToMESLotBindingAlias(string eqpid, RVLotBindingAlias mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            //if (HostInfo.Current.ControlState == ControlState.Offline) return;
            var replyHeader = SendMESMessage.SendToMESLotBindingAlias(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }

        public void SendToMESOperatorLoginReport(string eqpid, RVOperatorLogin mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            SendMESMessage.SendToMESOperatorLoginReport(mesBody, transactionID);
        }
        #endregion

        #region
        public NewRVHeader SendToMESCheckLotBindingRequest(string eqpid, RVCheckLotBindingRequest mesBody, string transactionID = "")
        {
            var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);
            if (oEQP.ControlState == EnumControlState.OffLine)
                return null;
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("BC=>MES", System.Reflection.MethodBase.GetCurrentMethod().Name,
                System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                new object[] { mesBody, transactionID }));
            #endregion
            var replyHeader = SendMESMessage.SendToMESCheckLotBindingRequest(mesBody, transactionID);
            #region 写日志
            LogHelper.BCLog.Info(WriteLog("MES=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                new string[] { "Return" },
                new object[] { replyHeader }));
            #endregion
            return replyHeader;
        }
        #endregion
    }
}
