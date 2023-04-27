using System;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class wip_processend
    {
        public int id { get; set; }
        public string equipmentid { get; set; }
        public string portid { get; set; }
        public string porttype { get; set; }
        public string partname { get; set; }
        public string stepname { get; set; }
        public string durableid { get; set; }
        public string mainqty { get; set; }
        public string operatorid { get; set; }
        public string actioncomment { get; set; }
        public string returncode { get; set; }
        public string returnmsg { get; set; }
        public DateTime updatedate { get; set; }
    }
}
