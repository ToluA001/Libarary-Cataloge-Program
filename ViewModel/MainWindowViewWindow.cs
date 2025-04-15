using Libarary_Cataloge_Program.Model;
using Libarary_Cataloge_Program.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Libarary_Cataloge_Program.Views;

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
            using(var db = new LibDataBase())
            {
                Book book = db.GetBookByTitleAndAuthor(NameOfBook, AutherLastName);

                if(book == null)
                {
                    MessageBox.Show("This Book doesn't exist");
                }
                else
                {
                    book.Status = false;
                }
                    db.SaveChanges();
            }
        }

        private void In()
        {

            using (var db = new LibDataBase())
            {
                Book book = db.GetBookByTitleAndAuthor(NameOfBook, AutherLastName);

                if (book == null)
                {
                    MessageBox.Show("This Book doesn't exist");
                }
                else
                {
                    book.Status = true;
                }
                db.SaveChanges();
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
                Book book = new Book(NameOfBook, AutherLastName);

                using (var db = new LibDataBase())
                {
                    if(db.DoesBookExist(book) == true)
                    {
                        MessageBox.Show("This book already exists");
                    }
                    else
                    {
                        db.Add(book);
                    }
                    db.SaveChanges();
                }
            }

        }

        private void Check()
        {
            WSOB = repository.GetBookByTitleAndAuthor(NameOfBook,AutherLastName);
            
            
        }

        private void Delete()
        {
            using (var db = new LibDataBase())
            {
                Book book = db.GetBookByTitleAndAuthor(NameOfBook, AutherLastName);
                if (book == null)
                {
                    MessageBox.Show("This book doesn't exist");
                }
                else
                {
                    db.Books.Remove(book);
                }
                db.SaveChanges();
            }
        }
    }
}
