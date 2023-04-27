using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity
{
    public class UseRecipeList
    {
        public UseRecipeList()
        {
            RecipeNo = "";
            UnitID = "";
            Parameter = "";
            EQPID = "";
            PPID = "";
        }
        public string RecipeNo { get; set; }
        public string UnitID { get; set; }
        public string Parameter { get; set; }
        public string EQPID { get; set; }
        public string PPID { get; set; }
    }
}
