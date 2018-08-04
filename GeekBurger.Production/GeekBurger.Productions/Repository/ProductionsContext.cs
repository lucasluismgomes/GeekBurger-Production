using GeekBurger.Productions.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekBurger.Productions.Repository
{
    public class ProductionsContext : DbContext
    {
        public ProductionsContext(DbContextOptions<ProductionsContext> options) : base(options) { }
        public DbSet<ProductionArea> ProductionAreas { get; set; }
        public DbSet<Store> Stores { get; set; }
    }
}
