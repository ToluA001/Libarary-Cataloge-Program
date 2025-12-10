using System.IO;
using Microsoft.EntityFrameworkCore;

// THIS IS A DATABASE THAT CONTAINS LIBS
namespace Libarary_Cataloge_Program.Data;
using Libarary_Cataloge_Program.Model;

public class Libs:DbContext
{
    // this database is a database of libraries
    public DbSet<Library> Libraries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string libDb = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "libs.db");
        
        Directory.CreateDirectory(Path.GetDirectoryName(libDb));
        
        options.UseSqlite($"Data Source={libDb}");
    }

    public Libs()
    {
        Database.EnsureCreated();
    }
    
}