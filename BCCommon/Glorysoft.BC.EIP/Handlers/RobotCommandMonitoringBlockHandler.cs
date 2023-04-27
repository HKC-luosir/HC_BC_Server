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
    public class RobotCommandMonitoringBlockHandler : AbstractEventHandler
    {
        public RobotCommandMonitoringBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ RobotCommandMonitoringBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} begin", CurrentThread, data.EQPName, this.GetType().Name));
                var commandSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CommandSequenceNumber);
                var firstRCMD = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstRCMD);
                var firstArmNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstArmNumber);
                var firstGetPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstGetPosition);
                var firstPutPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstPutPosition);
                var firstGetSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstGetSlotNumber);
                var firstPutSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstPutSlotNumber);
                var firstSuCIMommand = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstSuCIMommand);
                var firstGetSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstGetSlotPosition);
                var firstPutSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FirstPutSlotPosition);

                var secondRCMD = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondRCMD);
                var secondArmNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondArmNumber);
                var secondGetPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondGetPosition);
                var secondPutPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondPutPosition);
                var secondGetSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondGetSlotNumber);
                var secondPutSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondPutSlotNumber);
                var secondSuCIMommand = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondSuCIMommand);
                var secondGetSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondGetSlotPosition);
                var secondPutSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SecondPutSlotPosition);

                var thirdRCMD = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdRCMD);
                var thirdArmNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdArmNumber);
                var thirdGetPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdGetPosition);
                var thirdPutPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdPutPosition);
                var thirdGetSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdGetSlotNumber);
                var thirdPutSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdPutSlotNumber);
                var thirdSuCIMommand = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdSuCIMommand);
                var thirdGetSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdGetSlotPosition);
                var thirdPutSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ThirdPutSlotPosition);

                var fourthRCMD = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthRCMD);
                var fourthArmNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthArmNumber);
                var fourthGetPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthGetPosition);
                var fourthPutPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthPutPosition);
                var fourthGetSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthGetSlotNumber);
                var fourthPutSlotNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthPutSlotNumber);
                var fourthSuCIMommand = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthSuCIMommand);
                var fourthGetSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthGetSlotPosition);
                var fourthPutSlotPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.FourthPutSlotPosition);
                //BCLog.Debug(string.Format("[HandlerName:{2}] [Thread:{0}] UnitName:{1} end", CurrentThread, data.EQPName, this.GetType().Name));
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ {0}:{1} ,Error:{2} +++", MethodBase.GetCurrentMethod().Name
                , args.Message.EQPName, ex.ToString());
            }
        }
    }
}
