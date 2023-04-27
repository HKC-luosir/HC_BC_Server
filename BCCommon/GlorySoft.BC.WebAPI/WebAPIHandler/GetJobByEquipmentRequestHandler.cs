using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using GlorySoft.BC.WebSocket;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetJobByEquipmentRequestHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(Dictionary<string, object> glassInfo)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "GetJobByEquipment",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = "admin"
            };
            #endregion
            try
            {
                #region Body
                object content, pageNum, pageSize;
                glassInfo.TryGetValue("content", out content);
                glassInfo.TryGetValue("pageNum", out pageNum);
                glassInfo.TryGetValue("pageSize", out pageSize);

                Hashtable glassmap = new Hashtable();
                if (content.ToString() != "System.Collections.ArrayList")
                {
                    foreach (var item in content as Dictionary<string, object>)
                    {
                        if (item.Value != null)
                        {
                            glassmap.Add(item.Key, item.Value);
                        }
                    }
                }
                var glass = dbService.Viewwip_glassinfoSel(glassmap);
                var newGlass = glass.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                glassInfo.Add("total", glass.Count);
                glassInfo.Add("rows", newGlass);

                WebSocketMessageStr.body = glassInfo;
                #endregion

                Hashtable htdata = new Hashtable() {
                {"eqpid",glassmap["SelLine"] },
                {"unitid",glassmap["SelEqp"] } };
                SendOPIMessage.SendToWebSocketAlarmReport(htdata);
                SendOPIMessage.SendToWebSocketJobPosition(htdata);


                #region result;
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "操作成功！"
                };
                #endregion 
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
