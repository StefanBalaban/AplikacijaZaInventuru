using AutoMapper;
using ApplicationCore.Entities;
using PublicApi.Util.FoodProductEndpoints;

namespace PublicApi.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoodProduct, FoodProductDto>();
        }
    }
}