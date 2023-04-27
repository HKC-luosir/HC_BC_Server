using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Contract
{
    public interface ITibcoRVService : IAutoRegister
    {
        #region Yuan
        void SendToMESDefectAlarmReply(string eqpid, RVDefectAlarmReply mesBody, RVHeader replyHeader, object requestMessage);
        void SendToMESSamplingDownloadReply(string eqpid, RVSamplingDownloadReply mesBody, RVHeader replyHeader, object requestMessage);
        void SendToMESSPCRateDownloadReply(string eqpid, RVSPCRateDownloadReply mesBody, RVHeader replyHeader, object requestMessage);
        void SendToMESRecipeParameterRequestReply(string eqpid, RVRecipeParameterRequestReply mesBody, RVHeader replyHeader, object requestMessage);
        RVHeader SendToMESRecipeChangeReport(string eqpid, RVRecipeChangeReport mesBody, string transactionID = "");
        void SendToMESPanelScrap(string eqpid, RVPanelScrap mesBody, string transactionID = "");
        void SendToMESPanelOutByIndexReport(string eqpid, RVPanelOutByIndexReport mesBody, string transactionID = "");
        void SendToMESPanelInByIndexReport(string eqpid, RVPanelInByIndexReport mesBody, string transactionID = "");
        void SendToMESChangeProcessMode(string eqpid, RVChangeProcessMode mesBody, string transactionID = "");
        RVHeader SendToMESLotProcessEnd(string eqpid, RVLotProcessEnd mesBody, string transactionID = "");
        RVHeader SendToMESProcessStartRequest(string eqpid, RVProcessStartRequest mesBody, string transactionID = "");
        RVHeader SendToMESCarrierProcessStart(string eqpid, RVCarrierProcessStart mesBody, string transactionID = "");
        void SendToMESLotProcessCancel(string eqpid, RVLotProcessCancel mesBody, string transactionID = "");
        void SendToMESEmptyPalletReturnRequest(string eqpid, RVEmptyPalletReturnRequest mesBody, string transactionID = "");
        void SendToMESLotProcessAbort(string eqpid, RVLotProcessAbort mesBody, string transactionID = "");
        void SendToMESLabelInfoRequest(string eqpid, RVLabelInfoRequest mesBody, string transactionID = "");
        void SendToMESLotProcessStart(string eqpid, RVLotProcessStart mesBody, string transactionID = "");
        void SendToMESPanelJudgeReport(string eqpid, RVPanelJudgeReport mesBody, string transactionID = "");
        void SendToMESWeightReport(string eqpid, RVWeightReport mesBody, string transactionID = "");
        void SendToMESSamplingFlagReport(string eqpid, RVSamplingFlagReport mesBody, string transactionID = "");
        void SendToMESAlarmReport(string eqpid, RVAlarmReport mesBody, string transactionID = "");
        RVHeader SendToMESMaterialValidation(string eqpid, RVMaterialValidation mesBody, string transactionID = "");
        void SendToMESMaterialUseReport(string eqpid, RVMaterialUseReport mesBody, string transactionID = "");
        void SendToMESMaterialUnloadRequest(string eqpid, RVMaterialUnloadRequest mesBody, string transactionID = "");
        void SendToMESMaterialUnloadComplete(string eqpid, RVMaterialUnloadComplete mesBody, string transactionID = "");
        RVMaterialLotInfoRequestReply SendToMESMaterialLotInfoRequest(string eqpid, RVMaterialLotInfoRequest mesBody, string transactionID = "");
        RVSamplingRequestReply SendToMESSamplingRequest(string eqpid, RVSamplingRequest mesBody, string transactionID = "");
        void SendToMESMaterialLoadRequest(string eqpid, RVMaterialLoadRequest mesBody, string transactionID = "");
        void SendToMESMaterialCountReport(string eqpid, RVMaterialCountReport mesBody, string transactionID = "");
        RVHeader SendToMESRecipeCheckRequest(string eqpid, RVRecipeCheckRequest mesBody, string transactionID = "");
        RVCuttingBoxInfoRequestReply SendToMESCuttingBoxInfoRequest(string eqpid, RVCuttingBoxInfoRequest mesBody, string transactionID = "");
        RVLotInfoRequestReply SendToMESLotInfoRequest(string eqpid, RVLotInfoRequest mesBody, string transactionID = "");
        RVCarrierInfoDownloadReply SendToMESCarrierInfoDownload(string eqpid, RVCarrierInfoDownload mesBody, string transactionID = "");
        void SendToMESLoadRequest(string eqpid, RVLoadRequest mesBody, string transactionID = "");
        void SendToMESBatchLoadRequest(string eqpid, RVBatchLoadRequest mesBody, string transactionID = "");
        void SendToMESLoadComplete(string eqpid, RVLoadComplete mesBody, string transactionID = "");
        void SendToMESBatchLoadComplete(string eqpid, RVBatchLoadComplete mesBody, string transactionID = "");
        void SendToMESCreateCarrierBatch(string eqpid, RVCreateCarrierBatch mesBody, string transactionID = "");
        void SendToMESMultiBoxLoadComplete(string eqpid, RVMultiBoxLoadComplete mesBody, string transactionID = "");
        void SendToMESUnloadRequest(string eqpid, RVUnloadRequest mesBody, string transactionID = "");
        void SendToMESBatchUnloadRequest(string eqpid, RVBatchUnloadRequest mesBody, string transactionID = "");
        void SendToMESUnloadComplete(string eqpid, RVUnloadComplete mesBody, string transactionID = "");
        void SendToMESBatchUnloadComplete(string eqpid, RVBatchUnloadComplete mesBody, string transactionID = "");
        void SendToMESBoxPacking(string eqpid, RVBoxPacking mesBody, string transactionID = "");
        #endregion
        #region Matti
        bool IsInLine();
        void SendToMESUnitOutReport(string eqpid, RVUnitOut mesBody, string transactionID = "");
        void SendToMESPanelInOutLine(string eqpid, RVPanelInOut mesBody, string transactionID = "");
        RVPanelInfoDownloadResponse SendToMESPanelInfoDownload(string eqpid, RVPanelInfoDownload mesBody, string transactionID = "");
        void SendToMESProcessData(string eqpid, RVProcessData mesBody, string transactionID = "");
        void SendToMESEquipmentStateReport(string eqpid, RVEquipmentState mesBody, string transactionID = "");
        void SendToMESUnitStateReport(string eqpid, RVUnitState mesBody, string transactionID = "");
        RVHeader SendToMESMaterialStateReport(string eqpid, RVMaterialState mesBody, string transactionID = "");
        void SendToMESPortTypeReport(string eqpid, RVPortType mesBody, string transactionID = "");
        void SendToMESPortCSTTypeChangeReport(string eqpid, RVPortCSTType mesBody, string transactionID = "");
        void SendToMESVCRReadReport(string eqpid, RVVCRRead mesBody, string transactionID = "");
        RVHeader SendToMESPanelTrackInOutReport(string eqpid, RVPanelTrackInOut mesBody, string transactionID = "");
        RVHeader SendToMESCommunicationReport(string eqpid, RVCommunication mesBody, string transactionID = "");
        RVHeader SendToMESCuttingCompleteReport(string eqpid, RVCuttingComplete mesBody, string transactionID = "");
        void SendToMESAreYouThere(string eqpid, RVAreYouThere mesBody, string transactionID = "");
        void SendToMESEquipmentPortReport(string eqpid, RVPortState mesBody, string transactionID = "");
        void SendToMESDailyCheckDataReport(string eqpid, RVDailyCheckDataReport mesBody, string transactionID = "");
        void SendToMESNGBufferInfoReport(string eqpid, RVNGBufferInfoReport mesBody, string transactionID = "");
        void SendToMESPanelTrackInReport(string eqpid, RVPanelTrackIn mesBody, string transactionID = "");
        RVHeader SendToMESPanelTrackOutReport(string eqpid, RVPanelTrackOut mesBody, string transactionID = "");
        void SendToMESEQPMaterialQTYReport(string eqpid, RVEQPMaterialQTY mesBody, string transactionID = "");
        RVHeader SendToMESLotBindingAlias(string eqpid, RVLotBindingAlias mesBody, string transactionID = "");
        void SendToMESOperatorLoginReport(string eqpid, RVOperatorLogin mesBody, string transactionID = "");
        #endregion
        #region
       NewRVHeader SendToMESCheckLotBindingRequest(string eqpid, RVCheckLotBindingRequest mesBody, string transactionID = "");
        #endregion
    }
}
