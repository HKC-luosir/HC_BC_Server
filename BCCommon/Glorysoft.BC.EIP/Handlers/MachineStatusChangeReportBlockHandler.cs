using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Glorysoft.BC.EIP.Handlers
{

    public class MachineStatusChangeReportBlockHandler : AbstractEventHandler
    {
        public MachineStatusChangeReportBlockHandler(IPLCContext context)
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
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ MachineStatusChangeReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var MachineStatus = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MachineStatus);
                var MachinestatusReasonCode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MachinestatusReasonCode);
                //var AlarmID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.AlarmID);
                //var Unitstatus#1 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.Unitstatus#1);
                ConcurrentDictionary<int,string> UnitList = new ConcurrentDictionary<int, string>();
                foreach (var item in plcmsg.ItemCollection)
                {
                    // key Parameter1ID Value1
                    if (item.Key.Contains("Unitstatus#"))
                    {
                        int no = Convert.ToInt32(item.Key.Replace("Unitstatus#", ""));
                        var status = item.Value.ToString();
                        UnitList.TryAdd(no, status);
                    }
                }

                logicService.MachineStatusChangeReport(oEQP, MachineStatus, MachinestatusReasonCode, UnitList, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ MachineStatusChangeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
