using Glorysoft.Auto.Contract;
using Glorysoft.BC.Entity;
using Glorysoft.BC.Logic.Contract;
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

namespace Glorysoft.BC.Server
{
    /// <summary>
    /// ConfirmWin.xaml 的交互逻辑
    /// </summary>
    public partial class ConfirmWin : Window
    {
        protected static ILogicService logicService;
        protected static IDBService dbService;
        public ConfirmWin()
        {
            InitializeComponent();
            try
            {
                
                logicService = CommonContexts.ResolveInstance<ILogicService>();
                dbService = CommonContexts.ResolveInstance<IDBService>();
                txtUser.Focus();
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
            }
           
        }
        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (txtUser.Text.Trim().Length == 0)
                //{
                //    MessageBox.Show("Please Input User ID!!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}

                if (txtPWD.Password.Trim().Length == 0)
                {
                    MessageBox.Show("Please Input Password!!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //if (txtReason.Text.Trim().Length == 0)
                //{
                //    MessageBox.Show("Please Input Reason!!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}

                //var oUser = dbService.FindUser(txtUser.Text.Trim());

                //if (HostInfo.Current.SystemConfig.UserName != txtUser.Text.Trim())
                //{
                //    MessageBox.Show("用户不存在！", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                //    txtUser.SelectAll();
                //    txtUser.Focus();
                //    return;
                //}
                HostInfo.Current.SystemConfig.Password = "123456";
                if (HostInfo.Current.SystemConfig.Password != txtPWD.Password)
                {
                    MessageBox.Show("密码不正确！", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtPWD.SelectAll();
                    txtPWD.Focus();
                    return;
                }

                //if (oUser.Level == 1)
                //{
                //    MessageBox.Show("普通用户无此权限！", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                //    return;
                //}
                DialogResult = true;
            }
            catch(Exception ex)
            {
                LogHelper.BCLog.Debug(ex);
                DialogResult = false;
            }
           
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void txtPWD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtReason.Focus();
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                txtPWD.Focus();
        }

        private void txtReason_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                btnYes_Click(sender, e);
        }
    }
}
