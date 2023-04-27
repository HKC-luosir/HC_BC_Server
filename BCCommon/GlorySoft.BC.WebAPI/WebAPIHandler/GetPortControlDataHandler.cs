using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetPortControlDataHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "getPortControlData",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                object eqpid;
                InitData.TryGetValue("eqpid", out eqpid);

                Hashtable ht = new Hashtable();

                Dictionary<string,object> data = new Dictionary<string, object>();
                var PortMode_Substrate_Type = HostInfo.EQRule.EQMappingItemList.mappingItems.FirstOrDefault(o => o.name == "PortMode_Substrate_Type").EQMappingValueList;
                data.Add("PortMode_Substrate_Type", PortMode_Substrate_Type);
                var PortMode_Job_Type = HostInfo.EQRule.EQMappingItemList.mappingItems.FirstOrDefault(o => o.name == "PortMode_Job_Type").EQMappingValueList;
                data.Add("PortMode_Job_Type", PortMode_Job_Type);
                var PortMode_Judge_Port_Use_Type = HostInfo.EQRule.EQMappingItemList.mappingItems.FirstOrDefault(o => o.name == "PortMode_Judge_Port_Use_Type").EQMappingValueList;
                data.Add("PortMode_Judge_Port_Use_Type", PortMode_Judge_Port_Use_Type);
                var PortCassetteType = HostInfo.EQRule.EQMappingItemList.mappingItems.FirstOrDefault(o => o.name == "PortCassetteType").EQMappingValueList;
                data.Add("PortCassetteType", PortCassetteType);
                ht.Add("eqpid", eqpid);
                ht.Add("enabled", 0);
                IList<cfg_portgradegroup> list = dbService.Viewcfg_portgradegroup(ht);
                data.Add("PortGradeGroup", list);

                WebSocketMessageStr.body = data;

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
            return WebSocketMessageStr;
        }

    }
}
