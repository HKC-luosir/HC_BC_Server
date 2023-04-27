using System;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_eqpprofile_itemgroup
    {
        public int id { get; set; }
        public int profileid { get; set; }
        /// <summary>
        /// itemgroup¿‡–Õ 1-JOBDATA 2-RECIPE 3-PROCESSDATA
        /// </summary>
        public int grouptype { get; set; }
        public string unitname { get; set; }
        public string itemgroupname { get; set; }
        public int itemgrouporder { get; set; }
        public DateTime updatedate { get; set; }
    }
}
