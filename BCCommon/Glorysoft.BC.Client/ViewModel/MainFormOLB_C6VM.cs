using GalaSoft.MvvmLight;
using Glorysoft.BC.Entity;
using System;


namespace Glorysoft.BC.Client.ViewModel
{
    class MainFormOLB_C6VM : ViewModelBase
    {
        public MainFormOLB_C6VM()
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
