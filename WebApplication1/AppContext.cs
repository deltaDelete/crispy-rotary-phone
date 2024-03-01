using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1;

public class AppContext : DbContext
{
    public static readonly string
        ConnectionString = "server=10.10.1.24;uid=user_01;password=user01pro;database=pro1_2;";

    public DbSet<City> Cities { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
    }
}

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CityId { get; set; }
    public string Name { get; set; } = string.Empty;
}