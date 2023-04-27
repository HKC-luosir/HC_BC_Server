using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_ecid
    {
        public int ecid { get; set; }
        public string ecname { get; set; }
        public double ecmax { get; set; }
        public double ecmin { get; set; }
        public double ecdef { get; set; }
        public double ecv { get; set; }
        public DateTime updatetime { get; set; }
    }
}
