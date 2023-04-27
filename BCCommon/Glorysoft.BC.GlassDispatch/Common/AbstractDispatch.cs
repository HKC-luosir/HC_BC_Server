using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using log4net;
using System;
using Glorysoft.Auto.Contract;
using Glorysoft.BC.Logic.Contract;
using System.Text;
using System.Threading;

namespace Glorysoft.BC.GlassDispath
{
    public abstract class AbstractDispatch : IJobDispatch
    {
        public ILog Logger;
        private DispatchRun runner;
        //internal readonly List<PortInfo> PortList;
        public HostInfo LineInfo;
        protected readonly List<Linksignal> StageLinksignalList;
        //  protected readonly List<Linksignal> IdxLinksignalList = new List<Linksignal>();
        protected List<RobotCommand> cmdList { get; set; } = new List<RobotCommand>();
        public readonly List<JobStage> StageList;
        internal readonly Robot robot;
        internal readonly Unit Indexer;
        internal const int PriorityHighest = int.MaxValue;
        internal CommandFactory factory;
        public delegate void RobotCommandEventHandler(RobotCommand cmd);
        protected static readonly IEQPService eqpCmd = CommonContexts.ResolveInstance<IEQPService>();
        protected static readonly IDBService dbCmd = CommonContexts.ResolveInstance<IDBService>();
        public event RobotCommandEventHandler OnSendRobotCommand;

        public delegate void ModifyDBOldPriorityEventHandler();
        public event ModifyDBOldPriorityEventHandler OnModifyDBOldPriority;
        protected AbstractDispatch(string name)
        {
            try
            {
                LineInfo = HostInfo.Current;
                Name = name;
                factory = new CommandFactory(Name);
                Configure = RobotConfigureManagement.Current.RobotConfigureList[Name];
                StageList = new List<JobStage>();
                Indexer = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == name);
                //robot = Indexer.SUnitList.FirstOrDefault(o => o.SUnitType == (int)EnumUnitType.Robot) as Robot;
                robot = (Robot)Indexer;
                // PortList = HostInfo.Current.PortList;
                //foreach (var val in PortList)
                //{
                //    StageList.Add(new JobStage(val, Indexer.UnitName, val.UnitPathNo));
                //}
                foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
                {
                    //if (unitItem.GetType().Name != "PortInfo")
                    //{
                    //    foreach (var RobotModelItem in unitItem.RobotModelList)
                    //    {
                    //        StageList.Add(new JobStage(unitItem, Indexer.UnitName, val.UnitPathNo));
                    //    }
                    //}
                    // else
                    //{
                    foreach (var RobotModelItem in unitItem.RobotModelList)
                    {

                        if (unitItem.GetType().Name == "Robot")
                        {
                            StageList.Add(new JobStage(EnumUnitType.Robot, unitItem.UnitName, RobotModelItem.ModelPosition));
                        }
                        else if (unitItem.GetType().Name == "Unit")
                        {
                            StageList.Add(new JobStage(EnumUnitType.Stage, unitItem.UnitName, RobotModelItem.ModelPosition));
                        }
                    }


                    //}

                    //foreach (var sunitItem in unitItem.Value.SUnitList)
                    //{

                    //}
                }

                //var lst = Indexer.SUnitList.FindAll(o => o.Type == EnumUnitType.Stage || o.Type == EnumUnitType.Conveyor);
                //foreach (var val in lst)
                //{
                //    StageList.Add(new JobStage(val, Indexer.UnitName));
                //}
                StageLinksignalList = new List<Linksignal>();
                foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
                {
                    //foreach (var sunitItem in unitItem.SUnitList)
                    //{
                    foreach (var RobotModelItem in unitItem.RobotModelList)
                    {
                        foreach (var LinkItem in RobotModelItem.LinksignalList)
                        {
                            StageLinksignalList.Add(LinkItem);
                            // Logger.InfoFormat("[AbstractDispatch][StageLinksignalList] - Check StageLinksignalList:{0}UnitName:{1}LinkSignalItem:{2}", LinkItem.LinkName, LinkItem.UnitName, LinkItem.LinkSignalItem);
                        }

                    }
                    // }
                }
                //if (Robot != null)
                //{
                //    IdxLinksignalList = Robot.Linksignals;
                //}

                //foreach (var idxlink in IdxLinksignalList)
                //{
                //    var unit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == idxlink.UnitName);
                //    if (unit != null)
                //    {
                //        //var sunit = unit.SUnitList.FirstOrDefault(o => o.SUnitName == idxlink.OSUnitName);
                //        foreach (var RobotModelItem in unit.RobotModelList)
                //        {
                //            foreach (var ulink in RobotModelItem.LinksignalList)
                //            {
                //                if (ulink.UnitName == idxlink.UnitName)
                //                {
                //                    StageLinksignalList.Add(ulink);
                //                    if (!StageList.Exists(o => o.ModelPosition == RobotModelItem.ModelPosition && o.UnitName == unit.UnitName))
                //                    {
                //                        StageList.Add(new JobStage(unit, unit.UnitName, RobotModelItem.ModelPosition, RobotModelItem.ModelID));
                //                    }
                //                }
                //            }

                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }

        private void CheckCFGError(RobotConfigure cfg)
        {
            //try
            //{
            //    foreach (var kv in cfg.PathList)
            //    {
            //        foreach (var path in kv.Value)
            //        {

            //            if (cfg.GroupList.Exists(o => o.Name == path.SourcePathName))
            //            {
            //                var group = cfg.GroupList.Find(o => o.Name == path.SourcePathName);
            //                if (group == null)
            //                {
            //                    Logger.ErrorFormat("[0]: Not Find Group for Unit {1}", cfg.Name, path.SourcePathName);
            //                }
            //                else
            //                {
            //                    foreach (var groupname in group.List)
            //                    {
            //                        CheckSUnitError(cfg.Name, groupname);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                CheckModelPositionError(cfg.Name, path.SourcePathName);
            //            }
            //            if (cfg.GroupList.Exists(o => o.Name == path.TargetPathName))
            //            {
            //                var group = cfg.GroupList.Find(o => o.Name == path.TargetPathName);
            //                if (group == null)
            //                {
            //                    Logger.ErrorFormat("[0]: Not Find Group for Unit {1}", cfg.Name, path.TargetPathName);
            //                }
            //                else
            //                {
            //                    foreach (var groupname in group.List)
            //                    {
            //                        CheckSUnitError(cfg.Name, groupname);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                CheckModelPositionError(cfg.Name, path.TargetPathName);
            //            }
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    LogHelper.BCLog.Debug(ex);
            //}

        }

        private void CheckModelPositionError(string robot, int ModelPosition)
        {
            try
            {
                foreach (var unitItem in LineInfo.EQPInfo.Units)
                {

                    var RobotModel = unitItem.RobotModelList.FirstOrDefault(o => o.ModelPosition == ModelPosition);
                    if (RobotModel == null)
                    {
                        Logger.ErrorFormat("[0]: Cannot Find ModelPosition {1}", robot, ModelPosition);
                    }
                    //else
                    //{
                    //    //var sunit = unit.SUnitList.FirstOrDefault(o => o.SUnitName == sunitname);

                    //    if (unitItem.UnitType != EnumUnitType.Robot)
                    //    {
                    //        if (RobotModel.LinksignalList.Count == 0)
                    //        {
                    //            Logger.ErrorFormat("[0]: No Link Signal for Unit {1}", robot, ModelPosition);
                    //        }
                    //    }
                    //}

                }

            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        internal abstract List<RobotCommand> Dispatch(ref StringBuilder logStr);
        public string Name { get; private set; }
        public RobotConfigure Configure { get; set; }

        #region 接口事件实现
        public virtual void Initialize()
        {
            try
            {
                LogManager.ConfigureLogger(Configure.LogName, Configure.LogPath, eLogFilter.DEBUG, eLogFilter.ERROR | eLogFilter.INFO, eFileFomart.Date, null);
                Logger = LogManager.GetConfigureLogger(Configure.LogName);
                //CheckCFGError(Configure);
                //初始化派工线程和Timer
                runner = new DispatchRun(Logger, this);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        public void Start()
        {
            runner.Start();
        }

        public void Stop()
        {
            runner.Stop();
        }
        /// <summary>
        /// Robot Control Command Result Report   robot 执行命令还是拒绝
        /// </summary>
        /// <param name="code">1 执行; 其他拒绝</param>
        public void CommandReturnCodeReport(int code)
        {
            try
            {
                if (code == 1)
                {
                    robot.CommandExecuting = true;
                    //派工3分钟超时开始计时
                }
                else
                {
                    Thread.Sleep(2000);
                }
                robot.IsWaitCmdCode = false;
                robot.CmdResultCode = code;
                Logger.InfoFormat("Command ReturnCode Report: {0}", code);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        /// <summary>
        /// Robot Execute Result Report robot执行完后的结果上报
        /// </summary>
        /// 
        public void CommandExecuteResultReport()
        {
            try
            {
                robot.CommandExecuting = false;
                if (robot.IsWaitCmdCode)
                    robot.IsWaitCmdCode = false;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        /// <summary>
        /// Robot Execute Result Report robot执行完后的结果上报
        /// </summary>
        /// <param name="result"></param>
        //public void CommandExecuteResultReport(RobotCommandResult result)
        //{
        //    try
        //    {
        //        robot.CommandExecuting = false;
        //        if (robot.IsWaitCmdCode)
        //            robot.IsWaitCmdCode = false;
        //        Logger.InfoFormat("Command ExecuteResult Report {0}", result.ResultCode);
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }

        //}

        internal void SendCommand(RobotCommand cmd)
        {
            try
            {
                if (OnSendRobotCommand != null)
                {
                    OnSendRobotCommand(cmd);
                }
                Logger.InfoFormat("SendCommand, CommandExecuting: {0}, IsWaitCmdCode:{1}", robot.CommandExecuting, robot.IsWaitCmdCode);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        internal void ModifyDBOldPriority()
        {
            try
            {
                if (OnModifyDBOldPriority != null)
                {
                    OnModifyDBOldPriority();
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        #endregion

        internal bool IsAutoDispatch
        {
            get { return true; } //Indexer.EquipmentAutoMode == EnumEqpAutoMode.AUTO; }
        }

        public Robot Robot
        {
            get { return robot; }
        }

        protected List<PortInfo> GetLoadingPossiblePortList(List<PortInfo> portList, string functionName)
        {
            var list1 = new List<PortInfo>();
            try
            {
                try
                {
                    if (portList.Any(c => c.EQPID == HostInfo.Current.EQPInfo.EQPID))
                    {
                        portList = portList.Where(c => c.EQPID == HostInfo.Current.EQPInfo.EQPID).ToList();
                        //20180801 Modify
                        foreach (var p in portList)
                        {
                            if (IsPortStatusReadyForProcessing(p, functionName))
                            {
                                var wlist = p.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();// && o.SamplingFlag == "Y"

                                if (wlist.Count > 0)
                                {
                                    //Logger.InfoFormat("[{2}][Check Port Wait Glass Count OK] PortID: {0} Wait Glass Count: {1} ", p.PortID, wlist.Count, functionName);
                                    Logger.InfoFormat("[{0}] - Pre Check OK.PortID={1},PortStatus={2},CassetteStatus={3},Wait Glass Count={4}; WaitingforProcessingTime:{5}", functionName, p.PortID, p.PortStatus, p.CassetteInfo.CassetteStatus, wlist.Count, p.WaitingforProcessingTime);

                                    list1.Add(p);
                                }
                                else
                                {
                                    //Logger.InfoFormat("[{2}][Check Port Wait Glass Count NG] PortID: {0} Wait Glass Count: {1} ", p.PortID, wlist.Count, functionName);
                                    Logger.InfoFormat("[{0}] - Pre Check NG.PortID={1},PortStatus={2},CassetteStatus={3},Wait Glass Count={4}; WaitingforProcessingTime:{5}", functionName, p.PortID, p.PortStatus, p.CassetteInfo.CassetteStatus, wlist.Count, p.WaitingforProcessingTime);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    Logger.Info(ex);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return list1;
        }
        //protected List<PortInfo> GetCurrentLoadingPortList()
        //{
        //    var list1 = new List<PortInfo>();
        //    try
        //    {
        //        //20180801 Modify
        //        foreach (var p in PortList)
        //        {
        //            if (p.IsInGetPut)
        //            {
        //                Logger.InfoFormat("+++ GetCurrentLoadingPortList: {0} Port Type: {1}, IsInGetPut: {2}", p.UnitID, p.PortType, p.IsInGetPut);
        //                var wlist = p.GlassInfos.ToList().FindAll(o =>  o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed);
        //                Logger.InfoFormat("+++ GetCurrentLoadingPortList: Current {0} Wait Glass Count: {1} ", p.UnitID, wlist.Count);
        //                if (wlist.Count > 0)
        //                {
        //                    list1.Add(p);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return list1;
        //}
        //对于PU的Unloader Port,不能混批，因此要检查同一个Port里面的Glass，Lot ID和Glass Category要一致
        //protected List<PortInfo> GetUnloadingPossiblePortList(GlassInfo info)
        //{
        //    var list1 = new List<PortInfo>();
        //    //try
        //    //{
        //    //    if (string.IsNullOrEmpty(info.GLSGrade))
        //    //    {
        //    //        HostInfo.Current.OPCallListAdd(string.Format("[GetUnloadingPossiblePortList] Panelid:{0} ; PanelGrade is null;set panelgrade  G", info.GLSID));
        //    //        LogHelper.BCLog.Debug(string.Format("[GetUnloadingPossiblePortList] Panelid:{0} ; PanelGrade is null;set panelgrade  G", info.GLSID));
        //    //        info.GLSGrade = "G";
        //    //    }
        //    //    //20180801 Modify
        //    //    var pList = PortList.FindAll(o => o.PortType == EnumPortType.PU.GetHashCode().ToString());
        //    //    foreach (var p in pList)
        //    //    {
        //    //        if (IsPortStatusReadyForProcessing(p))
        //    //        {
        //    //            var wlist = p.GlassInfos.ToList().FindAll(o => !o.IsExist && o.SlotSatus == (int)EnumGlassSlotStatus.Wait);
        //    //            Logger.InfoFormat("+++ GetUnloadingPossiblePortList: {0} Empty Slot Count: {1} ", p.PortID, wlist.Count);
        //    //            //Port里面要有空的位置, 不能满卡
        //    //            if (wlist.Count > 0)
        //    //            {
        //    //                var glasslist = p.GlassInfos.ToList().FindAll(o => o.IsExist);
        //    //                if (glasslist.Count > 0)
        //    //                {
        //    //                    //Port里面已经有部分玻璃，需检查PanelGrade一样
        //    //                    if (glasslist.Exists(o => o.IsExist && o.GLSGrade == info.GLSGrade))
        //    //                    {
        //    //                        list1.Add(p);
        //    //                    }
        //    //                    else
        //    //                    {
        //    //                        Logger.InfoFormat("+++ GetUnloadingPossiblePortList: Cannot Put {1} to {0}, PanelGrade or PortUseType Not Right ", p.PortID, info.GLSID);
        //    //                        Logger.InfoFormat("+++ GetUnloadingPossiblePortList: Port {0}: PortUseType: {1}, LotID: {2} ", p.PortID, p.PortUseType, glasslist[0].LotID);
        //    //                        Logger.InfoFormat("+++ GetUnloadingPossiblePortList: Glass {0}: PanelGrade: {1}, LotID: {2} ", info.GLSID, info.GLSGrade, info.LotID);
        //    //                    }
        //    //                }

        //    //            }
        //    //            else  //Port里面没有玻璃，全部为空
        //    //            {
        //    //                //检查PortUseType和玻璃的PanelGrade一致
        //    //                if (p.PortUseType.Contains(info.GLSGrade))
        //    //                {
        //    //                    list1.Add(p);
        //    //                }
        //    //                else
        //    //                {
        //    //                    Logger.InfoFormat("+++ GetUnloadingPossiblePortList: Cannot Put {1} to {0}, PanelGrade Not Right ", p.PortID, info.GLSID);
        //    //                    Logger.InfoFormat("+++ GetUnloadingPossiblePortList: Port {0}: UseType: {1}", p.PortID, p.PortUseType);
        //    //                    Logger.InfoFormat("+++ GetUnloadingPossiblePortList: Glass {0}: PanelGrade: {1}, LotID: {2} ", info.GLSID, info.GLSGrade, info.LotID);
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Logger.Error(ex);
        //    //}
        //    return list1;
        //}

        protected bool IsPortStatusReadyForProcessing(PortInfo p, string functionName)
        {
            //20180801 Modify
            try
            {

                if ((p.PortStatus == (int)EnumPortStatus.InUse)
                    && (p.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing || p.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing))
                {
                    //Logger.InfoFormat("[{3}][Check Load Port OK] PortID:{0}; PortStatus:{1};CassetteStatus:{2} ", p.PortID, p.PortStatus, p.CassetteInfo.CassetteStatus, functionName);
                    return true;
                }
                else
                {
                    Logger.InfoFormat("[{3}] - Pre Check NG.PortID={0},PortStatus={1},CassetteStatus={2}", p.PortID, p.PortStatus, p.CassetteInfo.CassetteStatus, functionName);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return false;
        }
        protected bool IsPortStatusInProcessing(PortInfo p)
        {
            try
            {
                //20180801 Modify
                Logger.InfoFormat("[IsPortStatusInProcessing] - {0} Check: Port Type: {1}, PortProcessStatus: {2}, CstStatus:{3}, Enabled: {4}，LoadUnloadType:{5} ", p.UnitID, p.PortType, p.PortStatus, "", "", "");
                Logger.InfoFormat("[IsPortStatusInProcessing] - {0} Check: Port Type: {1}, PortProcessStatus: {2}, CstStatus:{3}, Enabled: {4}，LoadUnloadType:{5} ", p.UnitID, p.PortType, p.PortStatus, "", "", "");
                if ((p.PortStatus == (int)EnumPortStatus.InUse)
                    && (p.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return false;
        }
        //protected PortInfo GetCurrentUnloadingPort(List<PortInfo> pList)
        //{
        //    try
        //    {
        //        //筛选出数量最多的先放
        //        var ports = pList.FindAll(p =>  IsPortStatusReadyForProcessing(p));
        //        if (ports.Count < 1)
        //        {
        //            Logger.InfoFormat("[GetCurrentUnloadingPort]No Search UnLoading Port");
        //            return null;
        //        }
        //        //筛选出unload口中panel数量最多的port
        //        var port = pList.OrderByDescending(p => p.GlassInfos.Count()).FirstOrDefault();
        //        if(port!=null)
        //        {
        //            Logger.InfoFormat("[GetCurrentUnloadingPort] CurrentPort!=null CurrentPort:{0}", port.PortID);
        //        }else
        //        {
        //            Logger.InfoFormat("[GetCurrentUnloadingPort] CurrentPort==null ");
        //        }
        //        return port;
        //        //var portList = ports.FindAll(p => p.PanelInfos.ToList().Exists(o => !o.IsExist && o.SlotSatus == (int)EnumGlassSlotStatus.E));
        //        //if (portList.Count > 0)
        //        //{
        //        //    foreach (var item in portList)
        //        //    {
        //        //        var exsitGlassCount = item.PanelInfos.Count(o => o.IsExist);
        //        //        Logger.InfoFormat("+++ {0} is ready for Unloading. exsitGlassCount:{1} ", item.UnitID, exsitGlassCount);
        //        //    }
        //        //    var port = portList.OrderByDescending(p => p.PanelInfos.Count(o => o.IsExist)).ToArray();

        //        //    var okPorts = port.First();
        //        //    Logger.InfoFormat("+++ Current unloading Port is {0}. ", okPorts.UnitID);
        //        //    return okPorts;
        //        //}
        //        //return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //    return null;
        //}

        //protected bool CheckPILensOut(GlassInfo spacerGlass)
        //{
        //    if (spacerGlass.GlassCateGory == EnumGlassCategory.Spacer)
        //    {
        //        var lensGlass = LineInfo.PILensGlassList.Find(o => o.GlassID == spacerGlass.PairComponentID);
        //        if (lensGlass == null) return false;
        //    }
        //    return true;
        //}
        public bool CheckLinkStatusReceive(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            try
            {
                if (link == null)
                {
                    Logger.InfoFormat("[CreateProcessPutCommand] come6");
                    Logger.InfoFormat("[CheckLinkStatusReceive] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                    return false;
                }
                //if (unit.CommandType == 1)
                //{
                //    Logger.InfoFormat("[CreateProcessPutCommand] come7");
                //    //Logger.InfoFormat("[CheckLinkStatusReceive][unit.CommandType==CCLINK]  unitid:{0}", unit.UnitID);
                //    if (unit.DownstreamInlineMode && !unit.LoadingStop)
                //    {
                //        Logger.InfoFormat("[CreateProcessPutCommand] come8");
                //        //robot SendAble 必须是putwait 之后，才能put
                //        return CheckDownStreamLink(link, unit, ref logStr);
                //    }
                //    else
                //    {
                //        Logger.InfoFormat("[CreateProcessPutCommand] come9");
                //        Logger.InfoFormat("[CheckLinkStatusReceive] - Check Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                //        var logInfo = string.Format("[CreateProcessPutCommand]   NG  Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                //        logStr.Append(logInfo);
                //        logStr.AppendLine();

                //    }
                //}
                //else if (unit.CommandType == 2)
                //{
                //    Logger.InfoFormat("[CreateProcessPutCommand] come10");
                    //Logger.InfoFormat("[CheckLinkStatusReceive][unit.CommandType==HSMS]  unitid:{0}", unit.UnitID);
                    return CheckDownStreamLink(link, unit, ref logStr);
                //}
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
            return false;
        }
        public bool CheckDownStreamLink(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            var DownstreamLinkSignal = (DownstreamLinkSignal)link.LinkSignalItem;
            //if (!robot.Linksignals[0].UpstreamLinkSignal.SendAble && link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble && (link.DownstreamLinkSignal.ReceiveAble && !link.DownstreamLinkSignal.ReceiveStart ))
            if (DownstreamLinkSignal.DownstreamInline && !DownstreamLinkSignal.DownstreamTrouble && (DownstreamLinkSignal.ReceiveAble && !DownstreamLinkSignal.ReceiveStart))
            {
                Logger.InfoFormat("[CheckLinkStatusReceive] - DownstreamLinkSignal OK; unitid:{0};LinkName:{1};DownstreamInline:{2},DownstreamTrouble:{3},ReceiveAble:{4},ReceiveStart:{5}",
                unit.UnitID, link.LinkName, DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble, DownstreamLinkSignal.ReceiveAble, DownstreamLinkSignal.ReceiveStart);
                //Logger.InfoFormat("[CreateProcessPutCommand]LinkSignal : {0} Status MisMatch (Next Target Receive)", link.UnitName);
                //Logger.InfoFormat("[CreateProcessPutCommand] Current ReceiveStatus => DownStreamInline : {0}, DownStreamTrouble :{1}",
                //DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble);
                return true;
            }
            Logger.InfoFormat("[CheckLinkStatusReceive] - DownstreamLinkSignal NG; unitid:{0};LinkName:{1};DownstreamInline:{2},DownstreamTrouble:{3},ReceiveAble:{4},ReceiveStart:{5}",
                unit.UnitID, link.LinkName, DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble, DownstreamLinkSignal.ReceiveAble, DownstreamLinkSignal.ReceiveStart);
            return false;
        }
        public bool CheckPutWaitLinkStatusReceive(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            try
            {
                if (link == null)
                {
                    Logger.InfoFormat("[CreateProcessPutCommand] come11");
                    Logger.InfoFormat("[CheckPutWaitLinkStatusReceive] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                    return false;
                }
                //if (unit.CommandType == 1)
                //{
                //    Logger.InfoFormat("[CreateProcessPutCommand] come12");
                //    if (unit.DownstreamInlineMode && !unit.LoadingStop)
                //    {
                //        Logger.InfoFormat("[CreateProcessPutCommand] come13");
                //        // Logger.InfoFormat("[CheckPutWaitLinkStatusReceive][unit.CommandType==CCLINK]  unitid:{0}", unit.UnitID);
                //        //robot SendAble 必须是putwait 之后，才能put
                //        return CheckPutWaitDownLink(link, unit, ref logStr);
                //        //Logger.InfoFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Receive)", link.UnitName );
                //        //Logger.InfoFormat("+++ Current ReceiveStatus => DownStreamInline : {0}, DownStreamTrouble :{1}",
                //        //    DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble);
                //    }
                //    else
                //    {
                //        Logger.InfoFormat("[CreateProcessPutCommand] come14");
                //        Logger.InfoFormat("[CheckPutWaitLinkStatusReceive] - Check Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                //        var logInfo = string.Format("[     GetWait Check]   NG  Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                //        logStr.Append(logInfo);
                //        logStr.AppendLine();
                //    }
                //}
                //else if (unit.CommandType == 2)
                //{
                    //Logger.InfoFormat("[CheckPutWaitLinkStatusReceive][unit.CommandType==HSMS]  unitid:{0}", unit.UnitID);
                    return CheckPutWaitDownLink(link, unit, ref logStr);
                //}
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
            return false;
        }
        public bool CheckPutWaitDownLink(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            if (link == null)
            {
                Logger.InfoFormat("[CheckPutWaitLinkStatusReceive] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                return false;
            }
            var DownstreamLinkSignal = (DownstreamLinkSignal)link.LinkSignalItem;
            //if (!robot.Linksignals[0].UpstreamLinkSignal.SendAble && link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble && (link.DownstreamLinkSignal.ReceiveAble && !link.DownstreamLinkSignal.ReceiveStart ))
            if (DownstreamLinkSignal.DownstreamInline
                && !DownstreamLinkSignal.DownstreamTrouble
                // && DownstreamLinkSignal.ReceiveReady
                && (!DownstreamLinkSignal.ReceiveAble && !DownstreamLinkSignal.ReceiveStart))
            {
                Logger.InfoFormat("[CheckPutWaitLinkStatusReceive] - DownstreamLinkSignal OK; unitid:{0};LinkName:{1};DownstreamInline:{2},DownstreamTrouble:{3},ReceiveReady:{4},ReceiveAble:{5},ReceiveStart:{6}",
                unit.UnitID, link.LinkName, DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble, DownstreamLinkSignal.ReceiveReady, DownstreamLinkSignal.ReceiveAble, DownstreamLinkSignal.ReceiveStart);
                //Logger.InfoFormat("[CreateProcessPutCommand]LinkSignal : {0} Status MisMatch (Next Target Receive)", link.UnitName);
                //Logger.InfoFormat("[CreateProcessPutCommand] Current ReceiveStatus => DownStreamInline : {0}, DownStreamTrouble :{1}",
                //DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble);
                return true;
            }
            Logger.InfoFormat("[CheckPutWaitLinkStatusReceive] - DownstreamLinkSignal NG; unitid:{0};LinkName:{1};DownstreamInline:{2},DownstreamTrouble:{3},ReceiveReady:{4},ReceiveAble:{5},ReceiveStart:{6}",
                unit.UnitID, link.LinkName, DownstreamLinkSignal.DownstreamInline, DownstreamLinkSignal.DownstreamTrouble, DownstreamLinkSignal.ReceiveReady, DownstreamLinkSignal.ReceiveAble, DownstreamLinkSignal.ReceiveStart);
            return false;
        }
        public bool CheckGetWaitLinkStatusSend(Linksignal link, string UnitName, ref StringBuilder logStr)
        {
            try
            {
                if (link == null)
                {
                    Logger.InfoFormat("[CheckGetWaitLinkStatusSend] - Pre Check NG UnitName={0},Link is null", UnitName);
                    return false;
                }
                var UpstreamLinkSignal = (UpstreamLinkSignal)link.LinkSignalItem;
                if (UpstreamLinkSignal.UpstreamInline
                    && !UpstreamLinkSignal.UpstreamTrouble
                    && UpstreamLinkSignal.SendReady
                    && !UpstreamLinkSignal.SendAble
                    && !UpstreamLinkSignal.SendStart)
                {
                    //Logger.InfoFormat("[CreateProcessGetCommand] LinkSignal : {0} Status MisMatch (Source Send) ", link.UnitName );
                    //Logger.InfoFormat("[CreateProcessGetCommand] Current Send Status => UpstreamInline : {0}, UpstreamTrouble :{1}, SendAble : {2}, SendStart : {3},",
                    //    UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart);
                    Logger.InfoFormat("[CheckGetWaitLinkStatusSend][UpstreamLinkSignal][UpLink Check OK] - UpstreamInline: {0}, UpstreamTrouble :{1},SendReady:{4}, SendAble : {2}, SendStart : {3},",
                        UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, UpstreamLinkSignal.SendReady);
                    return true;
                }
                Logger.InfoFormat("[CheckGetWaitLinkStatusSend][UpstreamLinkSignal][UpLink Check NG] - UpstreamInline: {0}, UpstreamTrouble :{1},SendReady:{4}, SendAble : {2}, SendStart : {3},",
                  UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, UpstreamLinkSignal.SendReady);
                //Logger.InfoFormat("+++ LinkSignal : {0} Status MisMatch (Source Send) ", link.UnitName );
                //Logger.InfoFormat("+++ Current Send Status => UpstreamInline : {0}, UpstreamTrouble :{1}, SendAble : {2}, SendStart : {3},",
                //    UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
            return false;
        }
        //public bool CheckLinkStatusPutWait(Linksignal link)
        //{
        //    try
        //    {
        //        if (link.UpstreamLinkSignal.UpstreamInline
        //            && !link.UpstreamLinkSignal.UpstreamTrouble
        //            && !link.UpstreamLinkSignal.SendAble
        //            && !link.UpstreamLinkSignal.SendStart)
        //            return true;
        //        Logger.InfoFormat("+++ LinkSignal : {0} Status MisMatch (Source Send) ", link.UnitName );
        //        Logger.InfoFormat("+++ Current Send Status => UpstreamInline : {0}, UpstreamTrouble :{1}, SendAble : {2}, SendStart : {3},",
        //            link.UpstreamLinkSignal.UpstreamInline, link.UpstreamLinkSignal.UpstreamTrouble, link.UpstreamLinkSignal.SendAble, link.UpstreamLinkSignal.SendStart);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return false;
        //}
        //public bool CheckLinkStatusReceiveAndExchange(Linksignal link)
        //{
        //    try
        //    {
        //        //if ((link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble && link.DownstreamLinkSignal.ReceiveAble && !link.DownstreamLinkSignal.ReceiveStart && link.DownstreamLinkSignal.StageInterlock) ||
        //        //    (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible && link.UpstreamLinkSignal.StageInterlock))
        //        if ((link.DownstreamLinkSignal.DownstreamInline && !link.DownstreamLinkSignal.DownstreamTrouble ) ||
        //            (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible ))
        //            return true;
        //        Logger.InfoFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Receive and Exchange)", link.UnitName );
        //        Logger.InfoFormat("+++ Current ReceiveStatus => DownStreamInline : {0}, DownStreamTrouble :{1}", link.DownstreamLinkSignal.DownstreamInline, link.DownstreamLinkSignal.DownstreamTrouble);
        //        Logger.InfoFormat("+++ Current ExchangeStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}", link.UpstreamLinkSignal.UpstreamInline, link.UpstreamLinkSignal.UpstreamTrouble, link.UpstreamLinkSignal.SendAble, link.UpstreamLinkSignal.SendStart, link.UpstreamLinkSignal.ExchangePossible);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return false;
        //}
        public bool CheckLinkStatusExchange(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            try
            {

                if (link == null)
                {
                    Logger.InfoFormat("[CheckLinkStatusExchange] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                    return false;
                }
                if (unit.CommandType == 1)
                {
                    if (unit.UpstreamInlineMode && !unit.LoadingStop)
                    {
                        //Logger.InfoFormat("[CheckLinkStatusExchange][unit.CommandType==CCLINK]  unitid:{0}", unit.UnitID);
                        return CheckUpLinkExchange(link, unit, ref logStr);
                    }
                    else
                    {
                        Logger.InfoFormat("[FindExchangeCommand] - Check Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                        var logInfo = string.Format("[    Exchange Check]   NG  Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                    }
                }
                else if (unit.CommandType == 2)
                {
                    //Logger.InfoFormat("[CheckLinkStatusExchange][unit.CommandType==HSMS]  unitid:{0}", unit.UnitID);
                    return CheckUpLinkExchange(link, unit, ref logStr);
                }
                //Logger.InfoFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Exchange)", link.UnitName);
                //Logger.InfoFormat("+++ Current ExchangeStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}", UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, UpstreamLinkSignal.ExchangePossible);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
            return false;
        }
        public bool CheckUpLinkExchange(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            if (link == null)
            {
                Logger.InfoFormat("[CheckLinkStatusExchange] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                return false;
            }
            var UpstreamLinkSignal = (UpstreamLinkSignal)link.LinkSignalItem;
            //if (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible && link.UpstreamLinkSignal.StageInterlock)
            if (UpstreamLinkSignal.UpstreamInline && !UpstreamLinkSignal.UpstreamTrouble && UpstreamLinkSignal.SendAble && !UpstreamLinkSignal.SendStart && UpstreamLinkSignal.ExchangePossible)
            {
                Logger.InfoFormat("[CheckLinkStatusExchange] - UpstreamLinkSignal OK; LinkName:{5} => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}", UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, UpstreamLinkSignal.ExchangePossible, link.UnitName);
                return true;
            }
            Logger.InfoFormat("[CheckLinkStatusExchange] - UpstreamLinkSignal NG; LinkName:{5} => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}", UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, UpstreamLinkSignal.ExchangePossible, link.UnitName);
            return false;
        }



        public bool CheckUpLinkStatus(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            try
            {
                if (link == null)
                {
                    Logger.InfoFormat("[CheckUpLinkStatus] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                    return false;
                }
                //if (unit.CommandType == 1)
                //{
                //    if (unit.UpstreamInlineMode && !unit.LoadingStop)
                //    {
                //        //Logger.InfoFormat("[CheckUpLinkStatus][unit.CommandType==CCLINK]  unitid:{0}", unit.UnitID);
                //        return CheckUpLink(link, unit, ref logStr);
                //    }
                //    else
                //    {
                //        Logger.InfoFormat("[CheckUpLinkStatus] - Check Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                //        var logInfo = string.Format("[CreateProcessGetCommand]   NG  Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", unit.UnitName, unit.UpstreamInlineMode, unit.LoadingStop);
                //        logStr.Append(logInfo);
                //        logStr.AppendLine();
                //    }
                //}
                //else if (unit.CommandType == 2)
                //{
                    //Logger.InfoFormat("[CheckUpLinkStatus][unit.CommandType==HSMS]  unitid:{0}", unit.UnitID);
                    return CheckUpLink(link, unit, ref logStr);
                //}
                //Logger.InfoFormat("+++ LinkSignal : {0} Status MisMatch (Next Target Exchange)", link.UnitName);
                //Logger.InfoFormat("+++ Current ExchangeStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}, ExchangePossible : {4}", UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, UpstreamLinkSignal.ExchangePossible);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
            return false;
        }
        public bool CheckUpLink(Linksignal link, Unit unit, ref StringBuilder logStr)
        {
            if (link == null)
            {
                Logger.InfoFormat("[CheckUpLinkStatus] - Pre Check NG UnitName={0},Link is null", unit.UnitName);
                return false;
            }
            var UpstreamLinkSignal = (UpstreamLinkSignal)link.LinkSignalItem;
            //if (link.UpstreamLinkSignal.UpstreamInline && !link.UpstreamLinkSignal.UpstreamTrouble && link.UpstreamLinkSignal.SendAble && !link.UpstreamLinkSignal.SendStart && link.UpstreamLinkSignal.ExchangePossible && link.UpstreamLinkSignal.StageInterlock)
            if (UpstreamLinkSignal.UpstreamInline && !UpstreamLinkSignal.UpstreamTrouble && UpstreamLinkSignal.SendAble && !UpstreamLinkSignal.SendStart)
            {
                Logger.InfoFormat("[CheckUpLinkStatus] - UpstreamLinkSignal OK; LinkName:{4} Current UpLinkStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}", UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, link.UnitName);
                return true;
            }
            Logger.InfoFormat("[CheckUpLinkStatus] - UpstreamLinkSignal NG; LinkName:{4} Current UpLinkStatus => UpStreamInline : {0}, UpStreamTrouble :{1}, SendAbleOn : {2}, SendStartOn : {3}", UpstreamLinkSignal.UpstreamInline, UpstreamLinkSignal.UpstreamTrouble, UpstreamLinkSignal.SendAble, UpstreamLinkSignal.SendStart, link.UnitName);
            return false;
        }
        /// <summary>
        /// 根据ModelPosition 获取 所在的 mode
        /// </summary>
        /// <param name="ModelPosition"></param>
        /// <returns></returns>
        public RobotModel GetCurrentModel(int ModelPosition)
        {
            try
            {
                //RobotModel robotModel = null;
                foreach (var unitItem in LineInfo.EQPInfo.Units)
                {
                    foreach (var RobotModelItem in unitItem.RobotModelList)
                    {
                        if (RobotModelItem.ModelPosition == ModelPosition)
                        {
                            // robotModel = RobotModelItem;
                            return RobotModelItem;
                            //break;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        public RobotModel GetCurrentModel(string portid)
        {
            try
            {
                //RobotModel robotModel = null;
                foreach (var unitItem in LineInfo.EQPInfo.Units)
                {
                    foreach (var RobotModelItem in unitItem.RobotModelList)
                    {
                        if (RobotModelItem.PortID == portid)
                        {
                            // robotModel = RobotModelItem;
                            return RobotModelItem;
                            //break;
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return null;
            }

        }
        //public RobotModel GetCurrentModel(int ModelPosition,ref Unit unit)
        //{
        //    try
        //    {
        //        RobotModel robotModel = null;
        //        foreach (var unitItem in LineInfo.EQPInfo.Units)
        //        {
        //            foreach (var RobotModelItem in unitItem.RobotModelList)
        //            {
        //                if (RobotModelItem.ModelPosition == ModelPosition)
        //                {
        //                    // robotModel = RobotModelItem;
        //                    unit = unitItem;
        //                    robotModel= RobotModelItem;
        //                    //break;
        //                }
        //            }
        //        }
        //        return robotModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}
        //public SUnit GetOutUnit(Unit OutUnit, GlassInfo info)
        //{
        //    try
        //    {
        //        var outSUnit = OutUnit.SUnitList.FirstOrDefault(o => o.SUnitID == info.OutSUnitName || o.SUnitName == info.OutSUnitName||o.SUnitPathNo.ToString()==info.OutSUnitName);
        //        if (outSUnit == null)
        //        {
        //            Logger.InfoFormat("+++ Cannot Find Unit by OutUnitName: {0} for Glass {1} In EQPInfo: {2} ", info.OutSUnitName, info.GLSID, OutUnit.UnitName);
        //            return null;
        //        }
        //        return outSUnit;
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //        return null;
        //    }

        //}
        public string GetString(string val)
        {
            try
            {
                return string.IsNullOrEmpty(val) ? "" : val;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return "";
            }

        }
        public List<JobStage> FindNextStage(GlassInfo glass)
        {
            try
            {
                if (glass == null)
                {
                    Logger.Info("[FindNextStage] Can not Found NextStage ");
                    return null;
                }
                List<JobStage> jobStages = new List<JobStage>();
                if (string.IsNullOrEmpty(glass.ModePath))
                {
                    //Logger.InfoFormat("[FindNextStage] Check info.ModePath NG; glass.ModePath=null;glass[{0},{1}]", glass.CassetteSequenceNo, glass.SlotSequenceNo);
                    return null;
                }
                var PathConfigureList = Configure.GetPathConfigureList(glass.ModePath, glass.ModelPosition, "", Logger);
                //Logger.Info("[FindNextStage]Find PathConfigureList Count: {0} ");
                foreach (var PathConfigure in PathConfigureList)
                {
                    //if (!Configure.GroupList.Exists(o => o.Name == cfg.TargetPathName))
                    var JobStage = StageList.Find(o => o.ModelPosition == PathConfigure.TargetPathName);
                    if (JobStage != null)
                    {
                        JobStage.PathConfigure = PathConfigure;

                        //Logger.InfoFormat("[FindNextStage]Find Success, NextJobStage  UnitType{0}, ModelPosition{1}, UnitName:{2}", JobStage.Type.ToString(), JobStage.ModelPosition, JobStage.UnitName);
                        //return eqp;
                        jobStages.Add(JobStage);
                    }
                    //var group=Configure.GroupList.FirstOrDefault(o=>o.Name==cfg.TargetPathName);
                    //if (!cfg.RobotFixed || (cfg.RobotFixed && cfg.RobotArm == hand))
                    //{


                    //}
                    //else
                    //{
                    //    Logger.InfoFormat("[FindNextStage] Can not Found Path, Path: RobotFixed {0}, RobotArm {1}, Now RobotHand:{2};pathName:{3}", cfg.RobotFixed, cfg.RobotArm, hand.ToString(),cfg.Name);
                    //}
                }
                return jobStages;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
                return null;
            }
        }
        //public List<JobStage> FindNextStage(int CurrentModelPosition, string ruleID, string   ModePath)
        //{
        //   // Logger.InfoFormat("[FindNextStage]Start  CurrentModelPosition:{0},ruleID:{1} ", CurrentModelPosition, ruleID);
        //    var list = new List<JobStage>();
        //    try
        //    {
        //        if (CurrentModelPosition == 0)
        //        {
        //            Logger.Info("[FindNextStage] Can not Found NextStage due to unit name is null ");
        //            return list;
        //        }

        //        var configurelist = Configure.GetPathConfigureList(ModePath, CurrentModelPosition, ruleID, Logger);
        //        //Logger.InfoFormat("[FindNextStage] Configurelist Count:{0} ", configurelist.Count);
        //        if (configurelist == null || configurelist.Count == 0)
        //        {
        //            Logger.InfoFormat("[FindNextStage] Can not Found NextStage, OutUnit: {0}, RuleID: {1} ", CurrentModelPosition, GetString(ruleID));
        //            return list;
        //        }
        //        foreach (var cfg in configurelist)
        //        {
        //            //if (!Configure.GroupList.Exists(o => o.Name == cfg.TargetPathName))
        //            //{
        //                var stage = StageList.Find(o => o.ModelPosition == cfg.TargetPathName);
        //                if (stage != null)
        //                {
        //                    stage.PathConfigure = cfg;
        //                    list.Add(stage);
        //                }
        //           // }
        //            //else
        //            //{
        //            //    var groups = Configure.GroupList.Find(o => o.Name == cfg.TargetPathName);
        //            //    if (groups.List != null && groups.List.Count > 0)
        //            //    {
        //            //        foreach (var stagename in groups.List)
        //            //        {
        //            //            var s = StageList.Find(o => o.Name == stagename);
        //            //            if (s != null)
        //            //            {
        //            //                s.PathConfigure = cfg;
        //            //                list.Add(s);
        //            //            }
        //            //        }
        //            //    }
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //    if (list.Count == 0)
        //    {
        //        Logger.InfoFormat("[FindNextStage] Cannot Find Target, CurrentModelPosition: {0}, RuleID: {1} ", CurrentModelPosition, GetString(ruleID));
        //    }
        //    return list;
        //}
        public static int CompareGlassInfoBySlot(GlassInfo info1, GlassInfo info2)
        {
            //var info1slot = info1.SlotNo;
            //var info2slot = info2.SlotNo;
            //if (info1slot == info2slot)  //相等
            //    return 0;
            //if (info1slot > info2slot) //大于
            //    return 1;

            //小于
            return -1;
        }

        //internal RobotHand PortGetHandCheck(Robot robot, RobotPathConfigure cfg)
        //{
        //    var robotHand = RobotHand.Error;
        //    try
        //    {
        //        if (!cfg.RobotFixed)
        //        {
        //            if (!robot.LowerExistOn )
        //                robotHand = RobotHand.LowHand;
        //            else if (!robot.UpperExistOn)
        //                robotHand = RobotHand.UpHand;
        //        }
        //        //UpperExistOn:检查下手臂是否存在玻璃；UpHandEnable检查上手臂是否能用
        //        else if (cfg.RobotArm == RobotHand.LowHand && !robot.LowerExistOn)
        //        {
        //            robotHand = RobotHand.LowHand;
        //        }
        //        //LowerExistOn:检查上手臂是否存在玻璃；LowHandEnable检查下手臂是否能用
        //        else if (cfg.RobotArm == RobotHand.UpHand && !robot.UpperExistOn)
        //            robotHand = RobotHand.UpHand;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //    return robotHand;
        //}
        //internal RobotHand PathHandCheck(Robot robot, RobotPathConfigure cfg, Linksignal link)
        //{
        //    var upStreamLink = link.UpstreamLinkSignal;
        //    var robotHand = RobotHand.Error;
        //    try
        //    {
        //        if (cfg == null)
        //        {
        //            return robotHand;
        //        }
        //        if (!cfg.RobotFixed)
        //        {
        //            if (!robot.Lower1ExistOn)
        //                robotHand = RobotHand.LowHand;
        //            else if (!robot.Upper1ExistOn)
        //                robotHand = RobotHand.UpHand;
        //        }
        //        else if (cfg.RobotArm == RobotHand.LowHand && !robot.Lower1ExistOn)
        //        {
        //            robotHand = RobotHand.LowHand;
        //        }
        //        else if (cfg.RobotArm == RobotHand.UpHand && !robot.Upper1ExistOn)
        //            robotHand = RobotHand.UpHand;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return robotHand;
        //}
        /// <summary>
        /// 根据手臂是否有玻璃，以及上游设备的上下层是否有玻璃，获取可用的手臂
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        //internal RobotHand ProcessGetHandCheck(Robot robot, RobotPathConfigure cfg)
        //{
        //    var robotHand = RobotHand.Error;
        //    try
        //    {
        //        if (cfg == null)
        //        {
        //            return robotHand;
        //        }
        //        if (!cfg.RobotFixed)//如果没限制robot手臂使用
        //        {
        //            if (!robot.LowerExistOn)
        //                robotHand = RobotHand.LowHand;
        //            else if (!robot.UpperExistOn)
        //                robotHand = RobotHand.UpHand;
        //        }
        //        else if (cfg.RobotArm == RobotHand.LowHand && !robot.LowerExistOn)//如果是下手臂 并且下手臂没玻璃、 用下手臂
        //        {
        //            robotHand = RobotHand.LowHand;
        //        }
        //        else if (cfg.RobotArm == RobotHand.UpHand && !robot.UpperExistOn)
        //            robotHand = RobotHand.UpHand;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //    return robotHand;
        //}
        /// <summary>
        /// 根据上下游要片层数，以及Robot上下手臂是否有glass，获取送片手臂
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        //internal RobotHand ProcessPutHandCheck(Robot robot, Linksignal link)
        //{
        //    var downStreamLink = link.DownstreamLinkSignal;
        //    var robotHand = RobotHand.Error;
        //    try
        //    {
        //        //判断下游设备link，判断哪层request
        //        downStreamLink.UpGlass = true;
        //        downStreamLink.LowGlass = true;
        //        if (downStreamLink.UpGlass && downStreamLink.LowGlass)
        //        {
        //            robotHand = RobotHand.DoubleHand;
        //        }
        //        else if (downStreamLink.UpGlass)
        //        {
        //            robotHand = RobotHand.UpHand;
        //        }
        //        else if (downStreamLink.LowGlass)
        //        {
        //            robotHand = RobotHand.LowHand;
        //        }
        //        else
        //        {
        //            robotHand = RobotHand.Error;
        //        }
        //        //判断手臂是否有glass
        //        if (robotHand == RobotHand.LowHand)
        //        {
        //            robotHand = robot.LowerExistOn ? RobotHand.LowHand : RobotHand.Error;
        //        }
        //        else if (robotHand == RobotHand.UpHand)
        //        {
        //            robotHand = robot.UpperExistOn ? RobotHand.UpHand : RobotHand.Error;
        //        }
        //        else if (robotHand == RobotHand.DoubleHand)
        //        {
        //            if (robot.LowerExistOn && robot.UpperExistOn)
        //                robotHand = RobotHand.DoubleHand;
        //            else if (robot.UpperExistOn)
        //                robotHand = RobotHand.UpHand;
        //            else if (robot.LowerExistOn)
        //                robotHand = RobotHand.LowHand;
        //            else
        //                robotHand = RobotHand.Error;
        //        }
        //        else
        //        {
        //            //RobotHand.Error
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return robotHand;
        //}
        //internal RobotHand UnitOutHandCheck(Robot robot, RobotPathConfigure path)
        //{
        //    try
        //    {
        //        if (path.RobotFixed)
        //        {
        //            if (path.RobotArm == RobotHand.LowHand && !robot.LowerExistOn)
        //            {
        //                return RobotHand.LowHand;
        //            }
        //            if (path.RobotArm == RobotHand.UpHand && !robot.UpperExistOn)
        //            {
        //                return RobotHand.UpHand;
        //            }
        //            return RobotHand.Error;
        //        }
        //        if (!robot.LowerExistOn)
        //        {
        //            return RobotHand.LowHand;
        //        }
        //        if (!robot.UpperExistOn)
        //        {
        //            return RobotHand.UpHand;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }

        //    return RobotHand.Error;
        //}
        protected void UpdateOutUnitBeforeGet(SPanelInfo info, string eqpName, string unitName)
        {
            //if (info.OutEQPName != eqpName || info.OutUnitName != unitName)
            //    Logger.WarnFormat("+++ Before Get Command: After Update, Glass {0} OutEQPName
            //{
            //    Logger.WarnFormat("+++ Before Get Command: Glass {0} OutEQPName: {1}, OutUnitName:{2} ", info.PanelID, info.OutEQPName, info.OutUnitName);
            //    info.OutEQPName = eqpName;
            //    info.OutUnitName = unitName;: {1}, OutUnitName:{2} ", info.PanelID, info.OutEQPName, info.OutUnitName);
            //}
        }



        protected void AddCommand(RobotCommand cmd)
        {
            try
            {
                cmdList.Add(cmd);
                Logger.InfoFormat("[AddCommand] Unit {1} Command: {0}", cmd, cmd.Unit.UnitName);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        internal List<JobStage> CheckEQPByPass(Unit outUnit, JobStage targetstage, GlassInfo panelIndo)
        {
            var list = new List<JobStage>();
            try
            {
                var targetUnit = LineInfo.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetstage.UnitName);
                //if (targetEQP.ByPass)
                //{
                //    Logger.InfoFormat("+++ CheckEQPByPass GlassID: {0}, OutUnit: {1}, TargetUnit: {2}, TargetEQP: {3} ByPass is true. ", glassInfo.GlassID, outUnit.UnitName, targetstage.Name, targetEQP.EQPName);
                //    var rList = FindNextUnit(outUnit.UnitName, ConstDef.BY_PASS);
                //    if (rList.Count > 0)
                //    {
                //        foreach (var stage in rList)
                //        {
                //            list.Add(stage);
                //            Logger.InfoFormat("+++ CheckEQPByPass GlassID: {0}, OutUnit: {1}, OldTarget: {2}, NewTarget: {3} ", glassInfo.GlassID, outUnit.UnitName, targetstage.Name, stage.Name);
                //        }
                //        return list;
                //    }
                //    else
                //    {
                //        Logger.InfoFormat("+++ CheckEQPByPass Error, No ByPass Path Configure is find. Glass ID: {0} ", glassInfo.GlassID);
                //        return list;
                //    }
                //}
                //else
                //{
                list.Add(targetstage);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

            return list;
            //}
        }
    }
}
