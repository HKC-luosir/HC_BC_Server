﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Glorysoft.BC.Entity.RVEntity
{
    [Serializable]
    [XmlRoot("Body")]
    public class RVLotInfoRequest : RVBodyBase
    {
        public RVLotInfoRequest()
        {
            MessageName = "LCM.LOTINFOREQUEST";
        }
        public string EQUIPMENTID { get; set; }
        public string PORTID { get; set; }
        public string PORTTYPE { get; set; }
        public string DURABLEID { get; set; }
    }
}