
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
    public class Pht600SetPortHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(Dictionary<string, object> Init)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "Pht600SetPort",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = "admin"
            };
            #endregion
            try
            {
                // Hashtable hashtable = new Hashtable();
                var setPort = "";
               
                foreach (var item in Init)
                {
                   // hashtable.Add(item.Key, item.Value);
                    if (item.Key == "portid")
                    {
                        setPort = item.Value.ToString();
                    }
                    
                }
                if(HostInfo.Current.EQPID.Contains("PHT600"))
                {                     
                    if(string.IsNullOrEmpty(setPort)|| HostInfo.EQPInfo.PHT600Port!=setPort)
                    {
                        LogHelper.BCLog.Debug("[Pht600SetPortHandler] setPort is null; || HostInfo.EQPInfo.PHT600Port!=setPort");
                        if(!string.IsNullOrEmpty(HostInfo.EQPInfo.PHT600Port))
                        {
                            LogHelper.BCLog.Debug("[Pht600SetPortHandler] PHT600Port != null; ");
                            var port = HostInfo.PortList.FirstOrDefault(o => o.PortID == HostInfo.EQPInfo.PHT600Port);
                            LogHelper.BCLog.Debug(string.Format("[Pht600SetPortHandler] PHT600Port:{0}; PortStatus:{1} ", port.PortID, port.PortStatus));
                            if (port.PortStatus!=3&& port.PortStatus!=4)
                            {                                
                                WebSocketMessageStr.result = new WebSocketResult()
                                {
                                    returnCode = "1",
                                    returnMessageEN = "Operation failed !PHT600Port未下架",
                                    returnMessageCH = "操作失败！PHT600Port未下架"
                                };
                                return WebSocketMessageStr;
                            }
                        }
                    }

                    HostInfo.EQPInfo.PHT600Port = setPort;
                    //HostInfo.EQPInfo.PHT600PortSlot = 0;
                    dbService.UpdateEQPInfo(HostInfo.EQPInfo);
                    #region result;
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "0",
                        returnMessageEN = "Operation sucessful !",
                        returnMessageCH = "操作成功！"
                    };
                    #endregion
                }
                else
                {
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "1",
                        returnMessageEN = "Operation failed !Lineid!=PHT600",
                        returnMessageCH = "操作失败！不是PHT600"
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
            return WebSocketMessageStr;
        }
    }
}
