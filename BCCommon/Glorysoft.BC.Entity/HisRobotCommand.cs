using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
   public class HisRobotCommand
    {
        public int SequenceNo { get; set; }//0-10000   到1w恢复0
        //int stRCMD1, int stArmNo1,int stGetPosition1,int stPutPosition1,int stGetSlotNo1,        
       
        public string STRCMD1 { get; set; }
        /// <summary>
        /// 上下手臂：下手臂1 上手臂2 
        /// </summary>
        public string STArmNo1 { get; set; }
        
        public int STGetPosition1 { get; set; }
       
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
        public string STSubCommand1 { get; set; }
        /// <summary>
        /// 取片命令使用    Glass Postion
        /// </summary>
        public int STGetSlotPostion1 { get; set; }
        /// <summary>
        ///  放片命令使用    Glass Postion
        /// </summary>
        public int STPutSlotPostion1 { get; set; }
       
        // int ndRCMD2,int ndArmNo2,int ndGetPosition2,int ndPutPosition2,int ndGetSlotNo2,
        public string NDRCMD2 { get; set; }
        /// <summary>
        /// 上下手臂：下手臂1 上手臂2 
        /// </summary>
        public string NDArmNo2 { get; set; }
        public int NDGetPosition2 { get; set; }
        public int NDPutPosition2 { get; set; }
        public int NDGetSlotNo2 { get; set; }
       
        //int NDPutSlotNo2,int NDSubCommaND2,int NDGetSlotPostion2,int NDPutSlotPostion2,
        public int NDPutSlotNo2 { get; set; }
        public string NDSubCommand2 { get; set; }
        public int NDGetSlotPostion2 { get; set; }
        public int NDPutSlotPostion2 { get; set; }
       
        public int CommandResult1 { get; set; }
        public int CommandResult2 { get; set; }
        public int CommandResult3 { get; set; }
        public int CommandResult4 { get; set; }
        public int CurrentPosition { get; set; }
        public string FunctionName { get; set; }
        public DateTime CreateDate { get; set; }
   
    }
}
