using GeekBurger.Productions.Model;
using GeekBurger.Productions.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Productions.Repository
{
    public class ProductionAreaRepository : IProductionAreaRepository
    {
        private readonly ProductionsContext _productionContext;
        private readonly IProductionAreaChangedService _productionAreaChangedService;

        public ProductionAreaRepository(ProductionsContext context, IProductionAreaChangedService productionAreaChangedService)
        {
            _productionContext = context;
            _productionAreaChangedService = productionAreaChangedService;
        }

        public bool Add(ProductionArea productionArea)
        {
            productionArea.ProductionAreaId = Guid.NewGuid();
            _productionContext.ProductionAreas.Add(productionArea);
            return true;
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

        public void Save()
        {
            _productionAreaChangedService
                .AddToMessageList(_productionContext.ChangeTracker.Entries<ProductionArea>());

            _productionContext.SaveChanges();

            _productionAreaChangedService.SendMessagesAsync();
        }
    }
}
