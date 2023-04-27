using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class ConversationObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objID">PanelId, BoxId......</param>
        /// <param name="SF">Stream Function</param>
        /// <param name="sEQPID">Equip ID</param>
        public ConversationObject(string objID, string SF)
        {
            ObjectId = objID;
            StreamFunc = SF;
            DeadLine = DateTime.Now;
        }

        public string ObjectId { get; set; }

        public string StreamFunc { get; set; }

        public DateTime DeadLine { get; set; }
    }
}
