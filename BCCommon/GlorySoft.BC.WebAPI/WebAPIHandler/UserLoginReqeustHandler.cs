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
    public class UserLoginReqeustHandler : AbstractWebAPIMessageHandlercs
    {

        public WebSocketMessage Execute(bcUser data, string userName, string clientip, string type)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();


            try
            {
                var result = new WebSocketResult();
                bool nomatch = true;
                if (HostInfo.Current.AllEQPInfo.Any(c => c.EQPID == data.lineId))
                {
                    nomatch = false;
                }
                if (nomatch)
                {
                    result.returnCode = "1";
                    result.returnMessageEN = "EQID not match!";
                    result.returnMessageCH = "设备编号不匹配！";
                }
                else
                {
                    bcUsers bcUsers = new bcUsers();
                    bcUser logIn = new bcUser();
                    var user = dbService.GetUserList().First(p => p.UserID == data.userId && p.Password == data.password);

                    #region Handler
                    WebSocketMessageStr.header = new WebSocketHeader()
                    {
                        messageName = type,
                        transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        inboxName = null,
                        userName = user.UserName,
                        userId = user.UserID
                    };
                    #endregion


                    if (user != null)
                    {
                        var GroupAuthority = dbService.ViewUserGroupAuthority(user.UserID);
                        logIn.userId = user.UserID;
                        logIn.lineId = data.lineId;
                        logIn.userName = user.UserName;
                        logIn.password = user.Password;
                        logIn.userName = user.UserName;
                        logIn.roleId = user.UserID;
                        logIn.active = "0";
                        logIn.createtime = user.CreateDate.ToString("yyyyMMddHHmmss");
                        logIn.authorities = GroupAuthority;

                        bcUsers.bcUser = logIn;
                        bcUsers.authorities = GroupAuthority;

                        WebSocketMessageStr.body = bcUsers;

                        result.returnCode = "0";
                        result.returnMessageEN = "User operation sucessful!";
                        result.returnMessageCH = "用户操作成功！";
                    }
                    else
                    {
                        result.returnCode = "1";
                        result.returnMessageEN = "Username not exist!";
                        result.returnMessageCH = "用户名不存在！";
                    }
                }
                WebSocketMessageStr.result = result;
            }
            catch (Exception ex)
            {
                Logger.Info(ex);
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "1",
                    returnMessageEN = "Username or Password error!",
                    returnMessageCH = "用户名或密码错误！"
                };
            }
            #region OPI操作记录
            if (data.active.ToUpper() == "LOGIN")
            {
                Hashtable opiHis = new Hashtable();
                opiHis.Add("userid", data.userId);
                opiHis.Add("operating", "进行了" + HostInfo.Current.EQPID + "登录操作！");
                opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
                opiHis.Add("clientip", clientip);
                dbService.Inserthis_opilog(opiHis);
            }
            #endregion
            return WebSocketMessageStr;
        }
    }
}
