using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class RobotCommandResult
    {
        public RobotCommand ExecuteCommand { get; set; }
        public string LowerLotId { get; set; }        
        public string UpperLotId { get; set; }
        //下手臂1 上手臂2 
        public int LowerCassetteSequenceNo1 { get; set; }
        public int LowerSlotSequenceNo1 { get; set; }
        public int LowerCassetteSequenceNo2 { get; set; }
        public int LowerSlotSequenceNo2 { get; set; }
     

        public int UpperCassetteSequenceNo1 { get; set; }       
        public int UpperSlotSequenceNo1 { get; set; }
        public int UpperCassetteSequenceNo2 { get; set; }
        public int UpperSlotSequenceNo2 { get; set; }

        public int ResultCode { get; set; }



    }
}
