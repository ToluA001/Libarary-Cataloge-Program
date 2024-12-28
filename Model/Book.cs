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

        public bool Status { get; set; }
        public string Check { get; set; } 

        public DateTime DateCreated { get; set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            Status = true;
            ChangeStatus();
            //Check = "Checked in";
            DateCreated = DateTime.Now;
        }

        // Optional: You can override the ToString() method to provide a custom string representation of the book.
        public override string ToString()
        {
            return $"{Title} by {Author} status: {Check} Date created: {DateCreated}";
        }

        public void Checkout ()
        {
            Status = false;
            ChangeStatus();
        }

        public void Checkin()
        {
            Status = true;
            ChangeStatus();
        }

        public void ChangeStatus()
        {
            Check = Status ? "Checked in" : "Checked out";
        }
    }
}
