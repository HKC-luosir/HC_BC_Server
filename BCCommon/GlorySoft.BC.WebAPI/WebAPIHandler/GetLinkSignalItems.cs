using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.Configuration;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
   public class GetLinkSignalItems : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName,string type)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "GetLinkSignalItemsResponse",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = userName,
                userName = type
            };
            #endregion
            try
            {
                #region Body
                List<OPILinkSignal> oPILinkSignals = new List<OPILinkSignal>();

                List<LinkSignalMappingItem> LinkSignalMappingItem = HostInfo.Current.LinkSignal.LinkSignalMappingItemList.mappingItems;
                foreach (var item in LinkSignalMappingItem)
                {
                    OPILinkSignal oPILinkSignal = new OPILinkSignal();
                    oPILinkSignal.itemGroupName = item.name;
                    oPILinkSignal.itemBeans = new List<ItemBeans>();
                    foreach (var item2 in item.LinkSignalMappingValueList)
                    {
                        ItemBeans itemBeans = new ItemBeans() {
                            itemName = item2.Name,
                            itemValue = Convert.ToInt32(item2.Offset),
                            offSet = item2.Offset,
                            points = item2.Points,
                            type = item2.Type,
                        };
                        oPILinkSignal.itemBeans.Add(itemBeans);
                    }
                    oPILinkSignals.Add(oPILinkSignal);
                }
                WebSocketMessageStr.body = oPILinkSignals;
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
