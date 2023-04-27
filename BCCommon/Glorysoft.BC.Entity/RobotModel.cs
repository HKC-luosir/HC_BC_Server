using log4net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class RobotModel
    {
        public RobotModel()
        {
            LinksignalList = new List<Linksignal>();
        }
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public int ModelID { get; set; }
        public int ModelPosition { get; set; }
        public string UPLinkName { get; set; }
        public string DownLinkName { get; set; }
        public List<Linksignal> LinksignalList { get; set; }
        public string PortID { get; set; }
        public string SentOutName { get; set; }
        public string TransInName { get; set; }
        public string UnitNo { get; set; }

        /// <summary>
        /// 从EQP取片 对应的层数
        /// 对应GLASSA GLASSB
        /// </summary>
        public int EQPSendSlotNoA { get; set; } = 0;
        /// <summary>
        /// 从EQP取片 对应的层数
        /// 对应GLASSC GLASSD
        /// </summary>
        public int EQPSendSlotNoB { get; set; } = 0;
        public bool SendPositionFront1 { get; set; } = false;
        public bool SendPositionFront2 { get; set; } = false;
        public bool SendPositionBack1 { get; set; } = false;
        public bool SendPositionBack2 { get; set; } = false;

        /// <summary>
        /// 给EQP送片 对应的层数
        /// 对应GLASSA GLASSB
        /// </summary>
        public int EQPReciveSlotNoA { get; set; } = 0;
        /// <summary>
        /// 给EQP送片 对应的层数
        /// 对应GLASSC GLASSD
        /// </summary>
        public int EQPReciveSlotNoB { get; set; } = 0;

        public GlassInfo GlassA { get; set; }
        public GlassInfo GlassB { get; set; }
        public GlassInfo GlassC { get; set; }
        public GlassInfo GlassD { get; set; }
        /// <summary>
        /// 1 JobDataA ;2JobDataB;3 JobDataA and JobDataB
        /// </summary>
        public int UsedJobBlockNo { get; set; } = 3;
        public RobotMotion RobotMotion { get; set; }

        /// <summary>
        ///1:层数从小到大取片
        ///2:层数从大到小取片
        /// </summary>
        public PortGetType PortGetType { get; set; } = PortGetType.ASC;
        public bool DualArm { get; set; }

        public bool ExchangeEnable { get; set; }
        public bool TransferEnable { get; set; }
        public bool GetWaitEnable { get; set; }
        public bool PutWaitEnable { get; set; }
        public string ModelName { get; set; }
        public bool GetEnable { get; set; }

        public string GroupName { get; set; }

        public int INPriority { get; set; }
        public int OutPriority { get; set; }
        public int TransferPriority { get; set; }
        public int ExchangePriority { get; set; }

        /// <summary>
        /// 0 / 99 不限制手臂
        /// 1 下手臂
        /// 2 上手臂
        /// </summary>
        public int GetArm { get; set; }
        /// <summary>
        /// 0 不限制手臂
        /// 1 下手臂
        /// 2 上手臂
        /// </summary>
        public int PutArm { get; set; }
    }
}
