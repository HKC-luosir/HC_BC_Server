using System.Runtime.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Linq;

namespace Glorysoft.BC.Entity
{
    public class Robot : Unit
    {
        public Robot()
        {
            CurrentState = RobotState.PowerOff;
            RobotDispatchMode = EnumRobotDispatchMode.MANUAL;
            thredDispatchTimeOut = new Thread(DispatchTimeOut);
        }

        public List<RobotCommand> RobotCommandList { get; set; } = new List<RobotCommand>();
        private RobotState currentState;
        public RobotState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                if (currentState != value)
                {
                    currentState = value;
                    Notify("CurrentState");
                }
            }
        }
      
        public bool UpHandEnable { get; set; }
        public bool LowHandEnable { get; set; }
        public GlassInfo UpHandGlass1 { get; set; }
        public GlassInfo UpHandGlass2 { get; set; }
         
        public bool UpperExistOn
        {
            get
            {
                if (UpperCassetteSequenceNo1 != 0 && UpperSlotSequenceNo1 != 0)
                {
                    return true;
                }
                else if (UpperCassetteSequenceNo2!= 0 && UpperSlotSequenceNo2!= 0)
                {
                    return true;
                }
                return false;
            }
        }
        //public bool UpperExistOn
        //{
        //    get { 
        //        if(UpHandGlass1!=null)
        //        {
        //            return true;
        //        }else if(UpHandGlass2 != null)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
        //public bool Upper2ExistOn
        //{
        //    get { return UpHandGlass2 != null;  }
        //}
        //public bool UpperExistOn
        //{
        //    get { return UpHandGlass != null || !string.IsNullOrEmpty(UpperJobId); }
        //}

        public GlassInfo LowHandGlass1 { get; set; }
        public GlassInfo LowHandGlass2 { get; set; }
        public bool LowerExistOn
        {
            get
            {
                if (LowerCassetteSequenceNo1 != 0 && LowerSlotSequenceNo1 != 0)
                {
                    return true;
                }
                else if (LowerCassetteSequenceNo2 != 0 && LowerSlotSequenceNo2 != 0)
                {
                    return true;
                }
                return false;
            }
        }
        //public bool LowerExistOn
        //{
        //    get
        //    {
        //        if ((LowHandGlass1 != null)&&(LowerCassetteSequenceNo1!=0&& LowerSlotSequenceNo1!=0))
        //        {
        //            return true;
        //        }
        //        else if (LowHandGlass2 != null)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}
        //public bool Lower2ExistOn
        //{
        //    get { return LowHandGlass2 != null ; }
        //}
        //public bool LowerExistOn
        //{
        //    get { return LowHandGlass != null || !string.IsNullOrEmpty(LowerJobId); }
        //}
        private RobotCommand lastCommand = new RobotCommand();
        public RobotCommand LastCommand
        {
            get
            {
                return lastCommand;
            }
            set
            {
                if (lastCommand != value)
                {
                    lastCommand = value;
                    Notify("LastCommand");
                }
            }
        }
        private RobotCommand execCommand = new RobotCommand();
        public RobotCommand ExecCommand
        {
            get
            {
                return execCommand;
            }
            set
            {
                if (execCommand != value)
                {
                    execCommand = value;
                    Notify("ExecCommand");
                }
            }
        }
        private int cmdResultCode;
        public int CmdResultCode
        {
            get
            {
                return cmdResultCode;
            }
            set
            {
                if (cmdResultCode != value)
                {
                    cmdResultCode = value;
                    Notify("CmdResultCode");
                }
            }
        }
       
       
        private string upperLotId;
        public string UpperLotId
        {
            get
            {
                return upperLotId;
            }
            set
            {
                if (upperLotId != value)
                {
                    upperLotId = value;
                    Notify("UpperLotId");
                }
            }
        }       
        private string lowerLotId;
        public string LowerLotId
        {
            get
            {
                return lowerLotId;
            }
            set
            {
                if (lowerLotId != value)
                {
                    lowerLotId = value;
                    Notify("LowerLotId");
                }
            }
        }
        
        public int LowerCassetteSequenceNo1 { get; set; }
        public int LowerSlotSequenceNo1 { get; set; }
        public int LowerCassetteSequenceNo2 { get; set; }
        public int LowerSlotSequenceNo2 { get; set; }

        
        public int UpperCassetteSequenceNo1 { get; set; }
        public int UpperSlotSequenceNo1 { get; set; }
        public int UpperCassetteSequenceNo2 { get; set; }
        public int UpperSlotSequenceNo2 { get; set; }
        //private string upperJobId;
        //public string UpperJobId
        //{
        //    get
        //    {
        //        return upperJobId;
        //    }
        //    set
        //    {
        //        if (upperJobId != value)
        //        {
        //            upperJobId = value;
        //            Notify("UpperJobId");
        //        }
        //    }
        //}
        //private string lowerJobId;
        //public string LowerJobId
        //{
        //    get
        //    {
        //        return lowerJobId;
        //    }
        //    set
        //    {
        //        if (lowerJobId != value)
        //        {
        //            lowerJobId = value;
        //            Notify("LowerJobId");
        //        }
        //    }
        //}
        private EnumRobotDispatchMode robotDispatchMode;
        public EnumRobotDispatchMode RobotDispatchMode
        {
            get
            {
                return robotDispatchMode;
            }
            set
            {
                if (robotDispatchMode != value)
                {
                    robotDispatchMode = value;
                    Notify("RobotDispatchMode");
                }
            }
        }

        #region 派工3分钟计时
        private bool commandExecuting;
        /// <summary>
        /// true 正在执行 false 不知执行中
        /// </summary>
        public bool CommandExecuting
        {
            get
            {
                return commandExecuting;
            }
            set
            {
                if (commandExecuting != value)
                {
                    //if(commandExecuting)
                    //{
                    //    DispatchTimeOutDate = HostInfo.Current.SystemConfig.DispatchTimeOutDate;
                    //}
                    commandExecuting = value;
                    Notify("CommandExecuting");
                }
            }
        }
        /// <summary>
        /// 超时时间
        /// </summary>
        public int DispatchTimeOutDate { get; set; }
        private bool isWaitCmdCode;
        /// <summary>
        /// true 等待robot执行结果回复   false robot已回复
        /// </summary>
        public bool IsWaitCmdCode
        {
            get
            {
                return isWaitCmdCode;
            }
            set
            {
                if (isWaitCmdCode != value)
                {
                    isWaitCmdCode = value;
                    if (isWaitCmdCode)
                    {
                        if (HostInfo.Current.SystemSetting.Any(c => c.bckey == "DispatchTimeOutDate"))
                        {
                            DispatchTimeOutDate = Convert.ToInt32(HostInfo.Current.SystemSetting.FirstOrDefault(c => c.bckey == "DispatchTimeOutDate").bcvalue);//HostInfo.Current.SystemConfig.DispatchTimeOutDate;
                        }
                    }
                    Notify("IsWaitCmdCode");
                }
            }
        }       
        public Thread thredDispatchTimeOut;
        private void DispatchTimeOut()
        {
            while (true)
            {
                try
                {
                   if(CommandExecuting|| IsWaitCmdCode)
                    {
                        if(DispatchTimeOutDate>0)
                        {
                            DispatchTimeOutDate--;
                        }                       
                    }
                    if((CommandExecuting || IsWaitCmdCode)&&DispatchTimeOutDate == 0)
                    {
                        CommandExecuting = false;
                        IsWaitCmdCode = false;
                    }
                    //暂停300毫秒后继续执行
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    LogHelper.BCLog.Debug(ex.Message);
                }
            }
        }
        #endregion
    }

    //public class RobotDTO
    //{
    //    //public static RobotDTO GetRobotDTO(Robot robot)
    //    //{
    //    //    RobotDTO dto = new RobotDTO();
    //    //    dto.LineID = robot.LineID;
    //    //    dto.EQPID = robot.EQPID;
    //    //    dto.EQPName = robot.EQPName;
    //    //    dto.UnitID = robot.UnitID;
    //    //    dto.UnitName = robot.UnitName;
    //    //    dto.CurrentState = robot.CurrentState;
    //    //    dto.UpHandGlass = robot.UpHandGlass;
    //    //    dto.LowHandGlass = robot.LowHandGlass;
    //    //    dto.LastCommand = robot.LastCommand;
    //    //    dto.ExecCommand = robot.ExecCommand;
    //    //    dto.CmdResultCode = robot.CmdResultCode;
    //    //    dto.IsWaitCmdCode = robot.IsWaitCmdCode;
    //    //    dto.CommandExecuting = robot.CommandExecuting;
    //    //    dto.UpperJobId = robot.UpperJobId;
    //    //    dto.UpperLotId = robot.UpperLotId;
    //    //    dto.LowerJobId = robot.LowerJobId;
    //    //    dto.LowerLotId = robot.LowerLotId;
    //    //    dto.RobotDispatchMode = robot.RobotDispatchMode;

    //    //    return dto;
    //    //}

    //    public RobotState CurrentState { get; set; }
    //    public SPanelInfo UpHandGlass { get; set; }
    //    public bool UpperExistOn
    //    {
    //        get { return UpHandGlass != null; }
    //    }
    //    public SPanelInfo LowHandGlass { get; set; }
    //    public bool LowerExistOn
    //    {
    //        get { return LowHandGlass != null; }
    //    }
    //    public RobotCommand LastCommand { get; private set; }
    //    public RobotCommand ExecCommand { get; set; }
    //    public int CmdResultCode { get; set; }
    //    public bool IsWaitCmdCode { get; set; }
    //    public bool CommandExecuting { get; set; }
    //    public string UpperJobId { get; set; }
    //    public string UpperLotId { get; set; }
    //    public string LowerJobId { get; set; }
    //    public string LowerLotId { get; set; }
    //    public bool CheckODFMatchGlass { get; set; }
    //    public bool IsLensGlassFirst { get; set; }
    //    public EnumRobotDispatchMode RobotDispatchMode { get; set; }

    //    [DataMember]
    //    public string UnitID { get; set; }
    //    [DataMember]
    //    public string EQPID { get; set; }
    //    [DataMember]
    //    public string EQPName { get; set; }
    //    [DataMember]
    //    public string LineID { get; set; }
    //    [DataMember]
    //    public string UnitName { get; set; }
    //    public string RobotStatus { get; set; }



    //}
}
