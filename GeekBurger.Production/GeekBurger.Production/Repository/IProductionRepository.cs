using System;
using System.Collections.Generic;


namespace GeekBurger.Production.Repository
{
    public interface IProductionRepository
    {
        List<Model.Production> ListProductions();
        Model.Production GetProductionById(Guid id);
        List<Model.Production> GetProductionByStore(Guid idStore);
    }
}
