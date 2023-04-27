using GalaSoft.MvvmLight;
using Glorysoft.BC.Entity;
using System;


namespace Glorysoft.BC.Client.ViewModel
{
    class MainFormLIFT_MF0405VM : ViewModelBase
    {
        public MainFormLIFT_MF0405VM()
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
