﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class IndexerOperationModeChangeReport : BaseClass
    {
        public IndexerOperationModeChangeReport() { }
        public string UnitID { get; set; }
        public string IndexerOperationMode { get; set; }
    }
}
