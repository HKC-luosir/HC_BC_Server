using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class MESControlModeCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "MESControlModeCommandResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                object lineId, mesControlMode;
                InitData.TryGetValue("lineId", out lineId);
                InitData.TryGetValue("mesControlMode", out mesControlMode);
                var ControlMode = "";
                var currenteqp = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == lineId.ToString());
                var oldControlState = currenteqp.ControlState;
                EnumControlState EQPEnumControlState = EnumControlState.OffLine;
                switch (mesControlMode.ToString())
                {
                    case "OFFLINE":
                        ControlMode = "OFFLINE";
                        EQPEnumControlState = EnumControlState.OffLine;
                        break;
                    case "LOCAL":
                        ControlMode = "ONLINE";
                        EQPEnumControlState = EnumControlState.OnLineLocal;
                        break;
                    case "REMOTE":
                        ControlMode = "ONLINE";
                        EQPEnumControlState = EnumControlState.OnLineRemote;
                        break;
                    default:
                        break;
                }

                bool issuccess = false;

                if (oldControlState == EnumControlState.OffLine && ControlMode != "OFFLINE")
                {
                    RVAreYouThere rvayt = new RVAreYouThere();
                    rvayt.EQUIPMENTID = lineId.ToString();
                    rvService.SendToMESAreYouThere(rvayt.EQUIPMENTID, rvayt, HostInfo.Current.GetTransactionID());
                }
                RVCommunication data = new RVCommunication();
                data.EQUIPMENTID = lineId.ToString();
                data.COMMUNICATIONSTATE = ControlMode;
                var res = rvService.SendToMESCommunicationReport(data.EQUIPMENTID, data, HostInfo.Current.GetTransactionID());
                if (ControlMode == "OFFLINE")
                {
                    issuccess = true;
                }
                else
                {
                    if (res != null && res.RESULT == MESResult.SUCCESS.ToString())//相同 标识切换成功
                    {
                        issuccess = true;
                    }
                    else
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "0",
                            returnMessageEN = "Operation sucessful !",
                            returnMessageCH = "MES切换状态失败，请检查MES接口返回值！"
                        };
                    }
                }

                if (issuccess)
                {
                    //更新缓存
                    currenteqp.ControlState = EQPEnumControlState;
                    //更新数据库
                    dbService.UpdateEQPInfo(currenteqp);
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "0",
                        returnMessageEN = "Operation sucessful !",
                        returnMessageCH = "操作成功！"
                    };
                }


                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == lineId.ToString());

                if (oldControlState == EnumControlState.OffLine && ControlMode != "OFFLINE")
                {
                    //发送MES Equipment State Report
                    RVEquipmentState mesline = new RVEquipmentState();
                    mesline.EQUIPMENTID = eqpinfo.EQPID;
                    mesline.COMCLASS = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, eqpinfo.EqpStatus);
                    mesline.STATE = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, eqpinfo.EqpStatus);
                    rvService.SendToMESEquipmentStateReport(eqpinfo.EQPID, mesline, HostInfo.Current.GetTransactionID());

                    //发送MES
                    var eqpport = HostInfo.Current.PortList.Where(c => c.EQPID == eqpinfo.EQPID).ToList();
                    for (int i = 0; i < eqpport.Count; i++)
                    {
                        RVPortState mesportstate = new RVPortState();
                        mesportstate.EQUIPMENTID = eqpinfo.EQPID;
                        mesportstate.PORTLIST.Add(new RVPortStateInfo() { PORTID = eqpport[i].PortID, PORTNUM = eqpport[i].PortNo.ToString(), PORTSTATE = eqpport[i].PortStatus == 5 ? "DOWN" : "UP", PORTTYPE = eqpport[i].PortType, DURABLEID = (!String.IsNullOrEmpty(eqpport[i].CassetteID) ? eqpport[i].CassetteID : "") });
                        rvService.SendToMESEquipmentPortReport(eqpinfo.EQPID, mesportstate, HostInfo.Current.GetTransactionID());
                    }

                    //上报设备各Unit
                    RVUnitState meseqp = new RVUnitState();
                    meseqp.EQUIPMENTID = eqpinfo.EQPID;
                    foreach (var unit in eqpinfo.Units)
                    {
                        var levelthree = !String.IsNullOrEmpty(unit.ReasonCode) ? HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatusLevelThree, unit.UnitStatus + "_" + unit.ReasonCode) : HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, unit.UnitStatus);
                        var unitstat = new RVUnitList()
                        {
                            UNITID = unit.UnitID,
                            COMCLASS = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, unit.UnitStatus),
                            STATE = levelthree
                        };
                        meseqp.UNITLIST.Add(unitstat);
                    }
                    rvService.SendToMESUnitStateReport(eqpinfo.EQPID, meseqp, HostInfo.Current.GetTransactionID());

                    //上报Unit各Sunit
                    for (int i = 0; i < eqpinfo.Units.Count; i++)
                    {
                        RVUnitState meseqpsunit = new RVUnitState();
                        meseqpsunit.EQUIPMENTID = eqpinfo.Units[i].UnitID;

                        var levelthree = !String.IsNullOrEmpty(eqpinfo.Units[i].ReasonCode) ? HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatusLevelThree, eqpinfo.Units[i].UnitStatus + "_" + eqpinfo.Units[i].ReasonCode) : HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, eqpinfo.Units[i].UnitStatus);
                        foreach (var unit in eqpinfo.Units[i].SUnitList)
                        {
                            var sunitstat = new RVUnitList()
                            {
                                UNITID = unit.SUnitID,
                                COMCLASS = HostInfo.Current.GetBCToMESValue(MESEventItem.EQPStatus, unit.SUnitStatus),
                                STATE = levelthree
                            };
                            meseqpsunit.UNITLIST.Add(sunitstat);
                        }
                        rvService.SendToMESUnitStateReport(eqpinfo.EQPID, meseqpsunit, HostInfo.Current.GetTransactionID());
                    }
                }

                WebSocketMessageStr.body = null;


                //#region offline 切 local  校验
                //if (HostInfo.Current.EQPInfo.ControlState== EnumControlState.OffLine)
                //{
                //    LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] Current ControlState is OffLine"));
                //    if(mesControlMode.ToString()== "OnLineLocal")
                //    {
                //        LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] New ControlState is OnLineLocal"));

                //        Task.Factory.StartNew(() =>
                //        {
                //            var CurrentThread = System.Threading.Thread.CurrentThread.ManagedThreadId.ToString();
                //            LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}] OffLine>OnLineLocal begin", CurrentThread));
                //            foreach (var port in HostInfo.Current.PortList)
                //            {
                //                bool cstCheck = false;
                //                if (port.PortStatus == 2)
                //                {
                //                    if (port.CassetteInfo != null)
                //                    {
                //                        if (port.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforCassetteData)
                //                        {
                //                            LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}] PortID:{1}; CassetteStatus:WaitingforCassetteData;PortStatus:InUse", CurrentThread, port.PortID));
                //                            cstCheck = true;
                //                        }
                //                        else
                //                        {
                //                            LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}]PortID:{1}; CassetteStatus:{2};PortStatus:InUse", CurrentThread, port.PortID, port.CassetteInfo.CassetteStatus));
                //                        }
                //                    }
                //                    else
                //                    {
                //                        LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}]PortID:{1};PortStatus:{2}; CassetteInfo is null", CurrentThread, port.PortID, port.PortStatus));
                //                    }
                //                }
                //                else
                //                {
                //                    LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}]PortID:{1};PortStatus:{2}", CurrentThread, port.PortID, port.PortStatus));
                //                }
                //                if (cstCheck)
                //                {
                //                    CST_VALIDATION_REQUEST cstValidationRequest = new CST_VALIDATION_REQUEST();
                //                    cstValidationRequest.EQPID = HostInfo.Current.EQPID;
                //                    cstValidationRequest.PORTID = port.PortID;
                //                    cstValidationRequest.PORTSTATE = HostInfo.GetBCToMESValue(MESEventItem.PORTSTATUS, port.PortStatus.ToString());
                //                    cstValidationRequest.PORTTYPE = port.PortType;
                //                    cstValidationRequest.PORTUSETYPE = port.PortUseType;
                //                    cstValidationRequest.TRANSFERMODE = port.TransferMode;
                //                    cstValidationRequest.CSTID = port.CassetteID;
                //                    cstValidationRequest.SLOTINFO = port.CassetteInfo.SlotMap;// jobExistenceSlot;
                //                    rvService.SendToMESCST_VALIDATION_REQUEST(cstValidationRequest);
                //                    LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}][SendToMESCST_VALIDATION_REQUEST]PortID:{1}; Sleep:60s", CurrentThread, port.PortID));
                //                    Thread.Sleep(1000 * 60);
                //                }
                //            }
                //            LogHelper.BCLog.Debug(string.Format("[MESControlModeCommandHandler] [Thread:{0}] OffLine>OnLineLocal end", CurrentThread));
                //        });                     
                //    }
                //}
                //#endregion
                //COMM_STATE_CHANGE commStateChange = new COMM_STATE_CHANGE();

                //commStateChange.COMMUNICATIONSTATE = mesControlMode.ToString();
                //HostInfo.EQPInfo.NewControlState = (EnumControlState)Enum.Parse(typeof(EnumControlState), commStateChange.COMMUNICATIONSTATE, true);
                //HostInfo.Current.EQPInfo.ControlState = HostInfo.EQPInfo.NewControlState;
                //commStateChange.EQPID = lineId.ToString();
                //rvService.SendToMESCOMM_STATE_CHANGE(commStateChange);

                //WebSocketMessageStr.body = null;
                //WebSocketMessageStr.result = new WebSocketResult()
                //{
                //    returnCode = "0",
                //    returnMessageEN = "Operation sucessful !",
                //    returnMessageCH = "操作成功！"
                //};
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
            opiHis.Add("operating", "进行了MESControlMode切换操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
