using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class CurrentRecipeNumberChangeReportBlockHandler : AbstractEventHandler
    {
        public CurrentRecipeNumberChangeReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ CurrentRecipeNumberChangeReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var currentRecipeNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.CurrentRecipeNumber);
                var recipeVersionTimeYear = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeYear);
                var recipeVersionTimeMonth = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeMonth).PadLeft(2, '0');
                var recipeVersionTimeDay = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeDay).PadLeft(2, '0');
                var recipeVersionTimeHour = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeHour).PadLeft(2, '0');
                var recipeVersionTimeMinute = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeMinute).PadLeft(2, '0');
                var recipeVersionTimeSecond = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeSecond).PadLeft(2, '0');
                var unitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);

                int currentRecipeNumber = 0;
                int.TryParse(currentRecipeNumberStr, out currentRecipeNumber);

                var recipeVersion = $"{recipeVersionTimeYear}{recipeVersionTimeMonth}{recipeVersionTimeDay}" +
                    $"{recipeVersionTimeHour}{recipeVersionTimeMinute}{recipeVersionTimeSecond}";
                logicService.CurrentRecipeNumberChangeReport(oEQP, currentRecipeNumber, recipeVersion, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ CurrentRecipeNumberChangeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}