using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_port
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public string unitname { get; set; }
        public int portno { get; set; }
        public string portid { get; set; }
        public int portstatus { get; set; }
        public int portmode { get; set; }
        public int portcsttype { get; set; }
        public int portoperationmode { get; set; }
        public int porttypeautochangemode { get; set; }
        public int capacity { get; set; }
        public int portqtime { get; set; }
        public int glasstype { get; set; }
        public string portgrade { get; set; }
        public int portenablemode { get; set; }
        public int userms { get; set; }
        public DateTime updatedate { get; set; }
        public DateTime startcreatedate { get; set; }
        public DateTime endcreatedate { get; set; }
        public int isingetput { get; set; }
        public string productiontype { get; set; }
        public int cassettesequenceno { get; set; }
        public string cassetteid { get; set; }
        public string porttype { get; set; }
        public string transfermode { get; set; }
        public int portpausemode { get; set; }
        public string portusetype { get; set; }
        public string functionname { get; set; }
    }
}
