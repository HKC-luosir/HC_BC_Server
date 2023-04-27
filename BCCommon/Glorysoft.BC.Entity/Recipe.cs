using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class Recipe : INotifyPropertyChanged
    {
        #region INofifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INofifyPropertyChanged Members

        protected void Notify(string propertyName)
        {
            if (PropertyChanged == null) return;
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged(this, e);
        }
        public Recipe()
        {
            ParameterList = new List<Parameter>();
        }
        public string EQPID { get; set; }      
        public string UnitID { get; set; }
        public string SUnitID { get; set; }       
        public string RecipeNo { get; set; }
        public string RecipeVersion { get; set; }
        //<result property = "EQPID" column="eqpid"/>
        //    <result property = "UnitID" column="unitid"/>
        //    <result property = "SUnitID" column="sunitid"/>
        //    <result property = "RecipeNo" column="recipeno"/>
        //    <result property = "RecipeVersion" column="recipeversion"/>
        public string ParameterCount { get; set; }
        public string EventID { get; set; }
        public string PreviousRecipeNo { get; set; }
        public string RecipeChangeTime { get; set; }
        public DateTime CreateDate { get; set; }
      
        //public string RecipeParameterItems { get; set; }
   
        public List<Parameter> ParameterList { get; set; }

        public string MessageSequenceNo { get; set; }
        /// <summary>
        /// E U S 
        /// E : Check Only EQP Recipe
        /// U : Check EQP, Unit Recipe&Parameter
        /// S : Check EQP, Unit, SubUnit Recipe&Parameter
        /// </summary>
        public string RecipeType { get; set; }
    }
}
