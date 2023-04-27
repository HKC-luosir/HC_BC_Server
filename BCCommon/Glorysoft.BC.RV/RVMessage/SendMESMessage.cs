using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.BC.RV.Common;
using Glorysoft.BC.RV.RVService;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using TIBCO.Rendezvous;

namespace Glorysoft.BC.RV
{
    public class SendMESMessage
    {
        private static string[] s1 = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//字符2113列表5261
        private static readonly object syncConv = new object();
        public static string CreateTransactionID()
        {
            lock (syncConv)
            {
                Thread.Sleep(1);
                Random rand = new Random(DateTime.Now.Millisecond);//实例化rand
                string a = s1[rand.Next(0, s1.Length)];//label.text=数组4102s1[随机1653数]
                StringBuilder result = new StringBuilder();
                result.Append(DateTime.Now.ToString("yyyyMMddHHmmssfffff"));
                result.Append(a);
                return result.ToString();
            }
        }
        
        #region Yuan
        public static void SendToMESDefectAlarmReply(RVDefectAlarmReply mesBody,RVHeader replyHeader,object requestMessage)
        {
            var message = requestMessage as Message;
            SendReply(mesBody, mesBody.MessageName, replyHeader, message);
        }
        public static void SendToMESSamplingDownloadReply(RVSamplingDownloadReply mesBody, RVHeader replyHeader, object requestMessage)
        {
            var message = requestMessage as Message;
            SendReply(mesBody, mesBody.MessageName, replyHeader, message);
        }
        public static void SendToMESSPCRateDownloadReply(RVSPCRateDownloadReply mesBody, RVHeader replyHeader, object requestMessage)
        {
            var message = requestMessage as Message;
            SendReply(mesBody, mesBody.MessageName, replyHeader, message);
        }
        public static void SendToMESRecipeParameterRequestReply(RVRecipeParameterRequestReply mesBody, RVHeader replyHeader, object requestMessage)
        {
            var message = requestMessage as Message;
            SendReply(mesBody, mesBody.MessageName, replyHeader, message);
        }
        public static RVHeader SendToMESRecipeChangeReport(RVRecipeChangeReport mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            SendAsync<RVRecipeChangeReport>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            return replyHeader;
        }
        public static void SendToMESPanelScrap(RVPanelScrap mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPanelOutByIndexReport(RVPanelOutByIndexReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPanelInByIndexReport(RVPanelInByIndexReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESChangeProcessMode(RVChangeProcessMode mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESLotProcessEnd(RVLotProcessEnd mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            SendAsync<RVLotProcessEnd>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            return replyHeader;
        }
        public static RVHeader SendToMESProcessStartRequest(RVProcessStartRequest mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESCarrierProcessStart(RVCarrierProcessStart mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESLotProcessCancel(RVLotProcessCancel mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESEmptyPalletReturnRequest(RVEmptyPalletReturnRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESLotProcessAbort(RVLotProcessAbort mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESLabelInfoRequest(RVLabelInfoRequest mesBody, string transactionID = "")
        {
            Send(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESLotProcessStart(RVLotProcessStart mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPanelJudgeReport(RVPanelJudgeReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESWeightReport(RVWeightReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESSamplingFlagReport(RVSamplingFlagReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESAlarmReport(RVAlarmReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESMaterialValidation(RVMaterialValidation mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESMaterialUseReport(RVMaterialUseReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESMaterialUnloadRequest(RVMaterialUnloadRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESMaterialUnloadComplete(RVMaterialUnloadComplete mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVSamplingRequestReply SendToMESSamplingRequest(RVSamplingRequest mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            var mesReply = SendAsync<RVSamplingRequestReply>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            if (mesReply == null)
                mesReply = new RVSamplingRequestReply();
            mesReply.replyHeader = replyHeader;
            return mesReply;
        }
        public static RVMaterialLotInfoRequestReply SendToMESMaterialLotInfoRequest(RVMaterialLotInfoRequest mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            var mesReply = SendAsync<RVMaterialLotInfoRequestReply>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            if (mesReply == null)
                mesReply = new RVMaterialLotInfoRequestReply();
            mesReply.replyHeader = replyHeader;
            return mesReply;
        }
        public static void SendToMESMaterialLoadRequest(RVMaterialLoadRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESMaterialCountReport(RVMaterialCountReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESRecipeCheckRequest(RVRecipeCheckRequest mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVCuttingBoxInfoRequestReply SendToMESCuttingBoxInfoRequest(RVCuttingBoxInfoRequest mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            var mesReply = SendAsync<RVCuttingBoxInfoRequestReply>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            if (mesReply == null)
                mesReply = new RVCuttingBoxInfoRequestReply();
            mesReply.replyHeader = replyHeader;
            return mesReply;
        }
        public static RVLotInfoRequestReply SendToMESLotInfoRequest(RVLotInfoRequest mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            var mesReply = SendAsync<RVLotInfoRequestReply>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            if (mesReply == null)
                mesReply = new RVLotInfoRequestReply();
            mesReply.replyHeader = replyHeader;
            return mesReply;
        }
        public static RVCarrierInfoDownloadReply SendToMESCarrierInfoDownload(RVCarrierInfoDownload mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            var mesReply = SendAsync<RVCarrierInfoDownloadReply>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            if (mesReply == null)
                mesReply = new RVCarrierInfoDownloadReply();
            mesReply.replyHeader = replyHeader;
            return mesReply;
        }
        public static void SendToMESLoadRequest(RVLoadRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESBatchLoadRequest(RVBatchLoadRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESLoadComplete(RVLoadComplete mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESMultiBoxLoadComplete(RVMultiBoxLoadComplete mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESBatchLoadComplete(RVBatchLoadComplete mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESCreateCarrierBatch(RVCreateCarrierBatch mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESUnloadRequest(RVUnloadRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESBatchUnloadRequest(RVBatchUnloadRequest mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESUnloadComplete(RVUnloadComplete mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESBatchUnloadComplete(RVBatchUnloadComplete mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESBoxPacking(RVBoxPacking mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        #endregion
        #region Matti
        public static bool IsInLine()
        {
            if (TibcoManager.Current.TibcoList.Count == 0)
                return true;
            else
                return false;
        }
        public static void SendToMESUnitOutReport(RVUnitOut mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPanelInOutLine(RVPanelInOut mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVPanelInfoDownloadResponse SendToMESPanelInfoDownload(RVPanelInfoDownload mesBody, string transactionID = "")
        {
            RVHeader replyHeader = new RVHeader();
            var mesReply = SendAsync<RVPanelInfoDownloadResponse>(mesBody, mesBody.MessageName, out replyHeader, transactionID);
            if (mesReply == null)
                mesReply = new RVPanelInfoDownloadResponse();
            mesReply.replyHeader = replyHeader;
            return mesReply;
        }
        public static void SendToMESProcessData(RVProcessData mesBody, string transactionID = "")
        {
            Send(mesBody, mesBody.MessageName, transactionID, mesBody.MACHINENAME);
        }
        public static void SendToMESEquipmentStateReport(RVEquipmentState mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESUnitStateReport(RVUnitState mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESMaterialStateReport(RVMaterialState mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPortTypeReport(RVPortType mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPortCSTTypeChangeReport(RVPortCSTType mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESVCRReadReport(RVVCRRead mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESPanelTrackInOutReport(RVPanelTrackInOut mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESCommunicationReport(RVCommunication mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESCuttingCompleteReport(RVCuttingComplete mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESAreYouThere(RVAreYouThere mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESEquipmentPortReport(RVPortState mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESDailyCheckDataReport(RVDailyCheckDataReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESNGBufferInfoReport(RVNGBufferInfoReport mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESPanelTrackInReport(RVPanelTrackIn mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESPanelTrackOutReport(RVPanelTrackOut mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESEQPMaterialQTYReport(RVEQPMaterialQTY mesBody, string transactionID = "")
        {
            SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static RVHeader SendToMESLotBindingAlias(RVLotBindingAlias mesBody, string transactionID = "")
        {
            return SendAsync(mesBody, mesBody.MessageName, transactionID);
        }
        public static void SendToMESOperatorLoginReport(RVOperatorLogin mesBody, string transactionID = "")
        {
            Send(mesBody, mesBody.MessageName, transactionID);
        }
        #endregion
        #region luoxiangjing
        public static NewRVHeader SendToMESCheckLotBindingRequest(RVCheckLotBindingRequest mesBody, string transactionID = "")
        {
            return NewSendAsync(mesBody, mesBody.MessageName, transactionID);
        }

        #endregion
        #region HKC
        public static RVHeader SendAsync(object mesBody, string messageName, string transactionID = "")
        {
            return TibcoManager.Current.SendAsync(messageName, mesBody, HostInfo.Current.GetTransactionID());
        }
        public static NewRVHeader NewSendAsync(object mesBody, string messageName, string transactionID = "")
        {
            return TibcoManager.Current.NewSendAsync(messageName, mesBody, HostInfo.Current.GetTransactionID());
        }
        /// <summary>
        /// 向MES请求数据，需要MES回复内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mesBody"></param>
        /// <param name="messageName"></param>
        /// <param name="transactionID"></param>
        /// <returns></returns>
        public static T SendAsync<T>(object mesBody, string messageName, out RVHeader replyHeader, string transactionID = "") where T : class
        {
            return TibcoManager.Current.SendAsync<T>(messageName, mesBody, out replyHeader, HostInfo.Current.GetTransactionID());
        }
        public static void Send(object mesBody, string messageName, string transactionID = "", string MachineName = "")
        {
            TibcoManager.Current.Send(messageName, mesBody, HostInfo.Current.GetTransactionID(), MachineName);
        }
        public static void SendReply(object mesBody, string messageName, RVHeader replyHeader, Message requestMessage)
        {
            TibcoManager.Current.SendReply(messageName, mesBody, replyHeader, requestMessage);
        }
        //public static void SendToMESUnitOutReport(RVUnitOut mesBody, string transactionID = "")
        //{
        //    Send(mesBody, mesBody.MessageName, transactionID);
        //}
        #endregion

        
    }
}
