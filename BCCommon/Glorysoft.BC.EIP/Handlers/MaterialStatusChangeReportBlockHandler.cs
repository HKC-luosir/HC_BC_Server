using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{

    public class MaterialStatusChangeReportBlockHandler : AbstractEventHandler
    {
        public MaterialStatusChangeReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ MaterialStatusChangeReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var MaterialStatus = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialStatus);
                var MaterialID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialID);
                var MaterialType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialType);
                var UnitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);
                var SlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotNumber);
                var MaterialCount = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialCount);
                var UnloadingCode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnloadingCode);

                logicService.MaterialStatusChangeReport(oEQP, i, MaterialStatus, MaterialID, MaterialType, UnitNumber, SlotNumber, MaterialCount, UnloadingCode, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ MaterialStatusChangeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
