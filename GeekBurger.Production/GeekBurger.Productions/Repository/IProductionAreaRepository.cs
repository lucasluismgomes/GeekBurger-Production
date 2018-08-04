using GeekBurger.Productions.Model;
using System;
using System.Collections.Generic;

namespace GeekBurger.Productions.Repository
{
    public interface IProductionAreaRepository
    {
        List<ProductionArea> ListProductions();
        ProductionArea GetProductionById(Guid id);
        IEnumerable<ProductionArea> GetProductionByStoreName(string storeName);
    }
}
