using System;
using PublicApi.Util;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class CreateFoodStockRequest : BaseRequest 
    {
        public int UserId { get; set; }
        public int FoodProductId { get; set; }

        public float Amount { get; set; }

        public float? UpperAmount { get; set; }

        public float? LowerAmount { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public DateTime? BestUseByDate { get; set; }
    }
}