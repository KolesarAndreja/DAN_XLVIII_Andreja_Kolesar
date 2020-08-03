using DAN_XLIV_Andreja_Kolesar.ViewModel;
using System.Windows;

namespace DAN_XLIV_Andreja_Kolesar.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
        }
    }
}
