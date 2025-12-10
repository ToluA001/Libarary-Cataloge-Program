using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Libarary_Cataloge_Program.Model;
using Microsoft.EntityFrameworkCore;

// THIS IS A DATABASE THAT CONTAINS BOOKS 
namespace Libarary_Cataloge_Program.Data
{
    public class LibDataBase : DbContext
    {
        // this database is a database of books 
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            string libDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "library.db");
            
            Directory.CreateDirectory(Path.GetDirectoryName(libDbPath));
            
            options.UseSqlite($"Data Source={libDbPath}");
        }
            

        public LibDataBase()
        {
            Database.EnsureCreated(); // ← this line creates the DB & tables if they don’t exist
        }

        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            
            return Books.FirstOrDefault(b =>
                b.Title.ToLower() == title.ToLower() &&
                b.Author.ToLower() == author.ToLower());

        }
        public bool DoesBookExist(Book book)
        {
            using (var db = new LibDataBase()) 
            { 
                if(db.GetBookByTitleAndAuthor(book.Title, book.Author) != null)
                {
                    return true;
                }
                else
                {
                    return false; 
                }
            }

        }
    }
}
