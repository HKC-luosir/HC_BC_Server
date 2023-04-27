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
    public class EQPControlCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, Dictionary<string, object> data)
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

                object UnitName, PortCSTType, IsProcessEndFlag, IsJobDataRequestFlag, CVReportEnableMode, CycleType, CVReportFrequencyMinute, CVReportHour1, CVReportMinute1, CVReportHour2, CVReportMinute2, CVReportHour3, CVReportMinute3, RecipeName;
                data.TryGetValue("UnitName", out UnitName);
                data.TryGetValue("PortCSTType", out PortCSTType);
                data.TryGetValue("IsProcessEndFlag", out IsProcessEndFlag);
                data.TryGetValue("IsJobDataRequestFlag", out IsJobDataRequestFlag);
                data.TryGetValue("CVReportEnableMode", out CVReportEnableMode);
                data.TryGetValue("CycleType", out CycleType);
                data.TryGetValue("CVReportFrequencyMinute", out CVReportFrequencyMinute);
                data.TryGetValue("CVReportHour1", out CVReportHour1);
                data.TryGetValue("CVReportMinute1", out CVReportMinute1);
                data.TryGetValue("CVReportHour2", out CVReportHour2);
                data.TryGetValue("CVReportMinute2", out CVReportMinute2);
                data.TryGetValue("CVReportHour3", out CVReportHour3);
                data.TryGetValue("CVReportMinute3", out CVReportMinute3);
                data.TryGetValue("RecipeName", out RecipeName);

                switch (type)
                {
                    case "DateTimeCommand":
                        eqpService.SendDateTimeSetCommand(UnitName.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "CIMModeChangeCommand":
                        eqpService.SendCIMModeChangeCommand(UnitName.ToString(), PortCSTType.ToString(), HostInfo.Current.GetTransactionID());
                        break;
                    case "IsProcessEndFlagChange":
                        {
                            var EQPInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == UnitName.ToString()));
                            var unit = EQPInfo.Units.FirstOrDefault(c => c.UnitName == UnitName.ToString());
                            unit.IsProcessEnd = Convert.ToBoolean(IsProcessEndFlag);
                            dbService.UpdateUnitInfo(unit);
                        }
                        break;
                    case "IsJobDataRequestFlagChange":
                        {
                            var EQPInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == UnitName.ToString()));
                            var unit = EQPInfo.Units.FirstOrDefault(c => c.UnitName == UnitName.ToString());
                            unit.IsJobDataRequest = Convert.ToBoolean(IsJobDataRequestFlag);
                            dbService.UpdateUnitInfo(unit);
                        }
                        break;
                    case "CVReportTimeChangeCommand":
                        eqpService.SendCVReportTimeChangeCommand(UnitName.ToString(), CVReportEnableMode.ToString(), CycleType.ToString(), (CVReportFrequencyMinute == null ? "" : CVReportFrequencyMinute.ToString()), (CVReportHour1 == null ? "" : CVReportHour1.ToString()), (CVReportMinute1 == null ? "" : CVReportMinute1.ToString()), (CVReportHour2 == null ? "" : CVReportHour2.ToString()), (CVReportMinute2 == null ? "" : CVReportMinute2.ToString()), (CVReportHour3 == null ? "" : CVReportHour3.ToString()), (CVReportMinute3 == null ? "" : CVReportMinute3.ToString()), HostInfo.Current.GetTransactionID());
                        break;
                    case "RecipeParameterRequestCommand":
                        var txtid = HostInfo.Current.GetTransactionID();
                        if (HostInfo.Current.OPICommandTrans.ContainsKey(UnitName + "_RecipeParameterRequestCommand"))
                        {
                            HostInfo.Current.OPICommandTrans[UnitName + "_RecipeParameterRequestCommand"] = txtid;
                        }
                        else
                        {
                            HostInfo.Current.OPICommandTrans.TryAdd(UnitName + "_RecipeParameterRequestCommand", txtid);
                        }
                        eqpService.SendRecipeParameterRequestCommand(UnitName.ToString(), RecipeName.ToString(), txtid);
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
            opiHis.Add("operating", "进行了" + type + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
