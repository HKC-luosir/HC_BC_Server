using System;

namespace Glorysoft.BC.Entity
{
    public class AlarmInfo
    {
        public AlarmInfo()
        {
           // AlarmEnable = true;
           // AlarmID = "";
            AlarmText = "";
        }
        public int ID { get; set; }
        public string AlarmID { get; set; }
        public string EQPID { get; set; }
        public string UNITID { get; set; }
        public string UNITNO{ get; set; }
        public string SUNITID { get; set; }
        public string AlarmText { get; set; }
        public string AlarmStatus { get; set; }
        public string AlarmCode { get; set; }
        //AlarmLevel 作用和alarmtype一样   不使用
        //public string AlarmLevel { get; set; }
        //   public bool AlarmEnable { get; set; }
        public DateTime CreateDate { get; set; }
        public string AlarmType { get; set; }
        public string AlarmUnitNumber { get; set; }
    }
}