using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Glorysoft.BC.Client.View
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginVM();
            LoginVM.OnCloseEvent += new LoginVM.CloseWindowDeleget(CloseWindow);
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
        private void txtUserID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtPwd.Focus();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnLogin.PerformClick();
        }

        private void WindowBase_Closed(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void WindowBase_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CloseWindow(bool result)
        {
            if (result)
            {
                if (DialogResult == null)
                {
                    DialogResult = result;
                    this.Close();
                }
                else
                {
                    this.Close();
                }

            }
            else DialogResult = false;
        }
    }
}
