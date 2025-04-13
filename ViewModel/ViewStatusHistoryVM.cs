using Libarary_Cataloge_Program.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Libarary_Cataloge_Program.ViewModel
{
    class ViewStatusHistoryVM:BaseViewModel
    {
        public ObservableCollection<Dictionary<DateTime, bool>> BookHistory { get; set; }

        public ViewStatusHistoryVM()
        {
            BookHistory = new ObservableCollection<Dictionary<DateTime, bool>>( );
        }
    }
}
