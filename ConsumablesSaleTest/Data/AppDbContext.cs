using ConsumablesSaleTest.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConsumablesSaleTest.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<TypeProd> Types { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
