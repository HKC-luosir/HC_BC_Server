

using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class PackingBox : Unit
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void Notify(string propertyName)
        //{
        //    if (PropertyChanged == null) return;
        //    var e = new PropertyChangedEventArgs(propertyName);
        //    PropertyChanged(this, e);
        //}

        public PackingBox()
        {
            //PortStatus = EnumPortStatus.NoCassette;
            //CarrierStatus = EnumCarrierStatus.NoCassetteExist;
            //PortType = EnumPortType.Invalid;
            //PortUseType = EnumPortUseType.Invalid;
            //PortTransferMode = EnumPortMode.Invalid;
            //UpdateDate = DateTime.Now;

        }

        private string lineID;
        public string LineID
        {
            get
            {
                return lineID;
            }
            set
            {
                if (lineID != value)
                {
                    lineID = value;
                    Notify("LineID");
                }
            }
        }


        private int unitpathno;
        public int UnitPathNo
        {
            get { return unitpathno; }
            set { unitpathno = value; Notify("UnitPathNo"); }
        }
        private string eqpID;
        public string EQPID
        {
            get { return eqpID; }
            set { eqpID = value; Notify("EQPID"); }
        }
        private string eqpName;
        public string EQPName
        {
            get { return eqpName; }
            set { eqpName = value; Notify("EQPName"); }
        }
        private int portStatus;
        public int PortStatus
        {
            get { return portStatus; }
            set { portStatus = value; Notify("PortStatus"); }
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
                    Notify("UpdateDate");
                }
            }
        }

        private string transfermode;
        public string TransferMode
        {
            get { return transfermode; }
            set { transfermode = value; Notify("TransferMode"); }
        }
        private string portmode;
        public string PortMode
        {
            get { return portmode; }
            set { portmode = value; Notify("PortMode"); }
        }
        private string lotid;
        public string LotID
        {
            get { return lotid; }
            set { lotid = value; Notify("LotID"); }
        }
        private string ppid;
        public string PPID
        {
            get { return ppid; }
            set { ppid = value; Notify("PPID"); }
        }
        private string lotstatus;
        public string LotStatus
        {
            get { return lotstatus; }
            set { lotstatus = value; Notify("LotStatus"); }
        }
        private string carrierStatus;
        public string CarrierStatus
        {
            get { return carrierStatus; }
            set { carrierStatus = value; Notify("CarrierStatus"); }
        }
        //public string cassettestatus { get; set; }
        private int cassettestatus;
        public int CassetteStatus
        {
            get { return cassettestatus; }
            set { cassettestatus = value; Notify("CassetteStatus"); }
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
                    Notify("GetSlot");
                }
            }
        }


        private string boxID;
        public string BoxID
        {
            get { return boxID; }
            set { boxID = value; Notify("BoxID"); }
        }
        private string prodID;
        public string ProdID
        {
            get { return prodID; }
            set { prodID = value; Notify("ProdID"); }
        }
        private string date;
        public string Date
        {
            get { return date; }
            set { date = value; Notify("Date"); }
        }
        private string qty;
        public string Qty
        {
            get { return qty; }
            set { qty = value; Notify("Qty"); }
        }





        private ObservableCollection<GlassInfo> panelInfos = new ObservableCollection<GlassInfo>();
        public ObservableCollection<GlassInfo> PanelInfos
        {
            get
            {
                return panelInfos;
            }
            set
            {
                if (panelInfos != value)
                {
                    panelInfos = value;
                    Notify("PanelInfos");
                }
            }
        }
        //#region Robot Dispatch
        // public bool IsInGetPut { get; set; }
        //#endregion

    }
}
