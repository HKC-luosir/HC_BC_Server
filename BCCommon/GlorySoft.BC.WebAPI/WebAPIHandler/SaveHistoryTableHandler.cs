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
    public class SaveHistoryTableHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string body, string common)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "saveHistoryTable",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Map\\HistoryJson\\"+ common))
                {
                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Map\\HistoryJson\\" + common);
                }

                // 创建文件
                FileStream fs = new FileStream("Map\\HistoryJson\\"+ common, FileMode.OpenOrCreate, FileAccess.ReadWrite); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
                StreamWriter sw = new StreamWriter(fs); // 创建写入流
                sw.WriteLine(body); // 写入Hello World
                sw.Close(); //关闭文件
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
