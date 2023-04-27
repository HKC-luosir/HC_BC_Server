using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using GlorySoft.BC.WebSocket;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class EquipemntInformationRequestHandler : AbstractWebAPIMessageHandlercs
    {

     
        public WebSocketMessage Execute(Line line)
        {
            
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "EquipemntInformationResponse",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = "report",
                uuid = null,
                userName = "admin"
            };
            #endregion
            try
            {
                Equipments equipments = new Equipments();

                //HostInfo.Current.LineMode = line;
                //HostInfo.Current.isSendOPIInforamtion = true;
                //var unitno = line.equipmentNo;
                //var eqpid = line.equipmentNo.Split('-')[0];
                //#region Body
                //var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid);

                //for (int i = 0; i < eqpinfo.Units.Count; i++)
                //{
                //    var type = eqpinfo.Units[i].GetType().Name;
                //    if (type != "Unit" && type != "Robot")
                //    {
                //        continue;
                //    }
                //    if (eqpinfo.Units[i].UnitNo != line.equipmentNo)
                //    {
                //        continue;
                //    }
                //    string cimMode = "", cclinkStatus = "";
                //    #region 相关转换处理处理
                //    if (eqpinfo.Units[i].CIMMode)
                //    {
                //        cimMode = "CIMON";
                //    }
                //    else
                //    {
                //        cimMode = "CIMOFF";
                //    }

                //    if (eqpinfo.Units[i].IsConnect == "Alive")
                //    {
                //        cclinkStatus = "ON";
                //    }
                //    else
                //    {
                //        cclinkStatus = "OFF";
                //    }

                //    #endregion
                //    string UnitStatus = eqpinfo.Units[i].UnitStatus;
                //    equipments.currentStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, UnitStatus.ToString());//设备状态
                //    equipments.reportMode = "PLC";
                //    equipments.jobCount = eqpinfo.Units[i].CurrentWIPCount;//JobCount
                //    equipments.alive = eqpinfo.Units[i].IsConnect;
                //    equipments.cimMode = cimMode;
                //    equipments.cclinkStatus = cclinkStatus;
                //    equipments.equipmentId = eqpinfo.Units[i].UnitID;
                //    equipments.equipmentNo = eqpinfo.Units[i].UnitNo;
                //    equipments.eqpOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, eqpinfo.Units[i].UnitMode.ToString());
                //    equipments.equipmentName = eqpinfo.Units[i].UnitName;
                //    equipments.stcode = eqpinfo.Units[i].UnitSTCode;
                //    equipments.recipeIdCheck = eqpinfo.Units[i].CurrentRecipeID.ToString();
                //    equipments.indexerOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, eqpinfo.LineMode.ToString());
                //    equipments.autoRecipeChangeMode = "";
                //    //string UpstreamInlineMode = "";
                //    //if (eqpinfo.Units[i].UpstreamInlineMode)
                //    //{
                //    //    UpstreamInlineMode = "ON";
                //    //}
                //    //else
                //    //{
                //    //    UpstreamInlineMode = "OFF";
                //    //}
                //    //equipments.upstreamInlineMode = UpstreamInlineMode;
                //    //string DownstreamInlineMode = "";
                //    //if (eqpinfo.Units[i].DownstreamInlineMode)
                //    //{
                //    //    DownstreamInlineMode = "ON";
                //    //}
                //    //else
                //    //{
                //    //    DownstreamInlineMode = "OFF";
                //    //}
                //    //equipments.downstreamInlineMode = DownstreamInlineMode;
                //    //string VCREnableMode = "";
                //    //Hashtable map = new Hashtable
                //    //{
                //    //    {"EQPID",eqpinfo.EQPID },
                //    //    {"UnitID",eqpinfo.Units[i].UnitID },
                //    //};
                //    //var vcr = dbService.ViewVCRList(map).FirstOrDefault();
                //    //if (vcr != null)
                //    //{
                //    //    switch (vcr.VCREnableMode)
                //    //    {
                //    //        case 1:
                //    //            VCREnableMode = "Enable";
                //    //            break;
                //    //        case 2:
                //    //            VCREnableMode = "Disable";
                //    //            break;
                //    //        default:
                //    //            break;
                //    //    }
                //    //}
                //    //equipments.vcrEnableMode = VCREnableMode;
                //    //string CommandType = "";
                //    //switch (eqpinfo.Units[i].CommandType)
                //    //{
                //    //    case 1:
                //    //        CommandType = "CCLink";
                //    //        break;
                //    //    case 2:
                //    //        CommandType = "HSMS";
                //    //        break;
                //    //    default:
                //    //        break;
                //    //}
                //    equipments.commandType = Consts.dicUnitCommandType.ContainsKey(eqpinfo.Units[i].CommandType) ? Consts.dicUnitCommandType[eqpinfo.Units[i].CommandType] : "";
                //    equipments.loaderQTime = eqpinfo.Units[i].PortQTime;
                //    equipments.ports = new List<Ports>();
                //    var portlist = HostInfo.Current.PortList.Where(t => t.EQPID.Contains(eqpinfo.Units[i].EQPID == null ? "null" : eqpinfo.Units[i].EQPID)).ToList();
                //    for (int j = 0; j < portlist.Count(); j++)
                //    {
                //        Ports portss = new Ports();
                //        portss.equipmentNo = portlist[j].UnitID;
                //        portss.unitId = portlist[j].UnitID;
                //        portss.portId = portlist[j].PortID;
                //        portss.cassetteSeq = portlist[j].CassetteSequenceNo;
                //        portss.cstid = portlist[j].CassetteID;
                //        string PortStatus = "";
                //        switch (portlist[j].PortStatus)
                //        {
                //            case 1:
                //                PortStatus = "LOADREADY";
                //                break;
                //            case 2:
                //                PortStatus = "INUSE";
                //                break;
                //            case 3:
                //                PortStatus = "UNLOADREADY";
                //                break;
                //            case 4:
                //                PortStatus = "EMPTY";
                //                break;
                //            case 5:
                //                PortStatus = "BLOCKED";
                //                break;
                //            default:
                //                break;
                //        }
                //        portss.portStatus = PortStatus;
                //        //string CassetteStatus = "";
                //        //switch (portlist[j].CassetteStatus)
                //        //{
                //        //    case 1:
                //        //        CassetteStatus = "NoCassetteExist";
                //        //        break;
                //        //    case 2:
                //        //        CassetteStatus = "WaitingforCassetteData";
                //        //        break;
                //        //    case 3:
                //        //        CassetteStatus = "WaitingforStartCommand";
                //        //        break;
                //        //    case 4:
                //        //        CassetteStatus = "WaitingforProcessing";
                //        //        break;
                //        //    case 5:
                //        //        CassetteStatus = "InProcessing";
                //        //        break;
                //        //    case 6:
                //        //        CassetteStatus = "ProcessPaused";
                //        //        break;
                //        //    case 7:
                //        //        CassetteStatus = "ProcessCompleted";
                //        //        break;
                //        //    default:
                //        //        break;
                //        //}
                //        portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
                //        string PortType = portlist[j].PortType;
                //        //switch (portlist[j].PortType)
                //        //{
                //        //    case "1":
                //        //        PortType = "PL";
                //        //        break;
                //        //    case "2":
                //        //        PortType = "PU";
                //        //        break;
                //        //    case "3":
                //        //        PortType = "PB";
                //        //        break;
                //        //    case "4":
                //        //        PortType = "BB";
                //        //        break;
                //        //    case "5":
                //        //        PortType = "BL";
                //        //        break;
                //        //    case "6":
                //        //        PortType = "BU";
                //        //        break;
                //        //    case "7":
                //        //        PortType = "PS";
                //        //        break;
                //        //    default:
                //        //        break;
                //        //}
                //        portss.porttype = PortType;
                //        portss.portMode = portlist[j].PortMode.ToString();
                //        portss.portTypeAutoChg = Consts.dicPortTypeAutoChangeMode.ContainsKey(portlist[j].PortTypeAutoChangeMode) ? Consts.dicPortTypeAutoChangeMode[portlist[j].PortTypeAutoChangeMode] : "";
                //        portss.capacity = portlist[j].Capacity;
                //        string TransferMode = portlist[j].TransferMode;
                //        //switch (portlist[j].TransferMode)
                //        //{
                //        //    case "1":
                //        //        TransferMode = "MGV Mode";
                //        //        break;
                //        //    case "2":
                //        //        TransferMode = "AGV Mode";
                //        //        break;
                //        //    case "3":
                //        //        TransferMode = "Stocker Mode";
                //        //        break;
                //        //    case "4":
                //        //        TransferMode = "MCT Mode";
                //        //        break;
                //        //    case "5":
                //        //        TransferMode = "PNP Mode";
                //        //        break;
                //        //    default:
                //        //        break;
                //        //}
                //        portss.transferMode = TransferMode;
                //        string PortOperationMode = portlist[j].PortOperationMode.ToString();
                //        //switch (portlist[j].PortOperationMode)
                //        //{
                //        //    case "1":
                //        //        PortOperationMode = "Auto Mode";
                //        //        break;
                //        //    case "2":
                //        //        PortOperationMode = "Semi-Auto Mode";
                //        //        break;
                //        //    case "3":
                //        //        PortOperationMode = "Manual Mode";
                //        //        break;
                //        //    default:
                //        //        break;
                //        //}
                //        portss.portOperationMode = PortOperationMode;
                //        portss.portCSTType = Consts.dicPortCassetteType.ContainsKey(portlist[j].PortCSTType) ? Consts.dicPortCassetteType[portlist[j].PortCSTType] : "";
                //        portss.partialFullFlag = null;
                //        portss.glassExistence = portlist[j].CassetteInfo.JobExistenceSlot;
                //        portss.jobCountIncassette = portlist[j].CassetteInfo.ProductQuantity;//portlist[j].GlassInfos.Count();
                //        portss.completedCassetteData = portlist[j].CassetteInfo.CompeletedCassetteData;
                //        equipments.ports.Add(portss);
                //    }
                //    equipments.units = new List<Units>();
                //    for (int k = 0; k < eqpinfo.Units[i].SUnitList.Count(); k++)
                //    {
                //        Units units = new Units();
                //        units.unitId = eqpinfo.Units[i].SUnitList[k].UnitID;
                //        units.subUnitId = eqpinfo.Units[i].SUnitList[k].SUnitID;
                //        units.subUnitName = eqpinfo.Units[i].SUnitList[k].SUnitName;
                //        units.subUnitNo = eqpinfo.Units[i].SUnitList[k].SubUnitNo;
                //        units.sUnitStatus = eqpinfo.Units[i].SUnitList[k].SUnitStatus;
                //        //units.sUnitSTCode = eqpinfo.Units[i].SUnitList[k].SUnitSTCode;
                //        //units.unitNo = eqpinfo.Units[i].SUnitList[k].SubUnitNo;
                //        //units.currentStauts = eqpinfo.Units[i].SUnitList[k].SUnitStatus;
                //        equipments.units.Add(units);
                //    }
                //}



                //#endregion


                WebSocketMessageStr.body = equipments;


                //HostInfo.Current.IsLineInformationPush = true;

                Hashtable hashtable2 = new Hashtable() {
                {"eqpid",line.lineId },
                {"unitid",line.equipmentNo } };
                Hashtable hashtable3 = new Hashtable() {
                {"eqpid",line.lineId },
                {"unitid",line.equipmentNo }                };
                SendOPIMessage.SendToWebSocketAlarmReport(hashtable2);
                SendOPIMessage.SendToWebSocketJobPosition(hashtable3);


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
