using System.Collections.Generic;

namespace Glorysoft.BC.Entity.SECSEntity
{
    public class SelectSVIDRequest : COMMON_Send
    {
        public List<parameter> SVIDList { get; set; }
    }
}
