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
    public class SaveUserGroupHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string suerName, string clientip, GroupAuthority groupAuthority,string type)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = type,
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = suerName
            };
            #endregion
            try
            {
                if (groupAuthority.objectid != "")
                {
                    dbService.DeleteGroupAuthority(groupAuthority.groupId);
                }

                BC_Group bC_Group = new BC_Group()
                {
                    group_id = groupAuthority.groupId,
                    description = groupAuthority.description,
                    create_user = suerName

                };

                if (dbService.InsertUserGroup(bC_Group))
                {
                    for (int i = 0; i < groupAuthority.authorities.Count; i++)
                    {
                        Group_Authority Group_Authority = new Group_Authority()
                        {
                            groupId = groupAuthority.authorities[i].groupId,
                            menuName = groupAuthority.authorities[i].menuName,
                            pageName = groupAuthority.authorities[i].pageName,
                            action = groupAuthority.authorities[i].action
                        };
                        if (dbService.InsertGroupAuthority(Group_Authority))
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
                    }
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
            opiHis.Add("userid", suerName);
            opiHis.Add("operating", "进行了用户组添加操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
