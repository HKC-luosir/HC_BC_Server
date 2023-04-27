using Glorysoft.BC.Entity.RVEntity;
using System;

namespace Glorysoft.BC.Entity
{
    public class BoxCache : DURABLE
    {
        public DateTime dtNow { get; set; } = DateTime.Now;
    }
}
