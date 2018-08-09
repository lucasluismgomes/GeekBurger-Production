using GeekBurger.Productions.Model;
using GeekBurger.Productions.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Productions.Repository
{
    public class ProductionAreaRepository : IProductionAreaRepository
    {
        private readonly ProductionsContext _context;
        private readonly IProductionAreaChangedService _productionAreaChangedService;

        public ProductionAreaRepository(ProductionsContext context, IProductionAreaChangedService productionAreaChangedService)
        {
            _context = context;
            _productionAreaChangedService = productionAreaChangedService;
        }

        public bool Add(ProductionArea productionArea)
        {
            productionArea.ProductionAreaId = Guid.NewGuid();
            _context.ProductionAreas.Add(productionArea);
            return true;
        }
        
        public ProductionArea GetProductionAreaById(Guid id)
        {
            return _context.ProductionAreas.Where(x => 
                x.ProductionAreaId == id).FirstOrDefault();
        }

        public IEnumerable<ProductionArea> GetAvailableProductionAreaByStoreName(string storeName)
        {
            return _context.ProductionAreas.Where(x => x.Store.Name.Equals(storeName, StringComparison.InvariantCultureIgnoreCase)
                && x.On).ToList();
        }

        public List<ProductionArea> ListProductionAreas()
        {
            return _context.ProductionAreas.ToList();
        }

        public void Remove(ProductionArea productionArea)
        {
            _context.Remove(productionArea);
        }

        public void Save()
        {
            _productionAreaChangedService
                .AddToMessageList(_context.ChangeTracker.Entries<ProductionArea>());

            _context.SaveChanges();

            _productionAreaChangedService.SendMessagesAsync();
        }

        public bool Update(ProductionArea productionArea)
        {
            if (_context.ProductionAreas.FirstOrDefault(x => x.ProductionAreaId == productionArea.ProductionAreaId) == null)
                return false;

            _context.ProductionAreas.Update(productionArea);

            return true;
        }
    }
}
