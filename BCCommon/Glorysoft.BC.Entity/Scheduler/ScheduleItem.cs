using System;

namespace Glorysoft.BC.Entity
{
    /// <summary>
    /// 计划项目
    /// </summary>
    public class ScheduleItem
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 间隔（秒）
        /// </summary>
        public long Interval { get; set; }

        /// <summary>
        /// 执行方式
        /// </summary>
        public ScheduleOptions Options { get; set; }

        /// <summary>
        /// 处理方法
        /// </summary>
        public Action<object> Handler { get; set; }

        /// <summary>
        /// 处理方法所需要的数据
        /// </summary>
        public object State { get; set; }

        /// <summary>
        /// 是否失效
        /// </summary>
        public bool Expired { get; set; }

        /// <summary>
        /// 计时
        /// </summary>
        public long Tick { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="interval">间隔（秒）</param>
        /// <param name="options">执行方式</param>
        /// <param name="handler">处理方法</param>
        /// <param name="state">处理方法所需要的数据</param>
        public ScheduleItem(string name, int interval, ScheduleOptions options, Action<object> handler, object state)
        {
            Name = name;
            Interval = interval;
            Options = options;
            Handler = handler;
            State = state;
            Tick = 0;
            Expired = false;
        }
    }
}
