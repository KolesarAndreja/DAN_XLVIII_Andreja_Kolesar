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
    class EmployeeViewModel:ViewModelBase
    {
        #region PROPERTY
        Employee emp;
        //current user
        private List<tblOrder> _orderList;
        public List<tblOrder> orderList
        {
            get
            {
                return _orderList;
            }
            set
            {
                _orderList = value;
                OnPropertyChanged("orderList");
            }
        }

        private tblOrder _singleOrder;
        public tblOrder singleOrder
        {
            get
            {
                return _singleOrder;
            }
            set
            {
                _singleOrder = value;
                OnPropertyChanged("singleOrder");
            }
        }
        #endregion

        #region Constructor
        public EmployeeViewModel(Employee open)
        {
            emp = open;
            orderList = Service.Service.GetAllOrders();
        }
        #endregion

        #region COMMANDS
        private ICommand _action;
        public ICommand action
        {
            get
            {
                if (_action == null)
                {
                    _action = new Command.RelayCommand(param => ActionExecute(), param => CanActionExecute());
                }
                return _action;
            }
        }

        private void ActionExecute()
        {
            try
            {
                if (singleOrder != null)
                {
                    if (singleOrder.status != "waiting")
                    {
                        MessageBox.Show("You cannot change status of this order.");
                    }
                    else
                    {
                        RejactOrApprove changeOrder = new RejactOrApprove(singleOrder);
                        changeOrder.ShowDialog();
                        if ((changeOrder.DataContext as RejactOrApproveViewModel).isChanged == true)
                        {
                            orderList = Service.Service.GetAllOrders();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanActionExecute()
        {
            return true;

        }


        private ICommand _archive;
        public ICommand archive
        {
            get
            {
                if (_archive == null)
                {
                    _archive = new Command.RelayCommand(param => ArchiveExecute(), param => CanArchiveExecute());
                }
                return _archive;
            }
        }

        private void ArchiveExecute()
        {
            try
            {
                if (singleOrder != null)
                {
                    if (singleOrder.status != "waiting")
                    {
                        MessageBoxResult result = MessageBox.Show("Do you realy want to archive this order?", "Archive Order", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            Service.Service.DeleteOrder(singleOrder);
                            orderList = Service.Service.GetAllOrders();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot archive order with waiting status.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanArchiveExecute()
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
                emp.Close();
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
