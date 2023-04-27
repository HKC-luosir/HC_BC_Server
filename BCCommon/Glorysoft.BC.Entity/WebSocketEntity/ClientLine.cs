using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class ClientLine
    {
        public string lineOperationMode { get; set; }
        public int lineOperationModeCode { get; set; }
        public string indexOperationMode { get; set; }
        public List<Equipments> equipments { get; set; }
        public List<linkSignal> linkSignal { get; set; }
        public string fastRunMode { get; set; }
        public string mesControlMode { get; set; }  //mes状态   RUN/DOWN
        public string lineStatus { get; set; }  //线体状态     RUN/DOWN
        public string lineId { get; set; }      //线体ID   2TTM02
        public string batonPassStatus { get; set; }
        public string batonPassInterruption { get; set; }
        public string dataLinkStop { get; set; }
        public string stationLoopStatus { get; set; }
        public bool mplcCCLinkStatus { get; set; }
        public bool mplcCydlcTransmissionStatus { get; set; }
        public IList<cfg_glassexistenceposition> dynamicTags { get; set; }
        public string engMode { get; set; }
        public string lineType { get; set; }  //线体类型
        public string dispatchMode { get; set; }  //线体类型
        public int coldRunTotalQuantity { get; set; } //计划产量
        public int coldRunCurrentQuantity { get; set; }  //实际产量
        public bool isColdRun { get; set; }
        public bool EIPisConnected { get; set; } //BC的所有EIP通讯连接状态
        public string pht600PortInfo { get; set; }
        public int pht600PortSlot { get; set; }
    }
}
