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
    public class GetServerLinesMessageRequestHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "GetServerLinesMessageResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                object lineId;
                InitData.TryGetValue("serverName", out lineId);
                var currenteqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == lineId.ToString());

                #region 相关赋值
                string mesControlMode = "", lineOperationMode = "";
                int lineOperationModeCode = 0;
                switch (currenteqp.ControlState)
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
                hashtable.Add("equipmentvalue", (int)currenteqp.LineMode);
                var operationmode = dbService.Viewcfg_operationmode(hashtable).FirstOrDefault();
                if (operationmode != null)
                {
                    lineOperationMode = operationmode.operationmodename;
                    lineOperationModeCode = operationmode.equipmentvalue;
                }
                List<Equipments> equipmentsList = new List<Equipments>();
                for (int i = 0; i < currenteqp.Units.Count; i++)
                {
                    var type = currenteqp.Units[i].GetType().Name;
                    if (type != "Unit" && type != "Robot")
                    {
                        continue;
                    }
                    string cimMode = "", cclinkStatus = "";
                    #region 相关转换处理处理
                    if (currenteqp.Units[i].CIMMode)
                    {
                        cimMode = "CIMON";
                    }
                    else
                    {
                        cimMode = "CIMOFF";
                    }

                    if (currenteqp.Units[i].IsConnect == "Alive")
                    {
                        cclinkStatus = "ON";
                    }
                    else
                    {
                        cclinkStatus = "OFF";
                    }

                    #endregion
                    Equipments equipments = new Equipments();
                    equipments.currentStatus = currenteqp.Units[i].UnitStatus;//设备状态
                    equipments.reportMode = "PLC";
                    equipments.jobCount = currenteqp.Units[i].CurrentWIPCount;//JobCount
                    equipments.alive = currenteqp.Units[i].IsConnect;
                    equipments.cimMode = cimMode;
                    equipments.cclinkStatus = cclinkStatus;
                    equipments.equipmentId = currenteqp.Units[i].UnitID;
                    equipments.equipmentNo = currenteqp.Units[i].UnitNo;
                    equipments.eqpOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, currenteqp.Units[i].UnitMode.ToString());
                    equipments.equipmentName = currenteqp.Units[i].UnitName;
                    equipments.stcode = currenteqp.Units[i].UnitSTCode;
                    equipments.recipeIdCheck = currenteqp.Units[i].CurrentRecipeID.ToString();
                    equipments.indexerOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, currenteqp.LineMode.ToString());
                    equipments.autoRecipeChangeMode = "";
                    string UpstreamInlineMode = "";
                    if (currenteqp.Units[i].UpstreamInlineMode)
                    {
                        UpstreamInlineMode = "ON";
                    }
                    else
                    {
                        UpstreamInlineMode = "OFF";
                    }
                    equipments.upstreamInlineMode = UpstreamInlineMode;
                    string DownstreamInlineMode = "";
                    if (currenteqp.Units[i].DownstreamInlineMode)
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
                        {"EQPID",currenteqp.EQPID },
                        {"UnitID",currenteqp.Units[i].UnitID },
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
                    switch (currenteqp.Units[i].CommandType)
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
                    equipments.loaderQTime = currenteqp.Units[i].PortQTime;
                    equipments.ports = new List<Ports>();
                    var portlist = HostInfo.Current.PortList.Where(t => t.EQPID.Contains(currenteqp.Units[i].EQPID == null ? "null" : currenteqp.Units[i].EQPID)).ToList();
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
                    for (int k = 0; k < currenteqp.Units[i].SUnitList.Count(); k++)
                    {
                        Units units = new Units();
                        units.unitId = currenteqp.Units[i].SUnitList[k].UnitID;
                        units.subUnitId = currenteqp.Units[i].SUnitList[k].SUnitID;
                        units.subUnitName = currenteqp.Units[i].SUnitList[k].SUnitName;
                        units.subUnitNo = currenteqp.Units[i].SUnitList[k].SubUnitNo;
                        units.sUnitStatus = currenteqp.Units[i].SUnitList[k].SUnitStatus;
                        units.sUnitSTCode = currenteqp.Units[i].SUnitList[k].SUnitSTCode;

                        units.unitNo = currenteqp.Units[i].SUnitList[k].SubUnitNo;
                        units.currentStauts = currenteqp.Units[i].SUnitList[k].SUnitStatus;

                        equipments.units.Add(units);
                    }
                    if (currenteqp.Units[i].LoadingStop)
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

                #region Body
                List<Line> lines = new List<Line>();
                lines.Add(new Line()
                {
                    lineType = currenteqp.EQPID,
                    equipments = equipmentsList,
                    mesControlMode = mesControlMode,
                    lineStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, currenteqp.EqpStatus),
                    lineId = currenteqp.EQPID,
                    lineOperationMode = lineOperationMode,
                    lineOperationModeCode = lineOperationModeCode,
                    dispatchMode = currenteqp.RobotDispatchMode.ToString(),
                    coldRunTotalQuantity = currenteqp.ColdRunTotalQuantity,
                    coldRunCurrentQuantity = currenteqp.ColdRunCurrentQuantity
                });
                InitData.Add("lines", lines);
                WebSocketMessageStr.body = InitData;
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
