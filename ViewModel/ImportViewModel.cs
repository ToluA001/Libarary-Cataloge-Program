using Libarary_Cataloge_Program.Data;
using Libarary_Cataloge_Program.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Libarary_Cataloge_Program.ViewModel
{
    class ImportViewModel:BaseViewModel
    {

        private string _FilePath;
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                _FilePath = value;
                OnPropertyChanged();
            }
        }


        public ImportViewModel() 
        {
            Import = new RelayCommand(Imprt);
        }
        public ICommand Import { get; }

        private void Imprt()
        {
            using (var db = new LibDataBase())
            {
                
                try
                {
                    string[] lines = File.ReadAllLines(FilePath);
                    foreach (var line in lines)
                    {
                        string[] strings = line.Split(',');
                        if (strings.Length == 2)
                        {
                            string bkName = strings[0];
                            string bkAuthor = strings[1];

                            Book book = new Book(bkName, bkAuthor);
                            if (db.DoesBookExist(book) == true) 
                            {
                                MessageBox.Show("This book is already in the library");
                            }
                            else
                            {
                                db.Add(book);
                            }
                            db.SaveChanges();
                        }
                    }
                    MessageBox.Show("Books imported succesfully!");
                }
                catch (Exception ex) 
                {
                    MessageBox.Show($"Error importing books: {ex.Message}!");
                }
            }
        }
    }
}
