using System;
using Glorysoft.EIPDriver;

namespace Glorysoft.BC.EIP.Common
{
    public enum IndexerEventType
    {
        BitOnOff,
        Connect,
        Disconnect,
        RCVDEvent
    }

    public class PLCEventArgs : EventArgs
    {
        public IndexerEventType EventType { get; set; }

        public PLCMessage Message { get; set; }

        public Block BitBlock { get; set; }

        public Item BitItem { get; set; }

        public bool BitValue { get; set; }

        public IPLCContext Context { get; set; }

        public string Name { get; set; }
    }
}