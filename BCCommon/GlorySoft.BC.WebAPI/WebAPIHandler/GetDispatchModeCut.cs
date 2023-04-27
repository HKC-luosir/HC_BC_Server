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
   public class GetDispatchModeCut : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> DispatchMode)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getrobotDtatListResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                object eqpid, dispatchMode;
                
                DispatchMode.TryGetValue("eqpid", out eqpid);
                DispatchMode.TryGetValue("dispatchMode", out dispatchMode);
                
                switch (dispatchMode.ToString())
                {
                    case "AUTO":
                        dispatchMode = EnumEqpAutoMode.AUTO;
                        break;
                    case"MANUAL":
                        dispatchMode = EnumEqpAutoMode.MANUAL;
                        break;
                    default:
                        break;
                }
                if (HostInfo.EQPInfo.EQPID == eqpid.ToString())
                {
                    HostInfo.EQPInfo.RobotDispatchMode = (EnumEqpAutoMode)dispatchMode;
                    HostInfo.EQPInfo.FunctionName = this.GetType().Name;
                    dbService.InsertHisEQPInfo(HostInfo.EQPInfo);
                    if (!dbService.UpdateEQPInfo(HostInfo.EQPInfo))
                    {
                        WebSocketMessageStr.result = new WebSocketResult()
                        {
                            returnCode = "1",
                            returnMessageEN = "Operation failed ! Data update Fail",
                            returnMessageCH = "操作失败！数据更新失败"
                        };
                        goto Res;
                    }
                }
                else
                {
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "1",
                        returnMessageEN = "Operation failed ! Robot Control Unit Not Exist!",
                        returnMessageCH = "操作失败！不存在Robot控制的Unit!"
                    };
                    goto Res;
                }
                WebSocketMessageStr.body = null;
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
            Res:
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了DispatchMode切换操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
