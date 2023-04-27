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
    public class GetCfgMixRunConfigUpdate : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, Dictionary<string, object> Init)
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
                Hashtable hashtable = new Hashtable();
                Hashtable hashtable2 = new Hashtable();
                foreach (var item in Init)
                {
                    hashtable.Add(item.Key, item.Value);
                    if (item.Key== "machinerecipename")
                    {
                        hashtable2.Add(item.Key, item.Value);
                    }
                }
                bool body = false;
                switch (type)
                {
                    case "GetCfgMixRunConfigUpdate":
                        body = dbService.Updatecfg_mixrunconfig(hashtable);
                        break;
                    case "GetCfgMixRunConfigAdd":
                        if (dbService.Viewcfg_mixrunconfig(hashtable2).Count>0)
                        {
                            break;
                        }
                        body = dbService.Insertcfg_mixrunconfig(hashtable);
                        break;
                    default:
                        break;
                }
                WebSocketMessageStr.body = body;
                if (body)
                {
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "0",
                        returnMessageEN = "Operation sucessful !",
                        returnMessageCH = "操作成功！"
                    };
                }
                else
                {
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
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了"+ type + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion

            return WebSocketMessageStr;
        }

    }
}
