
using System;

namespace Glorysoft.BC.Entity
{
    public class DVData
    {
        public DVData()
        {
            
        }

        public string ID { get; set; }
        public string EQPID { get; set; }
        public string UNITID { get; set; }
        public DateTime CreateDate { get; set; }
        public int Index { get; set; }
        /// <summary>
        /// SITENAME
        /// </summary>
        public string DVName { get; set; }
        public string DVValue { get; set; }
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