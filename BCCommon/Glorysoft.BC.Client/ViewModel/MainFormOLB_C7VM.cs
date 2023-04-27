using GalaSoft.MvvmLight;
using Glorysoft.BC.Entity;
using System;


namespace Glorysoft.BC.Client.ViewModel
{
    class MainFormOLB_C7VM : ViewModelBase
    {
        public MainFormOLB_C7VM()
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
