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
    public class GetInitHistoryHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getInitHistory",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                

                IList<TableInformation> tableInformationList = new List<TableInformation>();
                var GroupAuthority = dbService.ViewAllTableName();
                for (int i = 0; i < GroupAuthority.Count; i++)
                {
                   var gg= dbService.ViewTableStructure(GroupAuthority[i].TableName);
                    TableInformation tableInformation = new TableInformation() {
                        tableName= GroupAuthority[i].TableName,
                        tableProperties= dbService.ViewTableStructure(GroupAuthority[i].TableName)
                    };
                    tableInformationList.Add(tableInformation);
                }
                InitHistory initHistory = new InitHistory() {
                    rows = tableInformationList.ToArray(),
                    order="asc"
                };
                WebSocketMessageStr.body = initHistory;
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
