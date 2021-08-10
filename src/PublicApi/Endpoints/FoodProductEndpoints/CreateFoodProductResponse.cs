using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class CreateFoodProductResponse : BaseResponse
    {
        public CreateFoodProductResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateFoodProductResponse()
        {
        }

        public FoodProductDto FoodProduct { get; set; }
    }
}