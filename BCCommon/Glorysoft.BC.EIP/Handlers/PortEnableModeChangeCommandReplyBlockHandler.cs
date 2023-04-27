﻿using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{

    public class PortEnableModeChangeCommandReplyBlockHandler : AbstractEventHandler
    {
        public PortEnableModeChangeCommandReplyBlockHandler(IPLCContext context)
           : base(context)
        {
        }

        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var i = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ PortEnableModeChangeCommandReplyBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var EnableModeReturnCode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.EnableModeReturnCode);
                portService.PortEnableModeChangeCommandReply(oEQP, EnableModeReturnCode, i, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PortEnableModeChangeCommandReplyBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
