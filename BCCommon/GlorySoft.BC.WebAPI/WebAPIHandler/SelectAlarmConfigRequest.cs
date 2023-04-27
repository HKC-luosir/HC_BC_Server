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
    public class SelectAlarmConfigRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SelectAlarmConfigResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                object lineId, unitId, alarmId;
                InitData.TryGetValue("lineId", out lineId);
                InitData.TryGetValue("unitId", out unitId);
                InitData.TryGetValue("alarmId", out alarmId);
                Hashtable hashtable = new Hashtable();
                hashtable.Add("EQPID", lineId);
                hashtable.Add("UNITID", unitId);
                hashtable.Add("ALARMID", alarmId);
                var list = dbService.ViewAlarmList(hashtable).ToList();
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
