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
using Glorysoft.BC.Logic.Contract;
using Glorysoft.BC.WebAPI;


namespace Glorysoft.BC.WebAPI.WebAPIHandler
{
    public class SetLocalRecive : AbstractWebAPIMessageHandlercs
    {
      //  protected static IWebSocketService webSocketService;
        public WebSocketMessage Execute(string userName, string PortId)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            try
            {
                #region Handler
                WebSocketMessageStr.header = new WebSocketHeader()
                {
                    messageName = "execuleBcCommand",
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    inboxName = null,
                    userName = userName
                };
                #endregion
                var port = HostInfo.PortList.FirstOrDefault(o => o.PortID == PortId);
                LotInformation lotInformation = new LotInformation();
                lotInformation.UnitID = port.UnitID;
                lotInformation.PortID = port.PortID;
                lotInformation.CassetteID = port.CassetteID;
                lotInformation.GlassExistence = port.CassetteInfo.SlotMap;
                lotInformation.GlassCount = port.GlassInfos.Count().ToString();
                lotInformation.CSTSeqNo = port.CassetteSequenceNo.ToString();
                lotInformation.GlassInfos = port.GlassInfos.ToList();
                webSocketService.SendToWebSocketLotInformation(lotInformation);

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

            return WebSocketMessageStr;
        }


    }
}
