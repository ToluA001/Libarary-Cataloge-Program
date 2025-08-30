using Libarary_Cataloge_Program.Model;
using Microsoft.EntityFrameworkCore;

namespace Libarary_Cataloge_Program.Data;

public class AuthDb:DbContext
{
    
    public DbSet<User> users {get; set;}
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=C:\\Users\\Tolu\\Desktop\\PROJ\\Libarary Cataloge Program\\Data\\User.db");
    
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