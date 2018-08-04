using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Production;

namespace GeekBurger.Production.Repository
{
    public class ProductionContext : DbContext
    {
        public ProductionContext(DbContextOptions<ProductionContext> options) : base(options) { }
        
        public DbSet<Model.Production> Production { get; set; }
        public DbSet<Model.Store> Store { get; set; }

    }
}
