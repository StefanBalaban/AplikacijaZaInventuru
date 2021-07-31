using Ardalis.GuardClauses;

namespace ApplicationCore.Entities.MealAggregate
{
    public class MealItem : BaseEntity
    {
        public FoodProduct FoodProduct { get; private set; }
        public float Amount { get; private set; }

        public MealItem()
        {
        }
    }
}