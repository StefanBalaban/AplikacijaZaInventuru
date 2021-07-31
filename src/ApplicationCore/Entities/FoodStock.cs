using Ardalis.GuardClauses;
using System;

namespace ApplicationCore.Entities
{
    public class FoodStock : BaseEntity
    {
        public FoodProduct FoodProduct { get; private set; }
        public float Amount { get; private set; }
        public float? UpperAmount { get; private set; }
        public float? LowerAmount { get; private set; }
        public DateTime? DateOfPurchase { get; private set; }
        public DateTime? BestUseByDate { get; set; }

        public FoodStock()
        {
        }
    }
}