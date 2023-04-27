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
    public class GetMaterialHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> cstHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getMaterialHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, unitid, materialid, firstdate, lastdate;
                cstHis.TryGetValue("pageNum", out pageNum);
                cstHis.TryGetValue("pageSize", out pageSize);
                //cstHis.TryGetValue("eqpid", out eqpid);
                cstHis.TryGetValue("unitid", out unitid);
                cstHis.TryGetValue("materialid", out materialid);
                cstHis.TryGetValue("firstdate", out firstdate);
                cstHis.TryGetValue("lastdate", out lastdate);

                var glassmap = new Hashtable();
                //glassmap.Add("eqpid", eqpid);
                if (unitid != null)
                {
                    glassmap.Add("unitid", unitid);
                }
                if (materialid != null)
                {
                    glassmap.Add("materialid", materialid);
                }
                if (firstdate != null )
                {
                    glassmap.Add("startcreatedate", firstdate);
                }
                if (lastdate !=  null)
                {
                    glassmap.Add("endcreatedate", lastdate);
                }
                var materialcount = dbService.Viewhis_materialCount(glassmap);
                if (pageNum != null)
                {
                    glassmap.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    glassmap.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var material = dbService.Viewhis_material(glassmap);
                //var newdata = data.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                cstHis.Add("total", materialcount.Count);
                cstHis.Add("rows", material);

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
