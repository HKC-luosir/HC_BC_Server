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
    public class importExcelUserListHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, List<User> users)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "importExcelUserList",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                //dbService.UpdateUnitInfo(unit);
                if (users != null && users.Count > 0)
                {
                    foreach (var user in users)
                    {
                        user.Creator = userName;
                    }
                }
                var userList = dbService.GetUserList();
                var userGroup = dbService.ViewUserGroupList(new Hashtable());
                //校验数据
                foreach (var user in users)
                {
                    //excel内数据重复
                    if (users.Count(c => c.UserID == user.UserID) > 1)
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = String.Format("Operation failed ! UserID:{0} Duplicate", user.UserID),
                            returnMessageCH = String.Format("操作失败！UserID:{0} 重复", user.UserID)
                        };
                        goto Res;
                    }
                    //数据库内数据重复
                    if (userList.Any(c => c.UserID == user.UserID))
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = String.Format("Operation failed ! UserID:{0} Duplicate", user.UserID),
                            returnMessageCH = String.Format("操作失败！UserID:{0} 重复", user.UserID)
                        };
                        goto Res;
                    }
                    //excel内用户组不存在
                    if (!userGroup.Any(c => c.group_id == user.GroupId))
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = String.Format("Operation failed ! GroupID:{0} Not Exist", user.GroupId),
                            returnMessageCH = String.Format("操作失败！GroupID:{0} 不存在", user.GroupId)
                        };
                        goto Res;
                    }
                    //excel内密码位数不足
                    if (user.Password.Length < 6)
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = String.Format("Operation failed ! UserID:{0} Password length Less than 6", user.UserID),
                            returnMessageCH = String.Format("操作失败！UserID:{0} 密码长度少于6位", user.UserID)
                        };
                        goto Res;
                    }
                }


                foreach (var user in users)
                {
                    user.Creator = userName;
                    dbService.InsertUser(user);
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
            opiHis.Add("operating", "进行了" + "导入Excel数据(用户)" + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
