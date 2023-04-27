using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using System.Linq;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class SamplingDownloadCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SamplingDownloadCommandResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                #region Body

                object UnitName, BatchQty;
                InitData.TryGetValue("UnitName", out UnitName);
                InitData.TryGetValue("BatchQty", out BatchQty);

                eqpService.SendSamplingDownloadCommand(UnitName.ToString(), BatchQty.ToString(), HostInfo.Current.GetTransactionID());

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

            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了SamplingDownloadCommand下发操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
