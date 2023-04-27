using System;
using System.Collections.Generic;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.BC.Entity.WebSocketEntity.WebSocketMessage;
namespace Glorysoft.BC.WebAPI.WebAPIHandler
{

    public abstract class AbstractWebAPIMessageHandlercs
    {
        protected static readonly log4net.ILog Logger = LogHelper.WebAPILog;        
        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly IEQPService eqpService = CommonContexts.ResolveInstance<IEQPService>();
        protected static readonly IPortService portService = CommonContexts.ResolveInstance<IPortService>();
        protected static readonly ITibcoRVService rvService = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected static IWebSocketService webSocketService = CommonContexts.ResolveInstance<IWebSocketService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        public static WebSocketMessage JsonFormat(Object obj, string messageName, WebSocketResult result)
        {
            var WebSocketMessage = new WebSocketMessage();
            try
            {
                var header = new WebSocketHeader()
                {
                    messageName = messageName,
                    transactionId = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    userId = "admin",
                };
                WebSocketMessage.header = header;
                WebSocketMessage.result = result;
                var body = new List<WebSocketBody>();
                var WebSocketBody = obj as WebSocketBody;
                body.Add(WebSocketBody);
                WebSocketMessage.body = body;
                //string WebSocketMessageStr = WebSocketMessage.JsonSerializer();
                return WebSocketMessage;
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return WebSocketMessage;
            }
            
        }

        public static WebSocketMessage JsonFormat(WebSocketHeader Header, Object Body, WebSocketResult Result)
        {
            var WebSocketMessage = new WebSocketMessage();
            try
            {
                WebSocketMessage.header = Header;
                WebSocketMessage.body = Body;
                WebSocketMessage.result = Result;
                return WebSocketMessage;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return WebSocketMessage;
            }

        }
        //public abstract void Execute(object obj);

        //public abstract void HandleResponse(PCIMEventArgs arg);

    }
}
