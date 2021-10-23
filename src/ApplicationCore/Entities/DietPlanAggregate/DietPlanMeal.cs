using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.DietPlanAggregate
{
    public class DietPlanMeal
    {
        public int DietPlanId { get; set; }
        public DietPlan DietPlan { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }

    }
}
