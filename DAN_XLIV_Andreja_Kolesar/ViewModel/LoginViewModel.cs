using DAN_XLIV_Andreja_Kolesar.Command;
using DAN_XLIV_Andreja_Kolesar.Model;
using DAN_XLIV_Andreja_Kolesar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLIV_Andreja_Kolesar.ViewModel
{
    class LoginViewModel:ViewModelBase
    {
        Login login;

        private User _currentUser;
        public User currentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                OnPropertyChanged("currentUser");
            }
        }

        public LoginViewModel(Login openLogin)
        {
            login = openLogin;
            currentUser = new User();
        }

        #region Commands
        private ICommand _loginBtn;
        public ICommand loginBtn
        {
            get
            {
                if (_loginBtn == null)
                {
                    _loginBtn = new RelayCommand(LoginExecute, CanLoginExecute);
                }
                return _loginBtn;
            }
        }

        private void LoginExecute(object obj)
        {
            currentUser.password = (obj as PasswordBox).Password;
            try
            {
                switch (currentUser.role)
                {
                    case "zaposleni":
                        Employee openEmployee = new Employee();
                        login.Close();
                        openEmployee.ShowDialog();
                        break;
                    case "gost":
                        Guest openGuest = new Guest(currentUser);
                        login.Close();
                        openGuest.ShowDialog();
                        break;
                    case null:
                        MessageBox.Show("Invalid username or password. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanLoginExecute(object obj)
        {
            return true;
        }
        #endregion


    }
}
