using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.Auto.Contract;

namespace Glorysoft.BC.EIP.Common
{
    internal interface IPLCEventHandler
    {
        void Execute(PLCEventArgs args);
        IPLCContext Context { get; }
    }

    public abstract class AbstractEventHandler : IPLCEventHandler
    {
        private readonly IPLCContext context;
        protected static readonly HostInfo HostInfo = HostInfo.Current;


        protected static readonly IRobotService robotService = CommonContexts.ResolveInstance<IRobotService>();
        protected static readonly IPortService portService = CommonContexts.ResolveInstance<IPortService>();
        protected static readonly ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        protected static readonly ITibcoRVService mesService = CommonContexts.ResolveInstance<ITibcoRVService>();
        protected static readonly IEQPService eqpService = CommonContexts.ResolveInstance<IEQPService>();
        protected static readonly IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
        protected AbstractEventHandler(IPLCContext context)
        {
            this.context = context;
        }

        public IPLCContext Context
        {
            get
            {
                return context;
            }
        }

        public abstract void Execute(PLCEventArgs args);
        protected string GetItemValue(string EventName, Dictionary<string, string> dict, string itemName)
        {
            if (dict.ContainsKey(itemName))
            {
                var a = dict[itemName].ToString().Trim();
                return a;
            }
            //if (!EventName.Contains("PanelGradeChange"))
            //{
            //    Glorysoft.BC.Entity.LogHelper.XmlLogger.ErrorFormat("[{0}] ItemName:{1} Not Exist Or Configuration Error,Please Check AddressMap!", EventName, itemName);
            //}
            return "";
        }
        protected static int FindInt(string msg)
        {
            int i = 0;
            string result = System.Text.RegularExpressions.Regex.Replace(msg, @"[^0-9]+", "");
            int.TryParse(result, out i);
            return i;
        }
        //matti 2022014
        protected JobDataInfo GetEQPJobData(string EventName, Dictionary<string, string> ItemCollection, string suffix)
        {
            JobDataInfo jobdata = new JobDataInfo();
            jobdata.PRODID = GetItemValue(EventName, ItemCollection, PLCEventItem.PRODID + suffix);
            jobdata.OperID = GetItemValue(EventName, ItemCollection, PLCEventItem.OperID + suffix);
            jobdata.LotID = GetItemValue(EventName, ItemCollection, PLCEventItem.LotID + suffix);
            jobdata.PPID1 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID2 + suffix);
            jobdata.PPID2 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID3 + suffix);
            jobdata.PPID3 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID4 + suffix);
            jobdata.PPID4 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID5 + suffix);
            jobdata.PPID5 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID6 + suffix);
            jobdata.PPID6 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID7 + suffix);
            jobdata.PPID7 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID8 + suffix);
            jobdata.PPID8 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID9 + suffix);
            jobdata.PPID9 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID10 + suffix);
            jobdata.PPID10 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID11 + suffix);
            jobdata.PPID11 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID12 + suffix);
            jobdata.PPID12 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID13 + suffix);
            jobdata.PPID13 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID14 + suffix);
            jobdata.PPID14 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID15 + suffix);
            jobdata.PPID15 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID16 + suffix);
            jobdata.PPID16 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID17 + suffix);
            jobdata.PPID17 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID18 + suffix);
            jobdata.PPID18 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID19 + suffix);
            jobdata.PPID19 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID20 + suffix);
            jobdata.PPID20 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID21 + suffix);
            jobdata.PPID21 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID22 + suffix);
            jobdata.PPID22 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID23 + suffix);
            jobdata.PPID23 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID24 + suffix);
            jobdata.PPID24 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID25 + suffix);
            jobdata.PPID25 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID26 + suffix);
            jobdata.PPID26 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID27 + suffix);
            jobdata.PPID27 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID28 + suffix);
            jobdata.PPID28 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID29 + suffix);
            jobdata.PPID29 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID30 + suffix);
            jobdata.PPID30 = GetItemValue(EventName, ItemCollection, PLCEventItem.PPID1 + suffix);
            jobdata.JobType = GetItemValue(EventName, ItemCollection, PLCEventItem.JobType + suffix);
            jobdata.JobID = GetItemValue(EventName, ItemCollection, PLCEventItem.JobID + suffix);
            jobdata.LotSequenceNumber = GetItemValue(EventName, ItemCollection, PLCEventItem.LotSequenceNumber + suffix);
            jobdata.SlotSequenceNumber = GetItemValue(EventName, ItemCollection, PLCEventItem.SlotSequenceNumber + suffix);
            jobdata.PropertyCode = GetItemValue(EventName, ItemCollection, PLCEventItem.PropertyCode + suffix);
            jobdata.JobJudgeCode = GetItemValue(EventName, ItemCollection, PLCEventItem.JobJudgeCode + suffix);
            jobdata.JobGradeCode = GetItemValue(EventName, ItemCollection, PLCEventItem.JobGradeCode + suffix);
            jobdata.SubstrateType = GetItemValue(EventName, ItemCollection, PLCEventItem.SubstrateType + suffix);
            jobdata.ProcessingFlag1 = GetItemValue(EventName, ItemCollection, PLCEventItem.ProcessingFlag1 + suffix);
            jobdata.ProcessingFlag2 = GetItemValue(EventName, ItemCollection, PLCEventItem.ProcessingFlag2 + suffix);
            jobdata.ProcessingFlag3 = GetItemValue(EventName, ItemCollection, PLCEventItem.ProcessingFlag3 + suffix);
            jobdata.SkipFlag1 = GetItemValue(EventName, ItemCollection, PLCEventItem.SkipFlag1 + suffix);
            jobdata.SkipFlag2 = GetItemValue(EventName, ItemCollection, PLCEventItem.SkipFlag2 + suffix);
            jobdata.SkipFlag3 = GetItemValue(EventName, ItemCollection, PLCEventItem.SkipFlag3 + suffix);
            jobdata.GlassThickness = GetItemValue(EventName, ItemCollection, PLCEventItem.GlassThickness + suffix);
            jobdata.JobAngle = GetItemValue(EventName, ItemCollection, PLCEventItem.JobAngle + suffix);
            jobdata.JobFlip = GetItemValue(EventName, ItemCollection, PLCEventItem.JobFlip + suffix);
            jobdata.MMGCode = GetItemValue(EventName, ItemCollection, PLCEventItem.MMGCode + suffix);
            jobdata.PanelInchSizeX = GetItemValue(EventName, ItemCollection, PLCEventItem.PanelInchSizeX + suffix);
            jobdata.PanelInchSizeY = GetItemValue(EventName, ItemCollection, PLCEventItem.PanelInchSizeY + suffix);
            jobdata.AbnormalFlag1 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag1 + suffix);
            jobdata.AbnormalFlag2 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag2 + suffix);
            jobdata.AbnormalFlag3 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag3 + suffix);
            jobdata.AbnormalFlag4 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag4 + suffix);
            jobdata.AbnormalFlag5 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag5 + suffix);
            jobdata.AbnormalFlag6 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag6 + suffix);
            //jobdata.AbnormalFlag7 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag7 + suffix);
            //jobdata.AbnormalFlag8 = GetItemValue(EventName, ItemCollection, PLCEventItem.AbnormalFlag8 + suffix);
            //jobdata.WorkOrderID = GetItemValue(EventName, ItemCollection, PLCEventItem.WorkOrderID + suffix);
            return jobdata;
        }
    }
}
