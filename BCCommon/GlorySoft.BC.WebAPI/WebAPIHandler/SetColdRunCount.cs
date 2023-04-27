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
    public class SetColdRunCount : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, int coldRunTotalQuantity)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "setColdRunCounttResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                EQPInfo eQPInfo = HostInfo.Current.EQPInfo;
                eQPInfo.ColdRunTotalQuantity = coldRunTotalQuantity;
                HostInfo.EQPInfo.FunctionName = this.GetType().Name;
                dbService.InsertHisEQPInfo(HostInfo.EQPInfo);
                dbService.UpdateEQPInfo(eQPInfo);
                WebSocketMessageStr.body = true;
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
            opiHis.Add("operating", "设置了ColdRunCount数量为"+ coldRunTotalQuantity + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
