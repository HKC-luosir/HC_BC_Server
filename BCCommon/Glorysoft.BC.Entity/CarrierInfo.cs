using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace Glorysoft.BC.Entity
{
    public class CarrierInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
        public CarrierInfo()
        {
            cstBoxList = new ObservableCollection<CSTBoxInfo>();
            panelList = new ObservableCollection<SPanelInfo>();
            SlotMap = "";
            InputProductMap = "";
            LotID = "";
            LotGrade = "";
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

        private string eqpID;
        public string EQPID
        {
            get { return eqpID; }
            set { eqpID = value; Notify("EQPID"); }
        }

        private string portID;
        public string PortID
        {
            get { return portID; }
            set
            {
                portID = value;
                Notify("PortID");
            }
        }

        private string carrierID;
        public string CarrierID
        {
            get { return carrierID; }
            set
            {
                if (carrierID != value)
                {
                    carrierID = value;
                    Notify("CarrierID");
                }
            }
        }


        private int carrierType;
        public int CarrierType
        {
            get
            {
                return carrierType;
            }
            set
            {
                if (carrierType != value)
                {
                    carrierType = value;
                    Notify("CarrierType");
                }
            }
        }

        private string mesCarrierType;
        public string MESCarrierType
        {
            get
            {
                return mesCarrierType;
            }
            set
            {
                if (mesCarrierType!=value)
                {
                    mesCarrierType = value;
                    Notify("MESCarrierType");
                }
            }
        }

        private int portStatus;
        public int PortStatus
        {
            get
            {
                return portStatus;
            }
            set
            {
                if (portStatus != value)
                {
                    portStatus = value;
                    Notify("PortStatus");
                }
            }
        }

        private int carrierStatus;
        public int CarrierStatus
        {
            get
            {
                return carrierStatus;
            }
            set
            {
                if (carrierStatus != value)
                {
                    carrierStatus = value;
                    Notify("CarrierStatus");
                }
            }
        }
        private string lotID;
        public string LotID
        {
            get
            {
                return lotID;
            }
            set
            {
                if (lotID != value)
                {
                    lotID = value;
                    Notify("LotID");
                }
            }
        }

        private string lotGrade;
        public string LotGrade
        {
            get
            {
                return lotGrade;
            }
            set
            {
                if (lotGrade != value)
                {
                    lotGrade = value;
                    Notify("LotGrade");
                }
            }
        }
        public string ProductSpecID { get; set; }
        public string OperationID { get; set; }
        public string ProductionType { get; set; }
        public string SlotMap { get; set; }
        public string InputProductMap { get; set; }

        private ObservableCollection<SPanelInfo> panelList;
        public ObservableCollection<SPanelInfo> PanelList
        {
            get
            {
                return panelList;
            }
            set
            {
                panelList = value;
                Notify("PanelList");
            }
        }

        private ObservableCollection<CSTBoxInfo> cstBoxList;
        public ObservableCollection<CSTBoxInfo> CSTBoxList
        {
            get
            {
                return cstBoxList;
            }
            set
            {
                cstBoxList = value;
                Notify("CSTBoxList");
            }
        }

        private ObservableCollection<GlassInfo> glassList;
        public ObservableCollection<GlassInfo> GlassList
        {
            get
            {
                return glassList;
            }
            set
            {
                glassList = value;
                Notify("GlassList");
            }
        }


    }
}
