using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{

    public class PortBoxGroupPortStatusReportBlockHandler : AbstractEventHandler
    {
        public PortBoxGroupPortStatusReportBlockHandler(IPLCContext context)
           : base(context)
        {
        }

        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{2}][{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName, args.Message.TransactionID);
                var i = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ PortBoxGroupPortStatusReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var PortStatus = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortStatus);
                var PortType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortType);
                var PortCassetteType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortCassetteType);
                var PortTransferMode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortTransferMode);
                List<string> BoxList = new List<string>();
                for (int j = 1; j <= 8; j++)
                {
                    var boxid = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "BoxID" + j.ToString());
                    if(!string.IsNullOrEmpty(boxid))
                    BoxList.Add(boxid);
                }
                portService.PortBoxGroupPortStatusReport(oEQP, i, Convert.ToInt32(PortStatus), PortType, Convert.ToInt32(PortCassetteType), PortTransferMode, BoxList, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PortBoxGroupPortStatusReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
