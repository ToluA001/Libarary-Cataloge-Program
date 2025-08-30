using Microsoft.EntityFrameworkCore;
using Libarary_Cataloge_Program.Model;

namespace Libarary_Cataloge_Program.Data;

public class ULDB: DbContext
{
    public DbSet<UserLibrary> UserLibrary { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=C:\\Users\\Tolu\\Desktop\\PROJ\\Libarary Cataloge Program\\Data\\UL.db");

    public ULDB()
    {
        
    }
}