using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetPalletHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> cstHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getPalletHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, unitid, palletid, firstdate, lastdate;
                cstHis.TryGetValue("pageNum", out pageNum);
                cstHis.TryGetValue("pageSize", out pageSize);
                cstHis.TryGetValue("unitid", out unitid);
                cstHis.TryGetValue("palletid", out palletid);
                cstHis.TryGetValue("firstdate", out firstdate);
                cstHis.TryGetValue("lastdate", out lastdate);

                var glassmap = new Hashtable();
                //glassmap.Add("eqpid", eqpid);
                if (unitid != null)
                {
                    glassmap.Add("unitid", unitid);
                }
                if (palletid != null)
                {
                    glassmap.Add("palletid", palletid);
                }
                if (firstdate != null )
                {
                    glassmap.Add("startcreatedate", firstdate);
                }
                if (lastdate !=  null)
                {
                    glassmap.Add("endcreatedate", lastdate);
                }
                var palletcount = dbService.Viewhis_palletListCount(glassmap);
                if (pageNum != null)
                {
                    glassmap.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    glassmap.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var pallet = dbService.Viewhis_palletList(glassmap);
                //var newdata = data.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                cstHis.Add("total", palletcount.Count);
                cstHis.Add("rows", pallet);

                WebSocketMessageStr.body = cstHis;
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
