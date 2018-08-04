using AutoMapper;
using GeekBurger.Productions.Contract;
using GeekBurger.Productions.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GeekBurger.Productions.Helper
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ProductionAreaToUpsert, ProductionArea>().AfterMap<MatchStoreFromRepository>();
            CreateMap<ProductionArea, ProductionAreaToUpsert>();
            CreateMap<ProductionArea, ProductionAreaToGet>();
            CreateMap<EntityEntry<ProductionArea>, ProductionAreaChangedMessage>()
            .ForMember(dest => dest.ProductionArea, opt => opt.MapFrom(src => src.Entity));
        }
    }
}
