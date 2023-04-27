
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Linq;

namespace Glorysoft.BC.Entity
{

    public class SUnit : NotifyPropertyChanged
    {
       
    
        public SUnit(Unit unit)
        {
            //LocationGlsInfo = new List<Location>();
            //Parent = unit;
            //Linksignals = new List<Linksignal>();
            AlarmInfoList = new List<AlarmInfo>();
            ssunitList = new List<SSUnit>();
        }
        public SUnit()
        {
            //LocationGlsInfo = new List<Location>();
            ////PanelList = new Dictionary<string, PanelInfo>();
            //Linksignals = new List<Linksignal>();
            AlarmInfoList = new List<AlarmInfo>();
            ssunitList = new List<SSUnit>();
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
        public List<AlarmInfo> AlarmInfoList { get; set; }
        public string EQPID { get; set; }

        //public string EQPName { get; set; }
        public int SubUnitNo { get; set; }
        public string UnitID { get; set; }

        public string UnitName { get; set; }
        public int SUnitPathNo { get; set; }
        public int SUnitType { get; set; }
        public string SUnitID { get; set; }
        public string SUnitName { get; set; }
        public bool ReportMesState { get; set; } = true;
        private string sunitStatus;
        /// <summary>
        /// 1	PM	
        ///2	DOWN	
        ///3	Pause	
        ///4	IDLE	
        ///5	RUN        
        /// </summary>
        public string SUnitStatus
        {
            get
            {
                return sunitStatus;
            }
            set
            {
                if (sunitStatus != value)
                {
                    sunitStatus = value;
                    Notify("SUnitStatus");
                }
            }
        }
        private string sunitSTCode;//SUNITSTCODE
        public string SUnitSTCode
        {
            get
            {
                return sunitSTCode;
            }
            set
            {
                if (sunitSTCode != value)
                {
                    sunitSTCode = value;
                    Notify("SUnitSTCode");
                }
            }
        }
        public bool HasSSUnit { get; set; }
        private List<SSUnit> ssunitList;
        public List<SSUnit> SSUnitList
        {
            get
            {
                return ssunitList;
            }
            set
            {
                if (ssunitList != value)
                {
                    ssunitList = value;
                    Notify("SSUnitList");
                }
            }
        }
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

        public string MainrecipePortName { get; set; }
        public string MainrecipeCarrierName { get; set; }

        private List<GlassInfo> currentGlassInfoList = new List<GlassInfo>();
        public List<GlassInfo> CurrentGlassInfoList
        {
            get
            {
                return currentGlassInfoList;
            }
            set
            {
                if (currentGlassInfoList != value)
                {
                    currentGlassInfoList = value;
                    Notify("CurrentGlassInfoList");
                }
            }
        }
       // public List<GlassInfo> CurrentGlassInfoList { get; set; }

        #region ====初始化常量=======================================
        //public Unit Parent { get; set; }
        //public bool BCPointFlag { get; set; }
        //[DataMember]
        //public EnumUnitType Type { get; set; }
        ////public string UnitID { get; set; }
        ////public string EQPID { get; set; }
        ////public string EQPName { get; set; }
        //public string RobotStatus { get; set; }
        ////public string LineID { get; set; }
        ////public string UnitName { get; set; }
        ////public int UnitNo { get; set; }
        //public int Capacity { get; set; }
        //[DataMember]
        //// public Dictionary<string, PanelInfo> PanelList { get; set; }
        //// [DataMember]
        ////public EnumUnitType UnitType { get; set; }
        //public bool IsBlockPoint { get; set; }

        //public List<Linksignal> Linksignals { get; set; }
        //public int LinkPathNo { get; set; }
        //public bool UseGetWait { get; set; }
        //public bool CanSUnitPut { get; set; }
        //public bool CanSUnitGet { get; set; }
        //public DateTime ReadyTime { get; set; }//full/empty/reserver


        //[DataMember]
        //public EnumLoadUnloadType LoadUnloadStatus { get; set; }  //0:Both, 1:Load, 2:Unload

        //public bool ClearGlassInformation()
        //{
        //    foreach (var gls in LocationGlsInfo)
        //    {
        //        gls.IsEmpty = true;
        //        gls.GlsInfo = null;
        //    }
        //    return true;
        //}
        ////上游设备Down机，打开CanUnitPut，否则不会放片到Buffer里面
        ////Buffer放片到水位线后，IsSuspend标记打开，不能再放片到Buffer
        ////Buffer水位线降低后，IsSusp标记关闭，可以再放片到Buffer
        ////TA3 Buffer到水位后，则TOA1不能进片
        //public void UpdateCanUnitGetPut()
        //{
        //    CanSUnitPut = true;
        //    CanSUnitGet = true;
        //}
        //public List<Location> LocationGlsInfo { get; protected set; }

        //public int Priority { get; set; }

        #endregion

    }
}