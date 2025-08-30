using System.Windows.Controls;
using Libarary_Cataloge_Program.ViewModel;

namespace Libarary_Cataloge_Program.Views;

public partial class Login : Page
{
    public Login()
    {
        InitializeComponent();
        DataContext = new UserViewModel();
    }
}