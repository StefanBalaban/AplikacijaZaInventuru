using PublicApi.Util.FoodProductEndpoints;
using System;

namespace PublicApi.Util.FoodProductEndpoints
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