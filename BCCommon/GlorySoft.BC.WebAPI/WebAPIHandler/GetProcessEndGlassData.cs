using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetProcessEndGlassData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> Initdata)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getProcessEndGlassData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, equipmentid, panelid, parentid;
                Initdata.TryGetValue("pageNum", out pageNum);
                Initdata.TryGetValue("pageSize", out pageSize);
                Initdata.TryGetValue("equipmentid", out equipmentid);
                Initdata.TryGetValue("panelid", out panelid);
                Initdata.TryGetValue("parentid", out parentid);

                var serdata = new Hashtable();
                if (equipmentid != null)
                {
                    serdata.Add("equipmentid", equipmentid);
                }
                if (panelid != null)
                {
                    serdata.Add("panelid", panelid);
                }
                if (parentid != null)
                {
                    serdata.Add("parentid", parentid);
                }
                var data = dbService.Viewwip_processend_glassList(serdata);
                //var newdata = data.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                Initdata.Add("total", data.Count);
                Initdata.Add("rows", data);

                WebSocketMessageStr.body = Initdata;
                #endregion
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
