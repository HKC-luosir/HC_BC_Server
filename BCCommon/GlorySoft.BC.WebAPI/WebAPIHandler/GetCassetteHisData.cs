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
    public class GetCassetteHisData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> cstHis)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "getCassetteHisData",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                #region Body
                object pageNum, pageSize, cassetteid, cassetteseqno, unitid, portid, firstdate, lastdate;
                cstHis.TryGetValue("pageNum", out pageNum);
                cstHis.TryGetValue("pageSize", out pageSize);
                cstHis.TryGetValue("unitid", out unitid);
                cstHis.TryGetValue("portid", out portid);
                cstHis.TryGetValue("cassetteid", out cassetteid);
                cstHis.TryGetValue("cassetteseqno", out cassetteseqno);
                cstHis.TryGetValue("firstdate", out firstdate);
                cstHis.TryGetValue("lastdate", out lastdate);

                var glassmap = new Hashtable();
                if (unitid != null)
                {
                    glassmap.Add("unitid", unitid);
                }
                if (portid != null)
                {
                    glassmap.Add("portid", portid);
                }
                if (cassetteid != null)
                {
                    glassmap.Add("cassetteid", cassetteid);
                }
                if (cassetteseqno != null)
                {
                    int icassetteseqno = 0;
                    int.TryParse(cassetteseqno.ToString(), out icassetteseqno);
                    if (icassetteseqno > 0)
                        glassmap.Add("cassettesequenceno", icassetteseqno);
                }
                if (firstdate != null)
                {
                    glassmap.Add("startcreatedate", firstdate);
                }
                if (lastdate != null)
                {
                    glassmap.Add("endcreatedate", lastdate);
                }
                var cstcount = dbService.Viewhis_cassetteCount(glassmap);
                if (pageNum != null)
                {
                    glassmap.Add("limitpage", Convert.ToInt32(pageNum) - 1);
                }
                if (pageSize != null)
                {
                    glassmap.Add("limitcount", Convert.ToInt32(pageSize));
                }
                var cst = dbService.Viewhis_cassette(glassmap);
                //var newGlass = glass.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                cstHis.Add("total", cstcount.Count);
                cstHis.Add("rows", cst);

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
