using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.EIP;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class PortCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, PortInfo portInfo)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = type,
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                switch (type)
                {
                    case "PortTypeChangeCommand":
                        eqpService.SendPortTypeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortType, HostInfo.Current.GetTransactionID());
                        break;
                    case "PortTransferModeChangeCommand":
                        eqpService.SendPortTransferModeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.TransferMode, HostInfo.Current.GetTransactionID());
                        break;
                    case "PortEnableModeChangeCommand":
                        eqpService.SendPortEnableModeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortEnableMode.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "PortTypeAutoChangeModeCommand":
                        eqpService.SendPortTypeAutoChangeModeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortTypeAutoChangeMode.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "PortCassetteTypeChangeCommand":
                        eqpService.SendPortCassetteTypeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortCSTType.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "PortModeChangeCommand":
                        eqpService.SendPortModeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortMode.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "PortGradeChangeCommand":
                        eqpService.SendPortGradeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortGrade, HostInfo.Current.GetTransactionID());
                        break;
                    case "PortPauseModeChangeCommand":
                        eqpService.SendPortPauseModeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortPauseMode.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "PortQTimeChangeCommand":
                        eqpService.SendPortQTimeChangeCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortQTime.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "PortControlCommand":
                        eqpService.SendPortControlCommand(portInfo.UnitName, portInfo.PortNo, portInfo.PortType, HostInfo.Current.GetTransactionID());
                        break;
                    default:
                        break;
                }
                
                WebSocketMessageStr.body = null;

                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "操作成功！"
                };

            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "1",
                    returnMessageEN = "Operation failed !",
                    returnMessageCH = "操作失败！"
                };
            }
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了" + type + "命令下发操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
