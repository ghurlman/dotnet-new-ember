
using Microsoft.EntityFrameworkCore;

namespace dotnet_new_ember
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Person> People { get; set; }
  }
}