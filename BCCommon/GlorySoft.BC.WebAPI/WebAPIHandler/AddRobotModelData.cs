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
    public class AddRobotModelData : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, bc_robot_model InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "addRobotModelDataResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                Hashtable hashtable = new Hashtable();
                hashtable.Add("eqpid", InitData.eqpid);
                hashtable.Add("unitid", InitData.unitid);
                hashtable.Add("modelid", InitData.modelid);
                hashtable.Add("modelposition", InitData.modelposition);
                hashtable.Add("uplinkname", InitData.uplinkname);
                hashtable.Add("downlinkname", InitData.downlinkname);
                hashtable.Add("portid", InitData.portid);
                hashtable.Add("sentoutname", InitData.sentoutname);
                hashtable.Add("unitno", InitData.unitno);

                //dbService.Insertbc_robot_model(hashtable);

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
            opiHis.Add("operating", "进行了RobotModelData数据添加操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }

    }
}
