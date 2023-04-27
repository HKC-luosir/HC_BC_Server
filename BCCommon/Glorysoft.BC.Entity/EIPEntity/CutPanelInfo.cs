using System.Collections.Generic;

namespace Glorysoft.BC.Entity
{
    public class CutPanelList
    {
        public CutPanelList()
        {
            PanelInfos = new List<CutPanelInfo>();
        }
        public int ScriberCount { get; set; }
        public string JobID { get; set; }
        public string LotSequenceNumber { get; set; }
        public string SlotSequenceNumber { get; set; }
        public List<CutPanelInfo> PanelInfos { get; set; }
    }
    public class CutPanelInfo
    {
        public string CutJobID { get; set; }
        public string CutPanelID { get; set; }
        public string QPanelCode { get; set; }
        public string ScriberModuleType { get; set; }
        public string CutPanelLotSequenceNumber { get; set; }
        public string CutPanelSlotSequenceNumber { get; set; }
    }
}
