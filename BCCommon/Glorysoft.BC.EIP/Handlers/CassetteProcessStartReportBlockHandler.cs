﻿using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{

    public class CassetteProcessStartReportBlockHandler : AbstractEventHandler
    {
        public CassetteProcessStartReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ CassetteProcessStartReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var lotSequenceNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var startOption = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.StartOption);
                var cassetteIDBoxID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CassetteIDBoxID);


                //字段转Int
                var lotSequenceNumber = 0;


                int.TryParse(lotSequenceNumberStr, out lotSequenceNumber);


                portService.CassetteProcessStartReport(oEQP, i, lotSequenceNumber, startOption, cassetteIDBoxID, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CassetteProcessStartReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
