using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Client.ViewModel;
using System;
using System.Windows;

namespace Glorysoft.BC.Client.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowBase
    {
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void WindowBase_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void WindowBase_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Login login = new Login();
                login.ShowDialog();
                if ((bool)login.DialogResult)
                {
                    Application.Current.Shutdown();
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}