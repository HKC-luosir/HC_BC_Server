using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Glorysoft.BC.Entity
{
    public class EQPInfo : NotifyPropertyChanged
    {


        public EQPInfo()
        {
            //新添加的
            Units = new List<Unit>();
           // EquipmentStatus=
        }
        //private string lineid;
        //public string LineID
        //{
        //    get
        //    {
        //        return lineid;
        //    }
        //    set
        //    {
        //        if (lineid != value)
        //        {
        //            lineid = value;
        //            Notify("LineID");
        //        }
        //    }
        //}
        private string eqpid;
        public string EQPID
        {
            get
            {
                return eqpid;
            }
            set
            {
                if (eqpid != value)
                {
                    eqpid = value;
                    Notify("EQPID");                          
                }
            }
        }
        //spublic bool RemainedGlassFlag { get; set; }
        
        private bool remainedGlassFlag;
        public bool RemainedGlassFlag
        {
            get
            {
                return remainedGlassFlag;
            }
            set
            {
                if (remainedGlassFlag != value)
                {
                    remainedGlassFlag = value;
                    HostInfo.Current.RemainedGlassFlagModify();
                }
            }
        }
        private bool trninCVDFlag;
        public bool TrninCVDFlag
        {
            get
            {
                return trninCVDFlag;
            }
            set
            {
                //if (trninCVDFlag != value)
                //{
                    LogHelper.BCLog.Debug(string.Format("[TrninCVDFlag] Change ; TrninCVDFlag:{0}=>{1}", trninCVDFlag,value));
                    trninCVDFlag = value;
                    HostInfo.Current.CVDGlassFlagModify();
                //}
            }
        }
        //private string eqpname;
        //public string EQPName
        //{
        //    get
        //    {
        //        return eqpname;
        //    }
        //    set
        //    {
        //        if (eqpname != value)
        //        {
        //            eqpname = value;
        //            Notify("EQPName");
        //        }
        //    }
        //}

        private DateTime updatedate;
        public DateTime UpdateDate
        {
            get
            {
                return updatedate;
            }
            set
            {
                if (updatedate != value)
                {
                    updatedate = value;
                    Notify("UpdateDate");
                }
            }
        }
        private string reasonCode;
        public string ReasonCode
        {
            get
            {
                return reasonCode;
            }
            set
            {
                if (reasonCode != value)
                {
                    reasonCode = value;
                    Notify("ReasonCode");
                }
            }
        }

       
        private string eqpstatus;
        /// <summary>
        /// 1	PM	
        ///2	DOWN	
        ///3	Pause	
        ///4	IDLE	
        ///5	RUN        
        /// </summary>
        public string EqpStatus
        {
            get
            {
                return eqpstatus;
            }
            set
            {
                if (eqpstatus != value)
                {
                    eqpstatus = value;
                    Notify("EqpStatus");
                }
            }
        }
        private EnumControlState newcontrolState;
        public EnumControlState NewControlState
        {
            get
            {
                return newcontrolState;
            }
            set
            {
                if (newcontrolState != value)
                {
                    newcontrolState = value;
                    Notify("NewControlState");
                }
            }
        }
        private EnumControlState controlState ;
        public EnumControlState ControlState
        {
            get
            {
                return controlState;
            }
            set
            {
                if (controlState != value)
                {
                    controlState = value;
                    Notify("ControlState");
                }
            }
        }
        private EnumEqpAutoMode robotDispatchMode;
        public EnumEqpAutoMode RobotDispatchMode
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
        private int indexerOperationMode;
        public int IndexerOperationMode
        {
            get
            {
                return indexerOperationMode;
            }
            set
            {
                if (indexerOperationMode != value)
                {
                    indexerOperationMode = value;
                    Notify("IndexerOperationMode");
                }
            }
        }
        /// <summary>
        ///Normal = 1,
        ///TP = 2, 
        ///V3 = 3,
        ///CF=4,
        ///NIKON=5,
        ///ARRAY=6,
        ///MOD=7
        /// </summary>
        public EnumLineType LineType { get; set; }


        /// <summary>
        /// EquipmentStatus
        /// </summary>
        //public EquipmentStatus EquipmentStatus { get; set; }
        private bool opiConnect=false;
        public bool OPIConnect
        {
            get { return opiConnect; }
            set
            {
                if (opiConnect != value)
                {
                    opiConnect = value;
                    Notify("OPIConnect");
                }
            }
        }

        private bool isMESConnect = false;
        public bool IsMESConnect
        {
            get { return isMESConnect; }
            set
            {
                if (isMESConnect != value)
                {
                    isMESConnect = value;
                    Notify("IsMESConnect");
                }
            }
        }
        private bool isSPCConnect = false;
        public bool IsSPCConnect
        {
            get { return isSPCConnect; }
            set
            {
                if (isSPCConnect != value)
                {
                    isSPCConnect = value;
                    Notify("IsSPCConnect");
                }
            }
        }

        private bool isFDCConnect = false;
        public bool IsFDCConnect
        {
            get { return isFDCConnect; }
            set
            {
                if (isFDCConnect != value)
                {
                    isFDCConnect = value;
                    Notify("IsFDCConnect");
                }
            }
        }




        

        //新添加的
        public List<Unit> Units { get; set; }
        public LineMode LineMode { get; set; }
        /// <summary>
        /// ColdRun 模式总数量
        /// </summary>
        public int ColdRunTotalQuantity { get; set; }
        /// <summary>
        /// ColdRun 模式当前数量
        /// </summary>
        public int ColdRunCurrentQuantity { get; set; }

        public string FunctionName { get; set; }
        public bool IsColdRun { get; set; }
        /// <summary>
        /// pht600强排指定portid
        /// </summary>
        public string PHT600Port { get; set; }
        /// <summary>
        /// pht600强排port 当前的slot层数
        /// </summary>
        public int PHT600PortSlot { get; set; }
    }
}