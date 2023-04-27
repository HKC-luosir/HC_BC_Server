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
   public class GetUnitHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName,Dictionary<string, object> unitHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getUnitHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, eqpid, unitid, firstdate, lastdate;
                unitHis.TryGetValue("pageNum", out pageNum);
                unitHis.TryGetValue("pageSize", out pageSize);
                unitHis.TryGetValue("eqpid", out eqpid);
                unitHis.TryGetValue("unitid", out unitid);
                unitHis.TryGetValue("firstdate", out firstdate);
                unitHis.TryGetValue("lastdate", out lastdate);

                var glassmap = new Hashtable();
                glassmap.Add("eqpid", eqpid);
                if (unitid != null)
                {
                    glassmap.Add("unitid", unitid);
                }
                if (firstdate != null)
                {
                    glassmap.Add("startcreatedate", firstdate);
                }
                if (lastdate != null)
                {
                    glassmap.Add("endcreatedate", lastdate);
                }
                var unitcount = dbService.Viewhis_unitCount(glassmap);
                if (pageNum != null)
                {
                    glassmap.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    glassmap.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var unit = dbService.Viewhis_unit(glassmap);
                //var newGlass = glass.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                unitHis.Add("total", unitcount.Count);
                unitHis.Add("rows", unit);

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
