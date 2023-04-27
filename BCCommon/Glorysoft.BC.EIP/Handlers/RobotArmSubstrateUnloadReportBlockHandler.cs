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
    public class RobotArmSubstrateUnloadReportBlockHandler : AbstractEventHandler
    {
        public RobotArmSubstrateUnloadReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ RobotArmSubstrateUnloadReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} begin", CurrentThread, data.EQPName, this.GetType().Name));
                var upperArm1LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UpperArm1LotSequenceNumber);
                var upperArm1SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UpperArm1SlotSequenceNumber);
                var upperArm2LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UpperArm2LotSequenceNumber);
                var upperArm2SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UpperArm2SlotSequenceNumber);
                var lowerArm1LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LowerArm1LotSequenceNumber);
                var lowerArm1SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LowerArm1SlotSequenceNumber);
                var lowerArm2LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LowerArm2LotSequenceNumber);
                var lowerArm2SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LowerArm2SlotSequenceNumber);
                var armNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ArmNumber);
                var currentPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CurrentPosition);
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} end", CurrentThread, data.EQPName, this.GetType().Name));

                //var cmdresult = new RobotCommandResult();

                //cmdresult.ExecuteCommand = new RobotCommand();
                //cmdresult.ExecuteCommand.Name = oEQP.UnitName;

                //cmdresult.LowerCassetteSequenceNo1 = int.Parse(lowerArm1LotSequenceNumber);
                //cmdresult.LowerSlotSequenceNo1 = int.Parse(lowerArm1SlotSequenceNumber);
                //cmdresult.LowerCassetteSequenceNo2 = int.Parse(lowerArm2LotSequenceNumber);
                //cmdresult.LowerSlotSequenceNo2 = int.Parse(lowerArm2SlotSequenceNumber);
                //cmdresult.UpperCassetteSequenceNo1 = int.Parse(upperArm1LotSequenceNumber);
                //cmdresult.UpperSlotSequenceNo1 = int.Parse(upperArm1SlotSequenceNumber);
                //cmdresult.UpperCassetteSequenceNo2 = int.Parse(upperArm2LotSequenceNumber);
                //cmdresult.UpperSlotSequenceNo2 = int.Parse(upperArm2SlotSequenceNumber);

                //robotService.RobotArmMonitoringReport(oEQP.UnitName, cmdresult);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ {0}:{1} ,Error:{2} +++", MethodBase.GetCurrentMethod().Name
                , args.Message.EQPName, ex.ToString());
            }
        }
    }
}
