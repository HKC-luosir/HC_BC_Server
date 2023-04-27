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
    public class GetBCCurrentInfoHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "BCCurrentInfo",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                BCCurrentInfo bi = new BCCurrentInfo();
                bi.eipconnect = "True";
                foreach (var currentinfo in HostInfo.Current.AllEQPInfo)
                {
                    switch (currentinfo.ControlState)
                    {
                        case EnumControlState.OffLine:
                            {
                                bi.rvconnect += (!String.IsNullOrEmpty(bi.rvconnect) ? ";" : "") + "OFFLINE";
                            }
                            break;
                        case EnumControlState.OnLineLocal:
                            {
                                bi.rvconnect += (!String.IsNullOrEmpty(bi.rvconnect) ? ";" : "") + "LOCAL";
                            }
                            break;
                        case EnumControlState.OnLineRemote:
                            {
                                bi.rvconnect += (!String.IsNullOrEmpty(bi.rvconnect) ? ";" : "") + "REMOTE";
                            }
                            break;
                        default:
                            break;
                    }
                    //组合线这里将所有线的设备都加进去

                    for (int i = 0; i < currentinfo.Units.Count; i++)
                    {
                        var type = currentinfo.Units[i].GetType().Name;
                        if (type != "Unit" && type != "Robot")
                        {
                            continue;
                        }

                        if (currentinfo.Units[i].IsConnect != "Alive")
                            bi.eipconnect = "False";//有一个设备false 则整线false
                    }
                }

                WebSocketMessageStr.body = bi;

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
