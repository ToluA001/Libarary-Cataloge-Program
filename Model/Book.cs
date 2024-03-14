using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libarary_Cataloge_Program.Model
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public string Check { get; set; } 

        public Book(string title, string author, string status)
        {
            Title = title;
            Author = author;
            Check = status;
        }
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            Check = "Checked in";
        }

        // Optional: You can override the ToString() method to provide a custom string representation of the book.
        public override string ToString()
        {
            return $"{Title} by {Author} status: {Check}";
        }

        public void checkout ()
        {
            this.Check = "Checked out";
        }

        public void checkin()
        {
            this.Check = "Checked in";
        }
    }
}
