using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libarary_Cataloge_Program.Model
{
    public class Book
    {
        public int Id { get; set; } // EF will assume this is the key
        public string Title { get; set; }
        public string Author { get; set; }

        public bool Status { get; set; }
        public string Check { get; set; } 

        public DateTime DateCreated { get; set; }

        public DateTime StatusChangeDate { get; set; }

        // dictionary of the date the status is changed and what the status it changing to
        Dictionary<DateTime, bool> StatusHistory = new Dictionary<DateTime, bool>();

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            Status = true;
            ChangeStatus();
            //Check = "Checked in";
            DateCreated = DateTime.Now;
            StatusChangeDate = DateTime.Now;

        }

        // You can override the ToString() method to provide a custom string representation of the book.
        public override string ToString()
        {
            return $"{Title} by {Author} status: {Check} Date Added: {DateCreated}";
        }

        public void Checkout ()
        {
            Status = false;
            ChangeStatus();
            StatusChangeDate = DateTime.Now;
            StatusHistory.Add(StatusChangeDate, Status);

        }

        public void Checkin()
        {
            Status = true;
            ChangeStatus();
            StatusChangeDate = DateTime.Now;
            StatusHistory.Add(StatusChangeDate, Status);

        }

        public void ChangeStatus()
        {
            Check = Status ? "Checked in" : "Checked out";

        }
    }
}
