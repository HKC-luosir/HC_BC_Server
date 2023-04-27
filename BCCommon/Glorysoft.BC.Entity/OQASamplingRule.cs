using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class OQASamplingRule
    {
        public string LineID { get; set; }
        public string RevisionCode { get; set; }
        public int SamplingRule { get; set; }
        public int CurrentCount { get; set; }
    }
}
