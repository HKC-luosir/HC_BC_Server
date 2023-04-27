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
    public class RVLotProcessAbort : RVBodyBase
    {
        public RVLotProcessAbort()
        {
            MessageName = "LCM.LOTPROCESSABORT";
        }
        public string ACTIONTYPE { get; set; }
        public string EQUIPMENTID { get; set; }
        public string MAINQTY { get; set; }
        public string PORTID { get; set; }
        public string LOTID { get; set; }
        public string DURABLEID { get; set; }
        public string REASONCODE { get; set; }
        [XmlArray("PANELLIST")]
        [XmlArrayItem("PANEL")]
        public List<PANEL> PANELLIST = new List<PANEL>();
    }
}
