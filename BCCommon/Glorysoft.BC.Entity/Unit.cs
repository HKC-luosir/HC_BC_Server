using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading;

namespace Glorysoft.BC.Entity
{
    [DataContract]
    [KnownType(typeof(PortInfo))]
    [KnownType(typeof(Buffer))]
    [KnownType(typeof(Robot))]
    public class Unit : NotifyPropertyChanged
    {
        private bool isWaitCmdCode;
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
                    Notify("IsWaitCmdCode");
                }
            }
        }
        // public bool IsWaitCmdCode { get; set; }
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

        //private string lineID;
        //public string LineID
        //{
        //    get { return lineID; }

        //    set
        //    {
        //        if (lineID != value)
        //        {
        //            lineID = value;
        //            //Notify("LineID");
        //        }

        //    }
        //}
        public string FunctionName { get; set; }
        public string EQPID { get; set; }
        public int LocalNo { get; set; }

        //public string EQPName { get; set; }
        // public int UnitPathNo { get; set; }
        public EnumUnitType UnitType { get; set; }
        public int UnitCapacity { get; set; }
        public string UnitNo { get; set; }
        public string UnitID { get; set; }

        public string UnitName { get; set; }


        private bool cimMode = false;
        public bool CIMMode
        {
            get
            {
                return cimMode;
            }
            set
            {
                if (cimMode != value)
                {
                    cimMode = value;
                    Notify("CIMMode");
                }
            }
        }
        //public string UnitStatus { get; set; }
        /// <summary>
        /// case ControlState.Offline: crst = "0";
        ///case ControlState.OnlineRemote:crst = "1";
        ///case ControlState.OnlineLocal:crst = "2";
        /// </summary>
        public EnumControlState CRST { get; set; }
        private string unitStatus;
        /// <summary>
        /// 1	PM	
        ///2	DOWN	
        ///3	Pause	
        ///4	IDLE	
        ///5	RUN        
        /// </summary>
        public string UnitStatus
        {
            get
            {
                return unitStatus;
            }
            set
            {
                if (unitStatus != value)
                {
                    unitStatus = value;
                    Notify("UnitStatus");
                }
            }
        }
        private string oldUnitStatus;
        /// <summary>
        /// 1	PM	
        ///2	DOWN	
        ///3	Pause	
        ///4	IDLE	
        ///5	RUN        
        /// </summary>
        public string OldUnitStatus
        {
            get
            {
                return oldUnitStatus;
            }
            set
            {
                if (oldUnitStatus != value)
                {
                    oldUnitStatus = value;
                    Notify("OldUnitStatus");
                }
            }
        }
        private string unitSTCode;//UNITSTCODE
        public string UnitSTCode
        {
            get
            {
                return unitSTCode;
            }
            set
            {
                if (unitSTCode != value)
                {
                    unitSTCode = value;
                    Notify("UnitSTCode");
                }
            }
        }

        public string ReasonCode { get; set; }
        public bool HasSUnit { get; set; }
        private List<SUnit> sunitList;
        public List<SUnit> SUnitList
        {
            get
            {
                return sunitList;
            }
            set
            {
                if (sunitList != value)
                {
                    sunitList = value;
                    Notify("SUnitList");
                }
            }
        }
        private string isConnect = Consts.IsConnect.Down.ToString();
        public string IsConnect
        {
            get
            {
                return isConnect;
            }
            set
            {
                isConnect = value;
                Notify("IsConnect");
            }
        }
        public List<AlarmInfo> AlarmInfoList { get; set; }
        /// <summary>
        /// 0 ok；1NG；
        /// </summary>
        public string RTCode { get; set; }
        public DateTime AliveUpdate { get; set; }

        /// <summary>
        /// 0: No Unit Mode exist
        ///01: Normal Mode
        ///02: Force Clean Out Mode
        ///03: Skip Mode
        ///04: Reserve
        ///05: TR Mode(for AOI)
        ///06: PECVD Only Mode(for PECVD)
        ///07: Manual Mode(For Furnace)
        ///08: Cleaner Only Mode(For Initial Cleaner)
        ///09: Mix Run Mode(Only D2 CVD, PVD, Dry Etch)
        ///10: Job Reservation Mode(Only D2 Annealing Furnace)
        ///11: Inspect Mode(Sorter Only)
        ///12: Operate Mode(Sorter Only)
        ///22: ELA Only Mode(For ELA)
        /// </summary>
        public int UnitMode { get; set; }
        public int CurrentWIPCount { get; set; }
        public int OperationWIPCount { get; set; }
        /// <summary>
        /// 是否是该线体最后一个设备
        /// </summary>
        public bool IsEqpEnd { get; set; } = false;
        /// <summary>
        /// 是否是该线体第一个设备
        /// </summary>
        public bool IsEqpStart { get; set; } = false;
        /// <summary>
        /// 是否校验recipeid
        /// </summary>
        public bool CurrentRecipeIdCheck { get; set; } = true;
        /// <summary>
        /// 是否是必经设备
        /// </summary>
        public bool IsProcessEnd { get; set; } = false;
        /// <summary>
        /// 是否重新请帐
        /// </summary>
        public bool IsJobDataRequest { get; set; } = false;
        /// <summary>
        /// VCR状态 number:status;
        /// </summary>
        public string VCRStatus { get; set; } = "";
        /// <summary>
        /// EIP DeviceType
        /// </summary>
        public string DeviceType { get; set; } = "";
        /// <summary>
        /// EIP Class3IP
        /// </summary>
        public string Class3IP { get; set; } = "";
        /// <summary>
        /// EIP PLCIP
        /// </summary>
        public string PLCIP { get; set; } = "";
        /// <summary>
        /// EIP PLCPort
        /// </summary>
        public string PLCPort { get; set; } = "";
        ///// <summary>
        ///// 1 : Enable
        /////2 : Disable
        ///// </summary>
        //public int VCREnableMode { get; set; }
        ///// <summary>
        ///// 1 : Key-In mode
        /////2 : Skip mode
        ///// </summary>
        //public int VCRReadFailOperationMode { get; set; }
        //BC不再使用Mask信息
        public MaskInfo MaskInfo { get; set; }





        private int cassetteOperationMode;
        public int CassetteOperationMode
        {
            get
            {
                return cassetteOperationMode;
            }
            set
            {
                if (cassetteOperationMode != value)
                {
                    cassetteOperationMode = value;
                    Notify("CassetteOperationMode");
                }
            }
        }
        private int portQTime;
        public int PortQTime
        {
            get
            {
                return portQTime;
            }
            set
            {
                if (portQTime != value)
                {
                    portQTime = value;
                    Notify("PortQTime");
                }
            }
        }
        /// <summary>
        /// Consts.CommandType.CCLink.GetHashCode()//1:CCLink
        /// Consts.CommandType.HSMS.GetHashCode()//2:HSMS
        /// </summary>
        public int CommandType { get; set; }
        public string AutoRecipeChangeMode { get; set; }
        public int CurrentRecipeID { get; set; }
        public string CurrentRecipeVersion { get; set; }

        private bool downstreamInlineMode = true;
        public bool DownstreamInlineMode
        {
            get
            {
                return downstreamInlineMode;
            }
            set
            {
                if (downstreamInlineMode != value)
                {
                    downstreamInlineMode = value;
                    Notify("DownstreamInlineMode");
                }
            }
        }

        private bool upstreamInlineMode = true;
        public bool UpstreamInlineMode
        {
            get
            {
                return upstreamInlineMode;
            }
            set
            {
                if (upstreamInlineMode != value)
                {
                    upstreamInlineMode = value;
                    Notify("UpstreamInlineMode");
                }
            }
        }

        public List<RobotModel> RobotModelList { get; set; }
        public bool LoadingStop { get; set; }


        public string MachineMode { get; set; }
        public string MainrecipePortName { get; set; }
        public string MainrecipeCarrierName { get; set; }
        // public bool HsmsSendTraceData { get; set; }
        //public bool S6F11CEID304OK { get; set; }
        //public string  S10F1Text { get; set; }
        //private bool loadingStop = false;
        //public bool LoadingStop
        //{
        //    get
        //    {
        //        return loadingStop;
        //    }
        //    set
        //    {
        //        if (loadingStop != value)
        //        {
        //            loadingStop = value;
        //            Notify("LoadingStop");
        //        }
        //    }
        //}
        #region HSMS LotProcessCanceled
        //public bool S6F11CEID304 { get; set; }
        private bool s6F11CEID304 = false;
        public bool S6F11CEID304
        {
            get
            {
                return s6F11CEID304;
            }
            set
            {
                if (s6F11CEID304 != value)
                {
                    s6F11CEID304 = value;
                    if (s6F11CEID304)
                    {
                        S6F11CEID304Time = 10;
                    }
                    Notify("S6F11CEID304");
                }
            }
        }
        public int S6F11CEID304Time { get; set; }

        //public bool S10F1 { get; set; }
        private bool s10F1 = false;
        public bool S10F1
        {
            get
            {
                return s10F1;
            }
            set
            {
                if (s10F1 != value)
                {
                    s10F1 = value;
                    if (s10F1)
                    {
                        S10F1TextTime = 10;
                    }
                    Notify("S10F1");
                }
            }
        }
        public string S10F1Text { get; set; }
        public int S10F1TextTime { get; set; }
        public string LotCancelPortID { get; set; }
        public void LotProcessCanceledMessage()
        {

            if (LotProcessCanceledFunction != null)
            {
                LotProcessCanceledFunction(LotCancelPortID, S10F1Text);
            }
        }
        public delegate void LotProcessCanceledDelegate(string LotCancelPortID, string S10F1Text);
        /// <summary>
        /// LotProcessCanceled
        /// </summary>
        public LotProcessCanceledDelegate LotProcessCanceledFunction;
        public Thread thredHSMSLotCancel;
        private void HSMSLotCancel()
        {
            while (true)
            {
                try
                {
                    if (S6F11CEID304 && S10F1)
                    {
                        // logicService.LotProcessCanceled(port);
                        LotProcessCanceledMessage();
                        S6F11CEID304 = false;
                        S10F1 = false;
                    }
                    if (S6F11CEID304)
                    {
                        if (S6F11CEID304Time > 0)
                        {
                            S6F11CEID304Time--;
                        }
                        if (S6F11CEID304Time == 0)
                        {
                            LotProcessCanceledMessage();
                            S6F11CEID304 = false;
                        }
                    }
                    if (S10F1)
                    {
                        if (S10F1TextTime > 0)
                        {
                            S10F1TextTime--;
                        }
                        if (S10F1TextTime == 0)
                        {
                            S10F1 = false;
                        }
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
        #region OPI Request
        //public bool OPIRecipeCheck { get; set; }
        private bool opiRecipeCheck = false;
        public bool OPIRecipeCheck
        {
            get
            {
                return opiRecipeCheck;
            }
            set
            {
                if (opiRecipeCheck != value)
                {
                    opiRecipeCheck = value;
                    if (opiRecipeCheck)
                    {
                        OPIRecipeCheckTime = 20;
                    }
                    Notify("OPIRecipeCheck");
                }
            }
        }
        public int OPIRecipeCheckTime { get; set; }
        //public bool OPIParameterRequest { get; set; }
        private bool opiParameterRequest = false;
        public bool OPIParameterRequest
        {
            get
            {
                return opiParameterRequest;
            }
            set
            {
                if (opiParameterRequest != value)
                {
                    opiParameterRequest = value;
                    if (opiParameterRequest)
                    {
                        OPIParameterRequestTime = 20;
                    }
                    Notify("OPIParameterRequest");
                }
            }
        }
        public bool RecipeParamCheckOK { get; set; }
        public int OPIParameterRequestTime { get; set; }
        public Thread thredOPIRequest;
        private void OPIRequest()
        {
            while (true)
            {
                try
                {
                    if (OPIRecipeCheck)
                    {
                        if (OPIRecipeCheckTime > 0)
                        {
                            OPIRecipeCheckTime--;
                        }
                        if (OPIRecipeCheckTime == 0)
                        {
                            OPIRecipeCheck = false;
                        }
                    }
                    if (OPIParameterRequest)
                    {
                        if (OPIParameterRequestTime > 0)
                        {
                            OPIParameterRequestTime--;
                        }
                        if (OPIParameterRequestTime == 0)
                        {
                            OPIParameterRequest = false;
                        }
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
        #region CCLINK RecipeChange
        public bool RecipeChangeParameterRequest { get; set; }
        //public bool RecipeChangeReport { get; set; }
        private bool recipeChangeReport = false;
        public bool RecipeChangeReport
        {
            get
            {
                return recipeChangeReport;
            }
            set
            {
                if (recipeChangeReport != value)
                {
                    recipeChangeReport = value;
                    if (recipeChangeReport)
                    {
                        RecipeChangeReportTime = 20;
                    }
                    Notify("RecipeChangeReport");
                }
            }
        }
        public int RecipeChangeReportTime { get; set; }

        //public bool RecipeParameterReply { get; set; }        
        private bool recipeParameterReply = false;
        public bool RecipeParameterReply
        {
            get
            {
                return recipeParameterReply;
            }
            set
            {
                if (recipeParameterReply != value)
                {
                    recipeParameterReply = value;
                    if (recipeParameterReply)
                    {
                        RecipeParameterReplyTime = 20;
                    }
                    Notify("RecipeParameterReply");
                }
            }
        }
        public int RecipeParameterReplyTime { get; set; }
        public Recipe Recipe { get; set; }
        public List<Parameter> ParameterList { get; set; }
        public void RecipeChangeMessage()
        {

            if (RecipeChangeFunction != null)
            {
                RecipeChangeFunction(Recipe, ParameterList);
            }
        }
        public delegate void RecipeChangeDelegate(Recipe Recipe, List<Parameter> ParameterList);
        /// <summary>
        /// LotProcessCanceled
        /// </summary>
        public RecipeChangeDelegate RecipeChangeFunction;
        public Thread thredCCLINKRecipeChange;
        private void CCLINKRecipeChange()
        {
            while (true)
            {
                try
                {
                    if (RecipeChangeReport && RecipeParameterReply)
                    {
                        RecipeChangeMessage();
                        RecipeChangeReport = false;
                        RecipeParameterReply = false;
                        RecipeChangeParameterRequest = false;
                    }
                    if (RecipeChangeReport)
                    {
                        if (RecipeChangeReportTime > 0)
                        {
                            RecipeChangeReportTime--;
                        }
                        if (RecipeChangeReportTime == 0)
                        {
                            RecipeChangeParameterRequest = false;
                            RecipeChangeReport = false;
                        }
                    }
                    if (RecipeParameterReply)
                    {
                        if (RecipeParameterReplyTime > 0)
                        {
                            RecipeParameterReplyTime--;
                        }
                        if (RecipeParameterReplyTime == 0)
                        {
                            RecipeParameterReply = false;
                        }
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
        //<result property = "CurrentRecipeID" column="currentrecipeid"/>
        //<result property = "DownstreamInlineMode" column="downstreaminlinemode"/>
        //<result property = "UpstreamInlineMode" column="upstreaminlinemode"/>
        //private Dictionary<string, SentOutJobInfo> sentOutJobInfoDic = new Dictionary<string, SentOutJobInfo>();
        //public Dictionary<string, SentOutJobInfo> SentOutJobInfoDic
        //{
        //    get
        //    {
        //        return sentOutJobInfoDic;
        //    }
        //    set
        //    {
        //        if (sentOutJobInfoDic != value)
        //        {
        //            sentOutJobInfoDic = value;
        //            Notify("SentOutJobInfoDic");
        //        }
        //    }
        //}      
        /// <summary>
        /// 1 JobDataA ;2JobDataB;3 JobDataA and JobDataB
        /// </summary>
        //public int UsedJobBlockNo { get; set; }
        public Unit(EQPInfo eq)
        {
            //LocationGlsInfo = new List<Location>();
            Parent = eq;
            //Linksignals = new List<Linksignal>();
            sunitList = new List<SUnit>();
            MaskInfo = new MaskInfo();
            RobotModelList = new List<RobotModel>();
            //thredHSMSLotCancel = new Thread(HSMSLotCancel);
            //thredCCLINKRecipeChange = new Thread(CCLINKRecipeChange);
            //thredOPIRequest = new Thread(OPIRequest);
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob1BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob2BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob3BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob4BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob5BlockHandler, new SentOutJobInfo());
            AlarmInfoList = new List<AlarmInfo>();
        }
        public Unit()
        {
            // LocationGlsInfo = new List<Location>();
            ////PanelList = new Dictionary<string, PanelInfo>();
            //Linksignals = new List<Linksignal>();
            sunitList = new List<SUnit>();
            MaskInfo = new MaskInfo();
            RobotModelList = new List<RobotModel>();
            //thredHSMSLotCancel = new Thread(HSMSLotCancel);
            //thredCCLINKRecipeChange = new Thread(CCLINKRecipeChange);
            //thredOPIRequest = new Thread(OPIRequest);
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob1BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob2BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob3BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob4BlockHandler, new SentOutJobInfo());
            //sentOutJobInfoDic.Add(SentOutJobInfoName.SentJob5BlockHandler, new SentOutJobInfo());
            AlarmInfoList = new List<AlarmInfo>();
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public Robot SetRobotInfo(Robot robot, Unit unit)
        {
            robot.EQPID = unit.EQPID;
            robot.UnitID = unit.UnitID;
            robot.UnitName = unit.UnitName;
            robot.UnitType = unit.UnitType;
            robot.UnitCapacity = unit.UnitCapacity;
            robot.ReasonCode = unit.ReasonCode;
            robot.UnitSTCode = unit.UnitSTCode;
            robot.HasSUnit = unit.HasSUnit;
            robot.UnitMode = unit.UnitMode;
            robot.CassetteOperationMode = unit.CassetteOperationMode;
            robot.PortQTime = unit.PortQTime;
            robot.CommandType = unit.CommandType;
            robot.CurrentRecipeID = unit.CurrentRecipeID;
            robot.DownstreamInlineMode = unit.DownstreamInlineMode;
            robot.UpstreamInlineMode = unit.UpstreamInlineMode;
            robot.UnitNo = unit.UnitNo;
            robot.LoadingStop = unit.LoadingStop;
            robot.CRST = unit.CRST;
            robot.UnitStatus = unit.UnitStatus;
            robot.CIMMode = unit.CIMMode;
            robot.IsEqpEnd = unit.IsEqpEnd;
            robot.IsEqpStart = unit.IsEqpStart;
            robot.CurrentRecipeIdCheck = unit.CurrentRecipeIdCheck;
            robot.IsProcessEnd = unit.IsProcessEnd;
            robot.IsJobDataRequest = unit.IsJobDataRequest;
            robot.VCRStatus = unit.VCRStatus;
            robot.LocalNo = unit.LocalNo;

            return robot;
        }
        #region ====初始化常量=======================================
        public EQPInfo Parent { get; set; }
        public bool BCPointFlag { get; set; }
        [DataMember]
        public EnumUnitType Type { get; set; }
        //public string UnitID { get; set; }
        //public string EQPID { get; set; }
        //public string EQPName { get; set; }
        public string RobotStatus { get; set; }
        //public string LineID { get; set; }
        //public string UnitName { get; set; }
        //public int UnitNo { get; set; }
        public int Capacity { get; set; }
        [DataMember]
        // public Dictionary<string, PanelInfo> PanelList { get; set; }
        // [DataMember]
        //public EnumUnitType UnitType { get; set; }
        public bool IsBlockPoint { get; set; }

        // public List<Linksignal> Linksignals { get; set; }
        public int LinkPathNo { get; set; }
        public bool UseGetWait { get; set; }
        public bool CanSUnitPut { get; set; }
        public bool CanSUnitGet { get; set; }
        public DateTime ReadyTime { get; set; }//full/empty/reserver


        [DataMember]
        public EnumLoadUnloadType LoadUnloadStatus { get; set; }  //0:Both, 1:Load, 2:Unload

        //public bool ClearGlassInformation()
        //{
        //    foreach (var gls in LocationGlsInfo)
        //    {
        //        gls.IsEmpty = true;
        //        gls.GlsInfo = null;
        //    }
        //    return true;
        //}
        //上游设备Down机，打开CanUnitPut，否则不会放片到Buffer里面
        //Buffer放片到水位线后，IsSuspend标记打开，不能再放片到Buffer
        //Buffer水位线降低后，IsSusp标记关闭，可以再放片到Buffer
        //TA3 Buffer到水位后，则TOA1不能进片
        public void UpdateCanUnitGetPut()
        {
            CanSUnitPut = true;
            CanSUnitGet = true;
        }
        // public List<Location> LocationGlsInfo { get; protected set; }

        public int Priority { get; set; }

        #endregion


    }
}