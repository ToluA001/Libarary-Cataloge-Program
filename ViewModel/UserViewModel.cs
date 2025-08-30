using Libarary_Cataloge_Program.Model;
using Libarary_Cataloge_Program.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Libarary_Cataloge_Program.Views;

namespace Libarary_Cataloge_Program.ViewModel;

class UserViewModel:BaseViewModel
{
    private string _libname;

    public string LibName
    {
        get { return _libname; }
        set { _libname = value; OnPropertyChanged(); }
    }
    
    private string _username;

    public string Username
    {
        get { return _username; }
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }
    
    private string _password;

    public string Password
    {
        get { return _password; }
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
    public UserViewModel()
    {
        SignUp = new RelayCommand(signup);
        LogIn = new RelayCommand(login);
        NewLib = new RelayCommand(create);
    }
    
    public ICommand NewLib { get; }
    public ICommand SignUp { get; }
    public ICommand LogIn { get; }

    private void create()
    {
        MessageBox.Show(Username);
        using (var db = new AuthDb())
        {
            var user = db.users.FirstOrDefault(u => u.Username.ToLower() == Username.ToLower());
            MessageBox.Show(user?.Username);
        }
    }
    private void login()
    {
        using (var db = new AuthDb())
        {
            var user = db.users.FirstOrDefault(u => u.Username.ToLower() == Username.ToLower()); // searches for if the username is in the db at all 
            if (user != null && user.VerifyPassword(Password))
            {
                user.IsLoggedIn = true;
                db.SaveChanges();
                MessageBox.Show("Login Success!");
            }
            else
            {
                MessageBox.Show("Login Failed!");
            }
        }
    }

    private void signup()
    {
        User x = new User(Username);
        x.SetPassword(Password);
        using (var db = new AuthDb())
        {
            if (db.DoesUserExist(Username)) // this username is already in the db 
            {
                MessageBox.Show("Username already in use");
            }
            else
            {
                db.Add(x);
                x.IsLoggedIn = true;
                db.SaveChanges();
                MessageBox.Show("Successfully signed up");
            }
        }
    }
    
    
}