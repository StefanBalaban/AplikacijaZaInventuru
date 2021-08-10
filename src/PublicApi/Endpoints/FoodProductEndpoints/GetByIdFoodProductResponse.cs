using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class GetByIdFoodProductResponse : BaseResponse
    {
        public GetByIdFoodProductResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdFoodProductResponse()
        {
        }

        public FoodProductDto FoodProduct { get; set; }
    }
}