
//using System;
//using System.ComponentModel;
//using System.Collections.ObjectModel;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace Glorysoft.BC.Entity
//{
//    public class MaskPortInfo : Unit
//    {
//        //public event PropertyChangedEventHandler PropertyChanged;
//        //protected void Notify(string propertyName)
//        //{
//        //    if (PropertyChanged == null) return;
//        //    var e = new PropertyChangedEventArgs(propertyName);
//        //    PropertyChanged(this, e);
//        //}

//        public MaskPortInfo()
//        {
//            //PortStatus = EnumPortStatus.NoCassette;
//            //CarrierStatus = EnumCarrierStatus.NoCassetteExist;
//            //PortType = EnumPortType.Invalid;
//            //PortUseType = EnumPortUseType.Invalid;
//            //PortTransferMode = EnumPortMode.Invalid;
//            //UpdateDate = DateTime.Now;

//        }

//        private string lineID;
//        public string LineID
//        {
//            get
//            {
//                return lineID;
//            }
//            set
//            {
//                if (lineID != value)
//                {
//                    lineID = value;
//                    Notify("LineID");
//                }
//            }
//        }

       
//        private int unitpathno;
//        public int UnitPathNo
//        {
//            get { return unitpathno; }
//            set { unitpathno = value; Notify("UnitPathNo"); }
//        }
//        private string eqpID;
//        public string EQPID
//        {
//            get { return eqpID; }
//            set { eqpID = value; Notify("EQPID"); }
//        }
       
//        private int portStatus;
//        public int PortStatus
//        {
//            get { return portStatus; }
//            set { portStatus = value; Notify("PortStatus"); }
//        }

//        private DateTime updatedate;
//        public DateTime UpdateDate
//        {
//            get { return updatedate; }
//            set
//            {
//                if (updatedate != value)
//                {
//                    updatedate = value;
//                    Notify("UpdateDate");
//                }
//            }
//        }

//        private string transfermode;
//        public string TransferMode
//        {
//            get { return transfermode; }
//            set { transfermode = value; Notify("TransferMode"); }
//        }
//        private string portmode;
//        public string PortMode
//        {
//            get { return portmode; }
//            set { portmode = value; Notify("PortMode"); }
//        }
//        private string lotid;
//        public string LotID
//        {
//            get { return lotid; }
//            set { lotid = value; Notify("LotID"); }
//        }
//        private string ppid;
//        public string PPID
//        {
//            get { return ppid; }
//            set { ppid = value; Notify("PPID"); }
//        }
//        private string lotstatus;
//        public string LotStatus
//        {
//            get { return lotstatus; }
//            set { lotstatus = value; Notify("LotStatus"); }
//        }
//        private string carrierStatus;
//        public string CarrierStatus
//        {
//            get { return carrierStatus; }
//            set { carrierStatus = value; Notify("CarrierStatus"); }
//        }
//        //public string cassettestatus { get; set; }
//        private int cassettestatus;
//        public int CassetteStatus
//        {
//            get { return cassettestatus; }
//            set { cassettestatus = value; Notify("CassetteStatus"); }
//        }
//        private int getslot;
//        public int GetSlot
//        {
//            get
//            {
//                return getslot;
//            }
//            set
//            {
//                if (getslot != value)
//                {
//                    getslot = value;
//                    Notify("GetSlot");
//                }
//            }
//        }

//        private int portNo;
//        public int PortNo
//        {
//            get { return portNo; }
//            set { portNo = value; Notify("PortNo"); }
//        }
//        private string portID;
//        public string PortID
//        {
//            get { return portID; }
//            set { portID = value; Notify("PortID"); }
//        }
//        private string portType;
//        public string PortType
//        {
//            get { return portType; }
//            set
//            {
//                if (portType != value)
//                {
//                    portType = value;
//                    Notify("PortType");
//                }
//            }
//        }
//        private string portUseType;
//        public string PortUseType
//        {
//            get { return portUseType; }
//            set
//            {
//                if (portUseType != value)
//                {
//                    portUseType = value;
//                    Notify("PortUseType");
//                }
//            }
//        }       
//        private string cassetteid;
//        public string CassetteID
//        {
//            get { return cassetteid; }
//            set { cassetteid = value; Notify("CassetteID"); }
//        }
//        private string unitID;
//        public string UnitID
//        {
//            get { return unitID; }
//            set { unitID = value; Notify("UnitID"); }
//        }
//        private string sUnitID;
//        public string SUnitID
//        {
//            get { return sUnitID; }
//            set { sUnitID = value; Notify("SUnitID"); }
//        }
//        private string maskCSTType;
//        public string MaskCSTType
//        {
//            get { return maskCSTType; }
//            set { maskCSTType = value; Notify("MaskCSTType"); }
//        }
//        private string cstEndFlag;
//        public string CSTEndFlag
//        {
//            get { return cstEndFlag; }
//            set { cstEndFlag = value; Notify("CSTEndFlag"); }
//        }
//        private string qty;
//        public string Qty
//        {
//            get { return qty; }
//            set { qty = value; Notify("Qty"); }
//        }
//        private string slotNO;
//        public string SlotNO
//        {
//            get { return slotNO; }
//            set { slotNO = value; Notify("SlotNO"); }
//        }
//        private string maskSlotMap;
//        public string MaskSlotMap
//        {
//            get { return maskSlotMap; }
//            set { maskSlotMap = value; Notify("MaskSlotMap"); }
//        }
//        private string inputMaskMap;
//        public string InputMaskMap
//        {
//            get { return inputMaskMap; }
//            set { inputMaskMap = value; Notify("InputMaskMap"); }
//        }


//        private ObservableCollection<MaskInfo> maskInfos = new ObservableCollection<MaskInfo>();
//        public ObservableCollection<MaskInfo> MaskInfos
//        {
//            get
//            {
//                return maskInfos;
//            }
//            set
//            {
//                if (maskInfos != value)
//                {
//                    maskInfos = value;
//                    Notify("MaskInfos");
//                }
//            }
//        }
//        //#region Robot Dispatch
//        // public bool IsInGetPut { get; set; }
//        //#endregion

//    }
//}
