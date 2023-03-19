using ConsumablesSaleTest.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsumablesSaleTest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<Type> Types { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
