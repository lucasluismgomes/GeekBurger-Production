using System;
using System.Collections.Generic;
using System.Linq;
using GeekBurger.Productions.Model;

namespace GeekBurger.Productions.Repository
{
    public class ProductionAreaRepository : IProductionAreaRepository
    {
        private readonly ProductionsContext _productionContext;
        public ProductionAreaRepository(ProductionsContext context)
        {
            _productionContext = context;
        }

        public ProductionArea GetProductionById(Guid id)
        {
            return _productionContext.ProductionAreas.Where(x => 
                x.ProductionAreaId == id).FirstOrDefault();
        }

        public IEnumerable<ProductionArea> GetProductionByStoreName(string storeName)
        {
            return _productionContext.ProductionAreas.Where(x =>
                x.Store.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }

        public List<ProductionArea> ListProductions()
        {
            return _productionContext.ProductionAreas.ToList();
        }
    }
}
