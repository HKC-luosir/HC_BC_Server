using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class his_recipe
    {
        public string eqpid { get; set; }
        public DateTime createdate { get; set; }
        public string unitid { get; set; }
        public string sunitid { get; set; }
        public string recipeno { get; set; }
        public string recipeversion { get; set; }
        public string parametercount { get; set; }
        public string eventid { get; set; }
        public string previousrecipeno { get; set; }
        public DateTime recipechangetime { get; set; }
        public string recipeparameteritems { get; set; }
        public string recipetype { get; set; }
        public string messagesequenceno { get; set; }
    }
}
