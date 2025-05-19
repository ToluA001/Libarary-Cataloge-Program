using System.Windows.Controls;
using Libarary_Cataloge_Program.ViewModel;

namespace Libarary_Cataloge_Program.Views;

public partial class ViewLibrary : Page
{
    public ViewLibrary()
    {
        InitializeComponent();
        DataContext = new ViewAllVM();
    }
}