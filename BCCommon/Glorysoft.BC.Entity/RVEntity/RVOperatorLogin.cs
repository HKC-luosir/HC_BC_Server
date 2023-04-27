using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVOperatorLogin : RVBodyBase
    {
        public RVOperatorLogin()
        {
            MessageName = "HKC.OPERATORLOGINREPORT";
        }
        public string MACHINEID { get; set; }
        public string UNITID { get; set; }
        public string OPERATORID { get; set; }
        public string TOUCHPANELNUMBER { get; set; }
        public string REPORTOPTION { get; set; }
        public string TIME { get; set; }
    }
}
