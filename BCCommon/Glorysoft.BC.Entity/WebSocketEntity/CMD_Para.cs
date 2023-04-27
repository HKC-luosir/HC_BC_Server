using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class CMD_Para
    {
        public int objectId { get; set; }
        public string lineIdStr { get; set; }
        public string commandType { get; set; }
        public string parameterId { get; set; }
        public string parameterType { get; set; }
        public string parameterName { get; set; }
        public bool required { get; set; }
        public string referenceValue { get; set; }
        public double maxValue { get; set; }
        public double minValue { get; set; }
        public string itemNumber { get; set; }
        public string paraEquipmentNo { get; set; }
        public string clientValue { get; set; }

    }
}
