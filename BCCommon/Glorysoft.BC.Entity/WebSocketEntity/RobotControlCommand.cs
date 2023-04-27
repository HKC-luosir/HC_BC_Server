using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
  
    public class RobotControlCommand : BaseClass
    {
        public RobotControlCommand()
        {
        }
        //SequenceNo
        //stRCMD1
        //stArmNo1
        //stGetPosition1
        //stPutPosition1
        public string SequenceNo { get; set; }
        public string stRCMD1 { get; set; }
        public string stArmNo1 { get; set; }
        public string stGetPosition1 { get; set; }
        public string stPutPosition1 { get; set; }
        //stGetSlotNo1
        //stPutSlotNo1
        //stSubCommand1
        //stGetSlotPostion1
        //stPutSlotPostion1
        public string stGetSlotNo1 { get; set; }
        public string stPutSlotNo1 { get; set; }
        public string stSubCommand1 { get; set; }
        public string stGetSlotPostion1 { get; set; }
        public string stPutSlotPostion1 { get; set; }
        //ndRCMD2
        //ndArmNo2
        //ndGetPosition2
        //ndPutPosition2
        //ndGetSlotNo2
        public string ndRCMD2 { get; set; }
        public string ndArmNo2 { get; set; }
        public string ndGetPosition2 { get; set; }
        public string ndPutPosition2 { get; set; }
        public string ndGetSlotNo2 { get; set; }
        //ndPutSlotNo2
        //ndSubCommand2
        //ndGetSlotPostion2
        //ndPutSlotPostion2
        //rdRCMD3
        public string ndPutSlotNo2 { get; set; }
        public string ndSubCommand2 { get; set; }
        public string ndGetSlotPostion2 { get; set; }
        public string ndPutSlotPostion2 { get; set; }
        public string rdRCMD3 { get; set; }
        //rdArmNo3
        //rdGetPosition3
        //rdPutPosition3
        //rdGetSlotNo3
        //rdPutSlotNo3
        public string rdArmNo3 { get; set; }
        public string rdGetPosition3 { get; set; }
        public string rdPutPosition3 { get; set; }
        public string rdGetSlotNo3 { get; set; }
        public string rdPutSlotNo3 { get; set; }
        //rdSubCommand3
        //rdGetSlotPostion3
        //rdPutSlotPostion3
        //thRCMD4
        //thArmNo4
        public string rdSubCommand3 { get; set; }
        public string rdGetSlotPostion3 { get; set; }
        public string rdPutSlotPostion3 { get; set; }
        public string thRCMD4 { get; set; }
        public string thArmNo4 { get; set; }
        //thGetPosition4
        //thPutPosition4
        //thGetSlotNo4
        //thPutSlotNo4
        //thSubCommand4
        public string thGetPosition4 { get; set; }
        public string thPutPosition4 { get; set; }
        public string thGetSlotNo4 { get; set; }
        public string thPutSlotNo4 { get; set; }
        public string thSubCommand4 { get; set; }
        //thGetSlotPostion4
        //thPutSlotPostion4
        public string thGetSlotPostion4 { get; set; }
        public string thPutSlotPostion4 { get; set; }
    }
}
