
using GlorySoft.BC.WebSocket.Common;
using Glorysoft.BC.Entity.WebSocketEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlorySoft.BC.WebSocket.MessageHandler
{
    public class RobotControlCommandHandler : AbstractWebSocketMessageHandlercs
    {
        public void Execute(RobotControlCommand data)
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
