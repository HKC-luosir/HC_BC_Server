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
    public class AddAlarmConfigSpecRequest : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "AddAlarmConfigSpecResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                #region Body
                WebSocketMessageStr.body = null;

                object eqpid, unitid, alarmid, alarmtext;
                InitData.TryGetValue("eqpid", out eqpid);
                InitData.TryGetValue("unitid", out unitid);
                InitData.TryGetValue("alarmID", out alarmid);
                InitData.TryGetValue("alarmText", out alarmtext);
                AlarmInfo data = new AlarmInfo();
                data.EQPID = eqpid.ToString();
                data.UNITID = unitid.ToString();
                data.AlarmID = alarmid.ToString();
                data.AlarmText = alarmtext.ToString();

                Hashtable ht = new Hashtable();
                ht.Add("eqpid", eqpid);
                ht.Add("unitid", unitid);
                ht.Add("alarmid", alarmid);
                var isExist = dbService.Viewcfg_alarmspec(ht);
                if (isExist != null && isExist.Count > 0)
                {
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "1",
                        returnMessageEN = "Operation failed, alarm id already exists!",
                        returnMessageCH = "操作失败, 该设备已存在相同AlarmID!"
                    };
                }
                else
                {
                    dbService.InsertAlarmInfo(data);
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "0",
                        returnMessageEN = "Operation sucessful !",
                        returnMessageCH = "操作成功！"
                    };
                }
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
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了Alarm Config Spec数据添加操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
