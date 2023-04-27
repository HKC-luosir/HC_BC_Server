using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Logic.Contract;
using GlorySoft.BC.WebSocket;
using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Glorysoft.BC.Logic.Service
{
    public class PortService : AbstractEventHandler, IPortService
    {
        #region matti
        public void PortTypeChangeReport(Unit oEQP, string PortType, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortType, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortType = HostInfo.Current.GetEQToBCValue(MESEventItem.PortType, PortType);//Consts.dicPortType[Convert.ToInt32(portinfo.PortType)];
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                    //发送MES
                    RVPortType data = new RVPortType();
                    data.EQUIPMENTID = oEQP.EQPID;
                    data.PORTID = portinfo.PortID;
                    data.PORTTYPE = portinfo.PortType;
                    data.PORTNUM = portinfo.PortNo.ToString();
                    mesService.SendToMESPortTypeReport(oEQP.EQPID, data, transactionID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortTypeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortTypeAutoChangeModeReport(Unit oEQP, string PortTypeAutoChangeMode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortTypeAutoChangeMode, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortTypeAutoChangeMode = Convert.ToInt32(PortTypeAutoChangeMode);
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                    ////发送MES
                    //RVPortType data = new RVPortType();
                    //data.EQUIPMENTID = oEQP.EQPID;
                    //data.PORTID = portinfo.PortID;
                    //data.PORTTYPE = Consts.dicPortTypeAutoChangeMode[portinfo.PortTypeAutoChangeMode];
                    //data.DURABLEID = portinfo.CassetteID;
                    //mesService.SendToMESPortTypeReport(data, transactionID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortTypeAutoChangeModeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortModeChangeReport(Unit oEQP, string PortMode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortMode, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortMode = Convert.ToInt32(PortMode);
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortModeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortEnableModeChangeReport(Unit oEQP, string PortEnableMode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortEnableMode, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortEnableMode = Convert.ToInt32(PortEnableMode);
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortEnableModeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortTransferModeChangeReport(Unit oEQP, string PortTransferMode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortTransferMode, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.TransferMode = PortTransferMode;
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortTransferModeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortPauseModeChangeReport(Unit oEQP, string PortPauseMode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortPauseMode, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortPauseMode = Convert.ToInt32(PortPauseMode);
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortPauseModeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortGradeChangeReport(Unit oEQP, string PortGrade, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortGrade, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortGrade = PortGrade;
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortGradeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortCassetteTypeChangeReport(Unit oEQP, string PortCassetteType, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortCassetteType, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortCSTType = Convert.ToInt32(PortCassetteType);
                    var PortCapacity = HostInfo.GetEQToBCValue(PLCEventItem.PortCassetteTypeToPortCapacity, PortCassetteType, true);
                    if (!String.IsNullOrEmpty(PortCapacity))
                        portinfo.Capacity = Convert.ToInt32(PortCapacity);
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                    //发送MES
                    RVPortCSTType data = new RVPortCSTType();
                    data.EQUIPMENTID = oEQP.EQPID;
                    data.PORTID = portinfo.PortID;
                    data.PORTTYPE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortType, portinfo.PortType);
                    data.PORTNUM = portinfo.PortNo.ToString();
                    data.PORTUSETYPE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortCassetteType, portinfo.PortCSTType.ToString());//Consts.dicPortCassetteType[portinfo.PortCSTType];
                    mesService.SendToMESPortCSTTypeChangeReport(oEQP.EQPID, data, transactionID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortCassetteTypeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortTypeChangeCommandReply(Unit oEQP, string PortTypeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortTypeReturnCode, portNo, transactionID }));
                #endregion

                string result = PortTypeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PortTypeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortTypeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortTypeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortTransferModeChangeCommandReply(Unit oEQP, string TransferModeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, TransferModeReturnCode, portNo, transactionID }));
                #endregion

                string result = TransferModeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = TransferModeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortTransferModeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortTransferModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortEnableModeChangeCommandReply(Unit oEQP, string EnableModeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, EnableModeReturnCode, portNo, transactionID }));
                #endregion

                string result = EnableModeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = EnableModeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortEnableModeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortEnableModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortTypeAutoChangeModeCommandReply(Unit oEQP, string PortTypeAutoChangeModeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortTypeAutoChangeModeReturnCode, portNo, transactionID }));
                #endregion

                string result = PortTypeAutoChangeModeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PortTypeAutoChangeModeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortTypeAutoChangeModeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortTypeAutoChangeModeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortCassetteTypeChangeCommandReply(Unit oEQP, string PortCassetteTypeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortCassetteTypeReturnCode, portNo, transactionID }));
                #endregion

                string result = PortCassetteTypeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PortCassetteTypeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortCassetteTypeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortCassetteTypeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortModeChangeCommandReply(Unit oEQP, string PortModeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortModeReturnCode, portNo, transactionID }));
                #endregion

                string result = PortModeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PortModeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortModeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortPauseModeChangeCommandReply(Unit oEQP, string PauseModeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PauseModeReturnCode, portNo, transactionID }));
                #endregion

                string result = PauseModeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PauseModeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortPauseModeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortPauseModeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortGradeChangeCommandReply(Unit oEQP, string PortGradeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortGradeReturnCode, portNo, transactionID }));
                #endregion

                string result = PortGradeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PortGradeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortGradeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortGradeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortQTimeChangeCommandReply(Unit oEQP, string PortQTimeReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortQTimeReturnCode, portNo, transactionID }));
                #endregion

                string result = PortQTimeReturnCode == "1" ? "OK" : "NG";
                string resultlevel = PortQTimeReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortQTimeChangeCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortQTimeChangeCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortQTimeChangeReport(Unit oEQP, int portNo, int portQTime, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portQTime, portNo, transactionID }));
                #endregion

                if (HostInfo.Current.PortList.Any(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo))
                {
                    //更新缓存
                    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                    portinfo.PortQTime = portQTime;
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortQTimeChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortControlCommandReply(Unit oEQP, string PortControlCommandReturnCode, int portNo, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, PortControlCommandReturnCode, portNo, transactionID }));
                #endregion

                string result = HostInfo.Current.GetEQToBCValue(MESEventItem.PortControlCommandReturnCode, PortControlCommandReturnCode);
                string resultlevel = PortControlCommandReturnCode == "1" ? "" : "error";
                string msg = string.Format("PortControlCommand EQP:{0} Port:{1} Result:{2}", oEQP.UnitID, portNo.ToString(), result);
                SendOPIMessage.SendToWebSocketClientReport(new HostReturnMessage() { returnLevel = resultlevel, message = msg });
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortControlCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortBoxInfoRequest(Unit oEQP, int portNo, string BoxID, int lotSequenceNumber, int jobCountInCassette, string jobExistenceSlot, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, BoxID, transactionID }));
                #endregion

                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portinfo != null)
                {
                    bool checkok = false;
                    string[] containEQP = new string[] { "DOM", "OLB" };
                    if (containEQP.Contains(oEQP.EQPID.Substring(2, 3)))
                    {
                        #region 清残帐
                        //ClearPortGlassData(portinfo, "Old Data Clear");
                        #endregion
                        //portinfo.WaitingforProcessingTime = DateTime.Now;
                        if (string.IsNullOrEmpty(BoxID))
                        {
                            portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                            portinfo.CassetteCancelText = "EQP report BoxID is empty";
                            eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                            return;
                        }
                        portinfo.CassetteID = BoxID;
                        portinfo.CassetteInfo.PortID = portinfo.PortID;
                        portinfo.CassetteInfo.PortNo = portinfo.PortNo;
                        portinfo.CassetteInfo.CassetteID = BoxID;
                        //portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforCassetteData;
                        portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;
                        portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                        portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                        portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                        portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;

                        var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                        if (IsInLineMode(oEQP.EQPID))
                        {
                            //由client端down帐
                            dbService.InsertCassette(portinfo.CassetteInfo);
                            eqpService.SendPortBoxInfoRequestReply(oEQP.UnitName, portNo, "1", transactionID);
                        }
                        else
                        {
                            //RVMultiBoxLoadComplete loadComplete = new RVMultiBoxLoadComplete();
                            //loadComplete.EQUIPMENTID = oEQP.EQPID;
                            //loadComplete.PORTID = portinfo.PortID;
                            //loadComplete.PORTTYPE = portinfo.PortType;
                            //loadComplete.PORTNUM = portNo.ToString();
                            //loadComplete.DURABLEID = BoxID;
                            //mesService.SendToMESMultiBoxLoadComplete(oEQP.EQPID, loadComplete, transactionID);

                            //if (portinfo.PortType != "PU")
                            //{
                            //    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                            //    portinfo.CassetteCancelText = "PortType is not PU";
                            //    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                            //    return;
                            //}
                            if (portinfo.PortType == "PL")
                            {
                                RVLotInfoRequest lotInfoRequest = new RVLotInfoRequest();
                                lotInfoRequest.EQUIPMENTID = oEQP.EQPID;
                                lotInfoRequest.PORTID = portinfo.PortID;
                                lotInfoRequest.PORTTYPE = portinfo.PortType;
                                lotInfoRequest.DURABLEID = BoxID;

                                portinfo.IsMESCarrierInfoDownload = false;
                                var lotInfoRequestReply = mesService.SendToMESLotInfoRequest(oEQP.EQPID, lotInfoRequest, transactionID);

                                if (lotInfoRequestReply != null)
                                {
                                    if (lotInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                    {
                                        //portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                        //portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                        //portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                        //portinfo.CassetteInfo.CassetteID = lotInfoRequestReply.DURABLEID;
                                        //portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                        portinfo.CassetteInfo.CarrierType = lotInfoRequestReply.DURABLETYPE;
                                        portinfo.CassetteInfo.ProductSpecName = lotInfoRequestReply.PARTNAME;
                                        portinfo.CassetteInfo.ProcessOperationName = lotInfoRequestReply.STEPNAME;
                                        portinfo.CassetteInfo.LotName = lotInfoRequestReply.LOTID;
                                        portinfo.CassetteInfo.LotType = lotInfoRequestReply.LOTTYPE;
                                        portinfo.CassetteInfo.MachineRecipeName = lotInfoRequestReply.RECIPENAME;
                                        portinfo.CassetteInfo.LotGrade = lotInfoRequestReply.GRADE;
                                        portinfo.CassetteInfo.ProductQuantity = HostInfo.FormatStringToInt(lotInfoRequestReply.MAINQTY);
                                        portinfo.CassetteInfo.UNITRECIPELIST = lotInfoRequestReply.UNITRECIPELIST;

                                        dbService.InsertCassette(portinfo.CassetteInfo);

                                        //根据portid获取modelposition  robot put时用于寻找target position
                                        var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault(c => c.PortID == portinfo.PortID);

                                        var glassInfoList = new List<GlassInfo>();
                                        var glassInfoListMES = lotInfoRequestReply.PANELLIST;
                                        foreach (var glassMES in glassInfoListMES)
                                        {
                                            var glass = new GlassInfo();
                                            glass.FunctionName = "MES Download Data";
                                            glass.PortID = portinfo.PortID;
                                            glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                                            glass.CassetteID = lotInfoRequestReply.DURABLEID;
                                            glass.CassetteSequenceNo = lotSequenceNumber;//在inuse那边再更新 这条消息没有这个字段值
                                            glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                            glass.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                            glass.WorkOrder = glassMES.WOID;
                                            glass.LotID = lotInfoRequestReply.LOTID;
                                            glass.ProductID = portinfo.CassetteInfo.ProductSpecName;
                                            glass.OperationID = portinfo.CassetteInfo.ProcessOperationName;
                                            glass.GlassID = glassMES.PANELID;
                                            glass.GlassGradeCode = glassMES.GRADE;
                                            glass.PanelGrade = glassMES.SUBGRADE;
                                            int mesPosition = Convert.ToInt32(glassMES.POSITION);
                                            //int slotPosition = mesPosition / 1000;//前后片
                                            //int slotNo = mesPosition % 1000;
                                            glass.Position = mesPosition;
                                            glass.SlotPosition = 0;
                                            glass.SlotSequenceNo = mesPosition;
                                            glass.BLID = glassMES.BONDINGID;
                                            glass.AbnormalCodes = glassMES.ABNORMALCODE;
                                            SetJobPPID(ref glass, lotInfoRequestReply.UNITRECIPELIST);
                                            glassInfoList.Add(glass);
                                            var ret = dbService.InsertGlassInfo(glass);
                                        }
                                        if (glassInfoList.Count > 0)
                                            portinfo.GlassInfos.AddRange(glassInfoList);

                                        //SlotMap校验成功，进行RMS校验
                                        if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                        {
                                            bool slotMapValidation = true;//TBD
                                            if (slotMapValidation)
                                            {
                                                bool rmsCheck = portinfo.PortType != "PL" ? true : RecipeCheckRequest(lotInfoRequestReply.UNITRECIPELIST, oEQP, portinfo, transactionID, false);
                                                if (rmsCheck)
                                                {
                                                    //OK
                                                    checkok = true;
                                                }
                                                //else
                                                //{
                                                //    //NG
                                                //}
                                            }
                                            else
                                            {
                                                //NG
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //NG
                                    }
                                }
                            }
                            else if (portinfo.PortType == "PU")
                            {
                                RVCarrierInfoDownload lotInfoRequest = new RVCarrierInfoDownload();
                                lotInfoRequest.EQUIPMENTID = oEQP.EQPID;
                                lotInfoRequest.PORTID = portinfo.PortID;
                                lotInfoRequest.PORTTYPE = portinfo.PortType;
                                lotInfoRequest.DURABLEID = BoxID;

                                portinfo.IsMESCarrierInfoDownload = true;
                                var lotInfoRequestReply = mesService.SendToMESCarrierInfoDownload(oEQP.EQPID, lotInfoRequest, transactionID);

                                if (lotInfoRequestReply != null)
                                {
                                    if (lotInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                    {
                                        //portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                        //portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                        //portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                        //portinfo.CassetteInfo.CassetteID = lotInfoRequestReply.DURABLEID;
                                        //portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                        portinfo.CassetteInfo.CarrierType = lotInfoRequestReply.DURABLETYPE;
                                        portinfo.CassetteInfo.ProductSpecName = lotInfoRequestReply.PARTNAME;
                                        portinfo.CassetteInfo.ProcessOperationName = lotInfoRequestReply.STEPNAME;
                                        portinfo.CassetteInfo.LotName = lotInfoRequestReply.LOTID;
                                        portinfo.CassetteInfo.LotType = lotInfoRequestReply.LOTTYPE;
                                        portinfo.CassetteInfo.MachineRecipeName = lotInfoRequestReply.RECIPENAME;
                                        portinfo.CassetteInfo.LotGrade = lotInfoRequestReply.GRADE;
                                        portinfo.CassetteInfo.ProductQuantity = HostInfo.FormatStringToInt(lotInfoRequestReply.MAINQTY);
                                        portinfo.CassetteInfo.UNITRECIPELIST = lotInfoRequestReply.UNITRECIPELIST;
                                        dbService.InsertCassette(portinfo.CassetteInfo);

                                        //根据portid获取modelposition  robot put时用于寻找target position
                                        var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault(c => c.PortID == portinfo.PortID);

                                        var glassInfoList = new List<GlassInfo>();
                                        var glassInfoListMES = lotInfoRequestReply.PANELLIST;
                                        foreach (var glassMES in glassInfoListMES)
                                        {
                                            var glass = new GlassInfo();
                                            glass.FunctionName = "MES Download Data";
                                            glass.PortID = portinfo.PortID;
                                            glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                                            glass.CassetteID = lotInfoRequestReply.DURABLEID;
                                            glass.CassetteSequenceNo = lotSequenceNumber; //在inuse那边再更新 这条消息没有这个字段值
                                            glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                            glass.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                            glass.WorkOrder = glassMES.WOID;
                                            glass.LotID = lotInfoRequestReply.LOTID;
                                            glass.ProductID = portinfo.CassetteInfo.ProductSpecName;
                                            glass.OperationID = portinfo.CassetteInfo.ProcessOperationName;
                                            glass.GlassID = glassMES.PANELID;
                                            glass.GlassGradeCode = glassMES.GRADE;
                                            glass.PanelGrade = glassMES.SUBGRADE;
                                            int mesPosition = Convert.ToInt32(glassMES.POSITION);
                                            //int slotPosition = mesPosition / 1000;//前后片
                                            //int slotNo = mesPosition % 1000;
                                            glass.Position = mesPosition;
                                            glass.SlotPosition = 0;
                                            glass.SlotSequenceNo = mesPosition;
                                            glass.BLID = glassMES.BONDINGID;
                                            glass.AbnormalCodes = glassMES.ABNORMALCODE;
                                            SetJobPPID(ref glass, lotInfoRequestReply.UNITRECIPELIST);
                                            glassInfoList.Add(glass);
                                            var ret = dbService.InsertGlassInfo(glass);
                                        }
                                        if (glassInfoList.Count > 0)
                                            portinfo.GlassInfos.AddRange(glassInfoList);
                                        //SlotMap校验成功，进行RMS校验
                                        if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                        {
                                            bool slotMapValidation = true;//TBD
                                            if (slotMapValidation)
                                            {
                                                bool rmsCheck = portinfo.PortType != "PL" ? true : RecipeCheckRequest(lotInfoRequestReply.UNITRECIPELIST, oEQP, portinfo, transactionID, false);
                                                if (rmsCheck)
                                                {
                                                    //OK
                                                    checkok = true;
                                                }
                                                //else
                                                //{
                                                //    //NG
                                                //}
                                            }
                                            else
                                            {
                                                //NG
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //NG
                                    }
                                }
                            }

                            if (checkok)
                                eqpService.SendPortBoxInfoRequestReply(oEQP.UnitName, portNo, "1", transactionID);
                            else
                                eqpService.SendPortBoxInfoRequestReply(oEQP.UnitName, portNo, "2", transactionID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortBoxInfoRequest:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortBoxGroupPortStatusReport(Unit oEQP, int portNo, int PortStatus, string PortType, int PortCassetteType, string PortTransferMode, List<string> BoxList, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, PortStatus, PortType, PortCassetteType, PortTransferMode, BoxList, transactionID }));
                #endregion

                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portinfo != null)
                {
                    //发送MES
                    RVPortState mesportstate = new RVPortState();
                    mesportstate.EQUIPMENTID = oEQP.EQPID;
                    mesportstate.PORTLIST.Add(new RVPortStateInfo() { PORTID = portinfo.PortID, PORTNUM = portinfo.PortNo.ToString(), PORTSTATE = PortStatus == 5 ? "DOWN" : "UP", PORTTYPE = portinfo.PortType, DURABLEID = BoxList.Count > 0 ? BoxList[0] : (!String.IsNullOrEmpty(portinfo.CassetteID) ? portinfo.CassetteID : "") });
                    mesService.SendToMESEquipmentPortReport(oEQP.EQPID, mesportstate, transactionID);

                    //更新缓存
                    portinfo.PortStatus = PortStatus;
                    portinfo.PortType = HostInfo.Current.GetEQToBCValue(MESEventItem.PortType, PortType);
                    portinfo.PortCSTType = PortCassetteType;
                    portinfo.TransferMode = PortTransferMode;
                    switch (PortStatus)
                    {
                        case (int)EnumPortStatus.Empty:
                            {
                                if (BoxList.Count > 0)
                                {
                                    string BoxID = BoxList[0];

                                    //获取box list
                                    if (HostInfo.Current.BoxListCache.ContainsKey(BoxID))
                                    {
                                        var boxlist = HostInfo.Current.BoxListCache[BoxID];
                                        List<DURABLE> boxdata = new List<DURABLE>();
                                        for (int i = 0; i < boxlist.Count; i++)
                                        {
                                            boxdata.Add(new DURABLE() { SEQNO = boxlist[i].SEQNO, DURABLEID = boxlist[i].DURABLEID });
                                        }

                                        RVBatchUnloadComplete unloadComplete = new RVBatchUnloadComplete();
                                        unloadComplete.EQUIPMENTID = oEQP.EQPID;
                                        unloadComplete.PORTID = portinfo.PortID;
                                        unloadComplete.PORTTYPE = portinfo.PortType;
                                        unloadComplete.PORTNUM = portNo.ToString();
                                        unloadComplete.DURABLELIST = boxdata;

                                        mesService.SendToMESBatchUnloadComplete(oEQP.EQPID, unloadComplete, transactionID);
                                    }
                                }
                                else
                                {
                                    LogHelper.BCLog.Debug($"PortBoxGroupPortStatusReport PortStatus.Empty BoxList is null");
                                }
                            }
                            break;
                        case (int)EnumPortStatus.LoadReady:
                            {
                                RVBatchLoadRequest loadRequest = new RVBatchLoadRequest();
                                loadRequest.EQUIPMENTID = oEQP.EQPID;
                                loadRequest.PORTNUM = portNo.ToString();
                                loadRequest.PORTID = portinfo.PortID;
                                loadRequest.PORTTYPE = portinfo.PortType;
                                //if(Unpacking loader  ="FabPalletLot")
                                //DOM OLB ASY  
                                //TBD待MES定义传的值
                                loadRequest.PORTUSETYPE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortCassetteType, portinfo.PortCSTType.ToString());
                                loadRequest.PORTACCESSMODE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTransferMode, PortTransferMode).Replace(" Mode", "");
                                mesService.SendToMESBatchLoadRequest(oEQP.EQPID, loadRequest, transactionID);
                            }
                            break;
                        case (int)EnumPortStatus.InUse:
                            {
                                RVBatchLoadComplete loadComplete = new RVBatchLoadComplete();
                                loadComplete.EQUIPMENTID = oEQP.EQPID;
                                loadComplete.PORTID = portinfo.PortID;
                                loadComplete.PORTTYPE = portinfo.PortType;
                                loadComplete.PORTNUM = portNo.ToString();

                                string FirstBoxID = "";
                                DateTime dtnow = DateTime.Now;
                                List<BoxCache> boxcache = new List<BoxCache>();
                                for (int i = 0; i < BoxList.Count; i++)
                                {
                                    var box = new DURABLE();
                                    box.SEQNO = (i + 1).ToString();
                                    box.DURABLEID = BoxList[i];
                                    loadComplete.DURABLELIST.Add(box);

                                    if (i == 0)//第一个BoxID
                                    {
                                        FirstBoxID = BoxList[i];
                                        #region 清历史缓存
                                        if (HostInfo.Current.BoxListCache.ContainsKey(FirstBoxID))
                                        {
                                            var t = new List<BoxCache>();
                                            HostInfo.Current.BoxListCache.TryRemove(FirstBoxID, out t);
                                        }
                                        //删超过2天的数据
                                        if (HostInfo.Current.BoxListCache.Any(c => c.Value.Any(d => d.dtNow.AddDays(2) <= DateTime.Now)))
                                        {
                                            var needdel = HostInfo.Current.BoxListCache.Where(c => c.Value.Any(d => d.dtNow.AddDays(2) <= DateTime.Now));
                                            foreach (var ndel in needdel)
                                            {
                                                var t = new List<BoxCache>();
                                                HostInfo.Current.BoxListCache.TryRemove(ndel.Key, out t);
                                            }
                                        }
                                        #endregion
                                    }

                                    boxcache.Add(new BoxCache() { SEQNO = box.SEQNO, DURABLEID = box.DURABLEID, dtNow = dtnow });
                                }
                                HostInfo.Current.BoxListCache.TryAdd(FirstBoxID, boxcache);

                                mesService.SendToMESBatchLoadComplete(oEQP.EQPID, loadComplete, transactionID);

                                portinfo.CassetteID = FirstBoxID;
                                portinfo.CassetteInfo.PortID = portinfo.PortID;
                                portinfo.CassetteInfo.PortNo = portinfo.PortNo;
                                portinfo.CassetteInfo.CassetteID = FirstBoxID;
                            }
                            break;
                        case (int)EnumPortStatus.UnloadReady:
                            {
                                if (BoxList.Count > 0)
                                {
                                    string BoxID = BoxList[0];
                                    portinfo.CassetteID = BoxID;
                                    portinfo.CassetteInfo.CassetteID = BoxID;
                                    portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.ProcessCompleted;
                                    //portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;

                                    //获取box list
                                    if (HostInfo.Current.BoxListCache.ContainsKey(BoxID))
                                    {
                                        var boxlist = HostInfo.Current.BoxListCache[BoxID];
                                        List<DURABLE> boxdata = new List<DURABLE>();
                                        for (int i = 0; i < boxlist.Count; i++)
                                        {
                                            boxdata.Add(new DURABLE() { SEQNO = boxlist[i].SEQNO, DURABLEID = boxlist[i].DURABLEID });
                                        }

                                        RVCreateCarrierBatch createCarrierBatch = new RVCreateCarrierBatch();
                                        createCarrierBatch.EQUIPMENTID = oEQP.EQPID;
                                        createCarrierBatch.PORTID = portinfo.PortID;
                                        createCarrierBatch.PORTTYPE = portinfo.PortType;
                                        createCarrierBatch.PORTNUM = portNo.ToString();
                                        createCarrierBatch.BATCHID = BoxID;
                                        createCarrierBatch.DURABLELIST = boxdata;
                                        mesService.SendToMESCreateCarrierBatch(oEQP.EQPID, createCarrierBatch, transactionID);

                                        RVBatchUnloadRequest unloadRequest = new RVBatchUnloadRequest();
                                        unloadRequest.EQUIPMENTID = oEQP.EQPID;
                                        unloadRequest.PORTID = portinfo.PortID;
                                        unloadRequest.PORTTYPE = portinfo.PortType;
                                        unloadRequest.PORTNUM = portNo.ToString();
                                        unloadRequest.PORTACCESSMODE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTransferMode, PortTransferMode).Replace(" Mode", "");
                                        unloadRequest.DURABLELIST = boxdata;

                                        mesService.SendToMESBatchUnloadRequest(oEQP.EQPID, unloadRequest, transactionID);
                                    }
                                }
                                else
                                {
                                    LogHelper.BCLog.Debug($"PortBoxGroupPortStatusReport PortStatus.UnloadReady BoxList is null");
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                    if (portinfo.CassetteInfo.ID > 0 && !String.IsNullOrEmpty(portinfo.CassetteID))
                        dbService.UpdateCassette(portinfo.CassetteInfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortBoxGroupPortStatusReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void PortBoxPortStatusReport(Unit oEQP, int portNo, int PortStatus, string PortType, string BoxID, int lotSequenceNumber, int jobCountInCassette, string jobExistenceSlot, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, PortStatus, PortType, BoxID, lotSequenceNumber, jobCountInCassette, jobExistenceSlot, transactionID }));
                #endregion
                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portinfo != null)
                {
                    //发送MES
                    RVPortState mesportstate = new RVPortState();
                    mesportstate.EQUIPMENTID = oEQP.EQPID;
                    mesportstate.PORTLIST.Add(new RVPortStateInfo() { PORTID = portinfo.PortID, PORTNUM = portinfo.PortNo.ToString(), PORTSTATE = PortStatus == 5 ? "DOWN" : "UP", PORTTYPE = portinfo.PortType, DURABLEID = !String.IsNullOrEmpty(BoxID) ? BoxID : (!String.IsNullOrEmpty(portinfo.CassetteID) ? portinfo.CassetteID : "") });
                    mesService.SendToMESEquipmentPortReport(oEQP.EQPID, mesportstate, transactionID);

                    //更新缓存
                    portinfo.PortStatus = PortStatus;
                    portinfo.PortType = HostInfo.Current.GetEQToBCValue(MESEventItem.PortType, PortType);
                    switch (PortStatus)
                    {
                        case (int)EnumPortStatus.InUse:
                            {
                                if (string.IsNullOrEmpty(BoxID))
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                    portinfo.CassetteCancelText = "EQP report BoxID is empty";
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                    return;
                                }
                                //先清除在in口的cstid
                                //删除wip_cassette
                                Hashtable csttb = new Hashtable();
                                csttb.Add("CassetteID", BoxID);
                                dbService.DeleteCassetteList(csttb);

                                portinfo.CassetteID = BoxID;
                                portinfo.CassetteSequenceNo = lotSequenceNumber;
                                portinfo.CassetteInfo.PortID = portinfo.PortID;
                                portinfo.CassetteInfo.PortNo = portinfo.PortNo;
                                portinfo.CassetteInfo.CassetteID = BoxID;
                                portinfo.WaitingforProcessingTime = DateTime.Now;
                                portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforCassetteData;
                                portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;
                                dbService.InsertCassette(portinfo.CassetteInfo);

                                string[] containEQP = new string[] { "DOM", "OLB" };
                                if (containEQP.Contains(oEQP.EQPID.Substring(2, 3)))//直接下账
                                {
                                    if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                    {
                                        RVMultiBoxLoadComplete loadComplete = new RVMultiBoxLoadComplete();
                                        loadComplete.EQUIPMENTID = oEQP.EQPID;
                                        loadComplete.PORTID = portinfo.PortID;
                                        loadComplete.PORTTYPE = portinfo.PortType;
                                        loadComplete.PORTNUM = portNo.ToString();
                                        loadComplete.DURABLEID = BoxID;
                                        mesService.SendToMESMultiBoxLoadComplete(oEQP.EQPID, loadComplete, transactionID);

                                        portinfo.WaitingforProcessingTime = DateTime.Now;
                                        portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforCassetteData;

                                        //查出glasslist
                                        List<GlassInfo> glas = new List<GlassInfo>();
                                        try
                                        {
                                            if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => c.CassetteID == BoxID)))
                                            {
                                                var inport = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteID == BoxID));
                                                var inglass = inport.GlassInfos.Where(d => d.CassetteID == BoxID).ToList();


                                                glas = inglass;

                                                //把glass数据转到INUSE口
                                                foreach (var ig in inglass)
                                                {
                                                    ig.PortID = portinfo.PortID;
                                                    ig.InPortID = portinfo.PortID;
                                                    ig.CassetteSequenceNo = lotSequenceNumber;

                                                    dbService.UpdateGlassInfo(ig);
                                                }
                                                if (inglass.Count > 0)
                                                {
                                                    portinfo.GlassInfos.AddRange(inglass);
                                                    for (int i = inport.GlassInfos.Count - 1; i >= 0; i--)
                                                    {
                                                        if (inglass.Any(c => c.GlassID == inport.GlassInfos[i].GlassID))
                                                            inport.GlassInfos.Remove(inport.GlassInfos[i]);
                                                    }
                                                }
                                                //Hashtable glassinfolist = new Hashtable();
                                                //glassinfolist.Add("CassetteID", BoxID);
                                                //var panelList = dbService.GetGlassInfoList(glassinfolist).ToList();

                                                //eqpService.SendPortControlCommand(oEQP.UnitName, portNo, "1", transactionID);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            LogHelper.BCLog.ErrorFormat("+++ PortBoxPortStatusReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
                                        }
                                        if (portinfo.PortType == "PU")
                                            portinfo.IsMESCarrierInfoDownload = true;

                                        eqpService.CassetteMapDownloadCommand(oEQP.UnitName, oEQP.LocalNo.ToString(), oEQP.CommandType, glas, portNo.ToString(), portinfo.Capacity, jobExistenceSlot, glas.Count.ToString(), transactionID);
                                    }
                                }
                                else//原逻辑
                                {
                                    #region 清残帐
                                    //ClearPortGlassData(portinfo, "Old Data Clear");
                                    #endregion
                                    portinfo.WaitingforProcessingTime = DateTime.Now;

                                    portinfo.CassetteID = BoxID;
                                    portinfo.CassetteSequenceNo = lotSequenceNumber;
                                    portinfo.CassetteInfo.PortID = portinfo.PortID;
                                    portinfo.CassetteInfo.PortNo = portinfo.PortNo;
                                    portinfo.CassetteInfo.CassetteID = BoxID;
                                    portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforCassetteData;
                                    portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                    portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                    portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                    portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                    portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;
                                                                        
                                    if (IsInLineMode(oEQP.EQPID))
                                    {
                                        //由client端down帐
                                        dbService.InsertCassette(portinfo.CassetteInfo);
                                    }
                                    else
                                    {
                                        RVMultiBoxLoadComplete loadComplete = new RVMultiBoxLoadComplete();
                                        loadComplete.EQUIPMENTID = oEQP.EQPID;
                                        loadComplete.PORTID = portinfo.PortID;
                                        loadComplete.PORTTYPE = portinfo.PortType;
                                        loadComplete.PORTNUM = portNo.ToString();
                                        loadComplete.DURABLEID = BoxID;
                                        mesService.SendToMESMultiBoxLoadComplete(oEQP.EQPID, loadComplete, transactionID);

                                        if (portinfo.PortType != "PU")
                                        {
                                            portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                            portinfo.CassetteCancelText = "PortType is not PU";
                                            eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                            return;
                                        }

                                        RVCarrierInfoDownload lotInfoRequest = new RVCarrierInfoDownload();
                                        lotInfoRequest.EQUIPMENTID = oEQP.EQPID;
                                        lotInfoRequest.PORTID = portinfo.PortID;
                                        lotInfoRequest.PORTTYPE = portinfo.PortType;
                                        lotInfoRequest.DURABLEID = BoxID;

                                        portinfo.IsMESCarrierInfoDownload = true;
                                        var lotInfoRequestReply = mesService.SendToMESCarrierInfoDownload(oEQP.EQPID, lotInfoRequest, transactionID);

                                        if (lotInfoRequestReply != null)
                                        {
                                            if (lotInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                            {
                                                //portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                                //portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                                //portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                                //portinfo.CassetteInfo.CassetteID = lotInfoRequestReply.DURABLEID;
                                                //portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                                portinfo.CassetteInfo.CarrierType = lotInfoRequestReply.DURABLETYPE;
                                                portinfo.CassetteInfo.ProductSpecName = lotInfoRequestReply.PARTNAME;
                                                portinfo.CassetteInfo.ProcessOperationName = lotInfoRequestReply.STEPNAME;
                                                portinfo.CassetteInfo.LotName = lotInfoRequestReply.LOTID;
                                                portinfo.CassetteInfo.LotType = lotInfoRequestReply.LOTTYPE;
                                                portinfo.CassetteInfo.MachineRecipeName = lotInfoRequestReply.RECIPENAME;
                                                portinfo.CassetteInfo.LotGrade = lotInfoRequestReply.GRADE;
                                                portinfo.CassetteInfo.ProductQuantity = HostInfo.FormatStringToInt(lotInfoRequestReply.MAINQTY);
                                                portinfo.CassetteInfo.UNITRECIPELIST = lotInfoRequestReply.UNITRECIPELIST;
                                                dbService.InsertCassette(portinfo.CassetteInfo);

                                                //根据portid获取modelposition  robot put时用于寻找target position
                                                var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault(c => c.PortID == portinfo.PortID);

                                                var glassInfoList = new List<GlassInfo>();
                                                var glassInfoListMES = lotInfoRequestReply.PANELLIST;
                                                foreach (var glassMES in glassInfoListMES)
                                                {
                                                    var glass = new GlassInfo();
                                                    glass.FunctionName = "MES Download Data";
                                                    glass.PortID = portinfo.PortID;
                                                    glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                                                    glass.CassetteID = lotInfoRequestReply.DURABLEID;
                                                    glass.CassetteSequenceNo = lotSequenceNumber;
                                                    glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                                    glass.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                                    glass.WorkOrder = glassMES.WOID;
                                                    glass.LotID = lotInfoRequestReply.LOTID;
                                                    glass.ProductID = portinfo.CassetteInfo.ProductSpecName;
                                                    glass.OperationID = portinfo.CassetteInfo.ProcessOperationName;
                                                    glass.GlassID = glassMES.PANELID;
                                                    glass.GlassGradeCode = glassMES.GRADE;
                                                    glass.PanelGrade = glassMES.SUBGRADE;
                                                    int mesPosition = Convert.ToInt32(glassMES.POSITION);
                                                    int slotPosition = mesPosition / 1000;//前后片
                                                    int slotNo = mesPosition % 1000;
                                                    glass.Position = slotNo;
                                                    glass.SlotPosition = slotPosition;
                                                    glass.SlotSequenceNo = mesPosition;
                                                    glass.BLID = glassMES.BONDINGID;
                                                    glass.AbnormalCodes = glassMES.ABNORMALCODE;
                                                    SetJobPPID(ref glass, lotInfoRequestReply.UNITRECIPELIST);
                                                    glassInfoList.Add(glass);
                                                    var ret = dbService.InsertGlassInfo(glass);
                                                }
                                                if (glassInfoList.Count > 0)
                                                    portinfo.GlassInfos.AddRange(glassInfoList);

                                                if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                                {
                                                    ExcuteDownloadJobFlow(oEQP, lotInfoRequestReply.UNITRECIPELIST, portinfo, glassInfoList, jobExistenceSlot, transactionID);
                                                }
                                            }
                                            else
                                            {
                                                //Carrier Cancel Command
                                                portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                                portinfo.CassetteCancelText = lotInfoRequestReply.replyHeader.RESULTMESSAGE; 
                                                eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                    if (portinfo.CassetteInfo.ID > 0 && !String.IsNullOrEmpty(portinfo.CassetteID))
                        dbService.UpdateCassette(portinfo.CassetteInfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortBoxPortStatusReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        #endregion

        #region Yuan
        public void PortStatusChangeReport(Unit oEQP, int portNo, int portStatus, int lotSequenceNumber, string cassetteIDBoxID, int jobCountInCassette, string operatorID, string jobExistenceSlot, int loadingCassetteType, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, portStatus, lotSequenceNumber, cassetteIDBoxID, jobCountInCassette, operatorID, jobExistenceSlot, loadingCassetteType, transactionID }));
                #endregion

                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portinfo != null)
                {
                    //发送MES
                    RVPortState mesportstate = new RVPortState();
                    mesportstate.EQUIPMENTID = oEQP.EQPID;
                    mesportstate.PORTLIST.Add(new RVPortStateInfo() { PORTID = portinfo.PortID, PORTNUM = portinfo.PortNo.ToString(), PORTSTATE = portStatus == 5 ? "DOWN" : "UP", PORTTYPE = portinfo.PortType, DURABLEID = !String.IsNullOrEmpty(cassetteIDBoxID) ? cassetteIDBoxID : (!String.IsNullOrEmpty(portinfo.CassetteID) ? portinfo.CassetteID : "") });
                    mesService.SendToMESEquipmentPortReport(oEQP.EQPID, mesportstate, transactionID);

                    //更新缓存
                    portinfo.PortStatus = portStatus;
                    switch (portStatus)
                    {
                        case (int)EnumPortStatus.Empty:
                            {
                                if (portinfo.PortType == "PU")
                                {
                                    portinfo.PortGrade = "";//清空等级 按等级放片需求
                                }
                                RVUnloadComplete unloadComplete = new RVUnloadComplete();
                                unloadComplete.EQUIPMENTID = oEQP.EQPID;
                                unloadComplete.PORTID = portinfo.PortID;
                                unloadComplete.PORTTYPE = portinfo.PortType;
                                unloadComplete.DURABLEID = cassetteIDBoxID;

                                mesService.SendToMESUnloadComplete(oEQP.EQPID, unloadComplete, transactionID);
                            }
                            break;
                        case (int)EnumPortStatus.LoadReady:
                            {
                                RVLoadRequest loadRequest = new RVLoadRequest();
                                loadRequest.EQUIPMENTID = oEQP.EQPID;
                                loadRequest.PORTID = portinfo.PortID;
                                loadRequest.PORTNUM = portinfo.PortNo.ToString();
                                loadRequest.PORTTYPE = portinfo.PortType;
                                loadRequest.PORTUSETYPE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortCassetteType, portinfo.PortCSTType.ToString());
                                loadRequest.PORTACCESSMODE = HostInfo.Current.GetEQToBCValue(MESEventItem.PortTransferMode, portinfo.TransferMode).Replace(" Mode", "");
                                loadRequest.DISPATCHTYPE = portinfo.PortType == "PL" ? "LOT" : "CARRIER";
                                mesService.SendToMESLoadRequest(oEQP.EQPID, loadRequest, transactionID);
                            }
                            break;
                        case (int)EnumPortStatus.InUse:
                            {
                                portinfo.CassetteCancelText = "";
                                if (string.IsNullOrEmpty(cassetteIDBoxID))
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                    portinfo.CassetteCancelText = "EQP report cassetteIDBoxID is empty";
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                    return;
                                }
                                #region 清残帐
                                ClearPortGlassData(portinfo, "ClearCacheData", 2);
                                #endregion

                                portinfo.WaitingforProcessingTime = DateTime.Now;

                                portinfo.CassetteID = cassetteIDBoxID;
                                portinfo.CassetteSequenceNo = lotSequenceNumber;
                                portinfo.CassetteInfo.PortID = portinfo.PortID;
                                portinfo.CassetteInfo.PortNo = portinfo.PortNo;
                                portinfo.CassetteInfo.CassetteID = cassetteIDBoxID;
                                portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforCassetteData;
                                portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;

                                if (!IsInLineMode(oEQP.EQPID))
                                {
                                    RVLoadComplete loadComplete = new RVLoadComplete();
                                    loadComplete.EQUIPMENTID = oEQP.EQPID;
                                    loadComplete.PORTID = portinfo.PortID;
                                    loadComplete.PORTTYPE = portinfo.PortType;
                                    loadComplete.PORTNUM = portNo.ToString();
                                    loadComplete.DURABLEID = cassetteIDBoxID;
                                    mesService.SendToMESLoadComplete(oEQP.EQPID, loadComplete, transactionID);
                                }
                                //PU口上非空卡 直接Cancel 德钰20220727
                                if (portinfo.PortType == "PU" && jobCountInCassette > 0)
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                    portinfo.CassetteCancelText = "PortType is PU but job exist in Cassette";
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                    return;
                                }
                                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == oEQP.EQPID);
                                if (IsInLineMode(oEQP.EQPID))
                                {
                                    //由client端down帐
                                    dbService.InsertCassette(portinfo.CassetteInfo);
                                }
                                else
                                {
                                    if (oEQP.EQPID.Contains("CUT") && portinfo.PortType != "PU")
                                    {
                                        StartT9TimeOut(oEQP, portinfo, transactionID);
                                        RVCuttingBoxInfoRequest cuttingBoxInfoRequest = new RVCuttingBoxInfoRequest();
                                        cuttingBoxInfoRequest.EQUIPMENTID = oEQP.EQPID;
                                        cuttingBoxInfoRequest.BOXID = cassetteIDBoxID;

                                        portinfo.IsMESCarrierInfoDownload = false;
                                        var cuttingBoxInfoRequestReply = mesService.SendToMESCuttingBoxInfoRequest(oEQP.EQPID, cuttingBoxInfoRequest, transactionID);

                                        if (cuttingBoxInfoRequestReply != null)
                                        {
                                            if (cuttingBoxInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                            {
                                                //portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                                //portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                                //portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                                //portinfo.CassetteInfo.CassetteID = cuttingBoxInfoRequestReply.BOXID;
                                                //portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                                //portinfo.CassetteInfo.CarrierType = lotInfoRequestReply.DURABLETYPE;
                                                portinfo.CassetteInfo.ProductSpecName = cuttingBoxInfoRequestReply.PARTNAME;
                                                portinfo.CassetteInfo.ProcessOperationName = cuttingBoxInfoRequestReply.STEPNAME;
                                                portinfo.CassetteInfo.LotName = cuttingBoxInfoRequestReply.LOTID;
                                                portinfo.CassetteInfo.LotType = cuttingBoxInfoRequestReply.LOTTYPE;
                                                portinfo.CassetteInfo.MachineRecipeName = cuttingBoxInfoRequestReply.RECIPENAME;
                                                portinfo.CassetteInfo.LotGrade = cuttingBoxInfoRequestReply.GRADE;
                                                portinfo.CassetteInfo.ProductQuantity = HostInfo.FormatStringToInt(cuttingBoxInfoRequestReply.QTY);
                                                portinfo.CassetteInfo.UNITRECIPELIST = cuttingBoxInfoRequestReply.UNITRECIPELIST;
                                                dbService.InsertCassette(portinfo.CassetteInfo);

                                                //根据portid获取modelposition  robot put时用于寻找target position
                                                var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault(c => c.PortID == portinfo.PortID);

                                                var glassInfoList = new List<GlassInfo>();
                                                var glassInfoListMES = cuttingBoxInfoRequestReply.GLASSLIST;
                                                foreach (var glassMES in glassInfoListMES)
                                                {
                                                    var glass = new GlassInfo();
                                                    glass.FunctionName = "MES Download Data";
                                                    glass.PortID = portinfo.PortID;
                                                    glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                                                    glass.CassetteID = cuttingBoxInfoRequestReply.BOXID;
                                                    glass.CassetteSequenceNo = lotSequenceNumber;
                                                    glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                                    glass.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                                    glass.ProductID = portinfo.CassetteInfo.ProductSpecName;
                                                    glass.OperationID = portinfo.CassetteInfo.ProcessOperationName;
                                                    glass.GlassID = glassMES.GLASSID;
                                                    glass.GlassGradeCode = glassMES.GRADE;
                                                    glass.PanelGrade = glassMES.SUBGRADE;
                                                    glass.WorkOrder = glassMES.WOID;
                                                    glass.LotID = cuttingBoxInfoRequestReply.LOTID;
                                                    int mesPosition = Convert.ToInt32(glassMES.POSITION);
                                                    //int slotPosition = mesPosition / 1000;//前后片
                                                    //int slotNo = mesPosition % 1000;
                                                    //glass.Position = slotNo;
                                                    //glass.SlotPosition = slotPosition;
                                                    glass.SlotSequenceNo = mesPosition;
                                                    glass.Position = mesPosition;
                                                    SetJobPPID(ref glass, cuttingBoxInfoRequestReply.UNITRECIPELIST);
                                                    glassMES.SPECIALCODELIST.ForEach(o => { glass.AbnormalCodes += (!String.IsNullOrEmpty(o.PANELID) ? (o.PANELID + "|") : "") + (o.CODEID + ";"); });
                                                    if (!string.IsNullOrEmpty(glass.AbnormalCodes))
                                                    {
                                                        glass.AbnormalCodes = glass.AbnormalCodes.Substring(0, glass.AbnormalCodes.Length - 1);
                                                    }
                                                    glassInfoList.Add(glass);
                                                    var ret = dbService.InsertGlassInfo(glass);
                                                }
                                                if (glassInfoList.Count > 0)
                                                    portinfo.GlassInfos.AddRange(glassInfoList);

                                                if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                                {
                                                    ExcuteDownloadJobFlow(oEQP, cuttingBoxInfoRequestReply.UNITRECIPELIST, portinfo, glassInfoList, jobExistenceSlot, transactionID);
                                                }
                                            }
                                            else
                                            {
                                                //Carrier Cancel Command
                                                portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                                portinfo.CassetteCancelText = cuttingBoxInfoRequestReply.replyHeader.RESULTMESSAGE;
                                                eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        StartT9TimeOut(oEQP, portinfo, transactionID);

                                        if (portinfo.PortType == "PL")
                                        {
                                            RVLotInfoRequest lotInfoRequest = new RVLotInfoRequest();
                                            lotInfoRequest.EQUIPMENTID = oEQP.EQPID;
                                            lotInfoRequest.PORTID = portinfo.PortID;
                                            lotInfoRequest.PORTTYPE = portinfo.PortType;
                                            lotInfoRequest.DURABLEID = cassetteIDBoxID;

                                            portinfo.IsMESCarrierInfoDownload = false;
                                            var lotInfoRequestReply = mesService.SendToMESLotInfoRequest(oEQP.EQPID, lotInfoRequest, transactionID);

                                            if (lotInfoRequestReply != null)
                                            {
                                                if (lotInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                                {
                                                    //portinfo.CassetteInfo.EQPID = oEQP.EQPID;
                                                    //portinfo.CassetteInfo.UnitID = oEQP.UnitID;
                                                    //portinfo.CassetteInfo.UnitName = oEQP.UnitName;
                                                    //portinfo.CassetteInfo.CassetteID = lotInfoRequestReply.DURABLEID;
                                                    //portinfo.CassetteInfo.CassetteSequenceNo = lotSequenceNumber;
                                                    portinfo.CassetteInfo.CarrierType = lotInfoRequestReply.DURABLETYPE;
                                                    portinfo.CassetteInfo.ProductSpecName = lotInfoRequestReply.PARTNAME;
                                                    portinfo.CassetteInfo.ProcessOperationName = lotInfoRequestReply.STEPNAME;
                                                    portinfo.CassetteInfo.LotName = lotInfoRequestReply.LOTID;
                                                    portinfo.CassetteInfo.LotType = lotInfoRequestReply.LOTTYPE;
                                                    portinfo.CassetteInfo.MachineRecipeName = lotInfoRequestReply.RECIPENAME;
                                                    portinfo.CassetteInfo.LotGrade = lotInfoRequestReply.GRADE;
                                                    portinfo.CassetteInfo.ProductQuantity = HostInfo.FormatStringToInt(lotInfoRequestReply.MAINQTY);
                                                    portinfo.CassetteInfo.JobCount = HostInfo.FormatStringToInt(lotInfoRequestReply.MAINQTY);
                                                    portinfo.CassetteInfo.UNITRECIPELIST = lotInfoRequestReply.UNITRECIPELIST;

                                                    dbService.InsertCassette(portinfo.CassetteInfo);

                                                    //根据portid获取modelposition  robot put时用于寻找target position
                                                    var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault(c => c.PortID == portinfo.PortID);

                                                    var glassInfoList = new List<GlassInfo>();
                                                    var glassInfoListMES = lotInfoRequestReply.PANELLIST;
                                                    foreach (var glassMES in glassInfoListMES)
                                                    {
                                                        var glass = new GlassInfo();
                                                        glass.FunctionName = "MES Download Data";
                                                        glass.PortID = portinfo.PortID;
                                                        glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                                                        glass.CassetteID = lotInfoRequestReply.DURABLEID;
                                                        glass.CassetteSequenceNo = lotSequenceNumber;
                                                        glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                                        glass.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                                        glass.WorkOrder = glassMES.WOID;
                                                        glass.LotID = lotInfoRequestReply.LOTID;
                                                        glass.ProductID = portinfo.CassetteInfo.ProductSpecName;
                                                        glass.OperationID = portinfo.CassetteInfo.ProcessOperationName;
                                                        glass.GlassID = glassMES.PANELID;
                                                        glass.GlassGradeCode = glassMES.GRADE;
                                                        glass.PanelGrade = glassMES.SUBGRADE;
                                                        int mesPosition = Convert.ToInt32(glassMES.POSITION);
                                                        int slotPosition = mesPosition / 1000;//前后片
                                                        int slotNo = mesPosition % 1000;
                                                        glass.Position = slotNo;
                                                        glass.SlotPosition = slotPosition;
                                                        glass.SlotSequenceNo = mesPosition;
                                                        glass.BLID = glassMES.BONDINGID;
                                                        glass.AbnormalCodes = glassMES.ABNORMALCODE;
                                                        SetJobPPID(ref glass, lotInfoRequestReply.UNITRECIPELIST);
                                                        glassInfoList.Add(glass);
                                                        var ret = dbService.InsertGlassInfo(glass);
                                                    }
                                                    if (glassInfoList.Count > 0)
                                                        portinfo.GlassInfos.AddRange(glassInfoList);

                                                    if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                                    {
                                                        ExcuteDownloadJobFlow(oEQP, lotInfoRequestReply.UNITRECIPELIST, portinfo, glassInfoList, jobExistenceSlot, transactionID, true, true);
                                                    }
                                                }
                                                else
                                                {
                                                    //Carrier Cancel Command
                                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                                    portinfo.CassetteCancelText = lotInfoRequestReply.replyHeader.RESULTMESSAGE;
                                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                                }
                                            }
                                        }
                                        else if (portinfo.PortType == "PU")
                                        {
                                            RVCarrierInfoDownload lotInfoRequest = new RVCarrierInfoDownload();
                                            lotInfoRequest.EQUIPMENTID = oEQP.EQPID;
                                            lotInfoRequest.PORTID = portinfo.PortID;
                                            lotInfoRequest.PORTTYPE = portinfo.PortType;
                                            lotInfoRequest.DURABLEID = cassetteIDBoxID;

                                            portinfo.IsMESCarrierInfoDownload = true;
                                            var lotInfoRequestReply = mesService.SendToMESCarrierInfoDownload(oEQP.EQPID, lotInfoRequest, transactionID);

                                            if (lotInfoRequestReply != null)
                                            {
                                                if (lotInfoRequestReply.replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                                {
                                                    portinfo.CassetteInfo.CarrierType = lotInfoRequestReply.DURABLETYPE;
                                                    portinfo.CassetteInfo.ProductSpecName = lotInfoRequestReply.PARTNAME;
                                                    portinfo.CassetteInfo.ProcessOperationName = lotInfoRequestReply.STEPNAME;
                                                    portinfo.CassetteInfo.LotName = lotInfoRequestReply.LOTID;
                                                    portinfo.CassetteInfo.LotType = lotInfoRequestReply.LOTTYPE;
                                                    portinfo.CassetteInfo.MachineRecipeName = lotInfoRequestReply.RECIPENAME;
                                                    portinfo.CassetteInfo.LotGrade = lotInfoRequestReply.GRADE;
                                                    portinfo.CassetteInfo.ProductQuantity = HostInfo.FormatStringToInt(lotInfoRequestReply.MAINQTY);
                                                    portinfo.CassetteInfo.UNITRECIPELIST = lotInfoRequestReply.UNITRECIPELIST;
                                                    dbService.InsertCassette(portinfo.CassetteInfo);

                                                    //根据portid获取modelposition  robot put时用于寻找target position
                                                    var robotmodel = oEQP.RobotModelList == null ? null : oEQP.RobotModelList.FirstOrDefault(c => c.PortID == portinfo.PortID);

                                                    var glassInfoList = new List<GlassInfo>();
                                                    var glassInfoListMES = lotInfoRequestReply.PANELLIST;
                                                    foreach (var glassMES in glassInfoListMES)
                                                    {
                                                        var glass = new GlassInfo();
                                                        glass.FunctionName = "MES Download Data";
                                                        glass.PortID = portinfo.PortID;
                                                        glass.CurrentSUnit = oEQP.UnitID + "-" + portinfo.PortID;
                                                        glass.CassetteID = lotInfoRequestReply.DURABLEID;
                                                        glass.CassetteSequenceNo = lotSequenceNumber;
                                                        glass.ModePath = HostInfo.Current.EQPInfo != null ? HostInfo.Current.EQPInfo.LineMode.ToString() : "";
                                                        glass.ModelPosition = robotmodel != null ? robotmodel.ModelPosition : 0;
                                                        glass.WorkOrder = glassMES.WOID;
                                                        glass.LotID = lotInfoRequestReply.LOTID;
                                                        glass.ProductID = portinfo.CassetteInfo.ProductSpecName;
                                                        glass.OperationID = portinfo.CassetteInfo.ProcessOperationName;
                                                        glass.GlassID = glassMES.PANELID;
                                                        glass.GlassGradeCode = glassMES.GRADE;
                                                        glass.PanelGrade = glassMES.SUBGRADE;
                                                        int mesPosition = Convert.ToInt32(glassMES.POSITION);
                                                        int slotPosition = mesPosition / 1000;//前后片
                                                        int slotNo = mesPosition % 1000;
                                                        glass.Position = slotNo;
                                                        glass.SlotPosition = slotPosition;
                                                        glass.SlotSequenceNo = mesPosition;
                                                        glass.BLID = glassMES.BONDINGID;
                                                        glass.AbnormalCodes = glassMES.ABNORMALCODE;
                                                        SetJobPPID(ref glass, lotInfoRequestReply.UNITRECIPELIST);
                                                        glassInfoList.Add(glass);
                                                        var ret = dbService.InsertGlassInfo(glass);
                                                    }
                                                    if (glassInfoList.Count > 0)
                                                        portinfo.GlassInfos.AddRange(glassInfoList);

                                                    if (eqpinfo.ControlState == EnumControlState.OnLineRemote)
                                                    {
                                                        ExcuteDownloadJobFlow(oEQP, lotInfoRequestReply.UNITRECIPELIST, portinfo, glassInfoList, jobExistenceSlot, transactionID);
                                                    }
                                                }
                                                else
                                                {
                                                    //Carrier Cancel Command
                                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                                    portinfo.CassetteCancelText = lotInfoRequestReply.replyHeader.RESULTMESSAGE;
                                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case (int)EnumPortStatus.UnloadReady:
                            {
                                StopT9TimeOut(oEQP.UnitID, portinfo.PortID, "UnloadReady");
                                portinfo.CassetteID = cassetteIDBoxID;
                                portinfo.CassetteSequenceNo = lotSequenceNumber;
                                portinfo.CassetteInfo.CassetteID = cassetteIDBoxID;
                                portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.ProcessCompleted;
                                portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;

                                RVUnloadRequest unloadRequest = new RVUnloadRequest();
                                unloadRequest.EQUIPMENTID = oEQP.EQPID;
                                unloadRequest.PORTID = portinfo.PortID;
                                unloadRequest.PORTTYPE = portinfo.PortType;
                                unloadRequest.DURABLEID = cassetteIDBoxID;

                                mesService.SendToMESUnloadRequest(oEQP.EQPID, unloadRequest, transactionID);
                            }
                            break;
                        case (int)EnumPortStatus.Blocked:
                            {
                                //portinfo.CassetteID = cassetteIDBoxID;
                                //portinfo.CassetteSequenceNo = lotSequenceNumber;
                                //portinfo.CassetteInfo.CassetteID = cassetteIDBoxID;
                                //portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforCassetteData;
                                //portinfo.CassetteInfo.JobExistenceSlot = jobExistenceSlot;
                            }
                            break;
                        default:
                            break;
                    }
                    //更新数据库
                    dbService.UpdatePortInfo(portinfo);
                    if (portinfo.CassetteInfo.ID > 0 && !String.IsNullOrEmpty(portinfo.CassetteID))
                        dbService.UpdateCassette(portinfo.CassetteInfo);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ PortStatusChangeReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public bool RecipeCheckRequest(List<UNITRECIPE> unitRecipeList, Unit oEQP, PortInfo portinfo, string transactionID, bool needcancel)
        {
            string CancelReason = "";
            if (unitRecipeList.Count == 0 || !unitRecipeList.Any(c => c.CHECKFLAG == "Y"))
            {
                return true;
            }
            var newunitRecipeList = unitRecipeList.Where(c => c.CHECKFLAG == "Y").ToList();

            var cassette = portinfo.CassetteInfo;
            #region currentRecipeName校验
            foreach (var unitRecipe in newunitRecipeList)
            {
                var lineInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == unitRecipe.UNITID.Split('-')[0]);
                var unitID = unitRecipe.UNITID;
                var unitRecipeName = unitRecipe.UNITRECIPENAME;
                var unit = lineInfo.Units.FirstOrDefault(o => o.UnitID == unitID);
                if (unit == null)
                {
                    LogHelper.BCLog.InfoFormat("+++ PortStatusChangeReport:{0} MES reply UnitID:{1} Error +++", oEQP.UnitName, unitID);
                    if (needcancel)
                    {
                        CancelReason = $"MES reply UnitID:{unitID} is not match in bc";
                        goto Res;
                    }
                    return false;
                }
                if (unit.CurrentRecipeIdCheck)//是否校验currentRecipe，不校验，直接跳过
                {
                    if (unit.CurrentRecipeID.ToString() != unitRecipeName)
                    {
                        LogHelper.BCLog.InfoFormat("+++ PortStatusChangeReport:{0} Current Recipe Mismatch,EQP:{1},MES:{2} +++", unit.UnitName, unit.CurrentRecipeID, unitRecipeName);
                        if (needcancel)
                        {
                            CancelReason = $"Current Recipe Mismatch,Unit:{unit.UnitName}  EQP:{unit.CurrentRecipeID},MES:{unitRecipeName}";
                            goto Res;
                        }
                        return false;
                    }
                }
            }
            #endregion
            //current Recipe No 校验完成，下一步校验recipeParameter
            #region recipeParameter 校验
            var resultEQPDic = new Dictionary<string, Unit>();
            foreach (var unitRecipe in newunitRecipeList)
            {
                var lineInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == unitRecipe.UNITID.Split('-')[0]);
                var unitID = unitRecipe.UNITID;
                var unitRecipeName = unitRecipe.UNITRECIPENAME;
                var unit = lineInfo.Units.FirstOrDefault(o => o.UnitID == unitID);
                resultEQPDic.Add(unitID, unit);
                unit.RecipeParamCheckOK = false;
                unit.Recipe = new Recipe();
                unit.Recipe.RecipeNo = unitRecipeName;
                eqpService.SendRecipeParameterRequestCommand(unit.UnitName, unitRecipeName, transactionID);
            }

            int retryCount = 0;
            while (true)
            {
                retryCount++;
                Thread.Sleep(1000);
                var falseEQP = resultEQPDic.Values.FirstOrDefault(o => o.RecipeParamCheckOK == false);
                if (falseEQP != null)
                {
                    LogHelper.BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:{0} EQP Not Reply:{1} Retry Count:{2} +++", falseEQP.UnitName, falseEQP.Recipe.RecipeNo, retryCount);
                    if (retryCount > 10)
                    {
                        LogHelper.BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:Wait TimeOut at last one EQP not Reply:{0} {1} Retry End +++", falseEQP.Recipe.RecipeNo, falseEQP.RecipeParamCheckOK);
                        break;
                    }
                    continue;
                }
                else
                {
                    LogHelper.BCLog.InfoFormat("+++ SendRecipeParameterRequestCommand:ALL EQP already Reply +++");
                    break;
                }
            }
            var falseEQPList = resultEQPDic.Values.Where(o => o.RecipeParamCheckOK == false);
            if (falseEQPList != null)
            {
                if (falseEQPList.Count() > 0)
                {
                    CancelReason = "EQP Not Reply Recipe Check Result,";
                    foreach (var eqp in falseEQPList)
                    {
                        CancelReason += $"Unit:{eqp.UnitName}  Recipe:{eqp.Recipe.RecipeNo},";
                        LogHelper.EIPLog.InfoFormat("+++ SendRecipeParameterRequestCommand:{0} EQP Still Not Reply:{1} Error +++", eqp.UnitName, eqp.Recipe.RecipeNo);
                    }
                    if (needcancel)
                    {
                        goto Res;
                    }
                    return false;
                }
            }
            RVRecipeCheckRequest recipeCheckRequest = new RVRecipeCheckRequest();
            recipeCheckRequest.EQUIPMENTID = oEQP.EQPID;
            recipeCheckRequest.DURABLEID = cassette.CassetteID;
            recipeCheckRequest.LOTID = cassette.LotName;
            recipeCheckRequest.PARTNAME = cassette.ProductSpecName;
            recipeCheckRequest.STEPNAME = cassette.ProcessOperationName;
            recipeCheckRequest.LOGICRECIPE = portinfo.CassetteInfo.MachineRecipeName;
            recipeCheckRequest.PORTID = portinfo.PortID;
            recipeCheckRequest.UNITRECIPELIST = new List<UNITRECIPE>();
            foreach (var unit in resultEQPDic.Values)
            {
                UNITRECIPE unitRecipe = new UNITRECIPE();
                unitRecipe.UNITID = unit.UnitID;
                unitRecipe.RECIPENAME = unit.Recipe.RecipeNo;
                unitRecipe.PARAMALIST = new List<PARAM>();
                unit.ParameterList.ForEach(o =>
                {
                    unitRecipe.PARAMALIST.Add(new PARAM() { NAME = o.ParameterName, VALUE = o.ParameterValue });
                });
                recipeCheckRequest.UNITRECIPELIST.Add(unitRecipe);
            }
            var recipeCheckReply = mesService.SendToMESRecipeCheckRequest(oEQP.EQPID, recipeCheckRequest, transactionID + "1");
            if (recipeCheckReply != null)
            {
                if (recipeCheckReply.RESULT == MESResult.SUCCESS.ToString())
                {

                }
                else
                {
                    LogHelper.BCLog.InfoFormat("+++ RVRecipeCheckRequest:{0} MES Reply NG .Error:{1} +++", oEQP.UnitName + "_" + portinfo.PortID, recipeCheckReply.RESULTMESSAGE);
                    if (needcancel)
                    {
                        CancelReason = $"MES Reply Recipe Check NG,Unit:{oEQP.UnitName + "_" + portinfo.PortID}  Result:{recipeCheckReply.RESULTMESSAGE}";
                        goto Res;
                    }
                    return false;
                }
            }
        #endregion

        Res:

            if (!String.IsNullOrEmpty(CancelReason))
            {
                portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                portinfo.CassetteCancelText = CancelReason;
                eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                return false;
            }

            return true;
        }
        public void CassetteMapDownloadCommandReply(Unit oEQP, int portNo, int returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, returnCode, transactionID }));
                #endregion

                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portinfo != null)
                {
                    if (returnCode == 1)//OK
                    {
                        StopT9TimeOut(oEQP.UnitID, portinfo.PortID, "CassetteMapDownloadCommandReply");
                        portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforStartCommand;
                        //更新DB
                        dbService.UpdateCassette(portinfo.CassetteInfo);

                        if (IsInLineMode(oEQP.EQPID))
                        {
                            //portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessStart;
                            //eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessStart, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                            return;
                        }
                        if (!portinfo.IsMESCarrierInfoDownload)
                        {
                            RVProcessStartRequest processStartRequest = new RVProcessStartRequest();
                            processStartRequest.EQUIPMENTID = oEQP.EQPID;
                            processStartRequest.DURABLEID = portinfo.CassetteInfo.CassetteID;
                            processStartRequest.LOTID = portinfo.CassetteInfo.LotName;
                            processStartRequest.PORTID = portinfo.PortID;
                            var line = HostInfo.Current.AllEQPInfo.FirstOrDefault(o => o.EQPID == oEQP.EQPID);
                            var actionType = line.EQPID.Contains("CUT") ? "CUTTING" : "NORMAL";
                            processStartRequest.ACTIONTYPE = actionType;//TBD
                            var mesReply = mesService.SendToMESProcessStartRequest(oEQP.EQPID, processStartRequest, transactionID);
                            if (mesReply != null)
                            {
                                if (mesReply.RESULT == MESResult.SUCCESS.ToString())
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessStart;
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessStart, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                }
                                else
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                    portinfo.CassetteCancelText = mesReply.RESULTMESSAGE;
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                }
                            }
                        }
                        else
                        {
                            RVCarrierProcessStart processStartRequest = new RVCarrierProcessStart();
                            processStartRequest.EQUIPMENTID = oEQP.EQPID;
                            processStartRequest.DURABLEID = portinfo.CassetteInfo.CassetteID;
                            processStartRequest.PORTTYPE = portinfo.PortType;
                            processStartRequest.PORTNUM = portinfo.PortNo.ToString();
                            processStartRequest.PORTID = portinfo.PortID;
                            var mesReply = mesService.SendToMESCarrierProcessStart(oEQP.EQPID, processStartRequest, transactionID);
                            if (mesReply != null)
                            {
                                if (mesReply.RESULT == MESResult.SUCCESS.ToString())
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessStart;
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessStart, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                }
                                else
                                {
                                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                                    portinfo.CassetteCancelText = mesReply.RESULTMESSAGE; 
                                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                                }
                            }
                        }
                    }
                    else//NG
                    {
                        portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                        portinfo.CassetteCancelText = "EQP CassetteMapDownloadCommand Reply NG";
                        eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CassetteMapDownloadCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CassetteControlCommandReply(Unit oEQP, int portNo, int returnCode, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, returnCode, transactionID }));
                #endregion

                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portinfo != null)
                {
                    if (returnCode == 1)//OK
                    {
                        //不做任何操作，等待设备上报 Process Start
                        if (portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessStart || portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessStartByCount)
                        {
                            portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.WaitingforProcessing;
                        }
                        else if (portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessAbort)
                        {
                            portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.CassetteProcessAbort;
                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")))
                            { }
                            else
                                eqpService.SendPortControlCommand(oEQP.UnitName, portNo, "2", transactionID);
                        }
                        else if (portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessCancel)
                        {
                            portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.CassetteProcessCancel;
                            StopT9TimeOut(oEQP.UnitID, portinfo.PortID, "CassetteProcessCancel");
                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")))
                            { }
                            else
                                eqpService.SendPortControlCommand(oEQP.UnitName, portNo, "2", transactionID);
                        }
                        else if (portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessEnd)
                        {
                            portinfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.ProcessCompleted;
                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")))
                            { }
                            else
                                eqpService.SendPortControlCommand(oEQP.UnitName, portNo, "2", transactionID);
                        }
                        //更新DB
                        dbService.UpdateCassette(portinfo.CassetteInfo);
                    }
                    else//NG
                    {
                        //Cancel Command  
                        if (portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessStart || portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessStartByCount)
                        {
                            portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                            portinfo.CassetteCancelText = "EQP CassetteProcessStart Command Reply NG";
                            eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), transactionID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CassetteControlCommandReply:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CassetteProcessStartReport(Unit oEQP, int portNo, int lotSequenceNumber, string startOption, string cassetteIDBoxID, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, lotSequenceNumber, startOption, cassetteIDBoxID, transactionID }));
                #endregion

                var portInfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portInfo != null)
                {
                    portInfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.InProcessing;
                    //更新DB
                    dbService.UpdateCassette(portInfo.CassetteInfo);

                    if (!portInfo.IsMESCarrierInfoDownload)
                    {
                        RVLotProcessStart lotProcessStart = new RVLotProcessStart();
                        lotProcessStart.EQUIPMENTID = oEQP.EQPID;
                        lotProcessStart.DURABLEID = portInfo.CassetteInfo.CassetteID;
                        lotProcessStart.LOTID = portInfo.CassetteInfo.LotName;
                        lotProcessStart.PORTID = portInfo.PortID;
                        var line = HostInfo.Current.AllEQPInfo.FirstOrDefault(o => o.EQPID == oEQP.EQPID);
                        var actionType = line.EQPID.Contains("CUT") ? "CUTTING" : "NORMAL";
                        lotProcessStart.ACTIONTYPE = actionType;//TBD
                        mesService.SendToMESLotProcessStart(oEQP.EQPID, lotProcessStart, transactionID);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CassetteProcessStartReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void CassetteProcessEndReport(Unit oEQP, int portNo, int lotSequenceNumber, string completeCassetteData, string cassetteIDBoxID, string transactionID = "")
        {
            try
            {
                #region 写日志
                LogHelper.BCLog.Info(WriteLog("EQP=>BC", System.Reflection.MethodBase.GetCurrentMethod().Name,
                    System.Reflection.MethodBase.GetCurrentMethod().GetParameters().Select(c => c.Name).ToArray(),
                    new object[] { oEQP.UnitName, portNo, lotSequenceNumber, completeCassetteData, cassetteIDBoxID, transactionID }));
                #endregion

                var portInfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == oEQP.EQPID && c.UnitID == oEQP.UnitID && c.PortNo == portNo);
                if (portInfo != null)
                {
                    //1:Normal Complete
                    //2:Operator Forced To Cancel
                    //3:Operator Forced To Abort
                    //4:CIM Forced To Cancel
                    //5:CIM Forced To Abort
                    //6:Indexer Forced To Cancel
                    //7:Indexer Forced To Abort

                    //根据completeCassetteData，赋值CassetteStatus
                    portInfo.CassetteInfo.CassetteStatus = EnumCarrierStatus.ProcessCompleted;
                    //更新DB
                    dbService.UpdateCassette(portInfo.CassetteInfo);

                    var lineInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(o => o.EQPID == oEQP.EQPID);
                    var lineType = lineInfo != null ? lineInfo.LineType.ToString() : "";
                    //FTS线,最后一个设备，上报boxpacking
                    if (lineType == EnumLineType.FTS.ToString() && oEQP.LocalNo != 2)
                    {
                        Hashtable glassinfolist = new Hashtable();
                        glassinfolist.Add("PortID", portInfo.PortID);
                        var panelList = dbService.GetGlassInfoList(glassinfolist);
                        if (panelList.Count == 0)
                        {
                            //未查询出panelList，该如何处理，TBD
                            return;
                        }

                        RVBoxPacking boxPacking = new RVBoxPacking();
                        boxPacking.ACTIONTYPE = "BOX";
                        boxPacking.EQUIPMENTID = oEQP.EQPID;
                        boxPacking.DURABLEID = cassetteIDBoxID;
                        boxPacking.UNITID = oEQP.UnitID;
                        boxPacking.PORTID = portInfo.PortID;
                        boxPacking.MAINQTY = panelList.Count.ToString();
                        foreach (var panel in panelList)
                        {
                            PANEL mesPanel = new PANEL();
                            mesPanel.PANELID = panel.GlassID;
                            mesPanel.POSITION = panel.SlotSequenceNo.ToString();
                            mesPanel.ACTIONCOMMENT = null;
                            boxPacking.PANELLIST.Add(mesPanel);
                        }
                        mesService.SendToMESBoxPacking(oEQP.EQPID, boxPacking, transactionID);
                        return;
                    }
                    switch (completeCassetteData)
                    {
                        case "1":
                            {
                                if (portInfo.PortType != "PL")
                                {
                                    Hashtable glassinfolist = new Hashtable();
                                    glassinfolist.Add("CassetteID", cassetteIDBoxID);
                                    var panelList = dbService.GetGlassInfoListByCstID(glassinfolist);
                                    var glassinfo = panelList != null ? panelList.Where(c => c.SlotSatus == EnumGlassSlotStatus.ProcessEnd) : null;
                                    RVLotProcessEnd lotProcessEnd = new RVLotProcessEnd();
                                    lotProcessEnd.EQUIPMENTID = oEQP.EQPID;
                                    lotProcessEnd.PORTID = portInfo.PortID;
                                    lotProcessEnd.PORTTYPE = portInfo.PortType;
                                    lotProcessEnd.PARTNAME = portInfo.CassetteInfo.ProductSpecName;
                                    lotProcessEnd.STEPNAME = portInfo.CassetteInfo.ProcessOperationName;
                                    lotProcessEnd.DURABLEID = portInfo.CassetteInfo.CassetteID;
                                    lotProcessEnd.OPERATOR = null;
                                    lotProcessEnd.ACTIONCOMMENT = null;
                                    lotProcessEnd.PANELLIST = new List<PANEL>();
                                    if (glassinfo != null)
                                    {
                                        foreach (var panel in glassinfo)
                                        {
                                            if (panel.SlotSatus != EnumGlassSlotStatus.ProcessEnd)
                                                continue;
                                            PANEL mesPanel = new PANEL();
                                            mesPanel.PANELID = panel.GlassID;
                                            mesPanel.BONDINGID = panel.BLID;
                                            mesPanel.GRADE = panel.GlassGradeCode;
                                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")) || (oEQP.UnitID.Contains("M2CUT") && oEQP.UnitID.Contains("TST1")))
                                                mesPanel.POSITION = panel.Position.ToString();
                                            else
                                                mesPanel.POSITION = ((panel.SlotPosition * 1000) + panel.Position).ToString();
                                            mesPanel.ACTIONCOMMENT = null;
                                            mesPanel.ABNORMALCODE = panel.AbnormalCodes;
                                            if (!string.IsNullOrEmpty(panel.DefectCodes) && !String.IsNullOrEmpty(panel.DefectCodes.TrimEnd(';')))
                                            {
                                                mesPanel.DEFECTLIST.Add(new RVDEFECTCODE() { DEFECTCODE = panel.DefectCodes.TrimEnd(';'), DEFECTMAIN = "Y" });
                                            }
                                            if (!string.IsNullOrEmpty(panel.ProcessingCount) && !String.IsNullOrEmpty(panel.ProcessingCount.TrimEnd(';')))
                                            {
                                                var processunits = panel.ProcessingCount.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                                                for (int i = 0; i < processunits.Length; i++)
                                                {
                                                    if (HostInfo.Current.AllEQPInfo.Any(c => c.Units.Any(d => d.LocalNo == Convert.ToInt32(processunits[i]))))
                                                    {
                                                        var unitinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.LocalNo == Convert.ToInt32(processunits[i]))).Units.FirstOrDefault(d => d.LocalNo == Convert.ToInt32(processunits[i]));
                                                        mesPanel.PROCESSEDUNITLIST.Add(new PROCESSEDUNIT() { PROCESSEDUNITNAME = unitinfo.UnitID });
                                                    }
                                                }
                                            }
                                            lotProcessEnd.PANELLIST.Add(mesPanel);
                                        }
                                    }
                                    lotProcessEnd.MAINQTY = lotProcessEnd.PANELLIST.Count.ToString();
                                    var replyHeader = mesService.SendToMESLotProcessEnd(oEQP.EQPID, lotProcessEnd, transactionID);
                                    if (replyHeader != null)
                                    {
                                        if (replyHeader.RESULT == MESResult.SUCCESS.ToString())
                                        {
                                            //foreach (var panel in panelList)
                                            //{
                                            //    panel.FunctionName = "CassetteProcessEnd";
                                            //    dbService.UpdateGlassInfo(panel);
                                            //}
                                        }
                                        else//退卡失败，如何处理
                                        {
                                            wip_processend wip = new wip_processend();
                                            wip.equipmentid = lotProcessEnd.EQUIPMENTID;
                                            wip.portid = lotProcessEnd.PORTID;
                                            wip.porttype = lotProcessEnd.PORTTYPE;
                                            wip.partname = lotProcessEnd.PARTNAME;
                                            wip.stepname = lotProcessEnd.STEPNAME;
                                            wip.durableid = lotProcessEnd.DURABLEID;
                                            wip.mainqty = lotProcessEnd.MAINQTY;
                                            wip.operatorid = lotProcessEnd.OPERATOR;
                                            wip.actioncomment = lotProcessEnd.EQUIPMENTID;
                                            wip.returncode = replyHeader.RESULT;
                                            wip.returnmsg = replyHeader.RESULTMESSAGE;
                                            wip.id = dbService.Insertwip_processend(wip);

                                            foreach (var g in glassinfo)
                                            {
                                                wip_processend_glass wipglass = new wip_processend_glass();
                                                wipglass.panelid = g.GlassID;
                                                wipglass.blid = g.BLID;
                                                wipglass.grade = g.GlassGradeCode;
                                                if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")) || (oEQP.UnitID.Contains("M2CUT") && oEQP.UnitID.Contains("TST1")))
                                                    wipglass.position = g.Position.ToString();
                                                else
                                                    wipglass.position = ((g.SlotPosition * 1000) + g.Position).ToString();
                                                wipglass.actioncomment = null;
                                                wipglass.abnormalcode = g.AbnormalCodes;
                                                wipglass.defectcode = g.DefectCodes;
                                                wipglass.parentid = wip.id;
                                                dbService.Insertwip_processend_glass(wipglass);
                                            }
                                        }
                                    }
                                    ClearPortGlassData(portInfo, "Process End", 1);
                                }
                            }
                            break;
                        case "2":
                        case "4": //CIM Forced To Cancel  由下cancel逻辑去处理
                        case "6":
                            {
                                var reasontext = "";
                                if (completeCassetteData == "2")
                                    reasontext = "Operator Forced To Cancel";
                                else if (completeCassetteData == "4")
                                    reasontext = portInfo.CassetteCancelText;
                                else if (completeCassetteData == "6")
                                    reasontext = "Indexer Forced To Cancel";
                                //if (completeCassetteData == "2" || completeCassetteData == "6")
                                //{
                                //Cancel
                                //var reasontext = completeCassetteData == "2" ? "Operator Forced To Cancel" : "Indexer Forced To Cancel";
                                Hashtable glassinfolist = new Hashtable();
                                glassinfolist.Add("CassetteID", cassetteIDBoxID);
                                var panelList = dbService.GetGlassInfoListByCstID(glassinfolist);
                                var glassinfo = panelList != null ? panelList.Where(c => c.SlotSatus == EnumGlassSlotStatus.Wait) : null;
                                RVLotProcessCancel lotProcessCancel = new RVLotProcessCancel();
                                lotProcessCancel.EQUIPMENTID = oEQP.EQPID;
                                lotProcessCancel.MAINQTY = glassinfo != null ? glassinfo.ToList().Count.ToString() : "0";
                                lotProcessCancel.PORTID = portInfo.PortID;
                                lotProcessCancel.LOTID = portInfo.CassetteInfo.LotName;
                                lotProcessCancel.DURABLEID = portInfo.CassetteInfo.CassetteID;
                                lotProcessCancel.REASONCODE = reasontext;
                                lotProcessCancel.STEPNAME = portInfo.CassetteInfo.ProcessOperationName;
                                lotProcessCancel.LOGICRECIPE = portInfo.CassetteInfo.MachineRecipeName;
                                lotProcessCancel.PANELLIST = new List<PANEL>();
                                if (glassinfo != null)
                                {
                                    glassinfo.ToList().ForEach(o =>
                                    {
                                        if (o.SlotSatus == EnumGlassSlotStatus.Wait)
                                        {
                                            PANEL panel = new PANEL();
                                            panel.PANELID = o.GlassID;
                                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")) || (oEQP.UnitID.Contains("M2CUT") && oEQP.UnitID.Contains("TST1")))
                                                panel.POSITION = o.Position.ToString();
                                            else
                                                panel.POSITION = ((o.SlotPosition * 1000) + o.Position).ToString();
                                            panel.PROCESSFLAG = o.SlotSatus == EnumGlassSlotStatus.Wait ? "N" : "Y";
                                            lotProcessCancel.PANELLIST.Add(panel);
                                        }
                                    });
                                }
                                mesService.SendToMESLotProcessCancel(oEQP.EQPID, lotProcessCancel, transactionID);
                                //}
                                ClearPortGlassData(portInfo, "Process Cancel", 2);
                            }
                            break;
                        case "3":
                        case "5":
                        case "7":
                            {
                                Hashtable glassinfolist = new Hashtable();
                                glassinfolist.Add("CassetteID", cassetteIDBoxID);
                                var panelList = dbService.GetGlassInfoListByCstID(glassinfolist);
                                var glassinfo = panelList != null ? panelList.Where(c => c.SlotSatus == EnumGlassSlotStatus.Wait) : null;
                                //Abort
                                var line = HostInfo.Current.AllEQPInfo.FirstOrDefault(o => o.EQPID == oEQP.EQPID);
                                RVLotProcessAbort lotProcessAbort = new RVLotProcessAbort();
                                var actionType = line.EQPID.Contains("CUT") ? "CUTTING" : "GENERATEMAINLOT";
                                lotProcessAbort.ACTIONTYPE = actionType;//TBD GENERATEMAINLOT OR CUTTING OR SUBLOTABORT
                                lotProcessAbort.EQUIPMENTID = oEQP.EQPID;
                                lotProcessAbort.MAINQTY = glassinfo != null ? glassinfo.ToList().Count.ToString() : "0";
                                lotProcessAbort.PORTID = portInfo.PortID;
                                lotProcessAbort.LOTID = portInfo.CassetteInfo.LotName;
                                lotProcessAbort.DURABLEID = portInfo.CassetteInfo.CassetteID;
                                lotProcessAbort.REASONCODE = completeCassetteData;
                                lotProcessAbort.PANELLIST = new List<PANEL>();
                                if (glassinfo != null)
                                {
                                    glassinfo.ToList().ForEach(o =>
                                    {
                                        if (o.SlotSatus == EnumGlassSlotStatus.Wait)
                                        {
                                            PANEL panel = new PANEL();
                                            panel.PANELID = o.GlassID;
                                            if ((oEQP.UnitID.Contains("M2OLB") && oEQP.UnitID.Contains("BUD1")) || (oEQP.UnitID.Contains("M2DOM") && oEQP.UnitID.Contains("DOM1")) || (oEQP.UnitID.Contains("M2CUT") && oEQP.UnitID.Contains("TST1")))
                                                panel.POSITION = o.Position.ToString();
                                            else
                                                panel.POSITION = ((o.SlotPosition * 1000) + o.Position).ToString();
                                            panel.PROCESSFLAG = o.SlotSatus == EnumGlassSlotStatus.Wait ? "N" : "Y";
                                            lotProcessAbort.PANELLIST.Add(panel);
                                        }
                                    });
                                }
                                mesService.SendToMESLotProcessAbort(oEQP.EQPID, lotProcessAbort, transactionID);
                                ClearPortGlassData(portInfo, "Process Abort", 2);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.ErrorFormat("+++ CassetteProcessEndReport:{0} ,Error:{1} +++", oEQP.UnitName, ex.ToString());
            }
        }
        public void ExcuteDownloadJobFlow(Unit oEQP, List<UNITRECIPE> UNITRECIPELIST, PortInfo portinfo, List<GlassInfo> glassInfoList, string jobExistenceSlot, string transactionID = "", bool needcheck = true, bool needcheckslot = false)
        {
            if (needcheck)
            {
                //SlotMap校验成功，进行RMS校验
                bool slotMapValidation = true;//TBD
                //if (needcheckslot)
                //    slotMapValidation = SlotMapCheck(glassInfoList, jobExistenceSlot);
                if (slotMapValidation)
                {
                    bool rmsCheck = portinfo.PortType != "PL" ? true : RecipeCheckRequest(UNITRECIPELIST, oEQP, portinfo, transactionID, true);
                    if (rmsCheck)
                    {
                        foreach (var glass in glassInfoList)
                        {
                            glass.FunctionName = "Cassette Map Download";
                            dbService.UpdateGlassInfo(glass);
                        }
                        //eqpService.SendPortControlCommand(oEQP.UnitName, portNo, "1", transactionID);
                        eqpService.CassetteMapDownloadCommand(oEQP.UnitName, oEQP.LocalNo.ToString(), oEQP.CommandType, glassInfoList, portinfo.PortNo.ToString(), portinfo.Capacity, jobExistenceSlot, glassInfoList.Count.ToString(), transactionID);
                    }
                    //else
                    //{
                    //    //RMS CHECK NG,已经在RecipeCheckRequest function cancel，此处无需再次cancel
                    //}
                }
                else
                {
                    //Carrier Cancel Command
                    portinfo.CassetteControlCommand = EnumCassetteControlCommand.CassetteProcessCancel;
                    portinfo.CassetteCancelText = "BC SlotMap Check NG";
                    eqpService.SendCassetteControlCommand(oEQP.UnitName, portinfo.PortNo.ToString(), EnumCassetteControlCommand.CassetteProcessCancel, portinfo.CassetteInfo.JobExistenceSlot, glassInfoList.Count.ToString(), transactionID);
                }
            }
            else
            {
                eqpService.CassetteMapDownloadCommand(oEQP.UnitName, oEQP.LocalNo.ToString(), oEQP.CommandType, glassInfoList, portinfo.PortNo.ToString(), portinfo.Capacity, jobExistenceSlot, glassInfoList.Count.ToString(), transactionID);
            }
        }
        private bool SlotMapCheck(List<GlassInfo> glassInfoList, string jobExistenceSlot)
        {
            bool check = true;
            List<string> jobExistenceSlotList = new List<string>();

            for (int i = 0; i < 15; i++)
            {
                string slotmap = "";
                for (int j = 0; j <= 15; j++)
                {
                    var lowpoint = (j + i) + (i * 15);
                    slotmap += jobExistenceSlot[lowpoint];
                }
                jobExistenceSlotList.Add(slotmap);
            }

            string revslotmap = "";
            for (int i = 0; i < jobExistenceSlotList.Count; i++)
            {
                revslotmap += Reversal(jobExistenceSlotList[i]);
            }

            String error1 = "";
            for (int i = 0; i < revslotmap.Length; i++)
            {
                if (revslotmap[i] == '1')
                {
                    if (i < 120)
                    {
                        if (!glassInfoList.Any(c => c.Position == (i + 1) && c.SlotPosition == 1))
                        {
                            error1 += $"{1000 + (i + 1)};";
                            check = false;
                        }
                    }
                    else
                    {
                        if (!glassInfoList.Any(c => c.Position == ((i - 120) + 1) && c.SlotPosition == 2))
                        {
                            error1 += $"{2000 + ((i - 120) + 1)};";
                            check = false;
                        }
                    }
                }
            }
            if (!String.IsNullOrEmpty(error1))
            {
                LogHelper.BCLog.Info($"SlotMapCheck NG, eqpslot not found in mesdata Position:{error1}");
            }

            String error2 = "";
            for (int i = 0; i < glassInfoList.Count; i++)
            {
                var slotmapint = ((glassInfoList[i].SlotPosition - 1) * 120) + glassInfoList[i].Position - 1;
                if (revslotmap[slotmapint] != '1')
                {
                    error2 += $"{glassInfoList[i].SlotPosition * 1000 + glassInfoList[i].Position};";
                    check = false;
                }
            }
            if (!String.IsNullOrEmpty(error2))
            {
                LogHelper.BCLog.Info($"SlotMapCheck NG, mesdata not found in eqpslot Position:{error2}");
            }

            return check;
        }
        private string Reversal(string input)
        {
            char[] array = input.ToCharArray();
            IEnumerable<char> cs = array.Reverse<char>();
            char[] array1 = cs.ToArray<char>();
            string result = new string(array1);
            return result;
        }
        #endregion
    }
}
