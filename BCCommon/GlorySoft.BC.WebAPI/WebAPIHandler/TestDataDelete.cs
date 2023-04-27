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
    public class TestDataDelete : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type,his_opilog his_Opilog)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = type,
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };

                if (dbService.Deletehis_opilog(his_Opilog.id))
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
                Logger.Error("[WebApi]" + ex);
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
            opiHis.Add("operating", "进行了OPILog数据删除操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion

            WebSocketMessageStr.body = WebSocketMessageStr.result.returnCode;

            return WebSocketMessageStr;
        }

    }
}
