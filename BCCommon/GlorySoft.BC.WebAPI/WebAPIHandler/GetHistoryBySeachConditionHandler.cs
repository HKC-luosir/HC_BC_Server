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
    public class GetHistoryBySeachConditionHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> BySeachCondition)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getHistoryTableInformation",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                object tableName, fields;

                BySeachCondition.TryGetValue("tableName", out tableName);
                BySeachCondition.TryGetValue("fields", out fields);
                ArrayList dfs = fields as ArrayList;

                Dictionary<string, object> fieldList = new Dictionary<string, object>();
                for (int i = 0; i < dfs.Count; i++)
                {
                    Hashtable sunitmap = new Hashtable
                    {
                        {"returnstr",dfs[i]},
                        {"value",tableName }
                    };
                    IList<DisCol> list = dbService.ViewDisCol(sunitmap);
                    for (int j = 0; j < list.Count; j++)
                    {
                        list[j].label = list[j].returnstr.ToString();
                        list[j].value = list[j].returnstr.ToString();
                    }
                    fieldList.Add(dfs[i].ToString(), list);
                }

                BySeachCondition.Add("fieldList", fieldList);

                WebSocketMessageStr.body = BySeachCondition;


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
