using System;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_pallet
    {
        public string id { get; set; }
        public string eqpid { get; set; }
        public string unitid { get; set; }
        public string palletid { get; set; }
        public string palletstatus { get; set; }
        public string pallettype { get; set; }
        public string productid { get; set; }
        public string operationid { get; set; }
        public string lottype { get; set; }
        public string grade { get; set; }
        public string boxqty { get; set; }
        public string reserveqty { get; set; }
        public DateTime createdate { get; set; }
    }
}
