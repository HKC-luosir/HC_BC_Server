using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class RobotModelControlHandler : AbstractWebAPIMessageHandlercs
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

                object eqpid, unitid, modelposition, pageNum, pageSize;
                InitData.TryGetValue("pageNum", out pageNum);
                InitData.TryGetValue("pageSize", out pageSize);
                InitData.TryGetValue("eqpid", out eqpid);
                InitData.TryGetValue("unitid", out unitid);
                InitData.TryGetValue("modelposition", out modelposition);

                switch (type)
                {
                    case "SelectRobotModelList":
                        {
                            ht = new Hashtable();
                            ht.Add("eqpid", eqpid.ToString());
                            ht.Add("unitid", unitid.ToString());
                            IList<bc_robot_model> list = dbService.Viewbc_robot_model(ht);
                            var newdata = list.Skip(((int)pageNum - 1) * (int)pageSize).Take((int)pageSize);
                            InitData.Add("total", list.Count);
                            InitData.Add("rows", newdata);
                            WebSocketMessageStr.body = InitData;
                        }
                        break;
                    case "RobotModelDelete":
                        {
                            ht = new Hashtable();
                            ht.Add("eqpid", eqpid.ToString());
                            ht.Add("unitid", unitid.ToString());
                            ht.Add("modelposition", Convert.ToInt32(modelposition));
                            dbService.Deletebc_robot_model(ht);
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
