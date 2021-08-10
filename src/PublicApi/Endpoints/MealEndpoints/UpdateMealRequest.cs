using System.Collections.Generic;
using ApplicationCore.Entities.MealAggregate;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class UpdateMealRequest : BaseRequest
    {
        public int Id { get; set; }

        public List<MealItem> Meals { get; set; }

        public string Name { get; set; }
    }
}