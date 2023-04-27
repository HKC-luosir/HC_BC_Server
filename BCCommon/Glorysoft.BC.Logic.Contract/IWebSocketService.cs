using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Glorysoft.BC.Logic.Contract
{
    
   public interface IWebSocketService : IAutoRegister
    {
        void SendWebSocketTestMessage();

        void SendToWebSocketAlarmReport(Hashtable AlarmReport);
        void SendToWebSocketJobPosition(Hashtable AlarmReport);
        void SendToWebSocketMessageReply(MessageReply MessageReply);
        void SendToWebSocketOPCall(string ReturnCode, string Message);
        void SendToWebSocketLotInformation(LotInformation LotInformation);
        void SendToWebSocketCassetteOnPortTime(string ReturnCode, string Message);
    }
}
