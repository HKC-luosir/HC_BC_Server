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
    public class GetUsergroupListHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(UserInfoRequest userInfoRequest)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getUsergroupList",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = "admin"
            };
            #endregion
            try
            {
                var userList = dbService.ViewUserGroupList(new Hashtable());
                if (userInfoRequest.groupId!="")
                {
                    userList = userList.Where(o => o.group_id == userInfoRequest.groupId).ToList();
                }
                var userListPage = userList.Skip((userInfoRequest.pageNum - 1) * userInfoRequest.pageSize)
.Take(userInfoRequest.pageSize);


                UserInfoReturn userInfoReturn = new UserInfoReturn()
                {
                    total = userList.Count(),
                    rows = userListPage.ToArray(),
                    from = 0,
                    size = userInfoRequest.pageSize,
                    pageNo = userInfoRequest.pageNum,
                    pageSize = userInfoRequest.pageSize,
                    order = "asc"
                };
                WebSocketMessageStr.body = userInfoReturn;

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
