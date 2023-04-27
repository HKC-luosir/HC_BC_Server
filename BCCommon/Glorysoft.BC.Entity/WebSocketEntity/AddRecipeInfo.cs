using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
    public class AddRecipeInfo
    {
        public string eqpid { get; set; }
        public string ppid { get; set; }
        public string processMode { get; set; }
        public string createUser { get; set; }
        public bool hascvd { get; set; }
        public string remark { get; set; }
        public List<RecipeRowData> recipeValueList { get; set; }

    }
}
