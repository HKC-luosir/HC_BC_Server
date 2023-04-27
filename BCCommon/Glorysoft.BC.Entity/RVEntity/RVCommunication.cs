using System;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVCommunication : RVBodyBase
    {
        public RVCommunication()
        {
            MessageName = "MES.COMMUNICATIONSTATEREPORT";
        }
        public string EQUIPMENTID { get; set; }
        public string COMMUNICATIONSTATE { get; set; }
    }
}
