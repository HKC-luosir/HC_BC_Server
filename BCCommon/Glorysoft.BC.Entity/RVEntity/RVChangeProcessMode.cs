using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVChangeProcessMode : RVBodyBase
    {
        public RVChangeProcessMode()
        {
            MessageName = "LCM.CHANGEPROCESSMODE";
        }
        public string EQUIPMENTID { get; set; }
        public string PROCESSMODE { get; set; }
    }
}
