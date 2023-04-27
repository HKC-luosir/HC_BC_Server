using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class PortInfo : Unit
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void Notify(string propertyName)
        //{
        //    if (PropertyChanged == null) return;
        //    var e = new PropertyChangedEventArgs(propertyName);
        //    PropertyChanged(this, e);
        //}

        public PortInfo()
        {
            //PortStatus = EnumPortStatus.NoCassette;
            //CarrierStatus = EnumCarrierStatus.NoCassetteExist;
            //PortType = EnumPortType.Invalid;
            //PortUseType = EnumPortUseType.Invalid;
            //PortTransferMode = EnumPortMode.Invalid;
            //UpdateDate = DateTime.Now;
            CassetteInfo = new Cassette();
        }
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }
        public int PortNo { get; set; }
        public bool IsMESCarrierInfoDownload { get; set; } = false;
        public EnumCassetteControlCommand CassetteControlCommand { get; set; }
        public string PortID { get; set; }
        /// <summary>
        ///Empty = 1, 
        ///Load Ready = 2, 
        ///In Use  = 3, 
        ///Unload Ready = 4
        ///Blocked = 5
        /// </summary>
        public int PortStatus { get; set; }
        public string PortType { get; set; }
        public string TransferMode { get; set; }
        public int PortPauseMode { get; set; }
        public int PortMode { get; set; }
        //public string PortGradeGroup { get; set; }
        public string PortUseType { get; set; }
        public int UseType { get; set; }
        /// <summary>
        /// 1AC
        /// 1EC
        /// 1EF
        /// 1EM
        /// 2AC
        /// 2EC
        /// 2EF
        /// 2EM
        /// </summary>
        public int PortCSTType { get; set; }
        public EnumPortOperationMode PortOperationMode { get; set; }
        public int PortTypeAutoChangeMode { get; set; }
        /// <summary>
        /// 一卡 Glass数量
        /// </summary>
        public int Capacity { get; set; }
        public int PortQTime { get; set; }
       
        public int GlassType { get; set; }
        public string ProductionType { get; set; }
        public string PortGrade { get; set; }
        public int PortEnableMode { get; set; }
        public int Userms { get; set; }
        public DateTime UpdateDate { get; set; }


        public int CassetteSequenceNo { get; set; }
        public string CassetteID { get; set; }
        private List<GlassInfo> glassInfos = new List<GlassInfo>();
        public List<GlassInfo> GlassInfos
        {
            get
            {

                return glassInfos==null?new List<GlassInfo> ():glassInfos;
            }
            set
            {
                if (glassInfos != value)
                {
                    glassInfos = value;
                    Notify("GlassInfos");
                }
            }
        }
        public Cassette CassetteInfo { get; set; }
        public string CassetteCancelText { get; set; } = "";
        public bool IsInGetPut { get; set; }
        public DateTime WaitingforProcessingTime { get; set; }

    }
}
