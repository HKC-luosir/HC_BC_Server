using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class PPIDAndRecipe
    {
        public PPIDAndRecipe()
        {

        }
       
        public string EQPID { get; set; }
      /// <summary>
      /// mes
      /// </summary>
        public string PPID { get; set; }
        /// <summary>
        /// eqp
        /// </summary>
        public string RecipeID { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string UnitID { get; set; }

        public int  LocalID { get; set; }
    }
}
