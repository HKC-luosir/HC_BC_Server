using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class PortInformationRequestHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(Ports ports)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "PortInformationResponse",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = "T7-0.0.2",
                uuid = null,
                userName = "admin"
            };
            #endregion
            try
            {
                #region Body
                var portlist = HostInfo.Current.PortList.Where(t => t.UnitID == ports.unitId && t.PortID == ports.portId).ToList();
                Ports portss = new Ports();
                for (int j = 0; j < portlist.Count(); j++)
                {
                    portss.equipmentNo = portlist[j].UnitID;
                    portss.unitId = portlist[j].UnitID;
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
                    //string CassetteStatus = "";
                    //switch (portlist[j].CassetteStatus)
                    //{
                    //    case 1:
                    //        CassetteStatus = "NoCassetteExist";
                    //        break;
                    //    case 2:
                    //        CassetteStatus = "WaitingforCassetteData";
                    //        break;
                    //    case 3:
                    //        CassetteStatus = "WaitingforStartCommand";
                    //        break;
                    //    case 4:
                    //        CassetteStatus = "WaitingforProcessing";
                    //        break;
                    //    case 5:
                    //        CassetteStatus = "InProcessing";
                    //        break;
                    //    case 6:
                    //        CassetteStatus = "ProcessPaused";
                    //        break;
                    //    case 7:
                    //        CassetteStatus = "ProcessCompleted";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
                    string PortType = portlist[j].PortType;
                    //switch (portlist[j].PortType)
                    //{
                    //    case "1":
                    //        PortType = "PL";
                    //        break;
                    //    case "2":
                    //        PortType = "PU";
                    //        break;
                    //    case "3":
                    //        PortType = "PB";
                    //        break;
                    //    case "4":
                    //        PortType = "BB";
                    //        break;
                    //    case "5":
                    //        PortType = "BL";
                    //        break;
                    //    case "6":
                    //        PortType = "BU";
                    //        break;
                    //    case "7":
                    //        PortType = "PS";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    portss.porttype = PortType;
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
                    string TransferMode = portlist[j].TransferMode;
                    //switch (portlist[j].TransferMode)
                    //{
                    //    case "1":
                    //        TransferMode = "MGV Mode";
                    //        break;
                    //    case "2":
                    //        TransferMode = "AGV Mode";
                    //        break;
                    //    case "3":
                    //        TransferMode = "Stocker Mode";
                    //        break;
                    //    case "4":
                    //        TransferMode = "MCT Mode";
                    //        break;
                    //    case "5":
                    //        TransferMode = "PNP Mode";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    portss.transferMode = TransferMode;
                    string PortOperationMode = portlist[j].PortOperationMode.ToString();
                    //switch (portlist[j].PortOperationMode)
                    //{
                    //    case "1":
                    //        PortOperationMode = "Auto Mode";
                    //        break;
                    //    case "2":
                    //        PortOperationMode = "Semi-Auto Mode";
                    //        break;
                    //    case "3":
                    //        PortOperationMode = "Manual Mode";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    portss.portOperationMode = PortOperationMode;
                    string PortCSTType = "";
                    switch (portlist[j].CassetteInfo.CarrierType)
                    {
                        case "1":
                            PortCSTType = "1AC";
                            break;
                        case "2":
                            PortCSTType = "1EC";
                            break;
                        case "3":
                            PortCSTType = "1EF";
                            break;
                        case "4":
                            PortCSTType = "1EM";
                            break;
                        case "5":
                            PortCSTType = "2AC";
                            break;
                        case "6":
                            PortCSTType = "2EC";
                            break;
                        case "7":
                            PortCSTType = "2EF";
                            break;
                        case "8":
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
                }
                PortInformationResponseReport PortInformationResponseReport = new PortInformationResponseReport()
                {
                    port = portss
                };
                



                WebSocketMessageStr.body = PortInformationResponseReport;

                #endregion
                #region result;
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "操作成功！"
                };
                #endregion
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
