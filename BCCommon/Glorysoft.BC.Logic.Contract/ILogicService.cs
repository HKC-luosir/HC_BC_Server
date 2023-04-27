using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using Glorysoft.Auto.Contract.PLC;
using System.Collections.Concurrent;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.Logic.Contract
{
    public interface ILogicService : IAutoRegister
    {
        #region Yuan 
        void MaterialValidationRequest(Unit oEQP, string MaterialStatus, string MaterialID, string MaterialType, string UnitNumber, string SlotNumber, string MaterialCount, string UnloadingCode, int requestNo, string transactionID = "");
        void PalletInfoRequest(Unit oEQP, string palletID, string palletStatus, string boxQTY, string palletType, int portNumber, string transactionID = "");
        void LableInfoRequestReport(Unit oEQP, string jobID, string boxID, string labelType, string transactionID = "");
        void DefectCodeReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string defectCodeName, string jobJudgeCode, string jobGradeCode, string transactionID = "");
        void BoxWeightCheckRequest(Unit oEQP, string cassetteIDOrBoxID, string boxWeight, string unitNumber, string transactionID = "");
        void JobAssemblyReport(Unit oEQP, string bluID, string bluIDLotSequenceNumber, string bluIDSlotSequenceNumber, string jobID, string jobIDLotSequenceNumber, string jobIDSlotSequenceNumber, string transactionID = "");
        void SamplingFlagReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string transactionID = "");
        void SamplingRequest(Unit oEQP, string samplingCode, string transactionID = "");
        void MachineModeChangeReport(Unit oEQP, string machineMode, string transactionID = "");
        void MESSamplingDownload(RVSamplingDownload samplingDownload, object requestMessage, string transactionID = "");
        void MESSPCRateDownload(RVSPCRateDownload sPCRateDownload, object requestMessage, string transactionID = "");
        void MESRecipeParamRequest(RVRecipeParameterRequest recipeParameterRequest, object requestMessage, string transactionID = "");
        void RecipeChangeReport(Unit oEQP, int recipeNumber, string changeType, string recipeVersion, string unitNumber, string operatorID, List<Parameter> parameterList, string transactionID = "");
        void JobJudgeResultReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string unitNumber, string slotNumber, string jobJudgeCode, string jobGradeCode, string transactionID = "");
        void PanelDataUpdateReport(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string judgeArray, string transactionID = "");
        void PanelJudgeDataDownloadRequest(Unit oEQP, string jobID, string lotSequenceNumber, string slotSequenceNumber, string transactionID = "");
        void CurrentRecipeNumberChangeReport(Unit oEQP, int recipeNumber, string recipeVersion, string unitNumber, string transactionID = "");
        void AutoRecipeChangeModeReport(Unit oEQP, string autoRecipeChangeMode, string transactionID = "");
        void AlarmReport(Unit oEQP, string alarmID, string alarmState, string alarmUnit, string alarmType, string alarmCode, string transactionID = "");
        void AbnormalCodeReport(Unit oEQP, string LotSequenceNumber, string SlotSequenceNumber, string AbnormalFlag1, string AbnormalFlag2, string AbnormalFlag3, string AbnormalFlag4, string AbnormalFlag5, string AbnormalFlag6, string AbnormalFlag7, string AbnormalFlag8, string transactionID = "");
        void MaterialCountChangeReport(Unit oEQP, int requestNo, string materialID, string unitNumber, string slotNumber, string materialType, string materialCount, string transactionID = "");
        void CellMaterialAssemblyReport(Unit oEQP, string lotSequenceNumber, string slotSequenceNumber, List<MaterialInfo> materialList, string transactionID = "");
        void MaterialLotInfoRequest(Unit oEQP, int requestNo, string materialID, string unitNumber, string slotNumber, string materialType, string transactionID = "");
        void TransferBoxReport(Unit oEQP, int materialType, int materialQty, int materialCurrentQty, int reportOption, int portNumber, string transactionID = "");
        #endregion
        #region matti
        void SendOutJobEventReport(Unit eqpName, int i, List<JobDataInfo> jobdatas, string transactionID = "");
        void JobDataChangeReport(Unit oEQP, JobDataInfo jobdata, string transactionID = "");
        void ReceiveJobEventReport(Unit eqpName, int i, List<JobDataInfo> jobdatas, string transactionID = "");
        void JobManualMoveReport(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string JobPosition, string ReportOption, string OperatorID, string UnitorPort, string UnitNumberorPortNo, string SlotNumber, string transactionID = "");
        void JobDataRequest(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string RequestOption, string transactionID = "");
        void DVDataReport(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string UnitNumber, string SlotNumber, string RecipeNumber, string DVSamplingFlag, List<Item> ITEMLIST, string transactionID = "");
        void CVDataReport(Unit oEQP, List<Item> ITEMLIST, string transactionID = "");
        void VCRStatusReport(Unit oEQP, string VCRNumber, string VCRStatus, string transactionID = "");
        void DateTimeRequest(Unit oEQP, string transactionID = "");
        void MachineStatusChangeReport(Unit oEQP, string MachineStatus, string MachinestatusReasonCode, ConcurrentDictionary<int, string> UnitList, string transactionID = "");
        void MaterialStatusChangeReport(Unit oEQP, int i, string MaterialStatus, string MaterialID, string MaterialType, string UnitNumber, string SlotNumber, string MaterialCount, string UnloadingCode, string transactionID = "");
        void VCRReadCompleteReport(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string UnitNumber, string VCRNumber, string VCRResult, string transactionID = "");
        void StoreInJobEventReport(Unit oEQP, string panelID, string carrierSeq, string slotID, string unitOrPort, string unitID, string portID, string slotNum, string transactionID = "");
        void FetchOutJobEventReport(Unit oEQP, string panelID, string carrierSeq, string slotID, string unitOrPort, string unitID, string portID, string slotNum, string transactionID = "");
        void MachineModeChangeCommandReply(Unit oEQP, string MachineModeChangeReturnCode, string transactionID = "");
        void RecipeParameterRequestCommandReply(Unit oEQP, string Result, string transactionID = "");
        void CIMModeChangeCommandReply(Unit oEQP, string ReturnCode, string transactionID = "");
        void CIMModeChange(Unit oEQP, string CIMMode, string transactionID = "");
        void CIMMessageSetCommandReply(Unit oEQP, string ReturnCode, string transactionID = "");
        void CIMMessageClearCommandReply(Unit oEQP, string ReturnCode, string transactionID = "");
        void DVSamplingFlagCommandReply(Unit oEQP, string ReasonCode, string transactionID = "");
        void SamplingDownloadCommandReply(Unit oEQP, string ReturnCode, string transactionID = "");
        void PositionStatusChange(Unit oEQP, List<GlassExistencePosition> data, string transactionID = "");
        void SpecialCodeRequest(Unit oEQP, string JobID, string LotSequenceNumber, string SlotSequenceNumber, string transactionID = "");
        void CuttingRequest(Unit oEQP, CutPanelList data, string transactionID = "");
        void BufferJobMonitoring(Unit oEQP, List<string> numbers, string transactionID = "");
        void CVReportTimeChangeCommandReply(Unit oEQP, string CVCommandReturnCode, string CycleType, string transactionID = "");
        BCInforamtionReport GetOPILineInfo(EQPInfo lineInfo);

        GlassInfo GetGlassInfoByCode(Unit oEQP, string functionname, string JobID, string CSTSeqNo, string SlotSeqNo, int type = 1, string BLID = "");
        PortInfo GetPortInfoByCode(Unit oEQP, string JobID, string CSTSeqNo, string SlotSeqNo, int type = 1);
        #endregion
        #region Qing
        void CheckLotBindingRequest(Unit oEQP, string jobID, string BLUID, string transactionID = "");

        #endregion
        #region
        void PanelProcessEndRequest(Unit eqpName, string JobID, string returnCode, string transactionID = "");
        void OperatorLoginReport(Unit oEQP, string OperatorID, string TouchPanelNumber, string ReportOption, string Time, string transactionID = "");
        #endregion
    }
}
