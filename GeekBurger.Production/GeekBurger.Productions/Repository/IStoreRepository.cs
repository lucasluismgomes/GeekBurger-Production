using GeekBurger.Productions.Model;

namespace GeekBurger.Productions.Repository
{
    public interface IStoreRepository
    {
        Store GetStoreByName(string name);
    }
}
