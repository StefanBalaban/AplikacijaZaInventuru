using ApplicationCore.Entities.DietPlanAggregate;
using System.Collections.Generic;

namespace PublicApi.Endpoints.DietPlanEndpoints
{
    public class DietPlanDto
    {
        public int Id { get; set; }

        public List<DietPlanMealDto> DietPlanMeals { get; set; } = new List<DietPlanMealDto>();

        public string Name { get; set; }

    }
}
