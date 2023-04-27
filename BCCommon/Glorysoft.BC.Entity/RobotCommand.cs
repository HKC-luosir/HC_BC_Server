using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class RobotCommand : INotifyPropertyChanged
    {
        #region INofifyPropertyChanged Members
        public static int Quanjuid = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INofifyPropertyChanged Members

        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
        public RobotCommand()
        {
            //SubCommand = RobotMotion.None;
            //Command = RobotMotion.None;
            //StageType = EStageType.Single;
        }
        public string Name { get; set; }
        #region robotCommand parameter
        //private RobotMotion command;
        //public RobotMotion Command
        //{
        //    get
        //    {
        //        return command;
        //    }
        //    set
        //    {
        //        if (command != value)
        //        {
        //            command = value;
        //            Notify("Command");
        //        }
        //    }
        //}
        //private EStageType stageType;
        //public EStageType StageType
        //{
        //    get
        //    {
        //        return stageType;
        //    }
        //    set
        //    {
        //        if (stageType != value)
        //        {
        //            stageType = value;
        //            Notify("StageType");
        //        }
        //    }
        //}
        //private int stagePathNo;
        //public int StagePathNo
        //{
        //    get
        //    {
        //        return stagePathNo;
        //    }
        //    set
        //    {
        //        if (stagePathNo != value)
        //        {
        //            stagePathNo = value;
        //            Notify("StagePathNo");
        //        }
        //    }
        //}
        //private int slotNo;
        //public int SlotNo
        //{
        //    get
        //    {
        //        return slotNo;
        //    }
        //    set
        //    {
        //        if (slotNo != value)
        //        {
        //            slotNo = value;
        //            Notify("SlotNo");
        //        }
        //    }
        //}
        //public int GlassThickness { get; set; }
        //public RobotMotion SubCommand { get; set; }
        //public ESubStageType SubCommandStageType { get; set; }
        //public int SubCommandStagePathNo { get; set; }
        //public int SubCommandSlotNo { get; set; }
        //public RobotHand SubCommandRobotHand { get; set; }

        //int SequenceNo, int stRCMD1, int stArmNo1,int stGetPosition1,int stPutPosition1,int stGetSlotNo1,int stPutSlotNo1,int stSubCommand1,int stGetSlotPostion1,int stPutSlotPostion1,
        //    int ndRCMD2,int ndArmNo2,int ndGetPosition2,int ndPutPosition2,int ndGetSlotNo2,int ndPutSlotNo2,int ndSubCommand2,int ndGetSlotPostion2,int ndPutSlotPostion2,
        //    int rdRCMD3,int rdArmNo3, int rdGetPosition3, int rdPutPosition3, int rdGetSlotNo3, int rdPutSlotNo3, int rdSubCommand3, int rdGetSlotPostion3, int rdPutSlotPostion3,
        //    int thRCMD4, int thArmNo4, int thGetPosition4, int thPutPosition4, int thGetSlotNo4, int thPutSlotNo4, int thSubCommand4, int thGetSlotPostion4, int thPutSlotPostion4

        public GlassInfo GetGlass { get; set; }
        //public GlassInfo ExchangeGetGlass { get; set; }
        public GlassInfo GetGlassA { get; set; }
        public GlassInfo GetGlassB { get; set; }
        public GlassInfo GetGlassC { get; set; }
        public GlassInfo GetGlassD { get; set; }
        public GlassInfo PutGlass { get; set; }
        public GlassInfo PutGlassA { get; set; }
        public GlassInfo PutGlassB { get; set; }
        public string GetModelName { get; set; }
        public string PutModelName { get; set; }

        public string STGetPosition1string { get; set; }
        public string STPutPosition1string { get; set; }
        public string NDGetPosition2string { get; set; }
        public string NDPutPosition2string { get; set; }



        public int SequenceNo { get; set; }//0-10000   到1w恢复0
        //int stRCMD1, int stArmNo1,int stGetPosition1,int stPutPosition1,int stGetSlotNo1,        
        /// <summary>
        /// None 0
        /// Robot Home 1
        /// Transfer 2
        /// Move 3
        /// Get 4
        /// Put 5
        /// One Action Exchange 6
        /// Two Action Exchange 7
        /// Command Clear 8
        /// One Action Batch Get 9
        /// One Action Batch Put 10
        /// Two Action Batch Get 11
        /// Two Action Batch Put 12
        /// </summary>
        public RobotMotion STRCMD1 { get; set; }
        /// <summary>
        /// 上下手臂：下手臂1 上手臂2 
        /// </summary>
        public RobotHand STArmNo1 { get; set; }
        /// <summary>
        ///  取片命令使用   unit path no
        /// </summary>
        public int STGetPosition1 { get; set; }
        /// <summary>
        /// 放片命令使用    unit path no
        /// </summary>
        public int STPutPosition1 { get; set; }
        /// <summary>
        /// 取片命令使用   UpstreamLink 中SlotNumber 1-6 组合出来的
        /// </summary>
        public int STGetSlotNo1 { get; set; }
        //int STPutSlotNo1,int STSubCommand1,int STGetSlotPoSTion1,int STPutSlotPoSTion1,
        /// <summary>
        /// 放片命令使用   DownstreamLink 中SlotNumber 1-6 组合出来的
        /// </summary>
        public int STPutSlotNo1 { get; set; }
        /// <summary>
        /// None 0
        /// Get Ready 1
        /// Put Ready 2
        /// </summary>
        public int STSubCommand1 { get; set; }
        /// <summary>
        /// 取片命令使用    Glass Postion
        /// </summary>
        public int STGetSlotPostion1 { get; set; }
        /// <summary>
        ///  放片命令使用    Glass Postion
        /// </summary>
        public int STPutSlotPostion1 { get; set; }
        // int ndRCMD2,int ndArmNo2,int ndGetPosition2,int ndPutPosition2,int ndGetSlotNo2,
        public RobotMotion NDRCMD2 { get; set; }
        /// <summary>
        /// 上下手臂：下手臂1 上手臂2 
        /// </summary>
        public RobotHand NDArmNo2 { get; set; }
        public int NDGetPosition2 { get; set; }
        public int NDPutPosition2 { get; set; }
        public int NDGetSlotNo2 { get; set; }
        //int NDPutSlotNo2,int NDSubCommaND2,int NDGetSlotPostion2,int NDPutSlotPostion2,
        public int NDPutSlotNo2 { get; set; }
        public int NDSubCommand2 { get; set; }
        public int NDGetSlotPostion2 { get; set; }
        public int NDPutSlotPostion2 { get; set; }

        //int rdRCMD3,int rdArmNo3, int rdGetPosition3, int rdPutPosition3, int rdGetSlotNo3,
        public RobotMotion RDRCMD3 { get; set; }
        public RobotHand RDArmNo3 { get; set; }
        public int RDGetPosition3 { get; set; }
        public int RDPutPosition3 { get; set; }
        public int RDGetSlotNo3 { get; set; }
        //int RDPutSlotNo3, int RDSubCommand3, int RDGetSlotPostion3, int RDPutSlotPostion3,
        public int RDPutSlotNo3 { get; set; }
        public int RDSubCommand3 { get; set; }
        public int RDGetSlotPostion3 { get; set; }
        public int RDPutSlotPostion3 { get; set; }

        //int thRCMD4, int thArmNo4, int thGetPosition4, int thPutPosition4, int thGetSlotNo4, 
        public RobotMotion THRCMD4 { get; set; }
        public RobotHand THArmNo4 { get; set; }
        public int THGetPosition4 { get; set; }
        public int THPutPosition4 { get; set; }
        public int THGetSlotNo4 { get; set; }
        //int THPutSlotNo4, int THSubCommand4, int THGetSlotPostion4, int THPutSlotPostion4
        public int THPutSlotNo4 { get; set; }
        public int THSubCommand4 { get; set; }
        public int THGetSlotPostion4 { get; set; }
        public int THPutSlotPostion4 { get; set; }
        #endregion
        public bool Excuting { get; set; }
        public string TransactionID { get; set; }

        private RobotHand robotHand;
        public RobotHand RobotHand
        {
            get
            {
                return robotHand;
            }
            set
            {
                if (robotHand != value)
                {
                    robotHand = value;
                    Notify("RobotHand");
                }
            }
        }
        public int Priority { get; set; }
        //  public GlassInfo JobData { get; set; }
        //  public GlassInfo PutJobData { get; set; }

        /// <summary>
        /// UnitInfo or PortInfo
        /// </summary>
        public Unit Unit { get; set; }

        public int TargetModelPosition { get; set; }

        //public bool IsEqual(RobotCommand cmd)
        //{
        //    if (Command == cmd.Command && StageType == cmd.StageType &&
        //        StagePathNo == cmd.StagePathNo && SlotNo == cmd.SlotNo &&
        //        RobotHand == cmd.RobotHand && Priority == cmd.Priority)
        //        return true;
        //    return false;
        //}
        //public bool IsSameJob(RobotCommand cmd)
        //{
        //    if (JobData != null && cmd.JobData != null && JobData.LotID == cmd.JobData.LotID && JobData.GLSID == cmd.JobData.GLSID)
        //        return true;
        //    return false;
        //}
        public override string ToString()
        {
            //var str = string.Format("CMD: {0},{1}, {2}, {3}, {4}, Priority: {5}",
            //    Command.ToString().ToUpper(),
            //    StageType.ToString().ToUpper(),
            //    stagePathNo, SlotNo,
            //    RobotHand.ToString().ToUpper(), Priority);
            //if (SubCommand != RobotMotion.None)
            //{
            //    str = string.Format("{0}; SUBCMD: {1}, {2}, {3}, {4}, {5}; JOBDATA: {6}", str,
            //        SubCommand.ToString().ToUpper(),
            //        SubCommandStageType.ToString().ToUpper(), SubCommandStagePathNo,
            //        SubCommandSlotNo, SubCommandRobotHand.ToString().ToUpper(), JobData != null ? JobData.GLSID : "null");
            //}
            //else
            //{
            //    if (Command == RobotMotion.Exchange)
            //    {
            //        str = string.Format("{0}; GETJOBDATA: {1}", str, JobData != null ? JobData.GLSID : "null");
            //        str = string.Format("{0}; PUTJOBDATA: {1}", str, PutJobData != null ? PutJobData.HPanelID : "null");
            //    }
            //    else
            //    {
            //        str = string.Format("{0}; JOBDATA: {1}", str, JobData != null ? JobData.GLSID : "null");
            //    }
            //}
            //return str;
            return "";
        }
    }
}