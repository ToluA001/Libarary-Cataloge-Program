using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libarary_Cataloge_Program.Model;

namespace Libarary_Cataloge_Program.Model
{
    public class BookRepository
    {
        //private List<Book> books;
        private static BookRepository instance;
        private List<Book> books;

        private BookRepository()
        {
            // Initialize an empty list to store books in memory
            books = new List<Book>();
            // Add some initial books for demonstration purposes
            Book x = new Book("book1", "auth1", "Checked in");
            Book y = new Book("book2", "auth2", "Checked in");
            books.Add(x);
            books.Add(y);
        }
        public static BookRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BookRepository();
                }
                return instance;
            }
        }

        public void AddBook(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");
            }

            // Check if a book with the same title and author already exists
            if (books.Any(b => b.Title == book.Title && b.Author == book.Author))
            {
                throw new InvalidOperationException($"Book with title '{book.Title}' and author '{book.Author}' already exists.");
            }

            books.Add(book);
        }

        public void UpdateBook(Book updatedBook)
        {
            if (updatedBook == null)
            {
                throw new ArgumentNullException(nameof(updatedBook), "Updated book cannot be null.");
            }

            // Find the existing book by title and author
            Book existingBook = books.FirstOrDefault(b => b.Title == updatedBook.Title && b.Author == updatedBook.Author);

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

        public void DeleteBook(string title, string author)
        {
            // Find the book by title and author
            Book bookToDelete = books.FirstOrDefault(b => b.Title == title && b.Author == author);

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
            }
            else
            {
                throw new InvalidOperationException($"Book with title '{title}' and author '{author}' not found.");
            }
        }

        public String GetAllBooks()
        {
            // Initialize an empty string to build the output
            StringBuilder sb = new StringBuilder();

            // Iterate through each book in the input list
            foreach (Book book in books)
            {
                // Create a string containing the book title and author, separated by "by"
                string bookNameAndAuthor = $"{book.Title} by {book.Author} Status: {book.Check}";

                // Append the formatted string with a newline character
                sb.AppendLine(bookNameAndAuthor);
            }

            // Return the final string containing all book information
            return sb.ToString();
        }

        public List<Book> GAB()
        {
            return books;
        }  

        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            // Find the book by title and author
            return books.FirstOrDefault(b => b.Title == title && b.Author == author);
        }

        public static bool CheckLib(Book x)
        {
            BookRepository repository = BookRepository.Instance;

            foreach (Book b in repository.GAB())
            {
                if ((b.Title).Equals(x.Title) && (b.Author).Equals(x.Author))
                {
                    return true;
                }
            }

            // If the loop finishes without finding a matching book, return false
            return false;
        }
    }
}
