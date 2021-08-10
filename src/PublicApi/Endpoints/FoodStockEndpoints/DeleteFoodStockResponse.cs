using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class DeleteFoodStockResponse : BaseResponse
    {
        public DeleteFoodStockResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteFoodStockResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}