using System;
using System.Collections.Generic;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Client.CommonClass;
using GlorySoft.UI;
using System.Windows.Input;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Data;

namespace Glorysoft.BC.Client.ViewModel
{
    public class PPIDandRecipeIDMapVM : PopupWindow
    {
        public ClientInfo OGloble
        {
            get
            {
                return ClientInfo.Current;
            }
            set
            {
                RaisePropertyChanged("OGloble");
            }
        }
        public PPIDandRecipeIDMapVM()
        {
            OGloble.PPIDRecipeTable = new DataTable();
           // ClientRequest.ViewPPIDAndRecipeListByLine("9MMTBA011");
        }
    }
}
