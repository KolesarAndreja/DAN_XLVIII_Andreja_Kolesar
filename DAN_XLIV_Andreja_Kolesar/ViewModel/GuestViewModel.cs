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
    class GuestViewModel:ViewModelBase
    {
        #region PROPERTY
        Guest guest;
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

        private List<tblDish> _menuList;
        public List<tblDish> menuList
        {
            get
            {
                return _menuList;
            }
            set
            {
                _menuList = value;
                OnPropertyChanged("menuList");
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

        private List<tblOrder> _myOrdersList;
        public List<tblOrder> myOrdersList
        {
            get
            {
                return _myOrdersList;
            }
            set
            {
                _myOrdersList = value;
                OnPropertyChanged("myOrdersList");
            }
        }

        private tblOrder _currentOrder;
        public tblOrder currentOrder
        {
            get
            {
                return _currentOrder;
            }
            set
            {
                _currentOrder = value;
                OnPropertyChanged("currentOrder");
            }
        }

        #endregion

        #region CONSTRUCTOR
        public GuestViewModel(Guest open, User user)
        {
            guest = open;
            currentUser = user;
            menuList = Service.Service.GetMenu();
            myOrdersList = Service.Service.GetOrdersByUsername(currentUser.username);
        }

        public GuestViewModel(Guest open)
        {
            guest = open;
        }
        #endregion

        #region VISIBILITY
        private Visibility _btnToOrder = Visibility.Visible;
        public Visibility btnToOrder
        {
            get
            {
                if (myOrdersList.Count!=0)
                {
                    for(int i=0; i< myOrdersList.Count; i++)
                    {
                        if(myOrdersList[i].status == "waiting")
                        {
                            return Visibility.Collapsed;
                        }
                    }
                }
                return _btnToOrder;
            }
            set
            {
                _btnToOrder = value;
                OnPropertyChanged("btnToOrder");
            }
        }
        private Visibility _ordersVisibility = Visibility.Visible;
        public Visibility ordersVisibility
        {
            get
            {
                if (myOrdersList.Count()==0)
                {
                    return Visibility.Collapsed;
                }
                return _ordersVisibility;
            }
            set
            {
                _ordersVisibility = value;
                OnPropertyChanged("ordersVisibility");
            }
        }
        #endregion

        #region COMMANDS
        private ICommand _orderPizza;
        public ICommand orderPizza
        {
            get
            {
                if (_orderPizza == null)
                {
                    _orderPizza = new Command.RelayCommand(param => OrderPizzaExecute(), param => CanOrderPizzaExecute());
                }
                return _orderPizza;
            }
        }

        private void OrderPizzaExecute()
        {
            try
            {
                if (pizza != null)
                {
                    MakeOrder newOrder = new MakeOrder(pizza,currentUser);
                    newOrder.ShowDialog();
                    if ((newOrder.DataContext as MakeOrderViewModel).isMade == true)
                    {
                        btnToOrder = Visibility.Collapsed;
                        myOrdersList = Service.Service.GetOrdersByUsername(currentUser.username);
                        ordersVisibility = Visibility.Visible;
                    }
                }   

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanOrderPizzaExecute()
        {
            return true;
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
                Login login = new Login();
                guest.Close();
                login.Show();
                
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
