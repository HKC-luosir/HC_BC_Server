using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;

namespace Glorysoft.BC.EIP.Handlers
{
    public class RecipeChangeReportBlockHandler : AbstractEventHandler
    {
        public RecipeChangeReportBlockHandler(IPLCContext context)
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
                    LogHelper.EIPLog.ErrorFormat("+++ RecipeChangeReportBlockHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }

                var recipeChangeType = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeChangeType);
                var recipeVersionTimeYear = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeYear);
                var recipeVersionTimeMonth = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeMonth).PadLeft(2, '0');
                var recipeVersionTimeDay = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeDay).PadLeft(2, '0');
                var recipeVersionTimeHour = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeHour).PadLeft(2, '0');
                var recipeVersionTimeMinute = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeMinute).PadLeft(2, '0');
                var recipeVersionTimeSecond = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeVersionTimeSecond).PadLeft(2, '0');
                var operatorID = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.OperatorID);
                var unitNumber = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.UnitNumber);
                var recipeNumberStr = GetItemValue(args.Message.EventName, plcmsg.ItemCollection, PLCEventItem.RecipeNumber);

                int recipeNumber = 0;
                int.TryParse(recipeNumberStr, out recipeNumber);

                var recipeVersion = $"{recipeVersionTimeYear}{recipeVersionTimeMonth}{recipeVersionTimeDay}" +
                    $"{recipeVersionTimeHour}{recipeVersionTimeMinute}{recipeVersionTimeSecond}";
                List<Parameter> parameterList = new List<Parameter>();

                //#region 从Driver缓存中，获取parameter ID和Value 
                ////获取各Block对应数据
                //string[] datatype = new string[] { "INT", "SI", "FLOAT", "ASCII" };
                //foreach (var dtype in datatype)
                //{
                //    var tagName = "BC_EQToCIM_Parameter" + dtype + "_" + (oEQP.LocalNo.ToString().PadLeft(2, '0')) + "_01_00";
                //    var block = PLCContexts.Current.GetBlock(eqpName, tagName);
                //    if (block != null)
                //    {
                //        Item it = new Item();
                //        it.ITEMNAME = dtype;
                //        var blockname = "Parameter" + dtype + "FormatDataBlock";
                //        if (block.BlockCollection.ContainsKey(blockname))
                //        {
                //            var data = block.BlockCollection.FirstOrDefault(f => f.Key == blockname).Value;
                //            var itemCount = data.ItemCollection.Count;
                //            var itemList = data.ItemCollection.Keys.ToList();
                //            for (int i = 0; i < itemCount - 1; i = i + 2)
                //            {
                //                var idItem = itemList[i];
                //                var idValue = data.ItemCollection[idItem].Value;
                //                var valueItem = itemList[i + 1];
                //                var valueValue = data.ItemCollection[valueItem].Value;
                //                if (idValue != "0")
                //                {
                //                    Parameter parameter = new Parameter();
                //                    parameter.ParameterName = idValue;
                //                    parameter.ParameterValue = valueValue;
                //                    parameterList.Add(parameter);
                //                }
                //            }
                //        }
                //    }
                //}
                //#endregion

                logicService.RecipeChangeReport(oEQP, recipeNumber, recipeChangeType, recipeVersion, unitNumber, operatorID, parameterList, txid);
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ RecipeChangeReportBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}