using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_packingbox
    {
        public string boxid { get; set; }
        public string boxspecid { get; set; }
        public string boxoperationid { get; set; }
        public string boxproductiontype { get; set; }
        public string boxworkorder { get; set; }
        public string boxgroupid { get; set; }
        public string boxcheckincode { get; set; }
        public string boxweight { get; set; }
        public string boxrivisioncode { get; set; }
        public string boxfgcode { get; set; }
        public string boxgrade { get; set; }
        public int panelqty { get; set; }
        public string boxserial { get; set; }
        public string palletid { get; set; }
        public DateTime createdate { get; set; }
        public string tranid { get; set; }
    }
}
