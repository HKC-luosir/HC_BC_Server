using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Entity;
using GlorySoft.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Glorysoft.BC.Client.ViewModel
{
    public class RecipeVM : PopupWindow
    {
        public RecipeVM()
        {
           // eqpList = new ObservableCollection<string>(ClientInfo.Current.OClient.EQPList.Values.ToList().Where(a => a.CheckRecipeFlag == true).Select(f => f.EQPName).ToList());
        }
        private ObservableCollection<string> eqpList;
        public ObservableCollection<string> EQPList
        {
            get
            {
                return eqpList;
            }
            set
            {
                eqpList = value;
                RaisePropertyChanged("EQPList");
            }
        }
        private ObservableCollection<Unit> unitList;
        public ObservableCollection<Unit> UnitList
        {
            get
            {
                return unitList;
            }
            set
            {
                unitList = value;
                RaisePropertyChanged("UnitList");
            }
        }

        private string eqpName;
        public string EQPName
        {
            get
            {
                return eqpName;
            }
            set
            {
                eqpName = value;
                RaisePropertyChanged("EQPName");
            }
        }
        private Unit unitName;
        public Unit UnitName
        {
            get
            {
                return unitName;
            }
            set
            {
                unitName = value;
                RaisePropertyChanged("UnitName");
            }
        }
        private IList<Parameter> recipeParaList;
        public IList<Parameter> RecipeParaList
        {
            get
            {
                return recipeParaList;
            }
            set
            {
                recipeParaList = value;
                RaisePropertyChanged("RecipeParaList");
            }
        }

        private IList<Recipe> recipeList;
        public IList<Recipe> RecipeList
        {
            get
            {
                return recipeList;
            }
            set
            {
                recipeList = value;
                RaisePropertyChanged("RecipeList");
            }
        }

        private IList<Recipe> recipeIDList;
        public IList<Recipe> RecipeIDList
        {
            get
            {
                return recipeIDList;
            }

            set
            {
                recipeIDList = value;
                RaisePropertyChanged("RecipeIDList");
            }
        }
        private Recipe selectRecipe;
        public Recipe SelectRecipe
        {
            get
            {
                return selectRecipe;
            }
            set
            {
                selectRecipe = value;
                RaisePropertyChanged("SelectRecipe");
                var paralist = RecipeList.Where(f => f.RecipeID == SelectRecipe.RecipeID).OrderBy(c => c.ParameterName).ToList();
                if (paralist.Count > 0)
                    RecipeParaList = paralist[0].ParameterList;
            }
        }

        private DelegateCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null) searchCommand = new DelegateCommand(Search);
                return searchCommand;
            }
        }
        private void Search()
        {
            if (string.IsNullOrEmpty(eqpName))
            {
                MessageBox.Show("请先选择一台设备！", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var eqpid = ClientInfo.Current.OClient.EQPList[eqpName].EQPID;
            var unitID = UnitName != null ? UnitName.UnitID : "";
            Recipe item = new Recipe();
            item.EQPID = eqpid;
            item.UnitID = unitID;
            item.EQPName = eqpName;
           // RecipeList = ClientRequest.ViewRecipeHistoryByEQPID(item).ToList();
            RecipeIDList = RecipeList;
        }
        private DelegateCommand selectChangeCommand;
        public DelegateCommand SelectChangeCommand
        {
            get
            {
                if (selectChangeCommand == null) selectChangeCommand = new DelegateCommand(SelectChange);
                return selectChangeCommand;
            }
        }
        private void SelectChange()
        {
            //var lst = new ObservableCollection<Unit>(ClientInfo.Current.OClient.EQPList[eqpName].UnitList.Values.ToList());
            //UnitList = lst;
        }
    }
}
