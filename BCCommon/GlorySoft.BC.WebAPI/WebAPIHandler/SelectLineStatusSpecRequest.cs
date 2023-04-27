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
    public class SelectLineStatusSpecRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SelectLineStatusSpecResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                object lineId, unitId, eqpStatus;
                InitData.TryGetValue("lineId", out lineId);
                InitData.TryGetValue("unitId", out unitId);
                InitData.TryGetValue("eqpStatus", out eqpStatus);
                Hashtable hashtable = new Hashtable();
                hashtable.Add("eqpid", lineId);
                hashtable.Add("unitidlist", unitId);
                hashtable.Add("eqpstatus", eqpStatus);

                var list = dbService.Viewcfg_eqpstatusrule(hashtable).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    //string UnitStatus = "";
                    //switch (list[i].eqpstatus)
                    //{
                    //    case 1:
                    //        UnitStatus = "PM";
                    //        break;
                    //    case 2:
                    //        UnitStatus = "BM";
                    //        break;
                    //    case 3:
                    //        UnitStatus = "PAUSE";
                    //        break;
                    //    case 4:
                    //        UnitStatus = "IDLE";
                    //        break;
                    //    case 5:
                    //        UnitStatus = "RUN";
                    //        break;
                    //    default:
                    //        break;
                    //}
                    list[i].eqpstatustext = list[i].eqpstatus;
                }
                WebSocketMessageStr.body = list;
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
            return WebSocketMessageStr;
        }
    }
}
