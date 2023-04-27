
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class SVData
    { 
        public SVData()
        {

        }
        public string ID { get; set; }
        public string EQPID { get; set; }
        public string UNITID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Index { get; set; }
        public string SVName { get; set; }
        public string SVValue { get; set; }
        public bool OperationEnable { get; set; }
        public string OperationSymbol { get; set; }
        public float OperationProportion { get; set; }
        /// <summary>
        /// group name
        /// </summary>
        public string ItemName { get; set; }
        public bool IsFloat { get; set; }
    }
}
