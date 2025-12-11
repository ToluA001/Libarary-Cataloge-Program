using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Libarary_Cataloge_Program.Model;

namespace Libarary_Cataloge_Program.Model
{
    public class BookRepository
    {
        //private List<Book> books;
        private static BookRepository LibraryInstance;
        private List<Book> Library;

        private BookRepository()
        {
            // Initialize an empty list to store books in memory
            Library = new List<Book>();

        }
        public static BookRepository Instance
        {
            get
            {
                if (LibraryInstance == null)
                {
                    LibraryInstance = new BookRepository();
                }
                return LibraryInstance;
            }
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            // Check if a book with the same title and author already exists
            if (Library.Any(b => b.Title == book.Title && b.Author == book.Author))
            {
                //For some reason this crashes the proram 12/28/24 9:52am
                throw new InvalidOperationException($"Book with title '{book.Title}' and author '{book.Author}' already exists.");
            }

            Library.Add(book);
        }

        public void UpdateBook(Book updatedBook)
        {
            if (updatedBook == null)
            {
                throw new ArgumentNullException(nameof(updatedBook), "Updated book cannot be null.");
            }

            // Find the existing book by title and author
            Book existingBook = Library.FirstOrDefault(b => b.Title == updatedBook.Title && b.Author == updatedBook.Author);

            if (existingBook != null)
            {
                // Update the properties of the existing book
                existingBook.Title = updatedBook.Title;
                existingBook.Author = updatedBook.Author;
            }
            else
            {
                throw new InvalidOperationException($"Book with title '{updatedBook.Title}' and author '{updatedBook.Author}' not found.");
            }
        }

        public void DeleteBook(Book bookToDelete)
        {
            Library.Remove(bookToDelete);
        }

        public override string ToString()
        {
            // Initialize an empty string to build the output
            StringBuilder sb = new StringBuilder();

            // Iterate through each book in the input list
            foreach (Book book in Library)
            {
                // Create a string containing the book title and author, separated by "by"
                string bookNameAndAuthor = $"{book.Title} by {book.Author} Status: {book.Check} Date created: {book.DateCreated}";

                // Append the formatted string with a newline character
                sb.AppendLine(bookNameAndAuthor);
            }

            // Return the final string containing all book information
            return sb.ToString();
        }

        public List<Book> GetAllBooks()
        {
            return Library;
        }  

        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            // Find a book by title and author
            return Library.FirstOrDefault(b => b.Title == title && b.Author == author);
        }


        public static bool CheckLib(Book x)
        { //Checks to see if a book exists 
            BookRepository repository = BookRepository.Instance;

            foreach (Book b in repository.GetAllBooks())
            {
                if ((b.Title).Equals(x.Title) && (b.Author).Equals(x.Author))
                {
                    return true;
                }
            }
            return false;
        }

        public static void ImportBooksFromCVS(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] fields = line.Split(',');

                    if (fields.Length == 2)
                    {
                        // Extract book name and author from the CSV fields
                        string bookName = fields[0].Trim();
                        string authorName = fields[1].Trim();

                        // Add the book to the library
                        BookRepository.Instance.AddBook(new Book(bookName, authorName));
                    }
                }
                // Notify the user that books have been imported successfully
                MessageBox.Show("Books imported successfully!");
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error importing books: {ex.Message}");
            }
        }
    }
}
