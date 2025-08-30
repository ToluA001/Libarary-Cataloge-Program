using Microsoft.EntityFrameworkCore;

namespace Libarary_Cataloge_Program.Data;
using Libarary_Cataloge_Program.Model;

public class Libs:DbContext
{
    public DbSet<Library> Libraries { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=C:\\Users\\Tolu\\Desktop\\PROJ\\Libarary Cataloge Program\\Data\\Libs.db");

    public Libs()
    {
        
    }
    
}