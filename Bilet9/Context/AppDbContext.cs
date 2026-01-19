using System.Reflection;
using Bilet9.Models;
using Microsoft.EntityFrameworkCore;

namespace Bilet9.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }    
    public DbSet<Chef> Chefs { get; set; }
    public DbSet<Position> Positions { get; set; }
   
}
