using System;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.BC.Entity;

namespace Glorysoft.BC.SECS.Service
{
    public class S9F1Handler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                var commack = 1;
                string rtMsg = "";
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(S9F1Handler)}", e);
            }
            return;
        }
    }
}
