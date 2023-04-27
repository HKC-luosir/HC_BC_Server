using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Handlers
{

    public class CellMaterialAssemblyReportBlockHandler : AbstractEventHandler
    {
        public CellMaterialAssemblyReportBlockHandler(IPLCContext context)
           : base(context)
        {
        }

        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var index = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = args.Message.EQPName;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ CellMaterialAssemblyReportBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                var lotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.LotSequenceNumber);
                var slotSequenceNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.SlotSequenceNumber);
                var materialCountStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialCount);
                int materialCount = 5;
                //int.TryParse(materialCountStr, out materialCount);
                List<MaterialInfo> materialList = new List<MaterialInfo>();
                for (int i = 1; i < materialCount + 1; i++)
                {
                    var materialID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialID + i);
                    var materialType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialType + i);
                    var materialPosition = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialPosition + i);
                    var materialTarget = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialTarget + i);
                    var materialSource = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialSource + i);
                    var materialUseCount = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialUseCount + i);
                    if (!string.IsNullOrEmpty(materialID))
                    {
                        MaterialInfo materialInfo = new MaterialInfo();
                        materialInfo.MaterialID = materialID;
                        materialInfo.MaterialType = materialType;
                        materialInfo.MaterialPosition = materialPosition;
                        materialInfo.MaterialTarget = materialTarget;
                        materialInfo.MaterialSource = materialSource;
                        materialInfo.MaterialUseCount = materialUseCount;
                        materialList.Add(materialInfo);
                    }
                }
                //var MaterialID1 = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.MaterialID1);

                logicService.CellMaterialAssemblyReport(oEQP, lotSequenceNumber, slotSequenceNumber, materialList, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CellMaterialAssemblyReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}
