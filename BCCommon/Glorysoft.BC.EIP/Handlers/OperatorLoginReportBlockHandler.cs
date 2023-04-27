using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class OperatorLoginReportBlockHandler : AbstractEventHandler
    {
        public OperatorLoginReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ OperatorLoginReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var OperatorID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.OperatorID);
                var TouchPanelNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.TouchPanelNumber);
                var ReportOption = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ReportOption);

                var loginLogoutTimeYear = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoginLogoutTimeYear);
                var loginLogoutTimeMonth = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoginLogoutTimeMonth).PadLeft(2, '0');
                var loginLogoutTimeDay = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoginLogoutTimeDay).PadLeft(2, '0');
                var loginLogoutTimeHour = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoginLogoutTimeHour).PadLeft(2, '0');
                var loginLogoutTimeMinute = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoginLogoutTimeMinute).PadLeft(2, '0');
                var loginLogoutTimeSecond = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoginLogoutTimeSecond).PadLeft(2, '0');

                var loginLogoutTime = $"{loginLogoutTimeYear}{loginLogoutTimeMonth}{loginLogoutTimeDay}{loginLogoutTimeHour}{loginLogoutTimeMinute}{loginLogoutTimeSecond}";

                logicService.OperatorLoginReport(oEQP, OperatorID, TouchPanelNumber, ReportOption, loginLogoutTime, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ OperatorLoginReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}