using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;

namespace GlorySoft.BC.WebSocket.Common
{

    public abstract class AbstractWebSocketMessageHandlercs
    {
        protected readonly log4net.ILog Logger = LogHelper.WebSocketLog;

        protected static readonly SendOPIMessage OPI = new SendOPIMessage();
        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly ITibcoRVService rvService = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();

        //public abstract void Execute(object obj);

        //public abstract void HandleResponse(PCIMEventArgs arg);

    }
}
