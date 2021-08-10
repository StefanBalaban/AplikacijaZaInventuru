using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class UpdateFoodStockResponse : BaseResponse
    {
        public UpdateFoodStockResponse(Guid correlationId) : base(correlationId)
        {
        }

        public UpdateFoodStockResponse()
        {
        }

        public FoodStockDto FoodStock { get; set; }
    }
}