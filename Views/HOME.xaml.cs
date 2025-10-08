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
            DataContext = new UserViewModel();
        }
        private void GoToTestPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ImportPage());
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Welcome());
        }

        private void GoToViewAllPages_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate((new ViewLibrary()));
        }
        
        private void GoToLoginPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate((new Login()));
        }
        
        private void GoToCreateLibraryPage_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate((new CreateLib()));
        }
    }
}
