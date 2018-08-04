using System;
using System.Collections.Generic;
using System.Linq;
using GeekBurger.Production.Model;

namespace GeekBurger.Production.Repository
{
    public class ProductionRepository : IProductionRepository
    {
        private readonly ProductionsContext _productionContext;
        public ProductionRepository(ProductionsContext context)
        {
            _productionContext = context;
        }

        public ProductionArea GetProductionById(Guid id)
        {
            return _productionContext.Production.Where(Z => Z.ProductionAreaId == id).FirstOrDefault();
        }

        public List<ProductionArea> GetProductionByStore(Guid idStore)
        {
            return _productionContext.Production.Where(Z => Z.StoreId == idStore).ToList();
        }

        public List<ProductionArea> ListProductions()
        {
            return _productionContext.Production.ToList();
        }
    }
}
