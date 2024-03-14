using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libarary_Cataloge_Program.Model;
using Libarary_Cataloge_Program.ViewModel;


namespace Libarary_Cataloge_Program.ViewModel
{
    class ViewAllVM:BaseViewModel
    {
        public ObservableCollection<Book> Books { get; set; }

        public ViewAllVM()
        {
            Books = new ObservableCollection<Book>(BookRepository.Instance.GAB());
        }
    }
}
