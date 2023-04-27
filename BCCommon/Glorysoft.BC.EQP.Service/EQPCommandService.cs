using Glorysoft.Auto.Contract;
using Glorysoft.Auto.Contract.PLC;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.RVEntity;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.PLCDriver;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Item = Glorysoft.PLCDriver.Item;

namespace Glorysoft.BC.EQP.Service
{
    public class EQPCommandService : IEQPCommandService
    {
        private IPLCContext context;
        protected ILog BCLog = Entity.LogHelper.BCLog;
        private ILogicService logicService = CommonContexts.ResolveInstance<ILogicService>();
        private IRobotService robotService = CommonContexts.ResolveInstance<IRobotService>();
        private IPLCContext GetPLCContextByEQPName(string eqpName)
        {
            return CommonContexts.GetPLCContextByName(eqpName);
        }

        public void SetZRBlock(PLCData ReplyData, int i, GlassInfo glassInfo)
        {

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PRODID, i)] = HostInfo.Current.FormatString(glassInfo.ProductID);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.OperID, i)] = HostInfo.Current.FormatString(glassInfo.OperationID);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.LotID, i)] = HostInfo.Current.FormatString(glassInfo.LotID);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID1, i)] = 0;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID2, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID1);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID3, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID2);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID4, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID3);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID5, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID4);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID6, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID5);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID7, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID6);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID8, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID7);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID9, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID8);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID10, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID9);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID11, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID10);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID12, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID11);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID13, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID12);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID14, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID13);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID15, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID14);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID16, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID15);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID17, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID16);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID18, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID17);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID19, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID18);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID20, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID19);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID21, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID20);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID22, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID21);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID23, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID22);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID24, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID23);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID25, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID24);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID26, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID25);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID27, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID26);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID28, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID27);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID29, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID28);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PPID30, i)] = HostInfo.Current.FormatStringToInt(glassInfo.PPID29);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.JobType, i)] = glassInfo.JobType;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.JobID, i)] = HostInfo.Current.FormatString(glassInfo.GlassID);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.LotSequenceNumber, i)] = glassInfo.CassetteSequenceNo;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.SlotSequenceNumber, i)] = glassInfo.SlotSequenceNo;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PropertyCode, i)] = 0;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.JobJudgeCode, i)] = HostInfo.Current.FormatString(glassInfo.GlassJudge);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.JobGradeCode, i)] = HostInfo.Current.FormatString(glassInfo.GlassGradeCode);

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.SubstrateType, i)] = glassInfo.SubstrateType;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.ProcessingFlag1, i)] = 0;//TBD

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.ProcessingFlag2, i)] = 0;//TBD

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.ProcessingFlag3, i)] = 0;//TBD

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.SkipFlag1, i)] = 0;//TBD

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.SkipFlag2, i)] = 0;//TBD

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.SkipFlag3, i)] = 0;//TBD

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.GlassThickness, i)] = glassInfo.Thickness;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.JobAngle, i)] = 0;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.JobFlip, i)] = 0;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.MMGCode, i)] = 0;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PanelInchSizeX, i)] = 0;

            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.PanelInchSizeY, i)] = 0;

            for (int index = 0; index < 8; index++)
            {
                ReplyData.ItemCollection[string.Format("{0}{1}#{2}", PLCEventItem.AbnormalFlag, index + 1, i)] = 0;
            }

            if (!String.IsNullOrEmpty(glassInfo.AbnormalCodes))
            {
                //string code = GetAbnormalFlag(glassInfo.AbnormalCodes);
                //var codes = code.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                //for (int index = 0; index < codes.Length; index++)
                //{
                //    ReplyData.ItemCollection[string.Format("{0}{1}#{2}", PLCEventItem.AbnormalFlag, index + 1, i)] = codes[index];
                //}
                var abnormalCodeList = glassInfo.AbnormalCodes.Split(';');
                for (int index = 0; index < 8; index++)
                {
                    string abnormalCode = "0";
                    if (index < abnormalCodeList.Count())
                        abnormalCode = abnormalCodeList[index];
                    ReplyData.ItemCollection[string.Format("{0}{1}#{2}", PLCEventItem.AbnormalFlag, index + 1, i)] = HostInfo.Current.FormatStringToInt(abnormalCode);
                }
            }
            ReplyData.ItemCollection[string.Format("{0}#{1}", PLCEventItem.WorkOrderID, i)] = HostInfo.Current.FormatString(glassInfo.WorkOrder);
        }
        public bool CassetteMapDownloadCommand(string eqpName, List<GlassInfo> glassInfoList, string portNo, int capacity, int iMapDownLoadDelay)
        {
            #region 需求7 1.下账失败则不on bit liuyusen 20221013
            bool Result = false;
            #endregion
            try
            {
                IPLCContext plcContext = null;
                PLCData ReplyData = null;
                plcContext = GetPLCContextByEQPName(eqpName);
                //string commandName = PLCCommandName.RecipeParameterCommandBlock;
                ReplyData = new PLCData(eqpName, $"Port{portNo}CassetteMapDownloadCommand");
                if (eqpName.Contains("DOM"))//DOM不分position 按顺序放
                {
                    List<GlassInfo> glasss = glassInfoList != null ? glassInfoList.OrderBy(c => c.Position).ToList() : new List<GlassInfo>();
                    for (int i = 0; i < capacity; i++)
                    {
                        var glass = glasss.Count >= (i + 1) ? glasss[i] : null;

                        if (glass != null)
                        {
                            SetZRBlock(ReplyData, (i + 1), glass);
                        }
                        else
                        {
                            SetZRBlock(ReplyData, (i + 1), new GlassInfo());
                        }
                    }
                }
                else//卡夹
                {
                    for (int i = 0; i < capacity; i++)
                    {
                        var seqno = i + 1;
                        //var SlotSequenceNo = (seqno <= 120) ? (1000 + seqno) : (2000 + (seqno - 120));
                        var SlotSequenceNo = (seqno % 2 != 0 ? 1000 : 2000) + seqno / 2 + seqno % 2;
                        var glass = glassInfoList.FirstOrDefault(o => o.SlotSequenceNo == SlotSequenceNo);

                        if (glass != null)
                        {
                            SetZRBlock(ReplyData, seqno, glass);
                        }
                        else
                        {
                            SetZRBlock(ReplyData, seqno, new GlassInfo());
                        }
                    }
                }

                plcContext.SendCommand(ReplyData);
                #region 需求7 1.下账失败则不on bit liuyusen 20221013
                //if (iMapDownLoadDelay > 0)
                //    Thread.Sleep(iMapDownLoadDelay);
                #region 需求8 2.收到下账结果直接on bit liuyusen 20221115
                while (iMapDownLoadDelay > 0)
                {
                    var blockdata = plcContext.GetBlock(eqpName, $"Port{portNo}CassetteMapDownloadCommand", "");
                    if (blockdata != null)
                    {
                        Block block = blockdata as Block;
                        if (block.ExcuteResult)
                            Result = true;
                    }
                    if (Result)
                    {
                        return Result;
                    }
                    iMapDownLoadDelay = iMapDownLoadDelay - 100;
                    Thread.Sleep(100);
                }
                #endregion
                #endregion
            }
            catch (Exception ex)
            {
                BCLog.Error(ex);
                //BCLog.Error(string.Format("[BC to CCLink][RedisCassetteMap]  [Thread:{0}]ex:{1}", CurrentThread, ex));
            }
            #region 需求7 1.下账失败则不on bit liuyusen 20221013
            return Result;
            #endregion
        }

    }
}
