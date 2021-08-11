using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.DietPlanAggregate;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.MealAggregate
{
    public class Meal : BaseEntity, IAggregateRoot
    {
        [Dto] [Get] [Post] [Put] [Required] public List<MealItem> Meals { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public string Name { get; set; }
        public List<DietPlanMeal> DietPlanMeals { get; set; }
    }
}