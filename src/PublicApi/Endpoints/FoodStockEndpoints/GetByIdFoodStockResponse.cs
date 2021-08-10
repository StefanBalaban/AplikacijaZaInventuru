using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class GetByIdFoodStockResponse : BaseResponse
    {
        public GetByIdFoodStockResponse(Guid correlationId) : base(correlationId)
        {
        }

        public GetByIdFoodStockResponse()
        {
        }

        public FoodStockDto FoodStock { get; set; }
    }
}