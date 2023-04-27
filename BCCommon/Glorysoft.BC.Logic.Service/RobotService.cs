using Glorysoft.Auto.Contract;
using Glorysoft.BC.Db.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.EQP.Contract;
using Glorysoft.BC.GlassDispath;
using Glorysoft.BC.Logic.Contract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glorysoft.BC.Entity.WebSocketEntity;
using Glorysoft.BC.EIP;
using System.Threading;
using System.Web.Script.Serialization;

namespace Glorysoft.BC.Logic.Service
{
    public class RobotService : AbstractEventHandler, IRobotService
    {
        #region 单例
        public RobotService()
        {
        }

        private static readonly Lazy<RobotService> Lazy = new Lazy<RobotService>(() => new RobotService());

        public static RobotService Current
        {
            get
            {
                return Lazy.Value;
            }
        }
        #endregion

        private readonly List<AbstractDispatch> list = new List<AbstractDispatch>();
        protected static readonly IEQPCommandService eqpCmd = CommonContexts.ResolveInstance<IEQPCommandService>();
        public void Start(string jsonfile)
        {
            try
            {
                Stop();
                list.Clear();
                if (HostInfo.Current.EQPInfo != null)
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("EQPID", HostInfo.Current.EQPInfo.EQPID);
                    var list1 = dbRobot.ViewRobotConfigure(ht);
                    //var list2 = dbRobot.ViewGroupConfigure(ht);
                    var list3 = dbRobot.ViewRobotPathConfigure(ht);
                    var listdic = RobotConfigureManagement.Current.LoadDbConfigure(list1, list3);
                    foreach (var name in RobotConfigureManagement.Current.RobotList)
                    {

                        var robot = new NormalDispatch(name);
                        robot.OnSendRobotCommand += robot_OnSendRobotCommand;//注册OnSendRobotCommand
                        robot.OnModifyDBOldPriority += ModifyDBOldPriority;
                        HostInfo.Current.DispatchList.Add(robot);
                        robot.Initialize();//初始化派工
                        robot.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        void ModifyDBOldPriority()
        {
            try
            {
                //dbConfig.UpdateCFGOLDPriority(HostInfo.OldPriority);
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
        }
        /// <summary>
        ///  bc发送 Robot Control Command命令给robot 
        /// </summary>
        /// <param name="cmd"></param>
        void robot_OnSendRobotCommand(RobotCommand cmd)
        {
            try
            {
                var indexer = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == cmd.Name);
                //var robot = indexer.SUnitList.Find(o => o.UnitType == (int)EnumUnitType.Robot) as Robot;
                var robot = indexer as Robot;
                //var robot = new Robot();
                robot.IsWaitCmdCode = true;
                System.Threading.Thread.Sleep(200);
                var transactionID = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000000, 9999999);
                cmd.TransactionID = transactionID;
                var sequenceno = HostInfo.Current.SequenceNo;
                cmd.SequenceNo = sequenceno;
                robot.RobotCommandList.Add(cmd);
                LogHelper.BCLog.Debug($"add robot cmd {cmd.SequenceNo}");
                CancellationObject cancellationObject = new CancellationObject();
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                cancellationObject.CancellationTokenSource = cancellationTokenSource;
                HostInfo.CancellationObjectDic.TryAdd(transactionID, cancellationObject);
                eqpService.SendRobotControlCommand(cmd.Name, indexer.LocalNo.ToString(), cmd, transactionID);
                robot.ExecCommand = cmd;
                if (cancellationTokenSource.Token.WaitHandle.WaitOne(5000))
                {
                    //设备正常回复,无需处理
                }
                else
                {
                    robot.RobotCommandList.Remove(cmd);
                    LogHelper.BCLog.Debug($"remove robot cmd {cmd.SequenceNo} EQP reply timeout");
                    LogHelper.EIPLog.ErrorFormat("+++ robot_OnSendRobotCommand:{0} EQP Reply Timeout +++", transactionID);
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        public void Stop()
        {
            try
            {
                foreach (var dispatch in HostInfo.Current.DispatchList)
                {
                    (dispatch as AbstractDispatch).Stop();
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        //public void CommandExecuteResultReport(string name, RobotCommandResult result)
        //{
        //    try
        //    {
        //        lock (HostInfo.Current.DispatchList)
        //        {
        //            AbstractDispatch dispatch = null;
        //            HostInfo.Current.DispatchList.ForEach(p =>
        //            {
        //                var tempdispatch = p as AbstractDispatch;
        //                if (tempdispatch.Name == name)
        //                {
        //                    dispatch = tempdispatch;
        //                }
        //            });
        //            if (dispatch != null)
        //            {
        //                try
        //                {
        //                    //更新Robot上玻璃数据
        //                    var indexer = HostInfo.EQPInfo.Units.FirstOrDefault((o => o.UnitName == name)); ;
        //                    //var robot = indexer.SUnitList.Find(o => o.SUnitType == (int)EnumUnitType.Robot) as Robot;
        //                    var robot = indexer  as Robot;
        //                    robot.UpperJobId = result.UpperJobId.Trim();//上手臂panelid
        //                    dispatch.Logger.DebugFormat("Upper Hand GlassId: {0}", robot.UpperJobId);
        //                    var UpperInfo = PanelInfo(robot.UpperJobId);
        //                    if (UpperInfo != null)
        //                    {
        //                        robot.UpHandGlass = UpperInfo;
        //                        dispatch.Logger.DebugFormat("Find Upper Hand Glass Data from Glass List: Glass Id: {0}", robot.UpperJobId);
        //                    }
        //                    else
        //                    {
        //                        robot.UpHandGlass = null;
        //                        dispatch.Logger.DebugFormat("Can Not Find Upper Hand Glass Data From GlassInfo,GlassId:{0}", robot.UpperJobId);
        //                    }
        //                    robot.LowerJobId = result.LowerJobId;
        //                    dispatch.Logger.DebugFormat("Lower Hand GlassId: {0}", robot.LowerJobId);
        //                    var LowerInfo = PanelInfo(robot.LowerJobId);
        //                    if (LowerInfo != null)
        //                    {
        //                        robot.LowHandGlass = LowerInfo;
        //                        dispatch.Logger.DebugFormat("Find Lower Hand Glass Data from Glass List: Glass Id: {0}", robot.LowerJobId);
        //                    }
        //                    else
        //                    {
        //                        robot.LowHandGlass = null;
        //                        dispatch.Logger.DebugFormat("Can Not Find Lower Hand Glass Data From GlassInfo,GlassId:{0}", robot.LowerJobId);
        //                    }
        //                    dispatch.Logger.DebugFormat("Command Result Code: {0}", result.ResultCode);

        //                }
        //                catch (Exception ex)
        //                {
        //                    dispatch.Logger.Error(ex);
        //                }

        //                dispatch.CommandExecuteResultReport(result);
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }

        //}
        public void RobotCommandFetchOutReport(string name, string commandSequenceNumber)
        {
            var indexer = HostInfo.EQPInfo.Units.FirstOrDefault((o => o.UnitName == name)); ;
            //var robot = indexer.SUnitList.Find(o => o.SUnitType == (int)EnumUnitType.Robot) as Robot;
            var robot = indexer as Robot;
            if (robot != null)
            {
                var cmd = robot.RobotCommandList.FirstOrDefault(o => o.SequenceNo.ToString() == commandSequenceNumber);
                if (cmd != null)
                {
                    cmd.Excuting = true;
                    LogHelper.BCLog.Debug($"robot cmd {commandSequenceNumber} is excuting");
                }
                else
                    LogHelper.BCLog.Debug($"RobotCommandFetchOutReport cmd {commandSequenceNumber} not found");
            }
            else
                LogHelper.BCLog.Debug($"RobotCommandFetchOutReport robot data {name} not found");
        }
        public void RobotArmMonitoringReport(string name, RobotCommandResult result)
        {
            try
            {
                lock (HostInfo.Current.DispatchList)
                {
                    AbstractDispatch dispatch = null;
                    HostInfo.Current.DispatchList.ForEach(p =>
                    {
                        var tempdispatch = p as AbstractDispatch;
                        if (tempdispatch.Name == name)
                        {
                            dispatch = tempdispatch;
                        }
                    });
                    if (dispatch != null)
                    {
                        try
                        {
                            LogHelper.BCLog.Debug("come1");
                            //更新Robot上玻璃数据
                            var indexer = HostInfo.EQPInfo.Units.FirstOrDefault((o => o.UnitName == name)); ;
                            //var robot = indexer.SUnitList.Find(o => o.SUnitType == (int)EnumUnitType.Robot) as Robot;
                            var robot = indexer as Robot;
                            try
                            {
                                #region 处理手臂StoreIn操作
                                if (robot.LocalNo != 2)//unloader
                                {
                                    LogHelper.BCLog.Debug($"robot.LocalNo {robot.LocalNo}");
                                    #region 下手臂前片 StoreIn
                                    LogHelper.BCLog.Debug($"robot.LowerCassetteSequenceNo1 {robot.LowerCassetteSequenceNo1} robot.LowerSlotSequenceNo1 {robot.LowerSlotSequenceNo1} result.LowerCassetteSequenceNo1 {result.LowerCassetteSequenceNo1} result.LowerSlotSequenceNo1 {result.LowerSlotSequenceNo1}");
                                    if ((robot.LowerCassetteSequenceNo1 != 0 && robot.LowerSlotSequenceNo1 != 0) && (result.LowerCassetteSequenceNo1 == 0 && result.LowerSlotSequenceNo1 == 0))
                                    {
                                        var cmd = robot.RobotCommandList.FirstOrDefault(o => o.Excuting);
                                        if (cmd != null)
                                        {
                                            LogHelper.BCLog.Debug($"cmd.status Excuting");
                                            //PutSlotNumber 层数 = STPutSlotNo1  PutSlotPosition 1前 2后 = STPutSlotPostion1
                                            LogHelper.BCLog.Debug($"cmd.STPutSlotNo1 {cmd.STPutSlotNo1} STPutSlotPostion1 {cmd.STPutSlotPostion1}");
                                            var port = cmd.Unit as PortInfo;
                                            var Position = cmd.STPutSlotNo1;
                                            //var SlotSequenceNo = (cmd.STPutSlotPostion1 * 1000) + cmd.STPutSlotNo1;
                                            var SlotPosition = cmd.STPutSlotPostion1 == 99 ? 2: cmd.STPutSlotPostion1; //99时robot上的1进去Port就是后片2
                                            LogHelper.BCLog.Debug($"Position {Position} SlotPosition {SlotPosition}");
                                            var glass = PanelInfo(indexer, robot.LowerCassetteSequenceNo1, robot.LowerSlotSequenceNo1);
                                            if (glass != null)
                                            {
                                                LogHelper.BCLog.Debug($"glass found {glass.GlassID}");
                                                PortInfo Inportinfo = null;
                                                if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID)))
                                                {
                                                    Inportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID));
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Inportinfo not found");
                                                var Outportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == port.EQPID && c.UnitID == port.UnitID && c.PortNo == Convert.ToInt32(port.PortNo));
                                                //更新panel信息
                                                if (Outportinfo != null)
                                                {
                                                    if (!glass.IsStoreIn)
                                                    {
                                                        //更新wip
                                                        glass.FunctionName = "RobotArmMonitorStoreIn";
                                                        glass.CurrentUnit = Outportinfo.UnitID;
                                                        glass.CurrentSUnit = Outportinfo.UnitID + "-" + Outportinfo.PortID;
                                                        glass.CassetteID = Outportinfo.CassetteID;
                                                        //glass.CassetteSequenceNo = Outportinfo.CassetteSequenceNo;
                                                        //glass.SlotSequenceNo = SlotSequenceNo;
                                                        glass.SlotPosition = SlotPosition;
                                                        glass.Position = Position;
                                                        glass.PortID = Outportinfo.PortID;
                                                        glass.OutCSTID = glass.CassetteID;
                                                        glass.OutPortID = Outportinfo.PortID;
                                                        glass.IsStoreIn = true;
                                                        dbService.UpdateGlassInfo(glass);

                                                        //缓存数据移动到对应的Port
                                                        Outportinfo.GlassInfos.Add(glass);
                                                        if (Inportinfo != null)
                                                            Inportinfo.GlassInfos.Remove(glass);
                                                    }
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Outportinfo not found");
                                            }
                                        }
                                        else
                                            LogHelper.BCLog.Debug($"cmd.status not Excuting");
                                    }
                                    #endregion
                                    #region 下手臂后片 StoreIn
                                    LogHelper.BCLog.Debug($"robot.LowerCassetteSequenceNo2 {robot.LowerCassetteSequenceNo2} robot.LowerSlotSequenceNo2 {robot.LowerSlotSequenceNo2} result.LowerCassetteSequenceNo2 {result.LowerCassetteSequenceNo2} result.LowerSlotSequenceNo2 {result.LowerSlotSequenceNo2}");
                                    if ((robot.LowerCassetteSequenceNo2 != 0 && robot.LowerSlotSequenceNo2 != 0) && (result.LowerCassetteSequenceNo2 == 0 && result.LowerSlotSequenceNo2 == 0))
                                    {
                                        var cmd = robot.RobotCommandList.FirstOrDefault(o => o.Excuting);
                                        if (cmd != null)
                                        {
                                            LogHelper.BCLog.Debug($"cmd.status Excuting");
                                            //PutSlotNumber 层数 = STPutSlotNo1  PutSlotPosition 1前 2后 = STPutSlotPostion1
                                            LogHelper.BCLog.Debug($"cmd.STPutSlotNo1 {cmd.STPutSlotNo1} STPutSlotPostion1 {cmd.STPutSlotPostion1}");
                                            var port = cmd.Unit as PortInfo;
                                            var Position = cmd.STPutSlotNo1;
                                            //var SlotSequenceNo = (cmd.STPutSlotPostion1 * 1000) + cmd.STPutSlotNo1;
                                            var SlotPosition = cmd.STPutSlotPostion1 == 99 ? 1 : cmd.STPutSlotPostion1; //99时robot上的2进去Port就是前片1
                                            LogHelper.BCLog.Debug($"Position {Position} SlotPosition {SlotPosition}");
                                            var glass = PanelInfo(indexer, robot.LowerCassetteSequenceNo2, robot.LowerSlotSequenceNo2);
                                            if (glass != null)
                                            {
                                                LogHelper.BCLog.Debug($"glass found {glass.GlassID}");
                                                PortInfo Inportinfo = null;
                                                if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID)))
                                                {
                                                    Inportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID));
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Inportinfo not found");
                                                var Outportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == port.EQPID && c.UnitID == port.UnitID && c.PortNo == Convert.ToInt32(port.PortNo));
                                                //更新panel信息
                                                if (Outportinfo != null)
                                                {
                                                    //更新wip
                                                    glass.FunctionName = "RobotArmMonitorStoreIn";
                                                    glass.CurrentUnit = Outportinfo.UnitID;
                                                    glass.CurrentSUnit = Outportinfo.UnitID + "-" + Outportinfo.PortID;
                                                    glass.CassetteID = Outportinfo.CassetteID;
                                                    //glass.CassetteSequenceNo = Outportinfo.CassetteSequenceNo;
                                                    //glass.SlotSequenceNo = SlotSequenceNo;
                                                    glass.SlotPosition = SlotPosition;
                                                    glass.Position = Position;
                                                    glass.PortID = Outportinfo.PortID;
                                                    glass.OutCSTID = glass.CassetteID;
                                                    glass.OutPortID = Outportinfo.PortID;
                                                    dbService.UpdateGlassInfo(glass);

                                                    //缓存数据移动到对应的Port
                                                    Outportinfo.GlassInfos.Add(glass);
                                                    if (Inportinfo != null)
                                                        Inportinfo.GlassInfos.Remove(glass);
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Outportinfo not found");
                                            }
                                        }
                                        else
                                            LogHelper.BCLog.Debug($"cmd.status not Excuting");
                                    }
                                    #endregion
                                    #region 上手臂前片 StoreIn
                                    LogHelper.BCLog.Debug($"robot.UpperCassetteSequenceNo1 {robot.UpperCassetteSequenceNo1} robot.UpperSlotSequenceNo1 {robot.UpperSlotSequenceNo1} result.UpperCassetteSequenceNo1 {result.UpperCassetteSequenceNo1} result.UpperSlotSequenceNo1 {result.UpperSlotSequenceNo1}");
                                    if ((robot.UpperCassetteSequenceNo1 != 0 && robot.UpperSlotSequenceNo1 != 0) && (result.UpperCassetteSequenceNo1 == 0 && result.UpperSlotSequenceNo1 == 0))
                                    {
                                        var cmd = robot.RobotCommandList.FirstOrDefault(o => o.Excuting);
                                        if (cmd != null)
                                        {
                                            LogHelper.BCLog.Debug($"cmd.status Excuting");
                                            //PutSlotNumber 层数 = STPutSlotNo1  PutSlotPosition 1前 2后 = STPutSlotPostion1
                                            LogHelper.BCLog.Debug($"cmd.STPutSlotNo1 {cmd.STPutSlotNo1} STPutSlotPostion1 {cmd.STPutSlotPostion1}");
                                            var port = cmd.Unit as PortInfo;
                                            var Position = cmd.STPutSlotNo1;
                                            //var SlotSequenceNo = (cmd.STPutSlotPostion1 * 1000) + cmd.STPutSlotNo1;
                                            var SlotPosition = cmd.STPutSlotPostion1 == 99 ? 2 : cmd.STPutSlotPostion1; //99时robot上的1进去Port就是后片2
                                            LogHelper.BCLog.Debug($"Position {Position} SlotPosition {SlotPosition}");
                                            var glass = PanelInfo(indexer, robot.UpperCassetteSequenceNo1, robot.UpperSlotSequenceNo1);
                                            if (glass != null)
                                            {
                                                LogHelper.BCLog.Debug($"glass found {glass.GlassID}");
                                                PortInfo Inportinfo = null;
                                                if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID)))
                                                {
                                                    Inportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID));
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Inportinfo not found");
                                                var Outportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == port.EQPID && c.UnitID == port.UnitID && c.PortNo == Convert.ToInt32(port.PortNo));
                                                //更新panel信息
                                                if (Outportinfo != null)
                                                {
                                                    //更新wip
                                                    glass.FunctionName = "RobotArmMonitorStoreIn";
                                                    glass.CurrentUnit = Outportinfo.UnitID;
                                                    glass.CurrentSUnit = Outportinfo.UnitID + "-" + Outportinfo.PortID;
                                                    glass.CassetteID = Outportinfo.CassetteID;
                                                    //glass.CassetteSequenceNo = Outportinfo.CassetteSequenceNo;
                                                    //glass.SlotSequenceNo = SlotSequenceNo;
                                                    glass.SlotPosition = SlotPosition;
                                                    glass.Position = Position;
                                                    glass.PortID = Outportinfo.PortID;
                                                    glass.OutCSTID = glass.CassetteID;
                                                    glass.OutPortID = Outportinfo.PortID;
                                                    dbService.UpdateGlassInfo(glass);

                                                    //缓存数据移动到对应的Port
                                                    Outportinfo.GlassInfos.Add(glass);
                                                    if (Inportinfo != null)
                                                        Inportinfo.GlassInfos.Remove(glass);
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Outportinfo not found");
                                            }
                                        }
                                        else
                                            LogHelper.BCLog.Debug($"cmd.status not Excuting");
                                    }
                                    #endregion
                                    #region 上手臂后片 StoreIn
                                    LogHelper.BCLog.Debug($"robot.UpperCassetteSequenceNo2 {robot.UpperCassetteSequenceNo2} robot.UpperSlotSequenceNo2 {robot.UpperSlotSequenceNo2} result.UpperCassetteSequenceNo2 {result.UpperCassetteSequenceNo2} result.UpperSlotSequenceNo2 {result.UpperSlotSequenceNo2}");
                                    if ((robot.UpperCassetteSequenceNo2 != 0 && robot.UpperSlotSequenceNo2 != 0) && (result.UpperCassetteSequenceNo2 == 0 && result.UpperSlotSequenceNo2 == 0))
                                    {
                                        var cmd = robot.RobotCommandList.FirstOrDefault(o => o.Excuting);
                                        if (cmd != null)
                                        {
                                            LogHelper.BCLog.Debug($"cmd.status Excuting");
                                            //PutSlotNumber 层数 = STPutSlotNo1  PutSlotPosition 1前 2后 = STPutSlotPostion1
                                            LogHelper.BCLog.Debug($"cmd.STPutSlotNo1 {cmd.STPutSlotNo1} STPutSlotPostion1 {cmd.STPutSlotPostion1}");
                                            var port = cmd.Unit as PortInfo;
                                            var Position = cmd.STPutSlotNo1;
                                            //var SlotSequenceNo = (cmd.STPutSlotPostion1 * 1000) + cmd.STPutSlotNo1;
                                            var SlotPosition = cmd.STPutSlotPostion1 == 99 ? 1 : cmd.STPutSlotPostion1; //99时robot上的2进去Port就是前片1
                                            LogHelper.BCLog.Debug($"Position {Position} SlotPosition {SlotPosition}");
                                            var glass = PanelInfo(indexer, robot.UpperCassetteSequenceNo2, robot.UpperSlotSequenceNo2);
                                            if (glass != null)
                                            {
                                                LogHelper.BCLog.Debug($"glass found {glass.GlassID}");
                                                PortInfo Inportinfo = null;
                                                if (HostInfo.Current.PortList.Any(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID)))
                                                {
                                                    Inportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.GlassInfos.Any(d => d.GlassID == glass.GlassID));
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Inportinfo not found");
                                                var Outportinfo = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == port.EQPID && c.UnitID == port.UnitID && c.PortNo == Convert.ToInt32(port.PortNo));
                                                //更新panel信息
                                                if (Outportinfo != null)
                                                {
                                                    //更新wip
                                                    glass.FunctionName = "RobotArmMonitorStoreIn";
                                                    glass.CurrentUnit = Outportinfo.UnitID;
                                                    glass.CurrentSUnit = Outportinfo.UnitID + "-" + Outportinfo.PortID;
                                                    glass.CassetteID = Outportinfo.CassetteID;
                                                    //glass.CassetteSequenceNo = Outportinfo.CassetteSequenceNo;
                                                    //glass.SlotSequenceNo = SlotSequenceNo;
                                                    glass.SlotPosition = SlotPosition;
                                                    glass.Position = Position;
                                                    glass.PortID = Outportinfo.PortID;
                                                    glass.OutCSTID = glass.CassetteID;
                                                    glass.OutPortID = Outportinfo.PortID;
                                                    dbService.UpdateGlassInfo(glass);

                                                    //缓存数据移动到对应的Port
                                                    Outportinfo.GlassInfos.Add(glass);
                                                    if (Inportinfo != null)
                                                        Inportinfo.GlassInfos.Remove(glass);
                                                }
                                                else
                                                    LogHelper.BCLog.Debug($"Outportinfo not found");
                                            }
                                        }
                                        else
                                            LogHelper.BCLog.Debug($"cmd.status not Excuting");
                                    }
                                    #endregion
                                }
                            }
                            catch (Exception ex)
                            {
                                dispatch.Logger.Error(ex);
                            }
                            #endregion
                            robot.LowerCassetteSequenceNo1 = result.LowerCassetteSequenceNo1;//下手臂CassetteSequenceNo1
                            robot.LowerSlotSequenceNo1 = result.LowerSlotSequenceNo1;//下手臂SlotSequenceNo1
                            robot.LowerCassetteSequenceNo2 = result.LowerCassetteSequenceNo2;//下手臂CassetteSequenceNo2
                            robot.LowerSlotSequenceNo2 = result.LowerSlotSequenceNo2;//下手臂SlotSequenceNo2
                            LogHelper.BCLog.Debug("come2");
                            dispatch.Logger.InfoFormat("Lower Hand LowerCassetteSequenceNo1: {0};LowerSlotSequenceNo1: {1};LowerCassetteSequenceNo2: {2};LowerSlotSequenceNo2: {3};",
                                robot.LowerCassetteSequenceNo1, result.LowerSlotSequenceNo1, result.LowerCassetteSequenceNo2, result.LowerSlotSequenceNo2);
                            //if (result.LowerCassetteSequenceNo1 != 0 || result.LowerSlotSequenceNo1 != 0)
                            {
                                if (result.LowerCassetteSequenceNo1 == 0 && result.LowerSlotSequenceNo1 == 0)
                                {
                                    robot.LowHandGlass1 = null;
                                }
                                else
                                {
                                    LogHelper.BCLog.Debug("come3");
                                    var LowerInfo1 = PanelInfo(indexer, result.LowerCassetteSequenceNo1, result.LowerSlotSequenceNo1);
                                    if (LowerInfo1 != null)
                                    {
                                        LogHelper.BCLog.Debug("come4");
                                        robot.LowHandGlass1 = LowerInfo1;
                                        LowerInfo1.SlotSatus = EnumGlassSlotStatus.Processing;
                                        dbService.UpdateGlassInfo(LowerInfo1);
                                        dispatch.Logger.InfoFormat("Find Lower Hand Glass1 Data from Glass List: LowerCassetteSequenceNo1: {0};LowerSlotSequenceNo1:{1}", robot.LowerCassetteSequenceNo1, robot.LowerSlotSequenceNo1);
                                    }
                                    else
                                    {
                                        robot.LowHandGlass1 = null;
                                        LogHelper.BCLog.Debug("come5");
                                        string message = string.Format("Can Not Find Lower Hand Glass1 Data From GlassInfo,LowerCassetteSequenceNo1:{0};LowerSlotSequenceNo1:{1}", robot.LowerCassetteSequenceNo1, robot.LowerSlotSequenceNo1);
                                        NotFindGlassError(message);
                                        dispatch.Logger.InfoFormat(message);
                                    }
                                }
                            }
                            //if (result.LowerCassetteSequenceNo2 != 0 || result.LowerSlotSequenceNo2 != 0)
                            {
                                if (result.LowerCassetteSequenceNo2 == 0 && result.LowerSlotSequenceNo2 == 0)
                                {
                                    robot.LowHandGlass2 = null;
                                }
                                else
                                {
                                    var LowerInfo2 = PanelInfo(indexer, result.LowerCassetteSequenceNo2, result.LowerSlotSequenceNo2);
                                    if (LowerInfo2 != null)
                                    {
                                        LogHelper.BCLog.Debug("come6");
                                        robot.LowHandGlass2 = LowerInfo2;
                                        LowerInfo2.SlotSatus = EnumGlassSlotStatus.Processing;
                                        dbService.UpdateGlassInfo(LowerInfo2);
                                        dispatch.Logger.InfoFormat("Find Lower Hand Glass2 Data from Glass List: LowerCassetteSequenceNo2: {0};LowerSlotSequenceNo2:{1}", robot.LowerCassetteSequenceNo2, robot.LowerSlotSequenceNo2);
                                    }
                                    else
                                    {
                                        LogHelper.BCLog.Debug("come7");
                                        robot.LowHandGlass2 = null;
                                        string message = string.Format("Can Not Find Lower Hand Glass2 Data From GlassInfo,LowerCassetteSequenceNo2:{0};LowerSlotSequenceNo2:{1}", robot.LowerCassetteSequenceNo2, robot.LowerSlotSequenceNo2);
                                        NotFindGlassError(message);
                                        dispatch.Logger.InfoFormat(message);
                                    }
                                }
                            }
                            robot.UpperCassetteSequenceNo1 = result.UpperCassetteSequenceNo1;//上手臂CassetteSequenceNo1
                            robot.UpperSlotSequenceNo1 = result.UpperSlotSequenceNo1;//上手臂SlotSequenceNo1
                            robot.UpperCassetteSequenceNo2 = result.UpperCassetteSequenceNo2;//上手臂CassetteSequenceNo2
                            robot.UpperSlotSequenceNo2 = result.UpperSlotSequenceNo2;//上手臂SlotSequenceNo2
                            dispatch.Logger.InfoFormat("Upper Hand UpperCassetteSequenceNo1: {0};UpperSlotSequenceNo1: {1};UpperCassetteSequenceNo2: {2};UpperSlotSequenceNo2: {3};",
                               robot.UpperCassetteSequenceNo1, result.UpperSlotSequenceNo1, result.UpperCassetteSequenceNo2, result.UpperSlotSequenceNo2);
                            //if (result.UpperCassetteSequenceNo1 != 0 || result.UpperSlotSequenceNo1 != 0)
                            {
                                if (result.UpperCassetteSequenceNo1 == 0 && result.UpperSlotSequenceNo1 == 0)
                                {
                                    robot.UpHandGlass1 = null;
                                }
                                else
                                {
                                    LogHelper.BCLog.Debug("come8");
                                    var UpperInfo1 = PanelInfo(indexer, result.UpperCassetteSequenceNo1, result.UpperSlotSequenceNo1);
                                    if (UpperInfo1 != null)
                                    {
                                        robot.UpHandGlass1 = UpperInfo1;
                                        UpperInfo1.SlotSatus = EnumGlassSlotStatus.Processing;
                                        dbService.UpdateGlassInfo(UpperInfo1);
                                        dispatch.Logger.InfoFormat("Find Upper Hand Glass1 Data from Glass List: UpperCassetteSequenceNo1: {0};UpperSlotSequenceNo1:{1}", robot.UpperCassetteSequenceNo1, robot.UpperSlotSequenceNo1);
                                    }
                                    else
                                    {
                                        robot.UpHandGlass1 = null;
                                        string message = string.Format("Can Not Find Upper Hand Glass1 Data From GlassInfo,UpperCassetteSequenceNo1: {0};UpperSlotSequenceNo1:{1}", robot.UpperCassetteSequenceNo1, robot.UpperSlotSequenceNo1);
                                        NotFindGlassError(message);
                                        dispatch.Logger.InfoFormat(message);
                                    }
                                }
                            }

                            //if (result.UpperCassetteSequenceNo2 != 0 || result.UpperSlotSequenceNo2 != 0)
                            {
                                if (result.UpperCassetteSequenceNo2 == 0 && result.UpperSlotSequenceNo2 == 0)
                                {
                                    robot.UpHandGlass2 = null;
                                }
                                else
                                {
                                    LogHelper.BCLog.Debug("come9");
                                    var UpperInfo2 = PanelInfo(indexer, result.UpperCassetteSequenceNo2, result.UpperSlotSequenceNo2);
                                    if (UpperInfo2 != null)
                                    {
                                        robot.UpHandGlass2 = UpperInfo2;
                                        UpperInfo2.SlotSatus = EnumGlassSlotStatus.Processing;
                                        dbService.UpdateGlassInfo(UpperInfo2);
                                        dispatch.Logger.InfoFormat("Find Upper Hand Glass2 Data from Glass List: UpperCassetteSequenceNo2: {0};UpperSlotSequenceNo2:{1}", robot.UpperCassetteSequenceNo2, robot.UpperSlotSequenceNo2);
                                    }
                                    else
                                    {
                                        LogHelper.BCLog.Debug("come10");
                                        robot.UpHandGlass2 = null;
                                        string message = string.Format("Can Not Find Upper Hand Glass2 Data From GlassInfo,UpperCassetteSequenceNo2: {0};UpperSlotSequenceNo2:{1}", robot.UpperCassetteSequenceNo2, robot.UpperSlotSequenceNo2);
                                        NotFindGlassError(message);
                                        dispatch.Logger.InfoFormat(message);
                                    }
                                }
                            }
                            dispatch.CommandExecuteResultReport();
                            //dispatch.Logger.DebugFormat("Command Result Code: {0}", result.ResultCode);

                        }
                        catch (Exception ex)
                        {
                            LogHelper.BCLog.Error(ex);
                            dispatch.Logger.Error(ex);
                        }

                        // dispatch.CommandExecuteResultReport(result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }

        private void NotFindGlassError(string message)
        {
            var robotUnit = HostInfo.EQPInfo.Units.Where(o => o.UnitType == EnumUnitType.Robot).FirstOrDefault();
            //string message = string.Format("Can Not Find Lower Hand Glass1 Data From GlassInfo,LowerCassetteSequenceNo1:{0};LowerSlotSequenceNo1:{1}", robot.LowerCassetteSequenceNo1, robot.LowerSlotSequenceNo1);
            //eqpCmd.CIMMessageSetCommand(robotUnit.UnitName, message, 0, 0);

            //OpCallInfo OpCallInfo = new OpCallInfo();
            //OpCallInfo.UnitID = robotUnit.UnitID;
            //OpCallInfo.ReturnCode = "NG";
            //OpCallInfo.Message = string.Format(message);
            //webSocketService.SendToWebSocketOPCall(OpCallInfo);
            webSocketService.SendToWebSocketOPCall("NG", message);
            //return message;
        }

        public void CommandExecuteResultReport(string name, string commandSequenceNumber)
        {
            try
            {
                lock (HostInfo.Current.DispatchList)
                {
                    AbstractDispatch robot = null;
                    HostInfo.Current.DispatchList.ForEach(p =>
                    {
                        var tempdispatch = p as AbstractDispatch;
                        if (tempdispatch.Name == name)
                        {
                            robot = tempdispatch;
                        }
                    });
                    if (robot != null)
                    {
                        if (robot.Name == name)
                        {
                            //robot.CommandExecuteResultReport();
                            var indexer = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == name);
                            var rb = indexer as Robot;
                            if (rb != null)
                            {
                                LogHelper.BCLog.Debug($"robot cmd count {rb.RobotCommandList.Count}");
                                //string testlog = "";
                                //for (int i = 0; i < rb.RobotCommandList.Count; i++)
                                //    testlog += $"cmd {rb.RobotCommandList[i].SequenceNo} Excuting {rb.RobotCommandList[i].Excuting} \r\n";
                                //LogHelper.BCLog.Debug(testlog);
                                var cmd = rb.RobotCommandList.FirstOrDefault(o => o.SequenceNo.ToString() == commandSequenceNumber);
                                if (cmd != null)
                                {
                                    rb.RobotCommandList.Remove(cmd);
                                    LogHelper.BCLog.Debug($"remove robot cmd {cmd.SequenceNo} CommandExecuteResultReport");
                                }
                                else
                                    LogHelper.BCLog.Debug($"CommandResultReport cmd {commandSequenceNumber} not found");
                            }
                            else
                                LogHelper.BCLog.Debug($"CommandResultReport robot data {name} not found");
                            ////var rb = indexer.SUnitList.Find(o => o.SUnitType == (int)EnumUnitType.Robot) as Robot;
                            //var rb = indexer as Robot;
                            //if (rb != null)
                            //{
                            //    rb.CmdResultCode = code;
                            //}
                        }
                        else
                            LogHelper.BCLog.Debug($"CommandResultReport Dispatch robot data {name} not match");
                    }
                    else
                        LogHelper.BCLog.Debug($"CommandResultReport Dispatch robot data {name} not found");
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        public void CommandReturnCode(string name, int code, string transactionID = "")
        {
            try
            {
                lock (HostInfo.Current.DispatchList)
                {
                    if (HostInfo.CancellationObjectDic.ContainsKey(transactionID))
                    {
                        var cancellationObject = HostInfo.CancellationObjectDic[transactionID];
                        var cancel = cancellationObject.CancellationTokenSource;
                        if (cancel != null)
                        {
                            cancel.Cancel();
                        }
                    }
                    AbstractDispatch robot = null;
                    HostInfo.Current.DispatchList.ForEach(p =>
                    {
                        var tempdispatch = p as AbstractDispatch;
                        if (tempdispatch.Name == name)
                        {
                            robot = tempdispatch;
                        }
                    });
                    if (robot != null)
                    {
                        if (robot.Name == name)
                        {
                            robot.CommandReturnCodeReport(code);
                            var indexer = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == name);
                            //var rb = indexer.SUnitList.Find(o => o.SUnitType == (int)EnumUnitType.Robot) as Robot;
                            var rb = indexer as Robot;
                            if (rb != null)
                            {
                                var cmdList = rb.RobotCommandList;
                                if (code != 1)
                                {
                                    var cmd = cmdList.FirstOrDefault(o => o.TransactionID == transactionID);
                                    if (cmd != null)
                                    {
                                        rb.RobotCommandList.Remove(cmd);
                                        LogHelper.BCLog.Debug($"remove robot cmd {cmd.SequenceNo} CommandReturnCode != 1");
                                    }
                                    else
                                        LogHelper.BCLog.Debug($"cannot find robot cmd transactionID {transactionID}");
                                }
                                rb.CmdResultCode = code;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }

        }
        public static GlassInfo PanelInfo(Unit oEQP, int CassetteSequenceNo, int SlotSequenceNo)
        {
            try
            {
                //GlassInfo panel = new GlassInfo();
                //var glassmap = new Hashtable();
                //glassmap.Add("CassetteSequenceNo", CassetteSequenceNo);
                //glassmap.Add("SlotSequenceNo", SlotSequenceNo);
                //IDbPanelService dbService = CommonContexts.ResolveInstance<IDbPanelService>();
                //panel = dbService.GetGlassInfoList(glassmap).OrderByDescending(o => o.UpdateDate).FirstOrDefault();

                var panel = logicService.GetGlassInfoByCode(oEQP, "RobotArmMonitoring", "", CassetteSequenceNo.ToString(), SlotSequenceNo.ToString());
                if (panel != null)
                {
                    return panel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
                return null;
            }

        }
        //public static GlassInfo PanelInfo(string panelid)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(panelid)) return null;
        //        GlassInfo panel = new GlassInfo();
        //        //Hashtable ht = new Hashtable();
        //        //ht.Add("GLSID", panelid);
        //        var glassmap = new Hashtable();
        //        glassmap.Add("GlassID", panelid);
        //        //glassmap.Add("CassetteSequenceNo", CassetteSequenceNo);
        //        //glassmap.Add("Position", Position);
        //        // ht.Add("FCassetteSequence", panelid);
        //        IDbPanelService dbService = CommonContexts.ResolveInstance<IDbPanelService>();
        //        panel = HostInfo.Current.CurrentPanelInfoList.FirstOrDefault(o => o.GlassID == panelid);
        //        if (panel == null)
        //        {
        //            LogHelper.BCLog.Debug(string.Format("[RobotService] PanelInfo panel==null panelid:{0}", panelid));                   
        //            panel = dbService.GetGlassInfoList(glassmap).OrderByDescending(o => o.UpdateDate).FirstOrDefault();
        //        }
        //        if (panel != null)
        //        {
        //            return panel;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //         return null;
        //    }

        //}



        //IList<RobotLinksignal> ViewRobotLinksignalList(Hashtable ht);
        //IList<RobotModel> ViewRobotModelList(Hashtable ht);
        //public IList<RobotLinksignal> ViewRobotLinksignalList(Hashtable ht)
        //{
        //    return dbRobot.ViewRobotLinksignalList(ht);
        //}
        public IList<RobotModel> ViewRobotModelList(Hashtable ht)
        {
            return dbRobot.ViewRobotModelList(ht);
        }


        public bool InsertHisRobotCommand(HisRobotCommand HisRobotCommand)
        {
            return dbRobot.InsertHisRobotCommand(HisRobotCommand);
        }
        public void WriteRobotDebugLog(string name, string logInfo)
        {
            try
            {
                lock (HostInfo.Current.DispatchList)
                {
                    AbstractDispatch dispatch = null;
                    HostInfo.Current.DispatchList.ForEach(p =>
                    {
                        var tempdispatch = p as AbstractDispatch;
                        if (tempdispatch.Name == name)
                        {
                            dispatch = tempdispatch;
                        }
                    });
                    if (dispatch != null)
                    {
                        try
                        {
                            dispatch.Logger.InfoFormat(logInfo);
                        }
                        catch (Exception ex)
                        {
                            dispatch.Logger.Error(ex);
                        }

                        // dispatch.CommandExecuteResultReport(result);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }
        }
    }
}
