using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace Glorysoft.BC.Client.CommonClass
{
    public class PopupWindow : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the PopupWindowVM class.
        /// </summary>
        public PopupWindow()
        {
        }
        public delegate void CloseWindow();
        public Action CloseParentWindow;
        public CloseWindow CloseWindowEvent;
        private RelayCommand closeCmd;
        public RelayCommand CloseCmd
        {
            get
            {
                return closeCmd ?? (closeCmd = new RelayCommand(Close));
            }
        }

        public virtual void Close()
        {
            if (CloseWindowEvent != null)
            {
                CloseWindowEvent();
            }
        }
    }
}
