using Libarary_Cataloge_Program.Model;
using Libarary_Cataloge_Program.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
    
    public string _currentLoggedInUser;
    public string CurrentLoggedInUser
    {
        get { return _currentLoggedInUser; }
        set
        {
            _currentLoggedInUser = value;
            OnPropertyChanged();
        }
    }
    
    public UserViewModel()
    {
        String lastLoggedIn = Username;
        SignUp = new RelayCommand(signup);
        LogIn = new RelayCommand(login);
        LogOut = new RelayCommand(logout);
        NewLib = new RelayCommand(create);
    }
    public ICommand LogOut { get; }
    public ICommand NewLib { get; }
    public ICommand SignUp { get; }
    public ICommand LogIn { get; }

    private void logout()
    {
        // Log out any currently logged-in user(s) in the DB and clear local state
        using (var db = new AuthDb())
        {
            var loggedInUsers = db.users.Where(u => u.IsLoggedIn).ToList();

            if (loggedInUsers.Count == 0)
            {
                MessageBox.Show("No one is logged in");
                return;
            }

            foreach (var u in loggedInUsers)
            {
                u.IsLoggedIn = false;
            }

            db.SaveChanges();
        }

        // Clear local/session fields so UI reflects logged-out state
        CurrentLoggedInUser = string.Empty;
        Username = string.Empty;
        Password = string.Empty;

        MessageBox.Show("You have been logged out");
    }
    private void create()
    {
        // create a new library
        
        MessageBox.Show("You are attempting to create a new library");
        
        using (var db = new AuthDb())
        {
            var user = db.users.FirstOrDefault(u => u.Username.ToLower() == Username.ToLower());
            MessageBox.Show(user?.Username);
        }

        using (var libDb = new Libs())
        {
            using (var userDb = new AuthDb())
            {
                // I need to get the ID of the user that is logged in
                var user = userDb.users.FirstOrDefault(u => u.Username.ToLower() == Username.ToLower());
                int UserId = user.Id; // this is the ID of the user that is logged in
            }
            
        }
    }
    private void login()
    {
        using (var db = new AuthDb())
        {
            var user = db.users.FirstOrDefault(u => u.Username.ToLower() == Username.ToLower()); // searches for if the username is in the db at all 
            if (user != null && user.VerifyPassword(Password))
            {
                CurrentLoggedInUser = user.Username;
                user.IsLoggedIn = true;
                MessageBox.Show(CurrentLoggedInUser);
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
                CurrentLoggedInUser = x.Username;
                MessageBox.Show(CurrentLoggedInUser);
                db.SaveChanges();
                MessageBox.Show("Successfully signed up");
            }
        }
    }
    
    
}