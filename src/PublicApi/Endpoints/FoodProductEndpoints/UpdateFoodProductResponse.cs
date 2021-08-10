using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class UpdateFoodProductResponse : BaseResponse
    {
        public UpdateFoodProductResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateFoodProductResponse()
        {
        }

        public FoodProductDto FoodProduct { get; set; }
    }
}