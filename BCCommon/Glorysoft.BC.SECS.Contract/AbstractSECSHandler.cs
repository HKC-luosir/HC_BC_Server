using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.SECSwell;
using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Glorysoft.BC.SECS.Contract
{
    public abstract class AbstractSECSHandler
    {
        protected ILog SECSLogger = LogHelper.SECSLog;
        protected static readonly ISECSCommandService secsCmd = CommonContexts.ResolveInstance<ISECSCommandService>();

        protected AbstractSECSHandler()
        {
        }

        public ConcurrentDictionary<string, UInt64> Dataids = new ConcurrentDictionary<string, UInt64>();
        protected SECSMessage GetReplyData(ISECSContext context, SECSMessage msg)
        {
            var trans = context.GetTransaction(msg.Stream, msg.Function + 1, 0, $"S{msg.Stream}F{msg.Function + 1}_H");
            if (trans == null)
            {
                return null;
            }
            if (!(trans.OrignalMessage is SECSTransaction Data))
            {
                return null;
            }
            var primarymsg = Data.Primary;
            return primarymsg;
        }
        protected string GetDATAID(string Command)
        {
            Command = "Common";
            UInt64 i = 1;
            if (!Dataids.ContainsKey(Command))
            {
                Dataids.TryAdd(Command, i);
            }
            else
            {
                i = Dataids[Command] + 1;
                Dataids[Command] = i;
            }
            return i.ToString();
        }
    }
}