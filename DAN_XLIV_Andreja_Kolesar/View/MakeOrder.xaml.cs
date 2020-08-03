using DAN_XLIV_Andreja_Kolesar.Model;
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
    /// Interaction logic for MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Window
    {
        public tblDish dish { get; set; }
        
        public MakeOrder(tblDish pizza, User user)
        {
            InitializeComponent();
            dish = pizza;
            this.DataContext = new MakeOrderViewModel(this, pizza, user);
        }

        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            string temp = txtQuantity.Text;
            if(Int32.TryParse(temp,out int quantity))
            {
                total.Content = (dish.price * quantity).ToString();
            }
            else
            {
                total.Content = "0";
            }
        }
    }
}
