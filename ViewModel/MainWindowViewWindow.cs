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
        
        private string _LibraryName;
        public string LibraryName
        {
            get { return _LibraryName; }
            set
            {
                _LibraryName = value;
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
            // 
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
            
            // the attributes needed to make a new book are book name, author name and the Id of the lib you want to add the book 
            // we need the currently logged in user 

            using (var userDb = new AuthDb())
            {
                var user = userDb.users.FirstOrDefault(u => u.IsLoggedIn);
                // now we need to get the id of the library they what to add the book too 
                
                // the below implementation is adding to a specific library 

                using (var libDb = new Libs())
                {
                    string libId = "-1";
                    
                    var x = libDb.Libraries;

                    foreach (var lib in libDb.Libraries)
                    {
                        if (lib.Name.Equals(LibraryName) && lib.CreatorId == user.Id.ToString())
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
                        Book book = new Book(NameOfBook, AutherLastName, libId); // this libid is the lib id of the library we want to add the book to
                        
                        var allbooks = bookDb.Books;
                        foreach (var b in allbooks)
                        {
                            if(b.Title.Equals(book.Title) && b.Author.Equals(book.Author) && b.LibraryID == book.LibraryID)
                            {
                                MessageBox.Show("This book already exists");
                                break;
                            }

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
            using (var userDb = new AuthDb())
            {
                var user = userDb.users.FirstOrDefault(u => u.IsLoggedIn);
                
                using (var libDb = new Libs())
                {
                    string libId = "-1";
                    
                    foreach (var lib in libDb.Libraries)
                    {
                        if (lib.Name.Equals(LibraryName) && lib.CreatorId == user.Id.ToString())
                        {
                            libId = lib.Id.ToString();
                            break; 
                        }
                    }

                    using (var bookDb = new LibDataBase())
                    {
                        if(string.IsNullOrWhiteSpace(NameOfBook) || string.IsNullOrWhiteSpace(AutherLastName))
                        {
                            MessageBox.Show("Cannot Remove: Missing details");
                            return;
                        }
                        
                        if (libId == "-1")
                        {
                            MessageBox.Show("Inputted library doesn't exist");
                            return;
                        }

                        // Find the ACTUAL book tracked by the database
                        var bookToRemove = bookDb.Books.FirstOrDefault(b => 
                            b.Title.Equals(NameOfBook) && 
                            b.Author.Equals(AutherLastName) && 
                            b.LibraryID == libId);

                        if (bookToRemove != null)
                        {
                            bookDb.Books.Remove(bookToRemove);
                            bookDb.SaveChanges();
                            MessageBox.Show("Book Removed");
                        }
                        else
                        {
                            MessageBox.Show("Book not found in the database");
                        }
                    }
                }
            }
        }
    }
}
