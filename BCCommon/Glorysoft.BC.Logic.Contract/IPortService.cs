using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Contract.PLC;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Logic.Contract
{
    public interface IPortService : IAutoRegister
    {
        #region matti
        void PortTypeChangeReport(Unit oEQP, string PortType, int portNo, string transactionID = "");
        void PortTypeAutoChangeModeReport(Unit oEQP, string PortTypeAutoChangeMode, int portNo, string transactionID = "");
        void PortModeChangeReport(Unit oEQP, string PortMode, int portNo, string transactionID = "");
        void PortEnableModeChangeReport(Unit oEQP, string PortEnableMode, int portNo, string transactionID = "");
        void PortTransferModeChangeReport(Unit oEQP, string PortTransferMode, int portNo, string transactionID = "");
        void PortPauseModeChangeReport(Unit oEQP, string PortPauseMode, int portNo, string transactionID = "");
        void PortGradeChangeReport(Unit oEQP, string PortGrade, int portNo, string transactionID = "");
        void PortCassetteTypeChangeReport(Unit oEQP, string PortCassetteType, int portNo, string transactionID = "");
        void PortTypeChangeCommandReply(Unit oEQP, string PortTypeReturnCode, int portNo, string transactionID = "");
        void PortTransferModeChangeCommandReply(Unit oEQP, string TransferModeReturnCode, int portNo, string transactionID = "");
        void PortEnableModeChangeCommandReply(Unit oEQP, string EnableModeReturnCode, int portNo, string transactionID = "");
        void PortTypeAutoChangeModeCommandReply(Unit oEQP, string PortTypeAutoChangeModeReturnCode, int portNo, string transactionID = "");
        void PortCassetteTypeChangeCommandReply(Unit oEQP, string PortCassetteTypeReturnCode, int portNo, string transactionID = "");
        void PortModeChangeCommandReply(Unit oEQP, string PortModeReturnCode, int portNo, string transactionID = "");
        void PortPauseModeChangeCommandReply(Unit oEQP, string PauseModeReturnCode, int portNo, string transactionID = "");
        void PortGradeChangeCommandReply(Unit oEQP, string PortGradeReturnCode, int portNo, string transactionID = "");
        void PortQTimeChangeCommandReply(Unit oEQP, string PortQTimeReturnCode, int portNo, string transactionID = "");
        void PortQTimeChangeReport(Unit oEQP, int portNo, int portQTime, string transactionID = "");
        void PortControlCommandReply(Unit oEQP, string PortControlCommandReturnCode, int portNo, string transactionID = "");
        void PortBoxInfoRequest(Unit oEQP, int portNo, string BoxID, int lotSequenceNumber, int jobCountInCassette, string jobExistenceSlot, string transactionID = "");
        void PortBoxGroupPortStatusReport(Unit oEQP, int portNo, int PortStatus, string PortType, int PortCassetteType, string PortTransferMode, List<string> BoxList, string transactionID = "");
        void PortBoxPortStatusReport(Unit oEQP, int portNo, int PortStatus, string PortType, string BoxID, int lotSequenceNumber, int jobCountInCassette, string jobExistenceSlot, string transactionID = "");
        #endregion

        #region Yuan
        void PortStatusChangeReport(Unit oEQP, int portNo, int portStatus, int lotSequenceNumber, string cassetteIDBoxID, int jobCountInCassette, string operatorID, string jobExistenceSlot, int loadingCassetteType, string transactionID = "");
        void CassetteMapDownloadCommandReply(Unit oEQP, int portNo, int returnCode, string transactionID = "");
        void CassetteControlCommandReply(Unit oEQP, int portNo, int returnCode, string transactionID = "");
        void CassetteProcessStartReport(Unit oEQP, int portNo, int lotSequenceNumber, string startOption, string cassetteIDBoxID, string transactionID = "");
        void CassetteProcessEndReport(Unit oEQP, int portNo, int lotSequenceNumber, string completeCassetteData, string cassetteIDBoxID, string transactionID = "");
        void ExcuteDownloadJobFlow(Unit oEQP, List<UNITRECIPE> UNITRECIPELIST, PortInfo portinfo, List<GlassInfo> glassInfoList, string jobExistenceSlot, string transactionID = "", bool needcheck = true, bool needcheckslot = false);
        #endregion
    }
}
