using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.MealEndpoints
{
    public class GetByIdMealResponse : BaseResponse
    {
        public GetByIdMealResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdMealResponse()
        {
        }

        public MealDto Meal { get; set; }
    }
}