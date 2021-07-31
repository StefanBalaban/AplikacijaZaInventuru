using Ardalis.GuardClauses;
using ApplicationCore.Entities.MealAggregate;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Entities.DietPlanAggregate
{
    public class DietPlan : BaseEntity
    {
        private readonly List<Meal> _meals = new List<Meal>();
        public IReadOnlyList<Meal> Meals => _meals.AsReadOnly();
        public string Name { get; private set; }

        public DietPlan()
        {
        }
    }
}