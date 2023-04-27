using Glorysoft.BC.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlorySoft.BC.WebSocket;
using Glorysoft.BC.Entity;
using System.Threading;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.RVEntity;
using System.Collections;

namespace Glorysoft.BC.Logic.Service
{
   public class WebSocketService : IWebSocketService
    {
        public void SendWebSocketTestMessage()
        {
            SendOPIMessage.SendWebSocketTestMessage();
        }

        public void SendToWebSocketAlarmReport(Hashtable AlarmReport)
        {
            SendOPIMessage.SendToWebSocketAlarmReport(AlarmReport);
        }
        public void SendToWebSocketJobPosition(Hashtable AlarmReport)
        {
            SendOPIMessage.SendToWebSocketJobPosition(AlarmReport);
        }
        public void SendToWebSocketMessageReply(MessageReply MessageReply)
        {
            SendOPIMessage.SendToWebSocketMessageReply(MessageReply);
        }
        public void SendToWebSocketOPCall(string ReturnCode,string Message)
        {
            Entity.WebSocketEntity.OpCallInfo OpCallInfo = new Entity.WebSocketEntity.OpCallInfo();
            OpCallInfo.ReturnCode = ReturnCode;
            OpCallInfo.Message = Message;
            SendOPIMessage.SendToWebSocketOPCall(OpCallInfo);
        }
        public void SendToWebSocketCassetteOnPortTime(string ReturnCode, string Message)
        {
            Entity.WebSocketEntity.CassetteOnPortTime BCInformationReport = new Entity.WebSocketEntity.CassetteOnPortTime();
            BCInformationReport.ReturnCode = ReturnCode;
            BCInformationReport.Message = Message;
            SendOPIMessage.SendToWebSocketCassetteOnPortTime(BCInformationReport);
        }
   
        public void SendToWebSocketLotInformation(LotInformation LotInformation)
        {
            SendOPIMessage.SendToWebSocketLotInformation(LotInformation);
        }
        //public   void SendToWebSocketEQPInfoReport()
        //{
        //    var eqp = HostInfo.Current.EQPInfo;
        //    EQPInfoReport EQPInfoReport = new EQPInfoReport();
        //    EQPInfoReport.ControlState = eqp.ControlState;
        //    EQPInfoReport.EqpStatus = eqp.EqpStatus;
        //    EQPInfoReport.ReasonCode = eqp.ReasonCode;
        //    EQPInfoReport.EquipmentAutoMode = eqp.EquipmentAutoMode.ToString();
        //    SendOPIMessage.SendToWebSocketEQPInfoReport(EQPInfoReport);
        //}
        //public   void SendToWebSocketUnitInfoReport(Unit Unit)
        //{
        //    UnitInfoReport UnitInfoReport = new UnitInfoReport();
        //    UnitInfoReport
        //    SendOPIMessage.SendToWebSocketUnitInfoReport(UnitInfoReport);
        //}
        //public   void SendToWebSocketPortInfoReport(PortInfoReport PortInfoReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortInfoReport(PortInfoReport);
        //}
        //public   void SendToWebSocketGlassInfoReport(GlassInfoReport GlassInfoReport)
        //{
        //    SendOPIMessage.SendToWebSocketGlassInfoReport(GlassInfoReport);
        //}
        //public   void SendToWebSocketTrayInfoReport(TrayInfoReport TrayInfoReport)
        //{
        //    SendOPIMessage.SendToWebSocketTrayInfoReport(TrayInfoReport);
        //}
        //public   void SendToWebSocketRecipeInfoReport(RecipeInfoReport RecipeInfoReport)
        //{
        //    SendOPIMessage.SendToWebSocketRecipeInfoReport(RecipeInfoReport);
        //}
        //public   void SendToWebSocketCimMessageReport(CimMessageReport CimMessageReport)
        //{
        //    SendOPIMessage.SendToWebSocketCimMessageReport(CimMessageReport);
        //}
        //public   void SendToWebSocketAlarmReport(Entity.WebSocketEntity.AlarmReport AlarmReport)
        //{
        //    SendOPIMessage.SendToWebSocketAlarmReport(AlarmReport);
        //}
        //public   void SendToWebSocketCurrentRecipeNoChangeReport(CurrentRecipeNoChangeReport CurrentRecipeNoChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketCurrentRecipeNoChangeReport(CurrentRecipeNoChangeReport);
        //}
        //public   void SendToWebSocketCSTOperationModeReport(CSTOperationModeReport CSTOperationModeReport)
        //{
        //    SendOPIMessage.SendToWebSocketCSTOperationModeReport(CSTOperationModeReport);
        //}
        //public   void SendToWebSocketIndexerOperationModeChangeReport(IndexerOperationModeChangeReport IndexerOperationModeChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketIndexerOperationModeChangeReport(IndexerOperationModeChangeReport);
        //}
        //public   void SendToWebSocketLoaderPortQTimeChangeReport(LoaderPortQTimeChangeReport LoaderPortQTimeChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketLoaderPortQTimeChangeReport(LoaderPortQTimeChangeReport);
        //}
        //public   void SendToWebSocketPortCassetteTypeChangeModeReport(PortCassetteTypeChangeModeReport PortCassetteTypeChangeModeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortCassetteTypeChangeModeReport(PortCassetteTypeChangeModeReport);
        //}
        //public   void SendToWebSocketPortStatusChangeReport(PortStatusChangeReport PortStatusChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortStatusChangeReport(PortStatusChangeReport);
        //}
        //public   void SendToWebSocketPortModeChangeReport(PortModeChangeReport PortModeChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortModeChangeReport(PortModeChangeReport);
        //}
        //public   void SendToWebSocketPortOperationModeChangeReport(PortOperationModeChangeReport PortOperationModeChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortOperationModeChangeReport(PortOperationModeChangeReport);
        //}
        //public   void SendToWebSocketPortTransferModeChangeReport(PortTransferModeChangeReport PortTransferModeChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortTransferModeChangeReport(PortTransferModeChangeReport);
        //}
        //public   void SendToWebSocketPortTypeAutoChangeModeReport(PortTypeAutoChangeModeReport PortTypeAutoChangeModeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortTypeAutoChangeModeReport(PortTypeAutoChangeModeReport);
        //}
        //public   void SendToWebSocketPortTypeChangeReport(PortTypeChangeReport PortTypeChangeReport)
        //{
        //    SendOPIMessage.SendToWebSocketPortTypeChangeReport(PortTypeChangeReport);
        //}
    }
}
