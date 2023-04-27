using System;
using System.Collections;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using System.Linq;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class CIMMessageCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "CIMMessageCommandResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                #region Body

                object EqpID, UnitIDs, CIMMessageID, TouchPanelNumber, CIMMessageType, CIMMessageData, Action;
                InitData.TryGetValue("lineId", out EqpID);
                InitData.TryGetValue("equipmentNo", out UnitIDs);
                InitData.TryGetValue("cimmsgId", out CIMMessageID);
                InitData.TryGetValue("touchNo", out TouchPanelNumber);
                InitData.TryGetValue("msgType", out CIMMessageType);
                InitData.TryGetValue("message", out CIMMessageData);
                InitData.TryGetValue("action", out Action);

                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == EqpID.ToString());
                if (oEQP != null)
                {
                    ArrayList uids = UnitIDs as ArrayList;
                    object[] unitids = uids.ToArray();
                    foreach (var unitid in unitids)
                    {
                        var unitinfo = oEQP.Units.FirstOrDefault(c => c.UnitID == unitid.ToString());
                        if (unitinfo != null)
                        {

                            if (!Convert.ToBoolean(Action))
                            {
                                eqpService.SendCIMMessageSetCommand(unitinfo.UnitName, CIMMessageType.ToString(), CIMMessageID.ToString(), TouchPanelNumber.ToString(), CIMMessageData.ToString(), HostInfo.Current.GetTransactionID());
                            }
                            else
                            {
                                eqpService.SendCIMMessageClearCommand(unitinfo.UnitName, CIMMessageID.ToString(), TouchPanelNumber.ToString(), HostInfo.Current.GetTransactionID());
                            }
                        }
                    }
                }

                WebSocketMessageStr.body = null;

                #endregion

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

            #region OPI操作记录
            Hashtable opiHis = new Hashtable();
            opiHis.Add("userid", userName);
            opiHis.Add("operating", "进行了CIMMessageCommand下发操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
