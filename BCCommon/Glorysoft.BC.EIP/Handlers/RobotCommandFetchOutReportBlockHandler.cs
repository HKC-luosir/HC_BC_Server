using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Reflection;

namespace Glorysoft.BC.EIP.Handlers
{
    public class RobotCommandFetchOutReportBlockHandler : AbstractEventHandler
    {
        public RobotCommandFetchOutReportBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Name;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ RobotCommandFetchOutReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} begin", CurrentThread, data.EQPName, this.GetType().Name));
                var SequenceNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CommandSequenceNumber);
                var RCMD = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RCMDRobotCommand);
                var ArmNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ArmNumber);
                var GetPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.GetPosition);
                var PutPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PutPosition);
                var GetSlotNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.GetSlotNumber);
                var PutSlotNo = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PutSlotNumber);
                var SubCommand = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SuCIMommand);
                var GetSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.GetSlotPosition);
                var PutSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PutSlotPosition);
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} end", CurrentThread, data.EQPName, this.GetType().Name));
                robotService.RobotCommandFetchOutReport(eqpName, SequenceNo);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ {0}:{1} ,Error:{2} +++", MethodBase.GetCurrentMethod().Name
                , args.Message.EQPName, ex.ToString());
            }
        }
    }
}
