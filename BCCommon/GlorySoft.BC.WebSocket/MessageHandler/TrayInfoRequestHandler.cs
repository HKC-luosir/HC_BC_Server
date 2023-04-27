using GlorySoft.BC.WebSocket.Common;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlorySoft.BC.WebSocket.MessageHandler
{
    public class TrayInfoRequestHandler : AbstractWebSocketMessageHandlercs
    {
        public void Execute(TrayInfoRequest data)
        {
            try
            {
              

            }
            catch (Exception ex)
            {
                Logger.Error("[WebSocket]" + ex);
            }
        }
    }
}
