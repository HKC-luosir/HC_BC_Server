using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class CurrentSVData
    {
        public CurrentSVData()
        {
            ParameterList = new List<Parameter>();
        }
        public string DateTime { get; set; }
        public List<Parameter> ParameterList { get; set; }
        public string UnitID { get; set; }

    }
}
