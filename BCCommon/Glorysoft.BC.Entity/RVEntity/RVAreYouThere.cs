using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVAreYouThere : RVBodyBase
    {
        public RVAreYouThere()
        {
            MessageName = "MES.AREYOUTHERE";
        }
        public string EQUIPMENTID { get; set; }
    }
}
