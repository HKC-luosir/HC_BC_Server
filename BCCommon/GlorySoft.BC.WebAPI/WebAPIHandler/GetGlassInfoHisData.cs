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
    public class GetGlassInfoHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> cstHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getGlassInfoHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, unitid, portid, glassid, cassetteseqno, slotseqno, firstdate, lastdate;
                cstHis.TryGetValue("pageNum", out pageNum);
                cstHis.TryGetValue("pageSize", out pageSize);
                //cstHis.TryGetValue("eqpid", out eqpid);
                cstHis.TryGetValue("unitid", out unitid);
                cstHis.TryGetValue("portid", out portid);
                cstHis.TryGetValue("glassid", out glassid);
                cstHis.TryGetValue("cassetteseqno", out cassetteseqno);
                cstHis.TryGetValue("slotseqno", out slotseqno);
                cstHis.TryGetValue("firstdate", out firstdate);
                cstHis.TryGetValue("lastdate", out lastdate);

                var glassmap = new Hashtable();
                //glassmap.Add("eqpid", eqpid);
                if (unitid != null)
                {
                    glassmap.Add("currentsunit", unitid);
                }
                if (portid != null)
                {
                    glassmap.Add("portid", portid);
                }
                if (glassid != null)
                {
                    glassmap.Add("glassid", glassid);
                }
                if (cassetteseqno != null)
                {
                    int icassetteseqno = 0;
                    int.TryParse(cassetteseqno.ToString(), out icassetteseqno);
                    if (icassetteseqno > 0)
                        glassmap.Add("cassettesequenceno", icassetteseqno);
                }
                if (slotseqno != null)
                {
                    int islotseqno = 0;
                    int.TryParse(slotseqno.ToString(), out islotseqno);
                    if (islotseqno > 0)
                        glassmap.Add("slotsequenceno", islotseqno);
                }
                if (firstdate != null )
                {
                    glassmap.Add("startcreatedate", firstdate);
                }
                if (lastdate !=  null)
                {
                    glassmap.Add("endcreatedate", lastdate);
                }
                var glasscount = dbService.Viewhis_glassinfoCount(glassmap);
                if (pageNum != null)
                {
                    glassmap.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    glassmap.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var glass = dbService.Viewhis_glassinfo(glassmap);
                //var newGlass = glass.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                cstHis.Add("total", glasscount[0].rowcount);
                cstHis.Add("rows", glass);

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
