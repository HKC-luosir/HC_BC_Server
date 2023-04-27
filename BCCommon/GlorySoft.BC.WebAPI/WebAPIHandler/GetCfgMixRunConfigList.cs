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
    public class GetCfgMixRunConfigList : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string eqpId)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "GetCfgMixRunConfigList",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = "admin"
            };
            #endregion
            try
            {
                #region Body
                Hashtable hashtable = new Hashtable();
                hashtable.Add("eqpid", eqpId);
                var body = dbService.Viewcfg_mixrunconfig(hashtable);
                WebSocketMessageStr.body = body;
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
