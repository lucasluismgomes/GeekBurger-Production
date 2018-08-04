using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Production;
using GeekBurger.Production.Model;

namespace GeekBurger.Production.Repository
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(DbContextOptions<ProductionContext> options) : base(options) { }
        
        public DbSet<ProductionArea> Production { get; set; }
        public DbSet<Store> Store { get; set; }

    }
}
