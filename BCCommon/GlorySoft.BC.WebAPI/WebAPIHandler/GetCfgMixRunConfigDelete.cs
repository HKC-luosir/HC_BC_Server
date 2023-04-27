using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class GetCfgMixRunConfigDelete : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, string type, string id)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = type,
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = null,
                userName = userName
            };
            #endregion
            try
            {
                bool body = false;
                Hashtable map = new Hashtable();
                map.Add("id", id);
                var mixrunconfig = dbService.Viewcfg_mixrunconfig(map).FirstOrDefault();
                if(mixrunconfig!=null)
                {
                    map = new Hashtable();
                    map.Add("EQPID", mixrunconfig.eqpid);
                    map.Add("MachineRecipeName", mixrunconfig.machinerecipename);
                    var deleteCount= dbService.DeleteMIXRunInputRatio(map);
                    
                    var MIXRunInputRatioList=  dbService.GetMIXRunInputRatioList(map);
                    var deleteResult = dbService.Deletecfg_mixrunconfig(Convert.ToInt32(id));
                    if(MIXRunInputRatioList.Count()==0&& deleteResult)
                    {
                        body = true;
                    }
                }

                if (body)
                {
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "0",
                        returnMessageEN = "Operation sucessful !",
                        returnMessageCH = "操作成功！"
                    };
                }
                else
                {
                    WebSocketMessageStr.result = new WebSocketResult()
                    {
                        returnCode = "1",
                        returnMessageEN = "Operation failed !",
                        returnMessageCH = "操作失败！"
                    };
                }
                WebSocketMessageStr.body = body;

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
