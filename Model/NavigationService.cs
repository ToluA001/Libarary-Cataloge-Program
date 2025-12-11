using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Libarary_Cataloge_Program.Model
{
    public class NavigationService
    {
        private static Frame _mainFrame;

        public static void Initialize(Frame frame)
        {
            _mainFrame = frame;
        }

        /// <summary>
        /// Open the page.
        /// </summary>
        /// <param name="pageName">string</param>
        public static void NavigateTo(string pageName)
        {
            _mainFrame.Navigate(new Uri($"{pageName}.xaml", UriKind.Relative));
        }

        public static void GoBack()
        {
            if (_mainFrame.CanGoBack)
                _mainFrame.GoBack();
        }
    }
}
