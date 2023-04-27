using GalaSoft.MvvmLight;
using Glorysoft.BC.Entity;
using System;


namespace Glorysoft.BC.Client.ViewModel
{
    class MainFormCutVM : ViewModelBase
    {
        public MainFormCutVM()
        {

        }
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
    }
}
