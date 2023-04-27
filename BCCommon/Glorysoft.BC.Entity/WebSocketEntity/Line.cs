using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class Line
    {

        public List<Equipments> equipments { get; set; }
        public string recipeName { get; set; }
        public string partialFullMode { get; set; }
        public string recipeByMES { get; set; }
        public string firstRunMode { get; set; }
        public int firstRunGlassCount { get; set; }
        public string fastRunMode { get; set; }
        public bool jobDataCheckPosition { get; set; }
        public bool inLineMergeMode { get; set; }
        public string subBlockEnable { get; set; }
        public BatonPassStatusEnum batonPassStatusEnum { get; set; }
        public BatonPassInterruptionEnum batonPassInterruptionEnum { get; set; }
        public DataLinkStopEnum dataLinkStopEnum { get; set; }
        public StationLoopStatusEnum stationLoopStatusEnum { get; set; }
        public bool mplcCCLinkStatus { get; set; }
        public bool mplcCydlcTransmissionStatus { get; set; }
        public TemperatureSettings temperatureSettings { get; set; }
        public string ppidEmptyByPass { get; set; }
        public bool spanLineUpstreamLine { get; set; }
        public string engMode { get; set; }
        public string lineType { get; set; }
        public string mesControlMode { get; set; }
        public Array linkSignals { get; set; }
        public string lineStatus { get; set; }
        public string indexOperationMode { get; set; }
        public string lineId { get; set; }
        public string equipmentNo { get; set; }
        public string uuid { get; set; }
        public string lineOperationMode { get; set; }
        public int lineOperationModeCode { get; set; }
        public string dispatchMode { get; set; }
        public int coldRunTotalQuantity { get; set; } //计划产量
        public int coldRunCurrentQuantity { get; set; }  //实际产量

    }
}
