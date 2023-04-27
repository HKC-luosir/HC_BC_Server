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
    public class GetHistorySelectFilesHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string common)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getHistorySelectFiles",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion


                string HistoryJson = "";
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Map\\HistoryJson\\";
                if (common == "{}")
                {
                    var files = Directory.GetFiles(baseDirectory);
                    foreach (var file in files)
                    {
                        string filename = new FileInfo(file).Name;
                        if (true)
                        {

                        }
                        string reportfilepath = baseDirectory + filename;

                        string reportContent = File.ReadAllText(reportfilepath);
                        HistoryJson += reportContent+"@";
                    }
                }
                else
                {
                    string reportfilepath = baseDirectory + common;
                    string reportContent = File.ReadAllText(reportfilepath);
                    HistoryJson = reportContent;
                }

                WebSocketMessageStr.body = HistoryJson;
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
