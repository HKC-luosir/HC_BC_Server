using System;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class cfg_eqpprofile
    {
        public int id { get; set; }
        public string profilename { get; set; }
        public string profileversion { get; set; }
        /// <summary>
        /// 0-δ��Ч 1-����Ч
        /// </summary>
        public int isenable { get; set; } = 0;
        public DateTime updatedate { get; set; }
        public string eqpid { get; set; }
    }
}
