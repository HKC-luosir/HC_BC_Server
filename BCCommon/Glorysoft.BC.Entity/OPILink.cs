using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Glorysoft.BC.Entity
{
    public class OPILink
    {
        public OPILink()
        {
            DownstreamLinkData = new List<OPILinkItem>();
            UpstreamLinkData = new List<OPILinkItem>();
        }
        public string LinkSignalName { get; set; }
        public List<OPILinkItem> DownstreamLinkData { get; set; }
        public List<OPILinkItem> UpstreamLinkData { get; set; }
    }

    public class OPILinkItem
    {
        public string ItemName { get; set; }
        public bool ItemValue { get; set; }
    }
}
