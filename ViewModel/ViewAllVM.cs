using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libarary_Cataloge_Program.Data;
using Libarary_Cataloge_Program.Model;
using Libarary_Cataloge_Program.ViewModel;


namespace Libarary_Cataloge_Program.ViewModel
{
    class ViewAllVM:BaseViewModel
    {
        private readonly LibDataBase _context;

        public ObservableCollection<Book> Books { get; set; }

        public ViewAllVM()
        {
            LoadBooks();
        }
        
        private void LoadBooks()
        {
            using (var db = new LibDataBase())
            {
                var booksFromDb = db.Books.ToList();
                Books = new ObservableCollection<Book>((IEnumerable<Book>)booksFromDb);
                OnPropertyChanged(nameof(Books));
            }

        }
    }
}
