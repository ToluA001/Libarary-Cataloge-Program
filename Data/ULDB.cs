using Microsoft.EntityFrameworkCore;
using Libarary_Cataloge_Program.Model;
using System.IO;

namespace Libarary_Cataloge_Program.Data;

public class ULDB: DbContext
{
    // this database is a database of users and their libraries
    public DbSet<UserLibrary> UserLibrary { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string ulDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "UserLibrary.db");
        
        Directory.CreateDirectory(Path.GetDirectoryName(ulDbPath));
        
        options.UseSqlite($"Data Source={ulDbPath}");
    }
        

    public ULDB()
    {
        
    }
}