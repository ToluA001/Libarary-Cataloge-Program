using Libarary_Cataloge_Program.Views;
using Libarary_Cataloge_Program.Data;
using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace Libarary_Cataloge_Program
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Initialize all databases
            try
            {
                using (var context = new LibDataBase())
                {
                    context.Database.Migrate();
                }
                
                using (var context = new AuthDb())
                {
                    context.Database.Migrate();
                }
                
                using (var context = new Libs())
                {
                    context.Database.Migrate();
                }
                
                using (var context = new ULDB())
                {
                    context.Database.Migrate();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Database initialization error: {ex.Message}\n\n{ex.StackTrace}", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
