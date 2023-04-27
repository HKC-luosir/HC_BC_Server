using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class PortGradeGroupUpdateHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, cfg_portgradegroup data)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = type,
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                switch (type)
                {
                    case "PortGradeGroupAdd":
                        {
                            dbService.Insertcfg_portgradegroup(data);
                        }
                        break;
                    case "PortGradeGroupUpdate":
                        {
                            dbService.Updatecfg_portgradegroup(data);
                        }
                        break;
                    default:
                        break;
                }
                var removedata = new List<cfg_portgradegroup>();
                HostInfo.PortGradeGroupList.TryRemove(data.eqpid, out removedata);
                Hashtable PortGradeGroupHT = new Hashtable();
                PortGradeGroupHT.Add("eqpid", data.eqpid);
                PortGradeGroupHT.Add("enabled", 0);
                var PortGradeGroup = dbService.Viewcfg_portgradegroup(PortGradeGroupHT).ToList();
                if (PortGradeGroup.Count > 0)
                {
                    HostInfo.PortGradeGroupList.TryAdd(data.eqpid, PortGradeGroup);
                }

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

        Res:
            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了" + type + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
