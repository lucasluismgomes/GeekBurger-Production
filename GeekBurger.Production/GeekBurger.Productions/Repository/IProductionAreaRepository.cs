using GeekBurger.Productions.Model;
using System;
using System.Collections.Generic;

namespace GeekBurger.Productions.Repository
{
    public interface IProductionAreaRepository
    {
        bool Add(ProductionArea productionArea);
        List<ProductionArea> ListProductionAreas();
        ProductionArea GetProductionAreaById(Guid id);
        bool Update(ProductionArea productionArea);
        void Remove(ProductionArea productionArea);
        IEnumerable<ProductionArea> GetAvailableProductionAreaByStoreName(string storeName);
        void Save();
    }
}
