using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVMessage
{
    [Serializable]
    [XmlRoot("Return")]
    public class Return
    {
        public Return()
        {
            RETURNCODE = "0";
            RETURNMESSAGE = "Success";
        }
        public string RETURNCODE { get; set; }
        public string RETURNMESSAGE { get; set; }
    }
}
