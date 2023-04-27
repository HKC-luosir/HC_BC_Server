using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLotProcessCancel : RVBodyBase
    {
        public RVLotProcessCancel()
        {
            MessageName = "LCM.LOTPROCESSCANCEL";
        }
        public string EQUIPMENTID { get; set; }
        public string MAINQTY { get; set; }
        public string PORTID { get; set; }
        public string LOTID { get; set; }
        public string DURABLEID { get; set; }
        public string REASONCODE { get; set; }
        public string STEPNAME { get; set; }
        public string LOGICRECIPE { get; set; }
        [XmlArray("PANELLIST")]
        [XmlArrayItem("PANEL")]
        public List<PANEL> PANELLIST = new List<PANEL>();
    }
}
