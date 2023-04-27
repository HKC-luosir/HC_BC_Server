

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity
{
    [Serializable]
    [XmlRoot("Body")]
    public class PhotoMaskStateChanged
    {
        public PhotoMaskStateChanged()
        {
           
        }
        public string MACHINENAME { get; set; }
        public string UNITNAME { get; set; }
        public string MASKNAME { get; set; }
        public string POSITION { get; set; }
        /// <summary>
        /// [ MOUNTED | INUSE | UNMOUNTED ]
        /// </summary>
        public string TRANSFERSTATE { get; set; }
        public string USECOUNT { get; set; }
    }

}
