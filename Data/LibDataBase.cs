using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libarary_Cataloge_Program.Model;
using Microsoft.EntityFrameworkCore;

namespace Libarary_Cataloge_Program.Data
{
    public class LibDataBase : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=C:\\Users\\Tolu\\Desktop\\PROJ\\Libarary Cataloge Program\\Data\\library.db");

        public LibDataBase()
        {
            Database.EnsureCreated(); // ← this line creates the DB & tables if they don’t exist
        }

        public Book GetBookByTitleAndAuthor(string title, string author)
        {
            return Books.FirstOrDefault(b =>
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                b.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
        }

    }
}
