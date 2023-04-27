using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class PortGradeGroupControlHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, Dictionary<string, object> InitData)
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
                Hashtable ht = new Hashtable();

                object id, eqpid, portgradegroup, portgrade, pageNum, pageSize;
                InitData.TryGetValue("pageNum", out pageNum);
                InitData.TryGetValue("pageSize", out pageSize);
                InitData.TryGetValue("id", out id);
                InitData.TryGetValue("eqpid", out eqpid);
                InitData.TryGetValue("portgradegroup", out portgradegroup);
                InitData.TryGetValue("portgrade", out portgrade);

                switch (type)
                {
                    case "SelectPortGradeGroupList":
                        {
                            ht = new Hashtable();
                            ht.Add("eqpid", eqpid.ToString());
                            ht.Add("portgradegroup", portgradegroup.ToString());
                            ht.Add("portgrade", portgrade.ToString());
                            IList<cfg_portgradegroup> list = dbService.Viewcfg_portgradegroup(ht);
                            var newdata = list.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                            InitData.Add("total", list.Count);
                            InitData.Add("rows", newdata);
                            WebSocketMessageStr.body = InitData;
                        }
                        break;
                    case "PortGradeGroupDelete":
                        {
                            ht = new Hashtable();
                            ht.Add("id", Convert.ToInt32(id));
                            IList<cfg_portgradegroup> hasdata = dbService.Viewcfg_portgradegroup(ht);

                            if (hasdata.Any())
                            {
                                var data = hasdata.FirstOrDefault();

                                ht = new Hashtable();
                                ht.Add("id", Convert.ToInt32(id));
                                dbService.Deletecfg_portgradegroup(ht);

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
                            }
                        }
                        break;
                    default:
                        break;
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
            if (!type.Contains("Select"))
            {
                Hashtable opiHis = new Hashtable();
                opiHis.Add("userid", userName);
                opiHis.Add("operating", "进行了" + type + "操作！");
                opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
                opiHis.Add("clientip", clientip);
                dbService.Inserthis_opilog(opiHis);
            }
            #endregion
            return WebSocketMessageStr;
        }
    }
}
