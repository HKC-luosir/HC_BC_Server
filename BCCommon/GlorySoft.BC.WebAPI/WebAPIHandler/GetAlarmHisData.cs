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
   public class GetAlarmHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> unitHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getAlarmHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, eqpid, unitid, firstdate, lastdate, alarmtype, alarmid;
                unitHis.TryGetValue("pageNum", out pageNum);
                unitHis.TryGetValue("pageSize", out pageSize);
                unitHis.TryGetValue("eqpid", out eqpid);
                unitHis.TryGetValue("unitid", out unitid);
                unitHis.TryGetValue("alarmtype", out alarmtype);
                unitHis.TryGetValue("alarmid", out alarmid);
                unitHis.TryGetValue("firstdate", out firstdate);
                unitHis.TryGetValue("lastdate", out lastdate);

                var glassmap = new Hashtable();
                glassmap.Add("eqpid", eqpid);
                if (unitid != null)
                {
                    glassmap.Add("unitid", unitid);
                }
                if (alarmtype != null)
                {
                    glassmap.Add("alarmtype", alarmtype);
                }
                if (alarmid != null)
                {
                    glassmap.Add("alarmid", alarmid);
                }
                if (firstdate != null)
                {
                    glassmap.Add("startcreatedate", firstdate);
                }
                if (lastdate != null)
                {
                    glassmap.Add("endcreatedate", lastdate);
                }
                var alarmcount = dbService.Viewhis_alarmCount(glassmap);
                if (pageNum != null)
                {
                    glassmap.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    glassmap.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var alarm = dbService.Viewhis_alarm(glassmap);
                //var newGlass = glass.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                unitHis.Add("total", alarmcount.Count);
                unitHis.Add("rows", alarm);

                WebSocketMessageStr.body = unitHis;
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
