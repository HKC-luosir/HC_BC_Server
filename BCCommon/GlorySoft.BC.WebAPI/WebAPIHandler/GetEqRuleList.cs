using System;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetEqRuleList : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getEqRuleList",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                WebSocketMessageStr.body = HostInfo.Current.EQRule;

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
