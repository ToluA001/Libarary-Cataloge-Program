using System.IO;
using Libarary_Cataloge_Program.Model;
using Microsoft.EntityFrameworkCore;

namespace Libarary_Cataloge_Program.Data;

public class AuthDb:DbContext
{
    
    public DbSet<User> users {get; set;}

    // Changing this to be a relative path
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string userDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "User.db");

        Directory.CreateDirectory(Path.GetDirectoryName(userDbPath));
        
        options.UseSqlite($"Data Source={userDbPath}");
    }
    
    public AuthDb()
    {
        //Database.EnsureCreated();
    }

    public bool DoesUserExist(string name)
    {
        if (users.FirstOrDefault(u => u.Username.ToLower() == name.ToLower()) != null)
        {
            return true; // meaning the user does already exist
        }
        else
        {
            return false;
        }
    }
    
}