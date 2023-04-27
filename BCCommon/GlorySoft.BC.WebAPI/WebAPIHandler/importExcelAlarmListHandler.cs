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
    public class importExcelAlarmListHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, List<AlarmInfo> alarms)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "importExcelAlarmList",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                Hashtable hashtable = new Hashtable();
                var list = dbService.ViewAlarmList(hashtable).ToList();
                //校验数据
                foreach (var alarm in alarms)
                {
                    //excel内数据重复
                    if (alarms.Count(c => c.EQPID == alarm.EQPID && c.UNITID == alarm.UNITID && c.AlarmID == alarm.AlarmID) > 1)
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = String.Format("Operation failed ! AlarmID:{0} Duplicate", alarm.AlarmID),
                            returnMessageCH = String.Format("操作失败！AlarmID:{0} 重复", alarm.AlarmID)
                        };
                        goto Res;
                    }
                    //数据库内数据重复
                    if (list.Any(c => c.EQPID == alarm.EQPID && c.UNITID == alarm.UNITID && c.AlarmID == alarm.AlarmID))
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = String.Format("Operation failed ! AlarmID:{0} Duplicate", alarm.AlarmID),
                            returnMessageCH = String.Format("操作失败！AlarmID:{0} 重复", alarm.AlarmID)
                        };
                        goto Res;
                    }
                }

                foreach (var alarm in alarms)
                {
                    dbService.InsertAlarmInfo(alarm);
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

            Res:
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了" + "导入Excel数据(Alarm)" + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
