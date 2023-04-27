using System;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_eqpprofile_item
    {
        public int id { get; set; }
        public int itemgroupid { get; set; }
        public string itemname { get; set; }
        public string itemoffset { get; set; }
        public string itempoints { get; set; }
        public string itemtype { get; set; }
        public int itemorder { get; set; }
        public DateTime updatedate { get; set; }
        public string mesitemname { get; set; }
        public float operationproportion { get; set; } = 1;
        public string dataindex { get; set; }
    }
}
