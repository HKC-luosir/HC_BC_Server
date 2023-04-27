using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_cassette
    {
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public string unitname { get; set; }
        public int portno { get; set; }
        public string portid { get; set; }
        public int cassettesequenceno { get; set; }
        public int cassettestatus { get; set; }
        public string porttype { get; set; }
        public int portstatus { get; set; }
        public string cassetteid { get; set; }
        public string lotname { get; set; }
        public string processflowname { get; set; }
        public string processflowversion { get; set; }
        public string processoperationname { get; set; }
        public string processoperationversion { get; set; }
        public string portusetype { get; set; }
        public string productspecname { get; set; }
        public string productspecversion { get; set; }
        public string productiontype { get; set; }
        public int productquantity { get; set; }
        public string slotmap { get; set; }
        public string slotsel { get; set; }
        public string machinerecipename { get; set; }
        public string workorder { get; set; }
        public string lotjudge { get; set; }
        public DateTime cassetteprocessendtime { get; set; }
        public DateTime cassetteprocessstarttime { get; set; }
        public DateTime updatedate { get; set; }
        public DateTime startcreatedate { get; set; }
        public DateTime endcreatedate { get; set; }
        public int qtimeflag { get; set; }
        public string cassettecode { get; set; }
        public string jobexistenceslot { get; set; }
        public string carriertype { get; set; }
        public int compeletedcassettedata { get; set; }
        public string functionname { get; set; }
        public int id { get; set; }
    }
}
