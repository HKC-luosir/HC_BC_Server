using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
   public class GlassExistencePosition
    {
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public int Position { get; set; }
        public string PositionName { get; set; }
        public int CassetteSequenceNo { get; set; }
        /// <summary>
        /// （SLOTPOSITION*1000+POSITION）
        /// </summary>
        public int SlotSequenceNo { get; set; }
        public bool Exist { get; set; }
    }
}
