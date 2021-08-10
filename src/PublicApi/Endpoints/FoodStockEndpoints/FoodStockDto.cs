using System;

namespace PublicApi.Endpoints.FoodStockEndpoints
{
    public class FoodStockDto
    {
        public int Id { get; set; }
        public int FoodProductId { get; set; }

        public float Amount { get; set; }

        public float? UpperAmount { get; set; }

        public float? LowerAmount { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public DateTime? BestUseByDate { get; set; }
    }
}