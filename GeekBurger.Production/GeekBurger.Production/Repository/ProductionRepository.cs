using System;
using System.Collections.Generic;
using System.Linq;
using GeekBurger.Production.Model;

namespace GeekBurger.Production.Repository
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly ProductionContext _productionContext;
        public ProductionRepository(ProductionContext context)
        {
            _productionContext = context;
        }

        public Model.Production GetProductionById(Guid id)
        {
            return _productionContext.Production.Where(Z => Z.ProductionId == id).FirstOrDefault();
        }

        public List<Model.Production> GetProductionByStore(Guid idStore)
        {
            return _productionContext.Production.Where(Z => Z.IdStore == idStore).ToList();
        }

        public List<Model.Production> ListProductions()
        {
            return _productionContext.Production.ToList();
        }
    }
}
