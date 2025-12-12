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
            AddBook = new RelayCommand(Add);
            CheckOut = new RelayCommand(Out);
            CheckIn = new RelayCommand(In);
            StatusHistoryCheck = new RelayCommand(Check);
            DeleteBook = new RelayCommand(Delete);
            WipeLibrary = new RelayCommand(Wipe);
            SearchLibrary = new RelayCommand(Search);
        }

        public ICommand AddBook { get; }
        public ICommand CheckIn { get; }
        public ICommand CheckOut { get; }
        public ICommand StatusHistoryCheck { get; }
        public ICommand DeleteBook { get; }
        public ICommand WipeLibrary { get; }
        public ICommand SearchLibrary { get; }
        private void Search()
        {
            using(var db = new LibDataBase())
            {
                var allBooks = db.Books.ToList();

                if (NameOfBook != null)
                {

                    foreach (var book in allBooks)
                    {
                        if (book.Title.Equals(NameOfBook))
                        {
                            MessageBox.Show(book.ToString());
                        }
                        else
                        {
                            MessageBox.Show("This book doesn't exist");
                        }

                    }
                }else if(AutherLastName != null)
                {
                    foreach (var book in allBooks)
                    {
                        if (book.Author.Equals(AutherLastName))
                        {
                            MessageBox.Show(book.ToString());
                        }
                        else
                        {
                            MessageBox.Show("This book doesn't exist");
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Wipe of the contents of the library 
        /// </summary>
        private void Wipe()
        {
            // Th
            using(var db = new LibDataBase())
            {
                var allBooks = db.Books.ToList();
                db.Books.RemoveRange(allBooks);
                db.SaveChanges();
                MessageBox.Show("Library cleared");
            }
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
        {// This is not the final implimentation of this function 
            // TODO: add to a specific library
            
            // the attributes needed to make a new book are book name, author name and the Id of the lib you want to add the book 
            // we need the currently logged in user 

            using (var userDb = new AuthDb())
            {
                var user = userDb.users.FirstOrDefault(u => u.IsLoggedIn);
                // now we need to get the id of the library they what to add the book too 
                
                //for now assuming they only have one library
                using (var libDb = new Libs())
                {
                    string libId = "-1"; 

                    var x = libDb.Libraries;

                    foreach (var lib in libDb.Libraries)
                    {
                        if (lib.CreatorId == user.Id.ToString())
                        {
                            libId = lib.Id.ToString();
                            break; 
                        }
                    }

                    using (var bookDb = new LibDataBase())
                    {
                        if(string.IsNullOrWhiteSpace(NameOfBook).Equals(true) || string.IsNullOrWhiteSpace(AutherLastName).Equals(true))
                        {
                            MessageBox.Show("Cannot add");
                        }
                        if (libId == "-1")
                        {
                            MessageBox.Show("You don't have a library");
                        }
                        Book book = new Book(NameOfBook, AutherLastName, libId);
                        
                        if(bookDb.DoesBookExist(book) == true)
                        {
                            MessageBox.Show("This book already exists");
                        }
                        else
                        {
                            bookDb.Add(book);
                        }
                        bookDb.SaveChanges();
                    }
                }
            }
        }

        private void Check()
        {
            WSOB = repository.GetBookByTitleAndAuthor(NameOfBook,AutherLastName);
        }
        
        /// <summary>
        /// Delete a book from the library 
        /// </summary>
        private void Delete()
        {
            // TODO: delete from a specific library
            
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
