using ApplicationCore.Entities.DietPlanAggregate;
using PublicApi.Util;
using System.Collections.Generic;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class CreateDietPlanRequest : BaseRequest 
    {
        public int UserId { get; set; }
        public List<DietPlanMealDto> DietPlanMeals { get; set; }

        public string Name { get; set; }
    }
}
