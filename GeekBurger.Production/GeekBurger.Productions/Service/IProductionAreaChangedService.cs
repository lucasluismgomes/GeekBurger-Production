using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Production.Service
{
    public interface IProductionAreaChangedService
    {
        void SendMessagesAsync();
        void AddToMessageList(IEnumerable<EntityEntry<Model.ProductionArea>> changes);
    }
}
