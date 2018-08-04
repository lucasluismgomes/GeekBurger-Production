using AutoMapper;
using GeekBurger.Productions.Contract;
using GeekBurger.Productions.Model;
using GeekBurger.Productions.Repository;

namespace GeekBurger.Productions.Helper
{
    public class MatchStoreFromRepository : IMappingAction<ProductionAreaToUpsert, ProductionArea>
    {
        private IStoreRepository _storeRepository;
        public MatchStoreFromRepository(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public void Process(ProductionAreaToUpsert source, ProductionArea destination)
        {
            var store = _storeRepository.GetStoreByName(source.StoreName);

            if (store != null)
                destination.StoreId = store.StoreId;
        }
    }
}
