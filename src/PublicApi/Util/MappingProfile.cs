using ApplicationCore.Entities;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using AutoMapper;
using PublicApi.Endpoints.DietPlanEndpoints;
using PublicApi.Endpoints.FoodProductEndpoints;
using PublicApi.Endpoints.FoodStockEndpoints;
using PublicApi.Endpoints.MealEndpoints;

namespace PublicApi.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FoodProduct, FoodProductDto>();
            CreateMap<FoodStock, FoodStockDto>();
            CreateMap<Meal, MealDto>();
            CreateMap<DietPlanMeal, DietPlanMealDto>();
            CreateMap<DietPlan, DietPlanDto>();
        }
    }
}