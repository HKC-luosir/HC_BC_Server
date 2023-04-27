﻿using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class BoxWeightCheckRequestBlockHandler : AbstractEventHandler
    {
        public BoxWeightCheckRequestBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = plcmsg.EQPName;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ BoxWeightCheckRequestBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var cassetteIDOrBoxID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CassetteIDOrBoxID);
                var boxWeight = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.BoxWeight);
                var unitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);


                logicService.BoxWeightCheckRequest(oEQP, cassetteIDOrBoxID, boxWeight, unitNumber, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ BoxWeightCheckRequestBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}