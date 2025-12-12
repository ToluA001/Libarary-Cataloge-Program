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
        public string Check { get; set; } // as in checked in or checked out, is purly determined by the value of status
        
        public string LibraryID { get; set; } //the ID of the Library the book belongs to 

        public DateTime DateCreated { get; set; }

        public DateTime StatusChangeDate { get; set; }

        // dictionary of the date the status is changed and what the status it is changing to
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
        
        public Book(string title, string author, string libraryID)
        {
            // ksnfljkhndfkgndfklngh
            Title = title;
            Author = author;
            LibraryID = libraryID; // id of the library this book belongs too 
            ChangeStatus();
            DateCreated = DateTime.Now;
            StatusChangeDate = DateTime.Now;
        }

        // You can override the ToString() method to provide a custom string representation of the book.
        public override string ToString()
        {
            return $"{Title} by {Author} status: {Check} Date Added: {DateCreated}";
        }

        /// <summary>
        /// Change the status of the book to false, checked out.
        /// </summary>
        public void Checkout ()
        {
            Status = false;
            ChangeStatus();
            StatusChangeDate = DateTime.Now;
            StatusHistory.Add(StatusChangeDate, Status);

        }

        /// <summary>
        /// Change the status of the book to true, checked in. 
        /// </summary>
        public void Checkin()
        {
            Status = true;
            ChangeStatus();
            StatusChangeDate = DateTime.Now;
            StatusHistory.Add(StatusChangeDate, Status);

        }

        /// <summary>
        ///  Change the status to the opposite of its current status.
        /// </summary>
        public void ChangeStatus()
        {
            Check = Status ? "Checked in" : "Checked out";

        }
    }
}
