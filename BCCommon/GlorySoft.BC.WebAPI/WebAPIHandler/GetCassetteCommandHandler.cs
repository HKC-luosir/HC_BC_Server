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
    public class GetCassetteCommandHandler : AbstractWebAPIMessageHandlercs
    {
        public WebSocketMessage Execute(string userName, string clientip, Dictionary<string, object> InitData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            object eqpid, unitId, portId, command = null;
            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "SelectLineStatusSpecResponse",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion

                InitData.TryGetValue("eqpid", out eqpid);
                InitData.TryGetValue("unitId", out unitId);
                InitData.TryGetValue("portId", out portId);
                InitData.TryGetValue("command", out command);
                var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.UnitID == unitId.ToString() && c.PortID == portId.ToString());
                var eqpinfo = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.EQPID == eqpid.ToString());
                var unitinfo = eqpinfo.Units.FirstOrDefault(c => c.UnitID == unitId.ToString());
                portinfo.CassetteControlCommand = (EnumCassetteControlCommand)Enum.Parse(typeof(EnumCassetteControlCommand), command.ToString());

                var txid = HostInfo.Current.GetTransactionID();
                if (portinfo.CassetteControlCommand == EnumCassetteControlCommand.CassetteProcessCancel)
                {
                    portinfo.CassetteCancelText = $"cassette cancel from OPI, user:{userName}";
                }
                
                eqpService.SendCassetteControlCommand(unitinfo.UnitName, portinfo.PortNo.ToString(), (EnumCassetteControlCommand)Enum.Parse(typeof(EnumCassetteControlCommand), command.ToString()), portinfo.CassetteInfo.JobExistenceSlot, portinfo.CassetteInfo.JobCount.ToString(), txid);

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
            opiHis.Add("operating", "进行了" + command.ToString() + "命令下发 操作！");
            opiHis.Add("operationresult", WebSocketMessageStr.result.returnMessageCH);
            opiHis.Add("clientip", clientip);
            dbService.Inserthis_opilog(opiHis);
            #endregion
            return WebSocketMessageStr;
        }
    }
}
