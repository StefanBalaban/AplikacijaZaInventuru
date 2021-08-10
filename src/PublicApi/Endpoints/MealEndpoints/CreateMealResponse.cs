using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class CreateMealResponse : BaseResponse
    {
        public CreateMealResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateMealResponse()
        {
        }

        public MealDto Meal { get; set; }
    }
}