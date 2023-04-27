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
    public class AddLineStatusSpecRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, cfg_eqpstatusrule InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "AddLineStatusSpecResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                //switch (InitData.eqpstatustext)
                //{
                //    case "PM":
                //        InitData.eqpstatus = 1;
                //        break;
                //    case "BM":
                //        InitData.eqpstatus = 2;
                //        break;
                //    case "PAUSE":
                //        InitData.eqpstatus = 3;
                //        break;
                //    case "IDLE":
                //        InitData.eqpstatus = 4;
                //        break;
                //    case "RUN":
                //        InitData.eqpstatus = 5;
                //        break;
                //    default:
                //        break;
                //}
                Hashtable hashtable = new Hashtable();
                if (InitData.lineId == null)
                {
                    InitData.lineId = InitData.eqpid;
                }
                hashtable.Add("unitidlist", InitData.unitidlist);
                hashtable.Add("eqpstatus", InitData.eqpstatustext);
                hashtable.Add("eqpid", InitData.lineId);
                //判断是否存在  存在则删除后在添加
                var list = dbService.Viewcfg_eqpstatusrule(hashtable);
                if (list.Count != 0)
                {
                    dbService.Deletecfg_eqpstatusrule(hashtable);
                }
                dbService.Insertcfg_eqpstatusrule(hashtable);
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
            opiHis.Add("operating", "进行了EQPStatusRule数据添加操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
