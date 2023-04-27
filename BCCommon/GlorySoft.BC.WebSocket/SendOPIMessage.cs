using Glorysoft.BC.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using GlorySoft.BC.WebSocket.Common;
using System.Collections;

namespace GlorySoft.BC.WebSocket
{

    public class SendOPIMessage: AbstractWebSocketMessageHandlercs
    {

        public static void SendWebSocketTestMessage()
        {
            //FGCodeRestrictPushACK e = new FGCodeRestrictPushACK();
            //string s = e.JsonSerializer();
            //webSocket.SendToMCIM(s);
            EQPInfoRequest EQPInfoRequest = new EQPInfoRequest();
            string EQPInfoRequestStr = JsonFormat(EQPInfoRequest, EQPInfoRequest.TYPE);
            //var message= GetJsonType(EQPInfoRequestStr);




            EQPInfoReport EQPInfoReport = new EQPInfoReport();
            var eqp = HostInfo.Current.EQPInfo;
            EQPInfoReport.ControlState = eqp.ControlState.ToString();
            EQPInfoReport.EqpStatus = eqp.EqpStatus;
            EQPInfoReport.ReasonCode = eqp.ReasonCode;
            EQPInfoReport.EquipmentAutoMode = eqp.RobotDispatchMode.ToString();

            string WebSocketMessageStr = JsonFormat(EQPInfoReport, EQPInfoReport.TYPE);

            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);




            var begin = (WebSocketMessageStr.IndexOf("\"body\"") + 8);
            var end = (WebSocketMessageStr.IndexOf(",\"result\":") - begin - 1);
            var body = WebSocketMessageStr.Substring(begin, end);

            EQPInfoReport = JsonConvert.DeserializeObject<EQPInfoReport>(body);
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //EQPInfoReport = js.Deserialize<EQPInfoReport>(WebSocketMessageStr);

        }

        private static string JsonFormat(Object obj, string messageName)
        {
            var WebSocketMessage = new WebSocketMessage();
            var header = new WebSocketHeader()
            {
                messageName = messageName,
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                userId = HostInfo.Current.EQPInfo.EQPID,

            };
            WebSocketMessage.header = header;
            var result = new WebSocketResult() { returnCode = "0", returnMessageCH = "", returnMessageEN = "" };
            WebSocketMessage.result = result;
            var body = new List<WebSocketBody>();
            //var WebSocketBody = obj as WebSocketBody;
            //body.Add(WebSocketBody);
            WebSocketMessage.body = obj;
            string WebSocketMessageStr = WebSocketMessage.JsonSerializer();
            return WebSocketMessageStr;
        }

        #region matti
        public static void SendToWebSocketClientReport(HostReturnMessage data)
        {
            string WebSocketMessageStr = JsonFormat(data, "PublishMessageToClientReport");
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        #endregion

        public static void SendToWebSocketMessageReply(MessageReply MessageReply)
        {
            string WebSocketMessageStr = JsonFormat(MessageReply, MessageReply.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketOPCall(Glorysoft.BC.Entity.WebSocketEntity.OpCallInfo OpCallInfo)
        {
            string WebSocketMessageStr = JsonFormat(OpCallInfo, OpCallInfo.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketCassetteOnPortTime(Glorysoft.BC.Entity.WebSocketEntity.CassetteOnPortTime BCInformationReport)
        {
            string WebSocketMessageStr = JsonFormat(BCInformationReport, BCInformationReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketLotInformation(LotInformation LotInformation)
        {
            string WebSocketMessageStr = JsonFormat(LotInformation, LotInformation.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketEQPInfoReport(EQPInfoReport EQPInfoReport)
        {
            //string EQPInfoReportStr = EQPInfoReport.JsonSerializer();
            string WebSocketMessageStr = JsonFormat(EQPInfoReport, EQPInfoReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketUnitInfoReport(UnitInfoReport UnitInfoReport)
        {
            //string UnitInfoReportStr = UnitInfoReport.JsonSerializer();
            //WebSocketHandler.Current.SendToWebSocket(UnitInfoReportStr);
            string WebSocketMessageStr = JsonFormat(UnitInfoReport, UnitInfoReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortInfoReport(PortInfoReport PortInfoReport)
        {
            string WebSocketMessageStr = JsonFormat(PortInfoReport, PortInfoReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketGlassInfoReport(GlassInfoReport GlassInfoReport)
        {
            string WebSocketMessageStr = JsonFormat(GlassInfoReport, GlassInfoReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketTrayInfoReport(TrayInfoReport TrayInfoReport)
        {
            string WebSocketMessageStr = JsonFormat(TrayInfoReport, TrayInfoReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketRecipeInfoReport(RecipeInfoReport RecipeInfoReport)
        {
            string WebSocketMessageStr = JsonFormat(RecipeInfoReport, RecipeInfoReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketCimMessageReport(CimMessageReport CimMessageReport)
        {
            string WebSocketMessageStr = JsonFormat(CimMessageReport, CimMessageReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketAlarmReport(Hashtable hashtable)
        {
            var list=  dbService.Viewwip_alarm(hashtable).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].createdatetext = list[i].createdate.ToString("yyyy-MM-dd HH:mm:ss");
                }
                string WebSocketMessageStr = JsonFormat(list.ToArray(), "AlarmReport");
                WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketJobPosition(Hashtable hashtable)
        {
            var list = dbService.Viewcfg_glassexistenceposition(hashtable).ToList();
            string WebSocketMessageStr = JsonFormat(list.ToArray(), "PublishJobPositionToClientReport");
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketCurrentRecipeNoChangeReport(CurrentRecipeNoChangeReport CurrentRecipeNoChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(CurrentRecipeNoChangeReport, CurrentRecipeNoChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketCSTOperationModeReport(CSTOperationModeReport CSTOperationModeReport)
        {
            string WebSocketMessageStr = JsonFormat(CSTOperationModeReport, CSTOperationModeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketIndexerOperationModeChangeReport(IndexerOperationModeChangeReport IndexerOperationModeChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(IndexerOperationModeChangeReport, IndexerOperationModeChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketLoaderPortQTimeChangeReport(LoaderPortQTimeChangeReport LoaderPortQTimeChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(LoaderPortQTimeChangeReport, LoaderPortQTimeChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortCassetteTypeChangeModeReport(PortCassetteTypeChangeModeReport PortCassetteTypeChangeModeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortCassetteTypeChangeModeReport, PortCassetteTypeChangeModeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortStatusChangeReport(PortStatusChangeReport PortStatusChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortStatusChangeReport, PortStatusChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortModeChangeReport(PortModeChangeReport PortModeChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortModeChangeReport, PortModeChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortOperationModeChangeReport(PortOperationModeChangeReport PortOperationModeChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortOperationModeChangeReport, PortOperationModeChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortTransferModeChangeReport(PortTransferModeChangeReport PortTransferModeChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortTransferModeChangeReport, PortTransferModeChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortTypeAutoChangeModeReport(PortTypeAutoChangeModeReport PortTypeAutoChangeModeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortTypeAutoChangeModeReport, PortTypeAutoChangeModeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }
        public static void SendToWebSocketPortTypeChangeReport(PortTypeChangeReport PortTypeChangeReport)
        {
            string WebSocketMessageStr = JsonFormat(PortTypeChangeReport, PortTypeChangeReport.TYPE);
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr);
        }

        //状态推送
        //public static string BCInforamtionReportstr = "";
        public static void SendBCInforamtionReport(BCInforamtionReport data)
        {
            bool needlog = true;
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();

            #region Handler
            #endregion
            try
            {
                #region Body
                WebSocketMessageStr.body = data;
                #endregion
                #region result;
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "推送成功！"
                };
                #endregion

                //string bodydata = WebSocketMessageStr.JsonSerializer();
                //if (BCInforamtionReportstr == bodydata)
                //{
                    needlog = false;
                //}
                //BCInforamtionReportstr = bodydata;
            }
            catch (Exception ex)
            {
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "1",
                    returnMessageEN = "Operation failed !",
                    returnMessageCH = "推送失败！"
                };
            }
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "BCInforamtionReport",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = "report",
                uuid = "",
                userName = "AllDataUpdate"
            };
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr.JsonSerializer(), needlog);
        }

        public static void SendEquipmentInforamtionReport(AllData AllData)
        {
            WebSocketMessage WebSocketMessageStr = new WebSocketMessage();
            #region Handler
            WebSocketMessageStr.header = new WebSocketHeader()
            {
                messageName = "BCEquipmentInforamtionReport",
                transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                inboxName = "report",
                uuid = "",
                userName = "admin"
            };
            #endregion
            try
            {
                #region Body
                WebSocketMessageStr.body = AllData;
                #endregion
                #region result;
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "0",
                    returnMessageEN = "Operation sucessful !",
                    returnMessageCH = "推送成功！"
                };
                #endregion
            }
            catch (Exception ex)
            {
                WebSocketMessageStr.result = new WebSocketResult()
                {
                    returnCode = "1",
                    returnMessageEN = "Operation failed !",
                    returnMessageCH = "推送失败！"
                };
            }
            WebSocketHandler.Current.SendToWebSocket(WebSocketMessageStr.JsonSerializer());
        }

    }
}
