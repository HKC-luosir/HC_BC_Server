using System;
using System.Collections;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class SaveProcessEndGlassData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, ProcessEndGlassInfo InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "saveProcessEndGlassData",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                if (InitData.listData.Count > 0)
                {
                    //新增 修改
                    foreach (var data in InitData.listData)
                    {
                        if (data.id == 0)
                        {
                            dbService.Insertwip_processend_glass(data);
                        }
                        else
                        {
                            dbService.Updatewip_processend_glass(data);
                        }
                    }
                }
                if (!String.IsNullOrEmpty(InitData.needDelData))
                {
                    var deldatas = InitData.needDelData.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    if (deldatas.Length > 0)
                    {
                        //删除
                        foreach (var deldata in deldatas)
                        {
                            Hashtable delHT = new Hashtable();
                            delHT.Add("id", Convert.ToInt32(deldata));
                            dbService.Deletewip_processend_glass(delHT);
                        }
                    }
                }
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
            opiHis.Add("operating", "进行了ProcessEndGlass数据更新操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
