using Glorysoft.BC.Entity;
using Glorysoft.BC.Server.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Glorysoft.BC.Server.Infrastructure
{
    public class InforController
    {
        /// <summary>
        /// show window
        /// </summary>
        /// <typeparam name="T">ViewModel Type</typeparam>
        /// <param name="vm">ViewModel instance</param>
        public static void ShowSubWindow<T>(T vm) where T : PopupWindowViewModel
        {
            try
            {
                var viewType = Consts.ViewModelViewMap[typeof(T).Name];
                ObjectHandle handler = System.Activator.CreateInstance(null, Consts.ViewNameSpace + "." + viewType);
                var view = (Window)handler.Unwrap();
                view.DataContext = vm;
                vm.CloseWindowEvent = () => view.Close();
                view.ShowDialog();
            }
            catch (Exception e)
            {
                LogHelper.BCLog.Debug(string.Format(e.Message));
            }
        }
    }
}
