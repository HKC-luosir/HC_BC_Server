using Glorysoft.BC.Entity;
using System;

namespace  Glorysoft.BC.GlassDispath
{
    public class JobStage
    {
        public JobStage(EnumUnitType type,  string unitName, int ModelPosition)
        {
            try
            {
                Type = type;
                //Name = unit.UnitName;
                this.ModelPosition = ModelPosition;
                //this.ModelNo = ModelNo;
               // Data = unit;
               // StageType = unit.Capacity > 1 ? EStageType.Multi : EStageType.Single;
                UnitName = unitName;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
        }
        //public JobStage(PortInfo port, string unitName,int ModelPosition,int ModelNo)
        //{
        //    try
        //    {
        //        Type = EnumUnitType.Port;
        //        this.ModelPosition = ModelPosition;
        //        this.ModelNo = ModelNo;
        //        //PathNo = port.UnitNo;
        //       // Data = port;
        //        StageType = EStageType.Multi;
        //        UnitName = unitName;
        //    }
        //    catch(Exception ex)
        //    {
        //        LogHelper.BCLog.Debug(ex);
        //    }
           
        //}
        public JobStage()
        { }
        //public int ModelNo { get; private set; }
        public int ModelPosition { get; private set; }
        public string UnitName { get; private set; }
        //public int PathNo { get; private set; }
       // public EStageType StageType { get; private set; }
        public EnumUnitType Type { get; private set; }

        public RobotPathConfigure PathConfigure { get; set; }
        //public object Data { get; set; }
        //public System.DateTime ReadyTime
        //{
        //    get
        //    {
        //        var unit = Data as Unit;
        //        return unit.ReadyTime;
        //    }
        //}
    }
}
