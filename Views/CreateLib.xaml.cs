using System.Windows.Controls;
using Libarary_Cataloge_Program.ViewModel;

namespace Libarary_Cataloge_Program.Views;

public partial class CreateLib : Page
{
    public CreateLib()
    {
        DataContext = new UserViewModel();
        InitializeComponent();
    }
}