using System.Collections.Generic;
using ApplicationCore.Entities.MealAggregate;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class MealDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        public List<MealItem> Meals { get; set; }

        public string Name { get; set; }
    }
}