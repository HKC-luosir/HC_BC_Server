using Glorysoft.BC.Entity;
using System.Collections.Generic;
using GlorySoft.UI;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using Glorysoft.BC.Client.CommonClass;

namespace Glorysoft.BC.Client.ViewModel
{
    public class ControlStateSettingVM : PopupWindow
    {
        public ControlStateSettingVM()
        {
            switch (OClient.ControlState)
            {
                case ControlState.OnlineRemote:onLineRemote = true;
                    break;
                case ControlState.OnlineLocal:
                    onLineLocal = true;
                    break;
                case ControlState.Offline:
                    offLine = true;
                    break;
                case ControlState.LCControl:
                    lcControl = true;
                    break;

            }
        }

        #region properties
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


        private bool onLineRemote;

        public bool OnLineRemote
        {
            get
            {
                return onLineRemote;
            }
            set
            {
                if (value != onLineRemote)
                {
                    onLineRemote = value;
                    RaisePropertyChanged("OnLineRemote");
                }
            }
        }

        private bool onLineLocal;

        public bool OnLineLocal
        {
            get
            {
                return onLineLocal;
            }
            set
            {
                if (value != onLineLocal)
                {
                    onLineLocal = value;
                    RaisePropertyChanged("OnLineLocal");
                }
            }
        }

        private bool offLine;

        public bool OffLine
        {
            get
            {
                return offLine;
            }
            set
            {
                if (value != offLine)
                {
                    offLine = value;
                    RaisePropertyChanged("OffLine");
                }
            }
        }

        private bool lcControl;
        public bool LCControl
        {
            get
            {
                return lcControl;
            }
            set
            {
                if (value!=lcControl)
                {
                    lcControl = value;
                    RaisePropertyChanged("LCControl");
                }
            }
        }

        #endregion properties

        #region Command

        public DelegateCommand setControlStateCommand
        {
            get;
            private set;
        }

        public ICommand SetControlStateCommand
        {
            get
            {
                if (setControlStateCommand == null) setControlStateCommand = new DelegateCommand(SetControlState);
                return setControlStateCommand;
            }
        }
        #endregion Command
        private void SetControlState()
        {
            try
            {
                if(!ClientInfo.Current.OClient.IsHostConnect) return;
                if (offLine && OClient.ControlState != ControlState.Offline)
                {
                    OClient.ControlState = ControlState.Offline;
                    ClientRequest.UpdateControlState(OClient.ControlState);
                }
                else if (OnLineRemote && OClient.ControlState != ControlState.OnlineRemote)
                {
                    OClient.ControlState = ControlState.OnlineRemote;
                    ClientRequest.UpdateControlState(OClient.ControlState);
                }
                else if (OnLineLocal)
                {
                    OClient.ControlState = ControlState.OnlineLocal;
                    ClientRequest.UpdateControlState(OClient.ControlState);
                }
                else if (LCControl)
                {
                    OClient.ControlState = ControlState.LCControl;
                    ClientRequest.UpdateControlState(OClient.ControlState);
                }
                 
            }
            catch (System.Exception ex)
            {

            }

        }



    }
}