using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{

    public class BufferJobMonitoringBlockHandler : AbstractEventHandler
    {
        public BufferJobMonitoringBlockHandler(IPLCContext context)
           : base(context)
        {
        }

        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var index = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ BufferJobMonitoringBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                List<string> numbers = new List<string>();
                for (int i = 1; i < 11; i++)
                {
                    var lotNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, $"BufferSlot{i.ToString().PadLeft(3, '0')}LotNumber");
                    var slotNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, $"BufferSlot{i.ToString().PadLeft(3, '0')}SlotNumber");
                    if (slotNumberStr != "0")
                        numbers.Add(lotNumberStr + ";" + slotNumberStr);
                }
                logicService.BufferJobMonitoring(oEQP, numbers, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ BufferJobMonitoringBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
