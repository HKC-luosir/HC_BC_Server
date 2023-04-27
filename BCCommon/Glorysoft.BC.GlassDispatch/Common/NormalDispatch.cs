using System;
using System.Collections.Generic;
using System.Linq;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Logic.Contract;
using Glorysoft.Auto.Contract;
using System.Globalization;
using System.Collections;
using System.Text;

namespace Glorysoft.BC.GlassDispath
{
    public class NormalDispatch : AbstractDispatch
    {
        public NormalDispatch(string name)
            : base(name)
        {

        }
        //查询是否可以取片或者放片
        internal override List<RobotCommand> Dispatch(ref StringBuilder logStr)
        {
            if (cmdList.Count > 0)
                return cmdList;
            //cmdList = new List<RobotCommand>();
            try
            {
                try
                {
                    Logger.InfoFormat("[RobotControlService][start]---------------Dispatch Start-----------------");
                    logStr.AppendLine();
                    //if (HostInfo.Current.EQPInfo.LineMode == LineMode.MultiChamber || HostInfo.Current.EQPInfo.LineMode == LineMode.MultiChamberForceCleanOut)//多层
                    //{
                    //    Logger.InfoFormat("[RobotControlService][LineModeCheck] - Pre Check.LineMode == {0}", HostInfo.Current.EQPInfo.LineMode);
                    //    FindMultiChamberGetCommand(ref logStr);

                    //    FindMultiChamberPutCommand(ref logStr);

                    //    AbortMultiChamberCommand(ref logStr);
                    //}
                    //else if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyB)
                    //{
                    //    Logger.InfoFormat("[RobotControlService][LineModeCheck] - Pre Check.LineMode == {0}", HostInfo.Current.EQPInfo.LineMode);

                    //    FindOnlyBGetCommand(ref logStr);

                    //    FindOnlyBPutCommand(ref logStr);
                    //}
                    //else if (HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                    //{
                    //    PHT600ForceCleanOutTransferEQPToIndex(ref logStr);
                    //    PHT600ForceCleanOutFindPutCommand(ref logStr);
                    //    //if ( HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                    //    //{
                    //    //    Logger.Info("[FindPutCommand]   LineMode is ForceCleanOut; ");
                    //    //    logInfo = string.Format("[  PutCommand Check]   LineMode is ForceCleanOut;");
                    //    //    logStr.Append(logInfo);
                    //    //    logStr.AppendLine();


                    //    //    CreatePHT600ForceCleanOutPortPutCommand(RobotHand, ref logStr);
                    //    //    return;
                    //    //}
                    //}
                    //else
                    //{
                    Logger.InfoFormat("[RobotControlService][LineModeCheck] - Pre Check.LineMode == {0}", HostInfo.Current.EQPInfo.LineMode);
                    List<PortInfo> LoadingPortList = GetLoadingPossiblePortList(HostInfo.Current.PortList, "CheckLoadingPort");

                    // Logger.InfoFormat("+++ TransferIndexToEQPCommand ");
                    TransferIndexToEQPCommand(ref logStr, LoadingPortList);
                    // Logger.InfoFormat("+++ TransferEQPToIndexOrEQPCommand ");
                    //Transfer 从eqp到index
                    TransferEQPToIndexOrEQPCommand(ref logStr);

                    //Exchange 交接片
                    //FindExchangeCommand(ref logStr);

                    //取panel                        
                    FindGetCommand(ref logStr, LoadingPortList);

                    //放panel
                    FindPutCommand(false, ref logStr);

                    //Transfer 从index到eqp                        
                    //Logger.InfoFormat("+++ AbortCommand ");
                    //ui abort后将手臂上的glass放回卡夹
                    //AbortCommand(ref logStr);

                    CreateGetWaitCommand(ref logStr);

                    FindPutCommand(true, ref logStr);

                    Logger.InfoFormat("[RobotControlService][end]---------------Dispatch End----------------- \r\n");
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    Logger.Info(ex);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
            return cmdList;
        }

        #region PHT600ForceCleanOut
        //private void PHT600ForceCleanOutTransferEQPToIndex(ref StringBuilder logStr)
        //{
        //    string logInfo = string.Format("");
        //    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] begin ");
        //    //检查手臂上有没有玻璃  有就跳出

        //    Dictionary<RobotHand, GlassInfo> putGlassDic = GetRobotHandGlass();
        //    if (putGlassDic.Count > 0)//检查手臂上有没有玻璃  有就跳出
        //    {
        //        Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] - Pre Check NG.RobotHandGlass Check NG; RobotHand Has Glass");
        //        logInfo = string.Format("[PHT600TransferCheck]   NG  .RobotHandGlass Check NG; RobotHand Has Glass");

        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    //循环所有设备
        //    foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
        //    {
        //        Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] UnitName{0} RobotModelList.count:{1} ", unitItem.GetType().Name, unitItem.RobotModelList.Count);
        //        if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
        //        {
        //            // List<GlassInfo> glassInfoList = new List<GlassInfo>();
        //            Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] unitName:{0} ", unitItem.UnitName);
        //            foreach (var getRobotModel in unitItem.RobotModelList)
        //            {
        //                // Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex]unitName:{0}; getRobotModel:{1} ", unitItem.UnitName, getRobotModel.ModelName);
        //                //校验该设备是否需要排片
        //                #region 校验Trouble信号
        //                Linksignal upLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == getRobotModel.UPLinkName);
        //                if (CheckUpLinkStatus(upLinksignal, unitItem, ref logStr))
        //                {
        //                    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
        //                }
        //                else
        //                {
        //                    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] - Pre Check NG.UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);
        //                    logInfo = string.Format("[PHT600TransferCheck]   NG  Condition NG,UpstreamLinkSignal={0}", getRobotModel.UPLinkName);

        //                    logStr.Append(logInfo);
        //                    logStr.AppendLine();
        //                    continue;
        //                }
        //                #endregion
        //                //如果需要排片生成该panel到指定port的transfer

        //                #region 获取slotPostion
        //                int slotPostion = 0;
        //                #endregion

        //                #region 获取取片手臂
        //                RobotHand getRobot = RobotHand.Error;
        //                getRobot = GetRobot(getRobotModel, getRobot);
        //                if (getRobot == RobotHand.Error)
        //                {
        //                    //Logger.Info("[PHT600ForceCleanOutTransferEQPToIndex] Check GetRobot.RobotHand NG; RobotHand.Error");
        //                    //return;
        //                    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] - Check GetRobot.RobotHand NG; RobotHand.Error ");
        //                    logInfo = string.Format("[PHT600TransferCheck]   NG Check GetRobot.RobotHand NG; RobotHand.Error");
        //                    logStr.Append(logInfo);
        //                    logStr.AppendLine();
        //                    continue;
        //                }
        //                #endregion

        //                #region 校验put port 状态
        //                var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == HostInfo.Current.EQPInfo.PHT600Port);
        //                if (unloadport.CassetteInfo.CassetteStatus != EnumCarrierStatus.WaitingforProcessing && unloadport.CassetteInfo.CassetteStatus != EnumCarrierStatus.InProcessing)
        //                {
        //                    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] - Check Port NG. [PHT600ForceCleanOut],Port=[{0}] CassetteStatus:{1},Not Exist Put Port", unloadport.PortID, unloadport.CassetteInfo.CassetteStatus);
        //                    logInfo = string.Format("[PHT600TransferCheck]   NG  [PHT600ForceCleanOut],Port=[{0}] CassetteStatus:{1} ,Not Exist Put Port", unloadport.PortID, unloadport.CassetteInfo.CassetteStatus);
        //                    logStr.Append(logInfo);
        //                    logStr.AppendLine();
        //                    eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};PHT600ForceCleanOut", unloadport.PortID));
        //                    return;
        //                }
        //                #endregion

        //                #region 校验put Stage                                                 
        //                var targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == unloadport.UnitName);
        //                if (targetUnit == null)
        //                {
        //                    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex]  - Check TargetUnit NG.[PHT600ForceCleanOut] ,TargetUnit is null");
        //                    logInfo = string.Format("[PHT600TransferCheck]   NG   ,[PHT600ForceCleanOut] TargetUnit is null");
        //                    logStr.Append(logInfo);
        //                    logStr.AppendLine();
        //                    return;
        //                }
        //                var putRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == unloadport.PortID);
        //                if (putRobotModel == null)
        //                {
        //                    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex][CreatePortPutCommand] - Check Action NG[PHT600ForceCleanOut],TargetRobotModel is null");
        //                    logInfo = string.Format("[PHT600TransferCheck]   NG  [PHT600ForceCleanOut],TargetRobotModel is null");
        //                    logStr.Append(logInfo);
        //                    logStr.AppendLine();
        //                    return;
        //                }
        //                // logInfo = string.Format("[PHT600ForceCleanOutTransferEQPToIndex] [CreatePortPutCommand] Check putRobotModel OK; robputRobotModelotmodel:{0} ", putRobotModel.ModelName);

        //                #endregion
        //                GlassInfo getGlass = new GlassInfo();
        //                getGlass.Position = HostInfo.Current.EQPInfo.PHT600PortSlot;
        //                getGlass.Position++;
        //                //Logger.InfoFormat(" [PHT600ForceCleanOutTransferEQPToIndex] Check getRobotModel OK,GetEnable==true;getRobotModel:{0};getRobotModel.DualArm:{1};getRobot:{2}", getRobotModel.ModelName, getRobotModel.DualArm, getRobot.ToString());
        //                #region 生成命令
        //                //Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex]  modelItem:{0};modelItem.OutPriority:{1} ", getRobotModel.ModelName, getRobotModel.OutPriority);

        //                var cmd = factory.CreateTransferEQPToIndexCommand(getRobot, unitItem, getGlass, putRobotModel.TransferPriority, getRobotModel.ModelPosition, putRobotModel.ModelPosition, slotPostion);
        //                GlassInfo GlassA = null;
        //                GlassInfo GlassB = null;
        //                GlassInfo Glass = null;
        //                GetUnitModelGlass(getRobotModel, ref Glass, ref GlassA, ref GlassB, "PHT600ForceCleanOutTransferEQPToIndex");
        //                cmd.GetGlass = Glass;
        //                cmd.GetGlassA = GlassA;
        //                cmd.GetGlassB = GlassB;
        //                //cmd.GlassInfo = glassInfo;

        //                var GetPosition1 = GetCurrentModel(getGlass.ModelPosition);
        //                cmd.STGetPosition1string = GetPosition1.ModelName;
        //                AddCommand(cmd);
        //                Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex][PHT600ForceCleanOutTransferEQPToIndex] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                     cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1);
        //                logInfo = string.Format("[PHT600TransferCheck]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                     cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1);
        //                logStr.Append(logInfo);
        //                logStr.AppendLine();
        //                #endregion

        //            }


        //        }
        //    }

        //    Logger.InfoFormat("[PHT600ForceCleanOutTransferEQPToIndex] End ");
        //}

        //private void PHT600ForceCleanOutFindPutCommand(ref StringBuilder logStr)
        //{
        //    string logInfo = string.Format("");
        //    Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand] begin ");
        //    //获取手臂资料
        //    #region 获取手臂及资料

        //    Dictionary<RobotHand, GlassInfo> putGlassDic = GetRobotHandGlass();

        //    if (putGlassDic.Count == 0)//都没有就不放
        //    {
        //        Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand]  - Check Data NG.RobotHandGlass is null");
        //        logInfo = string.Format("[  PHT600 Put Check]   NG  RobotHandGlass is null");
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    #endregion


        //    //Logger.InfoFormat("[FindPutCommand] Check HandGlass OK; putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //    foreach (var putGlassDICItem in putGlassDic)
        //    {
        //        RobotHand RobotHand = putGlassDICItem.Key;
        //        GlassInfo putGlass = putGlassDICItem.Value;

        //        Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand] - Pre Check OK.Glass[{1},{2}],RobotArm({0})", RobotHand.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);

        //        CreatePHT600ForceCleanOutPortPutCommand(RobotHand, ref logStr);
        //    }

        //    //获取put model

        //    Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand] end ");
        //}

        //private Dictionary<RobotHand, GlassInfo> GetRobotHandGlass()
        //{
        //    Dictionary<RobotHand, GlassInfo> putGlassDic = new Dictionary<RobotHand, GlassInfo>();

        //    if (Robot.UpperExistOn)//上手臂是否存在panel
        //    {
        //        if (Robot.UpHandGlass1 != null)
        //        {

        //            if (putGlassDic.Keys.Contains(RobotHand.UpHand))
        //            {
        //                putGlassDic[RobotHand.UpHand] = Robot.UpHandGlass1;
        //            }
        //            else
        //            {
        //                putGlassDic.Add(RobotHand.UpHand, Robot.UpHandGlass1);
        //            }
        //            //Logger.InfoFormat("[FindPutCommand] UpHandSubstrateID:{0},FetchDataTime:{1} ", Robot.UpHandGlass1.GlassID, Robot.UpHandGlass1.FetchDatetime.ToString());
        //            //Logger.InfoFormat("[FindPutCommand][LineMode!=OnlyA] [Robot.UpHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
        //        }
        //        else if (Robot.UpHandGlass2 != null)
        //        {
        //            //dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass2);
        //            //RobotHand = RobotHand.UpHand;
        //            //putGlass = Robot.UpHandGlass2;
        //            if (putGlassDic.Keys.Contains(RobotHand.UpHand))
        //            {
        //                putGlassDic[RobotHand.UpHand] = Robot.UpHandGlass2;
        //            }
        //            else
        //            {
        //                putGlassDic.Add(RobotHand.UpHand, Robot.UpHandGlass2);
        //            }
        //            //Logger.InfoFormat("[FindPutCommand] [LineMode!=OnlyA] [Robot.UpHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass2.CassetteSequenceNo, Robot.UpHandGlass2.SlotSequenceNo);
        //        }

        //    }

        //    if (Robot.LowerExistOn)//下手臂是否存在panel
        //    {
        //        if (Robot.LowHandGlass1 != null)
        //        {
        //            //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass1);
        //            //RobotHand = RobotHand.LowHand;
        //            //putGlass = Robot.LowHandGlass1;
        //            if (putGlassDic.Keys.Contains(RobotHand.LowHand))
        //            {
        //                putGlassDic[RobotHand.LowHand] = Robot.LowHandGlass1;
        //            }
        //            else
        //            {
        //                putGlassDic.Add(RobotHand.LowHand, Robot.LowHandGlass1);
        //            }
        //            //Logger.InfoFormat("[FindPutCommand][LineMode!=OnlyA]  [Robot.LowHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
        //        }
        //        else if (Robot.LowHandGlass2 != null)
        //        {
        //            //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass2);
        //            //RobotHand = RobotHand.LowHand;
        //            //putGlass = Robot.LowHandGlass2;
        //            if (putGlassDic.Keys.Contains(RobotHand.LowHand))
        //            {
        //                putGlassDic[RobotHand.LowHand] = Robot.LowHandGlass2;
        //            }
        //            else
        //            {
        //                putGlassDic.Add(RobotHand.LowHand, Robot.LowHandGlass2);
        //            }
        //            //Logger.InfoFormat("[FindPutCommand][LineMode!=OnlyA]  [Robot.LowHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass2.CassetteSequenceNo, Robot.LowHandGlass2.SlotSequenceNo);
        //        }

        //    }

        //    return putGlassDic;
        //}
        #endregion

        #region Get Command
        //private void FindOnlyBGetCommand(ref StringBuilder logStr)
        //{
        //    // Logger.InfoFormat("+++ CreateOnlyBProcessGetCommand ");
        //    //寻找设备取片命令
        //    CreateOnlyBProcessGetCommand(ref logStr);
        //    // Logger.InfoFormat("+++ CreateOnlyBPortGetCommand ");
        //    //寻找port取片命令
        //    CreateOnlyBPortGetCommand(ref logStr);

        //}
        /// <summary>
        /// 取panel
        /// </summary>
        private void FindGetCommand(ref StringBuilder logStr, List<PortInfo> LoadingPortList)
        {
            try
            {
                string logInfo = string.Format("");
                ////判断Robot手臂上是否有玻璃
                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut)
                //{
                //    Logger.Info("[FindGetCommand]   LineMode is ForceCleanOut; FindGetCommand NG ");
                //    logInfo = string.Format("[    FindGetCommand Check]   LineMode is ForceCleanOut; FindGetCommand NG");
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //    return;
                //}
                //Logger.InfoFormat("+++ CreateProcessGetCommand ");
                //寻找设备取片命令
                CreateProcessGetCommand(ref logStr);
                // Logger.InfoFormat("+++ CreatePortGetCommand ");
                //CreateBufferGetCommand();
                //寻找port取片命令
                CreatePortGetCommand(ref logStr, LoadingPortList);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }
        ///// <summary>
        ///// 从port取panel
        ///// </summary>
        //private void CreatePortGetCommand()
        //{
        //    try
        //    {
        //        //获取WaiForProcessing或者InProcessing状态的port
        //        List<PortInfo> possiblePortList = GetLoadingPossiblePortList(PortList);
        //        if (possiblePortList == null || possiblePortList.Count == 0)
        //        {
        //            Logger.Info("[PORT GET] Can not Found Processing Port ");
        //            return;
        //        }
        //        List<PortInfo> currentLoadingPort;
        //        //获取inprocessing
        //        currentLoadingPort = GetCurrentLoadingPortList();
        //        if (currentLoadingPort.Count == 0)
        //        {
        //            Logger.Info("[PORT GET] No Current Loading Port, Find one from Possible Loading Port ");
        //            currentLoadingPort = possiblePortList;//如果没有正在使用的port；赋值WaiForProcessing或者InProcessing状态的port
        //        }
        //        else
        //        {
        //            currentLoadingPort = currentLoadingPort.FindAll(o => possiblePortList.Exists(p => p.UnitID == o.UnitID));
        //            if (currentLoadingPort.Count == 0)
        //            {
        //                Logger.Info("[PORT GET] Current Loading Port Status Not Right ");
        //                return;
        //            }
        //        }

        //        Logger.InfoFormat("[PORT GET] Current Loading Port Count = {0} ", currentLoadingPort.Count);
        //        foreach (PortInfo port in currentLoadingPort)
        //        {
        //            //20180801 lsq_Modify
        //            var all = port.GlassInfos.ToList().FindAll(o => o.IsExist && o.SlotSatus == (int)EnumGlassSlotStatus.Wait);

        //            if (all.Count == 0)
        //            {
        //                Logger.InfoFormat("[PORT GET] Can not Found Waiting Status Job At Port :{0} ", port.PortID);
        //                continue;
        //            }

        //            var info = all.OrderBy(o => o.FSlotNO).FirstOrDefault();

        //            var targetlist = FindNextUnit(port.PortID, "");
        //            //当前取片层数PortGetSlot=Robot执行结果上报实际层数+1
        //            //int PortGetSlot = port.GetSlot + 1;

        //            int PortGetSlot = info.FSlotNO;
        //            //var count = port.TransferMode == "1" ? 60 : 120;
        //            //if (PortGetSlot > count)
        //            //{
        //            //    Logger.InfoFormat("[PORT GET] Cannot Find SlotNo :{0};Port:{1}", PortGetSlot, port.PortID);
        //            //    IEQPService eqpCmd = CommonContexts.ResolveInstance<IEQPService>();
        //            //    var message = string.Format("Cannot Find SlotNo :{0};Port:{1}", PortGetSlot, port.PortID);
        //            //    eqpCmd.SendCIMMessage(Consts.UnitName.Index.ToString(), message);
        //            //    HostInfo.Current.OPCallListAdd(string.Format("OPCall:{0},Message:{1}", "", message));
        //            //    continue;
        //            //}
        //            //60||120 Not Get
        //            foreach (var t in targetlist)
        //            {
        //                var robotHand1 = PortGetHandCheck(Robot, t.PathConfigure);
        //                if (robotHand1 == RobotHand.Error)
        //                {
        //                    Logger.InfoFormat("[PORT GET] Cannot Find Hand for Port :{0}, Arm Have Glass ", port.UnitName);
        //                    continue;
        //                }
        //                //第一次下命令PortGetSlot数量始终为1，需定义全局变量根据Robot取片结果上报层数更新PortGetSlot，并且下次PortGetSlot+1,初始赋值为0
        //                var cmd = factory.CreatePortGetCommand(port, info, PortGetSlot, robotHand1, t.PathConfigure.OutPriority);
        //                AddCommand(cmd);
        //                Logger.InfoFormat("[PORT GET] Port {0} Get from {1} for Glass {2} ", port.UnitName, info.FSlotNO, info.GLSID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        /// <summary>
        /// 从port取panel
        /// </summary>
        private void CreatePortGetCommand(ref StringBuilder logStr, List<PortInfo> LoadingPortList)
        {
            try
            {
                //Logger.InfoFormat("[CreatePortGetCommand] begin ");
                string logInfo = string.Format("");

                //获取WaiForProcessing或者InProcessing状态的port
                if (LoadingPortList == null || LoadingPortList.Count == 0)
                {
                    Logger.Info("[CreatePortGetCommand] - Check LoadingPortList NG.LoadingPortList.Count=0");
                    logInfo = string.Format("[     PortGet Check]   NG  LoadingPortList.Count=0");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                PortInfo currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
                var GlassInfos = currentLoadingPort.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();

                #region 校验手臂资料
                if (Robot.UpperExistOn || Robot.LowerExistOn)
                {
                    logInfo = string.Format("[     PortGet Check]   NG  Hand Glass Exist:Robot.UpperExistOn==true or LowerExistOn==true ");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion

                #region 获取CurrentPort
                
                //PortInfo currentLoadingPort = LoadingPortList.FirstOrDefault(o => o.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing || o.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing);
                //if (currentLoadingPort == null)
                //{
                //    //LoadingPortList = LoadingPortList.Where(o => o.WaitingforProcessingTime != DateTime.MinValue).ToList();
                //    currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
                //}
                // Logger.InfoFormat("[CreatePortGetCommand] CurrentLoadingPort{0} ", currentLoadingPort.PortID);
                var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == currentLoadingPort.UnitID);
                if (index == null)
                {
                    Logger.InfoFormat("[CreatePortGetCommand] - Check UnitID NG; CurrentLoadingPort.UnitID is null ");
                    logInfo = string.Format("[     PortGet Check]   NG Check UnitID NG; CurrentLoadingPort.UnitID is null ");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion

                #region 获取GetGlass
                

                int ModelPosition = 0;
                PortGetType PortGetType = PortGetType.ASC;
                RobotModel getRobotModel = null;

                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.MixRun)
                //{
                //    // Logger.InfoFormat("[CreatePortGetCommand][ProcessMode.MixRun]  ");
                //    //获取MixRunConfig 中两条recipe在port中是否都存在
                //    MixRunMode(ref LoadingPortList, ref currentLoadingPort, ref GlassInfos, ref getGlass, ref logStr);
                //    //  Logger.InfoFormat("[CreatePortGetCommand][ProcessMode.MixRun] CurrentGlasss  glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                //    //var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
                //    //if (model != null)
                //    //{
                //    //    ModelPosition = model.ModelPosition;
                //    //    PortGetType = model.PortGetType;
                //    //    getRobotModel = model;
                //    //}
                //}
                var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
                if (model != null)
                {
                    ModelPosition = model.ModelPosition;
                    PortGetType = model.PortGetType;
                    getRobotModel = model;
                }

                #region 校验 getRobotModel
                if (getRobotModel == null)
                {
                    Logger.InfoFormat("[CreatePortGetCommand] Check getRobotModel NG; PortID:{0}; CreatePortGetCommand NG ", currentLoadingPort.PortID);
                    logInfo = string.Format("[     PortGet Check]   NG  getRobotModel NG; PortID:{0}; CreatePortGetCommand NG ", currentLoadingPort.PortID);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.InfoFormat("[CreatePortGetCommand] PortGetType:{0},getRobotModel:{1} ", PortGetType.ToString(), getRobotModel.ModelName);
                if (!getRobotModel.GetEnable)
                {
                    Logger.InfoFormat("[CreatePortGetCommand] Check getRobotModel.GetEnable NG; getRobotModel.GetEnable=false;getRobotModel:{0}; CreatePortGetCommand NG ", getRobotModel.ModelName);
                    logInfo = string.Format("[     PortGet Check]   NG  Port:{0},GetEnable = false; PortGetCommand NG", currentLoadingPort.PortID);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion

                //两层 如果两个手臂都有空 就同时取片 
                GlassInfo getGlassSlot1 = null;
                GlassInfo getGlassSlot1B = null;
                GlassInfo getGlassSlot2 = null;
                GlassInfo getGlassSlot2B = null;
                //if (getGlass == null)
                //{
                //var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
                //if (model != null)
                //{
                //    ModelPosition = model.ModelPosition;
                //    PortGetType = model.PortGetType;
                //    getRobotModel = model;
                //}
                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                //{
                //    GlassInfos = GlassInfos.Where(o => o.SlotPosition == 1).ToList();
                //    if (PortGetType == PortGetType.DESC)
                //    {
                //        getGlass = GlassInfos.OrderByDescending(o => o.Position).FirstOrDefault();
                //    }
                //    else //if (PortGetType == PortGetType.DESC)
                //    {
                //        getGlass = GlassInfos.OrderBy(o => o.Position).FirstOrDefault();
                //        //info = all.OrderByDescending(o => o.Position).FirstOrDefault();
                //    }
                //    if (getGlass == null)
                //    {
                //        Logger.InfoFormat("[CreatePortGetCommand][LineMode== LineMode.OnlyA] Check glassInfo NG; info==null; PortGetCommand NG ");
                //        logInfo = string.Format("[     PortGet Check]   NG    OnlyA,glassInfo = null; PortGetCommand NG");
                //        logStr.Append(logInfo);
                //        logStr.AppendLine();
                //        return;
                //    }
                //    // info = all.OrderBy(o => o.Position).FirstOrDefault();
                //    //Logger.InfoFormat("[CreatePortGetCommand][ProcessMode.OnlyA] CurrentGlasss  glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                //}
                //else
                //{
                if (PortGetType == PortGetType.DESC)
                {
                    var descdata = GlassInfos.OrderByDescending(o => o.Position).ToList();
                    getGlassSlot1 = descdata.FirstOrDefault();
                    if (descdata.Any(c => c.Position == getGlassSlot1.Position && c.SlotPosition != getGlassSlot1.SlotPosition))
                    {
                        getGlassSlot1B = descdata.FirstOrDefault(c => c.Position == getGlassSlot1.Position && c.SlotPosition != getGlassSlot1.SlotPosition);
                    }
                    if (descdata.Any(c => c.Position != getGlassSlot1.Position))
                    {
                        var descdata2 = descdata.Where(c => c.Position != getGlassSlot1.Position).OrderByDescending(o => o.Position).ToList();
                        getGlassSlot2 = descdata2.FirstOrDefault();
                        if (descdata2.Any(c => c.Position == getGlassSlot2.Position && c.SlotPosition != getGlassSlot2.SlotPosition))
                        {
                            getGlassSlot2B = descdata2.FirstOrDefault(c => c.Position == getGlassSlot2.Position && c.SlotPosition != getGlassSlot2.SlotPosition);
                        }
                    }
                }
                else
                {
                    var ascdata = GlassInfos.OrderBy(o => o.Position).ToList();
                    getGlassSlot1 = ascdata.FirstOrDefault();
                    if (ascdata.Any(c => c.Position == getGlassSlot1.Position && c.SlotPosition != getGlassSlot1.SlotPosition))
                    {
                        getGlassSlot1B = ascdata.FirstOrDefault(c => c.Position == getGlassSlot1.Position && c.SlotPosition != getGlassSlot1.SlotPosition);
                    }
                    if (ascdata.Any(c => c.Position != getGlassSlot1.Position))
                    {
                        var ascdata2 = ascdata.Where(c => c.Position != getGlassSlot1.Position).OrderBy(o => o.Position).ToList();
                        getGlassSlot2 = ascdata2.FirstOrDefault();
                        if (ascdata2.Any(c => c.Position == getGlassSlot2.Position && c.SlotPosition != getGlassSlot2.SlotPosition))
                        {
                            getGlassSlot2B = ascdata2.FirstOrDefault(c => c.Position == getGlassSlot2.Position && c.SlotPosition != getGlassSlot2.SlotPosition);
                        }
                    }
                }
                string GetGlassInfoStr = "";
                if (getGlassSlot1 != null)
                {
                    GetGlassInfoStr += string.Format("Slot{0} Glass:{1},{2} ", getGlassSlot1.Position, getGlassSlot1.CassetteSequenceNo, getGlassSlot1.SlotSequenceNo);
                }
                if (getGlassSlot1B != null)
                {
                    GetGlassInfoStr += string.Format("Slot{0} Glass:{1},{2} ", getGlassSlot1B.Position, getGlassSlot1B.CassetteSequenceNo, getGlassSlot1B.SlotSequenceNo);
                }
                if (getGlassSlot2 != null)
                {
                    GetGlassInfoStr += string.Format("Slot{0}Glass:{1},{2} ", getGlassSlot2.Position, getGlassSlot2.CassetteSequenceNo, getGlassSlot2.SlotSequenceNo);
                }
                if (getGlassSlot2B != null)
                {
                    GetGlassInfoStr += string.Format("Slot{0}Glass:{1},{2} ", getGlassSlot2B.Position, getGlassSlot2B.CassetteSequenceNo, getGlassSlot2B.SlotSequenceNo);
                }

                Logger.InfoFormat("[CreatePortGetCommand] CurrentGlasss {0}];CurrentPortID:{1} ", GetGlassInfoStr, currentLoadingPort.PortID);
                logInfo = string.Format("[     PortGet Check]   OK  Port:{0}, CurrentGlasss {1}]", currentLoadingPort.PortID, GetGlassInfoStr);
                logStr.Append(logInfo);
                logStr.AppendLine();

                #endregion
                //var getGlassB = GlassInfos.FirstOrDefault(o => (o.Position == getGlass.Position && o.SlotPosition != getGlass.SlotPosition) && o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed);
                //if (getGlassB != null)
                //{
                //    //Logger.InfoFormat("[TransferIndexToEQPCommand] getGlassA[{0},{1}];getGlassB[{2},{3}]; LoadingPortList.OrderBy(o => o.WaitingforProcessingTime ：{4}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, currentLoadingPort.PortID);
                //    Logger.InfoFormat("[CreatePortGetCommand] CurrentGlasss  glassInfoA[{0},{1}];getGlassB[{2},{3}];CurrentPortID:{4} ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, currentLoadingPort.PortID);
                //    logInfo = string.Format("[     PortGet Check]   OK  Port:{0},GlassA[{1},{2}];getGlassB[{3},{4}]", currentLoadingPort.PortID, getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //}
                //else
                //{
                //    Logger.InfoFormat("[CreatePortGetCommand] CurrentGlasss  glassInfo[{0},{1}];CurrentPortID:{2} ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, currentLoadingPort.PortID);
                //    logInfo = string.Format("[     PortGet Check]   OK  Port:{0},Glass[{1},{2}]", currentLoadingPort.PortID, getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //}

                //RobotHand getRobot = RobotHand.Error;
                //if (HostInfo.Current.EQPID.Contains("A1DET400"))
                //{
                //    #region 获取targetlist
                //    var targetlist = FindNextStage(getGlass);
                //    if (targetlist.Count() == 0)
                //    {
                //        Logger.InfoFormat("[CreatePortGetCommand] - Check Targetlist NG.Targetlist.Count()=0");
                //        logInfo = string.Format("[     PortGet Check]   NG  Targetlist.Count()=0");

                //        logStr.Append(logInfo);
                //        logStr.AppendLine();
                //        return;
                //    }
                //    // Logger.InfoFormat("[TransferIndexToEQPCommand] Check targetlist OK; targetlist.Count:{0}", targetlist.Count());
                //    #endregion
                //    //var target = targetlist.FirstOrDefault();
                //    foreach (var target in targetlist)
                //    {
                //        #region 获取targetmodel
                //        Unit targetUnit = null;
                //        RobotModel targetmode = GetCurrentModel(target.ModelPosition);
                //        targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetmode.UnitID);
                //        if (targetUnit == null)
                //        {
                //            Logger.InfoFormat("[CreatePortGetCommand] - Check TartgetUnit NG.Glass:[{0},{1}] Target ModelPosition={2},TargetUnit is null", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, target.ModelPosition);
                //            logInfo = string.Format("[     PortGet Check]    NG  Glass:[{0},{1}] Target ModelPosition={2},TargetUnit is null", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, target.ModelPosition);
                //            logStr.Append(logInfo);
                //            logStr.AppendLine();
                //            continue;
                //        }
                //        #endregion
                //        #region 获取取片手臂                   
                //        getRobot = GetRobot(getRobotModel, getRobot, targetmode);
                //        #endregion
                //    }
                //}
                //else
                //{
                #region 获取取片手臂
                RobotHand getRobot = RobotHand.Error;
                getRobot = GetRobot(getRobotModel, getRobot);
                #endregion
                //}
                if (getRobot == RobotHand.Error)
                {
                    //Logger.Info("[CreatePortGetCommand] Check GetRobot.RobotHand NG; RobotHand.Error");
                    //return;
                    Logger.InfoFormat("[CreatePortGetCommand] - Check GetRobot.RobotHand NG; RobotHand.Error ");
                    logInfo = string.Format("[     PortGet Check]   NG Check GetRobot.RobotHand NG; RobotHand.Error");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //#region 获取SlotPostion
                //int SlotPostion = 99;
                ////if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                ////{
                ////    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                ////    {
                ////        SlotPostion = getGlass.SlotPosition;
                ////        //Logger.InfoFormat("[CreatePortGetCommand][LineMode == LineMode.OnlyA] SlotPostion:{0}", SlotPostion);
                ////    }
                ////    else
                ////    {
                ////        //var glassCount = all.Where(o => o.Position == info.Position).Count();
                ////        var glassCount = GlassInfos.Where(o => o.Position == getGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                ////        SlotPostion = glassCount == 1 ? Convert.ToInt32(getGlass.SlotPosition) : 99;
                ////        // Logger.InfoFormat("[CreatePortGetCommand][LineMode != LineMode.OnlyA] SlotPostion:{0}", SlotPostion);
                ////    }
                ////}
                ////else
                ////{
                ////    var glassCount = GlassInfos.Where(o => o.Position == getGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                ////    SlotPostion = glassCount == 1 ? Convert.ToInt32(getGlass.SlotPosition) : 99;//只有一片 就赋值
                ////}
                //#endregion

                //整理两层和前后片
                GlassInfo GlassA = null;
                GlassInfo GlassB = null;
                GlassInfo GlassC = null;
                GlassInfo GlassD = null;
                GetGlassByGlass(getGlassSlot1, getGlassSlot1B, getGlassSlot2, getGlassSlot2B, ref GlassA, ref GlassB, ref GlassC, ref GlassD, "CreatePortGetCommand");

                Logger.InfoFormat("[CreatePortGetCommand] Check getRobotModel OK; getRobotModel.GetEnable=true;getRobotModel:{0};getRobot:{1} ", getRobotModel.ModelName, getRobot.ToString());
                #region 生成命令
                Logger.InfoFormat("[CreatePortGetCommand] getRobotModel:{0};OutPriority:{1}", getRobotModel.ModelName, getRobotModel.OutPriority);
                //第一次下命令PortGetSlot数量始终为1，需定义全局变量根据Robot取片结果上报层数更新PortGetSlot，并且下次PortGetSlot+1,初始赋值为0
                var cmds = factory.CreatePortGetCommand(currentLoadingPort, ModelPosition, GlassA, GlassB, GlassC, GlassD, getRobot, getRobotModel.OutPriority);
                //cmd.GlassInfo = info;                    

                //cmd.GetGlass = Glass;
                //cmd.GetGlassA = GlassA;
                //cmd.GetGlassB = GlassB;
                for (int i = 0; i < cmds.Count; i++)
                {
                    var cmd = cmds[i];
                    cmd.STGetPosition1string = getRobotModel.ModelName;
                    AddCommand(cmd);
                    string glassinfostr = "";
                    if (cmd.GetGlassA != null)
                        glassinfostr += string.Format("GlassA:{0},{1};", cmd.GetGlassA.CassetteSequenceNo, cmd.GetGlassA.SlotSequenceNo);
                    if (cmd.GetGlassB != null)
                        glassinfostr += string.Format("GlassB:{0},{1};", cmd.GetGlassB.CassetteSequenceNo, cmd.GetGlassB.SlotSequenceNo);
                    if (cmd.GetGlassC != null)
                        glassinfostr += string.Format("GlassC:{0},{1};", cmd.GetGlassC.CassetteSequenceNo, cmd.GetGlassC.SlotSequenceNo);
                    if (cmd.GetGlassD != null)
                        glassinfostr += string.Format("GlassD:{0},{1};", cmd.GetGlassD.CassetteSequenceNo, cmd.GetGlassD.SlotSequenceNo);
                    Logger.InfoFormat("[PortGet Check]   OK  {0}({1},{2}), Arm({3}), Glass({4}), Position({5})",
                         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), glassinfostr, cmd.STGetSlotPostion1);
                    logInfo = string.Format("[     PortGet Check]   OK  {0}({1},{2}), Arm({3}), Glass({4}), Position({5})",
                         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), glassinfostr, cmd.STGetSlotPostion1);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion


                // Logger.InfoFormat("[CreatePortGetCommand] end ");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }
        //private void CreateOnlyBPortGetCommand(ref StringBuilder logStr)
        //{
        //    string logInfo = string.Format("");
        //    //Logger.Info("[CreateOnlyBPortGetCommand] begin ");
        //    //判断Robot手臂上是否有玻璃
        //    if (Robot.UpperExistOn || Robot.LowerExistOn)
        //    {
        //        Logger.Info("[CreateOnlyBPortGetCommand] Check Robot Up,Lower Exist NG; Robot Upper or Lower Hand  Have Glass;OnlyBPortGetCommand NG ");
        //        logInfo = string.Format("[     PortGet Check]   NG  Check Robot Hand NG:UpperExistOn = true or LowerExistOn = true;OnlyBPortGetCommand NG");
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    //Logger.Info("[CreateOnlyBPortGetCommand] Check Robot Up,Lower Exist OK;  ");
        //    //logInfo = string.Format("[CreateOnlyBPortGetCommand] Check Robot Up,Lower Exist OK;  ");
        //    //Logger.Info(logInfo);
        //    //logStr.Append(logInfo);
        //    //获取WaiForProcessing或者InProcessing状态的port
        //    #region 获取当前的port
        //    List<PortInfo> LoadingPortList = GetLoadingPossiblePortList(HostInfo.Current.PortList, "CreateOnlyBPortGetCommand");
        //    if (LoadingPortList == null || LoadingPortList.Count == 0)
        //    {
        //        Logger.Info("[CreateOnlyBPortGetCommand] Check LoadingPortList NG; Can not Found Processing Port;OnlyBPortGetCommand NG");
        //        return;
        //    }
        //    // Logger.Info("[CreateOnlyBPortGetCommand] Check LoadingPortList OK  ");
        //    //for (int i = 0; i < LoadingPortList.Count; i++)
        //    //{
        //    //    Logger.InfoFormat("[CreateOnlyBPortGetCommand] Check LoadingPort{0};PortID:{1},WaitingforProcessingTime:{2} ", (i + 1), LoadingPortList[i].PortID, LoadingPortList[i].WaitingforProcessingTime);
        //    //}
        //    //PortInfo currentLoadingPort = LoadingPortList.OrderBy(o => o.CassetteInfo.CassetteProcessStartTime).FirstOrDefault();
        //    PortInfo currentLoadingPort = LoadingPortList.FirstOrDefault(o => o.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing);
        //    if (currentLoadingPort == null)
        //    {
        //        // LoadingPortList = LoadingPortList.Where(o => o.WaitingforProcessingTime != DateTime.MinValue).ToList();
        //        currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
        //    }
        //    #endregion


        //    //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [PORT GET] Current Port:{0};ProcessStartTime:{1} ", currentLoadingPort.PortID, currentLoadingPort.CassetteInfo.CassetteProcessStartTime);
        //    //logInfo = string.Format("[CreateOnlyBPortGetCommand] [PORT GET] Current Port:{0};WaitingforProcessingTime:{1} ", currentLoadingPort.PortID, currentLoadingPort.WaitingforProcessingTime);
        //    //Logger.Info(logInfo);
        //    //logStr.Append(logInfo);
        //    var GlassInfos = currentLoadingPort.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();
        //    //var info = all.OrderBy(o => o.Position).FirstOrDefault();
        //    var AllGlassInfos = currentLoadingPort.GlassInfos.Where(o => o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();
        //    OnlyBCreatePortGetCommand(GlassInfos, AllGlassInfos, currentLoadingPort, ref logStr);
        //    // Logger.Info("[CreateOnlyBPortGetCommand] end ");
        //}

        //private void MixRunMode(ref List<PortInfo> LoadingPortList, ref PortInfo currentLoadingPort, ref List<GlassInfo> GlassInfos, ref GlassInfo getGlass, ref StringBuilder logStr)
        //{
        //    //Logger.InfoFormat("[MixRunMode][MixRunMode]  ");
        //    string logInfo = string.Format("");
        //    Hashtable hashtable = new Hashtable();
        //    hashtable.Add("EQPID", HostInfo.Current.EQPID);
        //    var MixRunConfigList = dbCmd.GetMIXRunConfigList(hashtable);
        //    MixRunConfigList = MixRunConfigList.Where(o => o.Exist).ToList();
        //    Logger.InfoFormat("[MixRunMode][MixRunMode] MixRunConfigList.Count:{0} ", MixRunConfigList.Count);
        //    if (MixRunConfigList.Count >= 2)
        //    {
        //        Logger.InfoFormat("[MixRunMode][MixRunConfigList.Count >= 2]  ");
        //        //如果两个都存在 查看投入数量不等于投入比例的recipe,该recipe即为当前需要跑的recipe
        //        //foreach (var MixRunConfigItem in MixRunConfigList)
        //        //{
        //        // var MIXRunInputRatio=MixRunConfigItem.MIXRunInputRatioList.FirstOrDefault(o => o.InputRatioID == MixRunConfigItem.CurrenRatioID);
        //        // if(MIXRunInputRatio!=null)
        //        // {

        //        // }
        //        //else
        //        // {
        //        //     Logger.InfoFormat("[MixRunMode][MIXRunInputRatio==null] MachineRecipeName:{0};CurrenRatioID:{1};", MixRunConfigItem.MachineRecipeName, MixRunConfigItem.CurrenRatioID);
        //        //     logInfo = string.Format("[  MixRunMode Check]   NG  [MIXRunInputRatio==null] MachineRecipeName:{0};CurrenRatioID:{1};", MixRunConfigItem.MachineRecipeName, MixRunConfigItem.CurrenRatioID);
        //        //     logStr.Append(logInfo);
        //        //     logStr.AppendLine();
        //        // }
        //        // var currentRecipe = MixRunConfigList.FirstOrDefault(o => o.InputCount < MIXRunInputRatio.InputRatio);
        //        var currentRecipe = MixRunConfigList.FirstOrDefault(o => o.InputCount < o.MIXRunInputRatioList.FirstOrDefault(m => m.InputRatioID == o.CurrenRatioID).InputRatio);
        //        if (currentRecipe == null)
        //        {
        //            Logger.InfoFormat("[MixRunMode][currentRecipe==null]  ");
        //            currentRecipe = MixRunConfigList.OrderBy(o => o.ID).ToList().FirstOrDefault();
        //            Logger.InfoFormat("[MixRunMode][currentRecipe==MixRunConfigList.FirstOrDefault()] CurrentRecipe:{0}  ", currentRecipe.MachineRecipeName);
        //            currentRecipe.InputCount = 0;
        //            dbCmd.UpdateMIXRunConfig(currentRecipe);
        //            Logger.InfoFormat("[MixRunMode] - MixRun Check.CurrentRecipe={0},InputCount = 0;[save to db UpdateMIXRunConfig] ", currentRecipe.MachineRecipeName);
        //        }
        //        var MIXRunInputRatio = currentRecipe.MIXRunInputRatioList.FirstOrDefault(o => o.InputRatioID == currentRecipe.CurrenRatioID);
        //        if (MIXRunInputRatio == null)
        //        {
        //            Logger.InfoFormat("[MixRunMode][MIXRunInputRatio==null] MachineRecipeName:{0};CurrenRatioID:{1};", currentRecipe.MachineRecipeName, currentRecipe.CurrenRatioID);
        //            logInfo = string.Format("[  MixRunMode Check]   NG  [MIXRunInputRatio==null] MachineRecipeName:{0};CurrenRatioID:{1};", currentRecipe.MachineRecipeName, currentRecipe.CurrenRatioID);
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //        }
        //        Logger.InfoFormat("[MixRunMode] CurrentRecipe:{0} ; InputCount:{1};InputRatio:{2};CurrenRatioID:{3} ", currentRecipe.MachineRecipeName, currentRecipe.InputCount, MIXRunInputRatio.InputRatio, currentRecipe.CurrenRatioID);
        //        //取该recipe下最早取片的port中的glass
        //        LoadingPortList = LoadingPortList.Where(o => o.CassetteInfo.MachineRecipeName == currentRecipe.MachineRecipeName).ToList();

        //        if (LoadingPortList.Count() > 0)
        //        {
        //            Logger.InfoFormat("[MixRunMode] Check LoadingPortList OK; LoadingPortList.Count():{0}", LoadingPortList.Count());
        //            //currentLoadingPort = LoadingPortList.OrderByDescending(o => o.CassetteInfo.CassetteProcessStartTime).FirstOrDefault();
        //            currentLoadingPort = LoadingPortList.FirstOrDefault(o => o.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing);
        //            if (currentLoadingPort == null)
        //            {
        //                //LoadingPortList = LoadingPortList.Where(o => o.WaitingforProcessingTime != DateTime.MinValue).ToList();
        //                currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
        //            }
        //            Logger.InfoFormat("[MixRunMode] CurrentLoadingPort:{0}", currentLoadingPort.PortID);
        //            GlassInfos = currentLoadingPort.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();
        //            Logger.InfoFormat("[MixRunMode] - MixRun Check.CurrentLoadingPort={1},GlassCount={0}", GlassInfos.Count, currentLoadingPort.PortID);
        //            var unitid = currentLoadingPort.UnitID;
        //            var PortID = currentLoadingPort.PortID;
        //            Logger.InfoFormat("[MixRunMode] CurrentLoadingPort UnitID:{0};PortID:{1}", unitid, PortID);
        //            var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == unitid);
        //            //int ModelPosition = 0;
        //            PortGetType PortGetType = PortGetType.ASC;
        //            // RobotModel CurrentModel = null;
        //            if (index != null)
        //            {
        //                Logger.InfoFormat("[MixRunMode] Check Index OK; Index:{0}", unitid);
        //                var model = index.RobotModelList.FirstOrDefault(o => o.PortID == PortID);
        //                if (model != null)
        //                {

        //                    // ModelPosition = model.ModelPosition;
        //                    PortGetType = model.PortGetType;
        //                    // CurrentModel = model;
        //                    Logger.InfoFormat("[MixRunMode] Check model OK; Model:{0};PortGetType:{1}", model.ModelName, PortGetType.ToString());
        //                }
        //                else
        //                {
        //                    Logger.InfoFormat("[MixRunMode] Check model NG; Model==null;");
        //                }
        //            }
        //            else
        //            {
        //                Logger.InfoFormat("[MixRunMode] Check Index NG; Index==null;");
        //            }
        //            Logger.InfoFormat("[MixRunMode] PortGetType:{0}", PortGetType.ToString());
        //            if (PortGetType == PortGetType.DESC)
        //            {
        //                getGlass = GlassInfos.OrderByDescending(o => o.Position).FirstOrDefault();
        //                Logger.InfoFormat("[MixRunMode][LineMode!=OnlyA or MixRun]  PortGetType==DESC,GlassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //            }
        //            else// if (PortGetType == PortGetType.ASC)
        //            {
        //                getGlass = GlassInfos.OrderBy(o => o.Position).FirstOrDefault();
        //                Logger.InfoFormat("[MixRunMode][LineMode!=OnlyA or MixRun] PortGetType==ASC,GlassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //            }

        //            // Logger.InfoFormat("[MixRunMode] GlassInfo[{0},{1}]", info.CassetteSequenceNo, info.SlotSequenceNo);
        //            //info = all.OrderBy(o => o.Position).FirstOrDefault();
        //            Logger.InfoFormat("[MixRunMode] CurrentGlassInfo ;glassInfo[{0},{1}];", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //        }
        //        else
        //        {
        //            Logger.InfoFormat("[MixRunMode] Check LoadingPortList NG; LoadingPortList.Count():{0}", LoadingPortList.Count());
        //            logInfo = string.Format("[  MixRunMode Check]   NG  Check LoadingPortList.Count={0}", LoadingPortList.Count());
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //        }
        //        //}
        //    }
        //    else
        //    {
        //        //如果只有一个存在 不走MixRun模式  不做处理
        //        Logger.InfoFormat("[MixRunMode][MixRunConfigList.Count  <2]  ");
        //        logInfo = string.Format("[  MixRunMode Check]   NG  MixRunRecipeConfigList <2");
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //    }
        //}

        //private void OnlyBCreatePortGetCommand(List<GlassInfo> GlassInfoList, List<GlassInfo> AllGlassInfos, PortInfo currentLoadingPort, ref StringBuilder logStr)
        //{
        //    string logInfo = string.Format("");
        //    #region 获取GlassB
        //    var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == currentLoadingPort.UnitID);
        //    int ModelPosition = 0;
        //    PortGetType PortGetType = PortGetType.ASC;
        //    RobotModel getRobotModel = null;
        //    if (index != null)
        //    {
        //        var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
        //        if (model != null)
        //        {
        //            ModelPosition = model.ModelPosition;
        //            PortGetType = model.PortGetType;
        //            getRobotModel = model;
        //        }
        //    }
        //    var GlassInfoBList = GlassInfoList.Where(o => o.SlotPosition == 2).ToList();
        //    GlassInfo GlassB = null;
        //    if (PortGetType == PortGetType.DESC)
        //    {
        //        GlassB = GlassInfoBList.OrderByDescending(o => o.Position).FirstOrDefault();
        //        // Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand][PortGetType== PortGetType.DESC]; glassInfo[{0},{1}] ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo);
        //    }
        //    else// if (PortGetType== PortGetType.ASC)
        //    {
        //        GlassB = GlassInfoBList.OrderBy(o => o.Position).FirstOrDefault();
        //        // Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand][PortGetType== PortGetType.ASC]; glassInfo[{0},{1}] ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo);
        //    }
        //    logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlasssB[{0},{1}];PortID:{2} ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, currentLoadingPort.PortID);
        //    Logger.Info(logInfo);
        //    #endregion

        //    // GlassInfo GlassB = GlassInfoBList.OrderBy(o => o.Position).FirstOrDefault();
        //    //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Current GlasssB; glassInfo[{0},{1}] ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo);

        //    //logStr.Append(logInfo);
        //    //获取A片数据
        //    GlassInfo GlassA = AllGlassInfos.FirstOrDefault(o => o.Position == GlassB.Position && o.SlotPosition == 1);



        //    // Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Current ModelPosition:{0} ", getRobotModel.ModelName);
        //    #region 获取glassB 手臂
        //    var GlassBGetRbot = RobotHand.Error;
        //    GlassBGetRbot = GetRobot(getRobotModel, GlassBGetRbot);
        //    if (GlassBGetRbot == RobotHand.Error)
        //    {
        //        Logger.Info("[OnlyBCreatePortGetCommand] Check GetRobot.RobotHand NG; RobotHand.Error");
        //        return;
        //    }
        //    #endregion

        //    if (GlassA != null)
        //    {
        //        #region 生成 取GlassA、GlassB命令
        //        //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA!=null;GlassA[{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo);
        //        logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA!=null;GlassA[{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo);
        //        Logger.Info(logInfo);
        //        // logStr.Append(logInfo);
        //        RobotHand GlassAGetRbot = RobotHand.Error;
        //        GlassAGetRbot = GlassBGetRbot == RobotHand.UpHand ? RobotHand.LowHand : RobotHand.UpHand;
        //        Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand]  GlassARbotHand:{0};GlassBRbotHand:{1}  ", GlassAGetRbot.ToString(), GlassBGetRbot.ToString());
        //        //  Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand]  getRobotModel:{0};getRobotModel.OutPriority:{1} ", getRobotModel.ModelName, getRobotModel.OutPriority);
        //        var cmd = factory.OnlyBCreatePortGetCommand(currentLoadingPort, ModelPosition, GlassAGetRbot, GlassA.Position, GlassA.SlotPosition, GlassBGetRbot, GlassB.Position, GlassB.SlotPosition, getRobotModel.OutPriority);
        //        cmd.GetGlassA = GlassA;
        //        cmd.GetGlassB = GlassB;
        //        cmd.STGetPosition1string = getRobotModel.ModelName;
        //        cmd.NDGetPosition2string = getRobotModel.ModelName;
        //        AddCommand(cmd);
        //        Logger.InfoFormat("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6} + {7}({8},{9}), Arm({10}), GlassB({11},{12}), Position({13}) ",
        //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassA.Position, cmd.STArmNo1.ToString(), GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, GlassA.SlotPosition, cmd.NDRCMD2.ToString(), cmd.NDGetPosition2string, GlassB.Position, cmd.NDArmNo2.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //        logInfo = string.Format("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6} + {7}({8},{9}), Arm({10}), GlassB({11},{12}), Position({13}) ",
        //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassA.Position, cmd.STArmNo1.ToString(), GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, GlassA.SlotPosition, cmd.NDRCMD2.ToString(), cmd.NDGetPosition2string, GlassB.Position, cmd.NDArmNo2.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        #endregion

        //    }
        //    else
        //    {
        //        #region 生成取GlassB命令
        //        //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA==null ");
        //        //logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA==null ");
        //        //Logger.Info(logInfo);
        //        //  logStr.Append(logInfo);
        //        //如果没有A片数据 生成B片命令发送给plc,B片在1       
        //        Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA==null  GlassBRbotHand:{0} ", GlassBGetRbot.ToString());
        //        var cmd = factory.CreatePortGetCommand(currentLoadingPort, ModelPosition, GlassB, GlassBGetRbot, getRobotModel.OutPriority, GlassB.SlotPosition);
        //        cmd.GetGlassB = GlassB;
        //        cmd.STGetPosition1string = getRobotModel.ModelName;
        //        AddCommand(cmd);
        //        Logger.InfoFormat("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6})",
        //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassB.Position, cmd.STArmNo1.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //        logInfo = string.Format("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6})",
        //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassB.Position, cmd.STArmNo1.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        #endregion

        //    }
        //    //根据B片数据获取下个position 并且得到B片手臂
        //    //var targetlist = FindNextStage(ModelPosition, "", GlassB.ModePath);
        //    //if(targetlist.Count==0)
        //    //{
        //    //    Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Check TargetList NG; targetlist.Count==0;GlassB[{0},{1}];CreatePortGetCommand NG ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo);
        //    //}
        //    //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Check TargetList OK; targetlist.Count{0}", targetlist.Count());
        //    //foreach (var target in targetlist)
        //    //{
        //    //    //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Target ModelPosition{0}", t.ModelPosition);
        //    //    logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Target ModelPosition{0}", target.ModelPosition);
        //    //    Logger.Info(logInfo);
        //    //   // logStr.Append(logInfo);
        //    //    var GlassBRbotHand = PortGetHandCheck(Robot, target.PathConfigure);
        //    //    if (GlassBRbotHand == RobotHand.Error)
        //    //    {
        //    //        Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Check GlassBRbotHand NG; GlassB[{0},{1}];CreatePortGetCommand NG ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo);
        //    //        logInfo = string.Format("[    OnlyBGet Check][OnlyBCreatePortGetCommand] Check GlassBRbotHand NG; GlassB[{0},{1}];CreatePortGetCommand NG ", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo);
        //    //        logStr.Append(logInfo);
        //    //        logStr.AppendLine();
        //    //        continue;
        //    //    }
        //    //    // Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Check GlassBRbotHand OK; GlassBRbotHand:{0} ", GlassBRbotHand.ToString());
        //    //    logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] Check GlassBRbotHand OK; GlassBRbotHand:{0} ", GlassBRbotHand.ToString());
        //    //    Logger.Info(logInfo);
        //    //    RobotModel targetmode = null;
        //    //    foreach (var targetUnitItem in HostInfo.Current.EQPInfo.Units)
        //    //    {
        //    //        var mode = targetUnitItem.RobotModelList.FirstOrDefault(o => o.ModelPosition == target.ModelPosition);
        //    //        if (mode != null)
        //    //        {
        //    //            targetmode = mode;
        //    //            break;
        //    //        }
        //    //    }
        //    //    //logStr.Append(logInfo);
        //    //    //如果有A片数据 将A片B片组合成一个命令发送给plc, A片在1   B片在2;
        //    //    if (GlassA!=null)
        //    //    {
        //    //        //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA!=null;GlassA[{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo);
        //    //        logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA!=null;GlassA[{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo);
        //    //        Logger.Info(logInfo);
        //    //       // logStr.Append(logInfo);
        //    //        RobotHand GlassARbotHand = RobotHand.Error;                    
        //    //        GlassARbotHand = GlassBRbotHand == RobotHand.UpHand?RobotHand.LowHand: RobotHand.UpHand;
        //    //        Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand]  GlassARbotHand:{0} ", GlassARbotHand.ToString());
        //    //        Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand]  getRobotModel:{0};getRobotModel.OutPriority:{1} ", getRobotModel.ModelName, getRobotModel.OutPriority);
        //    //        var cmd = factory.OnlyBCreatePortGetCommand(currentLoadingPort, ModelPosition, GlassARbotHand,GlassA.Position, GlassA.SlotPosition, GlassBRbotHand, GlassB.Position, GlassB.SlotPosition, targetmode.OutPriority);
        //    //        cmd.GetGlassA = GlassA;
        //    //        cmd.GetGlassB = GlassB;
        //    //        cmd.STGetPosition1string = getRobotModel.ModelName;
        //    //        cmd.NDGetPosition2string= getRobotModel.ModelName;
        //    //        AddCommand(cmd);
        //    //        Logger.InfoFormat("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6} + {7}({8},{9}), Arm({10}), GlassB({11},{12}), Position({13}) ",
        //    //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassA.Position, cmd.STArmNo1.ToString(), GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, GlassA.SlotPosition, cmd.NDRCMD2.ToString(), cmd.NDGetPosition2string, GlassB.Position, cmd.NDArmNo2.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //    //        logInfo = string.Format("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6} + {7}({8},{9}), Arm({10}), GlassB({11},{12}), Position({13}) ",
        //    //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassA.Position, cmd.STArmNo1.ToString(), GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, GlassA.SlotPosition, cmd.NDRCMD2.ToString(), cmd.NDGetPosition2string, GlassB.Position, cmd.NDArmNo2.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition );
        //    //        logStr.Append(logInfo);
        //    //        logStr.AppendLine();
        //    //    }
        //    //    else
        //    //    {
        //    //        //Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA==null ");
        //    //        logInfo = string.Format("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA==null ");
        //    //        Logger.Info(logInfo);
        //    //        //  logStr.Append(logInfo);
        //    //        //如果没有A片数据 生成B片命令发送给plc,B片在1       
        //    //        Logger.InfoFormat("[CreateOnlyBPortGetCommand] [OnlyBCreatePortGetCommand] GlassA==null  getRobotModel:{0};getRobotModel.OutPriority:{1} ", getRobotModel.ModelName, targetmode.OutPriority);
        //    //        var cmd = factory.CreatePortGetCommand(currentLoadingPort, ModelPosition, GlassB, GlassBRbotHand, getRobotModel.OutPriority, GlassB.SlotPosition);
        //    //        cmd.GetGlassB = GlassB;
        //    //        cmd.STGetPosition1string = getRobotModel.ModelName;
        //    //        AddCommand(cmd);
        //    //        Logger.InfoFormat("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6})",
        //    //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassB.Position, cmd.STArmNo1.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //    //        logInfo = string.Format("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6})",
        //    //              cmd.STRCMD1.ToString(), cmd.STGetPosition1string, GlassB.Position, cmd.STArmNo1.ToString(), GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, GlassB.SlotPosition);
        //    //        logStr.Append(logInfo);
        //    //        logStr.AppendLine();
        //    //    }

        //    //    //Logger.InfoFormat("[PORT GET] Port {0} Get from {1} for Glass {2} ", currentLoadingPort.UnitName, GlassB.Position, GlassB.GlassID);
        //    //}


        //}

        //private void MixRunCreatePortGetCommand(List<GlassInfo> GlassInfoList, PortInfo currentLoadingPort)
        //{
        //    GlassInfoList = GlassInfoList.Where(o => o.SlotPosition == 2).ToList();
        //    GlassInfo GlassB = GlassInfoList.OrderBy(o => o.Position).FirstOrDefault();
        //    //获取A片数据
        //    GlassInfo GlassA = GlassInfoList.FirstOrDefault(o => o.Position == GlassB.Position && o.SlotPosition == 1);

        //    var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == currentLoadingPort.UnitID);
        //    int ModelPosition = 0;
        //    if (index != null)
        //    {
        //        var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
        //        if (model != null)
        //        {
        //            ModelPosition = model.ModelPosition;
        //        }
        //    }
        //    //根据B片数据获取下个position 并且得到B片手臂
        //    var targetlist = FindNextStage(ModelPosition, "", GlassB.ModePath);
        //    foreach (var t in targetlist)
        //    {
        //        var GlassBRbotHand = PortGetHandCheck(Robot, t.PathConfigure);
        //        if (GlassBRbotHand == RobotHand.Error)
        //        {
        //            Logger.InfoFormat("[PORT GET] Cannot Find Hand for Port :{0}, Arm Have Glass ", currentLoadingPort.UnitName);
        //            continue;
        //        }
        //        //如果有A片数据 将A片B片组合成一个命令发送给plc, A片在1   B片在2;
        //        if (GlassA != null)
        //        {
        //            RobotHand GlassARbotHand = RobotHand.Error;

        //            GlassARbotHand = GlassBRbotHand == RobotHand.UpHand ? RobotHand.LowHand : RobotHand.UpHand;
        //            var cmd = factory.OnlyBCreatePortGetCommand(currentLoadingPort, ModelPosition, GlassARbotHand, GlassA.Position, GlassA.SlotPosition, GlassBRbotHand, GlassB.Position, GlassB.SlotPosition, t.PathConfigure.OutPriority);
        //            AddCommand(cmd);
        //        }
        //        else
        //        {
        //            //如果没有A片数据 生成B片命令发送给plc,B片在1                                         
        //            var cmd = factory.CreatePortGetCommand(currentLoadingPort, ModelPosition, GlassB, GlassBRbotHand, t.PathConfigure.OutPriority, GlassB.SlotPosition);
        //            AddCommand(cmd);
        //        }

        //        Logger.InfoFormat("[PORT GET] Port {0} Get from {1} for Glass {2} ", currentLoadingPort.UnitName, GlassB.Position, GlassB.GlassID);
        //    }


        //}
        /// <summary>
        /// 从设备取panel
        /// </summary>
        private void CreateProcessGetCommand(ref StringBuilder logStr)
        {
            try
            {
                string logInfo = string.Format("");
                Logger.InfoFormat("[CreateProcessGetCommand] begin ");
                foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
                {
                    Logger.InfoFormat("[CreateProcessGetCommand] UnitName{0} RobotModelList.count:{1} ", unitItem.GetType().Name, unitItem.RobotModelList.Count);
                    if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
                    {
                        // List<GlassInfo> glassInfoList = new List<GlassInfo>();
                        Logger.InfoFormat("[CreateProcessGetCommand] unitName:{0} ", unitItem.UnitName);
                        foreach (var getRobotModel in unitItem.RobotModelList)
                        {
                            // Logger.InfoFormat("[CreateProcessGetCommand]unitName:{0}; getRobotModel:{1} ", unitItem.UnitName, getRobotModel.ModelName);
                            #region 校验Trouble信号
                            Linksignal upLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == getRobotModel.UPLinkName);
                            if (CheckUpLinkStatus(upLinksignal, unitItem, ref logStr))
                            {
                                Logger.InfoFormat("[CreateProcessGetCommand]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
                            }
                            else
                            {
                                Logger.InfoFormat("[CreateProcessGetCommand] - Pre Check NG.UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);
                                logInfo = string.Format("[  GetCommand Check]   NG  Condition NG,UpstreamLinkSignal={0}", getRobotModel.UPLinkName);

                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            #endregion
                            //#region 获取GetGlass资料
                            //GlassInfo getGlass = null;
                            //switch (getRobotModel.UsedJobBlockNo)
                            //{
                            //    case 0:
                            //        if (getRobotModel.GlassA != null)
                            //        {
                            //            getGlass = getRobotModel.GlassA;
                            //            Logger.InfoFormat("[CreateProcessGetCommand][UsedJobBlockNo==0] GlassA getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //    case 1:
                            //        if (getRobotModel.GlassA != null)
                            //        {
                            //            getGlass = getRobotModel.GlassA;
                            //            //Logger.InfoFormat("[CreateProcessGetCommand][UsedJobBlockNo==1] GlassA getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //    case 2:
                            //        if (getRobotModel.GlassB != null)
                            //        {
                            //            getGlass = getRobotModel.GlassB;
                            //            // Logger.InfoFormat("[CreateProcessGetCommand][UsedJobBlockNo==2] GlassB getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //    case 3:
                            //        if (getRobotModel.GlassA != null)
                            //        {
                            //            getGlass = getRobotModel.GlassA;
                            //            //Logger.InfoFormat("[CreateProcessGetCommand][UsedJobBlockNo==3] GlassA getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //}
                            if (getRobotModel.GlassA == null && getRobotModel.GlassB == null && getRobotModel.GlassC == null && getRobotModel.GlassD == null)
                            {
                                Logger.InfoFormat("[CreateProcessGetCommand][getGlass==null] - Check Data NG.Glass is null");
                                logInfo = string.Format("[  GetCommand Check]   NG  Glass is null");
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            Logger.InfoFormat("[CreateProcessGetCommand]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
                            Logger.InfoFormat("[CreateProcessGetCommand] - Check UpstreamLinkSignal and getGlass OK;link.LinkName:{0} ", getRobotModel.UPLinkName);

                            #endregion

                            #region 校验getRobotModel 
                            if (!getRobotModel.GetEnable)
                            {
                                Logger.InfoFormat("[CreateProcessGetCommand] - Check Condition NG.ModelName={0},GetEnable is false ", getRobotModel.ModelName);
                                logInfo = string.Format("[  GetCommand Check]   NG  ModelName={0},GetEnable is false", getRobotModel.ModelName);
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            // Logger.InfoFormat(" [CreateProcessGetCommand] Check item.GetEnable OK,GetEnable==true;ModelName:{0};", getRobotModel.ModelName);
                            if (!getRobotModel.DualArm)
                            {
                                if (Robot.UpperExistOn || Robot.LowerExistOn)//有一个手臂有片 就不取了 等到放完片再执行取片
                                {
                                    Logger.InfoFormat("[CreateProcessGetCommand] - Check RobotArm NG.ModelName={0},Arm(Upper) is {1},Arm(Lower) is {2},", getRobotModel.ModelName, Robot.UpperExistOn, Robot.LowerExistOn);
                                    logInfo = string.Format("[  CreateProcessGetCommand Check]   NG  Arm Has Glass,ModelName={0},Arm(Upper) is {1},Arm(Lower) is {2}", getRobotModel.ModelName, Robot.UpperExistOn, Robot.LowerExistOn);
                                    logStr.Append(logInfo);
                                    logStr.AppendLine();
                                    continue;
                                }
                                else
                                {
                                    //Logger.InfoFormat("[CreateProcessGetCommand] Check Hand Exist OK,Robot.UpperExistOn==false and Robot.LowerExistOn==false;  ");
                                }
                            }
                            else
                            {
                                //Logger.InfoFormat("[CreateProcessGetCommand] [getRobotModel.DualArm=true] Model:{0}", getRobotModel.ModelName);
                                if (Robot.UpperExistOn || Robot.LowerExistOn)
                                {
                                    // Logger.InfoFormat("[CreateProcessGetCommand] [getRobotModel.DualArm=true] [Robot.UpperExistOn=true || Robot.LowerExistOn=true]");
                                    var robotHand = RobotHand.LowHand;
                                    if (Robot.UpperExistOn)
                                        robotHand = RobotHand.UpHand;
                                    //LineMode LineMode = HostInfo.Current.EQPInfo.LineMode;//putJobData.ProcessMode;
                                    //Logger.InfoFormat("[CreateProcessGetCommand] LineMode:{0}", LineMode.ToString());
                                    #region 获取putGlass
                                    GlassInfo putGlass = null;
                                    //switch (LineMode)
                                    //{
                                    //    case LineMode.OnlyA:
                                    //        if (robotHand == RobotHand.LowHand)
                                    //        {
                                    //            if (Robot.LowHandGlass1 != null)
                                    //            {
                                    //                putGlass = Robot.LowHandGlass1;
                                    //                //Logger.InfoFormat("[CreateProcessGetCommand][ProcessMode.OnlyA][Robot.LowHandGlass1 != null]  GlassInfo glassInfo[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                                    //            }
                                    //            else
                                    //            {
                                    //                // Logger.InfoFormat("[CreateProcessGetCommand][ProcessMode.OnlyA][Robot.LowHandGlass1 == null]    ");
                                    //            }
                                    //        }
                                    //        else if (robotHand == RobotHand.UpHand)
                                    //        {
                                    //            if (Robot.UpHandGlass1 != null)
                                    //            {
                                    //                putGlass = Robot.UpHandGlass1;
                                    //                // Logger.InfoFormat("[CreateProcessGetCommand][ProcessMode.OnlyA][Robot.UpHandGlass1 != null]  GlassInfo glassInfo[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                                    //            }
                                    //            else
                                    //            {
                                    //                // Logger.InfoFormat("[CreateProcessGetCommand][ProcessMode.OnlyA][Robot.UpHandGlass1 == null]    ");
                                    //            }
                                    //        }
                                    //        break;
                                    //    default:
                                    if (robotHand == RobotHand.LowHand)
                                    {
                                        //  Logger.Info("[CreateProcessGetCommand] [robotHand == RobotHand.LowHand]");
                                        if (Robot.LowHandGlass1 != null)
                                        {
                                            putGlass = Robot.LowHandGlass1;
                                            // Logger.InfoFormat("[CreateProcessGetCommand] [Robot.LowHandGlass1!=null]  GlassInfo glassInfo[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                                        }
                                        else if (Robot.LowHandGlass2 != null)
                                        {
                                            putGlass = Robot.LowHandGlass2;
                                            //  Logger.InfoFormat("[CreateProcessGetCommand] [Robot.LowHandGlass2!=null]  GlassInfo glassInfo[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                                        }
                                        else
                                        {
                                            //Logger.InfoFormat("[CreateProcessGetCommand] [robotHand == RobotHand.LowHand] LowHandGlass1 and LowHandGlass2==null ");
                                        }
                                    }
                                    else if (robotHand == RobotHand.UpHand)
                                    {
                                        // Logger.Info("[CreateProcessGetCommand] robotHand == RobotHand.UpHand");
                                        if (Robot.UpHandGlass1 != null)
                                        {
                                            putGlass = Robot.UpHandGlass1;
                                            //Logger.InfoFormat("[CreateProcessGetCommand] [Robot.UpHandGlass1!=null]  GlassInfo glassInfo[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                                        }
                                        else if (Robot.UpHandGlass2 != null)
                                        {
                                            putGlass = Robot.UpHandGlass2;
                                            //  Logger.InfoFormat("[CreateProcessGetCommand] [Robot.UpHandGlass2!=null]  GlassInfo glassInfo[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                                        }
                                        else
                                        {
                                            //   Logger.InfoFormat("[CreateProcessGetCommand] [robotHand == RobotHand.UpHand] UpHandGlass1 and UpHandGlass2 ==null ");
                                        }
                                    }
                                    //        break;
                                    //}
                                    //putJobData.ModelPosition

                                    //if (putGlass == null)
                                    //{
                                    //    //Logger.InfoFormat("[CreateProcessGetCommand] [getRobotModel.DualArm=true] [putJobData=null];getRobotModel.ModelName:{0};ProcessGetCommand NG  ", getRobotModel.ModelName);
                                    //}
                                    //#endregion
                                    //else
                                    //{
                                    //    // Logger.InfoFormat("[CreateProcessGetCommand] PutJobData:[{0},{1}];putGlass.ModelPosition:{2}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putGlass.ModelPosition);
                                    //    //var putJobPort = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putJobData.PortID);
                                    //    //Logger.InfoFormat("[CreateProcessGetCommand] putJobPort:{0} ", putJobPort.PortID);
                                    //    //var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == putJobPort.UnitID);
                                    //    #region 获取PutModel
                                    //    //RobotModel putRobotModel = null;
                                    //    RobotModel putRobotModel = GetCurrentModel(putGlass.ModelPosition);
                                    //    //foreach (var unit in HostInfo.Current.EQPInfo.Units)
                                    //    //{
                                    //    //    var model = unit.RobotModelList.FirstOrDefault(o => o.ModelPosition == putGlass.ModelPosition);
                                    //    //    if (model != null)
                                    //    //    {
                                    //    //        putRobotModel = model;
                                    //    //        break;
                                    //    //    }
                                    //    //}
                                    //    if (putRobotModel == null)
                                    //    {
                                    //        //Logger.InfoFormat("[CreateProcessGetCommand] [item.DualArm=true] [putJobModel=null]getRobotModel.ModelName:{0};ProcessGetCommand NG  ", getRobotModel.ModelName);
                                    //    }
                                    //    #endregion
                                    //    else
                                    //    {
                                    //        // Logger.InfoFormat("[CreateProcessGetCommand] PutJobModel:{0} ", putRobotModel.ModelName);
                                    //        if (putRobotModel.GroupName == getRobotModel.GroupName)
                                    //        {
                                    //            Logger.InfoFormat("[CreateProcessGetCommand][item.DualArm=true] - Check GroupName NG.putJobModel.GroupName==Model.GroupName,ModelName:{0} ;GroupName:{1}", getRobotModel.ModelName, getRobotModel.GroupName);
                                    //            logInfo = string.Format("[  GetCommand Check]   NG  PutJobModel.GroupName==Model.GroupName, ModelName:{0};GroupName:{1}", getRobotModel.ModelName, getRobotModel.GroupName);
                                    //            logStr.Append(logInfo);
                                    //            logStr.AppendLine();
                                    //            continue;
                                    //        }
                                    //        else
                                    //        {
                                    //            // Logger.InfoFormat("[CreateProcessGetCommand][item.DualArm=true] Check Model.GroupName OK,putJobModel.GroupName != Model.GroupName;");
                                    //        }

                                    //    }

                                    //}
                                }
                                else
                                {
                                    //Logger.InfoFormat("[CreateProcessGetCommand] [getRobotModel.DualArm=true] [Robot.UpperExistOn=false; Robot.LowerExistOn=false]getRobotModel.ModelName:{0};ProcessGetCommand NG  ", getRobotModel.ModelName);
                                }
                            }
                            #endregion

                            //#region 校验getUnit InLineMode
                            ////if (unitItem.CommandType == 1)
                            ////{
                            ////    if (!unitItem.UpstreamInlineMode || unitItem.LoadingStop)
                            ////    {
                            ////        Logger.InfoFormat("[CreateProcessGetCommand] Check UpstreamInlineMode and LoadingStop NG; unitItem.UpstreamInlineMode=false or unitItem.LoadingStop=true;Unit:{0};ProcessGetCommand NG", unitItem.UnitName);
                            ////        logInfo = string.Format("[  GetCommand Check]   NG  Check UpstreamInlineMode or LoadingStop NG;Unit:{0};ProcessGetCommand NG", unitItem.UnitName);
                            ////        logStr.Append(logInfo);
                            ////        logStr.AppendLine();
                            ////        continue;
                            ////    }
                            ////   // Logger.InfoFormat("[CreateProcessGetCommand] Check UpstreamInlineMode and LoadingStop OK; unitItem.UpstreamInlineMode=true; unitItem.LoadingStop=false");
                            ////}
                            //#endregion

                            //0 - None 1 - 前片 2 - 后片 99 - 前后片
                            //int slotPostion = 99;

                            //#region 获取slotPostion
                            ////if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                            ////{
                            ////    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                            ////    {
                            ////        slotPostion = getGlass.SlotPosition;
                            ////        //  Logger.InfoFormat("[CreateProcessGetCommand] [LineMode == LineMode.OnlyA]  slotPostion {0} ", slotPostion);
                            ////    }
                            ////    else
                            ////    {
                            ////        slotPostion = getRobotModel.UsedJobBlockNo == 3 ? 99 : getGlass.SlotPosition;
                            ////        //  Logger.InfoFormat("[CreateProcessGetCommand] [LineMode!= LineMode.OnlyA]  slotPostion {0} ", slotPostion);
                            ////    }
                            ////}
                            ////else
                            ////{
                            ////    slotPostion = 0;
                            ////}
                            //#endregion

                            #region 获取取片手臂
                            RobotHand getRobot = RobotHand.Error;
                            getRobot = GetRobot(getRobotModel, getRobot);
                            if (getRobot == RobotHand.Error)
                            {
                                //Logger.Info("[CreateProcessGetCommand] Check GetRobot.RobotHand NG; RobotHand.Error");
                                //return;
                                Logger.InfoFormat("[CreateProcessGetCommand] - Check GetRobot.RobotHand NG; RobotHand.Error ");
                                logInfo = string.Format("[  GetCommand Check]   NG Check GetRobot.RobotHand NG; RobotHand.Error");
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            #endregion

                            #region 判断取片是否有资料
                            string HasJobInfo = "";
                            if (getRobotModel.SendPositionFront1)
                            {
                                if (getRobotModel.GlassA == null)
                                {
                                    HasJobInfo += "GlassA Is Null;";
                                }
                                else if (String.IsNullOrEmpty(getRobotModel.GlassA.GlassGradeCode))
                                {
                                    HasJobInfo += "GlassA Grade Is Null;";
                                }
                            }
                            if (getRobotModel.SendPositionBack1)
                            {
                                if (getRobotModel.GlassB == null)
                                {
                                    HasJobInfo += "GlassB Is Null;";
                                }
                                else if (String.IsNullOrEmpty(getRobotModel.GlassB.GlassGradeCode))
                                {
                                    HasJobInfo += "GlassB Grade Is Null;";
                                }
                            }
                            if (getRobotModel.SendPositionFront2)
                            {
                                if (getRobotModel.GlassC == null)
                                {
                                    HasJobInfo += "GlassC Is Null;";
                                }
                                else if (String.IsNullOrEmpty(getRobotModel.GlassC.GlassGradeCode))
                                {
                                    HasJobInfo += "GlassC Grade Is Null;";
                                }
                            }
                            if (getRobotModel.SendPositionBack2)
                            {
                                if (getRobotModel.GlassD == null)
                                {
                                    HasJobInfo += "GlassD Is Null;";
                                }
                                else if (String.IsNullOrEmpty(getRobotModel.GlassD.GlassGradeCode))
                                {
                                    HasJobInfo += "GlassD Grade Is Null;";
                                }
                            }
                            if (!String.IsNullOrEmpty(HasJobInfo))
                            {
                                Logger.InfoFormat("[CreateProcessGetCommand] - {0}", HasJobInfo);
                                logInfo = string.Format("[CreateProcessGetCommand] - {0}", HasJobInfo);
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }

                            #endregion

                            //Logger.InfoFormat(" [CreateProcessGetCommand] Check getRobotModel OK,GetEnable==true;getRobotModel:{0};getRobotModel.DualArm:{1};getRobot:{2}", getRobotModel.ModelName, getRobotModel.DualArm, getRobot.ToString());
                            #region 生成命令
                            //Logger.InfoFormat("[CreateProcessGetCommand]  modelItem:{0};modelItem.OutPriority:{1} ", getRobotModel.ModelName, getRobotModel.OutPriority);
                            string errinfo = "";
                            var cmds = factory.CreateProcessGetCommand(unitItem, getRobot, getRobotModel, ref errinfo);
                            if (!String.IsNullOrEmpty(errinfo))
                            {
                                Logger.InfoFormat($"[CreateProcessGetCommand] - Create Cmd NG; {errinfo}");
                                logInfo = string.Format($"[CreateProcessGetCommand] - Create Cmd NG; {errinfo}");
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                            }
                            //GlassInfo GlassA = null;
                            //GlassInfo GlassB = null;
                            //GlassInfo Glass = null;
                            //GetUnitModelGlass(getRobotModel, ref Glass, ref GlassA, ref GlassB, "CreateProcessGetCommand");
                            //cmd.GetGlass = Glass;
                            //cmd.GetGlassA = GlassA;
                            //cmd.GetGlassB = GlassB;
                            //cmd.GlassInfo = glassInfo;

                            //var GetPosition1 = GetCurrentModel(getGlass.ModelPosition);
                            if (cmds.Count == 0)
                            {
                                //Logger.Info("[CreateProcessGetCommand] Check GetRobot.RobotHand NG; RobotHand.Error");
                                //return;
                                Logger.InfoFormat($"[CreateProcessGetCommand] - Create Cmd NG; Cmd is null, RobotHand={getRobot.ToString()} robotmodel.EQPSendSlotNoA={getRobotModel.EQPSendSlotNoA} robotmodel.EQPSendSlotNoB={getRobotModel.EQPSendSlotNoB}");
                                logInfo = string.Format($"[CreateProcessGetCommand] - Create Cmd NG; Cmd is null, RobotHand={getRobot.ToString()} robotmodel.EQPSendSlotNoA={getRobotModel.EQPSendSlotNoA} robotmodel.EQPSendSlotNoB={getRobotModel.EQPSendSlotNoB}");
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            for (int i = 0; i < cmds.Count; i++)
                            {
                                var cmd = cmds[i];
                                cmd.STGetPosition1string = getRobotModel.ModelName;
                                AddCommand(cmd);
                                string glassinfostr = "";
                                if (cmd.GetGlassA != null)
                                    glassinfostr += string.Format("GlassA:{0},{1},{2};", cmd.GetGlassA.CassetteSequenceNo, cmd.GetGlassA.SlotSequenceNo, cmd.GetGlassA.GlassGradeCode);
                                if (cmd.GetGlassB != null)
                                    glassinfostr += string.Format("GlassB:{0},{1},{2};", cmd.GetGlassB.CassetteSequenceNo, cmd.GetGlassB.SlotSequenceNo, cmd.GetGlassB.GlassGradeCode);
                                if (cmd.GetGlassC != null)
                                    glassinfostr += string.Format("GlassC:{0},{1},{2};", cmd.GetGlassC.CassetteSequenceNo, cmd.GetGlassC.SlotSequenceNo, cmd.GetGlassC.GlassGradeCode);
                                if (cmd.GetGlassD != null)
                                    glassinfostr += string.Format("GlassD:{0},{1},{2};", cmd.GetGlassD.CassetteSequenceNo, cmd.GetGlassD.SlotSequenceNo, cmd.GetGlassD.GlassGradeCode);

                                Logger.InfoFormat("[CreateProcessGetCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4}), Position({5})",
                                     cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), glassinfostr, cmd.STGetSlotPostion1);
                                logInfo = string.Format("[  GetCommand Check]   OK  {0}({1},{2}), Arm({3}), Glass({4}), Position({5})",
                                     cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), glassinfostr, cmd.STGetSlotPostion1);
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                return;
                            }
                            #endregion                                  
                        }
                    }
                }
                // Logger.InfoFormat("[CreateProcessGetCommand] end ");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }


        //private RobotHand PutRobot(RobotModel putRobotModel, RobotHand Robot)
        //{
        //    if (putRobotModel.PutArm != 0)
        //    {
        //        Robot = (RobotHand)putRobotModel.PutArm;
        //    }
        //    else
        //    {
        //        if (!robot.LowerExistOn)
        //            Robot = RobotHand.LowHand;
        //        else if (!robot.UpperExistOn)
        //            Robot = RobotHand.UpHand;
        //    }

        //    return Robot;
        //}
        /// <summary>
        /// 从设备取panel
        /// </summary>
        //private void CreateOnlyBProcessGetCommand(ref StringBuilder logStr)
        //{
        //    try
        //    {
        //        //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] begin ");
        //        string logInfo = "";
        //        foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
        //        {
        //            if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
        //            {
        //                // List<GlassInfo> glassInfoList = new List<GlassInfo>();
        //                //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] UnitItem:{0} ", unitItem.UnitName);
        //                foreach (var getRobotModel in unitItem.RobotModelList)
        //                {
        //                    //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] UnitItem:{0};RobotMode:{1} ", unitItem.UnitName, getRobotModel.ModelName);
        //                    #region 校验Trouble信号
        //                    Linksignal upLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == getRobotModel.UPLinkName);
        //                    if (CheckUpLinkStatus(upLinksignal, unitItem, ref logStr))
        //                    {
        //                        //Logger.InfoFormat("[CreateOnlyBProcessGetCommand]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
        //                    }
        //                    else
        //                    {
        //                        // Logger.InfoFormat("[CreateOnlyBProcessGetCommand]  UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);
        //                        logInfo = string.Format("[    OnlyBGet Check]   NG  Check Condition NG,LinkName={0}", getRobotModel.UPLinkName);

        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    #endregion
        //                    #region 获取设备上要取的glass
        //                    GlassInfo getGlass = null;
        //                    switch (getRobotModel.UsedJobBlockNo)
        //                    {
        //                        case 0:
        //                            if (getRobotModel.GlassA != null)
        //                            {
        //                                getGlass = getRobotModel.GlassA;
        //                                // Logger.InfoFormat("[CreateOnlyBProcessGetCommand] UsedJobBlockNo=0;glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //                            }
        //                            break;
        //                        case 1:
        //                            if (getRobotModel.GlassA != null)
        //                            {
        //                                getGlass = getRobotModel.GlassA;
        //                                //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] UsedJobBlockNo=1;glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //                            }
        //                            break;
        //                        case 2:
        //                            if (getRobotModel.GlassB != null)
        //                            {
        //                                getGlass = getRobotModel.GlassB;
        //                                // Logger.InfoFormat("[CreateOnlyBProcessGetCommand] UsedJobBlockNo=2;glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //                            }
        //                            break;
        //                        case 3:
        //                            if (getRobotModel.GlassA != null)
        //                            {
        //                                getGlass = getRobotModel.GlassA;
        //                                // Logger.InfoFormat("[CreateOnlyBProcessGetCommand] UsedJobBlockNo=3;glassInfo[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //                            }
        //                            break;
        //                    }
        //                    #endregion

        //                    if (getGlass != null)
        //                    {
        //                        //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] Check RobotModel GlassInfo OK; GlassInfo!=null glassInfo[{0},{1}];RobotModel:{2} ", glassInfo.CassetteSequenceNo, glassInfo.SlotSequenceNo, item.ModelName);
        //                        //logInfo = string.Format("[CreateOnlyBProcessGetCommand] - Check GlassInfo OK; getGlass[{0},{1}];getRobotModel:{2} ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getRobotModel.ModelName);
        //                        Logger.InfoFormat(logInfo);
        //                        //logStr.Append(logInfo);
        //                        if (unitItem.CommandType == 1)
        //                        {
        //                            if (!unitItem.UpstreamInlineMode || unitItem.LoadingStop)
        //                            {
        //                                Logger.InfoFormat("[CreateOnlyBProcessGetCommand] - Check Condition NG.UnitName={0},UpstreamInlineMode is {0},LoadingStop is {2}", unitItem.UnitName, unitItem.UpstreamInlineMode, unitItem.LoadingStop);
        //                                logInfo = string.Format("[    OnlyBGet Check]   NG  Check unit{0} UpstreamInlineMode or LoadingStop NG", unitItem.UnitName);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                continue;
        //                            }
        //                            //logInfo = string.Format("[CreateOnlyBProcessGetCommand] Check UpstreamInlineMode and LoadingStop OK; unit{0} ", unitItem.UnitName);
        //                        }

        //                        //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] Check UpstreamInlineMode and LoadingStop OK; unit{0} ", unitItem.UnitName);

        //                        //Logger.InfoFormat(logInfo);
        //                        // logStr.Append(logInfo);
        //                        //var targetlist = FindNextStage(getGlass.ModelPosition, "", getGlass.ModePath);                               
        //                        // var targetlist = FindNextStage(jobStage.ModelPosition, "");//Ruleid传空值过去
        //                        #region 获取手臂
        //                        RobotHand getRobot = RobotHand.Error;
        //                        getRobot = GetRobot(getRobotModel, getRobot);
        //                        if (getRobot == RobotHand.Error)
        //                        {
        //                            //Logger.Info("[CreateOnlyBProcessGetCommand] Check GetRobot.RobotHand NG; RobotHand.Error");
        //                            //return;
        //                            Logger.InfoFormat("[CreateOnlyBProcessGetCommand] - Check GetRobot.RobotHand NG; RobotHand.Error ");
        //                            logInfo = string.Format("[    OnlyBGet Check]   NG Check GetRobot.RobotHand NG; RobotHand.Error");
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }
        //                        #endregion

        //                        #region 生成命令
        //                        int slotPostion = getGlass.SlotPosition;
        //                        //Logger.InfoFormat("[CreateOnlyBProcessGetCommand]getRobot{0};slotPostion{1} ", getRobot.ToString(), slotPostion);
        //                        var cmd = factory.CreateProcessGetCommand(unitItem, getRobot, getRobotModel.OutPriority, getGlass.ModelPosition, slotPostion);
        //                        cmd.GetGlass = getGlass;
        //                        var GetPosition1 = GetCurrentModel(getGlass.ModelPosition);
        //                        cmd.STGetPosition1string = GetPosition1.ModelName;
        //                        AddCommand(cmd);
        //                        Logger.InfoFormat("[CreateOnlyBProcessGetCommand][CreateProcessGetCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                             cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1);
        //                        logInfo = string.Format("[    OnlyBGet Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                             cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        #endregion

        //                    }
        //                    else
        //                    {
        //                        Logger.InfoFormat("[CreateOnlyBProcessGetCommand] - Check Data NG.ModelName={0},GlassInfo is null", getRobotModel.ModelName);
        //                    }
        //                }

        //            }
        //        }

        //        //Logger.InfoFormat("[CreateOnlyBProcessGetCommand] end ");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //}
        ///// <summary>
        ///// 从设备取panel
        ///// </summary>
        //private void CreateProcessGetCommand()
        //{
        //    try
        //    {
        //        var list = StageLinksignalList.Where(o=>o.LinkType== Consts.LinkType.UpstreamLinkSignal.GetHashCode()).ToList().FindAll(CheckLinkStatusSend);
        //        Logger.Info(string.Format("[PROCESS GET] Find Get Possible LinkSignal List Count :{0} ", list.Count));
        //        if (list.Count == 0) return;
        //        foreach (var link in list)
        //        {
        //            int ModelPosition = 0;
        //            Unit sourcesUnit = null;
        //            foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
        //            {
        //                var RobotModel = unitItem.RobotModelList.FirstOrDefault(o => o.UPLinkName == link.LinkName || o.DownLinkName == link.LinkName);
        //                if(RobotModel!=null)
        //                {
        //                    ModelPosition = RobotModel.ModelPosition;
        //                    sourcesUnit = unitItem;
        //                    break;
        //                }
        //            }
        //            var jobStage = StageList.Find(o => o.ModelPosition == ModelPosition);
        //            if (jobStage == null)
        //            {
        //                Logger.Info(string.Format("[PROCESS GET] Cannot Find Get Stage by Link Name :{0} ", link.UnitName));
        //                continue;
        //            }
        //           // var sourcesUnit = jobStage.Data as Unit;
        //            if (sourcesUnit == null)
        //            {
        //                Logger.Info(string.Format("[PROCESS GET] Stage {0} Not Unit ", jobStage.ModelPosition));
        //                continue;
        //            }
        //            var targetlist = FindNextStage(jobStage.ModelPosition, "");//Ruleid传空值过去
        //            RobotHand robotHandFind = RobotHand.Error;
        //            RobotPathConfigure cfgFind = null;
        //            foreach (var t in targetlist)
        //            {

        //                var cfg = t.PathConfigure;
        //                if (cfg == null)
        //                {
        //                    Logger.InfoFormat("[PROCESS GET] Cannot Find Path Configure by Unit {0}, Rule ID: {1} ", jobStage.ModelPosition, "");
        //                    continue;
        //                }
        //                var robotHand = ProcessGetHandCheck(Robot, cfg);// 根据手臂是否有玻璃，以及上游设备的上下层是否有玻璃，获取可用的手臂
        //                if (robotHand == RobotHand.Error)
        //                {
        //                    Logger.InfoFormat("[PROCESS GET] Out From {0}, Arm Already Have Glass Can not Found Hand ", jobStage.ModelPosition);
        //                    continue;
        //                }

        //                Unit unit = LineInfo.EQPInfo.Units.FirstOrDefault(o => o.UnitName == t.UnitName);
        //                if (unit == null)
        //                {
        //                    Logger.InfoFormat("[PROCESS GET] Not Find EQP by  UnitName {0} ", t.UnitName);
        //                    continue;
        //                }

        //                if (unit.UnitStatus == EquipmentStatus.DOWN)
        //                {
        //                    Logger.InfoFormat("[PROCESS GET] EQP {0} Status is DOWN ", unit.UnitStatus);
        //                    continue;
        //                }
        //                robotHandFind = robotHand;
        //                cfgFind = cfg;
        //                break;

        //            }

        //            var cmd = factory.CreateProcessGetCommand(sourcesUnit, new GlassInfo(), 1, robotHandFind, cfgFind.OutPriority, jobStage.ModelPosition);
        //            AddCommand(cmd);
        //            Logger.InfoFormat("[PROCESS GET] Process Get [{0}] for Glass {1}", jobStage.ModelPosition, "");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        #endregion
        #region Get Wait Command
        private void CreateGetWaitCommand(ref StringBuilder logStr)
        {
            try
            {
                string logInfo = string.Format("");
                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut || HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                //{
                //    Logger.Info("[CreateGetWaitCommand]   LineMode is ForceCleanOut; CreateGetWaitCommand NG ");
                //    logInfo = string.Format("[    CreateGetWaitCommand Check]   LineMode is ForceCleanOut; CreateGetWaitCommand NG");
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //    return;
                //}
                #region 校验手臂资料
                if (Robot.UpperExistOn || Robot.LowerExistOn)
                {
                    Logger.InfoFormat("[CreateGetWaitCommand] Check UpperExistOn or LowerExistOn NG; Robot.UpperExistOn or Robot.LowerExistOn; CreateGetWaitCommand NG ");
                    logInfo = string.Format("[     GetWait Check]   NG  Check UpperExistOn or LowerExistOn NG; CreateGetWaitCommand NG");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                // Logger.InfoFormat("[CreateGetWaitCommand] Check UpperExistOn and LowerExistOn OK; ");
                #endregion

                foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
                {
                    if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
                    {
                        // List<GlassInfo> glassInfoList = new List<GlassInfo>();                        
                        foreach (var getWaitRobotModel in unitItem.RobotModelList)
                        {
                            //Logger.InfoFormat("[CreateGetWaitCommand] UnitItem:{0};getWaitRobotModel:{1} ", unitItem.UnitName, getWaitRobotModel.ModelName);
                            #region 校验getWaitRobotModel
                            if (!getWaitRobotModel.GetWaitEnable)
                            {
                                Logger.InfoFormat("[CreateGetWaitCommand] - Check Condition NG.ModelName={0},GetWaitEnable is false", getWaitRobotModel.ModelName);
                                logInfo = string.Format("[     GetWait Check]   NG  ModelName={0},GetWaitEnable is false", getWaitRobotModel.ModelName);
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            // Logger.InfoFormat("[CreateGetWaitCommand] Check WaitEnable OK; item.GetWaitEnable==true; ModelName:{0}; ", getWaitRobotModel.ModelName);
                            //if (getWaitRobotModel.ModelPosition == HostInfo.Current.CurrentPosition)
                            //{
                            //    Logger.InfoFormat("[CreateGetWaitCommand] - Check Action NG.WaitModelPosition==CurrentPosition,CurrentPosition{0}", getWaitRobotModel.ModelName);
                            //    logInfo = string.Format("[     GetWait Check]   NG  WaitModelPosition==CurrentPosition,CurrentPosition{0}", getWaitRobotModel.ModelName);
                            //    logStr.Append(logInfo);
                            //    logStr.AppendLine();
                            //    continue;
                            //}
                            //Logger.InfoFormat("[CreateGetWaitCommand] Check CurrentPosition OK; getWaitRobotModel.ModelPosition!=HostInfo.Current.CurrentPosition; item.ModelPosition:{0}; ", getWaitRobotModel.ModelPosition);
                            #endregion
                            //item.ModelPosition
                            #region 校验UpLink
                            Linksignal UpLinksignal = StageLinksignalList.Where(o => o.UnitName == unitItem.UnitName && o.LinkName == getWaitRobotModel.UPLinkName).FirstOrDefault();
                            var checkResult = CheckGetWaitLinkStatusSend(UpLinksignal, unitItem.UnitName, ref logStr);
                            #endregion
                            //Logger.InfoFormat("[CreateGetWaitCommand] - Check Condition OK; GetWaitEnable=true; getWaitRobotModel.ModelPosition:{0}; ", getWaitRobotModel.ModelPosition);
                            if (checkResult)
                            {
                                //Logger.InfoFormat("[CreateGetWaitCommand] Check CheckGetWaitLinkStatus OK; LinkName:{0}; ", UpLinksignal.LinkName);
                                #region 获取手臂
                                RobotHand getWaitRbot = RobotHand.LowHand;
                                getWaitRbot = Robot.UpperExistOn ? RobotHand.LowHand : RobotHand.UpHand;//哪个手臂有空
                                //getWaitRobotModel.GetArm
                                RobotHand getRobot = RobotHand.Error;
                                getRobot = GetRobot(getWaitRobotModel, getRobot);
                                if (getRobot == RobotHand.Error)
                                {
                                    //Logger.Info("[CreateGetWaitCommand] Check GetRobot.RobotHand NG; RobotHand.Error");
                                    //return;
                                    Logger.InfoFormat("[CreateGetWaitCommand] - Check GetRobot.RobotHand NG; RobotHand.Error ");
                                    logInfo = string.Format("[     GetWait Check]   NG Check GetRobot.RobotHand NG; RobotHand.Error");
                                    logStr.Append(logInfo);
                                    logStr.AppendLine();
                                    continue;
                                }
                                if (getRobot != RobotHand.AllArm && getRobot != getWaitRbot)
                                {
                                    Logger.InfoFormat("[CreateGetWaitCommand] - Check RobotArm NG.Arm mismatch,Arm({1}),Check Arm({0}) ", getWaitRbot.GetHashCode(), getWaitRobotModel.GetArm);
                                    logInfo = string.Format("[     GetWait Check]   NG  Arm mismatch,Arm({1}),Check Arm({0})", getWaitRbot.GetHashCode(), getWaitRobotModel.GetArm);
                                    logStr.Append(logInfo);
                                    logStr.AppendLine();
                                    continue;
                                }
                                #endregion
                                //Logger.InfoFormat("[CreateGetWaitCommand] Check getRobot OK; getRobot:{0}; ", getRobot.ToString());
                                #region 生成资料
                                var ModelPosition = getWaitRobotModel.ModelPosition;
                                var cmd = factory.CreateProcessGetWaitCommand(unitItem, getWaitRbot, -2, ModelPosition, 99);
                                cmd.STGetPosition1string = getWaitRobotModel.ModelName;
                                AddCommand(cmd);
                                Logger.InfoFormat("[CreateGetWaitCommand][CreateProcessGetWaitCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Position({4}), SubCommand({5})",
                                         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), cmd.STGetSlotPostion1, cmd.STSubCommand1.ToString());
                                logInfo = string.Format("[     GetWait Check]   OK  {0}({1},{2}), Arm({3}), Position({4}), SubCommand({5})",
                                         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), cmd.STGetSlotPostion1, cmd.STSubCommand1.ToString());
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                #endregion
                            }
                            else
                            {
                                //Logger.InfoFormat("[CreateGetWaitCommand] - Check Action NG.ModelName:{1};LinkName={0}; CreateGetWaitCommand NG", UpLinksignal.LinkName, getWaitRobotModel.ModelName);
                                if (UpLinksignal != null && getWaitRobotModel != null)
                                {
                                    logInfo = string.Format("[     GetWait Check]   NG  Check Condition NG.LinkName:{0},ModelName={1}", UpLinksignal.LinkName, getWaitRobotModel.ModelName);
                                    logStr.Append(logInfo);
                                    logStr.AppendLine();
                                }
                                else
                                {
                                    logInfo = string.Format("[     GetWait Check]   NG  Check Condition NG,UpLinksignal or getWaitRobotModel==null ");
                                    logStr.Append(logInfo);
                                    logStr.AppendLine();
                                }

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

        #endregion
        #region Put Wait Command
        //private void CreatePutWaitCommand()
        //{
        //    try
        //    {
        //        var dictionary = new Dictionary<RobotHand, GlassInfo>();
        //        GlassInfo putJobData = null;
        //        if (Robot.LowerExistOn)
        //        {
        //            Logger.Info(string.Format("[FindPutCommand] Robot.LowerExistOn "));
        //            if (Robot.LowHandGlass1 != null)
        //            {
        //                putJobData = Robot.LowHandGlass1;
        //                Logger.Info(string.Format("[FindPutCommand] [Robot.LowHandGlass1!=null] GlassInfo glassInfo[{0},{1}] ", putJobData.CassetteSequenceNo, putJobData.SlotSequenceNo));
        //            }
        //            else if (Robot.LowHandGlass2 != null)
        //            {
        //                putJobData = Robot.LowHandGlass2;
        //                Logger.Info(string.Format("[FindPutCommand] [Robot.LowHandGlass2!=null] GlassInfo glassInfo[{0},{1}] ", putJobData.CassetteSequenceNo, putJobData.SlotSequenceNo));
        //            }

        //            if (putJobData == null)
        //            {
        //                Logger.Info(string.Format("[FindPutCommand],putJobData == null"));
        //                return;
        //            }
        //        }
        //        else if (Robot.UpperExistOn)
        //        {
        //            if (Robot.UpHandGlass1 != null)
        //            {
        //                putJobData = Robot.UpHandGlass1;
        //                Logger.Info(string.Format("[FindPutCommand] [Robot.UpHandGlass1!=null] GlassInfo glassInfo[{0},{1}] ", putJobData.CassetteSequenceNo, putJobData.SlotSequenceNo));
        //            }
        //            else if (Robot.UpHandGlass2 != null)
        //            {
        //                putJobData = Robot.UpHandGlass2;
        //                Logger.Info(string.Format("[FindPutCommand] [Robot.UpHandGlass2!=null] GlassInfo glassInfo[{0},{1}] ", putJobData.CassetteSequenceNo, putJobData.SlotSequenceNo));
        //            }

        //            if (putJobData == null)
        //            {
        //                Logger.Info(string.Format("[FindPutCommand],putJobData == null"));
        //                return;
        //            }
        //        }
        //        if (putJobData == null)
        //        {
        //            Logger.Info(string.Format("[FindPutCommand],putJobData == null  PutCommand NG"));
        //            return;
        //        }
        //        ProcessMode processMode = putJobData.ProcessMode;
        //        switch (processMode)
        //        {
        //            case ProcessMode.OnlyA:
        //                if (Robot.UpperExistOn)//上手臂是否存在panel
        //                {
        //                    if (Robot.UpHandGlass1 != null)
        //                    {
        //                        dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass1);
        //                        Logger.InfoFormat("[FindPutCommand] [Robot.UpHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
        //                    }
        //                }
        //                if (Robot.LowerExistOn)//下手臂是否存在panel
        //                {
        //                    if (Robot.LowHandGlass1 != null)
        //                    {
        //                        dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass1);
        //                        Logger.InfoFormat("[FindPutCommand] [Robot.LowHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
        //                    }
        //                }
        //                break;
        //            //case ProcessMode.OnlyB:
        //            //    //暂不处理
        //            //    OnlyBFindPutCommand();
        //            //    return;                                          
        //            default:
        //                if (Robot.UpperExistOn)//上手臂是否存在panel
        //                {
        //                    if (Robot.UpHandGlass1 != null)
        //                    {
        //                        dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass1);
        //                        //Logger.InfoFormat("[FindPutCommand] UpHandSubstrateID:{0},FetchDataTime:{1} ", Robot.UpHandGlass1.GlassID, Robot.UpHandGlass1.FetchDatetime.ToString());
        //                        Logger.InfoFormat("[FindPutCommand] [Robot.UpHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
        //                    }
        //                    else if (Robot.UpHandGlass2 != null)
        //                    {
        //                        dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass2);
        //                        Logger.InfoFormat("[FindPutCommand] [Robot.UpHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass2.CassetteSequenceNo, Robot.UpHandGlass2.SlotSequenceNo);
        //                    }
        //                }
        //                if (Robot.LowerExistOn)//下手臂是否存在panel
        //                {
        //                    if (Robot.LowHandGlass1 != null)
        //                    {
        //                        dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass1);
        //                        Logger.InfoFormat("[FindPutCommand] [Robot.LowHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
        //                    }
        //                    else if (Robot.LowHandGlass2 != null)
        //                    {
        //                        dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass2);
        //                        Logger.InfoFormat("[FindPutCommand] [Robot.LowHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass2.CassetteSequenceNo, Robot.LowHandGlass2.SlotSequenceNo);
        //                    }
        //                }
        //                break;
        //        }
        //        if (dictionary.Count == 0)//都没有就不放
        //        {
        //            Logger.Info("[FindPutCommand] dictionary.Count == 0; PutCommand NG ");
        //            return;
        //        }               
        //        var list = dictionary.OrderBy(o => o.Value.FetchDatetime);//取出时间;优先级:先取先放
        //        if (list == null)
        //        {
        //            Logger.InfoFormat("[FindPutCommand] list == null; PutCommand NG");
        //            return;
        //        }
        //        foreach (var kv in list)
        //        {
        //            var glassInfo = kv.Value;
        //            var robotHand = kv.Key;

        //            //var outEQP = GetOutEquipment(glassInfo);
        //            //if (outEQP == null) continue;
        //            var robotModel = GetCurrentModel(glassInfo);
        //            if (robotModel == null)
        //            {
        //                Logger.InfoFormat("[FindPutCommand] robotModel == null ");
        //                continue;
        //            }

        //            Logger.InfoFormat("[FindPutCommand] ModelPosition:{0}", robotModel.ModelPosition);

        //            var targetStageList = FindNextStage(robotHand, robotModel.ModelPosition, glassInfo);//根据上一站的unit或者eqp获取下一站的站点
        //            if (targetStageList.Count == 0)
        //            {
        //                Logger.InfoFormat("[FindPutCommand] targetStageList.Count==0  continue ");
        //                continue;
        //            }

        //            foreach (var targetStage in targetStageList)
        //            {
        //                if (!string.IsNullOrEmpty(targetStage.UnitName))
        //                {
        //                    if (targetStage.Type == EnumUnitType.Robot)
        //                    {
        //                        ////Logger.InfoFormat("[PUT] NextEquipment is Port,Port Check, Put Glass ID: {0}, CurrentModelPosition: {1},  RobotHand: {2}, RuleID: {3}, TargetModelPosition: {4} ",
        //                        ////glassInfo.GlassID, glassInfo.ModelPosition, robotHand, "", targetStage.ModelPosition);
        //                        //Logger.InfoFormat("[FindPutCommand] targetStage.Type == EnumUnitType.Robot ");
        //                        //CreatePortPutCommand(targetStage, robotHand, glassInfo, targetStage.PathConfigure);//放unload口
        //                    }
        //                    else
        //                    {
        //                        Logger.InfoFormat("[FindPutCommand] targetStage.Type == EnumUnitType.Stage ");
        //                        //Logger.InfoFormat("[PUT] Process Check, Put Glass ID: {0}, ModelPosition: {1},  RobotHand: {2}, RuleID: {3}, TargetModelPosition: {4} ",
        //                        //        glassInfo.GlassID, glassInfo.ModelPosition, robotHand, "", targetStage.ModelPosition);
        //                        CreateProcessPutCommand(robotHand, glassInfo, targetStage);//放设备


        //                    }
        //                }
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        #endregion

        #region Put Command
        /// <summary>
        /// 放panel
        /// </summary>
        private void FindPutCommand(bool IsPutWait, ref StringBuilder logStr)
        {
            //20180801 lsq_Modify
            try
            {

                // Logger.Info("[FindPutCommand] begin");
                Logger.InfoFormat("[FindPutCommand] - IsPutWaitCommand={0}", IsPutWait);
                //var dictionary = new Dictionary<RobotHand, GlassInfo>();
                #region 获取手臂及资料
                //RobotHand RobotHand = RobotHand.Error;
                //GlassInfo putGlass = null;
                Dictionary<RobotHand, List<GlassInfo>> putGlassDic = new Dictionary<RobotHand, List<GlassInfo>>();
                string logInfo = string.Format("");
                //LineMode LineMode = HostInfo.Current.EQPInfo.LineMode;//putJobData.ProcessMode;
                //Logger.InfoFormat("[FindPutCommand] LineMode:{0}", LineMode.ToString());
                //switch (LineMode)
                //{
                //case LineMode.OnlyA:
                //    if (Robot.UpperExistOn)//上手臂是否存在panel
                //    {
                //        if (Robot.UpHandGlass1 != null)
                //        {
                //            //dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass1);
                //            if (putGlassDic.Keys.Contains(RobotHand.UpHand))
                //            {
                //                putGlassDic[RobotHand.UpHand] = Robot.UpHandGlass1;
                //            }
                //            else
                //            {
                //                putGlassDic.Add(RobotHand.UpHand, Robot.UpHandGlass1);
                //            }
                //            //RobotHand = RobotHand.UpHand;
                //            //putGlass = Robot.UpHandGlass1;
                //            Logger.InfoFormat("[FindPutCommand][OnlyA] [Robot.UpHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
                //        }
                //        else
                //        {
                //            Logger.InfoFormat("[FindPutCommand] [OnlyA] [Robot.UpHandGlass1 == null]  ");
                //        }
                //    }
                //    else
                //    {
                //        //  Logger.InfoFormat("[FindPutCommand][OnlyA]  [Robot.UpperExistOn == false] ");
                //    }
                //    if (Robot.LowerExistOn)//下手臂是否存在panel
                //    {
                //        if (Robot.LowHandGlass1 != null)
                //        {
                //            //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass1);
                //            //RobotHand = RobotHand.LowHand;
                //            //putGlass = Robot.LowHandGlass1;
                //            if (putGlassDic.Keys.Contains(RobotHand.LowHand))
                //            {
                //                putGlassDic[RobotHand.LowHand] = Robot.LowHandGlass1;
                //            }
                //            else
                //            {
                //                putGlassDic.Add(RobotHand.LowHand, Robot.LowHandGlass1);
                //            }
                //            //Logger.InfoFormat("[FindPutCommand][OnlyA]  [Robot.LowHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
                //        }
                //        else
                //        {
                //            // Logger.InfoFormat("[FindPutCommand][OnlyA]  [Robot.LowHandGlass1 == null]  ");
                //        }
                //    }
                //    else
                //    {
                //        Logger.InfoFormat("[FindPutCommand][OnlyA]  [Robot.LowerExistOn == false] ");
                //    }
                //    break;
                //default:
                if (Robot.UpperExistOn)//上手臂是否存在panel
                {
                    List<GlassInfo> Uplist = new List<GlassInfo>();
                    if (Robot.UpHandGlass1 != null)
                    {
                        //dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass1);
                        //RobotHand = RobotHand.UpHand;
                        //putGlass = Robot.UpHandGlass1;
                        Uplist.Add(Robot.UpHandGlass1);
                        //Logger.InfoFormat("[FindPutCommand] UpHandSubstrateID:{0},FetchDataTime:{1} ", Robot.UpHandGlass1.GlassID, Robot.UpHandGlass1.FetchDatetime.ToString());
                        Logger.InfoFormat("[FindPutCommand] [Robot.UpHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
                    }
                    if (Robot.UpHandGlass2 != null)
                    {
                        //dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass2);
                        //RobotHand = RobotHand.UpHand;
                        //putGlass = Robot.UpHandGlass2;
                        Uplist.Add(Robot.UpHandGlass2);
                        Logger.InfoFormat("[FindPutCommand] [Robot.UpHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass2.CassetteSequenceNo, Robot.UpHandGlass2.SlotSequenceNo);
                    }
                    if (Uplist.Count > 0)
                    {
                        if (putGlassDic.Keys.Contains(RobotHand.UpHand))
                        {
                            putGlassDic[RobotHand.UpHand] = Uplist;
                        }
                        else
                        {
                            putGlassDic.Add(RobotHand.UpHand, Uplist);
                        }
                    }
                    else
                    {
                        Logger.InfoFormat("[FindPutCommand] [Robot.UpHandGlass1 and UpHandGlass2== null] ");
                    }
                }
                else
                {
                    Logger.InfoFormat("[FindPutCommand]  [Robot.UpperExistOn==false] ");
                }
                if (Robot.LowerExistOn)//下手臂是否存在panel
                {
                    List<GlassInfo> Lowlist = new List<GlassInfo>();
                    if (Robot.LowHandGlass1 != null)
                    {
                        //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass1);
                        //RobotHand = RobotHand.LowHand;
                        //putGlass = Robot.LowHandGlass1;
                        Lowlist.Add(Robot.LowHandGlass1);
                        Logger.InfoFormat("[FindPutCommand] [Robot.LowHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
                    }
                    if (Robot.LowHandGlass2 != null)
                    {
                        //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass2);
                        //RobotHand = RobotHand.LowHand;
                        //putGlass = Robot.LowHandGlass2;
                        Lowlist.Add(Robot.LowHandGlass2);
                        Logger.InfoFormat("[FindPutCommand] [Robot.LowHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass2.CassetteSequenceNo, Robot.LowHandGlass2.SlotSequenceNo);
                    }
                    if (Lowlist.Count > 0)
                    {
                        if (putGlassDic.Keys.Contains(RobotHand.LowHand))
                        {
                            putGlassDic[RobotHand.LowHand] = Lowlist;
                        }
                        else
                        {
                            putGlassDic.Add(RobotHand.LowHand, Lowlist);
                        }
                    }
                    else
                    {
                        Logger.InfoFormat("[FindPutCommand] [Robot.LowHandGlass1 and LowHandGlass2== null] ");
                    }
                }
                else
                {
                    Logger.InfoFormat("[FindPutCommand] [Robot.LowerExistOn==false] ");
                }
                //break;
                //}

                if (putGlassDic.Count == 0)//都没有就不放
                {
                    if (IsPutWait)
                    {
                        Logger.InfoFormat("[FindPutCommand][IsPutWaitCommand:{0}] - Check Data NG. Robot Arm Glass Data is null", IsPutWait);
                        logInfo = string.Format("[  	  PutWait Check]   NG  Robot Arm Glass Data is null");
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        return;
                    }
                    else
                    {
                        Logger.InfoFormat("[FindPutCommand][IsPutWaitCommand:{0}] - Check Data NG.Robot Arm Glass Data is null", IsPutWait);
                        logInfo = string.Format("[  PutCommand Check]   NG  Robot Arm Glass Data is null");
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        return;
                    }
                }
                #endregion

                //Logger.InfoFormat("[FindPutCommand] Check HandGlass OK; putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                foreach (var putGlassDICItem in putGlassDic)
                {
                    RobotHand RobotHand = putGlassDICItem.Key;
                    List<GlassInfo> putGlasss = putGlassDICItem.Value;
                    //var glassInfo = kv.Value;
                    //var robotHand = kv.Key;
                    string glassstr = "";
                    foreach (var putGlass in putGlasss)
                    {
                        glassstr += putGlass.CassetteSequenceNo + "," + putGlass.SlotSequenceNo + ";";
                    }
                    Logger.InfoFormat("[FindPutCommand] - Pre Check OK.Glass[{1}],RobotArm({0})", RobotHand.ToString(), glassstr);
                    //var outEQP = GetOutEquipment(glassInfo);
                    //if (outEQP == null) continue;


                    //if ( HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                    //{
                    //    Logger.Info("[FindPutCommand]   LineMode is ForceCleanOut; ");
                    //    logInfo = string.Format("[  PutCommand Check]   LineMode is ForceCleanOut;");
                    //    logStr.Append(logInfo);
                    //    logStr.AppendLine();


                    //    CreatePHT600ForceCleanOutPortPutCommand(RobotHand, ref logStr);
                    //    return;
                    //}
                    //var robotModel = GetCurrentModel(putGlass.ModelPosition);
                    //if (robotModel == null)
                    //{
                    //    Logger.InfoFormat("[FindPutCommand] Check robotModel NG;  robotModel == null ");
                    //    return;
                    //}
                    //Logger.InfoFormat("[FindPutCommand] Check robotModel OK; CurrentModel:{0} ", robotModel.ModelName);

                    #region 获取targetStageList
                    var targetStageList = FindNextStage(putGlasss.FirstOrDefault());//根据上一站的unit或者eqp获取下一站的站点
                    if (targetStageList.Count == 0)
                    {
                        Logger.InfoFormat("[FindPutCommand] - Check TargetStage NG.TargetStageList.Count==0");
                        return;
                    }
                    else
                    {
                        //Logger.InfoFormat("[FindPutCommand] - Check targetStageList OK;  targetStageList.Count:{0} ", targetStageList.Count);
                    }
                    #endregion

                    foreach (var targetStage in targetStageList)
                    {
                        if (!string.IsNullOrEmpty(targetStage.UnitName))
                        {
                            //RobotHand,
                            #region 获取targetModel
                            var targetModel = GetCurrentModel(targetStage.ModelPosition);
                            #endregion

                            if (targetStage.Type == EnumUnitType.Robot)
                            {
                                if (!IsPutWait)
                                {
                                    CreatePortPutCommand(targetStage, RobotHand, putGlasss, ref logStr);//放unload口
                                }
                            }
                            else
                            {
                                CreateProcessPutCommand(RobotHand, putGlasss, targetModel, IsPutWait, ref logStr);//放设备
                            }
                        }
                    }
                    break;
                }

                // Logger.Info("[FindPutCommand] end");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }


        //private void FindOnlyBPutCommand(ref StringBuilder logStr)
        //{
        //    try
        //    {
        //        string logInfo = string.Format("");
        //        //  Logger.InfoFormat("[FindOnlyBPutCommand] begin ");
        //        //获取B片数据   如果没有B片数据  直接返回
        //        GlassInfo putGlassA = null;
        //        GlassInfo putGlassB = null;
        //        RobotHand GlassARobotHand = RobotHand.Error;
        //        RobotHand GlassBRobotHand = RobotHand.Error;
        //        #region 获取手臂上GlassA、GlassB的资料
        //        if (Robot.UpperExistOn)//上手臂是否存在panel
        //        {
        //            // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.UpperExistOn ==true");
        //            if (Robot.UpHandGlass1 != null)
        //            {
        //                // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.UpHandGlass1 != null ");
        //                if (Robot.UpHandGlass1.SlotPosition == 2)
        //                {
        //                    putGlassB = Robot.UpHandGlass1;
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand][Robot.UpHandGlass1.SlotPosition == 2] GlassBInfo; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                    GlassBRobotHand = RobotHand.UpHand;
        //                }
        //                else if (Robot.UpHandGlass1.SlotPosition == 1)
        //                {
        //                    putGlassA = Robot.UpHandGlass1;
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand][Robot.UpHandGlass1.SlotPosition == 1] GlassAInfo; putGlassA[{0},{1}] ", putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo);
        //                    GlassARobotHand = RobotHand.UpHand;
        //                }
        //            }
        //            else if (Robot.UpHandGlass2 != null)
        //            {
        //                //Logger.InfoFormat("[FindOnlyBPutCommand] Robot.UpHandGlass2 != null ");
        //                if (Robot.UpHandGlass2.SlotPosition == 2)
        //                {
        //                    putGlassB = Robot.UpHandGlass2;
        //                    //  Logger.InfoFormat("[FindOnlyBPutCommand][Robot.UpHandGlass2.SlotPosition == 2] GlassBInfo; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                    GlassBRobotHand = RobotHand.UpHand;
        //                }
        //                else if (Robot.UpHandGlass2.SlotPosition == 1)
        //                {
        //                    putGlassA = Robot.UpHandGlass2;
        //                    //Logger.InfoFormat("[FindOnlyBPutCommand][Robot.UpHandGlass2.SlotPosition == 1] GlassAInfo; putGlassA[{0},{1}] ", putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo);
        //                    GlassARobotHand = RobotHand.UpHand;
        //                }
        //            }
        //            else
        //            {
        //                //Logger.InfoFormat("[FindOnlyBPutCommand] Robot.UpHandGlass1 == null and Robot.UpHandGlass2 == null ");
        //            }
        //        }
        //        else
        //        {
        //            // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.UpperExistOn==false ");
        //        }
        //        if (Robot.LowerExistOn)//下手臂是否存在panel
        //        {
        //            //  Logger.InfoFormat("[FindOnlyBPutCommand] Robot.LowerExistOn ");
        //            if (Robot.LowHandGlass1 != null)
        //            {
        //                // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.LowHandGlass1 != null ");
        //                if (Robot.LowHandGlass1.SlotPosition == 2)
        //                {
        //                    putGlassB = Robot.LowHandGlass1;
        //                    //   Logger.InfoFormat("[FindOnlyBPutCommand][Robot.LowHandGlass1.SlotPosition == 2] GlassBInfo; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                    GlassBRobotHand = RobotHand.LowHand;
        //                }
        //                else if (Robot.LowHandGlass1.SlotPosition == 1)
        //                {
        //                    putGlassA = Robot.LowHandGlass1;
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand][Robot.LowHandGlass1.SlotPosition == 1] GlassAInfo; putGlassA[{0},{1}] ", putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo);
        //                    GlassARobotHand = RobotHand.LowHand;
        //                }
        //            }
        //            else if (Robot.LowHandGlass2 != null)
        //            {
        //                // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.LowHandGlass2 != null ");
        //                if (Robot.LowHandGlass2.SlotPosition == 2)
        //                {
        //                    putGlassB = Robot.LowHandGlass2;
        //                    //Logger.InfoFormat("[FindOnlyBPutCommand][Robot.LowHandGlass2.SlotPosition == 2] GlassBInfo; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                    GlassBRobotHand = RobotHand.LowHand;
        //                }
        //                else if (Robot.LowHandGlass2.SlotPosition == 1)
        //                {
        //                    putGlassA = Robot.LowHandGlass2;
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand][Robot.LowHandGlass2.SlotPosition == 1] GlassAInfo; putGlassA[{0},{1}] ", putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo);
        //                    GlassARobotHand = RobotHand.LowHand;
        //                }
        //            }
        //            else
        //            {
        //                // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.LowHandGlass1 == null and  Robot.LowHandGlass2 == null");
        //            }
        //        }
        //        else
        //        {
        //            // Logger.InfoFormat("[FindOnlyBPutCommand] Robot.LowerExistOn==false ");
        //        }
        //        #endregion


        //        if (putGlassB == null)
        //        {
        //            Logger.Info("[FindOnlyBPutCommand] Check GlassB NG;GlassB == null; OnlyBPutCommand NG ");
        //            logInfo = string.Format("[    OnlyBPut Check]  GlassB == null; OnlyBPutCommand NG");
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }

        //        //根据B片数据获取下个position
        //        #region 根据B片数据获取下个position
        //        var targetStageList = FindNextStage(putGlassB);//根据上一站的unit或者eqp获取下一站的站点 //发送B片命令将glass 放到eqp 或者port
        //        if (targetStageList.Count == 0)
        //        {
        //            Logger.InfoFormat("[FindOnlyBPutCommand] Check TargetStageList NG;  targetStageList.Count==0; putGlassB[{0},{1}]; OnlyBPutCommand NG", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //            logInfo = string.Format("[    OnlyBPut Check]   NG  Check TargetStageList.Count==0;putGlassB[{0},{1}]; OnlyBPutCommand NG", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        #endregion

        //        // Logger.InfoFormat("[FindOnlyBPutCommand] Check TargetStageList OK;  targetStageList.Count{0} ", targetStageList.Count);
        //        //Logger.Info("+++FindExchangeCommand Step 4, Start Check NextEquipment Status");
        //        foreach (var targetStage in targetStageList)
        //        {
        //            if (!string.IsNullOrEmpty(targetStage.UnitName))
        //            {
        //                RobotModel targetModel = GetCurrentModel(targetStage.ModelPosition);
        //                Logger.InfoFormat("[FindOnlyBPutCommand] Check targetStage OK; GlassB[{0},{1}];targetModel:{2} ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo, targetModel.ModelName);
        //                if (targetStage.Type == EnumUnitType.Robot)
        //                {
        //                    #region nextStage is robot
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand]  [targetStage.Type == EnumUnitType.Robot] NextEquipment is Port,  RobotHand: {0},  TargetModelPosition: {1} ", GlassBRobotHand, targetStage.ModelPosition);
        //                    #region 校验Put Stage
        //                    if (putGlassB.SlotSatus != EnumGlassSlotStatus.Processing)
        //                    {
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] Check  GlassB.SlotSatus!= EnumGlassSlotStatus.Processing; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logInfo = string.Format("[    OnlyBPut Check]   NG  Check GlassB.SlotSatus!= EnumGlassSlotStatus.Processing; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand]  putGlassB.SlotSatus== EnumGlassSlotStatus.Processing; putGlassB[{0},{1}] ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                    RobotCommand cmd;
        //                    var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlassB.PortID);
        //                    if (unloadport == null)
        //                    {
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] Check UnloadPort NG;PortID:{0}, putGlassB[{1},{2}] ", putGlassB.PortID, putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logInfo = string.Format("[    OnlyBPut Check]   NG  Check UnloadPort NG:PortID:{0}, putGlassB[{1},{2}] ", putGlassB.PortID, putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logStr.AppendLine();
        //                        eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", putGlassB.PortID, putGlassB.CassetteID));
        //                        continue;
        //                    }
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] Check UnloadPort OK; unloadport:{0} ", unloadport.PortID);
        //                    var targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
        //                    if (targetUnit == null)
        //                    {
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] Check targetUnit NG; targetUnit == null;putGlassB[{0},{1}]; OnlyBPutCommand NG ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logInfo = string.Format("[    OnlyBPut Check]   NG  [targetStage.Type==Robot] Check targetUnit==null;putGlassB[{0},{1}]; OnlyBPutCommand NG ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] Check targetUnit OK; targetUnit:{0} ", targetUnit.UnitName);
        //                    var putRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == putGlassB.PortID);
        //                    if (putRobotModel == null)
        //                    {
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] Check roboputRobotModeltmodel NG;putRobotModel == null;GlasputGlassBsB[{0},{1}]; OnlyBPutCommand NG ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logInfo = string.Format("[    OnlyBPut Check]   NG  [targetStage.Type==Robot] Check putRobotModel==null;putGlassB[{0},{1}]; OnlyBPutCommand NG ", putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    logInfo = string.Format("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] Check putRobotModel OK;putRobotModel:{0} ", putRobotModel.ModelName);
        //                    if (putRobotModel.ModelPosition != targetStage.ModelPosition)
        //                    {
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [putRobotModel.ModelPosition!= targetStage.ModelPosition] Check targetStage.ModelPosition NG; targetStage.ModelPosition:{0};GlassB[{1},{2}]; OnlyBPutCommand NG", targetStage.ModelPosition, putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);

        //                        logInfo = string.Format("[    OnlyBPut Check]   NG  Check targetStage.ModelPosition:{0} NG:robotmodel.ModelPosition!= targetStage.ModelPosition;GlassB[{1},{2}]; OnlyBPutCommand NG", targetStage.ModelPosition, putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    // Logger.InfoFormat("[FindOnlyBPutCommand] [putRobotModel.ModelPosition== targetStage.ModelPosition] Check targetStage.ModelPosition OK;targetStage.ModelPosition:{0}", targetStage.ModelPosition);
        //                    #endregion
        //                    Logger.InfoFormat("[FindOnlyBPutCommand]  [targetStage.Type == EnumUnitType.Robot] targetStage Check OK, putGlassB.SlotSatus != Processing; ");
        //                    //如果有A片数据 将A片B片组合成一个命令发送给plc, A片在1   B片在2;
        //                    if (putGlassA != null)
        //                    {
        //                        #region 生成放glass A、B 的命令
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] putGlassA != null; putGlassA[{0},{1}] ", putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo);
        //                        //int SlotPostion = GlassB.SlotPosition;
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] putGlassA != null; GlassARobotHand:{0};GlassBRobotHand:{1} ", GlassARobotHand.ToString(), GlassBRobotHand.ToString());
        //                        cmd = factory.CreateOnlyBPortPutCommand(unloadport, putRobotModel.ModelPosition, putGlassA.Position, GlassARobotHand, putGlassA.SlotPosition,
        //                         putGlassB.Position, GlassBRobotHand, putGlassB.SlotPosition, putRobotModel.INPriority);
        //                        cmd.PutGlassA = putGlassA;
        //                        cmd.PutGlassB = putGlassB;
        //                        cmd.STPutPosition1string = putRobotModel.ModelName;
        //                        cmd.NDPutPosition2string = putRobotModel.ModelName;
        //                        AddCommand(cmd);
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [CreateOnlyBPortPutCommand]{0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6} + {7}({8},{9}), Arm({10}), GlassB({11},{12}), Position({13} ",
        //                        cmd.STRCMD1.ToString(), cmd.STGetPosition1string, putGlassA.Position, cmd.STArmNo1.ToString(), putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo, putGlassA.SlotPosition, cmd.NDRCMD2.ToString(), cmd.NDGetPosition2string, putGlassB.Position, cmd.NDArmNo2.ToString(), putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo, putGlassB.SlotPosition);
        //                        logInfo = string.Format("[    OnlyBPut Check]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}), Position({6} + {7}({8},{9}), Arm({10}), GlassB({11},{12}), Position({13} ",
        //                        cmd.STRCMD1.ToString(), cmd.STGetPosition1string, putGlassA.Position, cmd.STArmNo1.ToString(), putGlassA.CassetteSequenceNo, putGlassA.SlotSequenceNo, putGlassA.SlotPosition, cmd.NDRCMD2.ToString(), cmd.NDGetPosition2string, putGlassB.Position, cmd.NDArmNo2.ToString(), putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo, putGlassB.SlotPosition);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        #endregion

        //                    }
        //                    //如果没有A片数据 生成B片命令发送给plc,B片在1
        //                    else
        //                    {
        //                        #region 生成放glassB的命令
        //                        //Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] GlassA == null;   ");
        //                        int SlotPostion = putGlassB.SlotPosition;
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [targetStage.Type == EnumUnitType.Robot] GlassA == null; GlassBRobotHand:{0}; ", GlassBRobotHand.ToString());
        //                        cmd = factory.CreatePortPutCommand(unloadport, putRobotModel.ModelPosition, putGlassB, GlassBRobotHand, putRobotModel.INPriority, SlotPostion);
        //                        cmd.PutGlassB = putGlassB;
        //                        cmd.STPutPosition1string = putRobotModel.ModelName;
        //                        AddCommand(cmd);
        //                        Logger.InfoFormat("[FindOnlyBPutCommand] [CreatePortPutCommand]{0}({1},{2}), Arm({3}), GlassB({4},{5}), Position({6};",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logInfo = string.Format("[    OnlyBPut Check]   OK  {0}({1},{2}), Arm({3}), GlassB({4},{5}), Position({6};",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlassB.CassetteSequenceNo, putGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        #endregion
        //                    }
        //                    //if (targetModel.PutArm != 0)
        //                    //{
        //                    //    Logger.InfoFormat("[FindOnlyBPutCommand] Check targetModel.PutArm!=0; targetModel:{0};PutArm:{1} ", targetModel.ModelName, targetModel.PutArm);
        //                    //    logInfo = string.Format("[    OnlyBPut Check]   NG  Check targetModel.PutArm!=0; targetModel:{0};PutArm:{1} ", targetModel.ModelName, targetModel.PutArm);
        //                    //    logStr.AppendLine();

        //                    //}
        //                    //else
        //                    //{

        //                    //}

        //                    #endregion

        //                    // CreatePortPutCommand(targetStage, robotHand, GlassB, targetStage.PathConfigure);//放unload口                          

        //                }
        //                else
        //                {

        //                    //Logger.InfoFormat("[PUT] Process Check, Put Glass ID: {0}, ModelPosition: {1},  RobotHand: {2}, RuleID: {3}, TargetModelPosition: {4} ",
        //                    //        GlassB.GlassID, GlassB.ModelPosition, GlassBRobotHand, "", targetStage.ModelPosition);
        //                    Logger.InfoFormat("[FindOnlyBPutCommand]  [targetStage.Type == EnumUnitType.Stage] NextEquipment is Stage,  RobotHand: {0},  TargetModelPosition: {1} ", GlassBRobotHand, targetStage.ModelPosition);
        //                    CreateProcessPutCommand(GlassBRobotHand, putGlassB, targetModel, false, ref logStr);//放设备
        //                }
        //            }
        //        }


        //        Logger.InfoFormat("[FindOnlyBPutCommand] end ");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }

        //}
        /// <summary>
        /// 向port放panel
        /// </summary>
        /// <param name="targetStage"></param>
        /// <param name="handNo"></param>
        /// <param name="handJob"></param>
        /// <param name="cfg"></param>
        private void CreatePortPutCommand(JobStage targetStage, RobotHand RobotHand, List<GlassInfo> putGlasss, ref StringBuilder logStr)
        {
            try
            {
                GlassInfo putGlass = putGlasss.FirstOrDefault();//随便取一片 用于下面逻辑判断

                string logInfo = string.Format("");

                //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] ");
                //#region 校验put Stage

                //if (HostInfo.Current.EQPInfo.LineMode != LineMode.ForceCleanOut)
                //{
                //    if (putGlass.SlotSatus != EnumGlassSlotStatus.Processing)
                //    {
                //        Logger.InfoFormat("[FindPutCommand] - Check Action NG.Glass[{0},{1}] SlotSatus!=Processing,current:{2}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putGlass.SlotSatus);
                //        logInfo = string.Format("[  PutCommand Check]   NG  Glass[{0},{1}] SlotSatus!=Processing", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //        logStr.Append(logInfo);
                //        logStr.AppendLine();
                //        return;
                //    }
                //}
                // Logger.InfoFormat("[FindPutCommand] putGlass.SlotSatus== EnumGlassSlotStatus.Processing; putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                RobotCommand cmd;
                //TBD 放到哪个Port，根据PortGrade查找满足的Port

                /*这里的putGlass.PortID 只是上卡时的port 先屏蔽此代码
                var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlass.PortID);
                if (unloadport == null)
                {
                    Logger.InfoFormat("[FindPutCommand] - Check Port NG.Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", putGlass.PortID, putGlass.CassetteID, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logInfo = string.Format("[  PutCommand Check]   NG  Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", putGlass.PortID, putGlass.CassetteID, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", putGlass.PortID, putGlass.CassetteID));
                    return;
                }
                */

                //Logger.InfoFormat("[FindPutCommand] Check unloadport OK; [CreatePortPutCommand] unloadport:{0}", unloadport.PortID);
                var targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
                if (targetUnit == null)
                {
                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check TargetUnit NG.Glass[{0},{1}],TargetUnit is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logInfo = string.Format("[  PutCommand Check]   NG  Glass[{0},{1}],TargetUnit is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
                var putRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.ModelPosition == targetStage.ModelPosition);
                if (putRobotModel == null)
                {
                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check Action NG.Glass[{0},{1}],TargetRobotModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logInfo = string.Format("[  PutCommand Check]   NG  Glass[{0},{1}],TargetRobotModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }

                var unloadport = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == HostInfo.Current.EQPInfo.EQPID && c.UnitName == targetStage.UnitName && c.PortID == putRobotModel.PortID && c.PortStatus == 3 && (c.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing || c.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing));
                if (unloadport == null)
                {
                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check unloadport NG.{0} PortStatus not inuse", putRobotModel.PortID);
                    return;
                }

                #region 检查PortGrade
                //HostInfo.Current.PortGradeGroupList
                //var portGradeGroup
                var GlassGradeCode = putGlass.GlassGradeCode;
                var portGradeGroups = HostInfo.Current.PortGradeGroupList.FirstOrDefault(o => o.Key == targetUnit.EQPID).Value;
                if (portGradeGroups != null && portGradeGroups.Count() > 0)
                {
                    var jobPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassGradeCode) && o.enabled == 0);
                    if (jobPortGradeGroups != null && jobPortGradeGroups.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(unloadport.PortGrade))//如果当前PortGradeGroup不为空，则检查是否满足GlassGradeCode
                        {
                            var jobPortGradeGroup = jobPortGradeGroups.FirstOrDefault(o => o.portgradegroup == unloadport.PortGrade);
                            if (jobPortGradeGroup != null)//当前Port满足GlassGradeCode 可以放片
                            {

                            }
                            else//当前Port不满足GlassGradeCode 不放片
                            {
                                Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .Not Mismatch.GlassGradeCode is {4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                return;
                            }
                        }
                        else//如果当前PortGradeGroup为空，则需要查询出是否有其他Port满足，如果没有，则使用当前Port，并赋值PortGradeGroup
                        {
                            bool checkResult = false;
                            PortInfo port = null;
                            foreach (var jobPGG in jobPortGradeGroups)
                            {
                                Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Find PortGradeGroup{0}", jobPGG.portgradegroup);
                                port = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == HostInfo.Current.EQPInfo.EQPID && c.PortGrade == jobPGG.portgradegroup && c.PortStatus == 3 && (c.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing || c.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing));
                                if (port != null)
                                {
                                    checkResult = true;
                                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Find Port {0}", port.PortID);
                                    break;
                                }
                            }
                            if (checkResult)//查找到其他满足条件的Port，则当前Port不处理
                            {
                                Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}], Other Port Match. TargetUnit is {2},PortGradeGroup is {3} .Not Mismatch.GlassGradeCode is {4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                return;
                            }
                            else//未查找到满足条件的Port，则使用当前的Port，因为当前的PortGradeGroup为空，代表未放置Panel
                            {
                                // 赋值优先级最高的可用的PortGradeGroup
                                var priority = jobPortGradeGroups.Where(o => o.enabled == 0).Max(o => o.priority);
                                unloadport.PortGrade = jobPortGradeGroups.FirstOrDefault(o => o.priority == priority).portgradegroup;
                                IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
                                dbService.UpdatePortInfo(unloadport);
                                Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Update Port {0} PortGrade={1}", unloadport.PortID, unloadport.PortGrade);
                            }
                        }
                    }
                    else
                    {
                        //没找到glass对应的group配置 默停
                        Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .GlassGradeCode is {4} Can not find BC PortGrade Config", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                        return;
                    }
                }
                else
                {
                    //没找到glass对应的group配置 默停
                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .GlassGradeCode is {4} Can not find BC PortGrade Config", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                    return;
                }
                #endregion

                // logInfo = string.Format("[FindPutCommand] [CreatePortPutCommand] Check putRobotModel OK; robputRobotModelotmodel:{0} ", putRobotModel.ModelName);
                //if (putRobotModel.ModelPosition != targetStage.ModelPosition)
                //{
                //    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check TargetStage NG.ModelPosition:{0},Glass[{2},{3}],TargetModelPostion={1}", targetStage.ModelPosition, putRobotModel.ModelPosition, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //    logInfo = string.Format("[  PutCommand Check]   NG  ModelPosition:{0},Glass[{2},{3}],TargetModelPostion={1}", targetStage.ModelPosition, putRobotModel.ModelPosition, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //    return;
                //}
                //#endregion

                #region 校验手臂
                if (!CheckModelPutArm(putRobotModel, RobotHand))
                {
                    Logger.InfoFormat("[FindPutCommand] - Arm mismatch.RobotArm({0}),TargetModel Arm({1})", RobotHand.GetHashCode(), putRobotModel.PutArm);
                    logInfo = string.Format("[  PutCommand Check]   NG  Arm mismatch.RobotArm({0}),TargetModel Arm({1})", RobotHand.GetHashCode(), putRobotModel.PutArm);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion
                //Logger.InfoFormat("[FindPutCommand]Check RobotHand OK ;Put Stage OK; RobotHand:{0}; putRobotModel:{1}", RobotHand.ToString(), putRobotModel.ModelName);
                #region 获取 SlotPostion
                int Position = 0;//放哪一层
                int SlotPostion = 0;//放前还是放后
                //找哪一层哪个位置能放
                //if (putRobotModel.PortGetType == PortGetType.DESC)
                //{
                if (unloadport.GlassInfos.Count > 0)
                {
                    var descdata = unloadport.GlassInfos.OrderBy(o => o.Position).ToList();//最下面的层
                    var lowslot = descdata.FirstOrDefault();
                    if (descdata.Count(c => c.Position == lowslot.Position) > 1)//这一层满了 放下一层
                    {
                        Position = lowslot.Position - 1;
                        SlotPostion = (putGlasss.Count == 1) ? 2 : 99;//手臂只有一片，优先放后面
                    }
                    else//这一层没满
                    {
                        if (putGlasss.Count == 1)
                        {
                            Position = lowslot.Position - 1;//这里暂时不补片了
                            SlotPostion = 2;//手臂只有一片，放后面  因为不补片了 放下一层 默认放后面
                        }
                        else//手臂有两片 放不下 放下一层
                        {
                            Position = lowslot.Position - 1;
                            SlotPostion = 99;
                        }
                    }
                }
                else
                {
                    Position = unloadport.Capacity / 2;
                    SlotPostion = (putGlasss.Count == 1) ? 2 : 99;
                }

                if (Position <= 0)
                {
                    var lastglass = unloadport.GlassInfos.OrderBy(o => o.Position).FirstOrDefault();
                    //Port没退 导致计算出负数
                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check Input Position NG.Glass[{0},{1}],TargetUnit is {2} lastglassid{3} position{4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, lastglass.GlassID, lastglass.Position);
                    return;
                }
                //}
                //else
                //{
                //    var ascdata = unloadport.GlassInfos.OrderByDescending(o => o.Position).ToList();//最上面的层
                //    var upslot = ascdata.FirstOrDefault();
                //    if (ascdata.Count(c => c.Position == upslot.Position) > 1)//这一层满了 放上一层
                //    {
                //        Position = upslot.Position + 1;
                //        SlotPostion = (putGlasss.Count == 1) ? 2 : 99;//手臂只有一片，优先放后面
                //    }
                //    else//这一层没满
                //    {
                //        if (putGlasss.Count == 1)
                //        {
                //            Position = upslot.Position;
                //            SlotPostion = 1;//手臂只有一片，放前面
                //        }
                //        else//手臂有两片 放不下 放下一层
                //        {
                //            Position = upslot.Position + 1;
                //            SlotPostion = 99;
                //        }
                //    }
                //}

                //if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                //{
                //    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                //    {
                //        SlotPostion = putGlass.SlotPosition;
                //        // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode == LineMode.OnlyA; SlotPostion:{0}", SlotPostion);
                //    }
                //    else
                //    {
                //        //var glassCount = unloadport.GlassInfos.Where(o => o.Position == handJob.Position).Count();
                //        var glassCount = unloadport.GlassInfos.Where(o => o.Position == putGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                //        SlotPostion = glassCount == 1 ? putGlass.SlotPosition : 99;
                //        // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; SlotPostion:{0}", SlotPostion);
                //    }
                //}
                //else
                //{
                //    //TBD 有问题
                //    SlotPostion = 99;
                //}

                #endregion

                #region 生成命令
                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
                cmd = factory.CreatePortPutCommand(unloadport, putRobotModel.ModelPosition, RobotHand, putRobotModel.INPriority, Position, SlotPostion);
                //cmd.GlassInfo = handJob;

                //GlassInfo GlassA = null;
                //GlassInfo GlassB = null;
                //GlassInfo Glass = null;
                //GetGlassByGlass(putGlass, ref GlassA, ref GlassB, ref Glass, "CreatePortPutCommand");
                //cmd.PutGlass = Glass;
                //cmd.PutGlassA = GlassA;
                //cmd.PutGlassB = GlassB;
                cmd.STPutPosition1string = putRobotModel.ModelName;
                AddCommand(cmd);
                string glassinfostr = "";
                foreach (var putgls in putGlasss)
                {
                    glassinfostr += string.Format("Glass:{0},{1};", putgls.CassetteSequenceNo, putgls.SlotSequenceNo);
                    if (cmd.PutGlassA == null)
                        cmd.PutGlassA = putgls;
                    else
                        cmd.PutGlassB = putgls;
                }

                Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4}), Position({5}; PortID({6}); PortHasGlass({7})",
                                cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), glassinfostr, cmd.STPutSlotPostion1, unloadport.PortID, unloadport.GlassInfos.Count);
                logInfo = string.Format("[  PutCommand Check]   OK  {0}({1},{2}), Arm({3}), Glass({4}), Position({5};PortID({6}); PortHasGlass({7})",
                                cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), glassinfostr, cmd.STPutSlotPostion1, unloadport.PortID, unloadport.GlassInfos.Count);
                logStr.Append(logInfo);
                logStr.AppendLine();
                #endregion
                //}
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }


        /// <summary>
        /// 向port放panel
        /// </summary>
        /// <param name="targetStage"></param>
        /// <param name="handNo"></param>
        /// <param name="handJob"></param>
        /// <param name="cfg"></param>
        //private void CreatePHT600ForceCleanOutPortPutCommand(RobotHand RobotHand, ref StringBuilder logStr)
        //{
        //    try
        //    {
        //        //20180801 lsq_Modify
        //        string logInfo = string.Format("");
        //        #region 校验put port 状态
        //        var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == HostInfo.Current.EQPInfo.PHT600Port);
        //        if (unloadport.CassetteInfo.CassetteStatus != EnumCarrierStatus.WaitingforProcessing || unloadport.CassetteInfo.CassetteStatus != EnumCarrierStatus.InProcessing)
        //        {
        //            Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand] - Check Port NG. [PHT600ForceCleanOut],Port=[{0}],Not Exist Put Port", unloadport.PortID);
        //            logInfo = string.Format("[  PHT600 Put Check]   NG  [PHT600ForceCleanOut],Port=[{0}],Not Exist Put Port", unloadport.PortID);
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};PHT600ForceCleanOut", unloadport.PortID));
        //            return;
        //        }
        //        #endregion

        //        #region 校验put Stage


        //        RobotCommand cmd;

        //        var targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == unloadport.UnitName);
        //        if (targetUnit == null)
        //        {
        //            Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check TargetUnit NG.[PHT600ForceCleanOut] ,TargetUnit is null");
        //            logInfo = string.Format("[  PutCommand Check]   NG   ,[PHT600ForceCleanOut] TargetUnit is null");
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        var putRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == unloadport.PortID);
        //        if (putRobotModel == null)
        //        {
        //            Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check Action NG[PHT600ForceCleanOut],TargetRobotModel is null");
        //            logInfo = string.Format("[  PutCommand Check]   NG  [PHT600ForceCleanOut],TargetRobotModel is null");
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        // logInfo = string.Format("[FindPutCommand] [CreatePortPutCommand] Check putRobotModel OK; robputRobotModelotmodel:{0} ", putRobotModel.ModelName);

        //        #endregion

        //        #region 校验手臂
        //        if (!CheckModelPutArm(putRobotModel, RobotHand))
        //        {
        //            Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand] -[PHT600ForceCleanOut] Arm mismatch.RobotArm({0}),TargetModel Arm({1})", RobotHand.GetHashCode(), putRobotModel.PutArm);
        //            logInfo = string.Format("[  PHT600 Put Check]   NG  [PHT600ForceCleanOut]Arm mismatch.RobotArm({0}),TargetModel Arm({1})", RobotHand.GetHashCode(), putRobotModel.PutArm);
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        #endregion

        //        #region 获取 SlotPostion
        //        int SlotPostion = 0;


        //        #endregion
        //        GlassInfo putGlass = new GlassInfo();
        //        putGlass.Position = HostInfo.Current.EQPInfo.PHT600PortSlot;
        //        putGlass.Position++;
        //        #region 生成命令

        //        cmd = factory.CreatePortPutCommand(unloadport, putRobotModel.ModelPosition, putGlass, RobotHand, putRobotModel.INPriority, SlotPostion);
        //        //cmd.GlassInfo = handJob;

        //        var glassList = unloadport.GlassInfos.ToList();
        //        GlassInfo GlassA = null;
        //        GlassInfo GlassB = null;
        //        GlassInfo Glass = null;
        //        GetGlassByGlass(glassList, putGlass, ref GlassA, ref GlassB, ref Glass, "CreatePortPutCommand[PHT600ForceCleanOut]");
        //        cmd.PutGlass = Glass;
        //        cmd.PutGlassA = GlassA;
        //        cmd.PutGlassB = GlassB;



        //        cmd.STPutPosition1string = putRobotModel.ModelName;
        //        AddCommand(cmd);
        //        Logger.InfoFormat("[PHT600ForceCleanOutFindPutCommand][CreatePortPutCommand][PHT600ForceCleanOut]- Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6};",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //        logInfo = string.Format("[  PHT600 Put Check]   OK  [PHT600ForceCleanOut]{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6};",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        #endregion
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //}
        ///// <summary>
        ///// 向port放panel
        ///// </summary>
        ///// <param name="targetStage"></param>
        ///// <param name="handNo"></param>
        ///// <param name="handJob"></param>
        ///// <param name="cfg"></param>
        //private void CreatePortPutCommand(JobStage targetStage, RobotHand handNo, GlassInfo handJob, RobotPathConfigure cfg)
        //{
        //    try
        //    {
        //        //20180801 lsq_Modify
        //        Logger.Info("[CreatePortPutCommand] Check Port Status");
        //        var port = targetStage.Data as PortInfo;
        //        if (cfg == null || port == null)
        //        {
        //            Logger.InfoFormat("[PORT PUT] Cannot Put Glass {0} to Port {1}, PathConfigure or Target is null +", handJob.GLSID, targetStage.Name);
        //            return;
        //        }
        //        if (!Configure.GroupList.Exists(o => o.Name == cfg.TargetPathName)) //Port没有定义Group，玻璃只能进这个Port
        //        {
        //            Logger.Info("[CreatePortPutCommand] Check Port Status for No GroupList Port");
        //            if (IsPortStatusReadyForProcessing(port))//校验cst状态
        //            {
        //                var all2 = port.GlassInfos.ToList().FindAll(o => !o.IsExist && o.SlotSatus == (int)EnumGlassSlotStatus.Empty);
        //                //从上往下放片，需要排序
        //                if (all2.Count > 0)
        //                {
        //                    var slot = all2.OrderBy(x => x.FSlotNO).FirstOrDefault();
        //                    var cmd = factory.CreatePortPutCommand(port, handJob, slot.FSlotNO, handNo, cfg.InPriority);
        //                    AddCommand(cmd);
        //                    Logger.InfoFormat("[PORT PUT] Port Type is PU, Put Port [{0}] and Slot[{1}] for Glass {2} ", port.UnitName, slot.FSlotNO, handJob.GLSID);
        //                }
        //                else
        //                {
        //                    Logger.InfoFormat("[PORT PUT] Port Type is PU, Cannot Put Glass {0} to {1}, Port is Full. ", handJob.GLSID, port.UnitName);
        //                }
        //            }
        //            else
        //            {
        //                Logger.InfoFormat("[PORT PUT] {0} Status Not Ready For Put Glass {1} ", port.UnitID, handJob.GLSID);
        //            }
        //        }
        //        else
        //        {
        //            Logger.Info("[CreatePortPutCommand] Check Port Status for GroupList Port");
        //            var list2 = GetUnloadingPossiblePortList(handJob);//获取能放panel的所有port
        //            if (list2.Count > 0)
        //            {
        //                var unloadPort = GetCurrentUnloadingPort(list2);//获取当前使用的port
        //                if (unloadPort != null)
        //                {
        //                    Logger.InfoFormat("unloadPort != null unloadPort:{0}", unloadPort.PortID);
        //                    //if (unloadPort.UnitID == port.UnitID && list2.Exists(p => p.UnitID == port.UnitID))
        //                    //{
        //                        //var slot = unloadPort.PanelInfos.ToList().FindAll(u => !u.IsExist && u.SlotSatus == (int)EnumGlassSlotStatus.E).OrderByDescending(x => x.FSlotNO).FirstOrDefault();
        //                        //var cmd = factory.CreatePortPutCommand(unloadPort, handJob, slot.FSlotNO, handNo, cfg.InPriority);
        //                        //AddCommand(cmd);
        //                        //Logger.InfoFormat("[PORT PUT] Port Type is PU, Put Port [{0}] and Slot[{1}] for Glass {2} ", unloadPort.UnitName, slot.FSlotNO, handJob.HPanelID);
        //                        RobotCommand cmd;
        //                        int putslot;
        //                        if (unloadPort.GlassInfos.Count == 0)//pu口没有panel
        //                        {
        //                            putslot = unloadPort.TransferMode == "1" ? 60 : 120;
        //                            cmd = factory.CreatePortPutCommand(unloadPort, handJob, putslot, handNo, cfg.InPriority);
        //                        }
        //                        else
        //                        {  //pu口最小的slotid -1 为当前panel需要放入的位置
        //                            var slot = unloadPort.GlassInfos.OrderBy(x => x.TSlotNO).FirstOrDefault();
        //                            putslot = slot.TSlotNO - 1;
        //                            cmd = factory.CreatePortPutCommand(unloadPort, handJob, putslot, handNo, cfg.InPriority);
        //                        }
        //                        AddCommand(cmd);
        //                        Logger.InfoFormat("[PORT PUT] Port Type is PU, Put Port [{0}] and Slot[{1}] for Glass {2} ", unloadPort.UnitName, putslot, handJob.GLSID);
        //                    //}
        //                    //else
        //                    //{
        //                    //    Logger.InfoFormat("[PORT PUT] Port Type is PU, Port {0} is not unloading Port for Glass {1} ", unloadPort.UnitName, handJob.HPanelID);
        //                    //}
        //                }
        //                else
        //                {
        //                    //unloadPort = list2.Find(p => p.UnitID == port.UnitID);
        //                    unloadPort = list2.FirstOrDefault();
        //                    Logger.InfoFormat("unloadPort = list2.FirstOrDefault() unloadport:{0}", unloadPort.PortID);
        //                    if (unloadPort != null)
        //                    {
        //                        //var slot = unloadPort.PanelInfos.ToList().FindAll(u => !u.IsExist && u.SlotSatus == (int)EnumGlassSlotStatus.E).OrderByDescending(x => x.FSlotNO).FirstOrDefault();
        //                        //var cmd = factory.CreatePortPutCommand(unloadPort, handJob, 60, handNo, cfg.InPriority);
        //                        RobotCommand cmd;
        //                        int putslot;
        //                        if (unloadPort.GlassInfos.Count == 0)//pu口没有panel
        //                        {
        //                            putslot = unloadPort.TransferMode == "1" ? 60 : 120;
        //                            cmd = factory.CreatePortPutCommand(unloadPort, handJob, putslot, handNo, cfg.InPriority);
        //                        }
        //                        else
        //                        {  //pu口最小的slotid -1 为当前panel需要放入的位置
        //                            var slot = unloadPort.GlassInfos.OrderBy(x => x.TSlotNO).FirstOrDefault();
        //                            putslot = slot.TSlotNO - 1;
        //                            cmd = factory.CreatePortPutCommand(unloadPort, handJob, putslot, handNo, cfg.InPriority);
        //                        }
        //                        AddCommand(cmd);
        //                        Logger.InfoFormat("[PORT PUT] Port Type is PU, Put Port [{0}] and Slot[{1}] for Glass {2} ", unloadPort.UnitName, putslot, handJob.GLSID);
        //                    }
        //                    else
        //                    {
        //                        Logger.InfoFormat("[PORT PUT] Port Type is PU, Port {1} Status Not Ready for Glass {0} Put ", handJob.GLSID, port.UnitID);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                Logger.InfoFormat("[PORT PUT] Port Type is PU, Port {1} Status Not Ready for Glass {0} Put ", handJob.GLSID, port.UnitID);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        //private void CreateBufferPutCommand(Unit outStage, JobStage targetStage, RobotHand handNo, JobDataR handJob)
        //{
        //    try
        //    {
        //        var cfg = targetStage.PathConfigure;
        //        var buffer = targetStage.Data as Buffer;

        //        if (!CheckBufferPutResult(buffer)) return;

        //        var all2 = buffer.GlassList.Values.ToList().FindAll(o => !o.IsExistGlass && o.SlotStatus == EnumGlassSlotStatus.E);
        //        if (all2.Count == 0)
        //        {
        //            Logger.InfoFormat("[BF PUT] Buffer {0} is Full when Put Glass {1}", buffer.UnitID, handJob.PanelID);
        //        }
        //        else
        //        {
        //            all2.Sort(CompareGlassInfoBySlot);
        //            var slot = all2.Last();
        //            //为保证先进先出的原则，如果Buffer里面有一片也是从同样的上游设备进Buffer的，则当前片要先进Buffer
        //            var all3 = buffer.GlassList.Values.ToList().FindAll(o => o.IsExistGlass && o.OutEQPName == handJob.OutEQPName && o.OutUnitName == handJob.OutUnitName);
        //            int pri = cfg.InPriority;
        //            if (all3.Count > 0)
        //            {
        //                pri = PriorityHighest;
        //                Logger.InfoFormat("[BF PUT]  FIFO: Have Same Upstream Glass in Buffer, Set High Priority for Glass {0} ", handJob.PanelID);
        //            }
        //            var cmd = factory.CreateBufferPutCommand(buffer, handJob, slot.SlotNo, handNo, cfg.InPriority);
        //            AddCommand(cmd);
        //            Logger.InfoFormat("[BF PUT]  Put Buffer [{0}] and Slot[{1}] for Glass {2} ", buffer.UnitName, slot.SlotNo, handJob.PanelID);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        /// <summary>
        /// 向设备放panel
        /// </summary>
        /// <param name="RobotHand"></param>
        /// <param name="handJob"></param>
        /// <param name="targetmode"></param>
        private void CreateProcessPutCommand(RobotHand RobotHand, List<GlassInfo> putGlasss, RobotModel targetmode, bool IsPutWait, ref StringBuilder logStr)
        {
            try
            {
                GlassInfo putGlass = putGlasss.FirstOrDefault();
                string logInfo = string.Format("");
                //var cfg = targetStage.PathConfigure;
                // var targetUnit =HostInfo.Current.EQPInfo.Units// targetStage.Data as Unit;
                #region 校验 Robot
                if (!CheckModelPutArm(targetmode, RobotHand))
                {
                    Logger.InfoFormat("[CreateProcessPutCommand] Check RobotHand NG;  RobotHand != targetModel.PutArm;RobotHand:{0};targetModel.PutArm:{1} ", RobotHand.GetHashCode(), targetmode.PutArm);
                    return;
                }
                #endregion

                #region 获取targetUnit
                Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetmode.UnitID);
                if (targetUnit == null)
                {
                    Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand:{0}] Check PathConfigure or targetUnit NG; putGlass[{1},{2}];ProcessPutCommand NG ", IsPutWait, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logInfo = string.Format("[  PutCommand Check]   NG  [IsPutWaitCommand:{0}] Check PathConfigure or targetUnit NG;putGlass[{1},{2}];  ProcessPutCommand NG", IsPutWait, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }

                //Logger.InfoFormat("[CreateProcessPutCommand]targetUnit:{0};targetmode:{1} ", targetUnit.UnitName, targetmode.ModelName);
                #endregion

                if (IsPutWait)//PutWait模式
                {
                    #region PutWait校验targetmode
                    // Logger.InfoFormat("[CreateProcessPutCommand] IsPutWaitCommand ");
                    if (!targetmode.PutWaitEnable)
                    {
                        Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand] Check PutWaitEnable NG; robotModel.PutWaitEnable==false;robotModel:{0};PutWaitCommand NG ", targetmode.ModelName);
                        logInfo = string.Format("[     PutWait Check][IsPutWaitCommand]    NG  Check targetmode:{0}PutWaitEnable==false;PutWaitCommand NG", targetmode.ModelName);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        return;
                    }
                    else
                    {
                        //Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand]  Check PutWaitEnable OK; targetmode.PutWaitEnable==true;targetmode:{0} ", targetmode.ModelName);
                    }

                    //if (targetmode.ModelPosition == HostInfo.Current.CurrentPosition)//上一个命令处理的也是这个设备
                    //{
                    //    Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand]  Check CurrentPosition NG; targetmode.ModelPosition==HostInfo.Current.CurrentPosition;targetmode.ModelPosition:{0};PutWaitCommand NG ", targetmode.ModelPosition);
                    //    logInfo = string.Format("[  PutCommand Check][IsPutWaitCommand]    NG  Check targetmode.ModelPosition:{0} ==HostInfo.Current.CurrentPosition;PutWaitCommand NG", targetmode.ModelPosition);
                    //    logStr.Append(logInfo);
                    //    logStr.AppendLine();
                    //    return;
                    //}
                    //else
                    //{
                    //    //Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand]  Check CurrentPosition OK; targetmode.ModelPosition!=HostInfo.Current.CurrentPosition;targetmode.ModelPosition:{0} ", targetmode.ModelPosition);
                    //}
                    #endregion

                }
                //Logger.InfoFormat("[CreateProcessPutCommand] - Check RobotHand and targetmode OK;  RobotHand {0};targetmode:{1} ", RobotHand.ToString(), targetmode.ModelName);
                try
                {
                    //mode.UPLinkName
                    //StageLinksignalList.FirstOrDefault().LinkName
                    //Unit unit = LineInfo.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
                    //List<Linksignal> downLinksignalList = StageLinksignalList.Where(o => o.UnitName == targetUnit.UnitName &&o.LinkType== Consts.LinkType.DownstreamLinkSignal.GetHashCode()).ToList();
                    #region 校验DownLink
                    Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == targetUnit.UnitName && o.LinkName == targetmode.DownLinkName);
                    Logger.InfoFormat("[CreateProcessPutCommand][PutCommand] - Check DownLinksignal:{0}UnitName:{1}Postion:{2}", targetmode.DownLinkName, targetUnit.UnitName, targetmode.ModelPosition);
                    if (downLinksignal == null)
                    {
                        Logger.InfoFormat("[CreateProcessPutCommand] come1");
                        Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand:{0}] - Check Condition NG.DownLinksignal is null", IsPutWait);
                        logInfo = string.Format("[  PutCommand Check]   NG   DownLinksignal is null");
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        return;
                    }
                    var DownstreamLinkSignal = (DownstreamLinkSignal)downLinksignal.LinkSignalItem;
                    if (DownstreamLinkSignal.PositionFront1 || DownstreamLinkSignal.PositionBack1)
                    {
                        Logger.InfoFormat("[CreateProcessPutCommand] come1");
                        Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand:{0}] - Check Condition NG. PositionFront1 or PositionBack1 is true", IsPutWait);
                        logInfo = string.Format("[  PutCommand Check]   NG   PositionFront1 or PositionBack1 is true");
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        return;
                    }
                    //Logger.InfoFormat("[CreateProcessPutCommand] Check downLinksignalList OK; downLinksignal{0}", downLinksignal.LinkName);
                    //if (targetUnit.UnitStatus == EquipmentStatus.DOWN)
                    //{
                    //    Logger.InfoFormat("[PROCESS PUT ] Equipment Status is Down for ModelPosition {0}, Glass {1} ", targetStage.ModelPosition, handJob.GlassID);
                    //    return;
                    //}
                    bool checkLink = false;
                    if (IsPutWait)
                    {
                        Logger.InfoFormat("[CreateProcessPutCommand] come2");
                        checkLink = CheckPutWaitLinkStatusReceive(downLinksignal, targetUnit, ref logStr);
                        // Logger.InfoFormat("[CreateProcessPutCommand][PutWaitCommand] Check downLinksignal:{0}", checkLink);
                    }
                    else
                    {
                        Logger.InfoFormat("[CreateProcessPutCommand] come3");
                        checkLink = CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr);
                        // Logger.InfoFormat("[CreateProcessPutCommand][PutCommand] - Check DownLinksignal:{0}", checkLink);
                    }

                    if (!checkLink)//校验下游收片信号
                    {
                        Logger.InfoFormat("[CreateProcessPutCommand] come4");
                        //Logger.InfoFormat("[CreateProcessPutCommand][IsPutWaitCommand:{0}] - DownstreamLinkSignal Check NG; LinkName:{1};ProcessPutCommand NG", IsPutWait, downLinksignal.LinkName);
                        logInfo = string.Format("[  PutCommand Check]   NG  Check {1} Condition NG", IsPutWait, downLinksignal.LinkName);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        return;
                    }
                    #endregion
                    Logger.InfoFormat("[CreateProcessPutCommand] come5");
                    /// Logger.InfoFormat("[CreateProcessPutCommand] DownstreamLinkSignal Check OK; LinkName:{0}", downLinksignal.LinkName);
                    //Logger.InfoFormat("[CreateProcessPutCommand] Check RobotModel OK; RobotModel:{0}", targetmode.ModelName);
                    #region 获取SlotPostion
                    int SlotPostion = 99;
                    if (putGlasss.Count == 1 && !DownstreamLinkSignal.PositionBack1)
                        SlotPostion = 2;

                    //if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                    //{
                    //    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                    //    {
                    //        SlotPostion = putGlass.SlotPosition;
                    //        // Logger.InfoFormat("[CreateProcessPutCommand]LineMode == LineMode.OnlyA; SlotPostion:{0}", SlotPostion);
                    //    }
                    //    else if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyB)
                    //    {
                    //        SlotPostion = putGlass.SlotPosition;
                    //        //  Logger.InfoFormat("[CreateProcessPutCommand]LineMode == LineMode.OnlyB; SlotPostion:{0}", SlotPostion);
                    //    }
                    //    else
                    //    {

                    //        var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlass.PortID);
                    //        if (port != null)
                    //        {
                    //            //var glassCount = port.GlassInfos.Where(o => o.Position == handJob.Position).Count();
                    //            var glassCount = port.GlassInfos.Where(o => o.Position == putGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                    //            SlotPostion = glassCount == 1 ? putGlass.SlotPosition : 99;
                    //        }
                    //        //Logger.InfoFormat("[CreateProcessPutCommand]LineMode != LineMode.OnlyA;LineMode != LineMode.OnlyB; SlotPostion:{0} ", SlotPostion);
                    //    }
                    //}
                    //else
                    //{
                    //    SlotPostion = 0;
                    //}
                        #endregion

                        #region 生成命令
                    RobotCommand cmd = null;
                    if (IsPutWait)
                    {
                        cmd = factory.CreateProcessPutWaitCommand(targetUnit, RobotHand, -1, targetmode.ModelPosition, SlotPostion);
                        Logger.InfoFormat("[CreateProcessPutCommand][PutWaitCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6}), STSubCommand1({7}) ",
                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1, cmd.STSubCommand1);
                        logInfo = string.Format("[     PutWait Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6}), STSubCommand1({7}) ",
                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1, cmd.STSubCommand1);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                    }
                    else
                    {
                        // Logger.InfoFormat("[CreateProcessPutCommand][PutCommand] targetmode:{0};targetmode.INPriority{1} ", targetmode.ModelName, targetmode.INPriority);
                        cmd = factory.CreateProcessPutCommand(targetUnit, RobotHand, targetmode, SlotPostion);
                        Logger.InfoFormat("[CreateProcessPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
                        logInfo = string.Format("[  PutCommand Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                    }
                    //var glassPort = HostInfo.Current.PortList.FirstOrDefault(o => o.CassetteSequenceNo == putGlass.CassetteSequenceNo);
                    //if (glassPort != null)
                    //{
                    //    var glassList = glassPort.GlassInfos.ToList();
                    //    GlassInfo GlassA = null;
                    //    GlassInfo GlassB = null;
                    //    GlassInfo Glass = null;
                    //    GetGlassByGlass(glassList, putGlass, ref GlassA, ref GlassB, ref Glass, "CreateProcessPutCommand");
                    //    cmd.PutGlass = Glass;
                    //    cmd.PutGlassA = GlassA;
                    //    cmd.PutGlassB = GlassB;
                    //}
                    //else
                    //{
                    //    cmd.PutGlass = putGlass;
                    //}

                    //GlassInfo GlassA = null;
                    //GlassInfo GlassB = null;
                    //GlassInfo GlassC = null;
                    //GlassInfo GlassD = null;
                    //GetGlassByGlass(putGlass, null, null,ref GlassA, ref GlassB, ref GlassC, ref GlassD, "CreateProcessPutCommand");

                    cmd.PutGlassA = putGlasss[0];
                    if (putGlasss.Count > 1)
                        cmd.PutGlassB = putGlasss[1];

                    cmd.STPutPosition1string = targetmode.ModelName;
                    AddCommand(cmd);
                    #endregion

                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    Logger.Info(ex);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }

        }
        #endregion

        #region Exchange Command
        private void FindExchangeCommand(ref StringBuilder logStr)
        {
            try
            {
                // Logger.Info("[FindExchangeCommand] begin");
                var robotHand = RobotHand.LowHand;
                string logInfo = string.Format("");
                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut || HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                //{
                //    Logger.Info("[FindExchangeCommand]   LineMode is ForceCleanOut; FindExchangeCommand NG ");
                //    logInfo = string.Format("[    Exchange Check]   LineMode is ForceCleanOut; FindExchangeCommand NG");
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //    return;
                //}
                #region 校验手臂资料  
                if (Robot.LowerExistOn && Robot.UpperExistOn)
                {
                    Logger.Info("[FindExchangeCommand][RobotArmCheck] - Check Arm NG.Arm(Lower)==true&&Arm(Upper)==true");

                    logInfo = string.Format("[    Exchange Check]   NG  Check Arm NG.Arm(Lower)==true&&Arm(Upper)==true");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                if (!robot.LowerExistOn && !robot.UpperExistOn)
                {
                    Logger.Info("[FindExchangeCommand][RobotArmCheck] - Check Arm NG.Arm(Lower)==false&&Arm(Upper)==false");
                    logInfo = string.Format("[    Exchange Check]   NG  Check Arm NG.Arm(Lower)==false&&Arm(Upper)==false");

                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion

                //Logger.InfoFormat("[FindExchangeCommand] Check Hand OK;Robot UpperExistOn{0}, LowerExistOn{1}, Can Make Exchange Command ", robot.UpperExistOn, robot.LowerExistOn);
                if (Robot.UpperExistOn)
                    robotHand = RobotHand.UpHand;


                #region 获取putGlass
                GlassInfo putGlass = null;
                //LineMode LineMode = HostInfo.Current.EQPInfo.LineMode;//putJobData.ProcessMode;
                //Logger.InfoFormat("[FindExchangeCommand] LineMode:{0}", LineMode.ToString());
                switch (HostInfo.Current.EQPInfo.LineMode)
                {
                    //case LineMode.OnlyA:
                    //    if (robotHand == RobotHand.LowHand)
                    //    {
                    //        if (Robot.LowHandGlass1 != null)
                    //        {
                    //            putGlass = Robot.LowHandGlass1;
                    //            //Logger.InfoFormat("[FindExchangeCommand][ProcessMode.OnlyA][Robot.LowHandGlass1 != null]   putGlass[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    //        }
                    //        else
                    //        {
                    //            // Logger.InfoFormat("[FindExchangeCommand][ProcessMode.OnlyA][Robot.LowHandGlass1 == null]    ");
                    //        }
                    //    }
                    //    else if (robotHand == RobotHand.UpHand)
                    //    {
                    //        if (Robot.UpHandGlass1 != null)
                    //        {
                    //            putGlass = Robot.UpHandGlass1;
                    //            // Logger.InfoFormat("[FindExchangeCommand][ProcessMode.OnlyA][Robot.UpHandGlass1 != null]   putGlass[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    //        }
                    //        else
                    //        {
                    //            //Logger.InfoFormat("[FindExchangeCommand][ProcessMode.OnlyA][Robot.UpHandGlass1 == null]    ");
                    //        }
                    //    }
                    //    break;
                    default:
                        if (robotHand == RobotHand.LowHand)
                        {
                            //Logger.Info("[FindExchangeCommand] [robotHand == RobotHand.LowHand]");
                            if (Robot.LowHandGlass1 != null)
                            {
                                putGlass = Robot.LowHandGlass1;
                                //Logger.InfoFormat("[FindExchangeCommand] [Robot.LowHandGlass1!=null]   putGlass[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            }
                            else if (Robot.LowHandGlass2 != null)
                            {
                                putGlass = Robot.LowHandGlass2;
                                // Logger.InfoFormat("[FindExchangeCommand] [Robot.LowHandGlass2!=null]   putGlass[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            }
                            else
                            {
                                // Logger.InfoFormat("[FindExchangeCommand] [robotHand == RobotHand.LowHand] LowHandGlass1 and LowHandGlass2 ==null ");
                            }
                        }
                        else if (robotHand == RobotHand.UpHand)
                        {
                            // Logger.Info("[FindExchangeCommand] robotHand == RobotHand.UpHand");
                            if (Robot.UpHandGlass1 != null)
                            {
                                putGlass = Robot.UpHandGlass1;
                                //Logger.InfoFormat("[FindExchangeCommand] [Robot.UpHandGlass1!=null]   putGlass[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            }
                            else if (Robot.UpHandGlass2 != null)
                            {
                                putGlass = Robot.UpHandGlass2;
                                // Logger.InfoFormat("[FindExchangeCommand] [Robot.UpHandGlass2!=null]   putGlass[{0},{1}] ", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            }
                            else
                            {
                                // Logger.InfoFormat("[FindExchangeCommand] [robotHand == RobotHand.UpHand] UpHandGlass1 and UpHandGlass2 ==null ");
                            }
                        }
                        break;
                }
                if (putGlass == null)
                {
                    Logger.InfoFormat("[FindExchangeCommand][ArmDataCheck] - RobotArm({0}),Arm Glass Data is null", robotHand);
                    logInfo = string.Format("[    Exchange Check]   NG  RobotArm({0}),Arm Glass Data is null", robotHand);
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                // Logger.InfoFormat("[FindExchangeCommand][ArmDataCheck] - Robot has Glass.CassetteSequenceNo[{0}],Postion[{1}] ", putGlass.CassetteSequenceNo, putGlass.Position);
                #endregion


                //var putJobData = robotHand != RobotHand.LowHand ? Robot.UpHandGlass1 : Robot.LowHandGlass1;

                CreateProcessExchange(robotHand, putGlass, ref logStr);

                // Logger.Info("[FindExchangeCommand] end");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }
        private void CreateProcessExchange(RobotHand robotHand, GlassInfo putGlass, ref StringBuilder logStr)
        {
            try
            {
                //  Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] ");
                string logInfo = string.Format("");
                #region 获取putModel
                var putRobotModel = GetCurrentModel(putGlass.ModelPosition);
                if (putRobotModel == null)
                {
                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Check Action NG.Arm Glass[{0},{1}] Condition NG,ModelPosition={2},ModelName=null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putGlass.ModelPosition);
                    logInfo = string.Format("[    Exchange Check]   NG  Arm Glass[{0},{1}] Condition NG,ModelPosition={2},ModelName=null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putGlass.ModelPosition);

                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check putRobotModel OK;  putRobotModel :{0}; ", putRobotModel.ModelName);

                #endregion
                #region 获取nextStaget
                List<JobStage> targetStageList = FindNextStage(putGlass);
                if (targetStageList.Count == 0)
                {
                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Get TargetStage NG.TargetStage.Count=0");
                    logInfo = string.Format("[    Exchange Check]   NG  Glass TargetStage is null;Glass[{0},{1}];GlassModelPosition:{2}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putGlass.ModelPosition);

                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check targetStageList.Count OK; targetStageList.Count:{0} ", targetStageList.Count);
                #endregion

                foreach (var targetStage in targetStageList)
                {

                    #region 获取targetModel
                    Unit targetUnit = null;
                    //RobotModel targetModel = null;
                    //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] targetStage.ModelPosition:{0} ", targetStage.ModelPosition);
                    //var targetModel = GetCurrentModel(targetStage.ModelPosition, ref targetUnit);
                    RobotModel targetModel = GetCurrentModel(targetStage.ModelPosition);
                    #endregion
                    //if (targetModel==null)
                    //{
                    //    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] targetModel ==null; targetStage.ModelPosition:{0} ", targetStage.ModelPosition);
                    //}else
                    //{
                    //    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] targetModel !=null; targetmodel.ModelName:{0} ", targetModel.ModelName);
                    //}
                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Action Check.Arm Glass[{0},{1}],CurrentModel={2},TargetModel={3}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putRobotModel.ModelName, targetModel.ModelName);
                    targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetModel.UnitID);
                    #region 校验 Robot PutArm
                    if (!CheckModelPutArm(targetModel, robotHand))
                    {
                        Logger.InfoFormat("[FindExchangeCommand] - Arm mismatch.PutArm({0}),Check TargetModel PutArm({1}) ", robotHand.GetHashCode(), targetModel.PutArm);
                        continue;
                    }
                    #endregion

                    #region 校验targetModel
                    if (targetUnit == null)
                    {
                        Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Check TargetUnit NG.TargetUnit is null,TargetStage.ModelPosition={0},TargetStage.ModelName={1}", targetStage.ModelPosition, targetModel.ModelName);
                        logInfo = string.Format("[    Exchange Check]   NG  Check TargetUnit is null.TargetStage.ModelPosition={0},TargetStage.ModelName={1}", targetStage.ModelPosition, targetModel.ModelName);

                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }
                    //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check targetUnit OK; targetUnit:{0},targetStage.ModelPosition:{1},ModelPath:{2}", targetStage.UnitName, targetStage.ModelPosition, targetStage.PathConfigure.ModePath);
                    if (!targetModel.ExchangeEnable)
                    {
                        Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Action Check NG.Targetmodel={0},ExchangeEnable is false ", targetModel.ModelName);
                        logInfo = string.Format("[    Exchange Check]   NG  Check Targetmodel={0},ExchangeEnable is false", targetModel.ModelName);

                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }
                    //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check targetmodel.ExchangeEnable ok; targetmodel.ExchangeEnable == true;targetmodel:{0}", targetmodel.ModelName);

                    //if (targetUnit.CommandType == 1)
                    //{
                    //    if (!targetUnit.UpstreamInlineMode || targetUnit.LoadingStop)
                    //    {
                    //        Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Check Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", targetUnit.UnitName, targetUnit.UpstreamInlineMode, targetUnit.LoadingStop);
                    //        logInfo = string.Format("[    Exchange Check]   NG  Condition NG.TargetUnit={0},UpstreamInlineMode={1},LoadingStop={2}", targetUnit.UnitName, targetUnit.UpstreamInlineMode, targetUnit.LoadingStop);

                    //        logStr.Append(logInfo);
                    //        logStr.AppendLine();
                    //        continue;
                    //    }
                    //    //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check UpstreamInlineMode and  LoadingStop OK; targetUnit.UpstreamInlineMode=true; targetUnit.LoadingStop=false; targetUnit:{0};", targetUnit.UnitName);
                    //}

                    //if (targetUnit.RobotModelList == null || targetUnit.RobotModelList.Count == 0)
                    //{
                    //    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Check RobotModelList NG;targetUnit.RobotModelList == null || targetUnit.RobotModelList.Count == 0;targetUnit:{0}; continue; ", targetUnit.UnitName);
                    //    logInfo = string.Format("[    Exchange Check]   NG  Check targetUnit.RobotModelList == null || targetUnit.RobotModelList.Count == 0;targetUnit:{0};", targetUnit.UnitName);
                    //    logStr.Append(logInfo);
                    //    logStr.AppendLine();
                    //    continue;
                    //}
                    //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check RobotModelList OK; targetUnit.RobotModelList.Count:{0} ", targetUnit.RobotModelList.Count);

                    if (putRobotModel.GroupName == targetModel.GroupName)
                    {
                        Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Check GroupName NG.CurrentModel.GroupName== TargetMode.GroupName.GroupName:{0}", putRobotModel.GroupName);
                        continue;
                    }
                    //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Check GroupName ok; [putRobotModel.GroupName!= targetMode.GroupName] putRobotModel.GroupName:{0}; targetMode.GroupName:{1}", putRobotModel.GroupName, targetmodel.GroupName);
                    #endregion

                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Condition Check OK; TargetModel={0},RobotArm({1}),ExchangeEnable={2} ", targetModel.ModelName, robotHand.ToString(), targetModel.ExchangeEnable);
                    foreach (var link in targetModel.LinksignalList)
                    {
                        if (link.LinkType == Consts.LinkType.UpstreamLinkSignal.GetHashCode())
                        {
                            #region 校验UpLink
                            //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange]RobotModel:{0},LinkName:{1} ", targetmodel.ModelName, link.LinkName);
                            //校验Link
                            if (!CheckLinkStatusExchange(link, targetUnit, ref logStr))
                            {
                                //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - UpstreamLinkSignal Check NG; link.LinkName:{0}", link.LinkName);
                                //logInfo = string.Format("[    Exchange Check]   NG  UpstreamLinkSignal Check NG; link.LinkName:{0}", link.LinkName);
                                //logStr.Append(logInfo);
                                //logStr.AppendLine();
                                continue;
                            }
                            #endregion
                            #region 校验 targetmodel Glass资料
                            //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] UpstreamLinkSignal Check OK");
                            if (targetModel.GlassA == null && targetModel.GlassB == null)
                            {
                                Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Data Check.Targetmodel={0},Glass is null", targetModel.ModelName);
                                logInfo = string.Format("[    Exchange Check]   NG Targetmodel={0},Glass is null", targetModel.ModelName);
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }

                            //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] UpLink Check OK;targetmodel.Glass OK; LinkName:{0}; ", link.LinkName);

                            #endregion

                            #region 获取 SlotPostion
                            int PutSlotPostion = 0;
                            int GetSlotPostion = 0;
                            //if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                            //{
                            //    GetSlotPostion = targetModel.UsedJobBlockNo == 3 ? 99 : targetModel.UsedJobBlockNo;
                            //    //if (targetmodel.UsedJobBlockNo == 0)
                            //    //{
                            //    //    GetSlotPostion = 1;
                            //    //}
                            //    //else
                            //    //{
                            //    //    GetSlotPostion = targetmodel.UsedJobBlockNo == 3 ? 99 : targetmodel.UsedJobBlockNo;
                            //    //}   
                            //    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                            //    {
                            //        PutSlotPostion = putGlass.SlotPosition;
                            //        //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] LineMode== LineMode.OnlyA; PutSlotPostion:{0}", PutSlotPostion);
                            //    }
                            //    else
                            //    {
                            //        var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlass.PortID);
                            //        if (port != null)
                            //        {
                            //            var glassCount = port.GlassInfos.Where(o => o.Position == putGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                            //            PutSlotPostion = glassCount == 1 ? putGlass.SlotPosition : 99;
                            //            //Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] LineMode!= LineMode.OnlyA; PutSlotPostion:{0}", PutSlotPostion);
                            //        }
                            //    }
                            //}
                            //else
                            {
                                GetSlotPostion = 0;
                                PutSlotPostion = 0;
                            }
                            //Logger.InfoFormat("[CreateProcessGetCommand] [LineMode!= LineMode.OnlyA]  GetslotPostion {0} ", GetSlotPostion);
                            #endregion

                            #region 生成Exchange 命令
                            // Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange]  targetRobotMode:{0};targetRobotMode.ExchangePriority:{1}", targetmodel.ModelName, targetmodel.ExchangePriority);
                            //var cmd = factory.CreateProcessExchangeCommand(robotHand, targetUnit, putJobData, maxInPriority * 4, targetRobotModeItem.ModelPosition, SlotPostion, targetRobotModeItem.RobotMotion);
                            var cmd = factory.CreateProcessExchangeCommand(robotHand, targetUnit, putGlass, targetModel.ExchangePriority, targetModel.ModelPosition, GetSlotPostion, PutSlotPostion, targetModel.RobotMotion);
                            //cmd.GlassInfo = putJobData;
                            GlassInfo ExchangeGetGlassA = null;
                            GlassInfo ExchangeGetGlassB = null;
                            GlassInfo ExchangeGetGlass = null;
                            GlassInfo ExchangePutGlassA = null;
                            GlassInfo ExchangePutGlassB = null;
                            GlassInfo ExchangePutGlass = null;
                            GetUnitModelGlass(targetModel, putGlass, ref ExchangeGetGlass, ref ExchangeGetGlassA, ref ExchangeGetGlassB, ref ExchangePutGlassA, ref ExchangePutGlassB, ref ExchangePutGlass, "FindExchangeCommand");
                            cmd.GetGlass = ExchangeGetGlass;
                            cmd.GetGlassA = ExchangeGetGlassA;
                            cmd.GetGlassB = ExchangeGetGlassB;
                            cmd.PutGlass = ExchangePutGlass;
                            cmd.PutGlassA = ExchangePutGlassA;
                            cmd.PutGlassB = ExchangePutGlassB;
                            cmd.STGetPosition1string = targetModel.ModelName;
                            cmd.STPutPosition1string = targetModel.ModelName;
                            AddCommand(cmd);
                            Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
                            cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
                            logInfo = string.Format("[    Exchange Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
                            cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);

                            logStr.Append(logInfo);
                            logStr.AppendLine();
                            #endregion
                        }

                    }
                    // }

                    //}
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }
        private void GetUnitModelGlass(RobotModel RobotModel, ref GlassInfo Glass, ref GlassInfo GlassA, ref GlassInfo GlassB, string functionName)
        {
            switch (RobotModel.UsedJobBlockNo)
            {
                case 0:
                    if (RobotModel.GlassA != null)
                    {
                        Glass = RobotModel.GlassA;
                        Logger.InfoFormat("[{2}] [UsedJobBlockNo==0] Glass [{0},{1}]", Glass.CassetteSequenceNo, Glass.SlotSequenceNo, functionName);
                    }
                    break;
                case 1:
                    if (RobotModel.GlassA != null)
                    {
                        GlassA = RobotModel.GlassA;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==1] GlassA [{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, functionName);
                    }
                    break;
                case 2:
                    if (RobotModel.GlassB != null)
                    {
                        GlassB = RobotModel.GlassB;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==2] GlassB [{0},{1}]", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, functionName);
                    }
                    break;
                case 3:
                    if (RobotModel.GlassA != null)
                    {
                        GlassA = RobotModel.GlassA;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==3] GlassA [{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, functionName);
                    }
                    if (RobotModel.GlassB != null)
                    {
                        GlassB = RobotModel.GlassB;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==3] GlassB [{0},{1}]", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, functionName);
                    }
                    break;
            }
        }
        private void GetUnitModelGlass(RobotModel RobotModel, GlassInfo PutGlass, ref GlassInfo Glass, ref GlassInfo GlassA, ref GlassInfo GlassB, ref GlassInfo ExchangePutGlassA, ref GlassInfo ExchangePutGlassB, ref GlassInfo ExchangePutGlass, string functionName)
        {
            switch (RobotModel.UsedJobBlockNo)
            {
                case 0:
                    if (RobotModel.GlassA != null)
                    {
                        Glass = RobotModel.GlassA;
                        Logger.InfoFormat("[{2}] [UsedJobBlockNo==0] Glass [{0},{1}]", Glass.CassetteSequenceNo, Glass.SlotSequenceNo, functionName);
                    }
                    break;
                case 1:
                    if (RobotModel.GlassA != null)
                    {
                        GlassA = RobotModel.GlassA;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==1] GlassA [{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, functionName);
                    }
                    break;
                case 2:
                    if (RobotModel.GlassB != null)
                    {
                        GlassB = RobotModel.GlassB;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==2] GlassB [{0},{1}]", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, functionName);
                    }
                    break;
                case 3:
                    if (RobotModel.GlassA != null)
                    {
                        GlassA = RobotModel.GlassA;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==3] GlassA [{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, functionName);
                    }
                    if (RobotModel.GlassB != null)
                    {
                        GlassB = RobotModel.GlassB;
                        Logger.InfoFormat("[{2}][UsedJobBlockNo==3] GlassB [{0},{1}]", GlassB.CassetteSequenceNo, GlassB.SlotSequenceNo, functionName);
                    }
                    break;
            }
            var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == PutGlass.PortID);
            //port.GlassInfos.FirstOrDefault();
            switch (PutGlass.SlotPosition)
            {
                case 0:
                    ExchangePutGlass = PutGlass;
                    break;
                case 1:
                    ExchangePutGlassA = PutGlass;
                    var glassB = port.GlassInfos.FirstOrDefault(o => o.Position == PutGlass.Position && o.SlotPosition != 1);
                    ExchangePutGlassB = glassB;
                    break;
                case 2:
                    var glassA = port.GlassInfos.FirstOrDefault(o => o.Position == PutGlass.Position && o.SlotPosition != 2);
                    ExchangePutGlassA = glassA;
                    ExchangePutGlassB = PutGlass;
                    break;
            }
        }

        //private void FindExchangeCommand()
        //{
        //    var robotHand = RobotHand.LowHand;
        //    try
        //    {
        //        Logger.Info("+++ FindExchangeCommand Step 1,  Start Find Robot Hand Exist Glass");
        //        if (Robot.LowerExistOn && Robot.UpperExistOn)
        //        {
        //            Logger.Info("+++ Robot  Already Have Two Glass, Can not Make Exchange Command ");
        //            return;
        //        }
        //        if (!robot.LowerExistOn && !robot.UpperExistOn)
        //        {
        //            Logger.Info("+++ Robot Have Not Glass, Can not Make Exchange Command ");
        //            return;
        //        }
        //        Logger.InfoFormat("+++ Robot UpperExistOn{0}, LowerExistOn{1}, Can Make Exchange Command ", robot.UpperExistOn, robot.LowerExistOn);
        //        if (Robot.UpperExistOn)
        //            robotHand = RobotHand.UpHand;

        //        Logger.Info("+++ FindExchangeCommand Step 2,  Start Find Robot Put Glass");

        //        var putJobData = robotHand != RobotHand.LowHand ? Robot.UpHandGlass : Robot.LowHandGlass;
        //        if (putJobData == null)
        //        {
        //            Logger.InfoFormat("+++ Have Not Put Job Data on {0} ", robotHand);
        //            return;
        //        }
        //        Logger.InfoFormat("+++FindExchangeCommand Step 3, Start Exchange From Process Check, Put Glass ID: {0} ", putJobData.PanelID);
        //        CreateProcessExchange(robotHand, putJobData);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        //private void CreateProcessExchange(RobotHand robotHand, JobDataR putJobData)
        //{
        //    try
        //    {
        //        Logger.InfoFormat("+++FindExchangeCommand Step 4, Start Find Put Glass:{0} OutEquipment and OutUnit", putJobData.PanelID);
        //        var outEQP = GetOutEquipment(putJobData);
        //        if (outEQP == null) return;
        //        var outUnit = GetOutUnit(outEQP, putJobData);
        //        if (outUnit == null) return;
        //        Logger.InfoFormat("+++Find Put Glass:{0} OutEquipment and Out Unit Success,OutEqupment:{1},OutUnit:{2}", putJobData.PanelID, outEQP.EQPName, outUnit.UnitName);
        //        Logger.InfoFormat("+++FindExchangeCommand Step 5, Start Find Put Glass:{0} To NextEquipment", putJobData.PanelID);
        //        List<JobStage> nextEquipment = FindNextEquipment(robotHand, outUnit.UnitName, putJobData);
        //        if (nextEquipment == null || nextEquipment.Count == 0)
        //        {
        //            Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Put Glass ID: {0}, Not Find Target, OutEQP: {1}, OutUnit: {2},  RobotHand: {3} ", putJobData.PanelID, putJobData.OutEQPName, putJobData.OutUnitName, robotHand);
        //            return;
        //        }
        //        Logger.Info("+++FindExchangeCommand Step 6(Last), Start Check NextEquipment Status and Add Exchange Command");
        //        nextEquipment = nextEquipment.OrderBy(o => o.ReadyTime).ToList();
        //        foreach (var jobStage in nextEquipment)
        //        {
        //            var unit = jobStage.Data as Unit;
        //            if (unit == null)
        //            {
        //                Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Put Glass ID: {0}, Target {1} Not Unit, Cannot Exchange. OutEQP: {2}, OutUnit: {3},  RobotHand: {4} ", putJobData.PanelID, jobStage.Name, putJobData.OutEQPName, putJobData.OutUnitName, robotHand);
        //                continue;
        //            }
        //            if (unit.Linksignals == null || unit.Linksignals.Count == 0)
        //            {
        //                Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Put Glass ID: {0}, Target {1} Have Not Linksignals. OutEQP: {2}, OutUnit: {3},  RobotHand: {4} ", putJobData.PanelID, jobStage.Name, putJobData.OutEQPName, putJobData.OutUnitName, robotHand);
        //                continue;
        //            }
        //            foreach (var link in unit.Linksignals)
        //            {
        //                if (CheckLinkStatusExchange(link))
        //                {
        //                    var inpath = jobStage.PathConfigure;
        //                    if (link.SendJobData == null || inpath == null)
        //                    {
        //                        Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Put Glass ID: {0}, Target {1} No Send Job Data. ", putJobData.PanelID, link.EQPName);
        //                        continue;
        //                    }
        //                    var list22 = LineInfo.GlassList;
        //                    foreach (var info2 in list22)
        //                    {
        //                        if (info2.Value.PanelID == link.SendJobData.PanelID.Trim())
        //                        {
        //                            link.SendJobData.RuleID = info2.Value.RuleID;
        //                            break;
        //                        }
        //                    }
        //                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] NextEquipment LinkStatus OK, Start Find Path Configure for RuleID,LineMode:{0},UnitName:{1},RuleID{2} ", LineInfo.LineRunMode.ToString(), unit.UnitName, link.SendJobData.RuleID);
        //                    var outpathlist = Configure.GetPathConfigureList(LineInfo.LineRunMode.ToString(), unit.UnitName, link.SendJobData.RuleID);
        //                    if (outpathlist == null || outpathlist.Count == 0)
        //                    {
        //                        Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Put Glass ID: {0}, Cannot Find Path by OutUnit {1}, Get Glass ID: {2}, RuleID: {3}, LineRunMode:{4}", putJobData.PanelID, unit.UnitName, link.SendJobData.PanelID, GetString(link.SendJobData.RuleID), LineInfo.LineRunMode.ToString());
        //                        continue;
        //                    }
        //                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Find Path Configure for RuleID Success,Configure Path Count:{0} ", outpathlist.Count);
        //                    var maxOutPriority = outpathlist.Max(o => o.OutPriority);
        //                    Logger.Info("[FindExchangeCommand][CreateProcessExchange] CreateProcessExchangeCommand amd Add Exchange Command ");
        //                    var cmd = factory.CreateProcessExchangeCommand(robotHand, unit, link.SendJobData, putJobData, inpath.InPriority + maxOutPriority);
        //                    AddCommand(cmd);
        //                    UpdateOutUnitBeforeGet(link.SendJobData, outUnit.EQPName, outUnit.UnitName);
        //                    Logger.InfoFormat("[FindExchangeCommand][CreateProcessExchange] Put Glass ID: {0}, OutUnit {1}, Get Glass ID: {2} ", putJobData.PanelID, unit.UnitName, link.SendSubstrateID);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //}
        #endregion

        #region Transfer Command
        private void TransferIndexToEQPCommand(ref StringBuilder logStr, List<PortInfo> LoadingPortList)
        {
            try
            {
                //Logger.InfoFormat("[TransferIndexToEQPCommand] Begin");
                string logInfo = string.Format("");
                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut || HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                //{
                //    Logger.Info("[TransferIndexToEQPCommand]   LineMode is ForceCleanOut; TransferEQPToIndexOrEQPCommand NG ");
                //    logInfo = string.Format("[    Transfer Check]   LineMode is ForceCleanOut; TransferEQPToIndexOrEQPCommand NG");
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //    return;
                //}
                //判断Robot手臂上是否有玻璃
                #region 校验手臂资料
                if (Robot.UpperExistOn || Robot.LowerExistOn)
                {
                    Logger.Info("[TransferIndexToEQPCommand] Check RobotHand NG; Robot.UpperExistOn or Robot.LowerExistOn; TransferIndexToEQPCommand NG ");
                    logInfo = string.Format("[TransferPort Check]   NG  Check RobotHand NG:Robot.UpperExistOn or Robot.LowerExistOn; TransferIndexToEQPCommand NG");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.Info("[TransferIndexToEQPCommand] Check RobotHand OK; ");
                #endregion

                #region 校验PutGlass
                //GlassInfo putGlass = null;
                //if (Robot.LowerExistOn)
                //{
                //    if (Robot.LowHandGlass1 != null)
                //    {
                //        putGlass = Robot.LowHandGlass1;
                //        //Logger.InfoFormat("[TransferIndexToEQPCommand][Robot.LowHandGlass1 != null] PutGlassInfo[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //    }
                //    else if (Robot.LowHandGlass2 != null)
                //    {
                //        putGlass = Robot.LowHandGlass2;
                //        // Logger.InfoFormat("[TransferIndexToEQPCommand][Robot.LowHandGlass2 != null] PutGlassInfo[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //    }
                //    else
                //    {
                //        // Logger.InfoFormat("[TransferIndexToEQPCommand][Robot.LowerExistOn] Robot.LowHandGlass1==null and Robot.LowHandGlass2==null");
                //    }
                //}
                //else if (Robot.UpperExistOn)
                //{
                //    if (Robot.UpHandGlass1 != null)
                //    {
                //        putGlass = Robot.UpHandGlass1;
                //        //Logger.InfoFormat("[TransferIndexToEQPCommand][Robot.UpHandGlass1 != null] PutGlassInfo[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //    }
                //    else if (Robot.UpHandGlass2 != null)
                //    {
                //        putGlass = Robot.UpHandGlass2;
                //        // Logger.InfoFormat("[TransferIndexToEQPCommand][Robot.UpHandGlass2 != null] PutGlassInfo[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                //    }
                //    else
                //    {
                //        // Logger.InfoFormat("[TransferIndexToEQPCommand][Robot.UpperExistOn] Robot.UpHandGlass1==null and Robot.UpHandGlass2==null");
                //    }
                //}
                //else
                //{
                //    // Logger.InfoFormat("[TransferIndexToEQPCommand] Robot.LowerExistOn==false and  Robot.UpperExistOn==false");
                //}

                //if (putGlass != null)
                //{
                //    var putGlassModel = GetCurrentModel(putGlass.ModelPosition);
                //    //if (getGlass.ModelPosition == putGlass.ModelPosition)
                //    if (!string.IsNullOrEmpty(putGlassModel.PortID))
                //    {
                //        Logger.Info(string.Format("[TransferIndexToEQPCommand] - Check Action NG;Robot Arm has Glass,GlassModelName={0}", putGlassModel.ModelName));
                //        logInfo = string.Format("[TransferPort Check]   NG  Robot Arm has Glass,GlassModelName={0}", putGlassModel.ModelName);

                //        logStr.Append(logInfo);
                //        logStr.AppendLine();
                //        return;
                //    }
                //}
                //else
                //{
                //    // Logger.Info(string.Format("[TransferIndexToEQPCommand]  putJobData==null; CreateTransferIndexToEQPCommand NG"));
                //}
                #endregion

                #region 获取 currentLoadingPort
                //List<PortInfo> LoadingPortList = GetLoadingPossiblePortList(HostInfo.Current.PortList, "TransferIndexToEQPCommand");
                if (LoadingPortList == null || LoadingPortList.Count == 0)
                {
                    Logger.Info("[TransferIndexToEQPCommand] - Check Action NG.LoadingPortList.Count=0");
                    logInfo = string.Format("[TransferPort Check]   NG  LoadingPortList.Count=0");

                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.InfoFormat("[TransferIndexToEQPCommand] Check LoadingPortList OK;LoadingPortList.Count:{0} ", LoadingPortList.Count());
                //PortInfo currentLoadingPort = LoadingPortList.OrderByDescending(o => o.CassetteInfo.CassetteProcessStartTime).FirstOrDefault();
                //PortInfo currentLoadingPort = LoadingPortList.FirstOrDefault(o => o.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing);
                //if (currentLoadingPort == null)
                //{
                //    //LoadingPortList = LoadingPortList.Where(o => o.WaitingforProcessingTime != DateTime.MinValue).ToList();
                //    currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
                //}
                PortInfo currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
                //Logger.InfoFormat("[TransferIndexToEQPCommand] CurrentPort:{0} ", currentLoadingPort.PortID);

                #endregion


                #region 获取 getRobotModel、GetGlass
                var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == currentLoadingPort.UnitID);

                //20180801 lsq_Modify
                var GlassInfos = currentLoadingPort.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();
                GlassInfo getGlass = null;

                int ModelPosition = 0;
                PortGetType PortGetType = PortGetType.ASC;
                RobotModel getRobotModel = null;

                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.MixRun)
                //{
                //    //Logger.InfoFormat("[CreatePortGetCommand][ProcessMode.MixRun]  ");
                //    //获取MixRunConfig 中两条recipe在port中是否都存在
                //    MixRunMode(ref LoadingPortList, ref currentLoadingPort, ref GlassInfos, ref getGlass, ref logStr);
                //    //Logger.InfoFormat("[CreatePortGetCommand][ProcessMode.MixRun] CurrentGlasss  glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);

                //}
                var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
                if (model != null)
                {
                    ModelPosition = model.ModelPosition;
                    PortGetType = model.PortGetType;
                    getRobotModel = model;
                }
                if (getGlass == null)
                {
                    //var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
                    //if (model != null)
                    //{
                    //    ModelPosition = model.ModelPosition;
                    //    PortGetType = model.PortGetType;
                    //    getRobotModel = model;
                    //}
                    //if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                    //{
                    //    GlassInfos = GlassInfos.Where(o => o.SlotPosition == 1).ToList();
                    //    if (PortGetType == PortGetType.DESC)
                    //    {
                    //        getGlass = GlassInfos.OrderByDescending(o => o.Position).FirstOrDefault();
                    //    }
                    //    else //if (PortGetType == PortGetType.DESC)
                    //    {
                    //        getGlass = GlassInfos.OrderBy(o => o.Position).FirstOrDefault();
                    //        //info = all.OrderByDescending(o => o.Position).FirstOrDefault();
                    //    }
                    //    if (getGlass == null)
                    //    {
                    //        //Logger.InfoFormat("[TransferIndexToEQPCommand][LineMode== LineMode.OnlyA] Check getGlass NG; info==null; PortGetCommand NG ");
                    //        logInfo = string.Format("[TransferPort Check]   NG    OnlyA,getGlass = null; PortGetCommand NG");
                    //        logStr.Append(logInfo);
                    //        logStr.AppendLine();
                    //        return;
                    //    }
                    //    // info = all.OrderBy(o => o.Position).FirstOrDefault();
                    //    //Logger.InfoFormat("[TransferIndexToEQPCommand][ProcessMode.OnlyA] CurrentGlasss  getGlass[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                    //}
                    //else
                    //{
                    if (PortGetType == PortGetType.DESC)
                    {
                        getGlass = GlassInfos.OrderByDescending(o => o.Position).FirstOrDefault();
                        // Logger.InfoFormat("[TransferIndexToEQPCommand][LineMode!=OnlyA or MixRun]  PortGetType==DESC,GlassInfo[{0},{1}] ", info.CassetteSequenceNo, info.SlotSequenceNo);
                    }
                    else// if (PortGetType == PortGetType.ASC)
                    {
                        getGlass = GlassInfos.OrderBy(o => o.Position).FirstOrDefault();
                        //Logger.InfoFormat("[TransferIndexToEQPCommand][LineMode!=OnlyA or MixRun] PortGetType==ASC,GlassInfo[{0},{1}] ", info.CassetteSequenceNo, info.SlotSequenceNo);
                    }

                    //Logger.InfoFormat("[TransferIndexToEQPCommand][LineMode!=OnlyA or MixRun] CurrentGlasss  getGlass[{0},{1}];PortGetType:{2}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, PortGetType.ToString());
                    //}
                }

                #endregion
                var getGlassB = GlassInfos.FirstOrDefault(o => o.Position == getGlass.Position && o.SlotPosition != getGlass.SlotPosition && o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed);
                if (getGlassB != null)
                {
                    Logger.InfoFormat("[TransferIndexToEQPCommand] - LoadingPortList.OrderBy WaitingforProcessingTime -> {4},GlassA[{0},{1}];GlassB[{2},{3}],CurrentModel={5}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, currentLoadingPort.PortID, getRobotModel.ModelName);
                }
                else
                {
                    Logger.InfoFormat("[TransferIndexToEQPCommand] - LoadingPortList.OrderBy WaitingforProcessingTime -> {2},Glass[{0},{1}],CurrentModel={3}}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, currentLoadingPort.PortID, getRobotModel.ModelName);
                }



                #region 获取targetlist
                var targetlist = FindNextStage(getGlass);
                if (targetlist.Count() == 0)
                {
                    Logger.InfoFormat("[TransferIndexToEQPCommand] - Check Targetlist NG.Targetlist.Count()=0");
                    logInfo = string.Format("[TransferPort Check]   NG  Targetlist.Count()=0");

                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                // Logger.InfoFormat("[TransferIndexToEQPCommand] Check targetlist OK; targetlist.Count:{0}", targetlist.Count());
                #endregion

                //var target = targetlist.FirstOrDefault();
                foreach (var target in targetlist)
                {
                    #region 获取targetmodel
                    Unit targetUnit = null;
                    RobotModel targetmode = GetCurrentModel(target.ModelPosition);
                    targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetmode.UnitID);
                    if (targetUnit == null)
                    {
                        Logger.InfoFormat("[TransferIndexToEQPCommand] - Check TartgetUnit NG.Glass:[{0},{1}] Target ModelPosition={2},TargetUnit is null", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, target.ModelPosition);
                        logInfo = string.Format("[TransferPort Check]    NG  Glass:[{0},{1}] Target ModelPosition={2},TargetUnit is null", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, target.ModelPosition);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }
                    #endregion

                    // transfer 应该是用取的mode的getArm还是放的mode的putArm————只看取
                    #region 获取取片手臂
                    RobotHand getRobot = RobotHand.Error;
                    //if (HostInfo.Current.EQPID.Contains("A1DET400"))
                    //{
                    //    getRobot = GetRobot(getRobotModel, getRobot, targetmode);
                    //}
                    //else
                    //{
                    getRobot = GetRobot(getRobotModel, getRobot);
                    //}
                    if (getRobot == RobotHand.Error)
                    {
                        Logger.InfoFormat("[TransferIndexToEQPCommand] - Check RobotHand NG.TargetModelName={0}", targetmode.ModelName);
                        logInfo = string.Format("[TransferPort Check]    NG  Check RobotHand NG,TargetModelName={0}", targetmode.ModelName);

                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }
                    if (getRobot == RobotHand.AllArm)
                    {
                        getRobot = RobotHand.LowHand;
                    }
                    //Logger.InfoFormat("[TransferIndexToEQPCommand] Check RobotHand OK; RobotHand:{0} ", getRobot.ToString());
                    #endregion

                    Logger.InfoFormat("[TransferIndexToEQPCommand] - Check TartgetModel OK.TartgetModelName={0}; Check Get Arm({1}) ", targetmode.ModelName, getRobot.ToString());

                    #region 校验targetmode.TransferEnable
                    if (!targetmode.TransferEnable)
                    {
                        Logger.InfoFormat("[TransferIndexToEQPCommand] - Check TransferEnable NG.TartgetModelName={0},TransferEnable is false", targetmode.ModelName);
                        logInfo = string.Format("[TransferPort Check]   NG  TartgetModelName={0},TransferEnable is false", targetmode.ModelName);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }

                    #endregion

                    //  Unit targetUnit = LineInfo.EQPInfo.Units.FirstOrDefault(o => o.UnitName == target.PathConfigure.TargetPathName);
                    #region 校验DownLink
                    Linksignal downLinksignal = StageLinksignalList.Find(o => o.UnitName == targetUnit.UnitName && o.LinkName == targetmode.DownLinkName);
                    if (downLinksignal == null)
                    {
                        Logger.InfoFormat("[TransferIndexToEQPCommand] - Get Condition NG.TargetModelName={1},LinkName={0},DownLinksignal is null", targetmode.DownLinkName, targetmode.ModelName);
                        logInfo = string.Format("[TransferPort Check]   NG  Condition NG,TargetModelName={1},LinkName={0},DownLinksignal is null", targetmode.DownLinkName, targetmode.ModelName);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }
                    //if (Convert.ToInt32(targetUnit.UnitStatus) == (int)EquipmentStatus.DOWN)
                    //{
                    //    Logger.InfoFormat("[PROCESS PUT ] Equipment Status is Down for Unit {0}, Glass {1} ", target.ModelPosition, glassInfo.GlassID);
                    //    return;
                    //}
                    if (!CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr))//校验下游收片信号
                    {
                        //Logger.InfoFormat("[TransferIndexToEQPCommand] - Check downLinksignal LinkStatus NG;downLinksignal:{0} ", downLinksignal.LinkName);
                        logInfo = string.Format("[TransferPort Check]   NG  Check {0} Condition NG", downLinksignal.LinkName);
                        logStr.Append(logInfo);
                        logStr.AppendLine();
                        continue;
                    }
                    #endregion
                    //Logger.InfoFormat("[TransferIndexToEQPCommand] DownstreamLinkSignal Check OK; LinkName:{0}", downLinksignal.LinkName);
                    //var model = targetUnit.RobotModelList.FirstOrDefault(o => o.DownLinkName == downLinksignal.LinkName);
                    //if (model != null)
                    //{
                    //Logger.InfoFormat("[TransferIndexToEQPCommand] Check RobotModel OK; RobotModel:{0} ", targetmode.ModelName);
                    #region 获取SlotPostion
                    int SlotPostion = 0;
                    //if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                    //{
                    //    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                    //    {
                    //        SlotPostion = getGlass.SlotPosition;
                    //        //Logger.InfoFormat("[TransferIndexToEQPCommand] LineMode == LineMode.OnlyA;SlotPostion:{0} ", SlotPostion);
                    //    }
                    //    else
                    //    {
                    //        //var glassCount = all.Where(o => o.Position == glassInfo.Position).Count();
                    //        var glassCount = GlassInfos.Where(o => o.Position == getGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                    //        SlotPostion = glassCount == 1 ? getGlass.SlotPosition : 99;
                    //        //Logger.InfoFormat("[TransferIndexToEQPCommand] LineMode != LineMode.OnlyA;SlotPostion:{0} ", SlotPostion);
                    //    }
                    //}
                    //else
                    {
                        var glassCount = GlassInfos.Where(o => o.Position == getGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                        SlotPostion = glassCount == 1 ? getGlass.SlotPosition : 99;
                    }
                    if (string.IsNullOrEmpty(getGlass.ModePath))
                    {
                        Logger.InfoFormat("[TransferIndexToEQPCommand] - Check ModePath NG.Glass[{0},{1}] ModePath is null", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                        continue;
                    }

                    #endregion

                    #region 生成命令
                    //target.PathConfigure.OutPriority; port口glass 的out
                    // Logger.InfoFormat("[TransferIndexToEQPCommand]targetmode:{0};targetmode.TransferPriority:{1}", targetmode.ModelName, targetmode.TransferPriority);
                    var cmd = factory.CreateTransferIndexToEQPCommand(getRobot, currentLoadingPort, ModelPosition, getGlass, targetmode, SlotPostion);

                    //GlassInfo GlassA = null;
                    //GlassInfo GlassB = null;
                    //GlassInfo Glass = null;
                    //GetGlassByGlass(getGlass, ref GlassA, ref GlassB, ref Glass, "TransferIndexToEQPCommand");
                    //cmd.GetGlass = Glass;
                    //cmd.GetGlassA = GlassA;
                    //cmd.GetGlassB = GlassB;

                    cmd.STGetPosition1string = getRobotModel.ModelName;
                    cmd.STPutPosition1string = targetmode.ModelName;
                    AddCommand(cmd);
                    Logger.InfoFormat("[TransferIndexToEQPCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6}) -> ({7},{8}), Position({9}) ",
                                    cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1, cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STPutSlotPostion1);
                    logInfo = string.Format("[TransferPort Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6}) -> ({7},{8}), Position({9}) ",
                                    cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1, cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STPutSlotPostion1);

                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    #endregion
                }


                // Logger.InfoFormat("[TransferIndexToEQPCommand] end");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }

        private void GetGlassByGlass(GlassInfo glassSlot1, GlassInfo glassSlot1B, GlassInfo glassSlot2, GlassInfo glassSlot2B, ref GlassInfo GlassA, ref GlassInfo GlassB, ref GlassInfo GlassC, ref GlassInfo GlassD, string functionName)
        {
            //if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
            //{
            //    GlassA = glassInfo;
            //    Logger.InfoFormat("[{2}][LineMode == LineMode.OnlyA] - GlassA [{0},{1}]", GlassA.CassetteSequenceNo, GlassA.SlotSequenceNo, functionName);
            //}
            //else
            //{
            //另一片
            //var glass = all.FirstOrDefault(o => o.Position == glassInfo.Position && o.SlotPosition != glassInfo.SlotPosition);

            //判断出 前后片
            if (glassSlot1 != null)
            {
                switch (glassSlot1.SlotPosition)
                {
                    case 1:
                        GlassA = glassSlot1;
                        GlassB = glassSlot1B;
                        break;
                    case 2:
                        GlassA = glassSlot1B;
                        GlassB = glassSlot1;
                        break;
                }
            }
            else if (glassSlot1B != null)
            {
                switch (glassSlot1B.SlotPosition)
                {
                    case 1:
                        GlassA = glassSlot1B;
                        GlassB = glassSlot1;
                        break;
                    case 2:
                        GlassA = glassSlot1;
                        GlassB = glassSlot1B;
                        break;
                }
            }

            if (glassSlot2 != null)
            {
                switch (glassSlot2.SlotPosition)
                {
                    case 1:
                        GlassC = glassSlot2;
                        GlassD = glassSlot2B;
                        break;
                    case 2:
                        GlassC = glassSlot2B;
                        GlassD = glassSlot2;
                        break;
                }
            }
            else if (glassSlot2B != null)
            {
                switch (glassSlot2B.SlotPosition)
                {
                    case 1:
                        GlassC = glassSlot2B;
                        GlassD = glassSlot2;
                        break;
                    case 2:
                        GlassC = glassSlot2;
                        GlassD = glassSlot2B;
                        break;
                }
            }
            //}
        }

        private void TransferEQPToIndexOrEQPCommand(ref StringBuilder logStr)
        {
            try
            {
                string logInfo = string.Format("");
                //判断Robot手臂上是否有玻璃
                #region 校验手臂资料
                if (Robot.UpperExistOn || Robot.LowerExistOn)
                {
                    Logger.Info("[TransferEQPToIndexOrEQPCommand] - Check Arm NG.Arm(Upper) or Arm(Lower) have glass ");
                    logInfo = string.Format("[  TransferEQ Check]   NG  Arm(Upper) or Arm(Lower) have glass ");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                #endregion

                foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
                {
                    if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
                    {
                        // List<GlassInfo> glassInfoList = new List<GlassInfo> ();
                        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] UnitName:{0}", unitItem.UnitName);
                        foreach (var getRobotModel in unitItem.RobotModelList)
                        {
                            //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - UnitName:{0}; getRobotModel:{1}", unitItem.UnitName, getRobotModel.ModelName);
                            #region 校验Trouble信号
                            Linksignal upLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == getRobotModel.UPLinkName);
                            if (CheckUpLinkStatus(upLinksignal, unitItem, ref logStr))
                            {
                                //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
                            }
                            else
                            {
                                //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);
                                //logInfo = string.Format("[    TransferEQ Check]   NG  UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);

                                //logStr.Append(logInfo);
                                //logStr.AppendLine();
                                continue;
                            }
                            #endregion

                            //eqp上的glass
                            //#region 获取GetGlass
                            //GlassInfo getGlass = null;
                            //switch (getRobotModel.UsedJobBlockNo)
                            //{
                            //    case 0:
                            //        if (getRobotModel.GlassA != null)
                            //        {
                            //            getGlass = getRobotModel.GlassA;
                            //            // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][UsedJobBlockNo==0] GlassA;getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //    case 1:
                            //        if (getRobotModel.GlassA != null)
                            //        {
                            //            getGlass = getRobotModel.GlassA;
                            //            //  Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][UsedJobBlockNo==1] GlassA;getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //    case 2:
                            //        if (getRobotModel.GlassB != null)
                            //        {
                            //            getGlass = getRobotModel.GlassB;
                            //            // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][UsedJobBlockNo==2] GlassB;getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //    case 3:
                            //        if (getRobotModel.GlassA != null)
                            //        {
                            //            getGlass = getRobotModel.GlassA;
                            //            //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][UsedJobBlockNo==3] GlassA;glassInfo[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //        }
                            //        break;
                            //}
                            //if (getGlass == null)
                            //{
                            //    Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Unit GlassInfo==null;UnitName:{0};RobotModel:{1}; TransferEQPToIndexOrEQPCommand NG", unitItem.UnitName, getRobotModel.ModelName);
                            //    logInfo = string.Format("[  TransferEQ Check]   NG  {0} GlassInfo==null; TransferEQPToIndexOrEQPCommand NG", getRobotModel.ModelName);

                            //    logStr.Append(logInfo);
                            //    logStr.AppendLine();
                            //    continue;
                            //}
                            //#endregion
                            if (getRobotModel.GlassA == null && getRobotModel.GlassB == null && getRobotModel.GlassC == null && getRobotModel.GlassD == null)
                            {
                                Logger.InfoFormat("[CreateProcessGetCommand][getGlass==null] - Check Data NG.Glass is null");
                                logInfo = string.Format("[  GetCommand Check]   NG  Glass is null");
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            //getGlass[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Link Check OK,ModelName={1},LinkName={0}", getRobotModel.UPLinkName, getRobotModel.ModelName);

                            if (!getRobotModel.TransferEnable)
                            {
                                Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check Condition NG.ModelName={0},TransferEnable is false ", getRobotModel.ModelName);
                                logInfo = string.Format("[  TransferEQPToIndexOrEQPCommand Check]   NG  ModelName={0},TransferEnable is false", getRobotModel.ModelName);
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            //#region 获取手臂资料
                            //GlassInfo putGlass = null;
                            //if (Robot.LowerExistOn)
                            //{
                            //    if (Robot.LowHandGlass1 != null)
                            //    {
                            //        putGlass = Robot.LowHandGlass1;
                            //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.LowHandGlass1 != null] putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            //    }
                            //    else if (Robot.LowHandGlass2 != null)
                            //    {
                            //        putGlass = Robot.LowHandGlass2;
                            //        //  Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.LowHandGlass2 != null] putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            //    }
                            //    else
                            //    {
                            //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.LowerExistOn][Robot.LowHandGlass1== null] [Robot.LowHandGlass2== null]; ");
                            //    }
                            //}
                            //else if (Robot.UpperExistOn)
                            //{
                            //    if (Robot.UpHandGlass1 != null)
                            //    {
                            //        putGlass = Robot.UpHandGlass1;
                            //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.UpHandGlass1 != null] glassInfo[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            //    }
                            //    else if (Robot.UpHandGlass2 != null)
                            //    {
                            //        putGlass = Robot.UpHandGlass2;
                            //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.UpHandGlass2 != null] glassInfo[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                            //    }
                            //    else
                            //    {
                            //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.UpperExistOn][Robot.UpHandGlass1== null] [Robot.UpHandGlass2== null]; ");
                            //    }
                            //}
                            //else
                            //{
                            //    // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][Robot.LowerExistOn==false][Robot.UpperExistOn==false]  ");
                            //}
                            //#endregion

                            //#region 校验手臂资料的modelposition
                            //if (putGlass != null)
                            //{
                            //    // Logger.Info(string.Format("[TransferEQPToIndexOrEQPCommand] Check putGlass OK; putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo));
                            //    //eqp上galss的地址和手臂上glass的地址一样  不做transfer
                            //    var putGlassModel = GetCurrentModel(putGlass.ModelPosition);
                            //    //if (getGlass.ModelPosition == putGlass.ModelPosition)
                            //    if (getRobotModel.GroupName == putGlassModel.GroupName)
                            //    {
                            //        Logger.Info(string.Format("[TransferEQPToIndexOrEQPCommand] - Check Action NG.Arm Glass[{2},{3}] GroupName == Glass[{0},{1}] GroupName", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, putGlass.ModelPosition));
                            //        continue;
                            //    }
                            //    else
                            //    {
                            //        // Logger.Info(string.Format("[TransferEQPToIndexOrEQPCommand] Check Glass ModelPosition OK; Hand Glass ModelPosition !=Port Glass ModelPosition;getGlassInfo:[{0},{1}];putGlassInfo:[{2},{3}];getModelPosition:{4};putModelPosition:{5}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, getGlass.ModelPosition, putGlass.ModelPosition));
                            //    }
                            //}
                            //else
                            //{
                            //    // Logger.Info(string.Format("[TransferEQPToIndexOrEQPCommand] Check putJobData NG; putJobData==null"));
                            //}
                            //#endregion

                            //#region 校验unit inlineMode
                            ////if (unitItem.CommandType == 1)
                            ////{
                            ////    if (!unitItem.UpstreamInlineMode || unitItem.LoadingStop)
                            ////    {
                            ////        Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check UpstreamInlineMode and LoadingStop NG; unitItem.UpstreamInlineMode=false; unitItem.LoadingStop=true;Unit:{0}; TransferEQPToIndexOrEQPCommand NG", unitItem.UnitName);
                            ////        logInfo = string.Format("[  TransferEQ Check]   NG  Check UpstreamInlineMode = false Or LoadingStop = true;Unit:{0}; TransferEQPToIndexOrEQPCommand NG", unitItem.UnitName);

                            ////        logStr.Append(logInfo);
                            ////        logStr.AppendLine();
                            ////        continue;
                            ////    }
                            ////    Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check UpstreamInlineMode and LoadingStop OK; unitItem.UpstreamInlineMode=true; unitItem.LoadingStop=false");
                            ////}
                            //if (string.IsNullOrEmpty(getGlass.ModePath))
                            //{
                            //    Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check ModePath NG.Glass[{0},{1}] ModePath is null", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                            //    continue;
                            //}
                            //#endregion


                            #region 获取取片手臂
                            RobotHand getRobot = RobotHand.Error;
                            getRobot = GetRobot(getRobotModel, getRobot);// 根据手臂是否有玻璃，以及上游设备的上下层是否有玻璃，获取可用的手臂
                            if (getRobot == RobotHand.Error)
                            {
                                //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check Arm NG.RobotArm({0})==RobotHand.Error", getRobotModel.ModelName);
                                Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check GetRobot.RobotHand NG; RobotHand.Error ");
                                logInfo = string.Format("[    TransferEQ Check]   NG Check GetRobot.RobotHand NG; RobotHand.Error");
                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            if (getRobot == RobotHand.AllArm)
                            {
                                //默认下手臂
                                getRobot = RobotHand.LowHand;
                            }
                            #endregion

                            //#region 


                            #region 获取targetStage列表
                            int EQPSendSlotNo = 0;//取哪一层
                            GlassInfo targetGlass = null;
                            if (getRobotModel.GlassA != null)
                            {
                                targetGlass = getRobotModel.GlassA;
                                EQPSendSlotNo = 1;
                            }
                            else if (getRobotModel.GlassB != null)
                            {
                                targetGlass = getRobotModel.GlassB;
                                EQPSendSlotNo = 1;
                            }
                            else if (getRobotModel.GlassC != null)
                            {
                                targetGlass = getRobotModel.GlassC;
                                EQPSendSlotNo = 2;
                            }
                            else if (getRobotModel.GlassD != null)
                            {
                                targetGlass = getRobotModel.GlassD;
                                EQPSendSlotNo = 2;
                            }
                            //这里先用可取的第一片
                            var targetStageList = FindNextStage(targetGlass);//根据上一站的unit或者eqp获取下一站的站点
                            if (targetStageList.Count == 0)
                            {
                                Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check TargetStageList NG.Glass[{0},{1}],RobotArm({2}),ModelPath={3},TargetStageList.Count=0", getRobotModel.GlassA.CassetteSequenceNo, getRobotModel.GlassA.SlotSequenceNo, getRobot, getRobotModel.GlassA.ModePath);
                                logInfo = string.Format("[  TransferEQ Check]   NG  Check Glass[{0},{1}],RobotArm({2}),ModelPath={3},TargetStageList.Count=0", getRobotModel.GlassA.CassetteSequenceNo, getRobotModel.GlassA.SlotSequenceNo, getRobot, getRobotModel.GlassA.ModePath);

                                logStr.Append(logInfo);
                                logStr.AppendLine();
                                continue;
                            }
                            //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check TargetStageList OK; targetStageList.Count:{4};GlassInfo:[{0},{1}];RobotHand:{2};ModelPath:{3}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getRobot, getGlass.ModePath, targetStageList.Count);
                            #endregion

                            //先判断取片的逻辑  后面判断放的逻辑时再决定怎么取
                            RobotHand STArmNo1 = RobotHand.LowHand;
                            int STGetPosition1 = -1;
                            int STGetSlotNo1 = 0;
                            int STGetSlotPostion1 = 0;
                            GlassInfo GetGlassA = null;
                            GlassInfo GetGlassB = null;
                            //RobotHand NDArmNo2 = RobotHand.LowHand;
                            //int NDGetPosition2 = -1;
                            //int NDGetSlotNo2 = 0;
                            //int NDGetSlotPostion2 = 0;
                            //GlassInfo GetGlassC = null;
                            //GlassInfo GetGlassD = null;
                            //if (getRobot == RobotHand.AllArm)//两个手臂都有空
                            //{
                            //    if (getRobotModel.EQPSendSlotNoA != 0 && getRobotModel.EQPSendSlotNoB != 0)//有两层
                            //    {
                            //        //判断两片是否同等级
                            //        var issamegrade1 = CheckTwoGlassGrade(getRobotModel.EQPID, getRobotModel.GlassA, getRobotModel.GlassB);
                            //        var issamegrade2 = CheckTwoGlassGrade(getRobotModel.EQPID, getRobotModel.GlassC, getRobotModel.GlassD);

                            //        //下手臂取下层
                            //        STArmNo1 = RobotHand.LowHand;
                            //        STGetPosition1 = getRobotModel.ModelPosition;
                            //        STGetSlotNo1 = getRobotModel.EQPSendSlotNoA;
                            //        STGetSlotPostion1 = !issamegrade1 ? (getRobotModel.GlassA != null ? 1 : 2) : ((getRobotModel.GlassA != null && getRobotModel.GlassB != null) ? 99 : (getRobotModel.GlassA != null ? 1 : 2));
                            //        if (getRobotModel.GlassA != null)
                            //            GetGlassA = getRobotModel.GlassA;
                            //        if (issamegrade1 && getRobotModel.GlassB != null)
                            //            GetGlassB = getRobotModel.GlassB;

                            //        //上手臂取上层
                            //        NDArmNo2 = RobotHand.UpHand;
                            //        NDGetPosition2 = getRobotModel.ModelPosition;
                            //        NDGetSlotNo2 = getRobotModel.EQPSendSlotNoB;
                            //        NDGetSlotPostion2 = !issamegrade2 ? (getRobotModel.GlassC != null ? 1 : 2) : ((getRobotModel.GlassC != null && getRobotModel.GlassD != null) ? 99 : (getRobotModel.GlassC != null ? 1 : 2));
                            //        if (getRobotModel.GlassC != null)
                            //            GetGlassC = getRobotModel.GlassC;
                            //        if (issamegrade1 && getRobotModel.GlassD != null)
                            //            GetGlassD = getRobotModel.GlassD;
                            //    }
                            //    else//只有一层
                            //    {
                            //        //优先下手臂去取
                            //        if (getRobotModel.EQPSendSlotNoA != 0)
                            //        {
                            //            //判断两片是否同等级
                            //            var issamegrade1 = CheckTwoGlassGrade(getRobotModel.EQPID, getRobotModel.GlassA, getRobotModel.GlassB);

                            //            STArmNo1 = RobotHand.LowHand;
                            //            STGetPosition1 = getRobotModel.ModelPosition;
                            //            STGetSlotNo1 = getRobotModel.EQPSendSlotNoA;
                            //            STGetSlotPostion1 = !issamegrade1 ? (getRobotModel.GlassA != null ? 1 : 2) : ((getRobotModel.GlassA != null && getRobotModel.GlassB != null) ? 99 : (getRobotModel.GlassA != null ? 1 : 2));
                            //            if (getRobotModel.GlassA != null)
                            //                GetGlassA = getRobotModel.GlassA;
                            //            if (issamegrade1 && getRobotModel.GlassB != null)
                            //                GetGlassB = getRobotModel.GlassB;
                            //        }
                            //        else if (getRobotModel.EQPSendSlotNoB != 0)
                            //        {
                            //            //判断两片是否同等级
                            //            var issamegrade1 = CheckTwoGlassGrade(getRobotModel.EQPID, getRobotModel.GlassC, getRobotModel.GlassD);

                            //            STArmNo1 = RobotHand.LowHand;
                            //            STGetPosition1 = getRobotModel.ModelPosition;
                            //            STGetSlotNo1 = getRobotModel.EQPSendSlotNoB;
                            //            STGetSlotPostion1 = !issamegrade1 ? (getRobotModel.GlassC != null ? 1 : 2) : ((getRobotModel.GlassC != null && getRobotModel.GlassD != null) ? 99 : (getRobotModel.GlassC != null ? 1 : 2));
                            //            if (getRobotModel.GlassC != null)
                            //                GetGlassA = getRobotModel.GlassC;
                            //            if (issamegrade1 && getRobotModel.GlassD != null)
                            //                GetGlassB = getRobotModel.GlassD;
                            //        }
                            //    }
                            //}
                            //else
                            {
                                if (EQPSendSlotNo == 1)
                                {
                                    //判断两片是否同等级
                                    var issamegrade1 = CheckTwoGlassGrade(getRobotModel.EQPID, getRobotModel.GlassA, getRobotModel.GlassB);

                                    STArmNo1 = getRobot;
                                    STGetPosition1 = getRobotModel.ModelPosition;
                                    STGetSlotNo1 = getRobotModel.EQPSendSlotNoA;
                                    STGetSlotPostion1 = !issamegrade1 ? (getRobotModel.GlassA != null ? 1 : 2) : ((getRobotModel.GlassA != null && getRobotModel.GlassB != null) ? 99 : (getRobotModel.GlassA != null ? 1 : 2));
                                    if (getRobotModel.GlassA != null)
                                        GetGlassA = getRobotModel.GlassA;
                                    if (issamegrade1 && getRobotModel.GlassB != null)
                                        GetGlassB = getRobotModel.GlassB;
                                }
                                else if (EQPSendSlotNo == 2)
                                {
                                    //判断两片是否同等级
                                    var issamegrade1 = CheckTwoGlassGrade(getRobotModel.EQPID, getRobotModel.GlassC, getRobotModel.GlassD);

                                    STArmNo1 = getRobot;
                                    STGetPosition1 = getRobotModel.ModelPosition;
                                    STGetSlotNo1 = getRobotModel.EQPSendSlotNoB;
                                    STGetSlotPostion1 = !issamegrade1 ? (getRobotModel.GlassC != null ? 1 : 2) : ((getRobotModel.GlassC != null && getRobotModel.GlassD != null) ? 99 : (getRobotModel.GlassC != null ? 1 : 2));
                                    if (getRobotModel.GlassC != null)
                                        GetGlassA = getRobotModel.GlassC;
                                    if (issamegrade1 && getRobotModel.GlassD != null)
                                        GetGlassB = getRobotModel.GlassD;
                                }
                            }

                            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check ProcessGetGlass. STArmNo1({0}) STGetSlotNo1({1}) STGetSlotPostion1({2}))", STArmNo1.ToString(), STGetSlotNo1, STGetSlotPostion1);// NDArmNo2({3}) NDGetSlotNo2({4}) NDGetSlotPostion2({5}    , NDArmNo2.ToString(), NDGetSlotNo2, NDGetSlotPostion2
                            logInfo = string.Format("[  TransferEQ Check]   Check ProcessGetGlass. STArmNo1({0}) STGetSlotNo1({1}) STGetSlotPostion1({2})", STArmNo1.ToString(), STGetSlotNo1, STGetSlotPostion1);// NDArmNo2({3}) NDGetSlotNo2({4}) NDGetSlotPostion2({5})      , NDArmNo2.ToString(), NDGetSlotNo2, NDGetSlotPostion2

                            logStr.Append(logInfo);
                            logStr.AppendLine();

                            //判断放到哪个port去
                            int STPutPosition1 = -1;
                            int STPutSlotNo1 = 0;
                            int STPutSlotPostion1 = 0;
                            //int NDPutPosition2 = -1;
                            //int NDPutSlotNo2 = 0;
                            //int NDPutSlotPostion2 = 0;

                            //Logger.Info("+++FindExchangeCommand Step 4, Start Check NextEquipment Status");
                            foreach (var targetStage in targetStageList)
                            {
                                //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand]getRobotModel:{0}; targetStage:{1}", getRobotModel.ModelName, targetStage.ModelPosition);
                                if (!string.IsNullOrEmpty(targetStage.UnitName))
                                {
                                    if (targetStage.Type == EnumUnitType.Robot)
                                    {
                                        #region targetStage is Robot
                                        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check Target Model.TargetModelName={0}; targetStage:{1}", getRobotModel.ModelName, targetStage.ModelPosition);
                                        #region 校验 Put Stage
                                        //if (getGlass.SlotSatus != EnumGlassSlotStatus.Processing)
                                        //{
                                        //    Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Condition NG.Glass[{0},{1}] SlotSatus!= Processing", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                                        //    logInfo = string.Format("[  TransferEQ Check]   NG  Condition NG.Glass[{0},{1}] SlotSatus!= Processing", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                                        //    logStr.Append(logInfo);
                                        //    logStr.AppendLine();
                                        //    continue;
                                        //}
                                        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] glassInfo.SlotSatus== EnumGlassSlotStatus.Processing;glassInfo[{0},{1}]", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
                                        var targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
                                        if (targetUnit == null)
                                        {
                                            Logger.InfoFormat($"[TransferEQPToIndexOrEQPCommand] - Check TargetUnit NG.TargetUnit {targetStage.UnitName} is null");
                                            logInfo = string.Format($"[  TransferEQ Check]   NG  Check TargetUnit NG.TargetUnit {targetStage.UnitName} is null ");
                                            logStr.Append(logInfo);
                                            logStr.AppendLine();
                                            continue;
                                        }
                                        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check targetUnit OK; targetUnit != null, targetUnit:{0} ", targetUnit.UnitName);
                                        var putModel = targetUnit.RobotModelList.FirstOrDefault(o => o.ModelPosition == targetStage.ModelPosition);
                                        if (putModel == null)
                                        {
                                            Logger.InfoFormat($"[TransferEQPToIndexOrEQPCommand] - Check Robotmodel NG.Robotmodel ModelPosition {targetStage.ModelPosition} is null");
                                            logInfo = string.Format($"[  TransferEQ Check]   NG  Check Robotmodel NG.Robotmodel ModelPosition {targetStage.ModelPosition} is null ");
                                            logStr.Append(logInfo);
                                            logStr.AppendLine();
                                            continue;
                                        }
                                        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check robotmodel OK;  robotmodel != null, robotmodel:{0} ", putModel.ModelName);
                                        //if (putModel.ModelPosition != targetStage.ModelPosition)
                                        //{
                                        //    //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][robotmodel.ModelPosition != targetStage.ModelPosition] Check targetStage.ModelPosition NG;  targetStage.ModelPosition:{0} ", targetStage.ModelPosition);
                                        //    //logInfo = string.Format("[  TransferEQ Check]   NG  Robotmodel.ModelPosition != TargetStage.ModelPosition, TargetStage.ModelPosition:{0} ", targetStage.ModelPosition);
                                        //    //logStr.Append(logInfo);
                                        //    //logStr.AppendLine();
                                        //    continue;
                                        //}
                                        #endregion
                                        Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check Action OK.],ModelName={0},TargetModelName={1}", getRobotModel.ModelName, putModel.ModelName);
                                        //#region 获取SlotPostion
                                        //int SlotPostion = 0;
                                        ////if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
                                        ////{
                                        ////    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                                        ////    {
                                        ////        SlotPostion = getGlass.SlotPosition;
                                        ////        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] LineMode == LineMode.OnlyA, SlotPostion:{0} ", SlotPostion);
                                        ////    }
                                        ////    else
                                        ////    {
                                        ////        var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlass.PortID);
                                        ////        if (port != null)
                                        ////        {
                                        ////            //var glassCount = port.GlassInfos.Where(o => o.Position == glassInfo.Position).Count();
                                        ////            var glassCount = port.GlassInfos.Where(o => o.Position == getGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
                                        ////            SlotPostion = glassCount == 1 ? getGlass.SlotPosition : 99;
                                        ////            // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] LineMode != LineMode.OnlyA, SlotPostion:{0} ", SlotPostion);
                                        ////        }
                                        ////    }
                                        ////}
                                        ////else
                                        //{
                                        //    SlotPostion = 0;
                                        //}
                                        //#endregion

                                        var unloadport = HostInfo.Current.PortList.FirstOrDefault(c => c.EQPID == HostInfo.Current.EQPInfo.EQPID && c.UnitName == targetStage.UnitName && c.PortID == putModel.PortID && c.PortStatus == 3 && (c.CassetteInfo.CassetteStatus == EnumCarrierStatus.WaitingforProcessing || c.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing));
                                        if (unloadport == null)
                                        {
                                            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check unloadport NG.{0} PortStatus not inuse", putModel.PortID);
                                            continue;
                                        }

                                        //上面已经计算出 上下手臂各自能取什么片
                                        //这里就按上下手臂各自判断能放到哪
                                        if (STGetSlotNo1 != 0)//手臂一能取到片
                                        {
                                            bool portcanput = false;
                                            //用第一片来判断即可
                                            #region 检查PortGrade
                                            GlassInfo putGlass = null;
                                            if (GetGlassA != null)
                                                putGlass = GetGlassA;
                                            else if (GetGlassB != null)
                                                putGlass = GetGlassB;

                                            if (putGlass != null)
                                            {
                                                var GlassGradeCode = putGlass.GlassGradeCode;
                                                var portGradeGroups = HostInfo.Current.PortGradeGroupList.FirstOrDefault(o => o.Key == targetUnit.EQPID).Value;
                                                if (portGradeGroups != null && portGradeGroups.Count() > 0)
                                                {
                                                    var jobPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassGradeCode) && o.enabled == 0);
                                                    if (jobPortGradeGroups != null && jobPortGradeGroups.Count() > 0)
                                                    {
                                                        if (!string.IsNullOrEmpty(unloadport.PortGrade))//如果当前PortGradeGroup不为空，则检查是否满足GlassGradeCode
                                                        {
                                                            var jobPortGradeGroup = jobPortGradeGroups.FirstOrDefault(o => o.portgradegroup == unloadport.PortGrade);
                                                            if (jobPortGradeGroup != null)//当前Port满足GlassGradeCode 可以放片
                                                            {
                                                                portcanput = true;
                                                            }
                                                            else//当前Port不满足GlassGradeCode 不放片
                                                            {
                                                                Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .Not Mismatch.GlassGradeCode is {4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                                                continue;
                                                            }
                                                        }
                                                        else//如果当前PortGradeGroup为空，则需要查询出是否有其他Port满足，如果没有，则使用当前Port，并赋值PortGradeGroup
                                                        {
                                                            bool checkResult = false;
                                                            PortInfo port = null;
                                                            foreach (var jobPGG in jobPortGradeGroups)
                                                            {
                                                                port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortGrade == jobPGG.portgradegroup);
                                                                if (port != null)
                                                                {
                                                                    checkResult = true;
                                                                    break;
                                                                }
                                                            }
                                                            if (checkResult)//查找到其他满足条件的Port，则当前Port不处理
                                                            {
                                                                Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}], Other Port Match. TargetUnit is {2},PortGradeGroup is {3} .Not Mismatch.GlassGradeCode is {4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                                                continue;
                                                            }
                                                            else//未查找到满足条件的Port，则使用当前的Port，因为当前的PortGradeGroup为空，代表未放置Panel
                                                            {
                                                                // 赋值优先级最高的可用的PortGradeGroup
                                                                var priority = jobPortGradeGroups.Where(o => o.enabled == 0).Max(o => o.priority);
                                                                unloadport.PortGrade = jobPortGradeGroups.FirstOrDefault(o => o.priority == priority).portgradegroup;
                                                                IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
                                                                dbService.UpdatePortInfo(unloadport);
                                                                Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Update Port {0} PortGrade={1}", unloadport.PortID, unloadport.PortGrade);
                                                                portcanput = true;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //没找到glass对应的group配置 不放
                                                        Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .GlassGradeCode is {4} Can not find BC PortGrade Config", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    //没找到glass对应的group配置 不放
                                                    Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .GlassGradeCode is {4} Can not find BC PortGrade Config", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                                    continue;
                                                }
                                            }
                                            #endregion

                                            if (portcanput)//port的等级对了，然后看能放几片 放到哪一层
                                            {
                                                int Position = 0;//放哪一层
                                                int SlotPostion = 0;//放前还是放后
                                                                    //找哪一层哪个位置能放
                                                                    //if (putModel.PortGetType == PortGetType.DESC)
                                                                    //{
                                                var descdata = unloadport.GlassInfos.OrderBy(o => o.Position).ToList();//最下面的层
                                                var lowslot = descdata.FirstOrDefault();
                                                if (descdata.Count(c => c.Position == lowslot.Position) > 1)//这一层满了 放下一层
                                                {
                                                    Position = lowslot.Position - 1;
                                                    SlotPostion = (STGetSlotPostion1 != 99) ? 2 : 99;//手臂只有一片，优先放后面
                                                }
                                                else//这一层没满
                                                {
                                                    if (STGetSlotPostion1 != 99)
                                                    {
                                                        Position = lowslot.Position - 1;//这里暂时不补片了
                                                        SlotPostion = 2;//手臂只有一片，放后面  因为不补片了 放下一层 默认放后面
                                                    }
                                                    else//手臂有两片 放不下 放下一层
                                                    {
                                                        Position = lowslot.Position - 1;
                                                        SlotPostion = 99;
                                                    }
                                                }
                                                if (Position <= 0)
                                                {
                                                    //Port没退 导致计算出负数
                                                    Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check Input Position NG.Glass[{0},{1}],TargetUnit is {2}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID);
                                                    continue;
                                                }
                                                //}
                                                //else
                                                //{
                                                //    var ascdata = unloadport.GlassInfos.OrderByDescending(o => o.Position).ToList();//最上面的层
                                                //    var upslot = ascdata.FirstOrDefault();
                                                //    if (ascdata.Count(c => c.Position == upslot.Position) > 1)//这一层满了 放上一层
                                                //    {
                                                //        Position = upslot.Position + 1;
                                                //        SlotPostion = (STGetSlotPostion1 != 99) ? 2 : 99;//手臂只有一片，优先放后面
                                                //    }
                                                //    else//这一层没满
                                                //    {
                                                //        if (STGetSlotPostion1 != 99)
                                                //        {
                                                //            Position = upslot.Position;
                                                //            SlotPostion = 1;//手臂只有一片，放前面
                                                //        }
                                                //        else//手臂有两片 放不下 放下一层
                                                //        {
                                                //            Position = upslot.Position + 1;
                                                //            SlotPostion = 99;
                                                //        }
                                                //    }
                                                //}

                                                if (Position != 0 && SlotPostion != 0)
                                                {
                                                    STPutPosition1 = putModel.ModelPosition;
                                                    STPutSlotNo1 = Position;
                                                    STPutSlotPostion1 = SlotPostion;
                                                    ////手臂一能transfer了 就不处理手臂二了
                                                    //NDGetPosition2 = -1;
                                                    //NDGetSlotNo2 = 0;
                                                    //NDGetSlotPostion2 = 0;
                                                    //GetGlassC = null;
                                                    //GetGlassD = null;
                                                }
                                            }
                                        }
                                        //if (NDGetSlotNo2 != 0)//手臂二能取到片
                                        //{
                                        //    bool portcanput = false;
                                        //    //用第一片来判断即可
                                        //    #region 检查PortGrade
                                        //    GlassInfo putGlass = null;
                                        //    if (GetGlassC != null)
                                        //        putGlass = GetGlassC;
                                        //    else if (GetGlassD != null)
                                        //        putGlass = GetGlassD;

                                        //    if (putGlass != null)
                                        //    {
                                        //        var GlassGradeCode = putGlass.GlassGradeCode;
                                        //        var portGradeGroups = HostInfo.Current.PortGradeGroupList.FirstOrDefault(o => o.Key == targetUnit.EQPID).Value;
                                        //        if (portGradeGroups != null && portGradeGroups.Count() > 0)
                                        //        {
                                        //            var jobPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassGradeCode) && o.enabled == 0);
                                        //            if (jobPortGradeGroups != null && jobPortGradeGroups.Count() > 0)
                                        //            {
                                        //                if (!string.IsNullOrEmpty(unloadport.PortGrade))//如果当前PortGradeGroup不为空，则检查是否满足GlassGradeCode
                                        //                {
                                        //                    var jobPortGradeGroup = jobPortGradeGroups.FirstOrDefault(o => o.portgradegroup == unloadport.PortGrade);
                                        //                    if (jobPortGradeGroup != null)//当前Port满足GlassGradeCode 可以放片
                                        //                    {
                                        //                        portcanput = true;
                                        //                    }
                                        //                    else//当前Port不满足GlassGradeCode 不放片
                                        //                    {
                                        //                        Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}],TargetUnit is {2},PortGradeGroup is {3} .Not Mismatch.GlassGradeCode is {4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                        //                    }
                                        //                }
                                        //                else//如果当前PortGradeGroup为空，则需要查询出是否有其他Port满足，如果没有，则使用当前Port，并赋值PortGradeGroup
                                        //                {
                                        //                    bool checkResult = false;
                                        //                    PortInfo port = null;
                                        //                    foreach (var jobPGG in jobPortGradeGroups)
                                        //                    {
                                        //                        port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortGrade == jobPGG.portgradegroup);
                                        //                        if (port != null)
                                        //                        {
                                        //                            checkResult = true;
                                        //                            break;
                                        //                        }
                                        //                    }
                                        //                    if (checkResult)//查找到其他满足条件的Port，则当前Port不处理
                                        //                    {
                                        //                        Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreatePortPutCommand] - Check PortGradeGroup NG.Glass[{0},{1}], Other Port Match. TargetUnit is {2},PortGradeGroup is {3} .Not Mismatch.GlassGradeCode is {4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, unloadport.UnitID + "-" + unloadport.PortID, unloadport.PortGrade, GlassGradeCode);
                                        //                    }
                                        //                    else//未查找到满足条件的Port，则使用当前的Port，因为当前的PortGradeGroup为空，代表未放置Panel
                                        //                    {
                                        //                        // 赋值优先级最高的可用的PortGradeGroup
                                        //                        var priority = jobPortGradeGroups.Where(o => o.enabled == 0).Max(o => o.priority);
                                        //                        unloadport.PortGrade = jobPortGradeGroups.FirstOrDefault(o => o.priority == priority).portgradegroup;
                                        //                        IDBService dbService = CommonContexts.ResolveInstance<IDBService>();
                                        //                        dbService.UpdatePortInfo(unloadport);
                                        //                        portcanput = true;
                                        //                    }
                                        //                }
                                        //            }
                                        //            else
                                        //            {
                                        //                //没找到glass对应的group配置 直接放
                                        //                portcanput = true;
                                        //            }
                                        //        }
                                        //        else
                                        //        {
                                        //            //没找到glass对应的group配置 直接放
                                        //            portcanput = true;
                                        //        }
                                        //    }
                                        //    #endregion

                                        //    if (portcanput)//port的等级对了，然后看能放几片 放到哪一层
                                        //    {
                                        //        int Position = 0;//放哪一层
                                        //        int SlotPostion = 0;//放前还是放后
                                        //                            //找哪一层哪个位置能放
                                        //                            //if (putModel.PortGetType == PortGetType.DESC)
                                        //                            //{
                                        //        var descdata = unloadport.GlassInfos.OrderBy(o => o.Position).ToList();//最下面的层
                                        //        var lowslot = descdata.FirstOrDefault();
                                        //        if (descdata.Count(c => c.Position == lowslot.Position) > 1)//这一层满了 放下一层
                                        //        {
                                        //            Position = lowslot.Position - 1;
                                        //            SlotPostion = (NDGetSlotPostion2 != 99) ? 2 : 99;//手臂只有一片，优先放后面
                                        //        }
                                        //        else//这一层没满
                                        //        {
                                        //            if (NDGetSlotPostion2 != 99)
                                        //            {
                                        //                Position = lowslot.Position;
                                        //                SlotPostion = 1;//手臂只有一片，放前面
                                        //            }
                                        //            else//手臂有两片 放不下 放下一层
                                        //            {
                                        //                Position = lowslot.Position - 1;
                                        //                SlotPostion = 99;
                                        //            }
                                        //        }
                                        //        //}
                                        //        //else
                                        //        //{
                                        //        //    var ascdata = unloadport.GlassInfos.OrderByDescending(o => o.Position).ToList();//最上面的层
                                        //        //    var upslot = ascdata.FirstOrDefault();
                                        //        //    if (ascdata.Count(c => c.Position == upslot.Position) > 1)//这一层满了 放上一层
                                        //        //    {
                                        //        //        Position = upslot.Position + 1;
                                        //        //        SlotPostion = (NDGetSlotPostion2 != 99) ? 2 : 99;//手臂只有一片，优先放后面
                                        //        //    }
                                        //        //    else//这一层没满
                                        //        //    {
                                        //        //        if (NDGetSlotPostion2 != 99)
                                        //        //        {
                                        //        //            Position = upslot.Position;
                                        //        //            SlotPostion = 1;//手臂只有一片，放前面
                                        //        //        }
                                        //        //        else//手臂有两片 放不下 放下一层
                                        //        //        {
                                        //        //            Position = upslot.Position + 1;
                                        //        //            SlotPostion = 99;
                                        //        //        }
                                        //        //    }
                                        //        //}

                                        //        if (Position != 0 && SlotPostion != 0)
                                        //        {
                                        //            NDPutPosition2 = putModel.ModelPosition;
                                        //            NDPutSlotNo2 = Position;
                                        //            NDPutSlotPostion2 = SlotPostion;
                                        //            ////手臂一能transfer了 就不处理手臂二了
                                        //            //STPutPosition1 = -1;
                                        //            //STPutSlotNo1 = 0;
                                        //            //STPutSlotPostion1 = 0;
                                        //        }
                                        //    }
                                        //}

                                        #region 生成命令
                                        //var cmd = factory.CreateTransferEQPToIndexCommand(robotHand, unitItem, glassInfo, targetStage.PathConfigure.InPriority, glassInfo.ModelPosition, robotmodel.ModelPosition, SlotPostion);
                                        var cmds = factory.CreateTransferEQPToIndexCommand(unitItem, getRobotModel, putModel, STArmNo1, STGetPosition1, STGetSlotNo1, STGetSlotPostion1, STPutPosition1, STPutSlotNo1, STPutSlotPostion1);//, NDArmNo2, NDGetPosition2, NDGetSlotNo2, NDGetSlotPostion2, NDPutPosition2, NDPutSlotNo2, NDPutSlotPostion2
                                        //cmd.GlassInfo = glassInfo;
                                        //GlassInfo GlassA = null;
                                        //GlassInfo GlassB = null;
                                        //GlassInfo Glass = null;
                                        //GetUnitModelGlass(getRobotModel, ref Glass, ref GlassA, ref GlassB, "TransferEQPToIndexOrEQPCommand");
                                        //cmd.GetGlass = Glass;
                                        for (int i = 0; i < cmds.Count; i++)
                                        {
                                            var cmd = cmds[i];
                                            if (cmd.STGetSlotNo1 == STGetSlotNo1)
                                            {
                                                cmd.GetGlassA = GetGlassA;
                                                cmd.GetGlassB = GetGlassB;
                                            }
                                            //else if (cmd.STGetSlotNo1 == NDGetSlotNo2)
                                            //{
                                            //    cmd.GetGlassA = GetGlassC;
                                            //    cmd.GetGlassB = GetGlassD;
                                            //}
                                            //var GetPosition1 = GetCurrentModel(getGlass.ModelPosition);
                                            cmd.STGetPosition1string = getRobotModel.ModelName;
                                            cmd.STPutPosition1string = putModel.ModelName;
                                            AddCommand(cmd);

                                            StringBuilder logstr = new StringBuilder();
                                            if (cmd.STGetSlotNo1 == STGetSlotNo1)
                                            {
                                                logstr.Append("STArmNo1:(" + STArmNo1.ToString() + ") ");
                                                logstr.Append("STGetPosition1:(" + STGetPosition1 + ") ");
                                                logstr.Append("STGetSlotNo1:(" + STGetSlotNo1 + ") ");
                                                logstr.Append("STGetSlotPostion1:(" + STGetSlotPostion1 + ") ");
                                                logstr.Append("STPutPosition1:(" + STPutPosition1 + ") ");
                                                logstr.Append("STPutSlotNo1:(" + STPutSlotNo1 + ") ");
                                                logstr.Append("STPutSlotPostion1:(" + STPutSlotPostion1 + ") ");
                                            }
                                            //else if (cmd.STGetSlotNo1 == NDGetSlotNo2)
                                            //{
                                            //    logstr.Append("STArmNo1:(" + NDArmNo2.ToString() + ") ");
                                            //    logstr.Append("STGetPosition1:(" + NDGetPosition2 + ") ");
                                            //    logstr.Append("STGetSlotNo1:(" + NDGetSlotNo2 + ") ");
                                            //    logstr.Append("STGetSlotPostion1:(" + NDGetSlotPostion2 + ") ");
                                            //    logstr.Append("STPutPosition1:(" + NDPutPosition2 + ") ");
                                            //    logstr.Append("STPutSlotNo1:(" + NDPutSlotNo2 + ") ");
                                            //    logstr.Append("STPutSlotPostion1:(" + NDPutSlotPostion2 + ") ");
                                            //}

                                            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreateTransferEQPToIndexCommand] - {0}", logstr.ToString());
                                            logInfo = string.Format("[  TransferEQ Check]   OK  {0} ", logstr.ToString());
                                            logStr.Append(logInfo);
                                            logStr.AppendLine();
                                            return;
                                        }
                                        #endregion
                                        #endregion

                                        if (cmds.Count > 0)
                                        {
                                            return;//每次只生成一条命令
                                        }
                                    }
                                    //    else
                                    //    {
                                    //        #region targetStage is Stage
                                    //        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - targetStage.Type == EnumUnitType.Stage; getRobotModel:{0}; targetStage:{1}", getRobotModel.ModelName, targetStage.ModelPosition);
                                    //        #region 获取 TargetMode
                                    //        Unit targetUnit = null;
                                    //        //RobotModel targetmode = null;
                                    //        RobotModel targetmode = GetCurrentModel(targetStage.ModelPosition);
                                    //        targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetmode.UnitID);
                                    //        if (targetUnit == null)
                                    //        {
                                    //            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check TargetUnit NG.TargetUnit is nul ");
                                    //            logInfo = string.Format("[  TransferEQ Check]   NG  Check TargetUnit NG.TargetUnit is nul");

                                    //            logStr.Append(logInfo);
                                    //            logStr.AppendLine();
                                    //            continue;
                                    //        }
                                    //        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
                                    //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check RobotModel OK; ModelPosition:{0} ", targetmode.ModelPosition);
                                    //        if (!targetmode.TransferEnable)
                                    //        {
                                    //            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check Action NG.TargetModelName={0},TransferEnable is false,", targetmode.ModelName);
                                    //            logInfo = string.Format("[  TransferEQ Check]   NG  Check TargetModelName={0},TransferEnable is false", targetmode.ModelName);

                                    //            logStr.Append(logInfo);
                                    //            logStr.AppendLine();
                                    //            continue;
                                    //        }
                                    //        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check TransferEnable OK;RobotModel.TransferEnable==true,targetmode:{0} ", targetmode.ModelName);

                                    //        #endregion
                                    //        #region 校验DownLink
                                    //        Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == targetUnit.UnitName && o.LinkName == targetmode.DownLinkName);
                                    //        if (downLinksignal == null)
                                    //        {
                                    //            //Logger.InfoFormat("[PROCESS PUT ] Cannot Find LinkSignal for {0}, UnitName: {1}, SUnitName: {2} ",
                                    //            //    glassInfo.GlassID, targetUnit.UnitName);
                                    //            Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] - Check Condition NG.TargetModelName={0},DownLinksignal is null ", targetmode.ModelName);
                                    //            logInfo = string.Format("[  TransferEQ Check]   NG  Check Condition NG.TargetModelName={0},DownLinksignal is null", targetmode.DownLinkName, targetmode.ModelName);

                                    //            logStr.Append(logInfo);
                                    //            logStr.AppendLine();
                                    //            continue;
                                    //        }
                                    //        // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check {0} OK", downLinksignal.LinkName);
                                    //        //if (Convert.ToInt32(targetUnit.UnitStatus) == (int)EquipmentStatus.DOWN)
                                    //        //{
                                    //        //    Logger.InfoFormat("[PROCESS PUT ] Equipment Status is Down for ModelPosition {0}, Glass {1} ", targetStage.ModelPosition, glassInfo.GlassID);
                                    //        //    return;
                                    //        //}
                                    //        if (!CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr))//校验下游收片信号
                                    //        {
                                    //            // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] DownstreamLinkSignal Check NG;LinkName:{0}; TransferEQPToIndexOrEQPCommand NG", downLinksignal.LinkName);
                                    //            logInfo = string.Format("[  TransferEQ Check]   NG  Check {0} Condition NG", downLinksignal.LinkName);
                                    //            logStr.Append(logInfo);
                                    //            logStr.AppendLine();
                                    //            continue;
                                    //        }
                                    //        #endregion
                                    //        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] DownstreamLinkSignal Check OK;LinkName:{0};RobotModel.TransferEnable==true; ", downLinksignal.LinkName);
                                    //        //var model = targetUnit.RobotModelList.FirstOrDefault(o => o.DownLinkName == downLinksignal.LinkName);
                                    //        //if (model != null)
                                    //        //{
                                    //        #region 获取SlotPosition
                                    //        int SlotPostion = 0;
                                    //        if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
                                    //        {
                                    //            SlotPostion = getGlass.SlotPosition;
                                    //            //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][LineMode == LineMode.OnlyA] SlotPostion:{0} ", SlotPostion);
                                    //        }
                                    //        else
                                    //        {
                                    //            var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlass.PortID);
                                    //            if (port != null)
                                    //            {
                                    //                var glassCount = port.GlassInfos.Where(o => o.Position == getGlass.Position).Count();
                                    //                SlotPostion = glassCount == 1 ? getGlass.SlotPosition : 99;
                                    //                // Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][LineMode != LineMode.OnlyA] SlotPostion:{0} ", SlotPostion);
                                    //            }
                                    //        }
                                    //        #endregion
                                    //        var cmd = new RobotCommand();
                                    //        if (HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut)
                                    //        {
                                    //            if (targetUnit.UnitType != EnumUnitType.Robot)
                                    //            {
                                    //                var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlass.PortID);

                                    //                var nextUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == port.UnitName);

                                    //                targetmode = nextUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlass.PortID);
                                    //                SlotPostion = getGlass.Position;
                                    //                //targetmode = GetCurrentModel(getGlass.);

                                    //            }
                                    //            cmd = factory.CreateTransferEQPToIndexCommand(getRobot, unitItem, getGlass, targetmode.TransferPriority, getGlass.ModelPosition, targetmode.ModelPosition, SlotPostion);
                                    //        }
                                    //        else
                                    //        {
                                    //            cmd = factory.CreateTransferEQPToEQPCommand(getRobot, unitItem, getGlass, targetmode.TransferPriority, getGlass.ModelPosition, targetmode.ModelPosition, SlotPostion);
                                    //        }
                                    //        #region 生成命令
                                    //        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand]targetmode:{0};targetmode.TransferPriority:{1} ", targetmode.ModelName, targetmode.TransferPriority);
                                    //        //var cmd = factory.CreateTransferEQPToEQPCommand(robotHand, unitItem, glassInfo, targetStage.PathConfigure.InPriority + targetStage.PathConfigure.InPriority, glassInfo.ModelPosition, model.ModelPosition, SlotPostion);
                                    //        cmd.GetGlass = getGlass;
                                    //        var GetPosition1 = GetCurrentModel(getGlass.ModelPosition);
                                    //        cmd.STGetPosition1string = GetPosition1.ModelName;
                                    //        cmd.STPutPosition1string = targetmode.ModelName;
                                    //        AddCommand(cmd);
                                    //        Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand][CreateTransferEQPToEQPCommand] - {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6}) -> ({7},{8}), Position({9}) ",
                                    //cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1, cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STPutSlotPostion1);
                                    //        logInfo = string.Format("[  TransferEQ Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6}) -> ({7},{8}), Position({9}) ",
                                    //cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, cmd.STGetSlotPostion1, cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STPutSlotPostion1);

                                    //        logStr.Append(logInfo);
                                    //        logStr.AppendLine();
                                    //        #endregion
                                    //        #endregion


                                    //    }
                                }
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

        /// <summary>
        /// 判断两片是否同等级
        /// </summary>
        /// <param name="glassA"></param>
        /// <param name="glassB"></param>
        /// <returns></returns>
        private bool CheckTwoGlassGrade(string EQPID, GlassInfo GlassA, GlassInfo GlassB)
        {
            try
            {
                if (GlassA != null && GlassB != null)
                {
                    var portGradeGroups = HostInfo.Current.PortGradeGroupList.FirstOrDefault(o => o.Key == EQPID).Value;
                    if (portGradeGroups != null && portGradeGroups.Count > 0)
                    {
                        var glassAPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassA.GlassGradeCode) && o.enabled == 0);
                        var glassBPortGradeGroups = portGradeGroups.Where(o => o.portgrade.Contains(GlassB.GlassGradeCode) && o.enabled == 0);
                        if (glassAPortGradeGroups != null && glassBPortGradeGroups != null)
                        {
                            var checkResult = false;
                            foreach (var glassAPGG in glassAPortGradeGroups)
                            {
                                checkResult = glassBPortGradeGroups.Any(c => c.portgradegroup == glassAPGG.portgradegroup);
                                if (checkResult)
                                    break;
                                else
                                    continue;
                            }
                            return checkResult;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                return true;
            }
        }

        #endregion
        #region Abort Command
        private void AbortCommand(ref StringBuilder logStr)
        {
            try
            {
                //Logger.Info("[AbortCommand] begin");
                string logInfo = string.Format("");
                //if (HostInfo.Current.EQPInfo.LineMode == LineMode.PHT600ForceCleanOut)
                //{
                //    Logger.Info("[AbortCommand]   LineMode is PHT600ForceCleanOut; TransferEQPToIndexOrEQPCommand NG ");
                //    logInfo = string.Format("[AbortCommand Check]   LineMode is PHT600ForceCleanOut; TransferEQPToIndexOrEQPCommand NG");
                //    logStr.Append(logInfo);
                //    logStr.AppendLine();
                //    return;
                //}
                //判断Robot手臂上是否有玻璃
                #region 校验手臂
                if (!Robot.UpperExistOn && !Robot.LowerExistOn)
                {
                    Logger.Info("[AbortCommand] - Check Arm NG.UpperExistOn=false,LowerExistOn=false ");
                    logInfo = string.Format("[AbortCommand Check]   NG  UpperExistOn=false,LowerExistOn=false");
                    logStr.Append(logInfo);
                    logStr.AppendLine();
                    return;
                }
                //Logger.Info("[AbortCommand] Check UpperExistOn,LowerExistOn OK;UpperExistOn=true;LowerExistOn=true ");

                #endregion

                //Dictionary<RobotHand,GlassInfo> HandJobDataDic = new Dictionary<RobotHand, GlassInfo>();
                #region 获取手臂和putGlass
                RobotHand RobotHand = RobotHand.Error;
                GlassInfo putGlass = null;

                if (Robot.LowerExistOn)
                {
                    if (Robot.LowHandGlass1 != null)
                    {
                        //HandJobDataDic.Add(RobotHand.LowHand, Robot.LowHandGlass1);
                        RobotHand = RobotHand.LowHand;
                        putGlass = Robot.LowHandGlass1;
                        // Logger.InfoFormat("[AbortCommand] [Robot.LowHandGlass1 != null] LowHandGlass1  glassInfo[{0},{1}]", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
                    }
                    else if (Robot.LowHandGlass2 != null)
                    {
                        //HandJobDataDic.Add(RobotHand.LowHand, Robot.LowHandGlass2);
                        RobotHand = RobotHand.LowHand;
                        putGlass = Robot.LowHandGlass2;
                        // Logger.InfoFormat("[AbortCommand] [Robot.LowHandGlass2 != null] LowHandGlass2  glassInfo[{0},{1}]", Robot.LowHandGlass2.CassetteSequenceNo, Robot.LowHandGlass2.SlotSequenceNo);
                    }
                    else
                    {
                        // Logger.InfoFormat("[AbortCommand][Robot.LowerExistOn] LowHandGlass1 and LowHandGlass2 ==null ");
                    }
                }
                else if (Robot.UpperExistOn)
                {
                    if (Robot.UpHandGlass1 != null)
                    {
                        //HandJobDataDic.Add(RobotHand.UpHand, Robot.UpHandGlass1);
                        RobotHand = RobotHand.UpHand;
                        putGlass = Robot.UpHandGlass1;
                        //  Logger.InfoFormat("[AbortCommand] [Robot.UpHandGlass1 != null] UpHandGlass1  glassInfo[{0},{1}]", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
                    }
                    else if (Robot.UpHandGlass2 != null)
                    {
                        //HandJobDataDic.Add(RobotHand.UpHand, Robot.UpHandGlass2);
                        RobotHand = RobotHand.UpHand;
                        putGlass = Robot.UpHandGlass2;
                        //  Logger.InfoFormat("[AbortCommand] [Robot.UpHandGlass2 != null] UpHandGlass2  glassInfo[{0},{1}]", Robot.UpHandGlass2.CassetteSequenceNo, Robot.UpHandGlass2.SlotSequenceNo);
                    }
                    else
                    {
                        // Logger.InfoFormat("[AbortCommand][Robot.UpperExistOn] UpHandGlass1 and UpHandGlass2 ==null ");
                    }
                }
                else
                {
                    // Logger.InfoFormat("[AbortCommand][Robot.LowerExistOn==false][Robot.UpperExistOn==false] ");
                }
                #endregion

                Logger.InfoFormat("[AbortCommand] - Check Action.Glass[{0},{1}],RobotArm({2})", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, RobotHand.ToString());
                var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlass.PortID);
                if (port != null)
                {
                    //if (port.CassetteInfo.CassetteStatus == EnumCarrierStatus.CassetteProcessAbort || HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut)
                    //{
                    //    //if (HostInfo.Current.EQPInfo.LineMode == LineMode.ForceCleanOut)
                    //    //{
                    //    //    Logger.InfoFormat("[AbortCommand] - LineMode ==ForceCleanOut");
                    //    //    logInfo = AbortCommand(logStr, RobotHand, putGlass, port);
                    //    //}
                    //    //else
                    //    {
                    //        #region 获取currentModel                        
                    //        RobotModel currentModel = GetCurrentModel(putGlass.ModelPosition);
                    //        if (currentModel == null)
                    //        {
                    //            Logger.InfoFormat("[AbortCommand] - Check RobotModel NG.Glass[{0},{1}],TargetModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    //            logInfo = string.Format("[AbortCommand Check]   NG  Glass[{0},{1}],TargetModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    //            logStr.Append(logInfo);
                    //            logStr.AppendLine();

                    //        }
                    //        #endregion
                    //        else
                    //        {
                    //            Logger.InfoFormat("[AbortCommand] - LineMode !=ForceCleanOut");
                    //            //Logger.InfoFormat("[AbortCommand] Check robotModel OK; targetModel:{0}", targetModel.ModelName);
                    //            if (!string.IsNullOrEmpty(currentModel.PortID))
                    //            {

                    //                Logger.InfoFormat("[AbortCommand] - Check Action OK;  Glass[{0},{1}],currentModel.PortID:{3};currentModel:{4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, currentModel.PortID, currentModel.ModelName);
                    //                //发送命令让glass返回原卡夹
                    //                logInfo = AbortCommand(logStr, RobotHand, putGlass, port);
                    //            }
                    //            else
                    //            {
                    //                Logger.InfoFormat("[AbortCommand] - Check Action NG.Glass[{0},{1}],currentModel.PortID is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    //                logInfo = string.Format("[AbortCommand Check]   NG  Glass[{0},{1}],currentModel.PortID is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
                    //                logStr.Append(logInfo);
                    //                logStr.AppendLine();
                    //            }
                    //        }

                    //    }

                    //}
                }

                //Logger.Info("[AbortCommand] end");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                Logger.Info(ex);
            }
        }

        private string AbortCommand(StringBuilder logStr, RobotHand RobotHand, GlassInfo putGlass, PortInfo port)
        {
            string logInfo;
            #region 获取SlotPostion
            int Position = 0;
            int SlotPostion = 0;
            //if (HostInfo.Current.EQPInfo.LineType == EnumLineType.TP)
            //{
            //    if (HostInfo.Current.EQPInfo.LineMode == LineMode.OnlyA)
            //    {
            //        SlotPostion = putGlass.SlotPosition;
            //        //Logger.InfoFormat("[AbortCommand][LineMode == LineMode.OnlyA];SlotPostion:{0}", SlotPostion);
            //    }
            //    else
            //    {
            //        //var glassCount = port.GlassInfos.Where(o => o.Position == putGlass.Position).Count();
            //        var glassCount = port.GlassInfos.Where(o => o.Position == putGlass.Position && o.SlotFlag != EnumGlassSlotStatus.Removed).Count();
            //        SlotPostion = glassCount == 1 ? putGlass.SlotPosition : 99;
            //        //Logger.InfoFormat("[AbortCommand][LineMode != LineMode.OnlyA];SlotPostion:{0}", SlotPostion);
            //    }
            //}
            //else
            {
                SlotPostion = 0;
            }
            #endregion
            var model = GetCurrentModel(putGlass.PortID);
            #region 生成命令
            RobotCommand cmd = factory.CreatePortPutCommand(port, model.ModelPosition, RobotHand, 999, Position, SlotPostion);
            //cmd.GlassInfo = putGlass;
            var glassPort = HostInfo.Current.PortList.FirstOrDefault(o => o.CassetteSequenceNo == putGlass.CassetteSequenceNo);
            if (glassPort != null)
            {
                var glassList = glassPort.GlassInfos.ToList();
                //GlassInfo GlassA = null;
                //GlassInfo GlassB = null;
                //GlassInfo Glass = null;
                //GetGlassByGlass(glassList, putGlass, ref GlassA, ref GlassB, ref Glass, "AbortCommand");
                //cmd.PutGlass = Glass;
                //cmd.PutGlassA = GlassA;
                //cmd.PutGlassB = GlassB;
            }
            else
            {
                cmd.PutGlass = putGlass;
            }

            //var PutPosition1 = GetCurrentModel(putGlass.ModelPosition);
            cmd.STPutPosition1string = model.ModelName;
            AddCommand(cmd);
            Logger.InfoFormat("[AbortCommand] [CreatePortPutCommand] - {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
            logInfo = string.Format("[AbortCommand Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
            logStr.Append(logInfo);
            logStr.AppendLine();
            #endregion
            return logInfo;
        }
        #endregion
        //#region  MultiChamber Command
        //private void FindMultiChamberGetCommand(ref StringBuilder logStr)
        //{
        //    #region 校验手臂资料
        //    if (Robot.UpperExistOn || Robot.LowerExistOn)
        //    {
        //        Logger.Info("[FindMultiChamberGetCommand] Check RobotHand NG; Robot.UpperExistOn || Robot.LowerExistOn; FindMultiChamberGetCommand NG ");
        //        var logInfo = string.Format("[FindMultiChamberGetCommand]   NG  Check RobotHand NG:Robot.UpperExistOn || Robot.LowerExistOn; FindMultiChamberGetCommand NG");
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    //Logger.Info("[CreateMultiChamberProcessGetCommand] Check RobotHand OK; ");
        //    #endregion
        //    // Logger.InfoFormat("+++ CreateMultiChamberProcessGetCommand ");
        //    //寻找设备取片命令
        //    CreateMultiChamberProcessGetCommand(ref logStr);
        //    // Logger.InfoFormat("+++ CreateOnlyBPortGetCommand ");
        //    //寻找port取片命令
        //    CreateMultiChamberPortGetCommand(ref logStr);

        //}
        //private void CreateMultiChamberProcessGetCommand(ref StringBuilder logStr)
        //{
        //    try
        //    {
        //        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] begin ");
        //        string logInfo = "";

        //        foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
        //        {
        //            if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
        //            {
        //                // List<GlassInfo> glassInfoList = new List<GlassInfo>();
        //                //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0} ", unitItem.UnitName);
        //                foreach (var getRobotModel in unitItem.RobotModelList)
        //                {
        //                    //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0};RobotMode:{1} ", unitItem.UnitName, getRobotModel.ModelName);
        //                    #region 校验Trouble信号
        //                    Linksignal upLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == getRobotModel.UPLinkName);
        //                    if (CheckUpLinkStatus(upLinksignal, unitItem, ref logStr))
        //                    {
        //                        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
        //                    }
        //                    else
        //                    {
        //                        // Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);
        //                        logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Check Condition NG,LinkName={0}", getRobotModel.UPLinkName);

        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    #endregion
        //                    #region 获取设备上要取的glass
        //                    //getRobotModel.EQPSendSlotNoA
        //                    GlassInfo getGlassA = null;
        //                    GlassInfo getGlassB = null;
        //                    switch (getRobotModel.UsedJobBlockNo)
        //                    {
        //                        case 0:
        //                            if (getRobotModel.GlassA != null)
        //                            {
        //                                getGlassA = getRobotModel.GlassA;
        //                                // Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=0;glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        //                            }
        //                            break;
        //                        case 3:
        //                            if (getRobotModel.GlassA != null)
        //                            {
        //                                getGlassA = getRobotModel.GlassA;
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3;glassInfo[{0},{1}]", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                            }
        //                            else
        //                            {
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3  getRobotModel.GlassA==NULL");
        //                                continue;
        //                            }
        //                            if (getRobotModel.GlassB != null)
        //                            {
        //                                getGlassB = getRobotModel.GlassB;
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3;glassInfo[{0},{1}]", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                            }
        //                            else
        //                            {
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3  getRobotModel.GlassB==NULL");
        //                                continue;
        //                            }
        //                            break;
        //                    }
        //                    #endregion



        //                    if (getGlassA != null)
        //                    {
        //                        if (HostInfo.Current.EQPInfo.LineMode == LineMode.MultiChamberForceCleanOut)
        //                        {
        //                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - LineMode ==MultiChamberForceCleanOut");
        //                            var upPort = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                            if (upPort == null)
        //                            {
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Port NG.getGlassA[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", getGlassA.PortID, getGlassA.CassetteID));
        //                                continue;
        //                            }

        //                            var targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == upPort.UnitName);

        //                            RobotCommand cmd = null;
        //                            #region 两张Glass
        //                            if (getGlassA != null && getGlassB != null)
        //                            {
        //                                #region 获取两个unloadPort

        //                                if (getGlassA.SlotSatus != EnumGlassSlotStatus.Processing && getGlassB.SlotSatus != EnumGlassSlotStatus.Skip)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassA[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                                var putGlassAModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                                if (putGlassAModel == null)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassA[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }


        //                                if (getGlassB.SlotSatus != EnumGlassSlotStatus.Processing && getGlassB.SlotSatus != EnumGlassSlotStatus.Skip)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassB[{0},{1}] SlotSatus!=Processing", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassB[{0},{1}] SlotSatus!=Processing", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                                var putGlassBModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlassB.PortID);
        //                                if (putGlassBModel == null)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassB[{0},{1}],TargetRobotModel is null", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassB[{0},{1}],TargetRobotModel is null", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }

        //                                #endregion

        //                                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
        //                                cmd = factory.CreateMultiChamberEQPToPortCommand(upPort, getRobotModel.ModelPosition, getRobotModel.EQPSendSlotNoA, putGlassAModel.ModelPosition, putGlassBModel.ModelPosition, getGlassA, getGlassB, 999, 0);
        //                                //cmd.GlassInfo = handJob;
        //                                cmd.PutGlassA = getGlassA;
        //                                cmd.PutGlassB = getGlassB;
        //                                cmd.STPutPosition1string = putGlassAModel.ModelName;
        //                                cmd.NDPutPosition2string = putGlassBModel.ModelName;
        //                                AddCommand(cmd);
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand][CreatePortPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), getGlassA({4},{5}),getGlassB({6},{7}), Position({8});",
        //                                                cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), getGlassA({4},{5}),getGlassB({6},{7}), Position({8});",
        //                                                cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                continue;
        //                            }
        //                            #endregion
        //                            #region 单张Glass

        //                            #region 校验put Stage
        //                            if (getGlassA.SlotSatus != EnumGlassSlotStatus.Processing)
        //                            {
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.Glass[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Glass[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                continue;
        //                            }
        //                            var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                            if (unloadport == null)
        //                            {
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Port NG.Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", getGlassA.PortID, getGlassA.CassetteID));
        //                                continue;
        //                            }
        //                            var putGlassModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                            if (putGlassModel == null)
        //                            {
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassA[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                continue;
        //                            }
        //                            #endregion

        //                            //Logger.InfoFormat("[FindPutCommand]Check RobotHand OK ;Put Stage OK; RobotHand:{0}; putRobotModel:{1}", RobotHand.ToString(), putRobotModel.ModelName);
        //                            #region 获取 SlotPostion
        //                            // int SlotPostion = 0;
        //                            #endregion
        //                            #region 获取手臂  
        //                            RobotHand getRobotA = RobotHand.Error;
        //                            getRobotA = GetRobot(getRobotModel, getRobotA);
        //                            if (getRobotA == RobotHand.Error)
        //                            {
        //                                Logger.Info("[CreateMultiChamberProcessGetCommand] Check getRobotA.RobotHand NG; RobotHand.Error");
        //                                continue;
        //                            }
        //                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] Check getRobotModel OK; getRobotModel:{0};getRobotA:{1};  ", getRobotModel.ModelName, getRobotA.ToString());
        //                            #endregion
        //                            #region 生成命令
        //                            // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
        //                            cmd = factory.CreateEQPToPortCommand(unloadport, getRobotModel.ModelPosition, putGlassModel.ModelPosition, getRobotModel.EQPSendSlotNoA, getGlassA, getRobotA, 999, 0);
        //                            //cmd.GlassInfo = handJob;
        //                            cmd.PutGlass = getGlassA;
        //                            cmd.STPutPosition1string = putGlassModel.ModelName;
        //                            AddCommand(cmd);
        //                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]- Create Action OK.{0}({1},{2}), Arm({3}), getGlassA({4},{5}), Position({6});",
        //                                            cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                            logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), getGlassA({4},{5}),  Position({6});",
        //                                            cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            #endregion
        //                            continue;
        //                            #endregion
        //                        }

        //                        #region 获取targetStage列表
        //                        var targetStageList = FindNextStage(getGlassA);//根据上一站的unit或者eqp获取下一站的站点
        //                        if (targetStageList.Count == 0)
        //                        {
        //                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check TargetStageList NG.Glass[{0},{1}], ModelPath={2},TargetStageList.Count=0", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassA.ModePath);
        //                            logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Check Glass[{0},{1}], ModelPath={2},TargetStageList.Count=0", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassA.ModePath);

        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }
        //                        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check TargetStageList OK; targetStageList.Count:{4};GlassInfo:[{0},{1}];RobotHand:{2};ModelPath:{3}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getRobot, getGlass.ModePath, targetStageList.Count);
        //                        #endregion


        //                        //if (HostInfo.Current.EQPID== "A1ANH100")
        //                        //{
        //                        #region 检查targetstage
        //                        //Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - EQPID:A1ANH100");
        //                        // List<Unit> targetUnitList = new List<Unit>();
        //                        foreach (var targetStage in targetStageList)
        //                        {

        //                            if (targetStage.Type == EnumUnitType.Robot)
        //                            {
        //                                RobotCommand cmd;
        //                                #region 获取targetModel
        //                                var targetModel = GetCurrentModel(targetStage.ModelPosition);
        //                                Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetModel.UnitID);
        //                                if (targetUnit == null)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  Check   targetUnit NG; targetModel[{0}];MultiChamberPutCommand NG ", targetModel.ModelName);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG    Check  targetUnit NG;targetModel[{0}];  MultiChamberPutCommand NG", targetModel.ModelName);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                #endregion

        //                                #region 检查putglass
        //                                //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                                var putRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                                if (putRobotModel == null)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand][CreatePortPutCommand] - Check Action NG.Glass[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Glass[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                // logInfo = string.Format("[FindPutCommand] [CreatePortPutCommand] Check putRobotModel OK; robputRobotModelotmodel:{0} ", putRobotModel.ModelName);
        //                                if (putRobotModel.ModelPosition != targetStage.ModelPosition)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand][CreatePortPutCommand] - Check TargetStage NG.ModelPosition:{0},Glass[{2},{3}],TargetModelPostion={1}", targetStage.ModelPosition, putRobotModel.ModelPosition, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  ModelPosition:{0},Glass[{2},{3}],TargetModelPostion={1}", targetStage.ModelPosition, putRobotModel.ModelPosition, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                #endregion

        //                                #region 两张Glass
        //                                if (getGlassA != null && getGlassB != null)
        //                                {
        //                                    #region 获取两个unloadPort
        //                                    var upPort = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                                    if (upPort == null)
        //                                    {
        //                                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Port NG.getGlassA[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                        logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                        logStr.Append(logInfo);
        //                                        logStr.AppendLine();
        //                                        eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", getGlassA.PortID, getGlassA.CassetteID));
        //                                        continue;
        //                                    }
        //                                    if (getGlassA.SlotSatus != EnumGlassSlotStatus.Processing && getGlassB.SlotSatus != EnumGlassSlotStatus.Skip)
        //                                    {
        //                                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassA[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                        logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                        logStr.Append(logInfo);
        //                                        logStr.AppendLine();
        //                                        continue;
        //                                    }
        //                                    //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                                    var putGlassAModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                                    if (putGlassAModel == null)
        //                                    {
        //                                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassA[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                        logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassA[{0},{1}],TargetRobotModel is null", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                        logStr.Append(logInfo);
        //                                        logStr.AppendLine();
        //                                        continue;
        //                                    }


        //                                    if (getGlassB.SlotSatus != EnumGlassSlotStatus.Processing && getGlassB.SlotSatus != EnumGlassSlotStatus.Skip)
        //                                    {
        //                                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassB[{0},{1}] SlotSatus!=Processing", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                        logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassB[{0},{1}] SlotSatus!=Processing", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                        logStr.Append(logInfo);
        //                                        logStr.AppendLine();
        //                                        continue;
        //                                    }
        //                                    //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                                    var putGlassBModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == getGlassB.PortID);
        //                                    if (putGlassBModel == null)
        //                                    {
        //                                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.getGlassB[{0},{1}],TargetRobotModel is null", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                        logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  getGlassB[{0},{1}],TargetRobotModel is null", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        //                                        logStr.Append(logInfo);
        //                                        logStr.AppendLine();
        //                                        continue;
        //                                    }

        //                                    #endregion

        //                                    // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
        //                                    cmd = factory.CreateMultiChamberEQPToPortCommand(upPort, getRobotModel.ModelPosition, getRobotModel.EQPSendSlotNoA, putGlassAModel.ModelPosition, putGlassBModel.ModelPosition, getGlassA, getGlassB, putRobotModel.TransferPriority, 0);
        //                                    //cmd.GlassInfo = handJob;
        //                                    cmd.PutGlassA = getGlassA;
        //                                    cmd.PutGlassB = getGlassB;
        //                                    cmd.STPutPosition1string = putGlassAModel.ModelName;
        //                                    cmd.NDPutPosition2string = putGlassBModel.ModelName;
        //                                    AddCommand(cmd);
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand][CreatePortPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), getGlassA({4},{5}),getGlassB({6},{7}), Position({8});",
        //                                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), getGlassA({4},{5}),getGlassB({6},{7}), Position({8});",
        //                                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                #endregion
        //                                #region 单张Glass

        //                                #region 校验put Stage
        //                                if (getGlassA.SlotSatus != EnumGlassSlotStatus.Processing)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Action NG.Glass[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Glass[{0},{1}] SlotSatus!=Processing", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == getGlassA.PortID);
        //                                if (unloadport == null)
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Port NG.Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", getGlassA.PortID, getGlassA.CassetteID, getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", getGlassA.PortID, getGlassA.CassetteID));
        //                                    continue;
        //                                }

        //                                #endregion

        //                                //Logger.InfoFormat("[FindPutCommand]Check RobotHand OK ;Put Stage OK; RobotHand:{0}; putRobotModel:{1}", RobotHand.ToString(), putRobotModel.ModelName);
        //                                #region 获取 SlotPostion
        //                                int SlotPostion = 0;
        //                                #endregion
        //                                #region 获取手臂  
        //                                RobotHand getRobotA = RobotHand.Error;
        //                                getRobotA = GetRobot(getRobotModel, getRobotA);
        //                                if (getRobotA == RobotHand.Error)
        //                                {
        //                                    Logger.Info("[CreateMultiChamberProcessGetCommand] Check getRobotA.RobotHand NG; RobotHand.Error");
        //                                    continue;
        //                                }
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] Check getRobotModel OK; getRobotModel:{0};getRobotA:{1};  ", getRobotModel.ModelName, getRobotA.ToString());
        //                                #endregion
        //                                #region 生成命令
        //                                // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
        //                                cmd = factory.CreateEQPToPortCommand(unloadport, getRobotModel.ModelPosition, putRobotModel.ModelPosition, getRobotModel.EQPSendSlotNoA, getGlassA, getRobotA, putRobotModel.TransferPriority, SlotPostion);
        //                                //cmd.GlassInfo = handJob;
        //                                cmd.PutGlass = getGlassA;
        //                                cmd.STPutPosition1string = putRobotModel.ModelName;
        //                                AddCommand(cmd);
        //                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]- Create Action OK.{0}({1},{2}), Arm({3}), getGlassA({4},{5}), Position({6});",
        //                                                cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), getGlassA({4},{5}),  Position({6});",
        //                                                cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                logStr.Append(logInfo);
        //                                logStr.AppendLine();
        //                                #endregion
        //                                #endregion

        //                            }
        //                            else
        //                            {

        //                                RobotModel putRobotModel = null;
        //                                Unit putUnit = null;
        //                                int EQPReciveSlotNoA = 0;
        //                                int EQPReciveSlotNoB = 0;
        //                                //FindMultiChamberPutCommand(RobotHand, putGlass, targetModel,  ref logStr);//放设备

        //                                Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
        //                                if (targetUnit.GetType().Name == "Unit" && targetUnit.RobotModelList.Count > 0)
        //                                {

        //                                    foreach (var unitRobotModel in targetUnit.RobotModelList)
        //                                    {
        //                                        if (unitRobotModel.ModelPosition == targetStage.PathConfigure.TargetPathName)
        //                                        {
        //                                            #region 校验DownLink
        //                                            Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == targetUnit.UnitName && o.LinkName == unitRobotModel.DownLinkName);
        //                                            if (downLinksignal == null)
        //                                            {
        //                                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  - Check Condition NG.DownLinksignal is null");
        //                                                logInfo = string.Format("[MultiChamberProcessGetCommand]   NG   DownLinksignal is null");
        //                                                logStr.Append(logInfo);
        //                                                logStr.AppendLine();
        //                                                continue;
        //                                            }
        //                                            bool checkLink = false;
        //                                            checkLink = CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr);
        //                                            if (!checkLink)//校验下游收片信号
        //                                            {
        //                                                //Logger.InfoFormat("[CreateMultiChamberPortGetCommand][IsPutWaitCommand:{0}] - DownstreamLinkSignal Check NG; LinkName:{1};ProcessPutCommand NG", IsPutWait, downLinksignal.LinkName);
        //                                                logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Check {0} Condition NG", downLinksignal.LinkName);
        //                                                logStr.Append(logInfo);
        //                                                logStr.AppendLine();
        //                                                continue;
        //                                            }
        //                                            if (checkLink)
        //                                            {
        //                                                if (unitRobotModel.EQPReciveSlotNoA == 0)
        //                                                {
        //                                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  - Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //                                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //                                                    logStr.Append(logInfo);
        //                                                    logStr.AppendLine();
        //                                                    continue;
        //                                                }
        //                                                putRobotModel = unitRobotModel;
        //                                                putUnit = targetUnit;

        //                                                EQPReciveSlotNoA = unitRobotModel.EQPReciveSlotNoA;
        //                                                EQPReciveSlotNoB = unitRobotModel.EQPReciveSlotNoB;
        //                                                break;
        //                                            }
        //                                            #endregion
        //                                        }

        //                                    }
        //                                }
        //                                if (putUnit == null)
        //                                {
        //                                    continue;
        //                                }
        //                                /// Logger.InfoFormat("[FindMultiChamberPutCommand] DownstreamLinkSignal Check OK; LinkName:{0}", downLinksignal.LinkName);
        //                                //Logger.InfoFormat("[FindMultiChamberPutCommand] Check RobotModel OK; RobotModel:{0}", targetmode.ModelName);
        //                                #region 获取SlotPostion
        //                                int SlotPostion = 0;
        //                                #endregion

        //                                #region 生成命令
        //                                if ((EQPReciveSlotNoA != 0 && getGlassA != null) && (EQPReciveSlotNoB != 0 && getGlassB != null))
        //                                {
        //                                    RobotCommand cmd = null;
        //                                    // Logger.InfoFormat("[FindMultiChamberPutCommand][PutCommand] targetmode:{0};targetmode.INPriority{1} ", targetmode.ModelName, targetmode.INPriority);
        //                                    cmd = factory.CreateMultiChamberEQPToEQPCommand(targetUnit, getRobotModel.ModelPosition, getRobotModel.EQPSendSlotNoA, EQPReciveSlotNoA, putRobotModel.TransferPriority, putRobotModel.ModelPosition, SlotPostion);
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Create Action OK.{0}({1},{2}), Arm({3}), getGlassA({4},{5}),getGlassB({6},{7}), Position({8})",
        //                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), getGlassA({4},{5}),getGlassB({6},{7}), Position({8})",
        //                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();

        //                                    cmd.PutGlassA = getGlassA;
        //                                    cmd.PutGlassB = getGlassB;
        //                                    cmd.STPutPosition1string = putRobotModel.ModelName;
        //                                    AddCommand(cmd);
        //                                }
        //                                else if (EQPReciveSlotNoA != 0 && getGlassA != null)
        //                                {
        //                                    int EQPReciveSlotNo = 0;
        //                                    if (HostInfo.Current.EQPID == "A1IMP100")
        //                                    {
        //                                        EQPReciveSlotNo = EQPReciveSlotNoB;
        //                                    }
        //                                    else
        //                                    {
        //                                        EQPReciveSlotNo = EQPReciveSlotNoA;
        //                                    }
        //                                    #region 获取手臂  
        //                                    RobotHand getRobotA = RobotHand.Error;
        //                                    getRobotA = GetRobot(getRobotModel, getRobotA);
        //                                    if (getRobotA == RobotHand.Error)
        //                                    {
        //                                        Logger.Info("[CreateMultiChamberProcessGetCommand] Check getRobotA.RobotHand NG; RobotHand.Error");
        //                                        continue;
        //                                    }
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] Check getRobotModel OK; getRobotModel:{0};getRobotA:{1};  ", getRobotModel.ModelName, getRobotA.ToString());
        //                                    #endregion
        //                                    RobotCommand cmd = null;
        //                                    // Logger.InfoFormat("[FindMultiChamberPutCommand][PutCommand] targetmode:{0};targetmode.INPriority{1} ", targetmode.ModelName, targetmode.INPriority);
        //                                    cmd = factory.CreateMultiChamberEQPToEQPCommand(targetUnit, getRobotModel.ModelPosition, getRobotModel.EQPSendSlotNoA, getRobotA, EQPReciveSlotNo, putRobotModel.TransferPriority, putRobotModel.ModelPosition, SlotPostion);
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();

        //                                    cmd.PutGlass = getGlassA;
        //                                    cmd.STPutPosition1string = putRobotModel.ModelName;
        //                                    AddCommand(cmd);
        //                                }
        //                                else
        //                                {
        //                                    Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check NG; (EQPReciveSlotNoA!=0&& getGlassA != null) && (EQPReciveSlotNoB != 0 && getGlassB != null) Is False;and (EQPReciveSlotNoA != 0 && getGlassA != null) Is False ");
        //                                    logInfo = string.Format("[MultiChamberProcessGetCommand]   Check NG; (EQPReciveSlotNoA!=0&& getGlassA != null) && (EQPReciveSlotNoB != 0 && getGlassB != null) Is False;and (EQPReciveSlotNoA != 0 && getGlassA != null) Is False ");
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                }
        //                                #endregion
        //                            }


        //                        }
        //                    }
        //                    #endregion
        //                    else
        //                    {
        //                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Data NG.ModelName={0},GlassInfo is null", getRobotModel.ModelName);
        //                    }
        //                }

        //            }
        //        }

        //        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] end ");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //}
        ////private void CreateMultiChamberProcessGetCommand(ref StringBuilder logStr)
        ////{
        ////    try
        ////    {
        ////        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] begin ");
        ////        string logInfo = "";

        ////        foreach (var unitItem in HostInfo.Current.EQPInfo.Units)
        ////        {
        ////            if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
        ////            {
        ////                // List<GlassInfo> glassInfoList = new List<GlassInfo>();
        ////                //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0} ", unitItem.UnitName);
        ////                foreach (var getRobotModel in unitItem.RobotModelList)
        ////                {
        ////                    //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0};RobotMode:{1} ", unitItem.UnitName, getRobotModel.ModelName);
        ////                    #region 校验Trouble信号
        ////                    Linksignal upLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == getRobotModel.UPLinkName);
        ////                    if (CheckUpLinkStatus(upLinksignal, unitItem, ref logStr))
        ////                    {
        ////                        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  UpstreamLinkSignal Check OK; link.LinkName:{0}", getRobotModel.UPLinkName);
        ////                    }
        ////                    else
        ////                    {
        ////                        // Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  UpstreamLinkSignal Check NG; link.LinkName:{0}", getRobotModel.UPLinkName);
        ////                        logInfo = string.Format("[    OnlyBGet Check]   NG  Check Condition NG,LinkName={0}", getRobotModel.UPLinkName);

        ////                        logStr.Append(logInfo);
        ////                        logStr.AppendLine();
        ////                        continue;
        ////                    }
        ////                    #endregion
        ////                    #region 获取设备上要取的glass
        ////                    //getRobotModel.EQPSendSlotNoA
        ////                    GlassInfo getGlassA = null;
        ////                    GlassInfo getGlassB = null;
        ////                    switch (getRobotModel.UsedJobBlockNo)
        ////                    {
        ////                        case 0:
        ////                            if (getRobotModel.GlassA != null)
        ////                            {
        ////                                getGlassA = getRobotModel.GlassA;
        ////                                // Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=0;glassInfo[{0},{1}] ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo);
        ////                            }
        ////                            break;
        ////                        case 3:
        ////                            if (getRobotModel.GlassA != null)
        ////                            {
        ////                                getGlassA = getRobotModel.GlassA;
        ////                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3;glassInfo[{0},{1}]", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        ////                            }
        ////                            else
        ////                            {
        ////                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3  getRobotModel.GlassA==NULL");
        ////                                return;
        ////                            }
        ////                            if (getRobotModel.GlassB != null)
        ////                            {
        ////                                getGlassB = getRobotModel.GlassB;
        ////                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3;glassInfo[{0},{1}]", getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        ////                            }
        ////                            else
        ////                            {
        ////                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UsedJobBlockNo=3  getRobotModel.GlassB==NULL");
        ////                                return;
        ////                            }
        ////                            break;
        ////                    }
        ////                    #endregion

        ////                    //add by linbin_zhou 20210305
        ////                    if (HostInfo.Current.EQPID == "A1ANH100" && HostInfo.Current.EQPInfo.LineMode != LineMode.MultiChamberForceCleanOut)
        ////                    {
        ////                        #region 获取targetStage列表
        ////                        var targetStageList = FindNextStage(getGlassA);//根据上一站的unit或者eqp获取下一站的站点
        ////                        if (targetStageList.Count == 0)
        ////                        {
        ////                            Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - Check TargetStageList NG.Glass[{0},{1}], ModelPath={2},TargetStageList.Count=0", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassA.ModePath);
        ////                            logInfo = string.Format("[MultiChamberPortGetCommand]   NG  Check Glass[{0},{1}], ModelPath={2},TargetStageList.Count=0", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassA.ModePath);

        ////                            logStr.Append(logInfo);
        ////                            logStr.AppendLine();
        ////                            return;
        ////                        }
        ////                        //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check TargetStageList OK; targetStageList.Count:{4};GlassInfo:[{0},{1}];RobotHand:{2};ModelPath:{3}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getRobot, getGlass.ModePath, targetStageList.Count);
        ////                        #endregion
        ////                        RobotModel putRobotModel = null;
        ////                        Unit putUnit = null;
        ////                        int EQPReciveSlotNoA = 0;
        ////                        int EQPReciveSlotNoB = 0;

        ////                        //if (HostInfo.Current.EQPID== "A1ANH100")
        ////                        //{
        ////                        #region 校验targetStage DownLink;获取 EQPReciveSlotNo
        ////                        //Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - EQPID:A1ANH100");
        ////                        // List<Unit> targetUnitList = new List<Unit>();
        ////                        foreach (var targetStage in targetStageList)
        ////                        {
        ////                            Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);

        ////                            if (targetUnit.GetType().Name == "Unit" && targetUnit.RobotModelList.Count > 0)
        ////                            {
        ////                                // List<GlassInfo> glassInfoList = new List<GlassInfo>();
        ////                                //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0} ", unitItem.UnitName);
        ////                                foreach (var unitRobotModel in targetUnit.RobotModelList)
        ////                                {
        ////                                    if (unitRobotModel.ModelPosition == targetStage.PathConfigure.TargetPathName)
        ////                                    {
        ////                                        #region 校验DownLink
        ////                                        Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == targetUnit.UnitName && o.LinkName == unitRobotModel.DownLinkName);
        ////                                        if (downLinksignal == null)
        ////                                        {
        ////                                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  - Check Condition NG.DownLinksignal is null");
        ////                                            logInfo = string.Format("[MultiChamberProcessGetCommand]   NG   DownLinksignal is null");
        ////                                            logStr.Append(logInfo);
        ////                                            logStr.AppendLine();
        ////                                            continue;
        ////                                        }
        ////                                        bool checkLink = false;
        ////                                        checkLink = CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr);
        ////                                        if (!checkLink)//校验下游收片信号
        ////                                        {
        ////                                            //Logger.InfoFormat("[CreateMultiChamberPortGetCommand][IsPutWaitCommand:{0}] - DownstreamLinkSignal Check NG; LinkName:{1};ProcessPutCommand NG", IsPutWait, downLinksignal.LinkName);
        ////                                            logInfo = string.Format("[MultiChamberProcessGetCommand]   NG  Check {0} Condition NG", downLinksignal.LinkName);
        ////                                            logStr.Append(logInfo);
        ////                                            logStr.AppendLine();
        ////                                            continue;
        ////                                        }
        ////                                        if (checkLink)
        ////                                        {
        ////                                            if (unitRobotModel.EQPReciveSlotNoA == 0)
        ////                                            {
        ////                                                Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]  - Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        ////                                                logInfo = string.Format("[MultiChamberProcessGetCommand]   Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        ////                                                logStr.Append(logInfo);
        ////                                                logStr.AppendLine();
        ////                                                continue;
        ////                                            }
        ////                                            putRobotModel = unitRobotModel;
        ////                                            putUnit = targetUnit;

        ////                                            EQPReciveSlotNoA = unitRobotModel.EQPReciveSlotNoA;
        ////                                            EQPReciveSlotNoB = unitRobotModel.EQPReciveSlotNoB;
        ////                                            break;
        ////                                        }
        ////                                        #endregion
        ////                                    }

        ////                                }
        ////                                if (putUnit != null)
        ////                                {
        ////                                    break;
        ////                                }
        ////                            }
        ////                            else if (targetUnit.GetType().Name == "Index" && targetUnit.RobotModelList.Count > 0)
        ////                            {
        ////                                foreach (var unitRobotModel in targetUnit.RobotModelList)
        ////                                {
        ////                                    putRobotModel = unitRobotModel;
        ////                                }
        ////                            }
        ////                        }

        ////                        if (putRobotModel == null)
        ////                        {
        ////                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check putRobotModel NG.Glass[{0},{1}], putRobotModel=null;CreateMultiChamberPortPutCommand NG ", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        ////                            logInfo = string.Format("[MultiChamberProcessGetCommand] - Check putRobotModel NG.Glass[{0},{1}], putRobotModel=null;CreateMultiChamberPortPutCommand NG", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        ////                            logStr.Append(logInfo);
        ////                            logStr.AppendLine();
        ////                            continue;
        ////                        }
        ////                        #endregion
        ////                    }
        ////                    //end 20210305

        ////                    if (getGlassA != null)
        ////                    {
        ////                        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] Check RobotModel GlassInfo OK; GlassInfo!=null glassInfo[{0},{1}];RobotModel:{2} ", glassInfo.CassetteSequenceNo, glassInfo.SlotSequenceNo, item.ModelName);
        ////                        //logInfo = string.Format("[CreateMultiChamberProcessGetCommand] - Check GlassInfo OK; getGlass[{0},{1}];getRobotModel:{2} ", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getRobotModel.ModelName);
        ////                        //Logger.InfoFormat(logInfo);
        ////                        //logStr.Append(logInfo);



        ////                        #region 生成命令
        ////                        if (getGlassB == null)
        ////                        {
        ////                            #region 获取手臂  
        ////                            RobotHand getRobotA = RobotHand.Error;
        ////                            getRobotA = GetRobot(getRobotModel, getRobotA);
        ////                            if (getRobotA == RobotHand.Error)
        ////                            {
        ////                                Logger.Info("[CreateMultiChamberProcessGetCommand] Check getRobotA.RobotHand NG; RobotHand.Error");
        ////                                return;
        ////                            }
        ////                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] Check getRobotModel OK; getRobotModel:{0};getRobotA:{1};  ", getRobotModel.ModelName, getRobotA.ToString());
        ////                            #endregion

        ////                            //int slotPostion = getGlassA.SlotPosition;
        ////                            //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand]getRobot{0};slotPostion{1} ", getRobot.ToString(), slotPostion);
        ////                            var cmd = factory.CreateMultiChamberProcessGetCommand(unitItem, getRobotA, getRobotModel.OutPriority, getGlassA.ModelPosition, getRobotModel.EQPSendSlotNoA);
        ////                            cmd.GetGlass = getGlassA;
        ////                            var GetPosition1 = GetCurrentModel(getGlassA.ModelPosition);
        ////                            cmd.STGetPosition1string = GetPosition1.ModelName;
        ////                            AddCommand(cmd);
        ////                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        ////                                 cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STGetSlotPostion1);
        ////                            logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        ////                                 cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STGetSlotPostion1);
        ////                            logStr.Append(logInfo);
        ////                            logStr.AppendLine();
        ////                        }
        ////                        else
        ////                        {
        ////                            var cmd = factory.CreateMultiChamberProcessGetCommand(unitItem, getRobotModel.OutPriority, getGlassA.ModelPosition, getRobotModel.EQPSendSlotNoA, getRobotModel.EQPSendSlotNoB);
        ////                            cmd.GetGlassA = getGlassA;
        ////                            cmd.GetGlassB = getGlassB;
        ////                            var GetPosition1 = GetCurrentModel(getGlassA.ModelPosition);
        ////                            cmd.STGetPosition1string = GetPosition1.ModelName;
        ////                            cmd.NDGetPosition2string = GetPosition1.ModelName;
        ////                            AddCommand(cmd);
        ////                            Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Create Action OK.{0}({1},{2}), Arm({3}), GlassA({4},{5}),GlassB({6},{7}), Position({6})",
        ////                                 cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STGetSlotPostion1, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        ////                            logInfo = string.Format("[MultiChamberProcessGetCommand]   OK  {0}({1},{2}), Arm({3}), GlassA({4},{5}),GlassB({6},{7}), Position({6})",
        ////                                 cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, cmd.STGetSlotPostion1, getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo);
        ////                            logStr.Append(logInfo);
        ////                            logStr.AppendLine();
        ////                        }

        ////                        #endregion

        ////                    }
        ////                    else
        ////                    {
        ////                        Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] - Check Data NG.ModelName={0},GlassInfo is null", getRobotModel.ModelName);
        ////                    }
        ////                }

        ////            }
        ////        }

        ////        //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] end ");
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        Logger.Error(ex);
        ////        Logger.Info(ex);
        ////    }
        ////}

        //private void CreateMultiChamberPortGetCommand(ref StringBuilder logStr)
        //{
        //    string logInfo = string.Format("");

        //    #region 获取CurrentPort
        //    //获取WaiForProcessing或者InProcessing状态的port
        //    List<PortInfo> LoadingPortList = GetLoadingPossiblePortList(HostInfo.Current.PortList, "CreateMultiChamberPortGetCommand");
        //    if (LoadingPortList == null || LoadingPortList.Count == 0)
        //    {
        //        Logger.Info("[CreateMultiChamberPortGetCommand] - Check LoadingPortList NG.LoadingPortList.Count=0");
        //        logInfo = string.Format("[MultiChamberPortGetCommand]   NG  LoadingPortList.Count=0");
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    //  Logger.InfoFormat("[CreateMultiChamberPortGetCommand] Check LoadingPortList OK; LoadingPortList.Count:{0} ", LoadingPortList.Count);
        //    //for (int i = 0; i < LoadingPortList.Count; i++)
        //    //{
        //    //    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] Check LoadingPort{0};PortID:{1},WaitingforProcessingTime:{2} ", (i + 1), LoadingPortList[i].PortID, LoadingPortList[i].WaitingforProcessingTime);
        //    //}
        //    // PortInfo currentLoadingPort = LoadingPortList.OrderByDescending(o => o.CassetteInfo.CassetteProcessStartTime).FirstOrDefault();
        //    PortInfo currentLoadingPort = LoadingPortList.FirstOrDefault(o => o.CassetteInfo.CassetteStatus == EnumCarrierStatus.InProcessing);
        //    if (currentLoadingPort == null)
        //    {
        //        //LoadingPortList = LoadingPortList.Where(o => o.WaitingforProcessingTime != DateTime.MinValue).ToList();
        //        currentLoadingPort = LoadingPortList.OrderBy(o => o.WaitingforProcessingTime).FirstOrDefault();
        //    }
        //    // Logger.InfoFormat("[CreateMultiChamberPortGetCommand] CurrentLoadingPort{0} ", currentLoadingPort.PortID);
        //    var index = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == currentLoadingPort.UnitID);
        //    if (index == null)
        //    {
        //        Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - Check UnitID NG; CurrentLoadingPort.UnitID is null ");
        //        logInfo = string.Format("[MultiChamberPortGetCommand]   NG Check UnitID NG; CurrentLoadingPort.UnitID is null ");
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    #endregion

        //    #region 获取GetGlassA ,getGlassB
        //    var GlassInfos = currentLoadingPort.GlassInfos.Where(o => o.SlotSatus == EnumGlassSlotStatus.Wait && o.SlotFlag != EnumGlassSlotStatus.Removed).ToList();
        //    GlassInfo getGlassA = null;
        //    RobotHand LowHand = RobotHand.LowHand;
        //    GlassInfo getGlassB = null;
        //    RobotHand UpHand = RobotHand.UpHand;
        //    int getModelPosition = 0;
        //    PortGetType PortGetType = PortGetType.ASC;
        //    RobotModel getRobotModel = null;
        //    var model = index.RobotModelList.FirstOrDefault(o => o.PortID == currentLoadingPort.PortID);
        //    if (model != null)
        //    {
        //        getModelPosition = model.ModelPosition;
        //        PortGetType = model.PortGetType;
        //        getRobotModel = model;
        //    }

        //    if (PortGetType == PortGetType.DESC)
        //    {
        //        GlassInfos = GlassInfos.OrderByDescending(o => o.Position).ToList();
        //        getGlassA = GlassInfos.FirstOrDefault();
        //        //var currentIndex = GlassInfos.IndexOf(getGlassA);
        //        //getGlassB = GlassInfos[(currentIndex + 1)];
        //        // Logger.InfoFormat("[CreateMultiChamberPortGetCommand][LineMode!=OnlyA or MixRun]  PortGetType==DESC,GlassInfo[{0},{1}] ", info.CassetteSequenceNo, info.SlotSequenceNo);
        //    }
        //    else// if (PortGetType == PortGetType.ASC)
        //    {
        //        //getGlassA = GlassInfos.OrderBy(o => o.Position).FirstOrDefault();
        //        GlassInfos = GlassInfos.OrderBy(o => o.Position).ToList();
        //        getGlassA = GlassInfos.FirstOrDefault();
        //        //var currentIndex = GlassInfos.IndexOf(getGlassA);
        //        //getGlassB = GlassInfos[(currentIndex + 1)];
        //        //Logger.InfoFormat("[CreateMultiChamberPortGetCommand][LineMode!=OnlyA or MixRun] PortGetType==ASC,GlassInfo[{0},{1}] ", info.CassetteSequenceNo, info.SlotSequenceNo);
        //    }
        //    //Logger.InfoFormat("[CreateMultiChamberPortGetCommand]ModelPosition:{0}", CurrentModel.ModelName);
        //    #endregion


        //    #region 获取targetStage列表
        //    var targetStageList = FindNextStage(getGlassA);//根据上一站的unit或者eqp获取下一站的站点
        //    if (targetStageList.Count == 0)
        //    {
        //        Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - Check TargetStageList NG.Glass[{0},{1}], ModelPath={2},TargetStageList.Count=0", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassA.ModePath);
        //        logInfo = string.Format("[MultiChamberPortGetCommand]   NG  Check Glass[{0},{1}], ModelPath={2},TargetStageList.Count=0", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo, getGlassA.ModePath);

        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    //Logger.InfoFormat("[TransferEQPToIndexOrEQPCommand] Check TargetStageList OK; targetStageList.Count:{4};GlassInfo:[{0},{1}];RobotHand:{2};ModelPath:{3}", getGlass.CassetteSequenceNo, getGlass.SlotSequenceNo, getRobot, getGlass.ModePath, targetStageList.Count);
        //    #endregion
        //    RobotModel putRobotModel = null;
        //    Unit putUnit = null;
        //    int EQPReciveSlotNoA = 0;
        //    int EQPReciveSlotNoB = 0;

        //    //if (HostInfo.Current.EQPID== "A1ANH100")
        //    //{
        //    #region 校验targetStage DownLink;获取 EQPReciveSlotNo
        //    //Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - EQPID:A1ANH100");
        //    // List<Unit> targetUnitList = new List<Unit>();
        //    foreach (var targetStage in targetStageList)
        //    {
        //        Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);

        //        if (targetUnit.GetType().Name == "Unit" && targetUnit.RobotModelList.Count > 0)
        //        {
        //            // List<GlassInfo> glassInfoList = new List<GlassInfo>();

        //            foreach (var unitRobotModel in targetUnit.RobotModelList)
        //            {
        //                if (unitRobotModel.ModelPosition == targetStage.PathConfigure.TargetPathName)
        //                {
        //                    #region 校验DownLink
        //                    Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == targetUnit.UnitName && o.LinkName == unitRobotModel.DownLinkName);
        //                    if (downLinksignal == null)
        //                    {
        //                        Logger.InfoFormat("[CreateMultiChamberPortGetCommand]  - Check Condition NG.DownLinksignal is null");
        //                        logInfo = string.Format("[MultiChamberPortGetCommand]   NG   DownLinksignal is null");
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    bool checkLink = false;
        //                    checkLink = CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr);
        //                    if (!checkLink)//校验下游收片信号
        //                    {
        //                        //Logger.InfoFormat("[CreateMultiChamberPortGetCommand][IsPutWaitCommand:{0}] - DownstreamLinkSignal Check NG; LinkName:{1};ProcessPutCommand NG", IsPutWait, downLinksignal.LinkName);
        //                        logInfo = string.Format("[MultiChamberPortGetCommand]   NG  Check {0} Condition NG", downLinksignal.LinkName);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    if (checkLink)
        //                    {
        //                        if (unitRobotModel.EQPReciveSlotNoA == 0)
        //                        {
        //                            Logger.InfoFormat("[CreateMultiChamberPortGetCommand]  - Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //                            logInfo = string.Format("[MultiChamberPortGetCommand]   Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }
        //                        putRobotModel = unitRobotModel;
        //                        putUnit = targetUnit;

        //                        EQPReciveSlotNoA = unitRobotModel.EQPReciveSlotNoA;
        //                        EQPReciveSlotNoB = unitRobotModel.EQPReciveSlotNoB;
        //                        continue;
        //                    }
        //                    #endregion
        //                }

        //            }
        //            if (putUnit != null)
        //            {
        //                continue;
        //            }
        //        }
        //    }

        //    if (putRobotModel == null)
        //    {
        //        Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - Check putRobotModel NG.Glass[{0},{1}], putRobotModel=null;CreateMultiChamberPortGetCommand NG ", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //        logInfo = string.Format("[MultiChamberPortGetCommand] - Check putRobotModel NG.Glass[{0},{1}], putRobotModel=null;CreateMultiChamberPortGetCommand NG", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    #endregion
        //    //}
        //    //else
        //    //{
        //    //    #region 校验targetStage DownLink;获取 EQPReciveSlotNo

        //    //    List<Unit> targetUnitList = new List<Unit>();
        //    //    foreach (var targetStage in targetStageList)
        //    //    {
        //    //        Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
        //    //        if (targetUnit != null)
        //    //        {
        //    //            targetUnitList.Add(targetUnit);
        //    //        }
        //    //    }
        //    //    //RobotModel putRobotModel = null;
        //    //    //Unit putUnit = null;
        //    //    //int EQPReciveSlotNoA = 0;
        //    //    //int EQPReciveSlotNoB = 0;

        //    //    foreach (var unitItem in targetUnitList)
        //    //    {
        //    //        if (unitItem.GetType().Name == "Unit" && unitItem.RobotModelList.Count > 0)
        //    //        {
        //    //            // List<GlassInfo> glassInfoList = new List<GlassInfo>();
        //    //            //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0} ", unitItem.UnitName);
        //    //            foreach (var unitRobotModel in unitItem.RobotModelList)
        //    //            {
        //    //                // if(unitRobotModel.ModelPosition)

        //    //                //Logger.InfoFormat("[CreateMultiChamberProcessGetCommand] UnitItem:{0};RobotMode:{1} ", unitItem.UnitName, getRobotModel.ModelName);
        //    //                #region 校验DownLink
        //    //                Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == unitItem.UnitName && o.LinkName == unitRobotModel.DownLinkName);
        //    //                if (downLinksignal == null)
        //    //                {
        //    //                    Logger.InfoFormat("[CreateMultiChamberPortGetCommand]  - Check Condition NG.DownLinksignal is null");
        //    //                    logInfo = string.Format("[MultiChamberPortGetCommand]   NG   DownLinksignal is null");
        //    //                    logStr.Append(logInfo);
        //    //                    logStr.AppendLine();
        //    //                    continue;
        //    //                }
        //    //                bool checkLink = false;
        //    //                checkLink = CheckLinkStatusReceive(downLinksignal, unitItem, ref logStr);
        //    //                if (!checkLink)//校验下游收片信号
        //    //                {
        //    //                    //Logger.InfoFormat("[CreateMultiChamberPortGetCommand][IsPutWaitCommand:{0}] - DownstreamLinkSignal Check NG; LinkName:{1};ProcessPutCommand NG", IsPutWait, downLinksignal.LinkName);
        //    //                    logInfo = string.Format("[MultiChamberPortGetCommand]   NG  Check {0} Condition NG", downLinksignal.LinkName);
        //    //                    logStr.Append(logInfo);
        //    //                    logStr.AppendLine();
        //    //                    continue;
        //    //                }
        //    //                if (checkLink)
        //    //                {
        //    //                    if (unitRobotModel.EQPReciveSlotNoA == 0)
        //    //                    {
        //    //                        Logger.InfoFormat("[CreateMultiChamberPortGetCommand]  - Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //    //                        logInfo = string.Format("[MultiChamberPortGetCommand]   Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //    //                        logStr.Append(logInfo);
        //    //                        logStr.AppendLine();
        //    //                        continue;
        //    //                    }
        //    //                    putRobotModel = unitRobotModel;
        //    //                    putUnit = unitItem;

        //    //                    EQPReciveSlotNoA = unitRobotModel.EQPReciveSlotNoA;
        //    //                    EQPReciveSlotNoB = unitRobotModel.EQPReciveSlotNoB;
        //    //                    break;
        //    //                }
        //    //                #endregion


        //    //            }
        //    //            if (putUnit != null)
        //    //            {
        //    //                break;
        //    //            }
        //    //        }
        //    //    }

        //    //    if (putRobotModel == null)
        //    //    {
        //    //        Logger.InfoFormat("[CreateMultiChamberPortGetCommand] - Check putRobotModel NG.Glass[{0},{1}], putRobotModel=null;CreateMultiChamberPortGetCommand NG ", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //    //        logInfo = string.Format("[MultiChamberPortGetCommand] - Check putRobotModel NG.Glass[{0},{1}], putRobotModel=null;CreateMultiChamberPortGetCommand NG", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //    //        logStr.Append(logInfo);
        //    //        logStr.AppendLine();
        //    //        return;
        //    //    }
        //    //    #endregion
        //    //}


        //    if (EQPReciveSlotNoB != 0)
        //    {
        //        #region 获取GlassB
        //        var currentIndex = GlassInfos.IndexOf(getGlassA);
        //        if (currentIndex < GlassInfos.Count() - 1)
        //        {
        //            getGlassB = GlassInfos[(currentIndex + 1)];
        //        }

        //        if (HostInfo.Current.EQPID == "A1IMP100")
        //        {
        //            if (getGlassB != null)
        //            {
        //                if (getGlassA.ProductRecipe != getGlassB.ProductRecipe)
        //                {
        //                    getGlassB = null;
        //                    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] getGlassA.ProductRecipe!= getGlassB.ProductRecipe;getGlassB = null; getGlassA[{0},{1}]; ", getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo);
        //                }
        //                else if (getGlassA.ProductRecipe == getGlassB.ProductRecipe)
        //                {
        //                    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] getGlassA.ProductRecipe = getGlassB.ProductRecipe;getGlassA[{0},{1},ProductRecipe:{2}];getGlassB[{3},{4},ProductRecipe:{5}];");
        //                }
        //            }
        //        }

        //        #endregion
        //    }

        //    #region 校验 getRobotModel
        //    if (getRobotModel == null)
        //    {
        //        Logger.InfoFormat("[CreateMultiChamberPortGetCommand] Check getRobotModel NG; PortID:{0}; CreateMultiChamberPortGetCommand NG ", currentLoadingPort.PortID);
        //        logInfo = string.Format("[MultiChamberPortGetCommand]   NG  getRobotModel NG; PortID:{0}; CreateMultiChamberPortGetCommand NG ", currentLoadingPort.PortID);
        //        logStr.Append(logInfo);
        //        logStr.AppendLine();
        //        return;
        //    }
        //    //Logger.InfoFormat("[CreateMultiChamberPortGetCommand] PortGetType:{0},getRobotModel:{1} ", PortGetType.ToString(), getRobotModel.ModelName);
        //    //if (!getRobotModel.GetEnable)
        //    //{
        //    //    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] Check getRobotModel.GetEnable NG; getRobotModel.GetEnable=false;getRobotModel:{0}; CreateMultiChamberPortGetCommand NG ", getRobotModel.ModelName);
        //    //    logInfo = string.Format("[MultiChamberPortGetCommand]   NG  Port:{0},GetEnable = false; PortGetCommand NG", currentLoadingPort.PortID);
        //    //    logStr.Append(logInfo);
        //    //    logStr.AppendLine();
        //    //    return;
        //    //}

        //    #endregion

        //    #region 获取SlotPostion
        //    int SlotPostion = 0;
        //    #endregion

        //    #region 生成命令

        //    if (HostInfo.Current.EQPInfo.IsColdRun)
        //    {
        //        bool islastglass = false;
        //        int count = HostInfo.Current.EQPInfo.ColdRunTotalQuantity - HostInfo.Current.EQPInfo.ColdRunCurrentQuantity;
        //        if (count == 1)
        //        {
        //            islastglass = true;
        //        }
        //        if (islastglass || getGlassB == null)
        //        {
        //            CreateMultiChamberPortOneGlassGetCommand(getRobotModel, EQPReciveSlotNoB, EQPReciveSlotNoA, currentLoadingPort, getModelPosition, putRobotModel, SlotPostion, getGlassA, logInfo, ref logStr);
        //        }
        //        else
        //        {
        //            CreateMultiChamberPortTwoGlassGetCommand(ref logStr, currentLoadingPort, getGlassA, getGlassB, LowHand, UpHand, getModelPosition, getRobotModel, putRobotModel, EQPReciveSlotNoA, EQPReciveSlotNoB, SlotPostion);
        //        }
        //    }
        //    else
        //    {
        //        if (getGlassB == null)
        //        {
        //            CreateMultiChamberPortOneGlassGetCommand(getRobotModel, EQPReciveSlotNoB, EQPReciveSlotNoA, currentLoadingPort, getModelPosition, putRobotModel, SlotPostion, getGlassA, logInfo, ref logStr);
        //        }
        //        else
        //        {
        //            CreateMultiChamberPortTwoGlassGetCommand(ref logStr, currentLoadingPort, getGlassA, getGlassB, LowHand, UpHand, getModelPosition, getRobotModel, putRobotModel, EQPReciveSlotNoA, EQPReciveSlotNoB, SlotPostion);
        //        }
        //    }
        //    #endregion

        //}

        //private void CreateMultiChamberPortTwoGlassGetCommand(ref StringBuilder logStr, PortInfo currentLoadingPort, GlassInfo getGlassA, GlassInfo getGlassB, RobotHand lowHand, RobotHand upHand, int getModelPosition, RobotModel getRobotModel, RobotModel putRobotModel, int EQPReciveSlotNoA, int EQPReciveSlotNoB, int SlotPostion)
        //{
        //    string logInfo;
        //    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] getRobotModel:{0};OutPriority:{1}", getRobotModel.ModelName, getRobotModel.OutPriority);
        //    //第一次下命令PortGetSlot数量始终为1，需定义全局变量根据Robot取片结果上报层数更新PortGetSlot，并且下次PortGetSlot+1,初始赋值为0
        //    var cmd = factory.CreateMultiChamberPortGetCommand(currentLoadingPort, getModelPosition, putRobotModel.ModelPosition,
        //        getGlassA, getGlassB, lowHand, upHand, EQPReciveSlotNoA, EQPReciveSlotNoB, putRobotModel.TransferPriority, SlotPostion);

        //    cmd.GetGlassA = getGlassA;
        //    cmd.GetGlassB = getGlassB;
        //    cmd.STGetPosition1string = getRobotModel.ModelName;
        //    cmd.STPutPosition1string = putRobotModel.ModelName;
        //    AddCommand(cmd);
        //    Logger.InfoFormat("[MultiChamberPortGetCommand]   OK  Get{0}({1},{2}), Arm({3}), GlassA({4},{5}),GlassB({6},{7}), Position({8});Put{9}(ReciveSlotNo:{10},{11})",
        //         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo,
        //         getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STGetSlotPostion1, putRobotModel.ModelName, EQPReciveSlotNoA, EQPReciveSlotNoB);
        //    logInfo = string.Format("[MultiChamberPortGetCommand]   OK  Get{0}({1},{2}), Arm({3}), GlassA({4},{5}),GlassB({6},{7}), Position({8});Put{9}(ReciveSlotNo:{10},{11})",
        //         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo,
        //         getGlassB.CassetteSequenceNo, getGlassB.SlotSequenceNo, cmd.STGetSlotPostion1, putRobotModel.ModelName, EQPReciveSlotNoA, EQPReciveSlotNoB);
        //    logStr.Append(logInfo);
        //    logStr.AppendLine();
        //    // return logInfo;
        //}

        //private void CreateMultiChamberPortOneGlassGetCommand(RobotModel getRobotModel, int EQPReciveSlotNoB, int EQPReciveSlotNoA, PortInfo currentLoadingPort, int getModelPosition,
        //    RobotModel putRobotModel, int SlotPostion, GlassInfo getGlassA, string logInfo, ref StringBuilder logStr)
        //{
        //    #region 获取手臂  
        //    RobotHand getRobotA = RobotHand.Error;
        //    getRobotA = GetRobot(getRobotModel, getRobotA);
        //    if (getRobotA == RobotHand.Error)
        //    {
        //        Logger.Info("[CreateMultiChamberPortGetCommand] Check getRobotA.RobotHand NG; RobotHand.Error");
        //        return;
        //    }
        //    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] Check getRobotModel OK; getRobotModel:{0};getRobotA:{1};  ", getRobotModel.ModelName, getRobotA.ToString());
        //    #endregion

        //    Logger.InfoFormat("[CreateMultiChamberPortGetCommand] getRobotModel:{0};OutPriority:{1}", getRobotModel.ModelName, getRobotModel.OutPriority);
        //    //第一次下命令PortGetSlot数量始终为1，需定义全局变量根据Robot取片结果上报层数更新PortGetSlot，并且下次PortGetSlot+1,初始赋值为0
        //    int EQPReciveSlotNo = 0;
        //    if (HostInfo.Current.EQPID == "A1IMP100")
        //    {
        //        EQPReciveSlotNo = EQPReciveSlotNoB;
        //    }
        //    else
        //    {
        //        EQPReciveSlotNo = EQPReciveSlotNoA;
        //    }
        //    var cmd = factory.CreateMultiChamberPortGetCommand(currentLoadingPort, getModelPosition, putRobotModel.ModelPosition,
        //     getGlassA, getRobotA, EQPReciveSlotNo, putRobotModel.TransferPriority, SlotPostion);
        //    //cmd.GlassInfo = info;                    
        //    //GlassInfo GlassA = null;
        //    //GlassInfo GlassB = null;
        //    //GlassInfo Glass = null;
        //    //GetGlassByGlass(GlassInfos, getGlassA, ref GlassA, ref GlassB, ref Glass, "CreateMultiChamberPortGetCommand");
        //    cmd.GetGlass = getGlassA;
        //    //cmd.GetGlassA = GlassA;
        //    //cmd.GetGlassB = GlassB;

        //    cmd.STGetPosition1string = getRobotModel.ModelName;
        //    cmd.STPutPosition1string = putRobotModel.ModelName;
        //    AddCommand(cmd);
        //    Logger.InfoFormat("[MultiChamberPortGetCommand]   OK  Get{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6});Put{7}(ReciveSlotNo:{8})",
        //         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo,
        //         cmd.STGetSlotPostion1, putRobotModel.ModelName, EQPReciveSlotNo);
        //    logInfo = string.Format("[MultiChamberPortGetCommand]   OK  Get{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6});Put{7}(ReciveSlotNo:{8})",
        //         cmd.STRCMD1.ToString(), cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STArmNo1.ToString(), getGlassA.CassetteSequenceNo, getGlassA.SlotSequenceNo,
        //         cmd.STGetSlotPostion1, putRobotModel.ModelName, EQPReciveSlotNo);
        //    logStr.Append(logInfo);
        //    logStr.AppendLine();
        //    // return logInfo;
        //}

        //private void FindMultiChamberPutCommand(ref StringBuilder logStr)
        //{
        //    //20180801 lsq_Modify
        //    try
        //    {

        //        // Logger.Info("[FindMultiChamberPutCommand] begin");
        //        //Logger.InfoFormat("[FindMultiChamberPutCommand] ");
        //        //var dictionary = new Dictionary<RobotHand, GlassInfo>();
        //        #region 获取手臂及资料
        //        RobotHand UpHand = RobotHand.UpHand;
        //        GlassInfo UpHandGlass = null;
        //        RobotHand LowHand = RobotHand.LowHand;
        //        GlassInfo LowHandGlass = null;
        //        string logInfo = string.Format("");
        //        if (Robot.UpperExistOn)//上手臂是否存在panel
        //        {
        //            if (Robot.UpHandGlass1 != null)
        //            {
        //                //dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass1);
        //                //UpHand = RobotHand.UpHand;
        //                UpHandGlass = Robot.UpHandGlass1;
        //                //Logger.InfoFormat("[FindMultiChamberPutCommand] UpHandSubstrateID:{0},FetchDataTime:{1} ", Robot.UpHandGlass1.GlassID, Robot.UpHandGlass1.FetchDatetime.ToString());
        //                //Logger.InfoFormat("[FindMultiChamberPutCommand][LineMode!=OnlyA] [Robot.UpHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
        //            }
        //            else if (Robot.UpHandGlass2 != null)
        //            {
        //                //dictionary.Add(RobotHand.UpHand, Robot.UpHandGlass2);
        //                //RobotHand = RobotHand.UpHand;
        //                UpHandGlass = Robot.UpHandGlass2;
        //                //Logger.InfoFormat("[FindMultiChamberPutCommand] [LineMode!=OnlyA] [Robot.UpHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.UpHandGlass2.CassetteSequenceNo, Robot.UpHandGlass2.SlotSequenceNo);
        //            }
        //            else
        //            {
        //                // Logger.InfoFormat("[FindMultiChamberPutCommand] [LineMode!=OnlyA] [Robot.UpHandGlass1 and UpHandGlass2== null] ");
        //            }
        //        }
        //        else
        //        {
        //            // Logger.InfoFormat("[FindMultiChamberPutCommand] [LineMode!=OnlyA] [Robot.UpperExistOn==false] ");
        //        }
        //        if (Robot.LowerExistOn)//下手臂是否存在panel
        //        {
        //            if (Robot.LowHandGlass1 != null)
        //            {
        //                //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass1);
        //                // RobotHand = RobotHand.LowHand;
        //                LowHandGlass = Robot.LowHandGlass1;
        //                //Logger.InfoFormat("[FindMultiChamberPutCommand][LineMode!=OnlyA]  [Robot.LowHandGlass1 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
        //            }
        //            else if (Robot.LowHandGlass2 != null)
        //            {
        //                //dictionary.Add(RobotHand.LowHand, Robot.LowHandGlass2);
        //                //RobotHand = RobotHand.LowHand;
        //                LowHandGlass = Robot.LowHandGlass2;
        //                //Logger.InfoFormat("[FindMultiChamberPutCommand][LineMode!=OnlyA]  [Robot.LowHandGlass2 != null] GlassInfo glassInfo[{0},{1}] ", Robot.LowHandGlass2.CassetteSequenceNo, Robot.LowHandGlass2.SlotSequenceNo);
        //            }
        //            else
        //            {
        //                // Logger.InfoFormat("[FindMultiChamberPutCommand] [LineMode!=OnlyA] [Robot.LowHandGlass1 and LowHandGlass2== null] ");
        //            }
        //        }
        //        else
        //        {
        //            // Logger.InfoFormat("[FindMultiChamberPutCommand] [LineMode!=OnlyA] [Robot.LowerExistOn==false] ");
        //        }
        //        if (UpHandGlass == null && LowHandGlass == null)//都没有就不放
        //        {
        //            Logger.InfoFormat("[FindMultiChamberPutCommand] - UpHandGlass == null&& LowHandGlass==null");
        //            logInfo = string.Format("[MultiChamberPutCommand]   UpHandGlass == null&& LowHandGlass==null");
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        if (UpHandGlass != null)
        //        {
        //            Logger.InfoFormat("[FindMultiChamberPutCommand] .UpHandGlass[{0},{1}];", UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //        }
        //        if (LowHandGlass != null)
        //        {
        //            Logger.InfoFormat("[FindMultiChamberPutCommand] .LowHandGlass[{0},{1}];", LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //        }

        //        GlassInfo putGlass = null;
        //        RobotHand putHand = RobotHand.Error;
        //        if (UpHandGlass == null)
        //        {
        //            putGlass = LowHandGlass;
        //            putHand = RobotHand.LowHand;
        //        }
        //        else
        //        {
        //            putGlass = UpHandGlass;
        //            putHand = RobotHand.UpHand;
        //        }
        //        #endregion


        //        #region 获取targetStageList

        //        //GlassInfo putGlass = UpHandGlass == null ? LowHandGlass : UpHandGlass;
        //        var targetStageList = FindNextStage(putGlass);//根据上一站的unit或者eqp获取下一站的站点
        //        if (targetStageList.Count == 0)
        //        {
        //            Logger.InfoFormat("[FindMultiChamberPutCommand] - Check TargetStage NG.TargetStageList.Count==0");
        //            return;
        //        }
        //        else
        //        {
        //            //Logger.InfoFormat("[FindMultiChamberPutCommand] - Check targetStageList OK;  targetStageList.Count:{0} ", targetStageList.Count);
        //        }
        //        #endregion

        //        foreach (var targetStage in targetStageList)
        //        {
        //            if (!string.IsNullOrEmpty(targetStage.UnitName))
        //            {
        //                //RobotHand,



        //                if (targetStage.Type == EnumUnitType.Robot)
        //                {
        //                    RobotCommand cmd;
        //                    #region 获取targetModel
        //                    var targetModel = GetCurrentModel(targetStage.ModelPosition);
        //                    Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitID == targetModel.UnitID);
        //                    if (targetUnit == null)
        //                    {
        //                        Logger.InfoFormat("[FindMultiChamberPutCommand]  Check   targetUnit NG; targetModel[{0}];MultiChamberPutCommand NG ", targetModel.ModelName);
        //                        logInfo = string.Format("[MultiChamberPutCommand]   NG    Check  targetUnit NG;targetModel[{0}];  MultiChamberPutCommand NG", targetModel.ModelName);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    #endregion

        //                    #region 检查putglass
        //                    //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                    var putRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == putGlass.PortID);
        //                    if (putRobotModel == null)
        //                    {
        //                        Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check Action NG.Glass[{0},{1}],TargetRobotModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logInfo = string.Format("[  PutCommand Check]   NG  Glass[{0},{1}],TargetRobotModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    // logInfo = string.Format("[FindPutCommand] [CreatePortPutCommand] Check putRobotModel OK; robputRobotModelotmodel:{0} ", putRobotModel.ModelName);
        //                    if (putRobotModel.ModelPosition != targetStage.ModelPosition)
        //                    {
        //                        Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check TargetStage NG.ModelPosition:{0},Glass[{2},{3}],TargetModelPostion={1}", targetStage.ModelPosition, putRobotModel.ModelPosition, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logInfo = string.Format("[  PutCommand Check]   NG  ModelPosition:{0},Glass[{2},{3}],TargetModelPostion={1}", targetStage.ModelPosition, putRobotModel.ModelPosition, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    #endregion

        //                    #region 两张Glass
        //                    if (UpHandGlass != null && LowHandGlass != null)
        //                    {
        //                        #region 获取两个unloadPort
        //                        var upPort = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == UpHandGlass.PortID);
        //                        if (upPort == null)
        //                        {
        //                            Logger.InfoFormat("[FindPutCommand] - Check Port NG.UpHandGlass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", UpHandGlass.PortID, UpHandGlass.CassetteID, UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //                            logInfo = string.Format("[  PutCommand Check]   NG  UpHandGlass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", UpHandGlass.PortID, UpHandGlass.CassetteID, UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", UpHandGlass.PortID, UpHandGlass.CassetteID));
        //                            continue;
        //                        }
        //                        if (UpHandGlass.SlotSatus != EnumGlassSlotStatus.Processing)
        //                        {
        //                            Logger.InfoFormat("[FindPutCommand] - Check Action NG.UpHandGlass[{0},{1}] SlotSatus!=Processing", UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //                            logInfo = string.Format("[  PutCommand Check]   NG  UpHandGlass[{0},{1}] SlotSatus!=Processing", UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }
        //                        //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                        var upRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == UpHandGlass.PortID);
        //                        if (upRobotModel == null)
        //                        {
        //                            Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check Action NG.UpHandGlass[{0},{1}],TargetRobotModel is null", UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //                            logInfo = string.Format("[  PutCommand Check]   NG  UpHandGlass[{0},{1}],TargetRobotModel is null", UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }

        //                        //var lowPort = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == LowHandGlass.PortID);
        //                        //if (lowPort == null)
        //                        //{
        //                        //    Logger.InfoFormat("[FindPutCommand] - Check Port NG.LowHandGlass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", LowHandGlass.PortID, LowHandGlass.CassetteID, LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //                        //    logInfo = string.Format("[  PutCommand Check]   NG  LowHandGlass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", LowHandGlass.PortID, LowHandGlass.CassetteID, LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //                        //    logStr.Append(logInfo);
        //                        //    logStr.AppendLine();
        //                        //    eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", LowHandGlass.PortID, LowHandGlass.CassetteID));
        //                        //    continue;
        //                        //}
        //                        if (LowHandGlass.SlotSatus != EnumGlassSlotStatus.Processing)
        //                        {
        //                            Logger.InfoFormat("[FindPutCommand] - Check Action NG.LowHandGlass[{0},{1}] SlotSatus!=Processing", LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //                            logInfo = string.Format("[  PutCommand Check]   NG  LowHandGlass[{0},{1}] SlotSatus!=Processing", LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }
        //                        //Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] Check targetUnit OK; targetUnit:{0}", targetUnit.UnitName);
        //                        var lowRobotModel = targetUnit.RobotModelList.FirstOrDefault(o => o.PortID == LowHandGlass.PortID);
        //                        if (lowRobotModel == null)
        //                        {
        //                            Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Check Action NG.LowHandGlass[{0},{1}],TargetRobotModel is null", LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //                            logInfo = string.Format("[  PutCommand Check]   NG  LowHandGlass[{0},{1}],TargetRobotModel is null", LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                            continue;
        //                        }

        //                        #endregion

        //                        // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
        //                        cmd = factory.CreateMultiChamberPortPutCommand(upPort, upRobotModel.ModelPosition, lowRobotModel.ModelPosition, UpHandGlass, LowHandGlass, UpHand, LowHand, putRobotModel.INPriority, 0);
        //                        //cmd.GlassInfo = handJob;
        //                        cmd.PutGlassA = UpHandGlass;
        //                        cmd.PutGlassB = LowHandGlass;
        //                        cmd.STPutPosition1string = upRobotModel.ModelName;
        //                        cmd.NDPutPosition2string = lowRobotModel.ModelName;
        //                        AddCommand(cmd);
        //                        Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), UpHandGlass({4},{5}),LowHandGlass({6},{7}), Position({8});",
        //                                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo, LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logInfo = string.Format("[  PutCommand Check]   OK  {0}({1},{2}), Arm({3}), UpHandGlass({4},{5}),LowHandGlass({6},{7}), Position({8});",
        //                                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo, LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        return;
        //                    }
        //                    #endregion
        //                    #region 单张Glass

        //                    #region 校验put Stage
        //                    if (putGlass.SlotSatus != EnumGlassSlotStatus.Processing)
        //                    {
        //                        Logger.InfoFormat("[FindPutCommand] - Check Action NG.Glass[{0},{1}] SlotSatus!=Processing", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logInfo = string.Format("[  PutCommand Check]   NG  Glass[{0},{1}] SlotSatus!=Processing", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    var unloadport = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlass.PortID);
        //                    if (unloadport == null)
        //                    {
        //                        Logger.InfoFormat("[FindPutCommand] - Check Port NG.Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", putGlass.PortID, putGlass.CassetteID, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logInfo = string.Format("[  PutCommand Check]   NG  Glass[{2},{3}],Port=[{0}],CSTID={1},Not Exist Put Port", putGlass.PortID, putGlass.CassetteID, putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        eqpCmd.SendAllEQPCIMMessage(string.Format("PORT PUT Dispatch Error,Port{0};CSTID{1}", putGlass.PortID, putGlass.CassetteID));
        //                        continue;
        //                    }
        //                    // Logger.InfoFormat("[FindPutCommand] putGlass.SlotSatus== EnumGlassSlotStatus.Processing; putGlass[{0},{1}]", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);                           
        //                    #endregion

        //                    //Logger.InfoFormat("[FindPutCommand]Check RobotHand OK ;Put Stage OK; RobotHand:{0}; putRobotModel:{1}", RobotHand.ToString(), putRobotModel.ModelName);
        //                    #region 获取 SlotPostion
        //                    int SlotPostion = 0;
        //                    #endregion

        //                    #region 生成命令
        //                    // Logger.InfoFormat("[FindPutCommand] [CreatePortPutCommand] LineMode != LineMode.OnlyA; putRobotModel:{0};putRobotModel.INPriority:{1}", putRobotModel.ModelName, putRobotModel.INPriority);
        //                    cmd = factory.CreatePortPutCommand(unloadport, putRobotModel.ModelPosition, putGlass, putHand, putRobotModel.INPriority, SlotPostion);
        //                    //cmd.GlassInfo = handJob;
        //                    cmd.PutGlass = putGlass;
        //                    cmd.STPutPosition1string = putRobotModel.ModelName;
        //                    AddCommand(cmd);
        //                    Logger.InfoFormat("[FindPutCommand][CreatePortPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), putGlass({4},{5}), Position({6});",
        //                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                    logInfo = string.Format("[  PutCommand Check]   OK  {0}({1},{2}), Arm({3}), putGlass({4},{5}),  Position({6});",
        //                                    cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                    logStr.Append(logInfo);
        //                    logStr.AppendLine();
        //                    #endregion
        //                    #endregion

        //                }
        //                else
        //                {

        //                    RobotModel putRobotModel = null;
        //                    Unit putUnit = null;
        //                    int EQPReciveSlotNoA = 0;
        //                    int EQPReciveSlotNoB = 0;
        //                    //FindMultiChamberPutCommand(RobotHand, putGlass, targetModel,  ref logStr);//放设备

        //                    Unit targetUnit = HostInfo.Current.EQPInfo.Units.FirstOrDefault(o => o.UnitName == targetStage.UnitName);
        //                    if (targetUnit.GetType().Name == "Unit" && targetUnit.RobotModelList.Count > 0)
        //                    {

        //                        foreach (var unitRobotModel in targetUnit.RobotModelList)
        //                        {
        //                            if (unitRobotModel.ModelPosition == targetStage.PathConfigure.TargetPathName)
        //                            {
        //                                #region 校验DownLink
        //                                Linksignal downLinksignal = StageLinksignalList.FirstOrDefault(o => o.UnitName == targetUnit.UnitName && o.LinkName == unitRobotModel.DownLinkName);
        //                                if (downLinksignal == null)
        //                                {
        //                                    Logger.InfoFormat("[FindMultiChamberPutCommand]  - Check Condition NG.DownLinksignal is null");
        //                                    logInfo = string.Format("[MultiChamberPutCommand]   NG   DownLinksignal is null");
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                bool checkLink = false;
        //                                checkLink = CheckLinkStatusReceive(downLinksignal, targetUnit, ref logStr);
        //                                if (!checkLink)//校验下游收片信号
        //                                {
        //                                    //Logger.InfoFormat("[CreateMultiChamberPortGetCommand][IsPutWaitCommand:{0}] - DownstreamLinkSignal Check NG; LinkName:{1};ProcessPutCommand NG", IsPutWait, downLinksignal.LinkName);
        //                                    logInfo = string.Format("[MultiChamberPutCommand]   NG  Check {0} Condition NG", downLinksignal.LinkName);
        //                                    logStr.Append(logInfo);
        //                                    logStr.AppendLine();
        //                                    continue;
        //                                }
        //                                if (checkLink)
        //                                {
        //                                    if (unitRobotModel.EQPReciveSlotNoA == 0)
        //                                    {
        //                                        Logger.InfoFormat("[CreateMultiChamberPutCommand]  - Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //                                        logInfo = string.Format("[MultiChamberPutCommand]   Check unitRobotModel.EQPReciveSlotNoA==0;unitRobotModel:{0}", unitRobotModel.ModelName);
        //                                        logStr.Append(logInfo);
        //                                        logStr.AppendLine();
        //                                        continue;
        //                                    }
        //                                    putRobotModel = unitRobotModel;
        //                                    putUnit = targetUnit;

        //                                    EQPReciveSlotNoA = unitRobotModel.EQPReciveSlotNoA;
        //                                    EQPReciveSlotNoB = unitRobotModel.EQPReciveSlotNoB;
        //                                    break;
        //                                }
        //                                #endregion
        //                            }

        //                        }
        //                        //if (putUnit != null)
        //                        //{
        //                        //    continue;
        //                        //}
        //                    }
        //                    if (putRobotModel == null)
        //                    {
        //                        Logger.InfoFormat("[FindMultiChamberPutCommand] - Check Condition NG.putRobotModell is null");
        //                        logInfo = string.Format("[MultiChamberPutCommand]   NG   putRobotModell is null");
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();
        //                        continue;
        //                    }
        //                    /// Logger.InfoFormat("[FindMultiChamberPutCommand] DownstreamLinkSignal Check OK; LinkName:{0}", downLinksignal.LinkName);
        //                    //Logger.InfoFormat("[FindMultiChamberPutCommand] Check RobotModel OK; RobotModel:{0}", targetmode.ModelName);
        //                    #region 获取SlotPostion
        //                    int SlotPostion = 0;
        //                    #endregion

        //                    #region 生成命令
        //                    if (UpHandGlass != null && LowHandGlass != null)
        //                    {
        //                        RobotCommand cmd = null;
        //                        // Logger.InfoFormat("[FindMultiChamberPutCommand][PutCommand] targetmode:{0};targetmode.INPriority{1} ", targetmode.ModelName, targetmode.INPriority);
        //                        cmd = factory.CreateMultiChamberProcessPutCommand(targetUnit, EQPReciveSlotNoA, EQPReciveSlotNoB, putRobotModel.INPriority, putRobotModel.ModelPosition, SlotPostion);
        //                        Logger.InfoFormat("[FindMultiChamberPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), UpHandGlass({4},{5}),LowHandGlass({6},{7}), Position({8})",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo, LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logInfo = string.Format("[MultiChamberPutCommand]   OK  {0}({1},{2}), Arm({3}), UpHandGlass({4},{5}),LowHandGlass({6},{7}), Position({8})",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), UpHandGlass.CassetteSequenceNo, UpHandGlass.SlotSequenceNo, LowHandGlass.CassetteSequenceNo, LowHandGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();

        //                        cmd.PutGlassA = UpHandGlass;
        //                        cmd.PutGlassB = LowHandGlass;
        //                        cmd.STPutPosition1string = putRobotModel.ModelName;
        //                        AddCommand(cmd);
        //                    }
        //                    else
        //                    {
        //                        int EQPReciveSlotNo = 0;
        //                        if (HostInfo.Current.EQPID == "A1IMP100")
        //                        {
        //                            EQPReciveSlotNo = EQPReciveSlotNoB;
        //                        }
        //                        else
        //                        {
        //                            EQPReciveSlotNo = EQPReciveSlotNoA;
        //                        }
        //                        RobotCommand cmd = null;
        //                        // Logger.InfoFormat("[FindMultiChamberPutCommand][PutCommand] targetmode:{0};targetmode.INPriority{1} ", targetmode.ModelName, targetmode.INPriority);
        //                        cmd = factory.CreateMultiChamberProcessPutCommand(targetUnit, putGlass, putHand, putRobotModel.INPriority, putRobotModel.ModelPosition, EQPReciveSlotNo, SlotPostion);
        //                        Logger.InfoFormat("[FindMultiChamberPutCommand] - Create Action OK.{0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logInfo = string.Format("[MultiChamberPutCommand]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //                        cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();

        //                        cmd.PutGlass = putGlass;
        //                        cmd.STPutPosition1string = putRobotModel.ModelName;
        //                        AddCommand(cmd);
        //                    }
        //                    #endregion
        //                }
        //            }
        //        }
        //        // Logger.Info("[FindMultiChamberPutCommand] end");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //}


        //private void AbortMultiChamberCommand(ref StringBuilder logStr)
        //{
        //    try
        //    {
        //        //Logger.Info("[AbortCommand] begin");
        //        string logInfo = string.Format("");
        //        //判断Robot手臂上是否有玻璃
        //        #region 校验手臂
        //        if (!Robot.UpperExistOn && !Robot.LowerExistOn)
        //        {
        //            Logger.Info("[AbortMultiChamberCommand] - Check Arm NG.UpperExistOn=false,LowerExistOn=false ");
        //            logInfo = string.Format("[AbortMultiChamberCommand Check]   NG  UpperExistOn=false,LowerExistOn=false");
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        //Logger.Info("[AbortCommand] Check UpperExistOn,LowerExistOn OK;UpperExistOn=true;LowerExistOn=true ");

        //        #endregion

        //        //Dictionary<RobotHand,GlassInfo> HandJobDataDic = new Dictionary<RobotHand, GlassInfo>();
        //        #region 获取手臂和putGlass
        //        RobotHand RobotHandA = RobotHand.Error;
        //        GlassInfo putGlassA = null;

        //        RobotHand RobotHandB = RobotHand.Error;
        //        GlassInfo putGlassB = null;
        //        if (Robot.LowerExistOn)
        //        {
        //            if (Robot.LowHandGlass1 != null)
        //            {
        //                //HandJobDataDic.Add(RobotHand.LowHand, Robot.LowHandGlass1);
        //                RobotHandA = RobotHand.LowHand;
        //                putGlassA = Robot.LowHandGlass1;
        //                // Logger.InfoFormat("[AbortCommand] [Robot.LowHandGlass1 != null] LowHandGlass1  glassInfo[{0},{1}]", Robot.LowHandGlass1.CassetteSequenceNo, Robot.LowHandGlass1.SlotSequenceNo);
        //            }
        //            else if (Robot.LowHandGlass2 != null)
        //            {
        //                //HandJobDataDic.Add(RobotHand.LowHand, Robot.LowHandGlass2);
        //                RobotHandA = RobotHand.LowHand;
        //                putGlassA = Robot.LowHandGlass2;
        //                // Logger.InfoFormat("[AbortCommand] [Robot.LowHandGlass2 != null] LowHandGlass2  glassInfo[{0},{1}]", Robot.LowHandGlass2.CassetteSequenceNo, Robot.LowHandGlass2.SlotSequenceNo);
        //            }
        //            else
        //            {
        //                // Logger.InfoFormat("[AbortCommand][Robot.LowerExistOn] LowHandGlass1 and LowHandGlass2 ==null ");
        //            }
        //        }
        //        else if (Robot.UpperExistOn)
        //        {
        //            if (Robot.UpHandGlass1 != null)
        //            {
        //                //HandJobDataDic.Add(RobotHand.UpHand, Robot.UpHandGlass1);
        //                RobotHandB = RobotHand.UpHand;
        //                putGlassB = Robot.UpHandGlass1;
        //                //  Logger.InfoFormat("[AbortCommand] [Robot.UpHandGlass1 != null] UpHandGlass1  glassInfo[{0},{1}]", Robot.UpHandGlass1.CassetteSequenceNo, Robot.UpHandGlass1.SlotSequenceNo);
        //            }
        //            else if (Robot.UpHandGlass2 != null)
        //            {
        //                //HandJobDataDic.Add(RobotHand.UpHand, Robot.UpHandGlass2);
        //                RobotHandB = RobotHand.UpHand;
        //                putGlassB = Robot.UpHandGlass2;
        //                //  Logger.InfoFormat("[AbortCommand] [Robot.UpHandGlass2 != null] UpHandGlass2  glassInfo[{0},{1}]", Robot.UpHandGlass2.CassetteSequenceNo, Robot.UpHandGlass2.SlotSequenceNo);
        //            }
        //            else
        //            {
        //                // Logger.InfoFormat("[AbortCommand][Robot.UpperExistOn] UpHandGlass1 and UpHandGlass2 ==null ");
        //            }
        //        }
        //        else
        //        {
        //            // Logger.InfoFormat("[AbortCommand][Robot.LowerExistOn==false][Robot.UpperExistOn==false] ");
        //        }
        //        #endregion
        //        GlassInfo putGlass = putGlassA == null ? putGlassB : putGlassA;
        //        if (putGlass == null)
        //        {
        //            Logger.Info("[AbortMultiChamberCommand] - Check putGlass NG.putGlass=null ");
        //            logInfo = string.Format("[AbortMultiChamberCommand Check]   Check putGlass NG.putGlass=null ");
        //            logStr.Append(logInfo);
        //            logStr.AppendLine();
        //            return;
        //        }
        //        Logger.InfoFormat("[AbortMultiChamberCommand] - Check Action.Glass[{0},{1}],RobotArm({2})", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, RobotHandA.ToString());
        //        var port = HostInfo.Current.PortList.FirstOrDefault(o => o.PortID == putGlass.PortID);
        //        if (port != null)
        //        {
        //            if (port.CassetteInfo.CassetteStatus == EnumCarrierStatus.CassetteProcessAbort || HostInfo.Current.EQPInfo.LineMode == LineMode.MultiChamberForceCleanOut)
        //            {
        //                if (HostInfo.Current.EQPInfo.LineMode == LineMode.MultiChamberForceCleanOut)
        //                {
        //                    Logger.InfoFormat("[AbortCommand] - LineMode ==MultiChamberForceCleanOut");
        //                    logInfo = AbortMultiChamberCommand(logStr, RobotHandA, putGlassA, RobotHandB, putGlassB, port);
        //                }
        //                else
        //                {
        //                    Logger.InfoFormat("[AbortCommand] - LineMode !=MultiChamberForceCleanOut");
        //                    #region 获取currentModel                        
        //                    RobotModel currentModel = GetCurrentModel(putGlass.ModelPosition);
        //                    if (currentModel == null)
        //                    {
        //                        Logger.InfoFormat("[AbortMultiChamberCommand] - Check RobotModel NG.Glass[{0},{1}],TargetModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logInfo = string.Format("[AbortMultiChamberCommand Check]   NG  Glass[{0},{1}],TargetModel is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                        logStr.Append(logInfo);
        //                        logStr.AppendLine();

        //                    }
        //                    #endregion
        //                    else
        //                    {

        //                        //Logger.InfoFormat("[AbortCommand] Check robotModel OK; targetModel:{0}", targetModel.ModelName);
        //                        if (!string.IsNullOrEmpty(currentModel.PortID))
        //                        {

        //                            Logger.InfoFormat("[AbortMultiChamberCommand] - Check Action OK;  Glass[{0},{1}],currentModel.PortID:{3};currentModel:{4}", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, currentModel.PortID, currentModel.ModelName);
        //                            //发送命令让glass返回原卡夹
        //                            logInfo = AbortMultiChamberCommand(logStr, RobotHandA, putGlassA, RobotHandB, putGlassB, port);
        //                        }
        //                        else
        //                        {
        //                            Logger.InfoFormat("[AbortMultiChamberCommand] - Check Action NG.Glass[{0},{1}],currentModel.PortID is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                            logInfo = string.Format("[AbortMultiChamberCommand Check]   NG  Glass[{0},{1}],currentModel.PortID is null", putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo);
        //                            logStr.Append(logInfo);
        //                            logStr.AppendLine();
        //                        }
        //                    }

        //                }


        //            }
        //        }

        //        //Logger.Info("[AbortCommand] end");
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //        Logger.Info(ex);
        //    }
        //}
        //private string AbortMultiChamberCommand(StringBuilder logStr, RobotHand RobotHandA, GlassInfo putGlassA, RobotHand RobotHandB, GlassInfo putGlassB, PortInfo port)
        //{
        //    string logInfo;
        //    #region 获取SlotPostion
        //    int SlotPostion = 0;

        //    #endregion
        //    GlassInfo putGlass = putGlassA == null ? putGlassB : putGlassA;
        //    RobotHand RobotHand = putGlassA == null ? RobotHandB : RobotHandA;
        //    var model = GetCurrentModel(putGlass.PortID);
        //    #region 生成命令
        //    RobotCommand cmd = null;
        //    if (putGlassB != null && putGlassA != null)
        //    {
        //        #region 获取putModel                
        //        var putModelA = GetCurrentModel(putGlassA.PortID);
        //        var putModelB = GetCurrentModel(putGlassB.PortID);
        //        #endregion
        //        cmd = factory.CreateMultiChamberPortPutCommand(port, putModelA.ModelPosition, putModelB.ModelPosition, putGlassA, putGlassB, RobotHandA, RobotHandB, 999, SlotPostion);
        //    }
        //    else
        //    {
        //        cmd = factory.CreatePortPutCommand(port, model.ModelPosition, putGlass, RobotHand, 999, SlotPostion);
        //    }


        //    //cmd.GlassInfo = putGlass;
        //    var glassPort = HostInfo.Current.PortList.FirstOrDefault(o => o.CassetteSequenceNo == putGlass.CassetteSequenceNo);
        //    if (glassPort != null)
        //    {
        //        var glassList = glassPort.GlassInfos.ToList();
        //        GlassInfo GlassA = null;
        //        GlassInfo GlassB = null;
        //        GlassInfo Glass = null;
        //        GetGlassByGlass(glassList, putGlass, ref GlassA, ref GlassB, ref Glass, "AbortCommand");
        //        cmd.PutGlass = Glass;
        //        cmd.PutGlassA = GlassA;
        //        cmd.PutGlassB = GlassB;
        //    }
        //    else
        //    {
        //        cmd.PutGlass = putGlass;
        //    }

        //    //var PutPosition1 = GetCurrentModel(putGlass.ModelPosition);
        //    cmd.STPutPosition1string = model.ModelName;
        //    AddCommand(cmd);
        //    Logger.InfoFormat("[AbortCommand] [CreatePortPutCommand] - {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //            cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //    logInfo = string.Format("[AbortCommand Check]   OK  {0}({1},{2}), Arm({3}), Glass({4},{5}), Position({6})",
        //            cmd.STRCMD1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STArmNo1.ToString(), putGlass.CassetteSequenceNo, putGlass.SlotSequenceNo, cmd.STPutSlotPostion1);
        //    logStr.Append(logInfo);
        //    logStr.AppendLine();
        //    #endregion
        //    return logInfo;
        //}
        //#endregion
        //private RobotHand GetRobot(RobotModel getRobotModel, RobotHand RobotHand, RobotModel NextModel)
        //{
        //    if (NextModel.PutArm == 0)
        //    {
        //        if (getRobotModel.GetArm != 0)
        //        {
        //            RobotHand = (RobotHand)getRobotModel.GetArm;
        //            if (RobotHand == RobotHand.LowHand && robot.LowerExistOn)
        //            {
        //                RobotHand = RobotHand.Error;
        //                Logger.InfoFormat("[GetRobotHand] - Check RobotHand NG.getRobotModel={0};GetArm is LowHand ;  LowerExistOn", getRobotModel.ModelName);
        //            }
        //            else if (RobotHand == RobotHand.UpHand && robot.UpperExistOn)
        //            {
        //                RobotHand = RobotHand.Error;
        //                Logger.InfoFormat("[GetRobotHand] - Check RobotHand NG.getRobotModel={0};GetArm is UpHand ;  UpperExistOn", getRobotModel.ModelName);
        //            }
        //        }
        //        else
        //        {
        //            if (!robot.LowerExistOn)
        //                RobotHand = RobotHand.LowHand;
        //            else if (!robot.UpperExistOn)
        //                RobotHand = RobotHand.UpHand;
        //        }
        //    }
        //    else
        //    {
        //        RobotHand = (RobotHand)NextModel.PutArm;
        //        if (RobotHand == RobotHand.LowHand && robot.LowerExistOn)
        //        {
        //            RobotHand = RobotHand.Error;
        //            Logger.InfoFormat("[GetRobotHand] - Check RobotHand NG.NextModel={0};PutArm is LowHand ;  LowerExistOn", NextModel.ModelName);
        //        }
        //        else if (RobotHand == RobotHand.UpHand && robot.UpperExistOn)
        //        {
        //            RobotHand = RobotHand.Error;
        //            Logger.InfoFormat("[GetRobotHand] - Check RobotHand NG.NextModel={0};PutArm is UpHand ;  UpperExistOn", NextModel.ModelName);
        //        }
        //    }

        //    return RobotHand;
        //}
        private RobotHand GetRobot(RobotModel getRobotModel, RobotHand RobotHand)
        {
            if (getRobotModel.GetArm != 0 && getRobotModel.GetArm != 99)
            {
                RobotHand = (RobotHand)getRobotModel.GetArm;
                if (RobotHand == RobotHand.LowHand && robot.LowerExistOn)
                {
                    RobotHand = RobotHand.Error;
                    Logger.InfoFormat("[GetRobotHand] - Check RobotHand NG.getRobotModel={0};GetArm is LowHand ;  LowerExistOn", getRobotModel.ModelName);
                }
                else if (RobotHand == RobotHand.UpHand && robot.UpperExistOn)
                {
                    RobotHand = RobotHand.Error;
                    Logger.InfoFormat("[GetRobotHand] - Check RobotHand NG.getRobotModel={0};GetArm is UpHand ;  UpperExistOn", getRobotModel.ModelName);
                }
            }
            else
            {
                if (!robot.LowerExistOn && !robot.UpperExistOn)
                    RobotHand = RobotHand.AllArm;
                else if (!robot.LowerExistOn)
                    RobotHand = RobotHand.LowHand;
                else if (!robot.UpperExistOn)
                    RobotHand = RobotHand.UpHand;
            }

            return RobotHand;
        }
        public bool CheckModelPutArm(RobotModel targetModel, RobotHand RobotHand)
        {
            bool result = true;
            if (targetModel != null)
            {
                if (targetModel.PutArm != 0 && targetModel.PutArm != 99)
                {
                    if (RobotHand.GetHashCode() != targetModel.PutArm)
                    {
                        result = false;
                        //Logger.InfoFormat("[FindPutCommand] Check RobotHand NG;  RobotHand != targetModel.PutArm;RobotHand:{0};targetModel.PutArm:{1} ", RobotHand.GetHashCode(), targetModel.PutArm);
                        return result;
                    }
                }
            }
            return result;
        }

        //#endregion
    }
}