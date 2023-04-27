using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class DVInfo
    {
        public DVInfo()
        {
            DVList = new List<Parameter>();
        }
        public string DVName { get; set; }
        public IList<Parameter> DVList { get; set; }
    }
}
