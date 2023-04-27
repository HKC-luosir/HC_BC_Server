using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetPortInformationListHandel : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName,string lineId)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getrobotDtatListResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                var portlist = HostInfo.Current.PortList.Where(t => t.EQPID== lineId).ToList();
                List<Ports> listPorts = new List<Ports>();
                for (int j = 0; j < portlist.Count(); j++)
                {
                    Ports portss = new Ports();
                    portss.unitId = portlist[j].UnitID;
                    portss.equipmentNo = portlist[j].UnitID;
                    portss.portId = portlist[j].PortID;
                    portss.cassetteSeq = portlist[j].CassetteSequenceNo;
                    portss.cstid = portlist[j].CassetteID;
                    string PortStatus = "";
                    switch (portlist[j].PortStatus)
                    {
                        case 1:
                            PortStatus = "LOADREADY";
                            break;
                        case 2:
                            PortStatus = "INUSE";
                            break;
                        case 3:
                            PortStatus = "UNLOADREADY";
                            break;
                        case 4:
                            PortStatus = "EMPTY";
                            break;
                        case 5:
                            PortStatus = "BLOCKED";
                            break;
                        default:
                            break;
                    }
                    portss.portStatus = PortStatus;
                    portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
                    portss.porttype = portlist[j].PortType;
                    portss.portMode = portlist[j].PortMode.ToString();
                    string portTypeAutoChg = "";
                    switch (portlist[j].PortTypeAutoChangeMode)
                    {
                        case 1:
                            portTypeAutoChg = "Enable";
                            break;
                        case 2:
                            portTypeAutoChg = "Disabled";
                            break;
                        default:
                            portTypeAutoChg = portlist[j].PortTypeAutoChangeMode.ToString();
                            break;
                    }
                    portss.portTypeAutoChg = portTypeAutoChg;
                    portss.capacity = portlist[j].Capacity;
                    portss.transferMode = portlist[j].TransferMode;
                    portss.portOperationMode = portlist[j].PortOperationMode.ToString();
                    string PortCSTType = "";
                    switch (portlist[j].PortCSTType)
                    {
                        case 1:
                            PortCSTType = "1AC";
                            break;
                        case 2:
                            PortCSTType = "1EC";
                            break;
                        case 3:
                            PortCSTType = "1EF";
                            break;
                        case 4:
                            PortCSTType = "1EM";
                            break;
                        case 5:
                            PortCSTType = "2AC";
                            break;
                        case 6:
                            PortCSTType = "2EC";
                            break;
                        case 7:
                            PortCSTType = "2EF";
                            break;
                        case 8:
                            PortCSTType = "2EM";
                            break;
                        default:
                            break;
                    }
                    portss.portCSTType = PortCSTType;
                    portss.partialFullFlag = null;
                    portss.glassExistence = portlist[j].CassetteInfo.JobExistenceSlot;
                    portss.jobCountIncassette = portlist[j].CassetteInfo.ProductQuantity;//portlist[j].GlassInfos.Count();
                    portss.completedCassetteData = portlist[j].CassetteInfo.CompeletedCassetteData;
                    listPorts.Add(portss);
                }
                WebSocketMessageStr.body = listPorts;
                #endregion

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
            return WebSocketMessageStr;
        }
    }
}
