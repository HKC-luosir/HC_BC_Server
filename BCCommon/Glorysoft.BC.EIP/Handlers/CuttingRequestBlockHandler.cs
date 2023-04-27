using System.Linq;
using Glorysoft.BC.EIP.Common;
using Glorysoft.BC.Entity;
using System;

namespace Glorysoft.BC.EIP.Handlers
{
    public class CuttingRequestBlockHandler : AbstractEventHandler
    {
        public CuttingRequestBlockHandler(IPLCContext context)
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
                var eqpName = args.Name;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ CuttingRequestBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                CutPanelList data = new CutPanelList();
                data.JobID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.JobID);
                data.LotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                data.SlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                int ScriberCount = 0;
                String sScriberCount = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.ScriberCount);
                int.TryParse(sScriberCount, out ScriberCount);
                data.ScriberCount = ScriberCount;
                if (data.ScriberCount > 0)
                {
                    for (int i = 1; i <= data.ScriberCount; i++)
                    {
                        CutPanelInfo datadetail = new CutPanelInfo();
                        datadetail.CutPanelID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "CutPanel#" + i.ToString() + "ID");
                        datadetail.QPanelCode = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "QPanel#" + i.ToString() + "Code");
                        datadetail.ScriberModuleType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "Scriber#" + i.ToString() + "ModuleType");
                        datadetail.CutPanelLotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "CutPanel#" + i.ToString() + "LotSequenceNumber");
                        datadetail.CutPanelSlotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, "CutPanel#" + i.ToString() + "SlotSequenceNumber");
                        data.PanelInfos.Add(datadetail);
                    }
                }

                logicService.CuttingRequest(oEQP, data, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CuttingRequestBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}