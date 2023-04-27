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
    public class GetOperationModeData : AbstractWebAPIMessageHandlercs
    {

        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitHistory)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "GetOperationModeDataReply",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                object eqpid, pageNum, pageSize;
                InitHistory.TryGetValue("eqpid", out eqpid);
                InitHistory.TryGetValue("pageNum", out pageNum);
                InitHistory.TryGetValue("pageSize", out pageSize);

                Hashtable hashtable = new Hashtable();
                hashtable.Add("eqpid", eqpid.ToString());
                var Viewcfg_operationmode= dbService.Viewcfg_operationmode(hashtable);
                var NewViewcfg_operationmode = Viewcfg_operationmode.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                InitHistory.Add("total", Viewcfg_operationmode.Count);
                InitHistory.Add("rows", NewViewcfg_operationmode);

                WebSocketMessageStr.body = InitHistory;

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
