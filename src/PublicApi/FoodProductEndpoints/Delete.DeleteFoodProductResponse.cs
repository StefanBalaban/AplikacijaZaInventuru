using System;

namespace PublicApi.Util.FoodProductEndpoints
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