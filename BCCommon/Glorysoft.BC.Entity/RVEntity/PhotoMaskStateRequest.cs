
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PhotoMaskStateRequest
    {
        public PhotoMaskStateRequest()
        {
           
        }
        public string MACHINENAME { get; set; }
      

    }
 
}
