using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Xml;
using Glorysoft.EIPDriver;
using System.Threading;
using Glorysoft.BC.EIP.Common;
using Glorysoft.BC.Entity;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Glorysoft.BC.EIP
{
    public class PLCContexts
    {
        public Dictionary<string, IPLCContext> plcContexts = new Dictionary<string, IPLCContext>();
        private readonly Dictionary<string, Dictionary<string, Block>> plcBlocks = new Dictionary<string, Dictionary<string, Block>>();
        //private string mplc;
        //private PLCContext proxy = null;
        private PLCContexts()
        {

        }

        private static readonly Lazy<PLCContexts> Lazy = new Lazy<PLCContexts>(() => new PLCContexts());

        public static PLCContexts Current
        {
            get
            {
                return Lazy.Value;
            }
        }
        /// <summary>
        /// PLC Instance Initialize
        /// </summary>
        public void InitializeContexts()
        {
            try
            {
                //var sPath = AppDomain.CurrentDomain.BaseDirectory;
                //var configPath = sPath + ConfigurationManager.AppSettings["ConnectConfigure"];
                //var xmlDoc = new XmlDocument();
                //xmlDoc.Load(configPath);


                //var root = xmlDoc.SelectSingleNode("configuration/PLCConfig");
                //string PLCName = root.SelectSingleNode("PLCName").InnerText;
                //string LogRootDir = root.SelectSingleNode("LogRootDir").InnerText;
                //string PLCMapFile = root.SelectSingleNode("PLCMapFile").InnerText;
                //string WorkflowFile = root.SelectSingleNode("WorkflowFile").InnerText;


                //var configure = PLCConfig.Instance;
                //proxy = new PLCContext();
                //SocketInfo sInfo = new SocketInfo();
                //sInfo.DriverName = PLCName;
                //sInfo.LogRootDir = LogRootDir;
                //sInfo.PlcMapFile = PLCMapFile;
                //sInfo.WorkflowFile = WorkflowFile;
                //proxy.Connect(sInfo);
                var configPath = "Configuration\\EIPConnection.xml";
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(configPath);
                var configs = XmlSerialization.XmlDeserialize<EIPConnections>(xmlDoc.InnerXml);
                List<String> eQPNameList = new List<string>();
                foreach (var line in HostInfo.Current.AllEQPInfo)
                {
                    foreach (var eqp in line.Units)
                    {
                        eQPNameList.Add(eqp.UnitName);
                    }
                }
                var sb = new StringBuilder();
                sb.AppendLine("<LIBRARY>");
                sb.AppendLine("<COMMENT>");
                sb.AppendLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));//"2019/3/3 14:55:23"
                sb.AppendLine("</COMMENT>");
                sb.AppendLine("<PLCTYPE>");
                sb.AppendLine("</PLCTYPE>");
                sb.AppendLine("<SYMBOL>");
                foreach (var eqpName in eQPNameList)
                {
                    var eip = configs.EIPConnectionList.FirstOrDefault(f => f.Name == eqpName);
                    if (eip == null)
                        continue;

                    var proxy = new PLCContext();
                    SocketInfo sInfo = new SocketInfo();
                    sInfo.DeviceType = (DeviceType)Enum.Parse(typeof(DeviceType), eip.DeviceType);
                    sInfo.DriverName = eip.Name;
                    sInfo.EIPType = (EIPType)Enum.Parse(typeof(EIPType), eip.EIPType);
                    sInfo.IpAddress = eip.RemoteIPAddress;
                    sInfo.Port = eip.RemoteIPPort;
                    sInfo.Protocol = eip.Protocol;
                    sInfo.ConnectMode = (PLCConst.CONNECT_MODE)Enum.Parse(typeof(PLCConst.CONNECT_MODE), eip.ConnectMode);
                    sInfo.Timeout = int.Parse(eip.Timeout);
                    sInfo.ConnectInterval = int.Parse(eip.ConnectInterval);
                    sInfo.LogRootDir = eip.LogPath;
                    sInfo.LogLevel = eip.LogLevel;
                    sInfo.PlcMapFile = eip.MapFile;
                    sInfo.WorkflowFile = eip.WorkflowFile;
                    #region 需求7 2.EIP Driver增加LogTitleName,变更日志文件夹名称 liuyusen 20221017
                    sInfo.LogTitleName = eip.LogTitleName;
                    #endregion
                    plcContexts.Add(eip.Name, proxy);
                    proxy.Connect(sInfo);

                    //Task.Factory.StartNew(() =>
                    //Sumcount(proxy),  TaskCreationOptions.LongRunning);

                    //Dictionary<string, List<Block>> lst = null;
                    //if (configure.BlockMaps.ContainsKey(eqpName.ToString()))
                    //    lst = configure.BlockMaps[eqpName.ToString()];
                    //if (lst == null) continue;
                    //else
                    //    //logger.Info(eqpName + "   Init OK!");
                    //    foreach (var kv in lst)
                    //    {
                    //        var eqpname = kv.Key;

                    //        if (!plcBlocks.ContainsKey(eqpname))
                    //        {
                    //            var dict = new Dictionary<string, Block>();
                    //            plcBlocks.Add(eqpname, dict);
                    //            foreach (var block in kv.Value)
                    //            {
                    //                if (!dict.ContainsKey(block.Name))
                    //                {
                    //                    dict.Add(block.Name, block);
                    //                }
                    //                else
                    //                {
                    //                    var msg = string.Format("Duplicate Block Name for Equipment {0} and Block {1}", eqpname, block.Name);
                    //                    throw new Exception(msg);
                    //                }
                    //            }
                    //            if (eqpname != "LZB")
                    //            {
                    //                if (plcBlocks[eqpname].Values.ElementAt(0).BlockCollection["EquipmentEvent"].ItemCollection.ContainsKey("EQPAlive"))
                    //                {
                    //                    plcBlocks[eqpname].Values.ElementAt(0).BlockCollection["EquipmentEvent"].ItemCollection["EQPAlive"].IsTimerReadWrite = true;
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var msg = string.Format("Duplicate Equipment Name for Equipment {0}", eqpname);
                    //            throw new Exception(msg);
                    //        }
                    //    }



                }
                foreach (var map in PLCConfig.Instance.BlockMaps.Values)
                {
                    foreach (var m in map.Values)
                    {
                        foreach (var b in m)
                        {
                            var name = b.Name;
                            var point = b.Points;
                            sb.AppendLine(string.Format("{0}	WORD[{1}]				0	{2}", name, point, b.Action == "READ" ? "IN" : "OUT"));
                        }
                    }
                }
                sb.AppendLine("<SYMBOL>");
                sb.AppendLine("</LIBRARY>");
                var s = sb.ToString();
                using (StreamWriter sw = new StreamWriter("EAS_Tags.cxr"))
                {
                    sw.Write(s);
                }
            }
            catch (Exception ex)
            {
                Entity.LogHelper.EIPLog.Error(ex.ToString());
            }
        }
        ////压力测试用 计算每天消息量
        //public void Sumcount(PLCContext proxy)
        //{
        //    while (true)
        //    {
        //        DateTime dtNow = DateTime.Now;
        //        if (dtNow.ToString("HH") == "00" && proxy.LastDateTime != dtNow.ToString("yyyy-MM-dd"))
        //        {
        //            String res = dtNow.ToString("yyyy-MM-dd") + "," + proxy.Name + "," + proxy.dispather.msgcount;
        //            Entity.LogHelper.EIPLog.Info(res);
        //            proxy.dispather.msgcount = 0;
        //            proxy.LastDateTime = dtNow.ToString("yyyy-MM-dd");
        //        }
        //        Thread.Sleep(1000 * 60 * 60 * 1);
        //    }
        //}
        public void WriteToPLC(string eqpName, Block block)
        {
            try
            {
                var ctx = plcContexts[eqpName];
                ctx.WriteToPLC(block);
            }
            catch (Exception e)
            {
                // logger.Error(e.Message, e);
            }
        }
        public void SendCommand(Block msg)
        {
            var ctx = plcContexts[msg.EQPName];
            ctx.SendCommand(msg);
        }
        public void Send2PLC(PLCMessage msg)
        {
            try
            {
                var ctx = plcContexts[msg.EQPName];
                ctx.SendCommand(msg);
            }
            catch (Exception e)
            {
                Entity.LogHelper.EIPLog.Error(e.Message, e);
            }
        }

        #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
        public void Terminate()
        {
            try
            {
                if (plcContexts.Count > 0)
                {
                    foreach (var ctx in plcContexts)
                    {
                        ctx.Value.Terminate();
                    }
                }
            }
            catch (Exception e)
            {
                Entity.LogHelper.EIPLog.Error(e.Message, e);
            }
        }
        #endregion
        public void ReadFromPLC(Block msg)
        {
            try
            {
                var ctx = plcContexts[msg.EQPName];
                ctx.ReadFromPLC(msg, 500);
            }
            catch (Exception e)
            {
                Entity.LogHelper.EIPLog.Error(e.Message, e);
            }
        }
        public Dictionary<string, Dictionary<string, Block>> PLCBlocks
        {
            get
            {
                return plcBlocks;
            }
        }

        public Dictionary<string, Block> GetBlockMap(string plcName)
        {
            if (plcBlocks.ContainsKey(plcName))
            {
                var ctx = plcBlocks[plcName];
                return ctx;
            }
            return null;
        }

        public Block GetBlock(string plcName, string blockName)
        {
            if (plcBlocks.ContainsKey(plcName))
            {
                var ctx = plcBlocks[plcName];
                if (ctx.ContainsKey(blockName))
                    return ctx[blockName];
            }
            return null;
        }
        public Block GetBlockByBIName(string plcName, string blockName, string itemName,out Block block)
        {
            if (plcBlocks.ContainsKey(plcName))
            {
                var ctx = plcBlocks[plcName];
                foreach (var tag in ctx.Values)
                {
                    foreach (var b in tag.BlockCollection)
                    {
                        if (b.Key == blockName)
                        {
                            foreach (var i in b.Value.ItemCollection)
                            {
                                if (i.Key == itemName)
                                {
                                    block = b.Value;
                                    return tag;
                                }
                            }
                        }
                    }
                }
                //var block = ctx.Where(o => o.Value.BlockCollection.ContainsKey(blockName));
                //if (block != null)
                //    block = block.Where(o => o.Value.ItemCollection.ContainsKey(itemName));
                
                //if (ctx.ContainsKey(blockName))
                //    return ctx[blockName];
            }
            block = new Block();
            return null;
        }
        public IPLCContext GetContext(string plcName)
        {
            string temp = plcName.Replace(" ", "").Replace("-", "");
            if (plcContexts.ContainsKey(temp))
            {
                return plcContexts[temp];
            }
            return null;
        }

        public IList<IPLCContext> Contexts
        {
            get
            {
                return plcContexts.Values.ToList();
            }
        }

        /// <summary>
        /// itemgroup的修改
        /// matti 20220526
        /// </summary>
        public void UpdateItemGroupConfig(String connName, List<Block> itemgroups)
        {
            try
            {
                var ctx = plcContexts[connName];
                ctx.UpdateItemGroupConfig(connName, itemgroups);
            }
            catch (Exception e)
            {
                Entity.LogHelper.EIPLog.Error(e.Message, e);
            }
        }
    }
}
