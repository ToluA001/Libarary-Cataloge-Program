using Libarary_Cataloge_Program.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Libarary_Cataloge_Program.ViewModel
{
    class ImportViewModel:BaseViewModel
    {
        BookRepository repository = BookRepository.Instance;


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
            BookRepository.ImportBooksFromCVS(FilePath);
            MessageBox.Show(repository.GetAllBooks());
        }
    }
}
