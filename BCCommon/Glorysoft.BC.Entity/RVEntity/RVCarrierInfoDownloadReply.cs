using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCarrierInfoDownloadReply : RVBodyBase
    {
        public RVCarrierInfoDownloadReply()
        {
            MessageName = "";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string PARTNAME { get; set; }
        public string STEPNAME { get; set; }
        public string LOTTYPE { get; set; }
        public string LOTID { get; set; }
        public string DURABLEID { get; set; }
        public string DURABLETYPE { get; set; }
        public string RECIPENAME { get; set; }
        public string GRADE { get; set; }
        public string MAINQTY { get; set; }
        public string SLOTMAP { get; set; }
        [XmlArray("UNITRECIPELIST")]
        [XmlArrayItem("UNITRECIPE")]
        public List<UNITRECIPE> UNITRECIPELIST = new List<UNITRECIPE>();
        [XmlArray("PANELLIST")]
        [XmlArrayItem("PANEL")]
        public List<PANEL> PANELLIST = new List<PANEL>();
    }
}
