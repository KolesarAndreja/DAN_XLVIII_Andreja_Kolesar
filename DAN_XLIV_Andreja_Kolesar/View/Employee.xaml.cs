using DAN_XLIV_Andreja_Kolesar.Model;
using DAN_XLIV_Andreja_Kolesar.ViewModel;
using System.Windows;

namespace DAN_XLIV_Andreja_Kolesar.View
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : Window
    {

        public Employee()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this);
        }
    }
}
