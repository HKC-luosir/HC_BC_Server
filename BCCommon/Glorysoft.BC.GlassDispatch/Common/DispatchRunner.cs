using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Glorysoft.BC.Entity;
using log4net;

namespace  Glorysoft.BC.GlassDispath
{
    internal class DispatchRun
    {
        private readonly AbstractDispatch dispatch;
        private readonly ILog logger;
        private readonly Thread dispathTread;
        private bool running;
        private int logCount;
        private bool bStatusChangeFlag;
        private DateTime actionStartTime = DateTime.Now;
        public DispatchRun(ILog logger, AbstractDispatch dispatch)
        {
            this.logger = logger;
            this.dispatch = dispatch;
            dispathTread = new Thread(ThreadFunc);
        }
        //搜寻命令
        private void ThreadFunc()
        {
            Thread.Sleep(5000);
            while (running)
            {
                try
                {
                    #region 需求4.2增加Robot线程执行Log
                    logger.InfoFormat("Robot ThreadFunc Start");
                    #endregion
                    //dispatch.Robot.RobotDispatchMode = EnumRobotDispatchMode.AUTO;
                    //校验RobotDispatchMode是手动还是自动
                    if (!dispatch.IsAutoDispatch || HostInfo.Current.EQPInfo.RobotDispatchMode == EnumEqpAutoMode.MANUAL)
                    {
                        logCount = logCount + 1;
                        if (logCount > 10)
                        {
                            logCount = 0;
                            logger.InfoFormat("Dispatching Manual");
                        }
                    }
                    //else if(HostInfo.Current.EQPInfo.LoadingStop)
                    //{
                    //    logCount = logCount + 1;
                    //    if (logCount == 10)
                    //    {                          
                    //        logger.InfoFormat("Dispatching LoadingStop");
                    //    }
                    //}
                    else
                    {
                        //( dispatch.Robot.CurrentState = RobotState.WaitingForCmd;)可能有问题、该信号应该由设备上报修改
                        dispatch.Robot.CurrentState = RobotState.WaitingForCmd;
                        //判断Robot状态是否可以接受命令
                        if (dispatch.Robot.CurrentState != RobotState.WaitingForCmd)
                        {
                            logCount = logCount + 1;
                            if (logCount > 10)
                            {
                                logCount = 0;
                                logger.InfoFormat("Robot Status Not Ready For Command");
                            }
                        }
                        else
                        {
                            //Robot执行命令或等待执行命令
                            if (dispatch.Robot.CommandExecuting || dispatch.Robot.IsWaitCmdCode)
                            {
                                logCount = logCount + 1;
                                if (logCount > 10)
                                {
                                    logCount = 0;
                                    logger.InfoFormat("Robot Command Executing");
                                }
                                if (!bStatusChangeFlag)
                                {
                                    actionStartTime = DateTime.Now;
                                    bStatusChangeFlag = true;
                                }
                                var now = DateTime.Now;
                                //if (now.Subtract(actionStartTime).TotalSeconds > 120.0)
                                //{
                                //    logger.InfoFormat("Do not Receive Command Result But 60 Second Over... Search Command");
                                //    dispatch.Robot.CommandExecuting = false;
                                //    if (dispatch.Robot.IsWaitCmdCode)
                                //        dispatch.Robot.IsWaitCmdCode = false;
                                //    bStatusChangeFlag = false;
                                //}
                            }
                            else
                            {
                                //搜寻命令
                                logCount = 0;
                                SearchRobotCommand();
                            }
                        }
                    }
                    Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    logger.ErrorFormat("Robot Dispatch: {0}",ex);
                }
            }
            #region 需求4.2增加Robot线程执行Log
            logger.InfoFormat("Robot ThreadFunc End");
            #endregion
        }
        public void Start()
        {
            running = true;
            dispathTread.Start();
        }
        public void Stop()
        {
            running = false;
        }

        private void SearchRobotCommand()
        {
            try
            {
                StringBuilder logStr = new StringBuilder();         
                logStr.Append("[SearchRobotCommand][result] ***************Robot Dispatch Reuslt*************** ");
                var lst = dispatch.Dispatch(ref logStr);//查询是否可以取片或者放片              
                if (lst.Count==0)
                {
                    logStr.AppendFormat("[SearchRobotCommand]   NG  Check CommandList.Count==0");
                    logStr.AppendLine();
                }
                //StringBuilder stringBuilder = new StringBuilder();

                /*下面执行后有打日志 这里先暂时屏蔽不打日志
                foreach (var item in lst)
                {
                    //item.NDGetPosition2  
                    StringBuilder log = new StringBuilder();
                    log.AppendFormat("[SearchRobotCommand]   OK  [Priority:{16}] CMD({0}), Arm({1}), PutPostion1({2},{3},{4}), GetPosition({5},{6},{7}) + CMD2({8}), Arm({9}), PutPosition({10},{11},{12}), GetPosition({13},{14},{15})",
                        item.STRCMD1.ToString(), item.STArmNo1.ToString(),  item.STPutPosition1string, item.STPutSlotNo1, item.STPutSlotPostion1, item.STGetPosition1string, item.STGetSlotNo1, item.STGetSlotPostion1, item.NDRCMD2.ToString(), item.NDArmNo2.ToString(), item.NDPutPosition2string, item.NDPutSlotNo2, item.NDPutSlotPostion2, item.NDGetPosition2string, item.NDGetSlotNo2, item.NDGetSlotPostion2, item.Priority);
                    if(item.GetGlass!=null)
                    {
                        log.AppendFormat("GetGlass({0},{1})", item.GetGlass.CassetteSequenceNo, item.GetGlass.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("GetGlass(0,0)");
                    }
                    if (item.GetGlassA != null)
                    {
                        log.AppendFormat("GetGlassA({0},{1})", item.GetGlassA.CassetteSequenceNo, item.GetGlassA.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("GetGlassA(0,0)");
                    }
                    if (item.GetGlassB != null)
                    {
                        log.AppendFormat("GetGlassB({0},{1})", item.GetGlassB.CassetteSequenceNo, item.GetGlassB.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("GetGlassB(0,0)");
                    }
                    if (item.PutGlass != null)
                    {
                        log.AppendFormat("PutGlass({0},{1})", item.PutGlass.CassetteSequenceNo, item.PutGlass.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("PutGlass(0,0)");
                    }
                    if (item.PutGlassA != null)
                    {
                        log.AppendFormat("PutGlassA({0},{1})", item.PutGlassA.CassetteSequenceNo, item.PutGlassA.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("PutGlassA(0,0)");
                    }
                    if (item.PutGlassB != null)
                    {
                        log.AppendFormat("PutGlassB({0},{1})", item.PutGlassB.CassetteSequenceNo, item.PutGlassB.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("PutGlassB(0,0)");
                    }
                    logStr.AppendFormat(log.ToString());
                    logStr.AppendLine();
                   
                }
                */
                RobotCommand cmd = null;
                //if (HostInfo.Current.EQPID == "A1ANH100")
                //{
                //    //校验old优先级是否出现在本次.HostInfo.Current.OldPriority
                //    //如果出现本次还用该优先级命令
                //    cmd = lst.FirstOrDefault(o => o.Priority == HostInfo.Current.OldPriority.OldPriority && o.TargetModelPosition == HostInfo.Current.OldPriority.TargetModelPosition);
                //    //如果未出现使用优先级排序并更新old优先级
                //    if (cmd == null&&lst.Count >0)
                //    {
                //        cmd = lst.FirstOrDefault(o => o.Priority == HostInfo.Current.OldPriority.OldPriority);
                //        if (cmd == null && lst.Count > 0)
                //        {
                //            cmd = GetMaxPriorityCommand(lst);//根据优先级做动作
                //            HostInfo.Current.OldPriority.OldPriority = cmd.Priority;
                //            HostInfo.Current.OldPriority.TargetModelPosition = cmd.TargetModelPosition;
                //            //modify db OldPriority
                //            dispatch.ModifyDBOldPriority();
                //            logStr.AppendFormat("[ModifyDBOldPriority]   NewPriority:{0} NewTargetModelPosition:{1}", cmd.Priority, cmd.TargetModelPosition);
                //            logStr.AppendLine();
                //        }

                //    }
                //}
                //else
                //{
                    cmd = GetMaxPriorityCommand(lst);//根据优先级做动作
                //}
                if (cmd != null)
                {

                    //if (dispatch.Robot.ExecCommand == null)
                    //{
                        //logger.InfoFormat("Unit {1} Command: {0}", cmd, cmd.Unit.UnitName);
                        dispatch.SendCommand(cmd);//执行取片或者放片
                    //}
                    //else
                    //{
                        //logger.InfoFormat("Unit {1} Command: {0}", cmd, cmd.Unit.UnitName);
                        //dispatch.SendCommand(cmd);//执行取片或者放片
                    //}


                    StringBuilder log = new StringBuilder();
                    log.AppendFormat("[SelectRobotCommand]   OK  [Priority:{16}] CMD({0}), Arm({1}), PutPostion1({2},{3},{4}), GetPosition({5},{6},{7}) + CMD2({8}), Arm({9}), PutPosition({10},{11},{12}), GetPosition({13},{14},{15})",
                         cmd.STRCMD1.ToString(), cmd.STArmNo1.ToString(), cmd.STPutPosition1string, cmd.STPutSlotNo1, cmd.STPutSlotPostion1, cmd.STGetPosition1string, cmd.STGetSlotNo1, cmd.STGetSlotPostion1, cmd.NDRCMD2.ToString(), cmd.NDArmNo2.ToString(), cmd.NDPutPosition2string, cmd.NDPutSlotNo2, cmd.NDPutSlotPostion2, cmd.NDGetPosition2string, cmd.NDGetSlotNo2, cmd.NDGetSlotPostion2, cmd.Priority);
                    if (cmd.GetGlass != null)
                    {
                        log.AppendFormat("GetGlass({0},{1})", cmd.GetGlass.CassetteSequenceNo, cmd.GetGlass.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("GetGlass(0,0)");
                    }
                    if (cmd.GetGlassA != null)
                    {
                        log.AppendFormat("GetGlassA({0},{1})", cmd.GetGlassA.CassetteSequenceNo, cmd.GetGlassA.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("GetGlassA(0,0)");
                    }
                    if (cmd.GetGlassB != null)
                    {
                        log.AppendFormat("GetGlassB({0},{1})", cmd.GetGlassB.CassetteSequenceNo, cmd.GetGlassB.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("GetGlassB(0,0)");
                    }
                    if (cmd.PutGlass != null)
                    {
                        log.AppendFormat("PutGlass({0},{1})", cmd.PutGlass.CassetteSequenceNo, cmd.PutGlass.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("PutGlass(0,0)");
                    }
                    if (cmd.PutGlassA != null)
                    {
                        log.AppendFormat("PutGlassA({0},{1})", cmd.PutGlassA.CassetteSequenceNo, cmd.PutGlassA.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("PutGlassA(0,0)");
                    }
                    if (cmd.PutGlassB != null)
                    {
                        log.AppendFormat("PutGlassB({0},{1})", cmd.PutGlassB.CassetteSequenceNo, cmd.PutGlassB.SlotSequenceNo);
                    }
                    else
                    {
                        log.AppendFormat("PutGlassB(0,0)");
                    }
                    logStr.AppendFormat(log.ToString());
                   

                }
                logStr.Append("\r\n \r\n \r\n");
                logger.Info(logStr.ToString());
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                logger.Info(ex);
            }
           
        }
        /// <summary>
        /// 从commandlist中根据优先级返回最优先的
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private RobotCommand GetMaxPriorityCommand(List<RobotCommand> list)
        {
            try
            {
                if (list.Count == 0)
                    return null;
                if (list.Count == 1)
                {
                    var data = list[0];
                    list.Remove(data);
                    return data;
                }
                //foreach (var cmd in list)
                //{
                //    //Logger.InfoFormat("[FindMax] Unit {1} Command: {0}", cmd, cmd.Unit.UnitName);
                //}
                var max = list.Max(o => o.Priority);
                var cmd2 = list.Find(o => o.Priority == max);
                list.Remove(cmd2);
                return cmd2;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                logger.Info(ex);
                return null;
            }
           
        }
    }
}
