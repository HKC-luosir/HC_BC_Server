using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.BaseClass;
using Glorysoft.BC.Logic.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Glorysoft.BC.Server.ViewModel
{
    public class LineModeViewModel : NotifyPropertyChanged
    {
        public LineModeViewModel()
        {
            HostInLineCommand = new DelegateCommand(HostInLine);
        }
        #region 字段
        public static HostInfo LineInfo
        {
            get
            {
                return HostInfo.Current;
            }
        }


        private bool onLineRemote = false;

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
                    Notify("OnLineRemote");
                }
            }
        }

        private bool onLineLocal = false;

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
                    Notify("OnLineLocal");
                }
            }
        }

        private bool offLine = true;

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
                    Notify("OffLine");
                }
            }
        }

        #endregion

        public DelegateCommand HostInLineCommand
        {
            get;
            private set;
        }
        private void HostInLine()
        {
            try
            {

                //if (!HostInfo.Current.IsHostConnect) return;
                if (MessageBoxResult.Yes ==
                  MessageBox.Show("您确定要修改Line Mode吗？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    //IEISService secsCmd = CommonContexts.ResolveInstance<IEISService>();
                    //if (offLine && HostInfo.Current.ControlState != ControlState.Offline)
                    //{
                    //    HostInfo.Current.ControlState = ControlState.Offline;
                    //    secsCmd.SendToHSMSControlStateChangeS6F11_111(ControlState.Offline);
                    //    //ClientRequest.UpdateControlState(OClient.ControlState);
                    //}
                    //else if (OnLineRemote && HostInfo.Current.ControlState != ControlState.OnlineRemote)
                    //{
                    //    HostInfo.Current.ControlState = ControlState.OnlineRemote;
                    //    secsCmd.SendToHSMSControlStateChangeS6F11_111(ControlState.OnlineRemote);
                    //    //ClientRequest.UpdateControlState(OClient.ControlState);
                    //}
                    //else if (OnLineLocal)
                    //{
                    //    HostInfo.Current.ControlState = ControlState.OnlineLocal;
                    //    secsCmd.SendToHSMSControlStateChangeS6F11_111(ControlState.OnlineLocal);
                    //    //ClientRequest.UpdateControlState(OClient.ControlState);
                    //}

                }

            }
            catch (System.Exception ex)
            {
                LogHelper.BCLog.Error(ex);
            }

        }
    }
}
