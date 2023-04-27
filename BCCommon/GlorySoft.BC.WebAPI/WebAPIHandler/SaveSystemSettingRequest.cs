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
    public class SaveSystemSettingRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, List<bc_sys_setting> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SaveSystemSettingResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                //object lineId;
                //InitData.TryGetValue("lineId", out lineId);
                if (InitData != null && InitData.Count > 0)
                {
                    foreach (var data in InitData)
                    {
                        Hashtable hashtable = new Hashtable();
                        hashtable.Add("id", data.id);
                        hashtable.Add("bckey", data.bckey);
                        hashtable.Add("bcvalue", data.bcvalue);
                        hashtable.Add("describe", data.describe);
                        hashtable.Add("eqpid", data.eqpid);
                        hashtable.Add("unitid", data.unitid);
                        var res = dbService.Updatebc_sys_setting(hashtable);
                    }
                    //更新缓存
                    HostInfo.Current.SystemSetting = dbService.Viewbc_sys_setting(new Hashtable() { }).ToList();
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
            opiHis.Add("operating", "进行了System Setting数据修改操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
