using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;
using System.Threading;

namespace Glorysoft.BC.EIP.Handlers
{
    public class LinkSignalHandler : AbstractEventHandler
    {
        public LinkSignalHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                var bitBlock = args.BitBlock;
                var eqpName = bitBlock.EQPName;
                //LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);


                var linkName = args.BitItem.Name;
                var linkValue = args.BitValue;
                var tagname = args.BitBlock.EventID;
                var blockname = args.BitItem.EventID;

                LogHelper.LinkSignalLog.InfoFormat("Tag:[{0}] Block:[{1}] Link:[{2}] Value:[{3}]", tagname, blockname, linkName, linkValue);

                var unitInfo = HostInfo.Current.EQPInfo.Units.FirstOrDefault(f => f.UnitName == eqpName);
                if (unitInfo == null)
                {
                    //LogHelper.EIPLog.ErrorFormat("+++ LinkSignalHandler:{0} Cannot Find EQPInfo +++", eqpName);
                    return;
                }
                Linksignal link = null;
                RobotModel robotModel = null;
                foreach (var item in unitInfo.RobotModelList)
                {
                    link = item.LinksignalList.FirstOrDefault(o => o.LinkName == args.BitItem.EventID);
                    if (link != null)
                    {
                        robotModel = item;
                        break;
                    }
                }
                //if (robotModel != null)
                //{
                //    //BCLog.Debug(string.Format("[HandlerName:{1}] [Thread:{0}] robotModel:{2}", CurrentThread, this.GetType().Name, robotModel.ModelName));
                //}
                //else
                //{
                //    //BCLog.Debug("robotModel==null");
                //}
                //var linkName = data.ItemCollection.ToList()[0].Key;
                //var linkValue = data.ItemCollection.ToList()[0].Value.ToString().Trim() == "0" ? false : true;


                #region OPILINK
                if (HostInfo.OPILinkList.Any(c => c.LinkSignalName == tagname))
                {
                    var linkdata = HostInfo.OPILinkList.FirstOrDefault(c => c.LinkSignalName == tagname);
                    if (blockname == "UpstreamLinkSignal")
                    {
                        if (linkdata.UpstreamLinkData.Any(c => c.ItemName == linkName))
                        {
                            var data = linkdata.UpstreamLinkData.FirstOrDefault(c => c.ItemName == linkName);
                            data.ItemValue = linkValue;
                        }
                    }
                    else if (blockname == "DownstreamLinkSignal")
                    {
                        if (linkdata.DownstreamLinkData.Any(c => c.ItemName == linkName))
                        {
                            var data = linkdata.DownstreamLinkData.FirstOrDefault(c => c.ItemName == linkName);
                            data.ItemValue = linkValue;
                        }
                    }
                }
                //OPILink OPILink = new OPILink();
                //if (data.Name.Contains("Downstream"))
                //{
                //    OPILink = HostInfo.OPILinkList.FirstOrDefault(o => o.DownLinkUnitName == unitName && o.DownLinkName == data.Name);
                //    if (OPILink != null)
                //    {
                //        HostInfo.Current.UpdateLinkSignalValue(OPILink.DownstreamLink.GetType(), OPILink.DownstreamLink, linkName, linkValue);
                //    }
                //}
                //else
                //{
                //    OPILink = HostInfo.OPILinkList.FirstOrDefault(o => o.UpLinkUnitName == unitName && o.UpLinkName == data.Name);
                //    if (OPILink != null)
                //    {
                //        HostInfo.Current.UpdateLinkSignalValue(OPILink.UpstreamLink.GetType(), OPILink.UpstreamLink, linkName, linkValue);
                //    }
                //}
                #endregion

                //// var link = unit.Linksignals.FirstOrDefault(o => o.LinkNo == Convert.ToInt32(LinkNo));
                if (link == null)
                {
                    //BCLog.DebugFormat(string.Format("[LinkSignalHandler][Thread:{2}] UnitName:{0};LinkName:{1} Cannot Find Linksignal! ", unitName, data.Name, CurrentThread));
                    return;
                }

                if (args.BitItem.EventID.Contains("Downstream"))
                {
                    HostInfo.Current.UpdateLinkSignalValue(new DownstreamLinkSignal().GetType(), link.LinkSignalItem, linkName, linkValue);

                    if (linkName == "ReceiveAble")
                    {
                        if (!linkValue)
                        {
                            if (robotModel != null)
                            {
                                robotModel.EQPReciveSlotNoA = 0;
                                robotModel.EQPReciveSlotNoB = 0;
                            }
                        }
                        else
                        {
                            if (robotModel != null)
                            {
                                Thread.Sleep(500);
                                var downstreamLinkSignal = (DownstreamLinkSignal)link.LinkSignalItem;
                                robotModel.EQPReciveSlotNoA = (downstreamLinkSignal.PositionFront1 || downstreamLinkSignal.PositionBack1) ? 1 : 0;
                                robotModel.EQPReciveSlotNoB = (downstreamLinkSignal.PositionFront2 || downstreamLinkSignal.PositionBack2) ? 2 : 0;
                            }
                        }
                    }
                }
                else
                {
                    HostInfo.Current.UpdateLinkSignalValue(new UpstreamLinkSignal().GetType(), link.LinkSignalItem, linkName, linkValue);

                    if (linkName == "SendAble")
                    {
                        if (!linkValue)
                        {
                            if (robotModel != null)
                            {
                                //robotModel.GlassA = null;
                                //robotModel.GlassB = null;
                                //robotModel.GlassC = null;
                                //robotModel.GlassD = null;
                                robotModel.EQPSendSlotNoA = 0;
                                robotModel.EQPSendSlotNoB = 0;
                                robotModel.SendPositionFront1 = false;
                                robotModel.SendPositionFront2 = false;
                                robotModel.SendPositionBack1 = false;
                                robotModel.SendPositionBack2 = false;
                            }
                        }
                        else
                        {
                            if (robotModel != null)
                            {
                                Thread.Sleep(500);
                                var upstreamLinkSignal = (UpstreamLinkSignal)link.LinkSignalItem;
                                robotModel.EQPSendSlotNoA = (upstreamLinkSignal.PositionFront1 || upstreamLinkSignal.PositionBack1) ? 1 : 0;
                                robotModel.EQPSendSlotNoB = (upstreamLinkSignal.PositionFront2 || upstreamLinkSignal.PositionBack2) ? 2 : 0;
                                robotModel.SendPositionFront1 = upstreamLinkSignal.PositionFront1;
                                robotModel.SendPositionFront2 = upstreamLinkSignal.PositionFront2;
                                robotModel.SendPositionBack1 = upstreamLinkSignal.PositionBack1;
                                robotModel.SendPositionBack2 = upstreamLinkSignal.PositionBack2;

                                //读取对应的Jobdatablock
                                var tag = PLCContexts.Current.GetBlock(eqpName, tagname);
                                if (tag != null)
                                {
                                    PLCContexts.Current.ReadFromPLC(tag);
                                    List<GlassInfo> glassdatas = new List<GlassInfo>();
                                    for (int i = 1; i <= 2; i++)
                                    {
                                        var jobdatablock = "JobData#" + i.ToString();
                                        if (tag.BlockCollection.ContainsKey(jobdatablock))
                                        {
                                            var data = tag.BlockCollection.FirstOrDefault(f => f.Key == jobdatablock);
                                            GlassInfo ginfo = new GlassInfo();
                                            for (int j = 1; j <= 2; j++)
                                            {
                                                var LotSequenceNumber = Convert.ToInt32(data.Value.ItemCollection[PLCEventItem.LotSequenceNumber + "#" + j.ToString()].Value);
                                                var SlotSequenceNumber = Convert.ToInt32(data.Value.ItemCollection[PLCEventItem.SlotSequenceNumber + "#" + j.ToString()].Value);
                                                //job缓存数据
                                                if (LotSequenceNumber == 0 || SlotSequenceNumber == 0)
                                                {
                                                    if (i == 1)
                                                    {
                                                        if (j == 1)
                                                        {
                                                            robotModel.GlassA = null;
                                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassA", bitBlock.EQPName);
                                                        }
                                                        if (j == 2)
                                                        {
                                                            robotModel.GlassB = null;
                                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassB", bitBlock.EQPName);
                                                        }
                                                    }
                                                    else if (i == 2)
                                                    {
                                                        if (j == 1)
                                                        {
                                                            robotModel.GlassC = null;
                                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassC", bitBlock.EQPName);
                                                        }
                                                        if (j == 2)
                                                        {
                                                            robotModel.GlassD = null;
                                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassD", bitBlock.EQPName);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == LotSequenceNumber && d.SlotSequenceNo == SlotSequenceNumber)))
                                                    //{
                                                    //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == LotSequenceNumber && d.SlotSequenceNo == SlotSequenceNumber));
                                                    //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == LotSequenceNumber && d.SlotSequenceNo == SlotSequenceNumber);
                                                    var glass = logicService.GetGlassInfoByCode(unitInfo, "LinkSignal", "", LotSequenceNumber.ToString(), SlotSequenceNumber.ToString());
                                                    if (glass != null)
                                                        {
                                                            glass.ModelPosition = robotModel.ModelPosition;// robot put时 用于寻找taget position//更新数据库
                                                            dbService.UpdateGlassInfo(glass);
                                                            if (i == 1)
                                                            {
                                                                if (j == 1)
                                                                {
                                                                    robotModel.GlassA = glass;
                                                                    LogHelper.EIPLog.InfoFormat("LinkSignalHandler:{0} ,Update Robot Jobdata: GlassA [{1},{2},{3}]", bitBlock.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                                                }
                                                                if (j == 2)
                                                                {
                                                                    robotModel.GlassB = glass;
                                                                    LogHelper.EIPLog.InfoFormat("LinkSignalHandler:{0} ,Update Robot Jobdata: GlassB [{1},{2},{3}]", bitBlock.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                                                }
                                                            }
                                                            else if (i == 2)
                                                            {
                                                                if (j == 1)
                                                                {
                                                                    robotModel.GlassC = glass;
                                                                    LogHelper.EIPLog.InfoFormat("LinkSignalHandler:{0} ,Update Robot Jobdata: GlassC [{1},{2},{3}]", bitBlock.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                                                }
                                                                if (j == 2)
                                                                {
                                                                    robotModel.GlassD = glass;
                                                                    LogHelper.EIPLog.InfoFormat("LinkSignalHandler:{0} ,Update Robot Jobdata: GlassD [{1},{2},{3}]", bitBlock.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                                                }
                                                            }
                                                        }
                                                    //}
                                                    //else
                                                    //    LogHelper.EIPLog.InfoFormat("LinkSignalHandler:{0} ,Update Robot Jobdata NG: Glass Not found in portinfo [{1},{2}]", bitBlock.EQPName, LotSequenceNumber, SlotSequenceNumber);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ LinkSignalHandler:{0} ,Error:{1} +++", args.BitBlock.EQPName, ex.ToString());
            }
        }
    }
}