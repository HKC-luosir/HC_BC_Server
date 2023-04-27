using Glorysoft.BC.Entity;
using Glorysoft.BC.Entity.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glorysoft.BC.Server.ViewModel
{
    public class PopupWindowViewModel : NotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the PopupWindowVM class.
        /// </summary>
        public PopupWindowViewModel()
        {
        }
        public delegate void CloseWindow();
        public Action CloseParentWindow;
        public CloseWindow CloseWindowEvent;
        private DelegateCommand closeCmd;
        public DelegateCommand CloseCmd
        {
            get
            {
                return closeCmd ?? (closeCmd = new DelegateCommand(Close));
            }
        }

        protected virtual void Close()
        {
            if (CloseWindowEvent != null)
            {
                CloseWindowEvent();
            }
        }
    }
}
