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
   public class GETModePathDis : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string type)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader() 
            {
                messageName = type,
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                WebSocketMessageStr.body = dbService.Viewbc_robot_path_configureDis();

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
