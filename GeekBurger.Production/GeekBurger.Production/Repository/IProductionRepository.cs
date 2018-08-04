using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Repository
{
    public interface IProductionRepository
    {
        List<Production> ListProductions();
        Production GetProductionById(Guid id);
    }
}
