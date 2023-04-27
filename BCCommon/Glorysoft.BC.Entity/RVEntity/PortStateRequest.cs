
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PortStateRequest
    {
        public PortStateRequest()
        {
           MACHINENAME = "";
        }
        public string MACHINENAME { get; set; }
      
    }
 
}
