using System;
using System.Collections.Generic;


namespace GeekBurger.Production.Repository
{
    public interface IProductionRepository
    {
        List<Model.ProductionArea> ListProductions();
        Model.ProductionArea GetProductionById(Guid id);
        List<Model.ProductionArea> GetProductionByStore(Guid idStore);
    }
}
