using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class AbnormalCodeReportBlockHandler : AbstractEventHandler
    {
        public AbnormalCodeReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ AbnormalCodeReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var AbnormalFlag1 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag1);
                var AbnormalFlag2 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag2);
                var AbnormalFlag3 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag3);
                var AbnormalFlag4 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag4);
                var AbnormalFlag5 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag5);
                var AbnormalFlag6 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag6);
                var AbnormalFlag7 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag7);
                var AbnormalFlag8 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AbnormalFlag8);

                logicService.AbnormalCodeReport(oEQP, LotSequenceNumber, SlotSequenceNumber, AbnormalFlag1, AbnormalFlag2, AbnormalFlag3, AbnormalFlag4, AbnormalFlag5, AbnormalFlag6, AbnormalFlag7, AbnormalFlag8, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ AbnormalCodeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}