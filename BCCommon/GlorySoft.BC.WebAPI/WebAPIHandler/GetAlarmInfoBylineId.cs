using System;
using System.Collections;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetAlarmInfoBylineId : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(wip_alarm wip_alarm)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "GetAlarmInfoBylineIdResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null
                };
                #endregion
                #region Body

              Hashtable hashtable = new Hashtable() {
                {"eqpid",wip_alarm.lineId },
                {"alarmtype","2" }

                };
                var list = dbService.Viewwip_alarm(hashtable).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].createdatetext = list[i].createdate.ToString("yyyy-MM-dd HH:mm:ss");
                }


                WebSocketMessageStr.body = list;
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
