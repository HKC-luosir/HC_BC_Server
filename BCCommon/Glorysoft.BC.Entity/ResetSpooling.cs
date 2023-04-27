using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class ResetSpooling
    {
        public ResetSpooling()
        {
            RSPACK = "";
        }
        public string RSPACK { get; set; }
        public List<StreamsList> StreamList { get; set; }
    }
    public class StreamsList
    {
        public string STRID { get; set; }
        public string STRACK { get; set; }
        public List<string> FCNID { get; set; }


    }
}
