using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Contract.PLC;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Logic.Contract;
using log4net;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Linq;
using System.Collections;
using System.Collections.ObjectModel;
using System.Threading;
using Glorysoft.BC.Entity.RVEntity;
using System.Text;
using Glorysoft.BC.Entity.WebSocketEntity;

namespace Glorysoft.BC.EQP.Contract
{
    public abstract class AbstractEventHandler : IPLCEventHandler
    {
        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected ILog BCLog = LogHelper.BCLog;
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        
        protected static readonly ITibcoRVService rvCmd = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected static readonly IRobotService RobotService = CommonContexts.ResolveInstance<IRobotService>();
        protected static readonly IWebSocketService webSocketService = CommonContexts.ResolveInstance<IWebSocketService>();
        protected AbstractEventHandler()
        {
        }

        public abstract void Execute(IPLCContext context, PLCData data);

        protected Dictionary<int, string> dicEQPStatus = new Dictionary<int, string>
        {
            {1,"IDLE" },
            {2,"RUN" },
            {3,"DOWN" },
            {4,"PM" },
            {5,"MCHG" },
            {6,"E-TIME" }
        };

        protected Dictionary<string, string> dicPanelProcessStart = new Dictionary<string, string>
        {
            {"POLCleaner","POLCleaner" },
            {"LoaderCV","LoaderCV" },
            {"OLBCleaner","OLBCleaner" }
        };
        protected Dictionary<string, string> dicPanelSendOutProcessEnd = new Dictionary<string, string>
        {
            {"POLCleaner","POLCleaner" },
            {"PCBBonding","PCBBonding" },
            {"ASSY","ASSY" }
        };
        protected static Dictionary<int, string> dicPortType = new Dictionary<int, string>
        {
            {1,"PL" },
            {2,"PU" },
            {3,"PB" }
        };

        protected static Dictionary<int, string> dicNGType = new Dictionary<int, string>
        {
            {0,"" },
            {1,"TCP PANEL NG" },
            {2,"POL NG" },
            {3,"TCP NG" },
            {4,"PCB NG" }
        };

    }

}
