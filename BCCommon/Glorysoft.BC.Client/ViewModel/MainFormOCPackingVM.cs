using GalaSoft.MvvmLight;
using Glorysoft.BC.Entity;
using System;


namespace Glorysoft.BC.Client.ViewModel
{
    class MainFormOCPackingVM : ViewModelBase
    {
        public MainFormOCPackingVM()
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
