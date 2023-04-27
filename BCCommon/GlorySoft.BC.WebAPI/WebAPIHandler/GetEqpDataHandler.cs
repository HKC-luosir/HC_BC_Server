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
    public class GetEqpDataHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "eqpdata",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                Hashtable sunitmap = new Hashtable();
                foreach (var item in InitHistory)
                {
                    sunitmap.Add(item.Key, item.Value);
                }
                IList<cfg_eqp> list = dbService.Viewcfg_eqp(sunitmap);

                WebSocketMessageStr.body = list;

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
