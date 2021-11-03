using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApplicationCore.Constants;
using ApplicationCore.Entities.MealAggregate;
using ApplicationCore.Filters;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Entities.DietPlanAggregate
{
    public class DietPlan : BaseEntity, IAggregateRoot
    {
        [Dto] [Get(FilterConstants.INCLUDE)] [Post] [Put] [Required] public List<DietPlanMeal> DietPlanMeals { get; set; }

        [Dto] [Get(FilterConstants.EQUAL)] [Post] [Put] [Required] public string Name { get; set; }
        [Dto] [Get(FilterConstants.EQUAL)] [Post] [Put] [Required] public int UserId { get; set; }

    }
}