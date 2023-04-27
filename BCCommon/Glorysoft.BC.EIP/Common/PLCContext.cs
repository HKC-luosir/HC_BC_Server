using Glorysoft.EIPDriver;
using Glorysoft.BC.Entity;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Threading;
using System.Collections.Concurrent;
using System.Linq;

namespace Glorysoft.BC.EIP.Common
{
    public class PLCContext : IPLCContext
    {
        private PLCProxy proxy;
        private readonly log4net.ILog logger = Entity.LogHelper.EIPLog;
        private readonly IPLCEventDispather dispather;
        //public IPLCEventDispather dispather { get; set; }//压力测试用 计算每天消息量
        //public string LastDateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");//压力测试用 计算每天消息量
        //private readonly ConcurrentQueue<PLCEventArgs> eventsRequest1Queue = new ConcurrentQueue<PLCEventArgs>();
        //private readonly ConcurrentQueue<PLCEventArgs> eventsRequest2Queue = new ConcurrentQueue<PLCEventArgs>();

        //private readonly ConcurrentQueue<PLCEventArgs> eventsReport1Queue = new ConcurrentQueue<PLCEventArgs>();
        //private readonly ConcurrentQueue<PLCEventArgs> eventsReport2Queue = new ConcurrentQueue<PLCEventArgs>();
        public SocketInfo socketInfo { get; private set; }
        public string Name { get; private set; }
        public void SendCommand(PLCMessage msg)
        {
            proxy.SendCommand(msg);
        }
        private void Run()
        {
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (eventsRequest1Queue.IsEmpty)
            //        {
            //            Thread.Sleep(5);
            //            continue;
            //        }
            //        PLCEventArgs args = null;
            //        eventsRequest1Queue.TryDequeue(out args);
            //        if (args != null)
            //        {
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] Start,RequestQueue1Count:{2}", args.Message.EQPName, args.Message.EventName, eventsRequest1Queue.Count);
            //            dispather.Dispath(args);
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] End", args.Message.EQPName, args.Message.EventName);
            //        }
            //        Thread.Sleep(1);
            //    }
            //}, TaskCreationOptions.LongRunning);
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (eventsRequest2Queue.IsEmpty)
            //        {
            //            Thread.Sleep(5);
            //            continue;
            //        }
            //        PLCEventArgs args = null;
            //        eventsRequest2Queue.TryDequeue(out args);
            //        if (args != null)
            //        {
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] Start,RequestQueue2Count:{2}", args.Message.EQPName, args.Message.EventName, eventsRequest2Queue.Count);
            //            dispather.Dispath(args);
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] End", args.Message.EQPName, args.Message.EventName);
            //        }
            //        Thread.Sleep(1);
            //    }
            //}, TaskCreationOptions.LongRunning);
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (eventsReport1Queue.IsEmpty)
            //        {
            //            Thread.Sleep(5);
            //            continue;
            //        }
            //        PLCEventArgs args = null;
            //        eventsReport1Queue.TryDequeue(out args);
            //        if (args != null)
            //        {
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] Start,ReportQueue1Count:{2}", args.Message.EQPName, args.Message.EventName, eventsReport1Queue.Count);
            //            dispather.Dispath(args);
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] End", args.Message.EQPName, args.Message.EventName);
            //        }
            //        Thread.Sleep(1);
            //    }
            //}, TaskCreationOptions.LongRunning);
            //Task.Factory.StartNew(() =>
            //{
            //    while (true)
            //    {
            //        if (eventsReport2Queue.IsEmpty)
            //        {
            //            Thread.Sleep(5);
            //            continue;
            //        }
            //        PLCEventArgs args = null;
            //        eventsReport2Queue.TryDequeue(out args);
            //        if (args != null)
            //        {
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] Start,ReportQueue2Count:{2}", args.Message.EQPName, args.Message.EventName, eventsReport2Queue.Count);
            //            dispather.Dispath(args);
            //            Entity.LogHelper.EIPLog.DebugFormat("Dispath EQPName:[{0}] Message:[{1}] End", args.Message.EQPName, args.Message.EventName);
            //        }
            //        Thread.Sleep(1);
            //    }
            //}, TaskCreationOptions.LongRunning);
        }
        //private ConcurrentQueue<PLCEventArgs> GetMinRequestQueue()
        //{
        //    var eventTempQueue = eventsRequest1Queue;
        //    if (eventTempQueue.Count > eventsRequest2Queue.Count)
        //        eventTempQueue = eventsRequest2Queue;
        //    return eventTempQueue;
        //}
        //private ConcurrentQueue<PLCEventArgs> GetMinReportQueue()
        //{
        //    var eventTempQueue = eventsReport1Queue;
        //    if (eventTempQueue.Count > eventsReport2Queue.Count)
        //        eventTempQueue = eventsReport2Queue;
        //    return eventTempQueue;
        //}
        public void SendCommand(Block msg)
        {
            proxy.SendCommand(msg);
        }

        #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
        public void Terminate()
        {
            proxy.Terminate();
        }
        #endregion

        public void WriteToPLC(Block block)
        {
            proxy.WriteToPLC(block, 0);
        }

        public void ReadFromPLC(Block block, int interval)
        {
            proxy.ReadFromPLC(block, interval);
        }

        public bool IsConnect { get; set; }

        public PLCContext()
        {
            Name = "";
            dispather = new PLCEventDispather(this);
            Run();
            EventListT = new List<string>()
            {
                "UTDataReport",
                "JobEachPosition",
                "JobCount",

            };
        }

        public void Connect(SocketInfo Info)
        {
            try
            {
                this.socketInfo = Info;
                Name = Info.DriverName;
                proxy = new PLCProxy(Info.DriverName);
                proxy.OnConncted += proxy_OnConncted;
                proxy.OnDisconnected += proxy_OnDisconnected;
                proxy.OnBitChanged += proxy_OnBitChanged;
                proxy.OnReceivedEvent += proxy_OnReceivedEvent;
                proxy.Connect(Info);
                foreach (var item in proxy.BlockMapCollection)
                {
                    var dicBlock = new Dictionary<string, Block>();
                    foreach (var block in item.Value)
                    {

                        //foreach (var block1 in block.BlockCollection)
                        //{
                        try
                        {
                            //if (block1.Key.Contains("EquipmentEvent") || block1.Key.Contains("LCCommand")) continue;
                            dicBlock.Add(block.Name, block);

                            #region matti 添加linksignal item
                            if (block.Name.Contains("LinkSignal"))
                            {
                                if (!HostInfo.Current.OPILinkList.Any(c => c.LinkSignalName == block.Name))
                                {
                                    OPILink linkdata = new OPILink();
                                    linkdata.LinkSignalName = block.Name;

                                    var UpstreamLinkSignalData = block.BlockCollection.FirstOrDefault(c => c.Key == "UpstreamLinkSignal");
                                    var DownstreamLinkSignalData = block.BlockCollection.FirstOrDefault(c => c.Key == "DownstreamLinkSignal");
                                    foreach (var linksignalitem in HostInfo.Current.LinkSignal.LinkSignalMappingItemList.mappingItems)
                                    {
                                        if (linksignalitem.name == "UpstreamLinkSignal")
                                        {
                                            for (int i = 0; i < linksignalitem.LinkSignalMappingValueList.Count; i++)
                                            {
                                                if (UpstreamLinkSignalData.Value.ItemCollection.Any(c => c.Value.Name == linksignalitem.LinkSignalMappingValueList[i].Name))
                                                {
                                                    var uplinkdata = UpstreamLinkSignalData.Value.ItemCollection.FirstOrDefault(c => c.Value.Name == linksignalitem.LinkSignalMappingValueList[i].Name);
                                                    linkdata.UpstreamLinkData.Add(new OPILinkItem() { ItemName = uplinkdata.Value.Name, ItemValue = uplinkdata.Value.Value == "0" ? false : true });
                                                }
                                            }
                                        }
                                        else if (linksignalitem.name == "DownstreamLinkSignal")
                                        {
                                            for (int i = 0; i < linksignalitem.LinkSignalMappingValueList.Count; i++)
                                            {
                                                if (DownstreamLinkSignalData.Value.ItemCollection.Any(c => c.Value.Name == linksignalitem.LinkSignalMappingValueList[i].Name))
                                                {
                                                    var downlinkdata = DownstreamLinkSignalData.Value.ItemCollection.FirstOrDefault(c => c.Value.Name == linksignalitem.LinkSignalMappingValueList[i].Name);
                                                    linkdata.DownstreamLinkData.Add(new OPILinkItem() { ItemName = downlinkdata.Value.Name, ItemValue = downlinkdata.Value.Value == "0" ? false : true });
                                                }
                                            }
                                        }
                                    }
                                    HostInfo.Current.OPILinkList.Add(linkdata);
                                }
                            }
                            #endregion
                        }
                        catch (System.Exception ex)
                        {
                            Entity.LogHelper.EIPLog.Info(block + ex.Message);
                        }
                        //}
                    }
                    PLCContexts.Current.PLCBlocks.Add(item.Key, dicBlock);
                }
                logger.Info(string.Format("START To Connect PLC for equipment {0}", Info.DriverName));

            }
            catch (System.Exception ex)
            {
                Entity.LogHelper.EIPLog.Error("Connect to EIP Fail:" + ex.ToString());
            }

        }
        public List<string> EventListT;
        #region Event Handler
        void proxy_OnReceivedEvent(object sender, PLCMessage msg)
        {
            Entity.LogHelper.EIPLog.DebugFormat("{2}   Receive Start EQPName:[{0}]     Message[{1}]", msg.EQPName, msg.EventName, msg.TransactionID);
            var args = new PLCEventArgs
            {
                EventType = IndexerEventType.RCVDEvent,
                Message = msg,
                Context = this,
                Name = msg.EQPName
            };
            dispather.Dispath(args);
            //if (msg.EventName.Contains("Request"))
            //{
            //    var queue = GetMinRequestQueue();
            //    queue.Enqueue(args);
            //}
            //else
            //{
            //    var queue = GetMinReportQueue();
            //    queue.Enqueue(args);
            //}
            Entity.LogHelper.EIPLog.DebugFormat("{2}   Receive End EQPName:[{0}]     Message[{1}]", msg.EQPName, msg.EventName, msg.TransactionID);
        }

        void proxy_OnBitChanged(object sender, Block block, Item item, bool isBitOn)
        {
            var args = new PLCEventArgs
            {
                EventType = IndexerEventType.BitOnOff,
                BitBlock = block,
                BitItem = item,
                BitValue = isBitOn,
                Context = this,
                Name = Name
            };
            dispather.Dispath(args);
        }

        void proxy_OnDisconnected(object sender, string errmsg)
        {
            IsConnect = false;
            var args = new PLCEventArgs
            {
                EventType = IndexerEventType.Disconnect,
                Context = this,
                Name = Name
            };
            dispather.Dispath(args);
        }

        void proxy_OnConncted(object sender, string socketInfo)
        {
            IsConnect = true;
            //var oEqp = ReadEQPStatus();
            // IsConnect = false;
            var args = new PLCEventArgs
            {
                EventType = IndexerEventType.Connect,
                Context = this,
                Name = Name
            };
            dispather.Dispath(args);
        }
        #endregion

        /// <summary>
        /// itemgroup的修改
        /// matti 20220526
        /// </summary>
        public void UpdateItemGroupConfig(String Name, List<Block> itemgroups)
        {
            proxy.UpdateItemGroupConfig(Name, itemgroups); 
        }
    }
}
