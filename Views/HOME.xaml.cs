using Libarary_Cataloge_Program.ViewModel;
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

namespace Libarary_Cataloge_Program.Views
{
    /// <summary>
    /// Interaction logic for HOME.xaml
    /// </summary>
    public partial class HOME : Window
    {
        public HOME()
        {
            InitializeComponent();
            DataContext = new MainWindowViewWindow();
        }
        private void GoToTestPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ImportPage());
        }
    }
}
