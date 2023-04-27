using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [XmlRoot("RETURN")]
    public class RVReturnSPC
    {
        public RVReturnSPC()
        {
            RETURNCODE = "0";
            RETURNMESSAGE = "";
        }
        [XmlElement("RETURNCODE")]
        public string RETURNCODE;
        [XmlElement("RETURNMESSAGE")]
        public string RETURNMESSAGE;
    }
}
