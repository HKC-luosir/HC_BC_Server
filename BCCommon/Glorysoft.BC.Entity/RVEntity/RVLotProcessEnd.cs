using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLotProcessEnd : RVBodyBase
    {
        public RVLotProcessEnd()
        {
            MessageName = "LCM.LOTPROCESSEND";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
        public string DURABLEID { get; set; }
        public string MAINQTY { get; set; }
        public string OPERATOR { get; set; }
        public string ACTIONCOMMENT { get; set; }
        [XmlArray("PANELLIST")]
        [XmlArrayItem("PANEL")]
        public List<PANEL> PANELLIST = new List<PANEL>();
    }
}
