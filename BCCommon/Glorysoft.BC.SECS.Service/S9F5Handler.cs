﻿using System;
using Glorysoft.Auto.Contract.SECS;
using Glorysoft.BC.SECS.Contract;
using Glorysoft.BC.Entity;

namespace Glorysoft.BC.SECS.Service
{
    public class S9F5Handler : AbstractSECSHandler, ISECSMessageHandler
    {
        public void Execute(ISECSContext context, ESECSEventType eventType, SECSMessageObject data)
        {
            try
            {
                var commack = 3;
                string rtMsg = "";
            }
            catch (Exception e)
            {
                SECSLogger.Error($"{context.Name}, MessageName: {nameof(S9F5Handler)}", e);
            }
            return;
        }
    }
}
