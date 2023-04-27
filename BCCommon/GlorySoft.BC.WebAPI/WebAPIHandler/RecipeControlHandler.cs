using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class RecipeControlHandler : AbstractWebAPIMessageHandlercs
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

                object eqpid, unitids, checkflag;
                InitData.TryGetValue("eqpid", out eqpid);
                InitData.TryGetValue("UnitIds", out unitids);
                InitData.TryGetValue("CheckFlag", out checkflag);

                var EQPInfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid.ToString());
                var units = unitids.ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                switch (type)
                {
                    case "CurrentRecipeIdCheckFlagChange":
                        {
                            foreach (var unit in units)
                            {
                                var unitinfo = EQPInfo.Units.FirstOrDefault(c => c.UnitID == unit);
                                if (unitinfo != null)
                                {
                                    unitinfo.CurrentRecipeIdCheck = Convert.ToBoolean(checkflag);
                                }
                                dbService.UpdateUnitInfo(unitinfo);
                            }
                        }
                        break;
                    //case "RecipeParamCheckFlagChange":
                    //    {
                    //        foreach (var unit in units)
                    //        {
                    //            var unitinfo = EQPInfo.Units.FirstOrDefault(c => c.UnitID == unit);
                    //            if (unitinfo != null)
                    //            {
                    //                unitinfo.RecipeParamCheck = Convert.ToBoolean(checkflag);
                    //            }
                    //            dbService.UpdateUnitInfo(unitinfo);
                    //        }
                    //    }
                    //    break;
                    default:
                        break;
                }
                
                WebSocketMessageStr.body = null;

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
            opiHis.Add("operating", "进行了" + type + "操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
