using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EIP.Common;
using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity.RVEntity;
using System.Collections;

namespace Glorysoft.BC.EIP.Handlers
{
    public class LinkSignalJobDataBlockHandler : AbstractEventHandler
    {
        public LinkSignalJobDataBlockHandler(IPLCContext context)
            : base(context)
        {
        }
        public override void Execute(PLCEventArgs args)
        {
            try
            {
                LogHelper.EIPLog.DebugFormat("+++ [EQP=>EAS]-[{0}]EQPName:{1}+++", args.Message.EventName, args.Message.EQPName);
                var i = FindInt(args.Message.EventName);
                var plcmsg = args.Message;
                var txid = args.Message.TransactionID;
                var eqpName = plcmsg.EQPName;
                if (plcmsg == null) return;
                var oEQP = HostInfo.Current.AllEQPInfo.FirstOrDefault(c => c.Units.Any(d => d.UnitName == eqpName)).Units.FirstOrDefault(d => d.UnitName == eqpName);
                if (oEQP == null)
                {
                    LogHelper.EIPLog.ErrorFormat("+++ LinkSignalJobDataBlockHandler:{0} Cannot Find EQPInfo +++", args.Message.EQPName);
                    return;
                }
                LogHelper.LinkSignalLog.Info(plcmsg.ToString());

                RobotModel robotModel = null;
                if (!String.IsNullOrEmpty(plcmsg.TagName) && plcmsg.TagName.StartsWith("RV_EQToEQ_LinkSignal_" + oEQP.LocalNo.ToString()))
                {
                    Linksignal link = null;
                    foreach (var item in oEQP.RobotModelList)
                    {
                        link = item.LinksignalList.FirstOrDefault(o => o.LinkName == "UpstreamLinkSignal");
                        if (link != null)
                        {
                            robotModel = item;
                            break;
                        }
                    }
                }
                if (robotModel != null)
                {
                    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Excuting Update Robot Jobdata", args.Message.EQPName);
                    GlassInfo ginfo = new GlassInfo();
                    for (int j = 1; j <= 2; j++)
                    {
                        var LotSequenceNumber = Convert.ToInt32(plcmsg.ItemCollection[PLCEventItem.LotSequenceNumber + "#" + j.ToString()]);
                        var SlotSequenceNumber = Convert.ToInt32(plcmsg.ItemCollection[PLCEventItem.SlotSequenceNumber + "#" + j.ToString()]);
                        //job缓存数据
                        if (LotSequenceNumber == 0 || SlotSequenceNumber == 0)
                        {
                            if (i == 1)
                            {
                                if (j == 1)
                                {
                                    robotModel.GlassA = null;
                                    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassA", args.Message.EQPName);
                                }
                                if (j == 2)
                                {
                                    robotModel.GlassB = null;
                                    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassB", args.Message.EQPName);
                                }
                            }
                            else if (i == 2)
                            {
                                if (j == 1)
                                {
                                    robotModel.GlassC = null;
                                    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassC", args.Message.EQPName);
                                }
                                if (j == 2)
                                {
                                    robotModel.GlassD = null;
                                    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Clear Robot Jobdata: GlassD", args.Message.EQPName);
                                }
                            }
                        }
                        else
                        {
                            //job缓存数据
                            //if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == LotSequenceNumber && d.SlotSequenceNo == SlotSequenceNumber)))
                            //{
                            //    var portinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.CassetteSequenceNo == LotSequenceNumber && d.SlotSequenceNo == SlotSequenceNumber));
                            //    var glass = portinfo.GlassInfos.FirstOrDefault(d => d.CassetteSequenceNo == LotSequenceNumber && d.SlotSequenceNo == SlotSequenceNumber);
                                var glass = logicService.GetGlassInfoByCode(oEQP, "LinkSignalJobData", "", LotSequenceNumber.ToString(), SlotSequenceNumber.ToString());
                                if (glass != null)
                                {
                                    glass.ModelPosition = robotModel.ModelPosition;// robot put时 用于寻找taget position//更新数据库
                                    dbService.UpdateGlassInfo(glass);
                                    if (i == 1)
                                    {
                                        if (j == 1)
                                        {
                                            robotModel.GlassA = glass;
                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Update Robot Jobdata: GlassA [{1},{2},{3}]", args.Message.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                        }
                                        if (j == 2)
                                        {
                                            robotModel.GlassB = glass;
                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Update Robot Jobdata: GlassB [{1},{2},{3}]", args.Message.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                        }
                                    }
                                    else if (i == 2)
                                    {
                                        if (j == 1)
                                        {
                                            robotModel.GlassC = glass;
                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Update Robot Jobdata: GlassC [{1},{2},{3}]", args.Message.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                        }
                                        if (j == 2)
                                        {
                                            robotModel.GlassD = glass;
                                            LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Update Robot Jobdata: GlassD [{1},{2},{3}]", args.Message.EQPName, glass.GlassID, glass.CassetteSequenceNo, glass.SlotSequenceNo);
                                        }
                                    }
                                }
                            //}
                            //else
                            //    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,Update Robot Jobdata NG: Glass Not found in portinfo [{1},{2}]", args.Message.EQPName, LotSequenceNumber, SlotSequenceNumber);
                        }
                    }
                }
                else
                    LogHelper.EIPLog.InfoFormat("LinkSignalJobDataBlockHandler:{0} ,RobotModel not found", args.Message.EQPName);
                //List<JobDataInfo> jobdatas = new List<JobDataInfo>();
                //int jobcount = (plcmsg.ItemCollection.Count) / 62;
                //for (int iJob = 1; iJob <= jobcount; iJob++)
                //{
                //    var suffix = "#" + iJob.ToString();
                //    JobDataInfo jobdata = GetEQPJobData(args.Message.EventName, plcmsg.ItemCollection, suffix);
                //    jobdatas.Add(jobdata);
                //}
            }
            catch (Exception ex)
            {
                LogHelper.EIPLog.ErrorFormat("+++ LinkSignalJobDataBlockHandler:{0} ,Error:{1} +++", args.Message.EQPName, ex.ToString());
            }
        }
    }
}