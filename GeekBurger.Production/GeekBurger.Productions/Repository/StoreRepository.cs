using GeekBurger.Productions.Model;
using System;
using System.Linq;

namespace GeekBurger.Productions.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private ProductionsContext _context { get; set; }

        public StoreRepository(ProductionsContext context)
        {
            _context = context;
        }
        
        public Store GetStoreByName(string name)
        {
            return _context.Stores.FirstOrDefault(store => store.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
