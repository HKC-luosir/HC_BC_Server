using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{

    public class RecipeInfoRquest : BaseClass
    {
        public RecipeInfoRquest() { }

        public string UnitID { get; set; }
        public string SUnitID { get; set; }
        public string RecipeNo { get; set; }
    }
}
