
using Fleck;
using Glorysoft.BC.Entity;
using GlorySoft.BC.WebSocket.MessageHandler;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
using Newtonsoft.Json;

namespace GlorySoft.BC.WebSocket
{
    public class WebSocketHandler
    {
        private WebSocketServer server;
        public List<IWebSocketConnection> AllSockets = new List<IWebSocketConnection>();
        protected readonly ILog Logger = LogHelper.WebSocketLog;
        string LocalIP = "";
        string LocalPort = "8181";
       
        private static readonly object syncRoot = new object();

        private static readonly Lazy<WebSocketHandler> webSocket = new Lazy<WebSocketHandler>(() => new WebSocketHandler());
        public static WebSocketHandler Current
        {
            get
            {
                return webSocket.Value;
            }
        }
        public void Start(string ip,string port)
        {
            try
            {
                LocalIP = ip;
                LocalPort = port;
                var connstr = "ws://" + LocalIP + ":" + LocalPort + "/";
                //server = new WebSocketServer("ws://127.0.0.1:8181/");
                server = new WebSocketServer(connstr);
                server.Start(socket =>
                {
                    socket.OnOpen = () =>
                    {
                        WebClientOnOpen(socket);
                    };
                    socket.OnClose = () =>
                    {
                        WebClientOnClose(socket);
                    };
                    socket.OnMessage = message =>
                    {
                        WebClientOnMessage(socket, message);
                    };
                    socket.OnError = exception =>
                    {
                        WebClientOnError(socket, exception);
                    };
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void Stop()
        {
            try
            {
                server.Dispose();
            }
            catch (Exception ee)
            {
                LogHelper.WebSocketLog.Error(ee);
            }

        }

        private void WebClientOnOpen(IWebSocketConnection socket)
        {
            try
            {
                AllSockets.Add(socket);
                HostInfo.Current.EQPInfo.OPIConnect = true;
                //PCIMInfo.GetInstance().MCimConnect = true;
            }
            catch (Exception ee)
            {
                LogHelper.WebSocketLog.Error(ee);
            }

            //if (ClientOnOpen != null)
            //    ClientOnOpen(socket);
            //EquipmentStatusEvent e = new EquipmentStatusEvent();
            //e.EQPSTATUS = "RUN";
            //TestResultEventReport e = new TestResultEventReport();
            //e.CSTID = "CSTID";
            //e.DEFECTCODE.Add("PB01");
            //e.DEFECTCODE.Add("PB02");
            //string s = e.JsonSerializer();
            //AllSockets.ForEach(so => so.Send(s));
        }
        private void WebClientOnClose(IWebSocketConnection socket)
        {
            try
            {
                AllSockets.Remove(socket);
                //if (ClientOnClose != null)
                //    ClientOnClose(socket);
                if (AllSockets.Count == 0)
                {
                    HostInfo.Current.EQPInfo.OPIConnect = false;
                    //PCIMInfo.GetInstance().MCimConnect = false;
                }
            }
            catch (Exception ee)
            {
                LogHelper.WebSocketLog.Error(ee);
            }

        }
        private void WebClientOnMessage(IWebSocketConnection socket, string message)
        {
            try
            {
                Logger.Info(message);
                //if (ClientOnMessage != null)
                //    ClientOnMessage(socket, message);
                JavaScriptSerializer js = new JavaScriptSerializer();
                var type = GetJsonType(message);
                //var begin = (message.IndexOf("\"body\"") + 7);
                //var end = (message.IndexOf(",\"result\":") - begin);
                var begin = (message.IndexOf("\"body\"") + 8);
                var end = (message.IndexOf(",\"result\":") - begin - 1);
                var body = message.Substring(begin, end);
                //BaseClass baseClass = js.Deserialize<BaseClass>(message);                
                switch (type)
                {
                    case "EQPInfoRequest":
                        EQPInfoRequest EQPInfoRequest = js.Deserialize<EQPInfoRequest>(body);
                        EQPInfoRequestHandler EQPInfoRequestHandler = new EQPInfoRequestHandler();
                        EQPInfoRequestHandler.Execute(EQPInfoRequest);                        
                        break;
                    case "UnitInfoRequest":
                        UnitInfoRequest UnitInfoRequest = js.Deserialize<UnitInfoRequest>(body);
                        UnitInfoRequestHandler UnitInfoRequestHandler = new UnitInfoRequestHandler();
                        UnitInfoRequestHandler.Execute(UnitInfoRequest);
                        break;
                    case "PortInfoRequest":
                        PortInfoRequest PortInfoRequest = js.Deserialize<PortInfoRequest>(body);
                        PortInfoRequestHandler PortInfoRequestHandler = new PortInfoRequestHandler();
                        PortInfoRequestHandler.Execute(PortInfoRequest);
                        break;
                    case "GlassInfoRequest":
                        GlassInfoRequest GlassInfoRequest = js.Deserialize<GlassInfoRequest>(body);
                        GlassInfoRequestHandler GlassInfoRequestHandler = new GlassInfoRequestHandler();
                        GlassInfoRequestHandler.Execute(GlassInfoRequest);
                        break;
                    case "TrayInfoRequest":
                        TrayInfoRequest TrayInfoRequest = js.Deserialize<TrayInfoRequest>(body);
                        TrayInfoRequestHandler TrayInfoRequestHandler = new TrayInfoRequestHandler();
                        TrayInfoRequestHandler.Execute(TrayInfoRequest);
                        break;
                    case "RecipeInfoRquest":
                        RecipeInfoRquest RecipeInfoRquest = js.Deserialize<RecipeInfoRquest>(body);
                        RecipeInfoRquestHandler RecipeInfoRquestHandler = new RecipeInfoRquestHandler();
                        RecipeInfoRquestHandler.Execute(RecipeInfoRquest);
                        break;
                    case "RobotControlCommand":
                        RobotControlCommand RobotControlCommand = js.Deserialize<RobotControlCommand>(body);
                        RobotControlCommandHandler RobotControlCommandHandler = new RobotControlCommandHandler();
                        RobotControlCommandHandler.Execute(RobotControlCommand);
                        break;
                    case "LogIn":
                        bcUser LogIn = js.Deserialize<bcUser>(body);
                        LogInHandler LogInHandler = new LogInHandler();
                        LogInHandler.Execute(LogIn);
                        break;
                    case "PortInfoSetting":
                        PortInfoSetting PortInfoSetting = js.Deserialize<PortInfoSetting>(body);
                        PortInfoSettingHandler PortInfoSettingHandler = new PortInfoSettingHandler();
                        PortInfoSettingHandler.Execute(PortInfoSetting);
                        break;
                    case "UnitInfoSetting":
                        UnitInfoSetting UnitInfoSetting = js.Deserialize<UnitInfoSetting>(body);
                        UnitInfoSettingHandler UnitInfoSettingHandler = new UnitInfoSettingHandler();
                        UnitInfoSettingHandler.Execute(UnitInfoSetting);
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }
        private void WebClientOnError(IWebSocketConnection socket, Exception ex)
        {
            //if (ClientOnError != null)
            //    ClientOnError(socket, ex);
            Logger.Error(ex);
        }
        public string GetJsonType(string json)
        {
            try
            {

                WebSocketMessage rt = JsonConvert.DeserializeObject<WebSocketMessage>(json);
                return rt.header.messageName;
            }
            catch (Exception ee)
            {
                LogHelper.WebSocketLog.Error(ee);
                return "";
            }


        }
        public void SendToWebSocket(string s, bool needlog = true)
        {
            try
            {
                AllSockets.ForEach(so => so.Send(s));
                if (needlog)
                    LogHelper.WebSocketLog.Info(s);
            }
            catch (Exception ee)
            {
                LogHelper.WebSocketLog.Error(ee);
            }
        }
    }
}
