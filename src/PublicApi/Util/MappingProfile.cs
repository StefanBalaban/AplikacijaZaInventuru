using ApplicationCore.Entities;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Entities.NotificationAggregate;
using ApplicationCore.Entities.UserAggregate;
using AutoMapper;
using PublicApi.Endpoints.DietPlanEndpoints;
using PublicApi.Endpoints.DietPlanPeriodEndpoints;
using PublicApi.Endpoints.FoodProductEndpoints;
using PublicApi.Endpoints.FoodStockEndpoints;
using PublicApi.Endpoints.MealEndpoints;
using PublicApi.Endpoints.NotificationRuleEndpoints;
using PublicApi.Endpoints.UserEndpoints;
using PublicApi.Endpoints.UserSubscriptionEndpoints;
using PublicApi.Endpoints.UserWeightEvidentionEndpoints;

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
            CreateMap<DietPlanPeriod, DietPlanPeriodDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserContactInfo, UserContactInfoDto>();
            CreateMap<NotificationRule, NotificationRuleDto>();
            CreateMap<NotificationRuleUserContactInfos, NotificationRuleUserContactInfosDto>();
            CreateMap<UserWeightEvidention, UserWeightEvidentionDto>();
            CreateMap<UserSubscription, UserSubscriptionDto>();
        }
    }
}