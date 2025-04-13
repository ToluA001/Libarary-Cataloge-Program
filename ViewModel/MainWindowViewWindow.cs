using Libarary_Cataloge_Program.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Libarary_Cataloge_Program.ViewModel
{
    class MainWindowViewWindow:BaseViewModel
    {
        BookRepository repository = BookRepository.Instance;
        public Book WSOB;

        private string _NameOfBook;
        public string NameOfBook
        {
            get { return _NameOfBook; }
            set
            {
                _NameOfBook = value;
                OnPropertyChanged();
            }
        }

        private string _AuthLastName;
        public string AutherLastName
        {
            get { return _AuthLastName; }
            set
            {
                _AuthLastName = value;
                OnPropertyChanged();
            }
        }
        public MainWindowViewWindow( ) 
        {
            ViewAllBooks = new RelayCommand(See);
            AddBook = new RelayCommand(Add);
            CheckOut = new RelayCommand(Out);
            CheckIn = new RelayCommand(In);
            OpenImportWin = new RelayCommand(Open);
            StatusHistoryCheck = new RelayCommand(Check);
            DeleteBook = new RelayCommand(Delete);
        }

        public ICommand ViewAllBooks { get; }
        public ICommand AddBook { get; }
        public ICommand CheckIn { get; }
        public ICommand CheckOut { get; }
        public ICommand OpenImportWin { get; }
        public ICommand StatusHistoryCheck { get; }
        public ICommand DeleteBook { get; }

        private void Open()
        {
            ImportFile importFile = new ImportFile();
            importFile.Show();
        }
        private void See()
        {
            Window1 window1 = new Window1();
            window1.Show();
        }

        private void Out() 
        {

            if (BookRepository.CheckLib(new Book(NameOfBook, AutherLastName)) == true)
            {
                Book currbook = repository.GetBookByTitleAndAuthor(NameOfBook, AutherLastName);
                currbook.Checkout();
                //MessageBox.Show(repository.ToString());
            }
            else if (BookRepository.CheckLib(new Book(NameOfBook, AutherLastName)) == false)
            {
                MessageBox.Show("That book does not exist");
            }
        }

        private void In()
        {

            if(BookRepository.CheckLib(new Book(NameOfBook, AutherLastName)) == true)
            {
                Book currbook = repository.GetBookByTitleAndAuthor(NameOfBook, AutherLastName);
                currbook.Checkin();
                //MessageBox.Show(repository.ToString());
            }else if(BookRepository.CheckLib(new Book(NameOfBook, AutherLastName)) == false)
            {
                MessageBox.Show("That book does not exist");
            }

        }

        private void Add()
        {
            if(string.IsNullOrWhiteSpace(NameOfBook).Equals(true) || string.IsNullOrWhiteSpace(AutherLastName).Equals(true))
            {
                MessageBox.Show("Cannot add");
            }
            else
            {
                Book x = new Book(NameOfBook, AutherLastName);

                repository.AddBook(x);
                //MessageBox.Show(repository.ToString());
            }
        }

        private void Check()
        {
            WSOB = repository.GetBookByTitleAndAuthor(NameOfBook,AutherLastName);
            
            
        }

        private void Delete()
        {
            if (BookRepository.CheckLib(new Book(NameOfBook, AutherLastName)) == true)
            {
                Book currbook = repository.GetBookByTitleAndAuthor(NameOfBook, AutherLastName);

                repository.DeleteBook(currbook);
            }
            else
            {
                MessageBox.Show("This book doesn't exist");
            }
        }
    }
}
