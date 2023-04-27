using Glorysoft.BC.Entity.RVEntity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class Cassette : NotifyPropertyChanged
    {
        public Cassette()
        {
            CassetteStatus = EnumCarrierStatus.NoCassette;
        }
        //        eqpid
        // unitid
        //unitname
        //portno
        //portid
        public string FunctionName { get; set; }
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string UnitName { get; set; }       
        public int PortNo { get; set; }
        public string PortID { get; set; }
        public string SlotToProcess { get; set; }
        public int JobCount { get; set; }
       
        //上一笔的CassetteStatus
        public int LastCstStatus { get; set; }
        //cassettesequenceno
        //cassettestatus
        //porttype
        //portstatus
        //cassetteid
        public int CassetteSequenceNo { get; set; } = 0;
        //public EnumCarrierStatus CassetteStatus { get; set; }

        private EnumCarrierStatus cassetteStatus;
        public EnumCarrierStatus CassetteStatus
        {
            get
            {
                return cassetteStatus;
            }
            set
            {
                if (cassetteStatus != value)
                {
                    cassetteStatus = value;
                    //if(cassetteStatus== EnumCarrierStatus.InProcessing|| cassetteStatus == EnumCarrierStatus.WaitingforProcessing )
                    //{
                    //    //HostInfo.Current.EQPInfo.RemainedGlassFlag = true;
                    //    var portList = HostInfo.Current.GetProcessPortList();
                    //    if (portList.Count() > 0)
                    //    {
                    //        LogHelper.BCLog.Debug(string.Format("[CassetteStatus Modify; PortID:{0};cassetteStatus:{1}]   RemainedGlassFlag = true;", PortID, cassetteStatus));
                    //        HostInfo.Current.EQPInfo.RemainedGlassFlag = true;
                    //    }
                    //    else
                    //    {
                    //        LogHelper.BCLog.Debug(string.Format("[CassetteStatus Modify; PortID:{0};cassetteStatus:{1}]   RemainedGlassFlag = false;", PortID, cassetteStatus));
                    //        HostInfo.Current.EQPInfo.RemainedGlassFlag = false;
                    //    }
                    //}                
                }
            }
        }

        public int PortType { get; set; }
        public int PortStatus { get; set; }
        public string CassetteID { get; set; }
       
        //lotname
        //processflowname
        //processflowversion
        //processoperationname
        //processoperationversion
        public string LotName { get; set; }
        public string LotType { get; set; }
        public string LotGrade { get; set; }
        public string ProcessFlowName { get; set; }
        public string ProcessFlowVersion { get; set; }
        public string ProcessOperationName { get; set; }
        public string ProcessOperationVersion { get; set; }

        //carriertype
        //portusetype
        //productspecname
        //productspecversion
        //productiontype
      
        public string CarrierType { get; set; }
        public string PortUseType { get; set; }
        public string ProductSpecName { get; set; }
        public string ProductSpecVersion { get; set; }
        public string ProductionType { get; set; }
       
        //productquantity
        //slotmap
        //slotsel
        //machinerecipename
        //workorder
        public int ProductQuantity { get; set; }
   
        public string SlotSel { get; set; }
        public string MachineRecipeName { get; set; }
        public string WorkOrder { get; set; }
       
        //lotjudge
        //cassetteprocessendtime
        //cassetteprocessstarttime
        //updatedate
        //qtimeflag
        public string LotJudge { get; set; }
        public DateTime CassetteProcessEndTime { get; set; }
        public DateTime CassetteProcessStartTime { get; set; }
        public DateTime UpdateDate { get; set; }
        public int QTimeFlag { get; set; }
       
        //cassettecode
        //compeletedcassettedata
        //jobexistenceslot
        public string CassetteCode { get; set; }
        public int CompeletedCassetteData { get; set; }
        /// <summary>
        /// 0011
        /// </summary>
        public string JobExistenceSlot { get; set; }
        /// <summary>
        /// OOXX
        /// </summary>
        public string SlotMap { get; set; }
        //public bool HasCVD { get; set; }

        private bool hascvd;
        public bool HasCVD
        {
            get
            {
                return hascvd;
            }
            set
            {
                if (hascvd != value)
                {
                    hascvd = value;                    
                }
            }
        }
        public int ID { get; set; }

        public List<UNITRECIPE> UNITRECIPELIST = new List<UNITRECIPE>();
    }
}
