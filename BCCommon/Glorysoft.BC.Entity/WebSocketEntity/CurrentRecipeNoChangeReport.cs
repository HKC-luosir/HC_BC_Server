
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Entity.WebSocketEntity
{
   
    public class CurrentRecipeNoChangeReport : BaseClass
    {
        public CurrentRecipeNoChangeReport() { }
        //       UnitID
        //CurrentRecipeNo
        public string UnitID { get; set; }
        public string CurrentRecipeNo { get; set; }
    }
}
