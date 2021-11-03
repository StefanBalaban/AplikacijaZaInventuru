using System.Collections.Generic;
using ApplicationCore.Entities.MealAggregate;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class CreateMealRequest : BaseRequest 
    {
        public int UserId { get; set; }
        public List<MealItem> Meals { get; set; } = new List<MealItem>();

        public string Name { get; set; }
    }
}