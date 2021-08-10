using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class UpdateMealResponse : BaseResponse
    {
        public UpdateMealResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateMealResponse()
        {
        }

        public MealDto Meal { get; set; }
    }
}