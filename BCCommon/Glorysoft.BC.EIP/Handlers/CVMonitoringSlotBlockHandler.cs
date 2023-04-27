using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{

    public class CVMonitoringSlotBlockHandler : AbstractEventHandler
    {
        public CVMonitoringSlotBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ CVMonitoringSlotBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var cv1LotNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CV1LotNumber);
                var cv1SlotNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CV1SlotNumber);
                var cv2LotNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CV2LotNumber);
                var cv2SlotNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CV2SlotNumber);


                //字段转Int
                var cv1LotNumber = 0;


                int.TryParse(cv1LotNumberStr, out cv1LotNumber);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CVMonitoringSlotBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
