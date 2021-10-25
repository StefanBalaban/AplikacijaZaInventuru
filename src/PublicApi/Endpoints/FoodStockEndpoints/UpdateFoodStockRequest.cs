using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class UpdateFoodStockRequest : BaseRequest
    {
        public int Id { get; set; }
        public float Amount { get; set; }

        public float? UpperAmount { get; set; }

        public float? LowerAmount { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public DateTime? BestUseByDate { get; set; }
        public int FoodProductId { get; set; }
    }
}