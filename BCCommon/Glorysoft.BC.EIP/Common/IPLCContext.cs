using Glorysoft.EIPDriver;
using System;
using System.Collections.Generic;

namespace Glorysoft.BC.EIP.Common
{
    public interface IPLCContext
    {
        //string LastDateTime { get; set; }//压力测试用 计算每天消息量
        //IPLCEventDispather dispather { get; set; }//压力测试用 计算每天消息量
        string Name { get; }

        void SendCommand(PLCMessage msg);

        void SendCommand(Block msg);

        void WriteToPLC(Block block);

        #region 需求5 1.增加EIP Terminate机制 liuyusen 20221012
        void Terminate();
        #endregion

        void ReadFromPLC(Block block, int interval);
        /// <summary>
        /// itemgroup的修改
        /// matti 20220526
        /// </summary>
        void UpdateItemGroupConfig(String Name, List<Block> itemgroups);

        bool IsConnect { get; set; }

        SocketInfo socketInfo { get; }
    }
}