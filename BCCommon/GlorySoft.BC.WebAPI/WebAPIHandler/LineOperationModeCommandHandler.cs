using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.EIP;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class LineOperationModeCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "LineOperationModeCommandReply",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                object eqpid, value, unitname;
                InitHistory.TryGetValue("eqpid", out eqpid);
                InitHistory.TryGetValue("value", out value);
                InitHistory.TryGetValue("UnitName", out unitname);

                var LineMode = (LineMode)Enum.Parse(typeof(LineMode), value.ToString(), true);
                var EQPInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid.ToString());
                EQPInfo.LineMode = (LineMode)Enum.Parse(typeof(LineMode), value.ToString(), true);
                EQPInfo.FunctionName = this.GetType().Name;
                dbService.UpdateEQPInfo(EQPInfo);

                var txid = HostInfo.Current.GetTransactionID();
                foreach (var unit in EQPInfo.Units)
                {
                    if (unit.UnitName == unitname.ToString())
                    {
                        unit.UnitMode = Convert.ToInt32(value);
                        unit.FunctionName = this.GetType().Name;
                        dbService.UpdateUnitInfo(unit);

                        //所有设备下发machinemodechangecommand
                        eqpService.SendMachineModeChangeCommand(unit.UnitName, unit.UnitMode.ToString(), txid);
                    }
                }

                //// eqpCmd.EquipmentOperationModeChangeCommand()
                //if (HostInfo.EQPInfo.LineMode == LineMode.MixRun)
                //{
                //    logicService.ModifyMixRunConfig();
                //}

                //#region ForceCleanOut
                //if (HostInfo.EQPInfo.LineMode == LineMode.ForceCleanOut || HostInfo.EQPInfo.LineMode == LineMode.MultiChamberForceCleanOut)
                //{

                //    foreach (var port in HostInfo.PortList)
                //    {
                //        if (port.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing)
                //        {
                //            LogHelper.BCLog.DebugFormat("[ForceCleanOut] port.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing");
                //            //將剩餘glass.PROCESSINGINFO 修改為B
                //            //var glassList = port.GlassInfos;//.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait).ToList();
                //            var glassList = port.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait).ToList();
                //            foreach (var item in glassList)
                //            {
                //                item.ProcessingInfo = "B";
                //                item.SlotSatus = EnumGlassSlotStatus.Skip;
                //                dbService.UpdateWIPSlotSatus(item);
                //                dbService.UpdateGlassInfo(item);
                //                LogHelper.BCLog.DebugFormat("[ForceCleanOut] glass.SlotSatus = EnumGlassSlotStatus.Skip;ProcessingInfo=B [UpdateGlassInfo]; glass[{0},{1}]", item.CassetteSequenceNo, item.SlotSequenceNo);
                //                item.FunctionName = "OPI ForceCleanOut";//this.GetType().Name;
                //                dbService.InsertHisGlassInfo(item);
                //                var glassPort = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == item.PortID);
                //                HostInfo.Current.PortPanelInforUpdate(glassPort, item);
                //            }
                //            port.CassetteInfo.CassetteStatus = EnumCarrierStatus.ProcessCompleted;
                //            dbService.UpdatePortInfo(port);
                //            LogHelper.BCLog.DebugFormat("[ForceCleanOut]port.CassetteInfo.CassetteStatus = EnumCarrierStatus.CassetteProcessAbort; [UpdatePortInfo]; ");
                //            port.FunctionName = "OPI ForceCleanOut";//this.GetType().Name;
                //            dbService.InsertHisPortInfoResult(port);
                //            port.CassetteInfo.FunctionName = "OPI ForceCleanOut";
                //            dbService.InsertHisCassette(port.CassetteInfo);
                //        }
                //        else if (port.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforCassetteData ||
                //            port.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforStartCommand ||
                //            port.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing)
                //        {
                //            // WaitingforCassetteData = 2, [EnumMember]
                //            //WaitingforStartCommand = 3,  [EnumMember]
                //            //WaitingforProcessing = 4, [EnumMember]
                //            //eqpCmd.OPICommandByPort(port.UnitName, 3, port.PortNo);
                //            //port = HostInfo.PortList.FirstOrDefault(o => o.PortID == portId.ToString());
                //            var jobExistence = "00000000000000000000000000000000";
                //            //var slotToProcess = HostInfo.Current.SlotToProcess;
                //            var jobCount = 0;                           
                //            eqpCmd.CassetteControlCommand(port.UnitName, 3, jobExistence, jobExistence, jobCount, port.PortNo);//松卡
                //            logicService.JobStartCancel(port,"", "");//发MES JOB_PROCESS_CANCEL
                //            LogHelper.BCLog.Debug(string.Format("OPI ForceCleanOut portid:{0};Cancel;", port.PortID));
                //        }
                //        logicService.CheckMixRunPortExist(port);
                //    }
                //    LogHelper.BCLog.Debug(string.Format("[ForceCleanOut Modify;]   RemainedGlassFlag = false;"));
                //    HostInfo.EQPInfo.RemainedGlassFlag = false;

                //    if (HostInfo.Current.EQPID.Contains("CVD"))
                //    {
                //        HostInfo.Current.EQPInfo.TrninCVDFlag = false;
                //    }

                //    //todo send ForceCleanOut mode to IMP
                //    if (HostInfo.EQPInfo.LineMode == LineMode.MultiChamberForceCleanOut&& HostInfo.Current.EQPID == "A1IMP100")
                //    {
                //        eqpCmd.WriteForceCleanOutCommand("IMP", 1);
                //    }
                //}
                //else if(HostInfo.EQPInfo.LineMode == LineMode.MultiChamber)
                //{
                //    //todo send MultiChamber mode to IMP
                //    if (HostInfo.Current.EQPID == "A1IMP100")
                //    {
                //        eqpCmd.WriteForceCleanOutCommand("IMP", 0);
                //    }
                //}
                //#endregion

                WebSocketMessageStr.body = null;
               // logicService.OperationModeChanged();
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
            opiHis.Add("operating", "进行了LineOperationMode切换操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
