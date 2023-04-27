using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;

namespace Glorysoft.BC.EIP.Handlers
{

    public class PortStatusChangeReportHandler : AbstractEventHandler
    {
        public PortStatusChangeReportHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ PortStatusChangeReportHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var portStatusStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.PortStatus);
                var lotSequenceNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var cassetteIDBoxID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CassetteIDBoxID);
                var jobCountInCassetteStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobCountInCassette);
                var operatorID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.OperatorID);
                var jobExistenceSlot1 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot1);
                var jobExistenceSlot2 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot2);
                var jobExistenceSlot3 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot3);
                var jobExistenceSlot4 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot4);
                var jobExistenceSlot5 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot5);
                var jobExistenceSlot6 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot6);
                var jobExistenceSlot7 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot7);
                var jobExistenceSlot8 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot8);
                var jobExistenceSlot9 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot9);
                var jobExistenceSlot10 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot10);
                var jobExistenceSlot11 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot11);
                var jobExistenceSlot12 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot12);
                var jobExistenceSlot13 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot13);
                var jobExistenceSlot14 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot14);
                var jobExistenceSlot15 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobExistenceSlot15);
                var jobExistenceSlot = jobExistenceSlot1 + jobExistenceSlot2 + jobExistenceSlot3 + jobExistenceSlot4 + jobExistenceSlot5 + jobExistenceSlot6 + jobExistenceSlot7 + jobExistenceSlot8 + jobExistenceSlot9 + jobExistenceSlot10 + jobExistenceSlot11 + jobExistenceSlot12 + jobExistenceSlot13 + jobExistenceSlot14 + jobExistenceSlot15;
                var loadingCassetteTypeStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LoadingCassetteType);

                //字段转Int
                var portStatus = 0;
                var lotSequenceNumber = 0;
                var jobCountInCassette = 0;
                var loadingCassetteType = 0;

                int.TryParse(portStatusStr, out portStatus);
                int.TryParse(lotSequenceNumberStr, out lotSequenceNumber);
                int.TryParse(jobCountInCassetteStr, out jobCountInCassette);
                int.TryParse(loadingCassetteTypeStr, out loadingCassetteType);

                portService.PortStatusChangeReport(oEQP, i, portStatus, lotSequenceNumber, cassetteIDBoxID, jobCountInCassette, operatorID, jobExistenceSlot, loadingCassetteType, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ PortStatusChangeReportHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
