using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{

    public class EQPInfoRequestHandler : AbstractWebAPIMessageHandlercs
    {
        protected readonly ILog Logger = LogHelper.WebAPILog;
        public WebSocketMessage Execute(EQPInfoRequest data)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            try
            {
                var eqp= dbService.ViewEQP(data.EQPID).FirstOrDefault();

                EQPInfoReport EQPInfoReport = new EQPInfoReport();
                //var eqp = HostInfo.Current.EQPInfo;
                EQPInfoReport.ControlState = eqp.ControlState.ToString();
                EQPInfoReport.EqpStatus = eqp.EqpStatus;
                EQPInfoReport.ReasonCode = eqp.ReasonCode;
                EQPInfoReport.EquipmentAutoMode = eqp.RobotDispatchMode.ToString();
                WebSocketResult result = new WebSocketResult() { returnCode = "0", returnMessageCH = "", returnMessageEN = "" };
                WebSocketMessageStr = JsonFormat(EQPInfoReport, EQPInfoReport.TYPE, result);
                return WebSocketMessageStr;
                //SendOPIMessage.SendWebSocketTestMessage();

            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                return WebSocketMessageStr;
            }
        }
    }
}
