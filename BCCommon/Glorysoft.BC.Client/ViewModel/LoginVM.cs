using GalaSoft.MvvmLight;
using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Entity;
using GlorySoft.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Glorysoft.BC.Client.ViewModel
{
    public class LoginVM: PopupWindow
    {
        public delegate void CloseWindowDeleget(bool result);
        public static event CloseWindowDeleget OnCloseEvent;
        public LoginVM()
        {
            curUser = new User();
        }
        private User curUser;
        public User CurUser
        {
            get { return curUser; }
            set
            {
                curUser = value;
                RaisePropertyChanged("CurUser");
            }
        }
        private string passWord;
        public string PassWord
        {
            get { return passWord; }
            set
            {
                passWord = value;
                RaisePropertyChanged("PassWord");
            }
        }

        #region Login
        private DelegateCommand loginCommand;
        public ICommand LoginCommand
        {
            get { return loginCommand ?? (loginCommand = new DelegateCommand(Login)); }
        }
        private void Login()
        {
            try
            {
                if (string.IsNullOrEmpty(curUser.UserID))
                {
                    MessageBox.Show("Please Input User ID!!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(curUser.Password))
                {
                    MessageBox.Show("Please Input Password!!", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var oUser = ClientRequest.GetUserInfo(curUser.UserID.Trim());
                if (oUser == null)
                {
                    MessageBox.Show("用户不存在！", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (oUser.Password != curUser.Password)
                {
                    MessageBox.Show("密码不正确！", "Info", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (oUser.Level == Level.NormarlUser)
                {
                    MessageBox.Show("你没有权限进行此项动作，请切换账号或者请有权限的人进行操作", "Warn", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                OnCloseEvent(true);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 

    }

}
