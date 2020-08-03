using DAN_XLIV_Andreja_Kolesar.Command;
using DAN_XLIV_Andreja_Kolesar.Model;
using DAN_XLIV_Andreja_Kolesar.Service;
using DAN_XLIV_Andreja_Kolesar.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLIV_Andreja_Kolesar.ViewModel
{
    class MakeOrderViewModel:ViewModelBase
    {
        #region PROPERTY
        MakeOrder thisOrder;


        private tblOrder _newOrder;
        public tblOrder newOrder
        {
            get
            {
                return _newOrder;
            }
            set
            {
                _newOrder = value;
                OnPropertyChanged("newOrder");
            }
        }

        private bool _isMade;
        public bool isMade
        {
            get
            {
                return _isMade;
            }
            set
            {
                _isMade = value;
            }
        }

        private tblDish _pizza;
        public tblDish pizza
        {
            get
            {
                return _pizza;
            }
            set
            {
                _pizza = value;
                OnPropertyChanged("pizza");
            }
        }

        //current user
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
        #endregion

        public MakeOrderViewModel(MakeOrder openOrder,tblDish dish, User user)
        {
            thisOrder = openOrder;
            pizza = dish;
            newOrder = new tblOrder()
            {
                dishId = pizza.dishId,
                dateAndTime = DateTime.Now,
                status = "waiting",
                username = user.username
            };
        }


        #region command
        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                tblOrder o =  Service.Service.AddNewOrder(newOrder);
                if (o != null)
                {
                    MessageBox.Show("Order has been send. Status: WAITING.");
                    isMade = true;
                    thisOrder.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
        {
            if (newOrder.count<1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private ICommand _close;
        public ICommand close
        {
            get
            {
                if (_close == null)
                {
                    _close = new Command.RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return _close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                thisOrder.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }
}
