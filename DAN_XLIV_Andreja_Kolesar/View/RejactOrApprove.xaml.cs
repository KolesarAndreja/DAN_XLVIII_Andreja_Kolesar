using DAN_XLIV_Andreja_Kolesar.Service;
using DAN_XLIV_Andreja_Kolesar.ViewModel;
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

namespace DAN_XLIV_Andreja_Kolesar.View
{
    /// <summary>
    /// Interaction logic for AdjactOrApprove.xaml
    /// </summary>
    public partial class RejactOrApprove : Window
    {
        public RejactOrApprove()
        {
            InitializeComponent();
            this.DataContext = new RejactOrApproveViewModel(this);
        }

        public RejactOrApprove(tblOrder order)
        {
            InitializeComponent();
            this.DataContext = new RejactOrApproveViewModel(this,order);
        }

    }
}
