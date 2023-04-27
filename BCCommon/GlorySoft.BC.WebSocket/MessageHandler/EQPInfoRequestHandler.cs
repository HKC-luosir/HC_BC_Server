using GlorySoft.BC.WebSocket.Common;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlorySoft.BC.WebSocket.MessageHandler
{
    public class EQPInfoRequestHandler : AbstractWebSocketMessageHandlercs
    {
        public void Execute(EQPInfoRequest data)
        {
            try
            {
                //dbService.ViewEQP(data.EQPID).FirstOrDefault();
                SendOPIMessage.SendWebSocketTestMessage();

            }
            catch (Exception ex)
            {
                Logger.Error("[WebSocket]" + ex);
            }
        }
    }
}
