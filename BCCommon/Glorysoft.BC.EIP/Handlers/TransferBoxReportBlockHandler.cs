using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{

    public class TransferBoxReportBlockHandler : AbstractEventHandler
    {
        public TransferBoxReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ TransferBoxReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var materialTypeStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialType);
                var materialQtyStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialQty);
                var materialCurrentQtyStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialCurrentQty);
                var reportOptionStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ReportOption);
                var portNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortNumber);


                //字段转Int
                var materialType = 0;
                var materialQty = 0;
                var materialCurrentQty = 0;
                var reportOption = 0;
                var portNumber = 0;


                int.TryParse(materialTypeStr, out materialType);
                int.TryParse(materialQtyStr, out materialQty);
                int.TryParse(materialCurrentQtyStr, out materialCurrentQty);
                int.TryParse(reportOptionStr, out reportOption);
                int.TryParse(portNumberStr, out portNumber);


                logicService.TransferBoxReport(oEQP, materialType, materialQty, materialCurrentQty, reportOption, portNumber, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ TransferBoxReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
