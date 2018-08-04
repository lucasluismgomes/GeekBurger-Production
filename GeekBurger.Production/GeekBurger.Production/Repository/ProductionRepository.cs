using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Production.Repository
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly ProductionContext _productionContext;
        public ProductionRepository(ProductionContext context)
        {
            _productionContext = context;
        }

        public Production GetProductionById(Guid id)
        {
            return _productionContext.Production.Where(Z => Z.ProductionId == id).FirstOrDefault();
        }

        public List<Production> ListProductions()
        {
            return _productionContext.Production.ToList();
        }
    }
}
