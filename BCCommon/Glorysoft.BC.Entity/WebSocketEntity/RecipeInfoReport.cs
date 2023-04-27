using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
 
    public class RecipeInfoReport : BaseClass
    {
        public RecipeInfoReport()
        {
            RecipeList = new List<RecipeInfoReportRecipe>();

        }
        public List<RecipeInfoReportRecipe> RecipeList { get; set; }
    }
    public class RecipeInfoReportRecipe
    {
        public string EQPID { get; set; }
        public string UnitID { get; set; }
        public string SUnitID { get; set; }
        public string RecipeNo { get; set; }
        public string RecipeVersion { get; set; }
        public string ParameterCount { get; set; }
        public string EventID { get; set; }
        public string PreviousRecipeNo { get; set; }
        public DateTime RecipeChangeTime { get; set; }
        public DateTime CreateDate { get; set; }
     
        public string RecipeParameterItems { get; set; }
      
        public string MessageSequenceNo { get; set; }
     
        public string RecipeType { get; set; }
    }
}
