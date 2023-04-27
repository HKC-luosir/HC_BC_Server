
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PhotoMaskStateReport
    {
        public PhotoMaskStateReport()
        {
            MaskList = new PhotoMaskStateReportMaskList();
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        //[XmlArray("MASKLIST")]
        //[XmlArrayItem("MASK")]
           [XmlElement("MASKLIST")]   
        public PhotoMaskStateReportMaskList MaskList { get; set; }

    }
  [Serializable]
    [XmlRoot("MASK")]
    public class PhotoMaskStateReportMaskList
    {
        public PhotoMaskStateReportMaskList()
        {
            MaskList = new List<PhotoMaskStateReportMask>();
        }
        [XmlElement("MASK")]
        public List<PhotoMaskStateReportMask> MaskList { get; set; }
    }
    public class PhotoMaskStateReportMask
    {
        public string ALARMCODE { get; set; }
        public string POSITION { get; set; }
        public string MASKNAME { get; set; }
        public string TRANSFERSTATE { get; set; }
        public string USECOUNT { get; set; }
    }
}
