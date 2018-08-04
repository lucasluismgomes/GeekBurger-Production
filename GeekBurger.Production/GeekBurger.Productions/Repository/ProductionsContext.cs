using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Production;
using GeekBurger.Production.Model;
using GeekBurger.Production.Extension;

namespace GeekBurger.Production.Repository
{
    public class ProductionsContext : DbContext
    {
        public ProductionsContext(DbContextOptions<ProductionsContext> options) : base(options) { }
        
        public DbSet<ProductionArea> Production { get; set; }
        public DbSet<Store> Store { get; set; }
    }
}
