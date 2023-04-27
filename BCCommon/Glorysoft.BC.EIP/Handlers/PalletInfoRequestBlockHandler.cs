using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class PalletInfoRequestBlockHandler : AbstractEventHandler
    {
        public PalletInfoRequestBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ PalletInfoRequestBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var palletID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PalletID);
                var palletStatus = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PalletStatus);
                var boxQTY = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.BoxQTY);
                var palletType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PalletType);
                var portNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortNumber);

                var portNo = 0;
                int.TryParse(portNumberStr, out portNo);

                logicService.PalletInfoRequest(oEQP, palletID, palletStatus, boxQTY, palletType, portNo, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PalletInfoRequestBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}