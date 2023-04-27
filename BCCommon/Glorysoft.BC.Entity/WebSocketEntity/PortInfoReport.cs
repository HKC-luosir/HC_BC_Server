
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class PortInfoReport : BaseClass
    {
        public PortInfoReport()
        {
            PortList = new List<PortInfoReportPort>();
            
        }
        public string UnitID { get; set; }
        public List<PortInfoReportPort> PortList { get; set; }
    }
    public class PortInfoReportPort
    {
        private bool isingetput;
        public bool IsInGetPut
        {
            get { return isingetput; }
            set { isingetput = value; }
        }
        private int unitpathno;
        public int UnitPathNo
        {
            get { return unitpathno; }
            set { unitpathno = value; }
        }
        private string eqpID;
        public string EQPID
        {
            get { return eqpID; }
            set { eqpID = value; }
        }
        private int portStatus;
        /// <summary>
        /// 1 : Load Ready  2 : In Use 3 : Unload Ready 4 : Empty 5 : Blocked
        /// </summary>
        public int PortStatus
        {
            get { return portStatus; }
            set { portStatus = value; }
        }
        private DateTime updatedate;
        public DateTime UpdateDate
        {
            get { return updatedate; }
            set
            {
                if (updatedate != value)
                {
                    updatedate = value;
                }
            }
        }

        private string unitid;
        public string UnitID
        {
            get { return unitid; }
            set { unitid = value;  }
        }
  
        private string transfermode;
        public string TransferMode
        {
            get { return transfermode; }
            set { transfermode = value;  }
        }

        private int portpausemode;
        public int PortPauseMode
        {
            get { return portpausemode; }
            set { portpausemode = value; }
        }

        private string portmode;
        public string PortMode
        {
            get { return portmode; }
            set { portmode = value;}
        }

        private string lotid;
        public string LotID
        {
            get { return lotid; }
            set { lotid = value; }
        }
        private string lotJudge;
        public string LotJudge
        {
            get { return lotJudge; }
            set { lotJudge = value; }
        }
        private string ppid;
        public string PPID
        {
            get { return ppid; }
            set { ppid = value; }
        }
        private string lotstatus;
        public string LotStatus
        {
            get { return lotstatus; }
            set { lotstatus = value;  }
        }
        private string carrierState;
        public string CarrierState
        {
            get { return carrierState; }
            set { carrierState = value;  }
        }
        private string carrierType;
        public string CarrierType
        {
            get { return carrierType; }
            set { carrierType = value;  }
        }
        //public string cassettestatus { get; set; }
        private int cassettestatus;
        /// <summary>
        /// 1)No Cassette:2)Waiting for Cassette Data: 3)	Waiting for Start Command: 4)	Waiting for Processing;5)	In Processing;6)	Process Completed:7)	Process Paused: 
        /// </summary>
        public int CassetteStatus
        {
            get { return cassettestatus; }
            set { cassettestatus = value; }
        }
        private int getslot;
        public int GetSlot
        {
            get
            {
                return getslot;
            }
            set
            {
                if (getslot != value)
                {
                    getslot = value;
                }
            }
        }

        private int portNo;
        public int PortNo
        {
            get { return portNo; }
            set { portNo = value;}
        }
        private string portID;
        public string PortID
        {
            get { return portID; }
            set { portID = value; }
        }
        private string portUseType;
        public string PortUseType
        {
            get { return portUseType; }
            set
            {
                if (portUseType != value)
                {
                    portUseType = value;
                }
            }
        }
        private string productSpecName;
        public string ProductSpecName
        {
            get { return productSpecName; }
            set
            {
                if (productSpecName != value)
                {
                    productSpecName = value;
                }
            }
        }
        private string productSpecVersion;
        public string ProductSpecVersion
        {
            get { return productSpecVersion; }
            set
            {
                if (productSpecVersion != value)
                {
                    productSpecVersion = value;
                }
            }
        }

        private string portType;
        public string PortType
        {
            get { return portType; }
            set
            {
                if (portType != value)
                {
                    portType = value;
                }
            }
        }
        private string cassetteid;
        public string CassetteID
        {
            get { return cassetteid; }
            set { cassetteid = value; }
        }
        private int cassetteSequenceNo;
        /// <summary>
        /// 1~65535
        /// </summary>
        public int CassetteSequenceNo
        {
            get { return cassetteSequenceNo; }
            set { cassetteSequenceNo = value; }
        }
        private string machineRecipeName;
        public string MachineRecipeName
        {
            get { return machineRecipeName; }
            set { machineRecipeName = value;  }
        }
        private int jobCountInCassette;
        public int JobCountInCassette
        {
            get { return jobCountInCassette; }
            set { jobCountInCassette = value; }
        }
        private int completedCassetteData;
        /// <summary>
        /// //1 : Normal Complete 2 : Operator Forced to Cancel 3 : Operator Forced to Abort 4 : BC Forced to Cancel 5 : BC Forced to Abort
        /// </summary>
        public int CompletedCassetteData
        {
            get { return completedCassetteData; }
            set { completedCassetteData = value;  }
        }
        private string trayGroupName;
        /// <summary>
        /// TRAYGROUPNAME
        /// </summary>
        public string TrayGroupName
        {
            get { return trayGroupName; }
            set { trayGroupName = value; }
        }

        private string qty;
        public string Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private string firstrunQty;
        public string FIRSTRUNQTY
        {
            get { return firstrunQty; }
            set { firstrunQty = value; }
        }
        private string crateQty;
        public string CrateQty
        {
            get { return crateQty; }
            set { crateQty = value; }
        }
        private string slotSel;
        public string SlotSel
        {
            get { return slotSel; }
            set { slotSel = value; }
        }
        private string hSlotSel;
        public string HSlotSel
        {
            get { return hSlotSel; }
            set { hSlotSel = value;  }
        }
        private string sLotMap;
        /// <summary>
        /// /*if it is Half Cassette, Empty*/
        /// </summary>
        public string SLotMap
        {
            get { return sLotMap; }
            set { sLotMap = value; }
        }
        private string hsLotMap;
        /// <summary>
        /// /*if it is Half Cassette, Empty*/
        /// </summary>
        public string HSLotMap
        {
            get { return hsLotMap; }
            set { hsLotMap = value; }
        }
        private string cstEndFlag;
        public string CSTEndFlag
        {
            get { return cstEndFlag; }
            set { cstEndFlag = value; }
        }
        private string workOrder;
        public string WorkOrder
        {
            get { return workOrder; }
            set { workOrder = value;  }
        }
        private string processFlow;
        public string ProcessFlow
        {
            get { return processFlow; }
            set { processFlow = value; }
        }
        private string processFlowVersion;//LOTPROCESSFLOWVERSION
        public string ProcessFlowVersion
        {
            get { return processFlowVersion; }
            set { processFlowVersion = value;  }
        }
        private string processOperationName;//PROCESSOPERATIONNAME
        public string ProcessOperationName
        {
            get { return processOperationName; }
            set { processOperationName = value; }
        }
        private string processOperationVersion;//PROCESSOPERATIONVERSION
        public string ProcessOperationVersion
        {
            get { return processOperationVersion; }
            set { processOperationVersion = value; }
        }
        private string operID;
        public string OperID
        {
            get { return operID; }
            set { operID = value; }
        }
        private string operVersion;
        public string OperVersion
        {
            get { return operVersion; }
            set { operVersion = value; }
        }
        private string prodID;
        public string ProdID
        {
            get { return prodID; }
            set { prodID = value; }
        }
        private string productionVersion;
        public string ProductionVersion
        {
            get { return productionVersion; }
            set { productionVersion = value; }
        }
        private string productionType;
        public string ProductionType
        {
            get { return productionType; }
            set { productionType = value;  }
        }
        private string productQuantity;
        public string ProductQuantity
        {
            get { return productQuantity; }
            set { productQuantity = value; }
        }

        private string crateID;
        public string CrateID
        {
            get { return crateID; }
            set { crateID = value;  }
        }
        private string glsthk;
        public string GLSTHK
        {
            get { return glsthk; }
            set { glsthk = value; }
        }
        private string glsSize;
        public string GLSSize
        {
            get { return glsSize; }
            set { glsSize = value; }
        }
        private string maker;
        public string Maker
        {
            get { return maker; }
            set { maker = value; }
        }
        private string slotInfo;
        public string SlotInfo
        {
            get { return slotInfo; }
            set { slotInfo = value;  }
        }
        private string maskSlotMap;
        public string MaskSlotMap
        {
            get { return maskSlotMap; }
            set { maskSlotMap = value; }
        }
        private string inputMaskMap;
        public string InputMaskMap
        {
            get { return inputMaskMap; }
            set { inputMaskMap = value; }
        }

        private string maskCSTType;
        public string MaskCSTType
        {
            get { return maskCSTType; }
            set { maskCSTType = value; }
        }
        private string slotNO;
        public string SlotNO
        {
            get { return slotNO; }
            set { slotNO = value;}
        }


        private string portcsttype;
        /// <summary>
        ///1)	1AC: Actual Array Cassette
        ///2)	1EC: Actual Cell Glass Cassette
        ///3)	1EF : Actual Cell Film Cassette
        ///4)	1EM : Actual Cell Mask Cassette
        ///5)	2AC: Empty Array Cassette
        ///6)	2EC: Empty Cell Glass Cassette
        ///7)	2EF : Empty Cell Film Cassette
        ///8)	2EM : Empty Cell Mask Cassette
        /// </summary>
        public string PortCSTType
        {
            get { return portcsttype; }
            set { portcsttype = value;  }
        }
        private string portOperationMode;
        public string PortOperationMode
        {
            get { return portOperationMode; }
            set { portOperationMode = value; }
        }      

        private int portTypeAutoChangeMode;
        /// <summary>
        ///1)	Enable Mode : This mode can automatically change the port type.
        ///2)	Disable Mode : This mode cannot automatically change the port type.
        /// </summary>
        public int PortTypeAutoChangeMode
        {
            get { return portTypeAutoChangeMode; }
            set { portTypeAutoChangeMode = value;  }
        }
    }
}
