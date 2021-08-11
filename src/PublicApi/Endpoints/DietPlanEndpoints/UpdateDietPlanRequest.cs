using ApplicationCore.Entities.DietPlanAggregate;
using PublicApi.Util;
using System.Collections.Generic;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class UpdateDietPlanRequest : BaseRequest
    {
        public int Id { get; set; }

        public List<DietPlanMeal> DietPlanMeals { get; set; }

        public string Name { get; set; }
    }
}
