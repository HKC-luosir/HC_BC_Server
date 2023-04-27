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
    public class AllDataUpdateRequestHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(Dictionary<string, object> data)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "AllDataUpdateResponse",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = "admin"
            };
            #endregion
            try
            {
                //#region Body
                #region 相关赋值
                object eqpid;
                data.TryGetValue("lineId", out eqpid);

                //string mesControlMode = "", lineOperationMode = "";
                //int lineOperationModeCode = 0;

                var currenteqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid.ToString());


                var allData = logicService.GetOPILineInfo(currenteqp);
                //switch (currenteqp.ControlState)
                //{
                //    case EnumControlState.OffLine:
                //        mesControlMode = "OFFLINE";
                //        break;
                //    case EnumControlState.OnLineLocal:
                //        mesControlMode = "LOCAL";
                //        break;
                //    case EnumControlState.OnLineRemote:
                //        mesControlMode = "REMOTE";
                //        break;
                //    default:
                //        break;
                //}
                //Hashtable hashtable = new Hashtable();
                //hashtable.Add("eqpid", currenteqp.EQPID);
                //hashtable.Add("equipmentvalue", (int)currenteqp.LineMode);
                //var operationmode = dbService.Viewcfg_operationmode(hashtable).FirstOrDefault();
                //if (operationmode != null)
                //{
                //    lineOperationMode = operationmode.operationmodename;
                //    lineOperationModeCode = operationmode.equipmentvalue;
                //}
                //List<Equipments> equipmentsList = new List<Equipments>();

                ////组合线这里将所有线的设备都加进去
                //foreach (var eqpinfo in HostInfo.Current.AllEQPInfo)
                //{
                //    for (int i = 0; i < eqpinfo.Units.Count; i++)
                //    {
                //        var type = eqpinfo.Units[i].GetType().Name;
                //        if (type != "Unit" && type != "Robot")
                //        {
                //            continue;
                //        }
                //        string cimMode = "", cclinkStatus = "";
                //        #region 相关转换处理处理
                //        if (eqpinfo.Units[i].CIMMode)
                //        {
                //            cimMode = "CIMON";
                //        }
                //        else
                //        {
                //            cimMode = "CIMOFF";
                //        }

                //        if (eqpinfo.Units[i].IsConnect == "Alive")
                //        {
                //            cclinkStatus = "ON";
                //        }
                //        else
                //        {
                //            cclinkStatus = "OFF";
                //        }

                //        #endregion
                //        Equipments equipments = new Equipments();
                //        equipments.currentStatus = eqpinfo.Units[i].UnitStatus;//设备状态
                //        equipments.reportMode = "PLC";
                //        equipments.jobCount = eqpinfo.Units[i].CurrentWIPCount;//JobCount
                //        equipments.alive = eqpinfo.Units[i].IsConnect;
                //        equipments.cimMode = cimMode;
                //        equipments.cclinkStatus = cclinkStatus;
                //        equipments.equipmentId = eqpinfo.Units[i].UnitID;
                //        equipments.equipmentNo = eqpinfo.Units[i].UnitNo;
                //        equipments.eqpOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, eqpinfo.Units[i].UnitMode.ToString());
                //        equipments.equipmentName = eqpinfo.Units[i].UnitName;
                //        equipments.stcode = eqpinfo.Units[i].UnitSTCode;
                //        equipments.recipeIdCheck = eqpinfo.Units[i].CurrentRecipeID.ToString();
                //        equipments.indexerOperationMode = HostInfo.Current.GetBCToMESValue(MESEventItem.OperationMode, eqpinfo.LineMode.ToString());
                //        equipments.autoRecipeChangeMode = "";
                //        string UpstreamInlineMode = "";
                //        if (eqpinfo.Units[i].UpstreamInlineMode)
                //        {
                //            UpstreamInlineMode = "ON";
                //        }
                //        else
                //        {
                //            UpstreamInlineMode = "OFF";
                //        }
                //        equipments.upstreamInlineMode = UpstreamInlineMode;
                //        string DownstreamInlineMode = "";
                //        if (eqpinfo.Units[i].DownstreamInlineMode)
                //        {
                //            DownstreamInlineMode = "ON";
                //        }
                //        else
                //        {
                //            DownstreamInlineMode = "OFF";
                //        }
                //        equipments.downstreamInlineMode = DownstreamInlineMode;
                //        string VCREnableMode = "";
                //        Hashtable map = new Hashtable
                //    {
                //        {"EQPID",eqpinfo.EQPID },
                //        {"UnitID",eqpinfo.Units[i].UnitID },
                //    };
                //        var vcr = dbService.ViewVCRList(map).FirstOrDefault();
                //        if (vcr != null)
                //        {
                //            switch (vcr.VCREnableMode)
                //            {
                //                case 1:
                //                    VCREnableMode = "Enable";
                //                    break;
                //                case 2:
                //                    VCREnableMode = "Disable";
                //                    break;
                //                default:
                //                    break;
                //            }
                //        }
                //        equipments.vcrEnableMode = VCREnableMode;
                //        string CommandType = "";
                //        switch (eqpinfo.Units[i].CommandType)
                //        {
                //            case 1:
                //                CommandType = "CCLink";
                //                break;
                //            case 2:
                //                CommandType = "HSMS";
                //                break;
                //            default:
                //                break;
                //        }
                //        equipments.commandType = CommandType;
                //        equipments.loaderQTime = eqpinfo.Units[i].PortQTime;
                //        equipments.ports = new List<Ports>();
                //        var portlist = HostInfo.Current.PortList.Where(t => t.EQPID.Contains(eqpinfo.Units[i].EQPID == null ? "null" : eqpinfo.Units[i].EQPID)).ToList();
                //        for (int j = 0; j < portlist.Count(); j++)
                //        {
                //            Ports portss = new Ports();
                //            portss.equipmentNo = portlist[j].UnitID;
                //            portss.unitId = portlist[j].UnitID;
                //            portss.portId = portlist[j].PortID;
                //            portss.portStatus = Consts.dicPortStatus.ContainsKey(portlist[j].PortStatus) ? Consts.dicPortStatus[portlist[j].PortStatus] : "";
                //            portss.portMode = portlist[j].PortMode.ToString();
                //            portss.portCSTType = Consts.dicPortCassetteType.ContainsKey(portlist[j].PortCSTType) ? Consts.dicPortCassetteType[portlist[j].PortCSTType] : "";
                //            portss.portOperationMode = portlist[j].PortOperationMode.ToString();
                //            portss.portTypeAutoChg = Consts.dicPortTypeAutoChangeMode.ContainsKey(portlist[j].PortTypeAutoChangeMode) ? Consts.dicPortTypeAutoChangeMode[portlist[j].PortTypeAutoChangeMode] : "";
                //            portss.portQTime = portlist[j].PortQTime;
                //            portss.portGrade = portlist[j].PortGrade;
                //            portss.portEnableMode = Consts.dicPortEnableMode.ContainsKey(portlist[j].PortEnableMode) ? Consts.dicPortEnableMode[portlist[j].PortEnableMode] : "";
                //            portss.porttype = portlist[j].PortType;
                //            portss.transferMode = portlist[j].TransferMode;
                //            portss.portPauseMode = Consts.dicPortPauseMode.ContainsKey(portlist[j].PortPauseMode) ? Consts.dicPortPauseMode[portlist[j].PortPauseMode] : "";
                //            portss.cassetteSeq = portlist[j].CassetteSequenceNo;
                //            portss.cstid = portlist[j].CassetteID;
                //            //portss.cassetteStatus = portlist[j].CassetteInfo.CassetteStatus.ToString();
                //            //portss.capacity = portlist[j].Capacity;
                //            //portss.partialFullFlag = null;
                //            //portss.glassExistence = portlist[j].CassetteInfo.JobExistenceSlot;
                //            //portss.jobCountIncassette = portlist[j].CassetteInfo.ProductQuantity;//portlist[j].GlassInfos.Count();
                //            //portss.completedCassetteData = portlist[j].CassetteInfo.CompeletedCassetteData;
                //            equipments.ports.Add(portss);
                //        }
                //        equipments.units = new List<Units>();
                //        for (int k = 0; k < eqpinfo.Units[i].SUnitList.Count(); k++)
                //        {
                //            Units units = new Units();
                //            units.unitId = eqpinfo.Units[i].SUnitList[k].UnitID;
                //            units.subUnitId = eqpinfo.Units[i].SUnitList[k].SUnitID;
                //            units.subUnitName = eqpinfo.Units[i].SUnitList[k].SUnitName;
                //            units.subUnitNo = eqpinfo.Units[i].SUnitList[k].SubUnitNo;
                //            units.sUnitStatus = eqpinfo.Units[i].SUnitList[k].SUnitStatus;
                //            //units.sUnitSTCode = eqpinfo.Units[i].SUnitList[k].SUnitSTCode;
                //            //units.unitNo = eqpinfo.Units[i].SUnitList[k].SubUnitNo;
                //            //units.currentStauts = eqpinfo.Units[i].SUnitList[k].SUnitStatus;
                //            equipments.units.Add(units);
                //        }
                //        if (eqpinfo.Units[i].LoadingStop)
                //        {
                //            equipments.localAlarmStatus = "ON";
                //        }
                //        else
                //        {
                //            equipments.localAlarmStatus = "OFF";
                //        }
                //        equipmentsList.Add(equipments);

                //    }
                //}

                //#endregion
                //string lineType = currenteqp.LineType.ToString();
                //AllData allData = new AllData()
                //{
                //    serverName = currenteqp.EQPID,
                //    line = new Line()
                //    {
                //        lineType = lineType,
                //        equipments = equipmentsList,
                //        mesControlMode = mesControlMode,
                //        lineStatus = HostInfo.Current.GetBCToMESValue(MESEventItem.ModuleState, currenteqp.EqpStatus),
                //        lineId = currenteqp.EQPID,
                //        lineOperationMode = lineOperationMode,
                //        lineOperationModeCode = lineOperationModeCode,
                //        dispatchMode = currenteqp.RobotDispatchMode.ToString(),
                //        coldRunTotalQuantity = currenteqp.ColdRunTotalQuantity,
                //        coldRunCurrentQuantity = currenteqp.ColdRunCurrentQuantity
                //    }
                //};
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
                Logger.Info( ex);
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
