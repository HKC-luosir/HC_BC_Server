
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Glorysoft.BC.Entity
{
    public class RecipeDownload : INotifyPropertyChanged
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
        public RecipeDownload()
        {
            LineID = "";
            ReturnCode = "";
        }
        public string LineID { get; set; }
        public string ReturnCode { get; set; }
    }
    public class RecipeParameter : INotifyPropertyChanged
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
        public RecipeParameter()
        {
           
        }
        public string EQPID { get; set; }
        public DateTime CreateDate { get; set; }
        public string UnitID { get; set; }
        public DateTime RecipeChangeTime { get; set; }
        //<result property = "RecipeParameterName" column="recipeparametername"/>
        //<result property = "Index" column="index"/>
        //<result property = "OperationEnable" column="operationenable"/>
        //<result property = "OperationSymbol" column="operationsymbol"/>
        //<result property = "OperationProportion" column="operationproportion"/>
        public string RecipeParameterName { get; set; }
        public int Index { get; set; }
        public bool OperationEnable { get; set; }
        public string OperationSymbol { get; set; }
        public float OperationProportion { get; set; }
        public string ItemName { get; set; }
        //public string ItemType { get; set; }
    }
}
