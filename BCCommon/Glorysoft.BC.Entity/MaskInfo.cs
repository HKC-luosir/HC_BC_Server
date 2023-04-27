
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class MaskInfo : NotifyPropertyChanged
    {
        public DateTime CurrentDateTime { get; set; }

        public string OutUnitName { get; set; }
        public string OutSUnitName { get; set; }
        public DateTime CreateDate { get; set; }
        public string FPorTID { get; set; }
         public string TPorTID { get; set; }
        //<result property = "OutUnitName" column="outunitname"/>
        //<result property = "OutSUnitName" column="outsunitname"/>
        //<result property = "CreateDate" column="createdate"/>
        //<result property = "FPorTID" column="fportid"/>
        //<result property = "TPorTID" column="tportid"/>
        public string FCSTID { get; set; }
        public string TCSTID { get; set; }
       
        public bool IsOutPort { get; set; }
        public bool IsExist { get; set; }
        public int SlotSatus { get; set; }
        //<result property = "FCSTID" column="fcstid"/>
        //<result property = "TCSTID" column="tcstid"/>
        //<result property = "IsOutPort" column="isoutport"/>
        //<result property = "IsExist" column="isexist"/>
        //<result property = "SlotSatus" column="slotsatus"/>

        public DateTime FetchDatetime { get; set; }        
        public string MASKID { get; set; }
        public string MaskGroupName { get; set; }
        public string MaskType { get; set; }
        public string POSITION { get; set; }
        //<result property = "FetchDatetime" column="fetchdatetime"/>
        //<result property = "MASKID" column="maskid"/>
        //<result property = "MaskGroupName" column="maskgroupname"/>
        //<result property = "MaskType" column="masktype"/>
        //<result property = "POSITION" column="position"/>
        public string MASKRECIPENAME { get; set; }
        public string MaskCLNState { get; set; }
        public string MaskAOIState { get; set; }
        public string MASKREPAIRCNT { get; set; }
        public string MASKINSPSTATE { get; set; }
       
       
       
        //<result property = "MASKRECIPENAME" column="maskrecipename"/>
        //<result property = "MaskCLNState" column="maskclnstate"/>
        //<result property = "MaskAOIState" column="maskaoistate"/>
        //<result property = "MASKREPAIRCNT" column="maskrepaircnt"/>
        //<result property = "MASKINSPSTATE" column="maskinspstate"/>
        public string MASKNGCODE { get; set; }
        public string MASKAMHSZONE { get; set; }
       
        public string SHELFNO { get; set; }
        public string PPID { get; set; }
        /// <summary>
        ///  / Mask CST slot number/
        /// </summary>
        public string FSlotNO { get; set; }
        //<result property = "MASKNGCODE" column="maskngcode"/>
        //<result property = "MASKAMHSZONE" column="maskamhszone"/>
        //<result property = "SHELFNO" column="shelfno"/>
        //<result property = "PPID" column="ppid"/>
        //<result property = "FSlotNO" column="fslotno"/>
        public string TSlotNO { get; set; }
        /// <summary>
        /// / insert sub-unit/
        /// </summary>
        public string MaskInSUnitID { get; set; }
        /// <summary>
        //// sub-unit Stage number
        /// </summary>
        public string SSLotNO { get; set; }
        public string MaskMaxCnt { get; set; }
        public string MaskUseCnt { get; set; }
        //<result property = "TSlotNO" column="tslotno"/>
        //<result property = "MaskInSUnitID" column="maskinsunitid"/>
        //<result property = "SSLotNO" column="sslotno"/>
        //<result property = "MaskMaxCnt" column="maskmaxcnt"/>
        //<result property = "MaskUseCnt" column="maskusecnt"/>
        public string OffSetX { get; set; }
        public string OffSetY { get; set; }
        public string OffSetT { get; set; }
        public string MaskMagnet { get; set; }
        public string MaskThickness { get; set; }
        //<result property = "OffSetX" column="offsetx"/>
        //<result property = "OffSetY" column="offsety"/>
        //<result property = "OffSetT" column="offsett"/>
        //<result property = "MaskMagnet" column="maskmagnet"/>
        //<result property = "MaskThickness" column="maskthickness"/>
        //public string MASKMODELNO { get; set; }
        public string Prodid { get; set; }
        public string MaskSpec { get; set; }
        public string MaskJudge { get; set; }
        public string MaskModelNo { get; set; }
        //<result property = "MASKMODELNO" column="maskmodelno"/>
        //<result property = "Prodid" column="prodid"/>
        //<result property = "MaskSpec" column="maskspec"/>
        //<result property = "MaskJudge" column="maskjudge"/>
        //<result property = "MaskModelNo" column="maskmodelno"/>
        public string ProcessingFlag { get; set; }
        public string ProcessFlow { get; set; }
        public string ProcessFlowVersion { get; set; }
        public string Operid { get; set; }
        public string OperVersion { get; set; }
        //<result property = "ProcessingFlag" column="processingflag"/>
        //<result property = "ProcessFlow" column="processflow"/>
        //<result property = "ProcessFlowVersion" column="processflowversion"/>
        //<result property = "Operid" column="operid"/>
        //<result property = "OperVersion" column="operversion"/>
        public string MaskStatus { get; set; }

        public string PROCESSOPERATIONNAME { get; set; }
        public string PROCESSOPERATIONVERSION { get; set; }
        public string MASKUSEDLIMIT { get; set; }
        public string CHAMBERNAME { get; set; }
        //<result property = "MaskStatus" column="maskstatus"/>
        //<result property = "PROCESSOPERATIONNAME" column="processoperationname"/>
        //<result property = "PROCESSOPERATIONVERSION" column="processoperationversion"/>
        //<result property = "MASKUSEDLIMIT" column="maskusedlimit"/>
        //<result property = "CHAMBERNAME" column="chambername"/>
        public string STAGENAME { get; set; }

        public int FCassetteSequence { get; set; }
        public int TCassetteSequence { get; set; }
        //<result property = "STAGENAME" column="stagename"/>
        //<result property = "FCassetteSequence" column="fcassettesequence"/>
        //<result property = "TCassetteSequence" column="tcassettesequence"/>







        public string UnitID { get; set; }
        public string SUnitID { get; set; }
        public string FSlotPosition { get; set; }
        public string TSlotPosition { get; set; }
       
      
        ////<result property = "UnitID" column="unitid"/>    
        ////<result property = "SUnitID" column="sunitid"/>    
        ////<result property = "FSlotPosition" column="fslotposition"/>    
        ////<result property = "TSlotPosition" column="tslotposition"/>    
        ////<result property = "SSLOTNO" column="sslotno"/>    
        ////<result property = "MASKGroupName" column="maskgroupname"/>   
        //unitid,sunitid,fslotposition,tslotposition,sslotno,maskgroupname
        //UnitID,SUnitID,FSlotPosition,TSlotPosition,SSLOTNO,MASKGroupName
        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}


