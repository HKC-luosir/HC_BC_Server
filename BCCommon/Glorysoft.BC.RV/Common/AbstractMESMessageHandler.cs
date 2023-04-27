using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVMessage;
using Glorysoft.BC.Db.Contract;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Logic.Contract;
using System.Collections.Generic;
using Glorysoft.BC.RV.RVService;
using Glorysoft.BC.RV.Entity;

namespace Glorysoft.BC.RV.Common
{
    public abstract class AbstractMESMessageHandler
    {
        private readonly ITibcoContext context;
        protected readonly HostInfo HostInfo = HostInfo.Current;
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly ITibcoRVService mesService = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected AbstractMESMessageHandler(ITibcoContext context)
        {
            this.context = context;
        }

        public ITibcoContext Context
        {
            get
            {
                return context;
            }
        }

        public abstract void Execute(RVData rvMessage);
    }
}