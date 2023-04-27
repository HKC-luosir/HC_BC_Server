using GalaSoft.MvvmLight;
using Glorysoft.BC.Client.CommonClass;
using Glorysoft.BC.Entity;
using GlorySoft.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Glorysoft.BC.Client.ViewModel
{
    public class UserConfigVM : PopupWindow
    {

        #region Constructor
        public UserConfigVM()
        {
            UserList = new ObservableCollection<User>(ClientRequest.GetUserList());
        }
        #endregion

        #region Property
        private ObservableCollection<User> userList;
        public ObservableCollection<User> UserList
        {
            get { return userList; }
            set
            {
                userList = value;
                RaisePropertyChanged("UserList");
            }
        }

        private User updatingUser;
        public User UpdatingUser
        {
            get { return updatingUser; }
            set
            {
                updatingUser = value;
                RaisePropertyChanged("UpdatingUser");
            }
        }

        private bool operater = false;
        public bool Operater
        {
            get { return operater; }
            set
            {
                operater = value;
                RaisePropertyChanged("Operater");
            }
        }

        private bool admin = true;
        public bool Admin
        {
            get { return admin; }
            set
            {
                admin = value;
                RaisePropertyChanged("Admin");
            }
        }

        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                RaisePropertyChanged("SelectedUser");
                var tmp = new User();
                if (selectedUser == null) return;
                tmp.UserID = selectedUser.UserID;
                tmp.UserName = selectedUser.UserName;
                tmp.Password = selectedUser.Password;
                tmp.Creator = selectedUser.Creator;
                tmp.CreateDate = selectedUser.CreateDate;
                tmp.Level = selectedUser.Level;
                switch (selectedUser.Level)
                {
                    case 1:
                        Operater = true;
                        break;
                    case 2:
                        Admin = true;
                        break;
                }
                UpdatingUser = tmp;
            }
        }
        #endregion

        #region  AddCommand
        private DelegateCommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand ?? (addCommand = new DelegateCommand(Add)); }
        }
        private void Add()
        {
            if (updatingUser.UserID.Trim().Length == 0)
            {
                MessageBox.Show("UserID不能为空！", "User", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (updatingUser.Password.Trim().Length == 0)
            {
                MessageBox.Show("Password不能为空！", "User", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            updatingUser.CreateDate = DateTime.Now;
            if (Operater) updatingUser.Level = 1;
            if (Admin) updatingUser.Level = 2;
            var bRet = ClientRequest.InsertUser(updatingUser);
            MessageBox.Show(
                string.Format("User: {0}: 新增" + (bRet ? "成功！" : "失败！"),
                              updatingUser.UserID), "Info",
                MessageBoxButton.OK, MessageBoxImage.Information);

            UserList = new ObservableCollection<User>(ClientRequest.GetUserList());
        }
        #endregion 

        #region EditCommand
        private DelegateCommand editCommand;
        public ICommand EditCommand
        {
            get { return editCommand ?? (editCommand = new DelegateCommand(Edit)); }
        }
        private void Edit()
        {
            if (updatingUser.UserID.Trim().Length == 0)
            {
                MessageBox.Show("UserID不能为空！", "User", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (updatingUser.Password.Trim().Length == 0)
            {
                MessageBox.Show("Password不能为空！", "User", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Operater) updatingUser.Level = 1;
            if (Admin) updatingUser.Level = 2;

            var user = new User
            {
                UserID = updatingUser.UserID,
                UserName=updatingUser.UserName,
                Password = updatingUser.Password,
                Level = updatingUser.Level,
                Creator = updatingUser.Creator,
                CreateDate = DateTime.Now
            };
            var bRet = ClientRequest.UpdateUser(user);
            MessageBox.Show(
    string.Format("User: {0}: 修改" + (bRet ? "成功！" : "失败！"), updatingUser.UserID),
    "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            UserList = new ObservableCollection<User>(ClientRequest.GetUserList());
        }
        #endregion

        #region DeleteCommand
        private DelegateCommand<object> deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                    deleteCommand = new DelegateCommand<object>(Delete);
                return deleteCommand;
            }
        }
        private void Delete(object items)
        {
            if (updatingUser == null) return;

            if (MessageBox.Show("你确定删除选中的用户？", "Info", MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var bRet = ClientRequest.DeleteUser(updatingUser.UserID);
                MessageBox.Show(
        string.Format("User: {0}: 删除" + (bRet ? "成功！" : "失败！"), updatingUser.UserID),
        "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                UserList = new ObservableCollection<User>(ClientRequest.GetUserList());
            }


        }
        #endregion

    }
}
