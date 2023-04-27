using Glorysoft.BC.Entity;
namespace  Glorysoft.BC.GlassDispath
{
    public class RobotPathConfigure
    {
        public string LineName { get; set; }
        public string IndexerName { get; set; }
        //public string ProductionMode { get; set; }
        public string Name { get; set; }
        public int SourcePathName { get; set; }
        public int TargetPathName { get; set; }
        /// <summary>
        /// 限制robot手臂 （上手臂只能取、下手臂只能放） 不再使用
        /// </summary>
        public bool RobotFixed { get; set; }
        /// <summary>
        /// 1下手臂 0上手臂  不再使用
        /// </summary>
        public RobotHand RobotArm { get; set; }
        public string RuleID { get; set; }
        public int OutPriority { get; set; }
        public int InPriority { get; set; }
        public bool PortGetCheckReceive { get; set; }
        public string ModePath { get; set; }
    }
}

