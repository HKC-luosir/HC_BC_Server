using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    //matti 20220315
    [Serializable]
    [XmlRoot("Body")]
    public class RVPanelInfoDownloadResponse : RVBodyBase
    {
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PANELID { get; set; }
        public string PARTNAME { get; set; }
        public string WOID { get; set; }
        public string STEPNAME { get; set; }
        public string LOTTYPE { get; set; }
        public string BONDINGID { get; set; }
        public string OQAGRADE { get; set; }
        public string SAMPLINGFLAG { get; set; }
        public string GRADE { get; set; }
        public string ABNORMALCODE { get; set; }
        public string SUBGRADE { get; set; }
        public string STATE { get; set; }
        [XmlArray("UNITRECIPELIST")]
        [XmlArrayItem("UNITRECIPE")]
        public List<UNITRECIPE> UNITRECIPELIST = new List<UNITRECIPE>();
        [XmlArray("SPECIALCODELIST")]
        [XmlArrayItem("SPECIALCODE")]
        public List<RVCODE> SPECIALCODELIST { get; set; }
    }

    public class RVCODE
    {
        public string CODEID { get; set; }
    }
}
