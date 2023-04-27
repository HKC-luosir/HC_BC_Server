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
    public class UpdateUserInfo : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(bcUser bcUser, string userName, string clientip, string type)
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
                #region Body
                User user = new User() {
                    Password = bcUser.password,
                    UserID=bcUser.userId,
                    UserName = bcUser.userName,
                    GroupId = bcUser.groupId
                };
                if (dbService.UpdateUser(user))
                {
                    WebSocketMessageStr.body = 1;
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "0",
                        returnMessageEN = "Operation sucessful !",
                        returnMessageCH = "操作成功！"
                    };

                }
                else
                {
                    WebSocketMessageStr.body = 0;
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "1",
                        returnMessageEN = "Operation failed !",
                        returnMessageCH = "操作失败！"
                    };
                }
                
                #endregion
                #region result;
               
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
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了用户信息更新操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }


    }
}
