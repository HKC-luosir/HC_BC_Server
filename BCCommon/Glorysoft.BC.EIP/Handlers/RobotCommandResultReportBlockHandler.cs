using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity;
using System.Reflection;

namespace Glorysoft.BC.EIP.Handlers
{
    public class RobotCommandResultReportBlockHandler : AbstractEventHandler
    {
        public RobotCommandResultReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ RobotCommandResultReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} begin", CurrentThread, data.EQPName, this.GetType().Name));
                var commandSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CommandSequenceNumber);
                var firstCommandResult = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstCommandResult);
                var firstCommandResultComment = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstCommandResultComment);
                var secondCommandResult = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondCommandResult);
                var secondCommandResultComment = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondCommandResultComment);
                var thirdCommandResult = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdCommandResult);
                var thirdCommandResultComment = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdCommandResultComment);
                var fourthCommandResult = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthCommandResult);
                var fourthCommandResultComment = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthCommandResultComment);
                var currentPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CurrentPosition);
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} end", CurrentThread, data.EQPName, this.GetType().Name));
                robotService.CommandExecuteResultReport(oEQP.UnitName, commandSequenceNumber);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ {0}:{1} ,Error:{2} +++", MethodBase.GetCurrentMethod().Name
                , args.Message.EQPName, ex.ToString());
            }
        }
    }
}
