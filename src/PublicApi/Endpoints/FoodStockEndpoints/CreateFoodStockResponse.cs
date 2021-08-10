using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class CreateFoodStockResponse : BaseResponse
    {
        public CreateFoodStockResponse(Guid correlationId) : base(correlationId)
        {
        }

        public CreateFoodStockResponse()
        {
        }

        public FoodStockDto FoodStock { get; set; }
    }
}