using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.BC.RV.RVService;
using Glorysoft.BC.EQP.Contract;
using log4net;
using System;

namespace Glorysoft.BC.RV.Common
{
    public abstract class AbstractFDCMessageHandler
    {
        private readonly ITibcoContext context;
        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected ILog BCLog = LogHelper.BCLog;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly ITibcoRVService rvService = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        protected AbstractFDCMessageHandler(ITibcoContext context)
        {
            this.context = context;
        }
        protected int GetIntItemValue(string item)
        {
            try
            {
                if (string.IsNullOrEmpty(item.ToString())) return 0;

                return Convert.ToInt32(item);
            }
            catch (Exception ex)
            {
                //LogHelper.FDCLogger.Error(ex);
                return 0;
            }

        }
        public ITibcoContext Context
        {
            get
            {
                return context;
            }
        }

        public abstract void Execute(RVFDCMessage rvMessage);
    }
}