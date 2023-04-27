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
    public class GetLineMaskInfoHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute()
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "UserLoginReqeust",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = "admin"
            };
            #endregion
            try
            {
                #region 相关赋值
                string mesControlMode = "", lineOperationMode = "";
                int lineOperationModeCode = 0;
                switch (HostInfo.Current.EQPInfo.ControlState)
                {
                    case EnumControlState.OffLine:
                        mesControlMode = "OFFLINE";
                        break;
                    case EnumControlState.OnLineLocal:
                        mesControlMode = "LOCAL";
                        break;
                    case EnumControlState.OnLineRemote:
                        mesControlMode = "REMOTE";
                        break;
                    default:
                        break;
                }

                Hashtable hashtable = new Hashtable();
                hashtable.Add("equipmentvalue", (int)HostInfo.Current.EQPInfo.LineMode);
                var operationmode = dbService.Viewcfg_operationmode(hashtable).FirstOrDefault();
                if (operationmode != null)
                {
                    lineOperationMode = operationmode.operationmodename;
                    lineOperationModeCode = operationmode.equipmentvalue;
                }
                List<Equipments> equipmentsList = new List<Equipments>();
                for (int i = 0; i < HostInfo.Current.EQPInfo.Units.Count; i++)
                {
                    var type = HostInfo.Current.EQPInfo.Units[i].GetType().Name;
                    if (type != "Unit" && type != "Robot")
                    {
                        continue;
                    }
                    string cimMode = "", cclinkStatus = "";
                    #region 相关转换处理处理
                    if (HostInfo.Current.EQPInfo.Units[i].CIMMode)
                    {
                        cimMode = "CIMON";
                    }
                    else
                    {
                        cimMode = "CIMOFF";
                    }

                    if (HostInfo.Current.EQPInfo.Units[i].IsConnect == "Alive")
                    {
                        cclinkStatus = "ON";
                    }
                    else
                    {
                        cclinkStatus = "OFF";
                    }

                    #endregion
                    Equipments equipments = new Equipments();
                    string UnitStatus = HostInfo.Current.EQPInfo.Units[i].UnitStatus;
                    equipments.currentStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, UnitStatus.ToString());//设备状态
                    equipments.reportMode = "PLC";
                    equipments.jobCount = HostInfo.Current.EQPInfo.Units[i].CurrentWIPCount;//JobCount
                    equipments.alive = HostInfo.Current.EQPInfo.Units[i].IsConnect;
                    equipments.cimMode = cimMode;
                    equipments.cclinkStatus = cclinkStatus;
                    equipments.equipmentId = HostInfo.Current.EQPInfo.Units[i].UnitID;
                    equipments.equipmentNo = HostInfo.Current.EQPInfo.Units[i].UnitNo;
                    equipments.eqpOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, HostInfo.Current.EQPInfo.Units[i].UnitMode.ToString());
                    equipments.equipmentName = HostInfo.Current.EQPInfo.Units[i].UnitName;
                    equipments.stcode = HostInfo.Current.EQPInfo.Units[i].UnitSTCode == null ? null : HostInfo.Current.EQPInfo.Units[i].UnitSTCode.ToString();
                    equipments.recipeIdCheck = HostInfo.Current.EQPInfo.Units[i].CurrentRecipeID.ToString();
                    equipments.indexerOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, HostInfo.Current.EQPInfo.LineMode.ToString());
                    equipments.autoRecipeChangeMode = "";
                    string UpstreamInlineMode = "";
                    if (HostInfo.Current.EQPInfo.Units[i].UpstreamInlineMode)
                    {
                        UpstreamInlineMode = "ON";
                    }
                    else
                    {
                        UpstreamInlineMode = "OFF";
                    }
                    equipments.upstreamInlineMode = UpstreamInlineMode;
                    string DownstreamInlineMode = "";
                    if (HostInfo.Current.EQPInfo.Units[i].DownstreamInlineMode)
                    {
                        DownstreamInlineMode = "ON";
                    }
                    else
                    {
                        DownstreamInlineMode = "OFF";
                    }
                    equipments.downstreamInlineMode = DownstreamInlineMode;
                    string VCREnableMode = "";
                    Hashtable map = new Hashtable
                    {
                        {"EQPID",HostInfo.Current.EQPInfo.EQPID },
                        {"UnitID",HostInfo.Current.EQPInfo.Units[i].UnitID },
                    };
                    var vcr = dbService.ViewVCRList(map).FirstOrDefault();
                    if (vcr != null)
                    {
                        switch (vcr.VCREnableMode)
                        {
                            case 1:
                                VCREnableMode = "Enable";
                                break;
                            case 2:
                                VCREnableMode = "Disable";
                                break;
                            default:
                                break;
                        }
                    }
                    equipments.vcrEnableMode = VCREnableMode;
                    string CommandType = "";
                    switch (HostInfo.Current.EQPInfo.Units[i].CommandType)
                    {
                        case 1:
                            CommandType = "CCLink";
                            break;
                        case 2:
                            CommandType = "HSMS";
                            break;
                        default:
                            break;
                    }
                    equipments.commandType = CommandType;
                    equipments.loaderQTime = HostInfo.Current.EQPInfo.Units[i].PortQTime;
                    equipments.ports = new List<Ports>();
                    var portlist = HostInfo.Current.PortList.Where(t => t.UnitID.Contains(HostInfo.Current.EQPInfo.Units[i].UnitID == null ? "null" : HostInfo.Current.EQPInfo.Units[i].UnitID)).ToList();
                    for (int j = 0; j < portlist.Count(); j++)
                    {
                        Ports portss = new Ports();
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
                        portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
                        string PortType = portlist[j].PortType;

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

                        portss.transferMode = TransferMode;
                        string PortOperationMode = portlist[j].PortOperationMode.ToString();

                        portss.portOperationMode = PortOperationMode;
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
                        equipments.ports.Add(portss);
                    }
                    equipments.units = new List<Units>();
                    for (int k = 0; k < HostInfo.Current.EQPInfo.Units[i].SUnitList.Count(); k++)
                    {
                        Units units = new Units();
                        units.unitId = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].UnitID;
                        units.subUnitId = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitID;
                        units.subUnitName = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitName;
                        units.subUnitNo = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SubUnitNo;
                        units.sUnitStatus = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitStatus;
                        units.sUnitSTCode = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitSTCode;

                        units.unitNo = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SubUnitNo;
                        units.currentStauts = HostInfo.Current.EQPInfo.Units[i].SUnitList[k].SUnitStatus;

                        equipments.units.Add(units);
                    }
                    if (HostInfo.Current.EQPInfo.Units[i].LoadingStop)
                    {
                        equipments.localAlarmStatus = "ON";
                    }
                    else
                    {
                        equipments.localAlarmStatus = "OFF";
                    }
                    equipmentsList.Add(equipments);

                }
                #endregion
                string lineType = HostInfo.Current.EQPInfo.LineType.ToString();
                
                #region Body
                AllData allData = new AllData()
                {
                    serverName = HostInfo.Current.EQPInfo.EQPID,
                    line = new Line()
                    {
                        lineType = lineType,
                        equipments = equipmentsList,
                        mesControlMode = mesControlMode,
                        lineStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, HostInfo.Current.EQPInfo.EqpStatus),
                        lineId = HostInfo.Current.EQPInfo.EQPID,
                        lineOperationMode = lineOperationMode,
                        lineOperationModeCode = lineOperationModeCode,
                        dispatchMode = HostInfo.Current.EQPInfo.RobotDispatchMode.ToString(),
                        coldRunTotalQuantity = HostInfo.Current.EQPInfo.ColdRunTotalQuantity,
                        coldRunCurrentQuantity = HostInfo.Current.EQPInfo.ColdRunCurrentQuantity
                    }
                };
                WebSocketMessageStr.body = allData;
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
