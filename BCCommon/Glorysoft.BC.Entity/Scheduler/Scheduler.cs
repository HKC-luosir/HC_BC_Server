using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    /// <summary>
    /// 计划调度器
    /// </summary>
    public static class Scheduler
    {
        /// <summary>
        /// 计划项目集合
        /// </summary>
        private static readonly List<ScheduleItem> ScheduleItems;

        /// <summary>
        /// 计划调度器线程
        /// </summary>
        private static readonly Task SchedulerTask;

        /// <summary>
        /// 线程安全锁
        /// </summary>
        private static readonly object syncRoot;

        /// <summary>
        /// 计划项目数量
        /// </summary>
        public static int Count => ScheduleItems.Count;

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static Scheduler()
        {
            syncRoot = new object();
            ScheduleItems = new List<ScheduleItem>();
            SchedulerTask = new Task(() =>
            {
                while (true)
                {
                    lock (syncRoot)
                    {
                        ScheduleItems.RemoveAll(item => item.Expired);

                        //FlowLogHelper.HostLog.Debug(string.Format("========================================Scheduler Begin======================================="));
                        foreach (var item in ScheduleItems)
                        {
                            item.Tick++;
                            //if (item.Expired || item.Tick == 0)
                            //{
                            //    continue;
                            //}
                            if (item.Tick % item.Interval == 0)
                            {
                                Task.Factory.StartNew(item.Handler, item.State);
                                if (item.Options == ScheduleOptions.Once)
                                {
                                    item.Expired = true;
                                }
                                item.Tick = 0;//执行后重新计时
                            }
                            //FlowLogHelper.HostLog.Debug(string.Format("[Scheduler]Scheduler:{0}", item.Name));
                        }
                        //FlowLogHelper.HostLog.Debug(string.Format("=========================================Scheduler End========================================"));
                    }
                    Thread.Sleep(1000);
                }
            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// 初始化计划调度器
        /// </summary>
        public static void Initialize()
        {
            if (ScheduleItems.Count == 0)
                return;

            if (SchedulerTask.Status != TaskStatus.Running)
            {
                SchedulerTask.Start();
            }
        }

        /// <summary>
        /// 添加计划项目
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="interval">间隔（秒）</param>
        /// <param name="options">执行方式</param>
        /// <param name="handler">处理方法</param>
        /// <param name="state">处理方法所需要的数据</param>
        /// <returns></returns>
        public static void Add(string name, int interval, ScheduleOptions options, Action<object> handler, object state)
        {
            lock (syncRoot)
            {
                ScheduleItems.Add(new ScheduleItem(name, interval, options, handler, state));
                //FlowLogHelper.HostLog.Debug(string.Format("[AddScheduler] Add {0}", name));
            }
        }
    }
}
