using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodProductEndpoints
{
    public class DeleteFoodProductResponse : BaseResponse
    {
        public DeleteFoodProductResponse(Guid correlationId) : base(correlationId)
        {
        }

        public DeleteFoodProductResponse()
        {
        }

        public string Status { get; set; } = "Deleted";
    }
}