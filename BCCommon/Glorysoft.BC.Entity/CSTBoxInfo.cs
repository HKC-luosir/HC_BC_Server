using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class CSTBoxInfo
    {
        public CSTBoxInfo()
        {
            PanelList = new List<SPanelInfo>();
        }
        public string CSTBoxID { get; set; }
        public List<SPanelInfo> PanelList { get; set; }
    }
}
