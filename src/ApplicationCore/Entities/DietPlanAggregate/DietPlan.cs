using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Filters;

namespace ApplicationCore.Entities.DietPlanAggregate
{
    public class DietPlan : BaseEntity
    {
        [Dto] [Get] [Post] [Put] [Required] public List<Meal> Meals { get; set; }

        [Dto] [Get] [Post] [Put] [Required] public string Name { get; set; }
    }
}