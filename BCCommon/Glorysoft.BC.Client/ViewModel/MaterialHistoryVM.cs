using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections;
using GalaSoft.MvvmLight;
using Glorysoft.BC.Entity;
using GlorySoft.UI;
using System.Linq;
using Glorysoft.BC.Client.CommonClass;

namespace Glorysoft.BC.Client.ViewModel
{
    public class MaterialHistoryVM : PopupWindow
    {
        public MaterialHistoryVM()
        {
            SelectDateFrom = DateTime.Today.AddDays(-7);
            SelectDateTo = DateTime.Today;
            CurrentEQP = "";
        }

        #region property
        public HostInfo OClient
        {
            get
            {
                return ClientInfo.Current.OClient;
            }
            set
            {
                RaisePropertyChanged("OClient");
            }
        }
        private DateTime selectDateFrom;
        public DateTime SelectDateFrom
        {
            get { return selectDateFrom; }
            set
            {
                selectDateFrom = value;
                RaisePropertyChanged("SelectDateFrom");
            }
        }

        private DateTime selectDateTo;
        public DateTime SelectDateTo
        {
            get { return selectDateTo; }
            set
            {
                selectDateTo = value;
                RaisePropertyChanged("SelectDateTo");
            }
        }

        private IList<MaterialInfo> materialList;
        public IList<MaterialInfo> MaterialList
        {
            get { return materialList; }
            set
            {
                materialList = value;
                RaisePropertyChanged("MaterialList");
            }
        }

        private string currentEQP;
        public string CurrentEQP
        {
            get
            {
                return string.IsNullOrEmpty(currentEQP) ? "*" : currentEQP;
            }
            set
            {
                currentEQP = value;
                RaisePropertyChanged("CurrentEQP");
            }
        }

        //public IList<string> EQPList
        //{
        //    get
        //    {
        //        return OClient.EQPList.Values.Select(f => f.EQPName).ToList();
        //    }
        //}

        private bool okChecked;
        public bool OkChecked
        {
            get { return okChecked; }
            set
            {
                okChecked = value;
                RaisePropertyChanged("OkChecked");
            }
        }

        private bool ngChecked;
        public bool NGChecked
        {
            get { return ngChecked; }
            set
            {
                ngChecked = value;
                RaisePropertyChanged("NGChecked");
            }
        }
        #endregion



        #region SearchCommand
        private DelegateCommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                    searchCommand = new DelegateCommand(Search);
                return searchCommand;
            }
        }
        private void Search()
        {
            GridBind();
        }
        private void GridBind()
        {
            var iRTCode = -1;
            if (okChecked) iRTCode = HostRTCode.OK;
            if (ngChecked) iRTCode = HostRTCode.NG;

            var map = new Hashtable
                                {                                    
                                    {"FromDate",SelectDateFrom.ToString("yyyy-MM-dd 00:00:00")},
                                    {"ToDate",SelectDateTo.ToString("yyyy-MM-dd 23:59:59")}
                                };
            
            if (iRTCode != -1) map.Add("ValidationResult", iRTCode);
            if (string.IsNullOrEmpty(CurrentEQP))
            {
                //string eqpid = OClient.EQPList.Values.FirstOrDefault(f => f.EQPName == CurrentEQP).EQPID;
                //map.Add("EQPID", eqpid);
            }
            //var lst = ClientRequest.ViewMaterialHistory(map);
            //MaterialList = lst;
        }
        #endregion

    }
}
