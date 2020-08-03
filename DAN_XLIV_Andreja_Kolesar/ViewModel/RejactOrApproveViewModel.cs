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
    class RejactOrApproveViewModel:ViewModelBase
    {
        RejactOrApprove rejectOrApprove;
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

        private bool _isChanged;
        public bool isChanged
        {
            get
            {
                return _isChanged;
            }
            set
            {
                _isChanged = value;
            }
        }

        public RejactOrApproveViewModel(RejactOrApprove open, tblOrder order)
        {
            rejectOrApprove = open;
            currentOrder = order;
        }

        public RejactOrApproveViewModel(RejactOrApprove open)
        {
            rejectOrApprove = open;
        }


        #region command
        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new Command.RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                tblOrder o = Service.Service.AddNewOrder(currentOrder);
                if (o != null)
                {
                    System.Windows.MessageBox.Show("Order status has been changed. New status: " + currentOrder.status);
                    isChanged = true;
                    rejectOrApprove.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanSaveExecute()
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
                rejectOrApprove.Close();
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
