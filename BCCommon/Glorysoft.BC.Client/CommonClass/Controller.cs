using System.Runtime.Remoting;
using System.Windows;

namespace Glorysoft.BC.Client.CommonClass
{
    public class Controller
    {
        /// <summary>
        /// show window
        /// </summary>
        /// <typeparam name="T">ViewModel Type</typeparam>
        /// <param name="vm">ViewModel instance</param>
        public static void ShowSubWindow<T>(T vm) where T : PopupWindow
        {
            try
            {
                var viewType = Consts.ViewModelViewMap[typeof(T).Name];
                ObjectHandle handler = System.Activator.CreateInstance(null, Consts.ViewNameSpace + "." + viewType);
                var view = (Window)handler.Unwrap();
                view.DataContext = vm;
                vm.CloseParentWindow = () => view.Close();
                vm.CloseWindowEvent = () => view.Close();
                view.Show();
            }
            catch (System.Exception ex)
            {

            }

        }

        public static bool ShowSubWindowDialog<T>(T vm) where T :PopupWindow
        {
            try
            {
                var viewType = Consts.ViewModelViewMap[typeof(T).Name];
                ObjectHandle handler = System.Activator.CreateInstance(null, Consts.ViewNameSpace + "." + viewType);
                var view = (Window)handler.Unwrap();
                view.DataContext = vm;
                vm.CloseParentWindow = () => view.Close();
                vm.CloseWindowEvent = () => view.Close();
                view.ShowDialog();
                return (bool)view.DialogResult;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}

